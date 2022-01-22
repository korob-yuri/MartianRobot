using MartianRobot.Common;
using MartianRobot.Models;
using MartianRobot.Models.Commands;

// ask for a a upper-right coordinates of a grid, lower-left are 0, 0
Grid grid = Utils.ReadValuesFromConsole("Upper-right coordinates of the rectangular world: ", Grid.Parse);

// repeat forever, you can exit by pressing Ctrl-C
while (true)
{
    // ask for robot position
    Position robotPosition = Utils.ReadValuesFromConsole("Robot position: ", (string? robotCoordinates) => Position.Parse(robotCoordinates, grid.Right, grid.Upper));
    Robot robot = new (robotPosition, grid);

    // ask for robot command instructions
    CommandEnum[] robotInstructions = Utils.ReadValuesFromConsole("Robot instructions: ", BaseCommand.Parse);

    // apply the command to the robbot
    Position finalPosition = robot.Move(robotInstructions);

    // display robot's resulting position 
    Console.Out.WriteLine($"Final grid position: {finalPosition}");
    Console.Out.WriteLine();
}
