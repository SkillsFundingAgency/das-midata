using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using MediatR;
using Moq;
using NLog;
using NUnit.Framework;
using SFA.DAS.MI.Api.Controllers;
using SFA.DAS.MI.Application.Queries.GetFractions;
using SFA.DAS.MI.Domain.Models.Fractions;


namespace SFA.DAS.MI.Api.Tests.Controllers.FractionControllerTests
{
    public class WhenIGetFractions
    {
        private FractionsController _controller;
        private Mock<ILogger> _logger;
        private Mock<IMediator> _mediator;

        private const string ExpectedEmpRef = "123/ABC";
        private const string ExpectedEncodedEmpRef = "123%2FABC";

        [SetUp]
        public void Arrange()
        {
            _logger = new Mock<ILogger>();
            _mediator = new Mock<IMediator>();
            _mediator.Setup(x => x.SendAsync(It.Is<GetFractionsRequest>(c => c.EmpRef.Equals(ExpectedEmpRef))))
                .ReturnsAsync(new GetFractionsResponse
                {
                    Fractions =
                        new EnglishFractionDeclarations
                        {
                            Empref = ExpectedEmpRef,
                            FractionCalculations = new List<FractionCalculation> {new FractionCalculation()}
                        }
                });

            _controller = new FractionsController(_logger.Object, _mediator.Object);
        }

        [Test]
        public async Task ThenATwoHundredAcceptedResponseIsReturnedWhenThereIsData()
        {
            //Act
            var actual = await _controller.GetFractions(ExpectedEncodedEmpRef);

            //Assert
            Assert.IsAssignableFrom<OkNegotiatedContentResult<EnglishFractionDeclarations>>(actual);
            var model = actual as OkNegotiatedContentResult<EnglishFractionDeclarations>;
            Assert.IsNotNull(model);
        }

        [Test]
        public async Task ThenAFourZeroFourErrorIsReturnedWhenNoDataIsFound()
        {
            //Act
            var actual = await _controller.GetFractions("123%2fRTG");

            //Assert
            Assert.IsAssignableFrom<NotFoundResult>(actual);
        }

        [Test]
        public async Task ThenTheEmprefIsDecodedCorrectlyAndTheMediatorCalled()
        {
            //Act
            await _controller.GetFractions(ExpectedEncodedEmpRef);

            //Assert
            _mediator.Verify(x => x.SendAsync(It.Is<GetFractionsRequest>(c => c.EmpRef.Equals(ExpectedEmpRef))), Times.Once);
        }
    }
}
