using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Core.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Orders.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
     [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    public class BaseController : Controller
    {
      
        protected async Task<APIResponseViewModel> GetAPIResponse(object data) {
            return new APIResponseViewModel(true, "done", data);
        }
    }
}
