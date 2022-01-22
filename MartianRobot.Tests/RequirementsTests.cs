using MartianRobot.Models;
using MartianRobot.Models.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MartianRobot.Tests
{
    /// <summary>
    /// Test of requirements implementation
    /// </summary>    
    [TestClass]
    public class RequirementsTests
    {
        /// <summary>
        /// End-to-end test of the provided scenarios
        /// </summary>
        [TestMethod]
        public void RequirementsEnd2EndTest()
        {
            Grid? grid = Grid.Parse("5 3").Item1;

            Assert.IsNotNull(grid);

            // --- step 1 ---

            Position? robotPosition = Position.Parse("1 1 E", grid.Right, grid.Upper).Item1;

            Assert.IsNotNull(robotPosition);

            Robot robot = new (robotPosition, grid);

            CommandEnum[]? robotInstructions = BaseCommand.Parse("RFRFRFRF").Item1;

            Assert.IsNotNull(robotInstructions);

            Position finalPosition = robot.Move(robotInstructions);

            // step #1 validation expected: 1 1 E
            Assert.IsTrue(finalPosition.X == 1);
            Assert.IsTrue(finalPosition.Y == 1);
            Assert.IsTrue(finalPosition.Orientation == OrientationEnum.East);
            Assert.IsTrue(!robot.IsLost);

            // --- step 2 ---

            robotPosition = Position.Parse("3 2 N", grid.Right, grid.Upper).Item1;

            Assert.IsNotNull(robotPosition);

            robot = new(robotPosition, grid);

            robotInstructions = BaseCommand.Parse("FRRFLLFFRRFLL").Item1;

            Assert.IsNotNull(robotInstructions);

            finalPosition = robot.Move(robotInstructions);

            // step #2 per requirement doc, it is expected: 3 3 N LOST
            // but based on the calculations, FRRFLLFFRRFLL doesn't take the roibot off the grid             
            Assert.IsTrue(finalPosition.X == 3);
            Assert.IsTrue(finalPosition.Y == 3);
            Assert.IsTrue(finalPosition.Orientation == OrientationEnum.North);
            Assert.IsTrue(!robot.IsLost);

            // --- step 3 ---

            robotPosition = Position.Parse("0 3 W", grid.Right, grid.Upper).Item1;

            Assert.IsNotNull(robotPosition);

            robot = new(robotPosition, grid);

            robotInstructions = BaseCommand.Parse("LLFFFLFLFL").Item1;

            Assert.IsNotNull(robotInstructions);

            finalPosition = robot.Move(robotInstructions);

            // step #3 base on the requirements doc, validation expected: 2 3 S
            // but based on calculations for LLFFFLFLFL it is: 2 4 S
            Assert.IsTrue(finalPosition.X == 2);
            Assert.IsTrue(finalPosition.Y == 4);
            Assert.IsTrue(finalPosition.Orientation == OrientationEnum.South);
            Assert.IsTrue(!robot.IsLost);
        }

        /// <summary>
        /// Test of the proper functionality of the logic for the lost robot senarios
        /// </summary>
        [TestMethod]
        public void LostLogic()
        {
            Grid? grid = Grid.Parse("5 3").Item1;

            Assert.IsNotNull(grid);

            Position? robotPosition = Position.Parse("2 2 N", grid.Right, grid.Upper).Item1;

            Assert.IsNotNull(robotPosition);

            Robot robot = new(robotPosition, grid);

            CommandEnum[]? robotInstructions = BaseCommand.Parse("FFFLFRFLF").Item1;

            Assert.IsNotNull(robotInstructions);

            Position finalPosition = robot.Move(robotInstructions);

            // must be lost on position: 1 5 N
            Assert.IsTrue(finalPosition.X == 1);
            Assert.IsTrue(finalPosition.Y == 5);
            Assert.IsTrue(finalPosition.Orientation == OrientationEnum.North);
            Assert.IsTrue(robot.IsLost);

            // repeat steps, but now (per requirements), the robot should recognize the last robot scent
            // and ignore the step that would take it off the grid
            robotPosition = Position.Parse("2 2 N", grid.Right, grid.Upper).Item1;

            Assert.IsNotNull(robotPosition);

            robot = new(robotPosition, grid);

            robotInstructions = BaseCommand.Parse("FFFLFRFLF").Item1;                                                   

            Assert.IsNotNull(robotInstructions);

            finalPosition = robot.Move(robotInstructions);

            Assert.IsTrue(finalPosition.X == 0);
            Assert.IsTrue(finalPosition.Y == 5);
            Assert.IsTrue(finalPosition.Orientation == OrientationEnum.West);
            Assert.IsTrue(!robot.IsLost);
        }
    }
}
