using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using MediatR;
using Moq;
using NLog;
using NUnit.Framework;
using SFA.DAS.MI.Api.Controllers;
using SFA.DAS.MI.Application;
using SFA.DAS.MI.Application.Queries.GetDeclarations;
using SFA.DAS.MI.Domain.Models.Declarations;

namespace SFA.DAS.MI.Api.Tests.Controllers.DeclarationControllerTests
{
    public class WhenIGetDeclarations
    {
        private DeclarationsController _controller;
        private Mock<ILogger> _logger;
        private Mock<IMediator> _mediator;

        private const string ExpectedEmpRef = "123/ABC";
        private const string ExpectedEncodedEmpRef = "123%2FABC";

        [SetUp]
        public void Arrange()
        {
            _logger = new Mock<ILogger>();
            _mediator = new Mock<IMediator>();

            _mediator.Setup(x => x.SendAsync(It.Is<GetDeclarationsRequest>(c=>c.EmpRef.Equals(ExpectedEmpRef)))).ReturnsAsync(new GetDeclarationsResponse {Declarations = new LevyDeclarations {Declarations = new List<Declaration>()} });

            _controller = new DeclarationsController(_logger.Object, _mediator.Object);
        }


        [Test]
        public async Task ThenATwoHundredAcceptedResponseIsReturnedWhenThereIsData()
        {
            //Act
            var actual = await _controller.GetDeclarations(ExpectedEncodedEmpRef);

            //Assert
            Assert.IsAssignableFrom<OkNegotiatedContentResult<LevyDeclarations>>(actual);
            var model = actual  as OkNegotiatedContentResult<LevyDeclarations>;
            Assert.IsNotNull(model);
        }

        [Test]
        public async Task ThenAFourZeroFourErrorIsReturnedWhenNoDataIsFound()
        {
            //Act
            var actual = await _controller.GetDeclarations("123%2fRTG");

            //Assert
            Assert.IsAssignableFrom<NotFoundResult>(actual);
        }

        [Test]
        public async Task ThenTheEmprefIsDecodedCorrectlyAndTheMediatorCalled()
        {
            //Act
            await _controller.GetDeclarations(ExpectedEncodedEmpRef);

            //Assert
            _mediator.Verify(x=>x.SendAsync(It.Is<GetDeclarationsRequest>(c => c.EmpRef.Equals(ExpectedEmpRef))),Times.Once);
        }


        [Test]
        public async Task ThenWhenAnInvalidRequestExceptionIsThrownABadRequestResponseIsReturned()
        {
            //Arrange
            _mediator.Setup(x => x.SendAsync(It.IsAny<GetDeclarationsRequest>()))
                .ThrowsAsync(new InvalidRequestException(new Dictionary<string, string>()));

            //Act
            var actual = await _controller.GetDeclarations(ExpectedEncodedEmpRef);

            //Act
            Assert.IsAssignableFrom<BadRequestErrorMessageResult>(actual);
            _logger.Verify(x => x.Info(It.IsAny<InvalidRequestException>()));
        }

        [Test]
        public async Task ThenWhenAnExceptionIsThrownAServerErrorIsReturned()
        {
            //Arrange
            _mediator.Setup(x => x.SendAsync(It.IsAny<GetDeclarationsRequest>())).ThrowsAsync(new Exception());

            //Act
            var actual = await _controller.GetDeclarations(ExpectedEncodedEmpRef);

            //Act
            Assert.IsAssignableFrom<InternalServerErrorResult>(actual);
            _logger.Verify(x=>x.Error(It.IsAny<Exception>()));
        }
    }
}
