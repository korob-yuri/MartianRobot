using MartianRobot.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MartianRobot.Tests
{
    /// <summary>
    /// Robot class tests
    /// </summary>
    [TestClass]
    public class RobotTests
    {
        /// <summary>
        /// Test of proper string formating functionality
        /// </summary>
        [TestMethod]
        public void StringFormat()
        {
            var grid = Grid.Parse("5 5")?.Item1;

            Assert.IsNotNull(grid);

            var position = Position.Parse("5 3 N", grid.Right, grid.Upper);
            Assert.IsNotNull(position?.Item1);
            Assert.IsNull(position?.Item2);

            Assert.IsNotNull(position);

            Robot robot = new (position.Item1, grid);

            Assert.IsTrue(robot.ToString() == "5 3 N");

            robot.MarkAsLost();

            Assert.IsTrue(robot.ToString() == "5 3 N LOST");
        }
    }
}
