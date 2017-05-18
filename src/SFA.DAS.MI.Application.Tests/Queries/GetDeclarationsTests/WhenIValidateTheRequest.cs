using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SFA.DAS.MI.Application.Queries.GetDeclarations;

namespace SFA.DAS.MI.Application.Tests.Queries.GetDeclarationsTests
{
    public class WhenIValidateTheRequest
    {
        private GetDeclarationsValidator _validator;

        [SetUp]
        public void Arrange()
        {
            _validator = new GetDeclarationsValidator();
        }

        [Test]
        public async Task ThenFalseIsReturnedWhenNoFieldsArePopulatedAndTheErrorDictionaryIsPopulated()
        {
            //Act
            var actual = await _validator.ValidateAsync(new GetDeclarationsRequest());

            //Assert
            Assert.IsFalse(actual.IsValid());
            Assert.Contains(new KeyValuePair<string, string>("EmpRef", "EmpRef has not been supplied"), actual.ValidationDictionary);
        }

        [Test]
        public async Task ThenTrueIsReturnedWhenAllFieldsArePopulated()
        {
            //Act
            var actual = await _validator.ValidateAsync(new GetDeclarationsRequest { EmpRef = "123/ABC" });

            //Assert
            Assert.IsTrue(actual.IsValid());
        }
    }
}
