using ProjektZTP.Data;
using ProjektZTP.Models;
using System.Threading.Tasks;

namespace ProjektZTP.Services
{
    public class WordsService
    {
        #region Properties

        private DbConnection _connection;

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

        #endregion Methods
    }
}