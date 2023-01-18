using ProjektZTP.Data;
using ProjektZTP.Models;
using System.Threading.Tasks;

namespace ProjektZTP.Services
{
    public class WordsService
    {
        #region Properties

        private readonly DbConnection _connection;

        #endregion Properties

        #region Constructors

        public WordsService(DbConnection connection)
        {
            _connection = connection;
        }

        #endregion Constructors

        #region Methods

        public async Task<ReadWordsDTO> GetWords(int pageNumber, string filterValue, string filterLang)
        {
            ReadWordsDTO result = await _connection.GetWords(pageNumber, filterValue, filterLang);

            return result;
        }

        public void AddWord(string engWord, string plWord)
        {
            _connection.AddWord(engWord, plWord);
        }

        public void UpdateWord(int id, string wordEn, string wordPl)
        {
            Word word = new Word()
            {
                Id = id,
                WordEn = wordEn,
                WordPl = wordPl
            };

            _connection.UpdateWord(word);
        }

        public void DeleteWord(int id)
        {
            _connection.RemoveWord(id);
        }

        #endregion Methods
    }
}