using System.Collections.Generic;
using FluentAssertions;
using GridMovingRobot.Robot;
using Xunit;

namespace GridMovingRobot.Tests.Robot
{
    public class RobotOnGridTests
    {
        [Fact]
        public void When_moving_unique_should_be_updated()
        {
            var robotOnGrid = new RobotOnGrid();
            robotOnGrid.Move(new Movement(Direction.North, 4));
            robotOnGrid.Move(new Movement(Direction.East, 2));
            robotOnGrid.Move(new Movement(Direction.South, 2));
            robotOnGrid.Move(new Movement(Direction.West, 4));

            robotOnGrid.UniqueVisited.Should().Be(11);
        }

        [Fact]
        public void When_moving_on_same_column_unique_should_be_correct()
        {
            var robotOnGrid = new RobotOnGrid();
            robotOnGrid.Move(new Movement(Direction.North, 4));
            robotOnGrid.Move(new Movement(Direction.South, 3));

            robotOnGrid.UniqueVisited.Should().Be(4);
        }

        [Fact]
        public void When_moving_on_same_row_unique_should_be_correct()
        {
            var robotOnGrid = new RobotOnGrid();
            robotOnGrid.Move(new Movement(Direction.West, 4));
            robotOnGrid.Move(new Movement(Direction.East, 3));

            robotOnGrid.UniqueVisited.Should().Be(4);
        }

        [Fact]
        public void When_moving_east_unique_should_be_correct()
        {
            var robotOnGrid = new RobotOnGrid();
            robotOnGrid.Move(new Movement(Direction.East, 2));
            robotOnGrid.UniqueVisited.Should().Be(2);

            robotOnGrid.Move(new Movement(Direction.East, 2));
            robotOnGrid.UniqueVisited.Should().Be(4);
        }

        [Fact]
        public void When_moving_south_unique_should_be_correct()
        {
            var robotOnGrid = new RobotOnGrid();
            robotOnGrid.Move(new Movement(Direction.South, 2));
            robotOnGrid.UniqueVisited.Should().Be(2);

            robotOnGrid.Move(new Movement(Direction.South, 100));
            robotOnGrid.UniqueVisited.Should().Be(102);
        }

        [Fact]
        public void When_moving_west_unique_should_be_correct()
        {
            var robotOnGrid = new RobotOnGrid();
            robotOnGrid.Move(new Movement(Direction.West, 200));
            robotOnGrid.UniqueVisited.Should().Be(200);

            robotOnGrid.Move(new Movement(Direction.West, 100));
            robotOnGrid.UniqueVisited.Should().Be(300);
        }

        [Fact]
        public void When_moving_north_unique_should_be_correct()
        {
            var robotOnGrid = new RobotOnGrid();
            robotOnGrid.Move(new Movement(Direction.West, 21));
            robotOnGrid.UniqueVisited.Should().Be(21);

            robotOnGrid.Move(new Movement(Direction.West, 21));
            robotOnGrid.UniqueVisited.Should().Be(42);
        }

        [Fact]
        public void When_moving_in_square_should_only_count_first_visits()
        {
            var robotOnGrid = new RobotOnGrid();
            robotOnGrid.Move(new Movement(Direction.North, 1)); // first move is to get on the board

            // this should walk on a 4x4 grid multiple times
            for (var i = 0; i < 10; i++)
            {
                robotOnGrid.Move(new List<Movement>()
                {
                    new Movement(Direction.North, 3),
                    new Movement(Direction.East, 3),
                    new Movement(Direction.South, 3),
                    new Movement(Direction.West, 3)
                });
            }
            
            robotOnGrid.UniqueVisited.Should().Be(12);
        }

        [Fact]
        public void When_using_movement_that_causes_growth_unique_should_be_correct()
        {
            var movements = new List<Movement>()
            {
                new Movement(Direction.West, 10),
                new Movement(Direction.South, 10),
                new Movement(Direction.East, 20),
                new Movement(Direction.North, 20)
            };

            var robotOnGrid = new RobotOnGrid();
            robotOnGrid.Move(movements); 
            robotOnGrid.UniqueVisited.Should().Be(60);
        }

        [Fact]
        public void When_complicated_movement_unique_should_be_correct_1()
        {
            var movements = new List<Movement>()
            {
                new Movement(Direction.East, 2),
                new Movement(Direction.South, 1),
                new Movement(Direction.West, 2),
                new Movement(Direction.North, 3),
                new Movement(Direction.East, 1),
                new Movement(Direction.South, 2),
                new Movement(Direction.East, 1),
                new Movement(Direction.East, 1),
                new Movement(Direction.West, 3)
            };

            var robotOnGrid = new RobotOnGrid();
            robotOnGrid.Move(movements);
            robotOnGrid.UniqueVisited.Should().Be(11);
        }

        [Fact]
        public void When_complicated_movement_unique_should_be_correct_2()
        {
            var movements = new List<Movement>()
            {
                new Movement(Direction.South, 3),
                new Movement(Direction.West, 2),
                new Movement(Direction.North, 2),
                new Movement(Direction.East, 1),
                new Movement(Direction.South, 2),
                new Movement(Direction.East, 2),
                new Movement(Direction.South, 2),
                new Movement(Direction.West, 4),
                new Movement(Direction.North, 2),
                new Movement(Direction.East, 3),
                new Movement(Direction.North, 2),
                new Movement(Direction.West, 2)

            };

            var robotOnGrid = new RobotOnGrid();
            robotOnGrid.Move(movements);
            robotOnGrid.UniqueVisited.Should().Be(18);
        }

        [Fact]
        public void When_complicated_movement_unique_should_be_correct_3()
        {
            var movements = new List<Movement>()
            {
                new Movement(Direction.South, 2),
                new Movement(Direction.West, 1),
                new Movement(Direction.North, 2),
                new Movement(Direction.East, 1),
                new Movement(Direction.South, 2),
                new Movement(Direction.East, 1),
                new Movement(Direction.West, 3)
            };

            var robotOnGrid = new RobotOnGrid();
            robotOnGrid.Move(movements);
            robotOnGrid.UniqueVisited.Should().Be(8);
        }
    }
}