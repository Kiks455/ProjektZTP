using ProjektZTP.Data;
using ProjektZTP.Services;
using System.Web.Mvc;

namespace ProjektZTP.Controllers
{
    public class LangController : Controller
    {
        private readonly DbConnection _connection;
        private readonly LangService _langService;

        public LangController()
        {
            _connection = DbConnection.GetDbConnection();
            _langService = new LangService(_connection);
        }

        // GET: Lang
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChooseLanguage(string lang)
        {
            var username = System.Web.HttpContext.Current.User.Identity.Name;

            _langService.SetUserLang(username, lang);

            Session["lang"] = lang;

            return RedirectToAction("Index", "Home");
        }
    }
}