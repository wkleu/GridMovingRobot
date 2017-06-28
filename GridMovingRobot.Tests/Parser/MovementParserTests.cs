using System;
using System.Linq;
using FluentAssertions;
using GridMovingRobot.Parser;
using GridMovingRobot.Robot;
using Xunit;

namespace GridMovingRobot.Tests.Parser
{
    public class MovementParserTests
    {
        [Fact]
        public void When_east_provided_parse_correctly()
        {
            var movements = MovementParser.Parse("E2");

            movements.Count().Should().Be(1);
            movements[0].Direction.Should().Be(Direction.East);
            movements[0].Steps.Should().Be(2);
        }

        [Fact]
        public void When_north_provided_parse_correctly()
        {
            var movements = MovementParser.Parse("N10");

            movements.Count().Should().Be(1);
            movements[0].Direction.Should().Be(Direction.North);
            movements[0].Steps.Should().Be(10);
        }

        [Fact]
        public void When_west_provided_parse_correctly()
        {
            var movements = MovementParser.Parse("W100");

            movements.Count().Should().Be(1);
            movements[0].Direction.Should().Be(Direction.West);
            movements[0].Steps.Should().Be(100);
        }

        [Fact]
        public void When_south_provided_parse_correctly()
        {
            var movements = MovementParser.Parse("S1000");

            movements.Count().Should().Be(1);
            movements[0].Direction.Should().Be(Direction.South);
            movements[0].Steps.Should().Be(1000);
        }

        [Fact]
        public void When_multiple_provided_order_preserved()
        {
            var movements = MovementParser.Parse("N1,S2,E3,W4");

            movements.Count().Should().Be(4);
            movements[0].Direction.Should().Be(Direction.North);
            movements[0].Steps.Should().Be(1);
            movements[1].Direction.Should().Be(Direction.South);
            movements[1].Steps.Should().Be(2);
            movements[2].Direction.Should().Be(Direction.East);
            movements[2].Steps.Should().Be(3);
            movements[3].Direction.Should().Be(Direction.West);
            movements[3].Steps.Should().Be(4);
        }

        [Fact]
        public void When_empty_it_is_ignored()
        {
            var movements = MovementParser.Parse(",N1,,E3,");

            movements.Count().Should().Be(2);
            movements[0].Direction.Should().Be(Direction.North);
            movements[0].Steps.Should().Be(1);
            movements[1].Direction.Should().Be(Direction.East);
            movements[1].Steps.Should().Be(3);
        }

        [Fact]
        public void When_negative_steps_exception_thrown()
        {
            Action act = () => MovementParser.Parse("S-3");

            act.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void When_unknown_direction_exception_thrown()
        {
            Action act = () => MovementParser.Parse("X2");

            act.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void When_complete_junk_exception_thrown()
        {
            Action act = () => MovementParser.Parse("!@#@!");

            act.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void When_only_direction_exception_thrown()
        {
            Action act = () => MovementParser.Parse("N");

            act.ShouldThrow<ArgumentException>();
        }
    }
}