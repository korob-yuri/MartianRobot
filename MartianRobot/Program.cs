using MartianRobot.Common;
using MartianRobot.Models;
using MartianRobot.Models.Commands;
using MartianRobot.Models.Factories;

Grid grid = Utils.ReadValuesFromConsole("Upper-right coordinates of the rectangular world: ", Grid.Parse);

while (true)
{
    Position robotPosition = Utils.ReadValuesFromConsole("Robot position: ", (string? robotCoordinates) => Position.Parse(robotCoordinates, grid.Right, grid.Upper));
    Robot robot = RobotFactory.CreateRobotOnGrid(robotPosition, grid);

    CommandEnum[] robotInstructions = Utils.ReadValuesFromConsole("Robot instructions: ", BaseCommand.Parse);

    Position finalPosition = robot.Move(robotInstructions);

    Console.Out.WriteLine($"Final grid position: {finalPosition}");
    Console.Out.WriteLine();
}
