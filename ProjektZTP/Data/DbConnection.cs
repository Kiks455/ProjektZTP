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

        public Word GetSameLetterWord(Word word)
        {
            Word result;
            
            if (_languageChosen == "eng")
            {
                string letter = word.WordEn[0].ToString();
                result = _context.Words.OrderBy(e => Guid.NewGuid()).FirstOrDefault(e => e.WordEn.StartsWith(letter));
            }
            else
            {
                string letter = word.WordPl[0].ToString();
                result = _context.Words.OrderBy(e => Guid.NewGuid()).FirstOrDefault(e => e.WordPl.StartsWith(letter));
            }

            return result;
        }

        public Word GetSameLengthWord(Word word)
        {
            Word result;

            if (_languageChosen == "eng")
            {
                result = _context.Words.OrderBy(e => Guid.NewGuid()).FirstOrDefault(e => e.WordEn.Length == word.WordEn.Length);
            }
            else
            {
                result = _context.Words.OrderBy(e => Guid.NewGuid()).FirstOrDefault(e => e.WordPl.Length == word.WordPl.Length);
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

        public void AddWord(string engWord, string plWord)
        {
            Word word = new Word()
            {
                WordEn = engWord,
                WordPl = plWord
            };

            _context.Words.Add(word);
            _context.SaveChanges();
        }

        public void UpdateWord(Word word)
        {
            Word oldWord = _context.Words.SingleOrDefault(e => e.Id == word.Id);

            if (oldWord != default)
            {
                _context.Entry(oldWord).CurrentValues.SetValues(word);
                _context.SaveChanges();
            }
        }

        public void RemoveWord(int id)
        {
            Word word = _context.Words.SingleOrDefault(e => e.Id == id);

            _context.Words.Remove(word);
            _context.SaveChanges();
        }

        public ApplicationUser GetUser(string id)
        {
            ApplicationUser user = _context.Users.SingleOrDefault(e => e.Id == id);

            return user;
        }
    }
}