using ProjektZTP.Data;
using ProjektZTP.Models;
using ProjektZTP.Patterns;
using ProjektZTP.Patterns.State;
using ProjektZTP.Services;
using System.Collections.Generic;
using System.Web.Mvc;
using static ProjektZTP.Models.QuestionViewModels;

namespace ProjektZTP.Controllers
{
    public class QuizController : Controller
    {
        private readonly DbConnection db;
        private readonly UserService _userService;
        private Context _context;

        public QuizController()
        {
            db = DbConnection.GetDbConnection();
            _userService = new UserService(db);
            _context = new Context();
        }

        // GET: Quiz
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Question()
        {
            var context = (Context)Session["context"];
            if (context == null)
            {
                return RedirectToAction("Index", "Home");
            }

            QuestionConnector connector = (QuestionConnector)Session["connector"];
            if (connector == null)
            {
                connector = new QuestionConnector(db);
                Session["connector"] = connector;
            }

            var question = connector.GetQuestion();

            string mode;
            State state = context.GetState();
            if (state is LearningState)
            {
                mode = "learning";
            }
            else
            {
                mode = "test";
            }

            if (question == null)
            {
                return RedirectToAction("Summary");
            }

            AnsweredQuestionModel model = new AnsweredQuestionModel()
            {
                Answers = question.Answers,
                Word = question.Word,
                QuestionNumber = question.QuestionNumber + 1,
                Mode = mode,
                Lang = (string)Session["lang"]
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Question(AnsweredQuestionModel model)
        {
            if (model.AnswerId != -1)
            {
                model.Answers = (List<Word>)Session["answers"];
                var context = (Context)Session["context"];
                State state = context.GetState();
                var result = context.GetState().CheckAnswer(model.Word, model.Answers[model.AnswerId]);
                if (state is LearningState)
                {
                    if (result == true)
                    {
                        return RedirectToAction("Question");
                    }
                    ViewBag.Message = "Wrong answer";
                    return View(model);
                }
                if (state is TestState)
                {
                    if (result == true)
                    {
                        state.SetPoints(1);
                    }

                    return RedirectToAction("Question");
                }
            }

            ViewBag.Message = "Pick answer";
            return View(model);
        }

        public ActionResult QuestionHard()
        {
            var context = (Context)Session["context"];
            if (context == null)
            {
                return RedirectToAction("Index", "Home");
            }

            QuestionConnector connector = (QuestionConnector)Session["connector"];
            if (connector == null)
            {
                connector = new QuestionConnector(db);
                Session["connector"] = connector;
            }

            var question = connector.GetQuestion();

            string mode;
            State state = context.GetState();
            if (state is LearningState)
            {
                mode = "learning";
            }
            else
            {
                mode = "test";
            }

            if (question == null)
            {
                return RedirectToAction("Summary");
            }

            QuestionHardModel model = new QuestionHardModel()
            {
                Lang = (string)Session["lang"],
                Mode = mode,
                QuestionNumber = question.QuestionNumber + 1,
                Word = question.Word
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult QuestionHard(QuestionHardModel model)
        {
            if (!string.IsNullOrEmpty(model.Answer))
            {
                var context = (Context)Session["context"];
                State state = context.GetState();
                var result = context.GetState().CheckAnswer(model.Word, new Word { WordEn = model.Answer, WordPl = model.Answer });
                if (state is LearningState)
                {
                    if (result == true)
                    {
                        return RedirectToAction("QuestionHard");
                    }
                    ViewBag.Message = "Wrong answer";
                    return View(model);
                }
                if (state is TestState)
                {
                    if (result == true)
                    {
                        state.SetPoints(1);
                    }

                    return RedirectToAction("QuestionHard");
                }
            }

            ViewBag.Message = "Pick answer";
            return View(model);
        }

        public ActionResult Summary()
        {
            var context = (Context)Session["context"];
            if (context == null)
            {
                return RedirectToAction("Index", "Home");
            }
            State state = context.GetState();
            if (state is LearningState)
            {
                return RedirectToAction("Index", "Home");
            }

            var points = state.GetPoints();
            ViewBag.Points = (double)points;

            var user = _userService.GetUserByEmail(User.Identity.Name);
            UpdateUserPoints(points);
            ViewBag.Level = user.Level;
            ViewBag.userPoints = (double)user.Score;
            ViewBag.MaxPoints = 10;
            Session["connector"] = null;
            Session["context"] = null;

            return View();
        }

        public ActionResult SelectDifficulty()
        {
            Session["connector"] = null;
            switch (Request.QueryString["mode"])
            {
                case "learning":
                    {
                        _context.ChangeState(StateMode.Learning);
                        break;
                    }
                case "test":
                    {
                        _context.ChangeState(StateMode.Test);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            Session["context"] = _context;
            return View();
        }

        public ActionResult ConfirmDifficulty()
        {
            var context = (Context)Session["context"];
            if (context == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Session["connector"] = null;

            if (Request.QueryString["difficulty"] == null)
            {
                return RedirectToAction("SelectDifficulty");
            }

            string difficulty = "";
            if (Request.QueryString["difficulty"] == "basedOnLevel")
            {
                difficulty = GetDifficulty();
                Session["difficulty"] = difficulty;
            }
            else
            {
                difficulty = Request.QueryString["difficulty"];
                Session["difficulty"] = difficulty;
            }

            if (difficulty == "hard")
            {
                return RedirectToAction("QuestionHard");
            }

            return RedirectToAction("Question");
        }

        private string GetDifficulty()
        {
            var userName = User.Identity.Name;
            var user = _userService.GetUserByEmail(userName);
            var level = user.Level;

            if (level < 5)
            {
                return "easy";
            }
            if (level < 10)
            {
                return "medium";
            }

            return "hard";
        }

        private void UpdateUserPoints(int points)
        {
            var userName = User.Identity.Name;
            ApplicationUser user = _userService.GetUserByEmail(userName);

            if (user == null)
                return;

            int userScore = _userService.GetUserScore(user.Id);
            int userLevel = _userService.GetUserLevel(user.Id);

            userScore += points;
            if (userScore >= 10)
            {
                userLevel++;
                userScore -= 10;
            }

            _userService.SetUserScore(user.Id, userScore);
            _userService.SetUserLevel(user.Id, userLevel);
        }
    }
}