using MartianRobot.Models;
using MartianRobot.Models.Commands;
using MartianRobot.Models.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MartianRobot.Tests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        public void ParseValidGridDimensions()
        {
            var grid = Grid.Parse("5 3");
            Assert.IsNotNull(grid?.Item1);
            Assert.IsNull(grid?.Item2);

            Assert.IsTrue(grid?.Item1.Upper == 5);
            Assert.IsTrue(grid?.Item1.Right == 3);
        }

        [TestMethod]
        public void ParseInvalidGridDimensions()
        {
            var grid = Grid.Parse("51 52");

            Assert.IsNull(grid?.Item1);
            Assert.IsNotNull(grid?.Item2);
        }
    }
}
