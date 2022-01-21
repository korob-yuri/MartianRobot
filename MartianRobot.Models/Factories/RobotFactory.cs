namespace MartianRobot.Models.Factories
{
    public static class RobotFactory
    {
        public static Robot CreateRobotOnGrid(Position position, Grid grid)
        {
            return new Robot(position, grid);
        }
    }
}
