namespace GridMovingRobot.Robot
{
    public class Movement
    {
        public Direction Direction { get; }
        public int Steps { get; }

        public Movement(Direction direction, int steps)
        {
            Direction = direction;
            Steps = steps;
        }
    }
}