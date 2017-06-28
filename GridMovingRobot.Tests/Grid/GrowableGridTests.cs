using FluentAssertions;
using GridMovingRobot.Grid;
using Xunit;

namespace GridMovingRobot.Tests.Grid
{
    public class GrowableGridTests
    {
        [Fact]
        public void When_initialise_default_value_is_set()
        {
            var growableGrid = new GrowableGrid<object>();

            growableGrid.CurrentMaxRowIndex.Should().Be(0);
            growableGrid.CurrentMaxColumnIndex.Should().Be(0);
            growableGrid[0].Count.Should().Be(1);
            AssertAllValuesAreSet(growableGrid);
        }

        [Fact]
        public void When_grow_up_rows_should_increase()
        {
            var growableGrid = new GrowableGrid<object>();

            growableGrid.GrowUp(2);

            growableGrid.CurrentMaxRowIndex.Should().Be(2);
            growableGrid.CurrentMaxColumnIndex.Should().Be(0);
            AssertAllValuesAreSet(growableGrid);
        }

        [Fact]
        public void When_grow_right_columns_should_increase()
        {
            var growableGrid = new GrowableGrid<object>();

            growableGrid.GrowRight(2);

            growableGrid.CurrentMaxRowIndex.Should().Be(0);
            growableGrid.CurrentMaxColumnIndex.Should().Be(2);
            AssertAllValuesAreSet(growableGrid);
        }

        [Fact]
        public void When_grow_down_rows_should_increase()
        {
            var growableGrid = new GrowableGrid<object>();

            growableGrid.GrowDown(2);

            growableGrid.CurrentMaxRowIndex.Should().Be(2);
            growableGrid.CurrentMaxColumnIndex.Should().Be(0);
            AssertAllValuesAreSet(growableGrid);
        }

        [Fact]
        public void When_grow_left_columns_should_increase()
        {
            var growableGrid = new GrowableGrid<object>();

            growableGrid.GrowLeft(2);

            growableGrid.CurrentMaxRowIndex.Should().Be(0);
            growableGrid.CurrentMaxColumnIndex.Should().Be(2);
            AssertAllValuesAreSet(growableGrid);
        }

        [Fact]
        public void When_multiple_grows_called_should_grow()
        {
            var growableGrid = new GrowableGrid<object>();

            growableGrid.GrowUp(1); // 2 x 1 grid
            growableGrid.CurrentMaxRowIndex.Should().Be(1);
            growableGrid.CurrentMaxColumnIndex.Should().Be(0);

            growableGrid.GrowLeft(1); // 2 x 2 grid
            growableGrid.CurrentMaxRowIndex.Should().Be(1);
            growableGrid.CurrentMaxColumnIndex.Should().Be(1);

            growableGrid.GrowDown(1); // 3 x 2 grid
            growableGrid.CurrentMaxRowIndex.Should().Be(2);
            growableGrid.CurrentMaxColumnIndex.Should().Be(1);

            growableGrid.GrowRight(1); // 3 x 3 grid
            growableGrid.CurrentMaxRowIndex.Should().Be(2);
            growableGrid.CurrentMaxColumnIndex.Should().Be(2);

            AssertAllValuesAreSet(growableGrid);
        }

        private void AssertAllValuesAreSet<T>(GrowableGrid<T> growableGrid)
        {
            for (var i = 0; i <= growableGrid.CurrentMaxRowIndex; i++)
            {
                for (var j = 0; j <= growableGrid.CurrentMaxColumnIndex; j++)
                {
                    growableGrid[i][j].Should().Be(default(T));
                }
            }
        }
    }

}