namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();

        public ITrackable Parse(string line)
        {
            // DONE - Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            // DONE - If your array.Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                // DONE - Log that and return null
                // DONE - Do not fail if one record parsing fails, return null
                logger.LogError("The array length was less than 3.");
                return null; // DONE - Implement
            }

            // DONE - grab the latitude from your array at index 0
            var latParseable = double.TryParse(cells[0], out double latitude);

            if (!latParseable) logger.LogError("Unable to parse latitude into a double.");

            // DONE - grab the longitude from your array at index 1
            var lonParseable = double.TryParse(cells[1], out double longitude);

            if (!lonParseable) logger.LogError("Unable to parse longitude into a double.");

            // DONE - grab the name from your array at index 2
            var name = cells[2];

            // DONE - Your going to need to parse your string as a `double`
            // DONE - which is similar to parsing a string as an `int`

            // DONE - You'll need to create a TacoBell class
            // DONE - that conforms to ITrackable

            // DONE - Then, you'll need an instance of the TacoBell class
            // DONE - With the name and point set correctly
            var location = new Point()
            {
                Latitude = latitude,
                Longitude = longitude
            };

            var tacoBell = new TacoBell()
            {
                Name = name,
                Location = location
            };

            // DONE - Then, return the instance of your TacoBell class
            // DONE - Since it conforms to ITrackable

            return tacoBell;
        }
    }
}