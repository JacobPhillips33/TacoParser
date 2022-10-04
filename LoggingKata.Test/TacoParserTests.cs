using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", " Taco Bell Acwort...")]
        public void ParseNameTest(string line, string expected)
        {
            // DONE: Complete Something, if anything

            //Arrange
            var parseNameTester = new TacoParser();

            //Act
            var actual = parseNameTester.Parse(line).Name;

            //Assert
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        public void ParseLongitudeTest(string line, double expected)
        {
            // DONE: Complete - "line" represents input data we will Parse to
            //       extract the Longitude.  Your .csv file will have many of these lines,
            //       each representing a TacoBell location

            //Arrange
            var parseLongitudeTester = new TacoParser();

            //Act
            var actual = parseLongitudeTester.Parse(line).Location.Longitude;

            //Assert
            Assert.Equal(expected, actual);
        }


        //DONE: Create a test ShouldParseLatitude
        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", 34.073638)]
        public void ParseLatitudeTest(string line, double expected)
        {
            //Arrange
            var parseLatitudeTester = new TacoParser();

            //Act
            var actual = parseLatitudeTester.Parse(line).Location.Latitude;

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
