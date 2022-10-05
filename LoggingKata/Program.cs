using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // DONE:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");
            Console.WriteLine();

            // DONE - use File.ReadAllLines(path) to grab all the lines from your csv file
            // DONE - Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            if (lines.Length < 2)
            {
                if (lines.Length < 1)
                {
                    logger.LogError("No lines found in csv file.");
                }
                logger.LogWarning("Only 1 line found in csv file.");
            }

            Console.Write("Would you like to see all of the Taco Bells in the csv file? yes/no: ");
            var userInput = Console.ReadLine().ToLower();
            Console.WriteLine();

            if (userInput != "yes" && userInput != "no")
            {
                Console.Write("Invalid entry. Please answer \"yes\" or \"no\": ");
                userInput = Console.ReadLine().ToLower();
                Console.WriteLine();
            }

            if (userInput == "yes")
            {
                Console.Write("Retrieving data");
                for (int i = 0; i < 9; i++)
                {
                    Console.Write(". ");
                    Thread.Sleep(500);
                }
                Console.WriteLine();

                for (int i = 0; i < lines.Length; i++)
                {
                    logger.LogInfo($"Lines: {lines[i]}");
                }
                Console.WriteLine();
            }

            Console.Write("Press ENTER to begin parsing: ");
            Console.ReadLine();
            Console.WriteLine();

            logger.LogInfo("Begin parsing");
            Console.WriteLine();
            Console.Write("Calculating");
            for (int i = 0; i < 9; i++)
            {
                Console.Write(". ");
                Thread.Sleep(500);
            }
            Console.WriteLine();
            Console.WriteLine();

            // DONE - Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // DONE - Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // DONE - Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco
            //        bells that are the farthest from each other.
            // DONE - Create a `double` variable to store the distance

            // DONE - Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)

            // Create a new corA Coordinate with your locA's lat and long

            // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location
            // (perhaps: `locB`)

            // Create a new Coordinate with your locB's lat and long

            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables
            // you set above

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.

            ITrackable tacoBell1 = new TacoBell();
            ITrackable tacoBell2 = new TacoBell();
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

            var milesApart = Math.Round(distance * .00062137, 2); // used to convert distance in meters to miles

            Console.WriteLine("***********************************************************************");
            Console.WriteLine();

            logger.LogInfo($"{tacoBell1.Name} and {tacoBell2.Name} are farthest away from each other.");
            Console.WriteLine();
            logger.LogInfo($"They are {milesApart} miles apart.");

            Console.WriteLine();
            Console.WriteLine("***********************************************************************");
        }
    }
}
