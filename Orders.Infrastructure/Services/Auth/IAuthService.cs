using Orders.Core.Dtos;
using Orders.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Services.Auth
{
    public interface IAuthService
    {
        public Task<LoginResponseViewModel> login(loginDto dto);
    }
}
