using ProjektZTP.Data;
using ProjektZTP.Models;
using ProjektZTP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektZTP.Controllers
{
    public class WordsController : Controller
    {
        #region Properties

        private DbConnection _connection;
        private WordsService _wordsService;

        #endregion Properties

        #region Constructors

        public WordsController()
        {
            _connection = DbConnection.GetDbConnection();
            _wordsService = new WordsService(_connection);
        }

        #endregion Constructors

        #region Methods

        public ActionResult Index()
        {
            List<Word> words = _wordsService.GetWords();

            return View(words);
        }

        #endregion Methods
    }
}