using Orders.Core.Dtos;
using Orders.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Services.Resturants
{
    public interface IReustarantService 
    {
        Task<List<ResturantViewModel>> GetAll(string searchKey);
        Task<List<ResturantViewModel>> NearMe(string userId);
        Task<int> Create(CreateResturantDto dto);

    }
}
