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

        public ActionResult CreateWord(string WordEn, string WordPl)
        {
            if (ModelState.IsValid)
            {
                _wordsService.AddWord(WordEn, WordPl);
            }

            return RedirectToAction("Index", new { pageNumber = 1, filterValue = "", filterLang = "" });
        }

        public ActionResult EditWord(int id, string wordEn, string wordPl)
        {
            if (ModelState.IsValid)
            {
                _wordsService.UpdateWord(id, wordEn, wordPl);
            }

            return RedirectToAction("Index", new { pageNumber = 1, filterValue = "", filterLang = "" });
        }

        public ActionResult DeleteWord(int id)
        {
            if (ModelState.IsValid)
            {
                _wordsService.DeleteWord(id);
            }

            return RedirectToAction("Index", new { pageNumber = 1, filterValue = "", filterLang = "" });
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id, string wordEn, string wordPl)
        {
            Word word = new Word()
            {
                Id = id,
                WordEn = wordEn,
                WordPl = wordPl
            };

            return View(word);
        }

        public ActionResult Delete(int id)
        {
            Word word = new Word()
            {
                Id = id
            };

            return View(word);
        }

        #endregion Methods
    }
}