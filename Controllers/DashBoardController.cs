using Microsoft.AspNetCore.Mvc;

namespace AhmedStore.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
