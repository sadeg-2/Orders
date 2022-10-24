using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Orders.API.Data;
using Orders.Core.Constant;
using Orders.Core.Dtos;
using Orders.Core.Exceptions;
using Orders.Core.Options;
using Orders.Core.ViewModel;
using Orders.Core.ViewModels;
using Orders.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly OrderDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtOptions _jwtOptions;

        public AuthService(OrderDbContext db, UserManager<User> userManager,IMapper mapper,IOptions<JwtOptions> jwtOptions) {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value;
        }

        
        public async Task<LoginResponseViewModel> login(loginDto dto)
        {
            var user = _db.Users.SingleOrDefault(x => !x.IsDelete &&x.UserName == dto.UserName);

            if (user == null)
            {
                throw new InvalidUserNameOrPassword();
            }
            bool ch = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!ch)
            {
                throw new InvalidUserNameOrPassword();
            }

            var accessTocken = await GenerateAccessTocken(user);
            var userVm = _mapper.Map<UserViewModel>(user);

            return new LoginResponseViewModel() { 
                accessTockenVm = accessTocken,
                userVm = userVm,
            };
        }

        private async Task<AccessTockenViewModel> GenerateAccessTocken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>() {
                new Claim(Claims.Sub,user.UserName),
                new Claim(Claims.PhoneNumber,user.PhoneNumber),
                new Claim(Claims.UserId,user.Id),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            if (roles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, String.Join(",", roles)));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));
            var credentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddMonths(1);

            var accessTocken = new JwtSecurityToken(_jwtOptions.Issuer,
                _jwtOptions.Issuer,
                claims,
                expires: expires,
                signingCredentials: credentails
                );

            var accessTockenVm = new AccessTockenViewModel()
            {
                AccessTocken = new JwtSecurityTokenHandler().WriteToken(accessTocken),
                ExpireAt = expires
            };

            return accessTockenVm;
        }


    }
}
