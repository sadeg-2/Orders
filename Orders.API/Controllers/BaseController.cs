using Microsoft.AspNetCore.Mvc;
using Orders.Core.ViewModels;
using System.Threading.Tasks;

namespace Orders.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : Controller
    {
      
        protected async Task<APIResponseViewModel> GetAPIResponse(object data) {
            return new APIResponseViewModel(true, "done", data);
        }
    }
}
