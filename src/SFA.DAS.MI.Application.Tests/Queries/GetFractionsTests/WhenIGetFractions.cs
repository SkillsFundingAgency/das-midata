﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.MI.Application.Queries.GetFractions;
using SFA.DAS.MI.Application.Validation;
using SFA.DAS.MI.Domain.Data;
using SFA.DAS.MI.Domain.Models.Fractions;

namespace SFA.DAS.MI.Application.Tests.Queries.GetFractionsTests
{
    public class WhenIGetFractions : QueryBaseTest<GetFractionsQueryHandler,GetFractionsRequest,GetFractionsResponse>
    {
        private Mock<IFractionsRepository> _repository;
        public override GetFractionsRequest Query { get; set; }
        public override GetFractionsQueryHandler RequestHandler { get; set; }
        public override Mock<IValidator<GetFractionsRequest>> RequestValidator { get; set; }
        private const string ExpectedEmpref = "123/ABC";

        [SetUp]
        public void Arrange()
        {
            SetUp();

            Query = new GetFractionsRequest {EmpRef = ExpectedEmpref};

            _repository = new Mock<IFractionsRepository>();
            _repository.Setup(x => x.GetFractionsByEmpref(ExpectedEmpref)).ReturnsAsync(new List<FractionCalculation> {new FractionCalculation {}});

            RequestHandler = new GetFractionsQueryHandler(RequestValidator.Object, _repository.Object);
        }

        [Test]
        public override async Task ThenIfTheMessageIsValidTheRepositoryIsCalled()
        {
            //Arrange
            
            RequestValidator.Setup(x => x.ValidateAsync(It.Is<GetFractionsRequest>(c => c.EmpRef.Equals(ExpectedEmpref)))).ReturnsAsync(new ValidationResult {ValidationDictionary = new Dictionary<string, string>()});

            //Act
            await RequestHandler.Handle(Query);

            //Assert
            _repository.Verify(x=>x.GetFractionsByEmpref(ExpectedEmpref), Times.Once);
        }

        [Test]
        public override async Task ThenIfTheMessageIsValidTheValueIsReturnedInTheResponse()
        {
            //Act
            var actual = await RequestHandler.Handle(new GetFractionsRequest { EmpRef = ExpectedEmpref });

            //Assert
            Assert.IsAssignableFrom<GetFractionsResponse>(actual);
            Assert.IsNotEmpty(actual.Fractions.FractionCalculations);
            Assert.AreEqual(actual.Fractions.Empref, ExpectedEmpref);
        }
    }
}