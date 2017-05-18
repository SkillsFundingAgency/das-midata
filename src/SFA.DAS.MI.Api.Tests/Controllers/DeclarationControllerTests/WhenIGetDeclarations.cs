using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using MediatR;
using Moq;
using NLog;
using NUnit.Framework;
using SFA.DAS.MI.Api.Controllers;
using SFA.DAS.MI.Application.Queries.GetFractions;

namespace SFA.DAS.MI.Api.Tests.Controllers.DeclarationControllerTests
{
    public class WhenIGetDeclarations
    {
        private DeclarationsController _controller;
        private Mock<ILogger> _logger;
        private Mock<IMediator> _mediator;

        private const string ExpectedEmpRef = "123/ABC";

        [SetUp]
        public void Arrange()
        {
            _logger = new Mock<ILogger>();
            _mediator = new Mock<IMediator>();

            _mediator.Setup(x => x.SendAsync(It.Is<GetFractionsRequest>(c=>c.EmpRef.Equals(ExpectedEmpRef)))).ReturnsAsync(new GetFractionsResponse());

            _controller = new DeclarationsController(_logger.Object, _mediator.Object);
        }


        [Test]
        public async Task ThenATwoHundredAcceptedResponseIsReturnedWhenThereIsData()
        {
            //Act
            var actual = await _controller.GetDeclarations("123REF");

            //Assert
            Assert.IsAssignableFrom<OkResult>(actual);
        }

        [Test]
        public async Task ThenAFourZeroFourErrorIsReturnedWhenNoDataIsFound()
        {
            
        }

        [Test]
        public async Task ThenTheEmprefIsDecodedCorrectlyAndTheMediatorCalled()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
