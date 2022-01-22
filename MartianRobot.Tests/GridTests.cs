using MartianRobot.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MartianRobot.Tests
{
    /// <summary>
    /// Grid class tests
    /// </summary>
    [TestClass]
    public class GridTests
    {
        /// <summary>
        /// Test proper parsing functionality
        /// </summary>
        [TestMethod]
        public void ParseValidGridDimensions()
        {
            var grid = Grid.Parse("5 3");
            Assert.IsNotNull(grid?.Item1);
            Assert.IsNull(grid?.Item2);

            Assert.IsTrue(grid?.Item1.Upper == 5);
            Assert.IsTrue(grid?.Item1.Right == 3);
        }

        /// <summary>
        /// Test of negative scenario of exceeding maximum size of the grid
        /// </summary>
        [TestMethod]
        public void ParseInvalidGridDimensions()
        {
            var grid = Grid.Parse("51 52");

            Assert.IsNull(grid?.Item1);
            Assert.IsNotNull(grid?.Item2);
        }
    }
}
