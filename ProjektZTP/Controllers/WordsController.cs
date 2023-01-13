using ProjektZTP.Data;
using ProjektZTP.Models;
using ProjektZTP.Services;
using System.Threading.Tasks;
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

        public async Task<ActionResult> Index(int pageNumber, string filterValue, string filterLang)
        {
            ReadWordsDTO words = await _wordsService.GetWords(pageNumber, filterValue, filterLang);

            ViewBag.pageNumber = pageNumber;
            ViewBag.lastPageNumber = words.LastPageNumber;
            ViewBag.filterValue = filterValue;
            ViewBag.filterLang = filterLang;

            return View(words.Words);
        }

        #endregion Methods
    }
}