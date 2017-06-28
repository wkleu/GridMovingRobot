using System.Collections.Generic;

namespace GridMovingRobot.Grid
{
    public class GrowableGrid<T>
    {
        // The backing internal data structure to represent the grid that can grow
        private readonly IList<IList<T>> _grid;

        public int CurrentMaxRowIndex { get; private set; }
        public int CurrentMaxColumnIndex { get; private set; }

        public IList<T> this[int i] => _grid[i];

        public GrowableGrid()
        {
            CurrentMaxRowIndex = 0;
            CurrentMaxColumnIndex = 0;
            _grid = new List<IList<T>>(1) { CreateListWithDefault(1) };
        }

        private static IList<T> CreateListWithDefault(int size)
        {
            var initialisedList = new List<T>();
            for (var i = 0; i < size; i++)
            {
                initialisedList.Add(default(T));
            }
            return initialisedList;
        }

        public void GrowUp(int size)
        {
            for (var i = 0; i < size; i++)
            {
                _grid.Insert(0, CreateListWithDefault(CurrentMaxColumnIndex + 1));
                CurrentMaxRowIndex++;
            }
        }

        public void GrowRight(int size)
        {
            foreach (var row in _grid)
            {
                for (var i = 0; i < size; i++)
                {
                    row.Add(default(T));

                }
            }
            CurrentMaxColumnIndex += size;
        }

        public void GrowDown(int size)
        {
            for (var i = 0; i < size; i++)
            {
                _grid.Add(CreateListWithDefault(CurrentMaxColumnIndex + 1));
                CurrentMaxRowIndex++;
            }
        }

        public void GrowLeft(int size)
        {
            foreach (var row in _grid)
            {
                for (var i = 0; i < size; i++)
                {
                    row.Insert(0, default(T));
                }
            }
            CurrentMaxColumnIndex += size;
        }
    }
}