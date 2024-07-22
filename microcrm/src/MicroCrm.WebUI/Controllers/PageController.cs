using Microsoft.AspNetCore.Mvc;

namespace MicroCrm.WebUI.Controllers
{
    public class PageController : Controller
    {
       
        public IActionResult Error() => View();
        public IActionResult Error404() => View();
      
    }
}
