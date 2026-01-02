using Xunit;
using PdfStampGenerator.Core.Models;

namespace PdfStampGenerator.Tests.Core
{
    public class StampModelTests
    {
        [Fact]
        public void BorderThikness_WhenNegative_ClamsToMinimum()
        {
            // Arrange
            var model = new StampModel
            {
                BorderThickness = -5
            };

            // Assert
            Assert.True(model.BorderThickness > 0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void FontSize_WhenNonPositive_ClamsToDefault(double size)
        {
            // Arrange
            var model = new StampModel
            {
                FontSize = size
            };

            // Assert
            Assert.True(model.FontSize > 0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Title_WhenNullOrWhitespace_FallsBackToDefault(string? title)
        {
            // Arrange
            var model = new StampModel
            {
                Title = title!
            };

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(model.Title));
        }
    }
}
