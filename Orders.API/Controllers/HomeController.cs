using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Orders.API.Controllers
{

    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }

      
    }
}
