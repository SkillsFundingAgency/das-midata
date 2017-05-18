using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SFA.DAS.MI.Application.Queries.GetFractions;

namespace SFA.DAS.MI.Application.Tests.Queries.GetFractionsTests
{
    public class WhenIValidateTheRequest
    {
        private GetFractionsValidator _validator;

        [SetUp]
        public void Arrange()
        {
            _validator = new GetFractionsValidator();
        }

        [Test]
        public async Task ThenFalseIsReturnedWhenNoFieldsArePopulatedAndTheErrorDictionaryIsPopulated()
        {
            //Act
            var actual = await _validator.ValidateAsync(new GetFractionsRequest());

            //Assert
            Assert.IsFalse(actual.IsValid());
            Assert.Contains(new KeyValuePair<string,string>("EmpRef", "EmpRef has not been supplied"),  actual.ValidationDictionary);
        }

        [Test]
        public async Task ThenTrueIsReturnedWhenAllFieldsArePopulated()
        {
            //Act
            var actual = await _validator.ValidateAsync(new GetFractionsRequest {EmpRef = "123/ABC"});

            //Assert
            Assert.IsTrue(actual.IsValid());
        }
    }
}
