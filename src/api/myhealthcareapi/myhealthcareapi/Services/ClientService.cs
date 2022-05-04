using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using myhealthcareapi.DataAccesLayers;
using myhealthcareapi.DataAccesLayers.Models;
using myhealthcareapi.Services.ServiceResponses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public class ClientService : IClientService
    {

        private readonly Context _context;
        private readonly IConfiguration _config;
        public ClientService(Context context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<ClientServiceResponses> AddClient(ClientEntity Client)
        {
            if (Client == null)
                return ClientServiceResponses.NULLPARAM;

            if (await GetClientByEmail(Client.Email) != null)
                return ClientServiceResponses.EMAILALREADYEXISTS;

            _context.Clients.Add(Client);

            try
            {
                await _context.SaveChangesAsync();
                return ClientServiceResponses.SUCCESS;
            }

            catch
            {
                return ClientServiceResponses.EXCEPTION;
            }
                    
        }

        public async Task<ClientEntity> GetClientByEmail(string email)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => string.Equals(email, c.Email));
        }

        public string GenerateJwtToken(ClientEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, "client"),
                        },
              expires: DateTime.Now.AddMinutes(2),

            signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ClientEntity> GetClientById(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
