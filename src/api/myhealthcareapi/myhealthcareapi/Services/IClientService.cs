using myhealthcareapi.DataAccesLayers.Models;
using myhealthcareapi.Services.ServiceResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myhealthcareapi.Services
{
    public interface IClientService
    {
        public Task<ClientServiceResponses> AddClient(ClientEntity Client);
        public Task<ClientEntity> GetClientByEmail(string email);
        public Task<ClientEntity> GetClientById(int dd);

        public string GenerateJwtToken(ClientEntity client);
    }
}
