using AutoMapper;
using Orders.API.Data;
using Orders.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Orders.Data.Models;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Orders.Core.Dtos;

namespace Orders.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly OrderDbContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(OrderDbContext db, IMapper mapper, UserManager<User> userManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<UserViewModel>> GetAll(string searchKey) {
            var users = await _db.Users.Where(x=> !x.IsDelete &&
            (string.IsNullOrEmpty(searchKey) || x.FullName.Contains(searchKey)|| x.PhoneNumber.Contains(searchKey))).ToListAsync();

            return _mapper.Map<List<UserViewModel>>(users);
        }

        public async Task<UserViewModel> Get(string id)
        {
            var user = _db.Users.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == id);
            if (user == null) {
                throw new Exception("User Id not Found");
            }
            return _mapper.Map<UserViewModel>(user);
        }
        public async Task<string> Create(CreateUserDto dto)
        {
            var isExist = _db.Users.Any(x => !x.IsDelete &&
            (x.FullName == dto.FullName || x.PhoneNumber == dto.PhoneNumber)
            );
            if (isExist)
            {
                // Throw Exception
                throw new Exception("User name or phone number is already exist ");
            }
            var user = _mapper.Map<User>(dto);
            user.UserName = dto.PhoneNumber;
            //user.Discriminator = "f";
            var r = await _userManager.CreateAsync(user, dto.Password);
            return user.Id;


        }

        public async Task<string> Update(UpdateUserDto dto) {
            var user = await _db.Users.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (user == null)
            {
                throw new Exception("User Id not Found");
            }
            var userUpdated = _mapper.Map(dto, user);
            _db.Users.Update(userUpdated);
            _db.SaveChanges();
            return userUpdated.Id;
        }

        public async Task<string> Delete(string id) {
            var user = await _db.Users.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == id);
            if (user == null)
            {
                throw new Exception("User Id not Found");
            }
            user.IsDelete = true;
            _db.Users.Update(user);
            _db.SaveChanges();
            return user.Id;
        }

    }
}
