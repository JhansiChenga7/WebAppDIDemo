using NUnit.Framework;
using Moq;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Web.Http.Results;
using CasCap.Controllers;
using CasCap;
namespace WebAppDI.Test.NUnit
{
    [TestFixture]
    public class ValuesControllerTests
    {
        private Mock<ILogger<ValuesController>> _loggerMock;
        private Mock<IDITestService> _diTestSvcMock;
        private ValuesController _controller;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<ValuesController>>();
            _diTestSvcMock = new Mock<IDITestService>();
            _controller = new ValuesController(_loggerMock.Object, _diTestSvcMock.Object);
        }

        [Test]
        public void TestDI_ShouldReturnOkWithIntValues()
        {
            // Arrange
            var expectedValues = new List<int> { 1, 2, 3 };
            _diTestSvcMock.Setup(x => x.GetIntValues()).Returns(expectedValues);

            // Act
            IHttpActionResult actionResult = _controller.TestDI();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<int>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(expectedValues, contentResult.Content);
            _loggerMock.Verify(l => l.LogTrace(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void TestDI_ShouldReturnOkWithEmptyList()
        {
            // Arrange
            var expectedValues = new List<int>();
            _diTestSvcMock.Setup(x => x.GetIntValues()).Returns(expectedValues);

            // Act
            IHttpActionResult actionResult = _controller.TestDI();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<int>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(expectedValues, contentResult.Content);
            _loggerMock.Verify(l => l.LogTrace(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void TestDI_ShouldHandleNullResult()
        {
            // Arrange
            _diTestSvcMock.Setup(x => x.GetIntValues()).Returns((List<int>)null);

            // Act
            IHttpActionResult actionResult = _controller.TestDI();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<int>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNull(contentResult.Content);
            _loggerMock.Verify(l => l.LogTrace(It.IsAny<string>()), Times.Once);
        }
    }
}