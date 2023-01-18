using ProjektZTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektZTP.Patterns.State
{
    public class TestState : State
    {
        private int Points = 0;

        public bool CheckAnswer(Word question, Word answer)
        {
            var lang = HttpContext.Current.Session["lang"];

            switch (lang)
            {
                case "eng":
                    {
                        return question.WordEn == answer.WordEn ? true : false;
                    }
                case "pl":
                    {
                        return question.WordPl == answer.WordPl ? true : false;
                    }
                default:
                    return false;
            }
        }

        public void SetPoints(int points)
        {
            Points += 1;
        }

        public int GetPoints()
        {
            return Points;
        }
    }
}