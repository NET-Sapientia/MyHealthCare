using Xunit;
using myhealthcareapi.Models;
using myhealthcareapi.Controllers;
using myhealthcareapi.Services;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace ClientsControllerTest
{
    public class GetClientTest
    {
        private ClientsController _controller;
        private readonly Mock<IClientService> _clientRepoMock = new Mock<IClientService>();
        private readonly Mock<IMapper> _mapperRepoMock = new Mock<IMapper>();

        public GetClientTest()
        {
            _controller = new ClientsController(_clientRepoMock.Object, _mapperRepoMock.Object);
        }

        [Fact]
        public async void ReturnNotFoundResult_whenClientNotFound()
        {
            string wrongEmail = "wrongemail@gmail.com";
            var result = _controller.GetClientByEmail(wrongEmail);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        public async void ReturnObjecResult_withSuccesStatusCode_whenClientFound()
        {
            var email = "asdf@gmail.com";
            var result = await _controller.GetClientByEmail(email);
            var resultObject = (ObjectResult)result;
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, resultObject.StatusCode);
        }
    }
}
