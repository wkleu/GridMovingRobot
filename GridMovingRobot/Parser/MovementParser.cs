using System;
using System.Collections.Generic;
using GridMovingRobot.Robot;

namespace GridMovingRobot.Parser
{
    public class MovementParser
    {
        private const string UnableToParseErrorMessage = "Unable to parse: {0}";
        private const string NegativeStepsProvidedErrorMessage = "Negative steps not supported: {0}";

        private static int ParseSteps(string movementString)
        {
            var stepsString = movementString.Substring(1);

            int steps;
            if (!int.TryParse(stepsString, out steps))
            {
                throw new ArgumentException(string.Format(UnableToParseErrorMessage, movementString));
            }

            if (steps < 0)
            {
                throw new ArgumentException(string.Format(NegativeStepsProvidedErrorMessage, movementString));
            }

            return steps;
        }

        private static Direction ParseDirection(string movementString)
        {
            var directionString = movementString.Substring(0, 1);

            switch (directionString)
            {
                case "N": return Direction.North;
                case "E": return Direction.East;
                case "S": return Direction.South;
                case "W": return Direction.West;
                default:
                    throw new ArgumentException(string.Format(UnableToParseErrorMessage, movementString));
            }
        }

        public static IList<Movement> Parse(string input)
        {
            var splitted = input.Split(',');
            var result = new List<Movement>();

            foreach (var movementString in splitted)
            {
                if (movementString.Length == 0)
                {
                    continue; // just ignore empty
                }

                if (movementString.Length < 2)
                {
                    throw new ArgumentException(string.Format(UnableToParseErrorMessage, movementString));
                }
                
                var steps = ParseSteps(movementString);
                var direction = ParseDirection(movementString);

                var movement = new Movement(direction, steps);
                result.Add(movement);
            }

            return result;
        }
    }
}