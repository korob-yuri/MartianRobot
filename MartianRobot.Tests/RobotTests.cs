using MartianRobot.Models;
using MartianRobot.Models.Commands;
using MartianRobot.Models.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MartianRobot.Tests
{
    [TestClass]
    public class RobotTests
    {
        [TestMethod]
        public void StringFormat()
        {
            var grid = Grid.Parse("5 5")?.Item1;

            Assert.IsNotNull(grid);

            var position = Position.Parse("5 3 N", grid.Right, grid.Upper);
            Assert.IsNotNull(position?.Item1);
            Assert.IsNull(position?.Item2);

            Assert.IsNotNull(position);

            var robot = RobotFactory.CreateRobotOnGrid(position.Item1, grid); 

            Assert.IsTrue(robot.ToString() == "5 3 N");

            robot.MarkAsLost();

            Assert.IsTrue(robot.ToString() == "5 3 N LOST");
        }
    }
}
