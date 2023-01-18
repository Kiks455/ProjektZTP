using ProjektZTP.Data;
using ProjektZTP.Models;
using System.Collections.Generic;

namespace ProjektZTP.Patterns.Builder
{
    public class SameLengthBuilder : AnswerBuilder
    {
        private List<Word> words;
        private Word correctAnswer;
        private DbConnection db ;

        public SameLengthBuilder(Word correctAnswer, DbConnection connection)
        {
            words = new List<Word>();
            db = connection;
            this.correctAnswer = correctAnswer;
        }

        public void FetchRandWord()
        {
            Word word = null;
            do
            {
                word = db.GetRandomWord();
            } while (word == correctAnswer);
            words.Add(word);
        }

        public void SetCorrectAnswer()
        {
            words.Add(correctAnswer);
        }

        public void FetchSpecialWord()
        {
            Word word = null;
            do
            {
                word = db.GetSameLengthWord(correctAnswer);
            } while (word == correctAnswer);
            words.Add(word);
        }

        public List<Word> GetResult()
        {
            return words;
        }
    }
}