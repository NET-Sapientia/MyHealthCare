using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using myhealthcareapi.DataAccesLayers;
using myhealthcareapi.DataAccesLayers.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public class MedicService : IMedicService
    {
        private readonly Context _context;
        private readonly IConfiguration _config;
        public MedicService(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public string GenerateJwtToken(MedicEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, "medic"),
                        },
              expires: DateTime.Now.AddMinutes(2),

            signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<MedicEntity> GetMedicByEmail(string email)
        {
            return await _context.Medics.FirstOrDefaultAsync(m => string.Equals(email, m.Email));
        }

        public async Task<MedicEntity> GetMedicById(int id)
        {
            return await _context.Medics.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
