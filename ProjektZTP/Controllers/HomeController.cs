using ProjektZTP.Data;
using System.Web.Mvc;

namespace ProjektZTP.Controllers
{
    public class HomeController : Controller
    {
        private DbConnection _connection = DbConnection.GetDbConnection();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}