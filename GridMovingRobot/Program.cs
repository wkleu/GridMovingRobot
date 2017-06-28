using System;
using GridMovingRobot.Parser;
using GridMovingRobot.Robot;

namespace GridMovingRobot
{
    /**
     * Submission by Wilhelm Kleu
     */
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide navigation argument (example: N2,E2,E3)");
            }
            else
            {
                var input = args[0];
                try
                {
                    var movements = MovementParser.Parse(input);
                    var robotOnGrid = new RobotOnGrid();
                    robotOnGrid.Move(movements);

                    Console.WriteLine("Unique grid visits: " + robotOnGrid.UniqueVisited);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
            Console.Read();
        }
    }
}