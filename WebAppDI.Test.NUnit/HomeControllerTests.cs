using Moq;
using Microsoft.Extensions.Logging;
using CasCap.Controllers;
namespace CasCap.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<ILogger<HomeController>> _mockLogger;
        private Mock<IDITestService> _mockDiTestService;
        private HomeController _controller;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();
            _mockDiTestService = new Mock<IDITestService>();

            // Initialize the HomeController with mocked dependencies
            _controller = new HomeController(_mockLogger.Object, _mockDiTestService.Object);
        }

        [Test]
        public void Index_ReturnsViewResult_WithValidViewModel()
        {
            // Arrange
            var expectedIntValues = new List<int> { 1, 2, 3 };
            var expectedStringValues = new List<string> { "Value1", "Value2" };

            _mockDiTestService.Setup(s => s.GetIntValues()).Returns(expectedIntValues);
            _mockDiTestService.Setup(s => s.GetStringValues()).Returns(expectedStringValues);

            // Act
            var result = _controller.Index() as ViewResult;
            var model = result?.Model as IndexViewModel;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IndexViewModel>(model);
            Assert.AreEqual(expectedIntValues, model?.SomeIntValues);
            Assert.AreEqual(expectedStringValues, model?.SomeStringValues);
        }
    }
}
