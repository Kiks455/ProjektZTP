using ProjektZTP.Data;
using ProjektZTP.Models;
using ProjektZTP.Patterns.Builder;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjektZTP.Controllers
{
    public class QuizController : Controller
    {
        private DbConnection db = DbConnection.GetDbConnection();

        // GET: Quiz
        public ActionResult Index()
        {
            Word question = db.GetRandomWord();

            AnswerBuilder builder = new SameLengthBuilder(question, db);

            AnswerDirector director = new AnswerDirector();
            director.Construct(builder, 0);
            List<Word> answers = builder.GetResult();

            ViewBag.Question = question;
            ViewBag.Answers = answers;

            return View();
        }
    }
}