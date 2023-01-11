using ProjektZTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektZTP.Data
{
    public sealed class DbConnection
    {
        private static DbConnection _connection = new DbConnection();
        private ApplicationDbContext _context;
        private string _languageChosen = "eng";

        private DbConnection()
        {
            _context = new ApplicationDbContext();
        }

        public static DbConnection GetDbConnection()
        {
            return _connection;
        }

        public Word GetRandomWord()
        {
            List<Word> words = _context.Words.ToList();

            Random random = new Random();
            int index = random.Next(words.Count);

            Word result = words[index];

            return result;
        }

        public Word GetSameLetterWord(string word)
        {
            Word result;
            string letter = word[0].ToString();

            if (_languageChosen == "eng")
            {
                result = _context.Words.FirstOrDefault(e => e.WordEn.StartsWith(letter));
            }
            else
            {
                result = _context.Words.FirstOrDefault(e => e.WordPl.StartsWith(letter));
            }

            return result;
        }

        public Word GetSameLengthWord(string word)
        {
            Word result;

            if (_languageChosen == "eng")
            {
                result = _context.Words.FirstOrDefault(e => e.WordEn.Length == word.Length);
            }
            else
            {
                result = _context.Words.FirstOrDefault(e => e.WordPl.Length == word.Length);
            }

            return result;
        }

        public List<Word> GetWords()
        {
            List<Word> result = _context.Words.ToList();

            return result;
        }

        public Word GetWord(int id)
        {
            Word result = _context.Words.SingleOrDefault(e => e.Id == id);

            return result;
        }
    }
}