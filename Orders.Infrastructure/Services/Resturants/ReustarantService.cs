using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Orders.API.Data;
using Orders.Core.Dtos;
using Orders.Core.ViewModel;
using Orders.Data.Models;
using Orders.Infrastructure.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Services.Resturants
{
    public class ReustarantService : IReustarantService
    {
        private readonly OrderDbContext _db;
        private readonly IMapper _mapper;

        public ReustarantService(OrderDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<List<ResturantViewModel>> GetAll(string searchKey) {
            var resturant = await _db.Resturants.Include(x => x.Meals).
                Where(x => x.Name.Contains(searchKey) || string.IsNullOrWhiteSpace(searchKey))
                .OrderByDescending(x => x.Meals.Count()).ToListAsync();
        
            return _mapper.Map<List<ResturantViewModel>>(resturant);
        }

        public async Task<List<ResturantViewModel>> NearMe(string userId) {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == userId);
            var resturant = await _db.Resturants.ToListAsync();
            var distances = new Dictionary<int, double>();
            var userLocation = new Coordinates((double)user.Latitude, (double)user.Longitude);

            foreach (var res in resturant) {
                var resturantLocation = new Coordinates((double)res.Latitude, (double)res.Longitude);
                var distanceKm = userLocation.DistanceTo(resturantLocation);
                distances.Add(res.Id, distanceKm);
            }

            var nearIds = distances.OrderBy(x => x.Value).Take(5).Select(x=> x.Key).ToList();
            
            var nearResturant =  _db.Resturants.Where(x=> nearIds.Contains(x.Id)).ToList();

            return _mapper.Map<List<ResturantViewModel>>(nearResturant);

        
        }

        public async Task<int> Create(CreateResturantDto dto) {
            var resturant = _mapper.Map<Resturant>(dto);
            await _db.Resturants.AddAsync(resturant);
            await _db.SaveChangesAsync();
            return resturant.Id;
        
        }
    }
}
