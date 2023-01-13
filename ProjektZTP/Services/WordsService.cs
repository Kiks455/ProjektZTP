using ProjektZTP.Data;
using ProjektZTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public List<Word> GetWords()
        {
            List<Word> result = _connection.GetWords();

            return result;
        }

        #endregion Methods
    }
}