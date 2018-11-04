using System.Web.Mvc;
using Grademoepi.Models.Pages;
using Grademoepi.Models.ViewModels;
using EPiServer.Web;
using EPiServer.Web.Mvc;

namespace Grademoepi.Controllers
{
    public class StartPageController : PageControllerBase<StartPage>
    {
        public ActionResult Index(StartPage currentPage)
        {
            var model = PageViewModel.Create(currentPage);
            return View(model);
        }

    }
}
