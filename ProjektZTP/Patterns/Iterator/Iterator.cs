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
        private string _language;
        private string _level;
        public Iterator(DbConnection connection, string level)
        {
            db = connection;
            _level = level;
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
                int id = random.Next(0, 59224);
                word = db.GetWord(id);

                _language = (string)System.Web.HttpContext.Current.Session["lang"];

                switch (_level)
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


                if (Questions.All(q => q.Word.Id != word.Id))
                {
                    return word;
                }
            }
        }

        private bool CheckForEasy()
        {
            if (_language == "pl")
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
            if (_language == "Eng")
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
            if (_language == "Eng")
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