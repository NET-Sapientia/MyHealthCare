using Xunit;
using myhealthcareapi.Models;
using myhealthcareapi.Controllers;
using myhealthcareapi.Services;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace ClientsControllerTest
{ 
    public class ClientLoginTest
    {

        private LoginModel _testModel;
        private ClientsController _controller;
        private readonly Mock<IClientService> _clientRepoMock = new Mock<IClientService>();
        private readonly Mock<IMapper> _mapperRepoMock = new Mock<IMapper>();

        public ClientLoginTest()
        {
            _testModel = new LoginModel()
            {
                Email = "asdf@gmail.com",
                Password = "asdfasdf"
            };

            _controller = new ClientsController(_clientRepoMock.Object, _mapperRepoMock.Object);
        }

        [Fact]
        public async void ReturnNotFoundResult_whenClientDoesntExist()
        {
            _testModel.Email = "wrongemail@gmail.com";
            var result = await _controller.Login(_testModel);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async void ReturnNotFoundResult_whenPasswordIsWrong()
        {
            _testModel.Password = "wrong";
            var result = await _controller.Login(_testModel);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async void ReturnObjecResult_withSuccesStatusCode_whenClientIsOk()
        {
            var result = await _controller.Login(_testModel);
            var resultObject = (ObjectResult)result;
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, resultObject.StatusCode);
        }
    }
}