using System;
using System.Collections.Generic;
using GridMovingRobot.Grid;

namespace GridMovingRobot.Robot
{
    public class RobotOnGrid
    {
        // growable grid with a flag to represent if it was visited
        private readonly GrowableGrid<bool> _growableGrid;
        private int _currentRow;
        private int _currentColumn;
        private bool _firstStepTaken = false; // the first step should mark the current as per example


        public int UniqueVisited { get; private set; }

        public RobotOnGrid()
        {
            // initialise grid and set starting location
            _growableGrid = new GrowableGrid<bool>();
            _currentColumn = 0;
            _currentRow = 0;
        }

        public void Move(IEnumerable<Movement> movements)
        {
            foreach (var movement in movements)
            {
                Move(movement);
            }
        }

        public void Move(Movement movement)
        {

            EnsureCapacity(movement);

            var steps = movement.Steps;

            switch (movement.Direction)
            {
                case Direction.North:
                    MoveEachStep(steps, () => _currentRow--);
                    break;
                case Direction.East:
                    MoveEachStep(steps, () => _currentColumn++);
                    break;
                case Direction.South:
                    MoveEachStep(steps, () => _currentRow++);
                    break;
                case Direction.West:
                    MoveEachStep(steps, () => _currentColumn--);
                    break;
            }
        }

        private void MoveEachStep(int steps, Action updateCurrentLocation)
        {
            for (var i = 0; i < steps; i++)
            {
                if (!_firstStepTaken)
                {
                    _firstStepTaken = true;
                }
                else
                {
                    // don't update with first step, start counting on row=0 and col=0
                    updateCurrentLocation();
                }
                
                SetVisitedOfCurrentLocation();
            }
        }

        private void SetVisitedOfCurrentLocation()
        {
            if (_growableGrid[_currentRow] [_currentColumn] == false)
            {
                UniqueVisited++;
                _growableGrid[_currentRow][_currentColumn] = true;
            }
        }

        private bool WillExceedWest(int steps)
        {
            return _currentColumn - steps < 0;
        }

        private bool WillExceedSouth(int steps)
        {
            return _currentRow + steps > _growableGrid.CurrentMaxRowIndex;
        }

        private bool WillExceedEast(int steps)
        {
            return _currentColumn + steps > _growableGrid.CurrentMaxColumnIndex;
        }

        private bool WillExceedNorth(int steps)
        {
            return _currentRow - steps < 0;
        }

        private void EnsureCapacity(Movement movement)
        {
            var steps = movement.Steps;
            switch (movement.Direction)
            {
                case Direction.North:
                    if (WillExceedNorth(steps))
                    {
                        _growableGrid.GrowUp(steps);
                        _currentRow += steps;
                    }
                    break;
                case Direction.East:
                    if (WillExceedEast(steps))
                    {
                        _growableGrid.GrowRight(steps);
                    }
                    break;
                case Direction.South:
                    if (WillExceedSouth(steps))
                    {
                        _growableGrid.GrowDown(steps);
                    }
                    break;
                case Direction.West:
                    if (WillExceedWest(steps))
                    {
                        _growableGrid.GrowLeft(steps);
                        _currentColumn += steps;
                    }
                    break;
            }
        }
    }
}
