using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // DONE - Find the two Taco Bells that are the furthest from one another.
            // DONE - HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");
            Console.WriteLine();

            // DONE - use File.ReadAllLines(path) to grab all the lines from your csv file
            // DONE - Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            if (lines.Length < 2)
            {
                if (lines.Length < 1)
                {
                    logger.LogError("No lines returned from csv file.");
                }
                logger.LogWarning("Only 1 line returned from csv file.");
            }

            Console.Write("Would you like to see a list of all the Taco Bells in the Log? yes/no: ");
            var userInput = Console.ReadLine().ToLower();
            
            while (userInput != "yes" && userInput != "no")
            {
                Console.WriteLine();
                Console.Write("Invalid entry. Please enter \"yes\" or \"no\": ");
                userInput = Console.ReadLine().ToLower();
            }

            if (userInput == "yes")
            {
                Console.WriteLine();
                foreach (var line in lines)
                {
                    logger.LogInfo($"Lines: {line}");
                }
            }
            Console.WriteLine();

            logger.LogInfo("Begin parsing");
            Console.WriteLine();

            // DONE - Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // DONE - Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS

            // DONE - Now that your Parse method is completed, START BELOW ----------

            // DONE: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco
            //       bells that are the farthest from each other.
            // DONE - Create a `double` variable to store the distance

            // DONE - Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // DONE - Do a loop for your locations to grab each location as the origin (perhaps: `locA`)

            // DONE - Create a new corA Coordinate with your locA's lat and long

            // DONE - Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location
            //        (perhaps: `locB`)

            // DONE - Create a new Coordinate with your locB's lat and long

            // DONE - Now, compare the two using `.GetDistanceTo()`, which returns a double
            // DONE - If the distance is greater than the currently saved distance, update the distance and the two `ITrackable`
            // DONE - variables you set above

            // DONE - Once you've looped through everything, you've found the two Taco Bells farthest away from each other.

            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;
            double distance = 0;

            var corA = new GeoCoordinate();
            var corB = new GeoCoordinate();

            foreach (var locA in locations)
            {
                corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);

                foreach (var locB in locations)
                {
                    corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                    
                    if (distance < corA.GetDistanceTo(corB))
                    {
                        distance = corA.GetDistanceTo(corB);
                        tacoBell1 = locA;
                        tacoBell2 = locB;
                    }
                }
            }

            Console.WriteLine("**************************Answer Acheived*************************************");
            Console.WriteLine();

            logger.LogInfo($"{tacoBell1.Name} and {tacoBell2.Name} are the farthest apart.");
            Console.WriteLine();
            logger.LogInfo($"{tacoBell1.Name} has a Latitude of {tacoBell1.Location.Latitude} and Longitude of {tacoBell1.Location.Longitude}");
            logger.LogInfo($"{tacoBell2.Name} has a Latitude of {tacoBell2.Location.Latitude} and Longitude of {tacoBell2.Location.Longitude}");
            Console.WriteLine();
            Console.WriteLine("******************************************************************************");

            
        }
    }
}
