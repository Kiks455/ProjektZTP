using ProjektZTP.Data;
using ProjektZTP.Models;
using ProjektZTP.Patterns.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Troschuetz.Random;
using static ProjektZTP.Models.QuestionViewModels;

namespace ProjektZTP.Patterns
{
    public class QuestionConnector
    {
        private DbConnection db;
        public List<QuestionModel> Questions;
        private Iterator.Iterator _iterator;
        private List<Word> _answers;
        private TRandom random;

        public QuestionConnector(DbConnection connection)
        {
            db = connection;
        }

        public QuestionModel GetQuestion()
        {
            if (_iterator == null)
            {
                string difficulty = (string)System.Web.HttpContext.Current.Session["difficulty"];
                _iterator = new Iterator.Iterator(db, difficulty);
            }
            if (Questions == null)
            {
                Questions = new List<QuestionModel>();
            }

            _iterator.Questions = Questions;

            if (!_iterator.IsDone())
            {
                var currentWord = _iterator.Next();
                GetAnswers(currentWord);
                var answers = ShuffleList(_answers);
                var newQuestion = new QuestionModel()
                {
                    Answers = answers,
                    Word = currentWord,
                    QuestionNumber = Questions.Count

                };

                HttpContext.Current.Session["answers"] = answers;
                Questions.Add(newQuestion);

                return newQuestion;
            }

            return null;
        }

        private void GetAnswers(Word currentWord)
        {
            int level = GetDifficultyValue();

            if (random == null)
            {
                random = new TRandom();
            }

            int builderType = random.Next(0, 2);

            if (builderType == 0)
            {
                _answers = DelegateBuilder("SameLetter", db, currentWord);
            }
            else if (builderType == 1)
            {

                _answers = DelegateBuilder("SameLength", db, currentWord);
            }
            else
            {
                _answers = null;
            }
        }

        private List<Word> DelegateBuilder(string buildingScheme, DbConnection db, Word correctAnswer)
        {
            AnswerBuilder builder = null;
            switch (buildingScheme)
            {
                case "SameLetter":
                    {
                        builder = new SameLetterBuilder(correctAnswer, db);
                        break;
                    }
                case "SameLength":
                    {
                        builder = new SameLengthBuilder(correctAnswer, db);
                        break;
                    }
                default:
                    {
                        builder = new SameLetterBuilder(correctAnswer, db);
                        break;
                    }
            }

            AnswerDirector director = new AnswerDirector();
            director.Construct(builder, GetDifficultyValue());

            return builder.GetResult();
        }

        private int GetDifficultyValue()
        {
            string difficulty = (string)System.Web.HttpContext.Current.Session["difficulty"];
            if (difficulty == null)
            {
                difficulty = "easy";
            }

            int difficultyValue = 0;
            switch (difficulty)
            {
                case "easy":
                    {
                        difficultyValue = 0;
                        break;
                    }

                case "medium":
                    {
                        difficultyValue = 1;
                        break;
                    }

                case "hard":
                    {
                        difficultyValue = 2;
                        break;
                    }
            }

            return difficultyValue;
        }

        public List<Word> ShuffleList(List<Word> list)
        {
            var random = new TRandom();
            int size = list.Count;
            List<Word> shuffledList = new List<Word>();
            int tempSize = size;

            for(int i = 0; i < tempSize; i++)
            {
                int randomIdx = random.Next() % size;
                shuffledList.Add(list[randomIdx]);
                list.RemoveAt(randomIdx);
                size = list.Count;
            }

            return shuffledList;
        }
    }
}