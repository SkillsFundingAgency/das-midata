using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.MI.Application.Queries.GetDeclarations;
using SFA.DAS.MI.Application.Validation;
using SFA.DAS.MI.Domain.Data;
using SFA.DAS.MI.Domain.Data.Repositories;
using SFA.DAS.MI.Domain.Models.Declarations;

namespace SFA.DAS.MI.Application.Tests.Queries.GetDeclarationsTests
{
    public class WhenIGetDeclarations : QueryBaseTest<GetDeclarationsQueryHandler,GetDeclarationsRequest,GetDeclarationsResponse>
    {
        private Mock<IDeclarationsRepository> _repository;
        public override GetDeclarationsRequest Query { get; set; }
        public override GetDeclarationsQueryHandler RequestHandler { get; set; }
        public override Mock<IValidator<GetDeclarationsRequest>> RequestValidator { get; set; }

        private string ExpectedEmpRef = "123/AVF3";

        [SetUp]
        public void Arrange()
        {
            SetUp();

            Query = new GetDeclarationsRequest {EmpRef = ExpectedEmpRef};

            _repository = new Mock<IDeclarationsRepository>();
            _repository.Setup(x => x.GetDeclarationsByEmpref(ExpectedEmpRef)).ReturnsAsync(new List<Declaration> {new Declaration()});

            RequestHandler = new GetDeclarationsQueryHandler(RequestValidator.Object, _repository.Object);
        }

        [Test]
        public override async Task ThenIfTheMessageIsValidTheRepositoryIsCalled()
        {
            //Act
            await RequestHandler.Handle(Query);

            //Assert
            _repository.Verify(x=>x.GetDeclarationsByEmpref(ExpectedEmpRef), Times.Once);
        }

        [Test]
        public override async Task ThenIfTheMessageIsValidTheValueIsReturnedInTheResponse()
        {
            //Act
            var actual = await RequestHandler.Handle(Query);

            //Assert
            Assert.IsAssignableFrom<GetDeclarationsResponse>(actual);
            Assert.IsTrue(actual.Declarations.Declarations.Any());
            Assert.AreEqual(actual.Declarations.EmpRef, ExpectedEmpRef);
        }
    }
}
