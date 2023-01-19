using ProjektZTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ProjektZTP.Data
{
    public sealed class DbConnection
    {
        #region Properties

        private static DbConnection _connection = new DbConnection();
        private ApplicationDbContext _context;
        private int pageSize;

        #endregion Properties

        #region Constructors

        private DbConnection()
        {
            _context = new ApplicationDbContext();
            pageSize = 50;
        }

        #endregion Constructors

        #region Methods

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

            string languageChosen = (string)System.Web.HttpContext.Current.Session["lang"];

            if (languageChosen == "eng")
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

            string languageChosen = (string)System.Web.HttpContext.Current.Session["lang"];

            if (languageChosen == "eng")
            {
                result = _context.Words.OrderBy(e => Guid.NewGuid()).FirstOrDefault(e => e.WordEn.Length == word.WordEn.Length);
            }
            else
            {
                result = _context.Words.OrderBy(e => Guid.NewGuid()).FirstOrDefault(e => e.WordPl.Length == word.WordPl.Length);
            }

            return result;
        }

        public async Task<ReadWordsDTO> GetWords(int pageNumber, string filterValue, string filterLang)
        {
            IQueryable<Word> words = _context.Words;

            string languageChosen = (string)System.Web.HttpContext.Current.Session["lang"];

            if (filterValue != null)
            {
                if (filterLang == "eng")
                {
                    words = words.Where(e => e.WordEn.StartsWith(filterValue));
                }
                else
                {
                    words = words.Where(e => e.WordPl.StartsWith(filterValue));
                }
            }

            if (languageChosen == "eng")
            {
                words = words.OrderBy(e => e.WordEn);
            }
            else
            {
                words = words.OrderBy(e => e.WordPl);
            }

            int count = words.Count();
            int lastPageNumber = (int)Math.Ceiling((double)count / pageSize);
            IEnumerable<Word> wordList = await words.ToPagedListAsync(pageNumber, pageSize);

            ReadWordsDTO result = new ReadWordsDTO()
            {
                Words = wordList,
                LastPageNumber = lastPageNumber
            };

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

        public ApplicationUser GetUserById(string id)
        {
            ApplicationUser user = _context.Users.SingleOrDefault(e => e.Id == id);

            return user;
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            ApplicationUser user = _context.Users.SingleOrDefault(e => e.Email == email);

            return user;
        }

        public void SetUserScore(string id, int newScore)
        {
            ApplicationUser user = _context.Users.SingleOrDefault(e => e.Id == id);

            if (user != default)
            {
                _context.Entry(user).CurrentValues["Score"] = newScore;
                _context.SaveChanges();
            }
        }

        public int GetUserScore(string id)
        {
            ApplicationUser user = _context.Users.SingleOrDefault(e => e.Id == id);

            if (user != default)
            {
                return user.Score;
            }

            return 0;
        }

        public void SetUserLevel(string id, int newLevel)
        {
            ApplicationUser user = _context.Users.SingleOrDefault(e => e.Id == id);

            if (user != default)
            {
                _context.Entry(user).CurrentValues["Level"] = newLevel;
                _context.SaveChanges();
            }
        }

        public int GetUserLevel(string id)
        {
            ApplicationUser user = _context.Users.SingleOrDefault(e => e.Id == id);

            if (user != default)
            {
                return user.Level;
            }

            return 0;
        }

        public void SetUserLang(string email, string lang)
        {
            ApplicationUser user = _context.Users.SingleOrDefault(e => e.Email == email);

            if (user != default)
            {
                _context.Entry(user).CurrentValues["Lang"] = lang;
                _context.SaveChanges();
            }
        }

        #endregion Methods
    }
}