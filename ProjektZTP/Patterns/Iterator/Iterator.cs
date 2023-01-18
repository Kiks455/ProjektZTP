using ProjektZTP.Data;
using ProjektZTP.Models;
using ProjektZTP.Patterns.State;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Troschuetz.Random;
using static ProjektZTP.Models.QuestionViewModels;


namespace ProjektZTP.Patterns.Iterator
{
    public class Iterator : InterfaceIterator
    {
        private DbConnection db;
        public List<QuestionModel> Questions;
        private TRandom random;
        private Word word;
        private string mode;
        public Iterator(DbConnection connection)
        {
            db = connection;
        }

        public QuestionModel First()
        {
            return Questions[0];
        }

        public Word Next()
        {
            if (random == null)
            {
                random = new TRandom();
            }
            while (true)
            {
                int id = random.Next(0, 59225);
                word = db.GetWord(id);
                mode = (string)System.Web.HttpContext.Current.Session["lang"];
                string level = (string)System.Web.HttpContext.Current.Session["difficulty"];
                if (level != null)
                {
                    if (level == "continious")
                    {
                        level = CheckLevel();
                    }
                    switch (level)
                    {
                        case "easy":
                            if (!CheckForEasy())
                            {
                                continue;
                            }
                            break;

                        case "medium":
                            if (!CheckForMedium())
                            {
                                continue;
                            }
                            break;

                        case "hard":
                            if (!CheckForHard())
                            {
                                continue;
                            }
                            break;

                        default:
                            break;
                    }
                }

                if (Questions.All(q => q.Word.Id != word.Id))
                {
                    return word;
                }
            }
        }

        private string CheckLevel()
        {
            var username = HttpContext.Current.User.Identity.Name;
            var user = db.GetUserByEmail(username);
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

        private bool CheckForEasy()
        {
            if (mode == "pl")
            {
                if (word.WordEn.Length < 6)
                {
                    return true;
                }
            }
            else
            {
                if (word.WordPl.Length < 6)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckForMedium()
        {
            if (mode == "Eng")
            {
                if (word.WordEn.Length > 6 && word.WordEn.Length < 12)
                {
                    return true;
                }
            }
            else
            {
                if (word.WordPl.Length > 6 && word.WordPl.Length < 12)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckForHard()
        {
            if (mode == "Eng")
            {
                if (word.WordEn.Length > 10)
                {
                    return true;
                }
            }
            else
            {
                if (word.WordPl.Length > 10)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsDone()
        {
            var type = (Context)HttpContext.Current.Session["mode"];
            State.State state = type.GetState();
            if (state is LearningState) 
            {
                return false;
            }

            if (Questions.Count == 10)
            {
                return true;
            }
            return false;
        }

        public QuestionModel CurrentItem()
        {
            return Questions[Questions.Count - 1];
        }
    }
}