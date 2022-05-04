using Xunit;
using myhealthcareapi.Models;
using myhealthcareapi.Controllers;
using myhealthcareapi.Services;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace ClientsControllerTest
{
    public class ClientRegisterTest
    {
        private AddClientModel _clientModel;
        private ClientsController _controller;
        private readonly Mock<IClientService> _clientRepoMock = new Mock<IClientService>();
        private readonly Mock<IMapper> _mapperRepoMock = new Mock<IMapper>();

        public ClientRegisterTest()
        {
            _clientModel = new AddClientModel()
            {
                Name = "New Client",
                Email = "newclient@gmail.com",
                Address = "Adress",
                Password = "pw2324tcfe"
            };

            _controller = new ClientsController(_clientRepoMock.Object, _mapperRepoMock.Object);
        }

        [Fact]
        public async void ReturnBadRequestResult_whenClientIsNull()
        {
            _clientModel = null;
            var result = await _controller.RegisterClient(_clientModel);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void ReturnObjectResult_withSuccesStatusCode_whenRegisterIsOk()
        {
            var result = await _controller.RegisterClient(_clientModel);
            var resultObject = (ObjectResult)result;
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, resultObject.StatusCode);
        }         
        
        [Fact]
        public async void ReturnObjectResult_withfailedStatusCode_whenEmailAlreadyExists()
        {
            var result = await _controller.RegisterClient(_clientModel);
            var resultObject = (ObjectResult)result;
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(409, resultObject.StatusCode);
        }         
        
        [Fact]
        public async void ReturnObjectResult_withfailedStatusCode_whenClientDidntAdd()
        {
            var result = await _controller.RegisterClient(_clientModel);
            var resultObject = (ObjectResult)result;
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, resultObject.StatusCode);
        }   
    }
}
