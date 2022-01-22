using MartianRobot.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MartianRobot.Tests
{
    /// <summary>
    /// Positoin class testrs
    /// </summary>
    [TestClass]
    public class PositionTests
    {
        /// <summary>
        /// Test of proper postion validation
        /// </summary>
        [TestMethod]
        public void ParseValidPosition()
        {
            var position = Position.Parse("4 3 N", 5, 5);
            Assert.IsNotNull(position?.Item1);
            Assert.IsNull(position?.Item2);

            Assert.IsTrue(position?.Item1.X == 4 && position?.Item1.Y == 3 && position?.Item1.Orientation == OrientationEnum.North);
        }

        /// <summary>
        /// Test of negative scenario of non-existing orientaion 
        /// </summary>
        [TestMethod]
        public void ParseInvalidPosition()
        {
            var position = Position.Parse("3 3 U", 5, 5);
            Assert.IsNull(position?.Item1);
            Assert.IsNotNull(position?.Item2);
        }

        /// <summary>
        /// Test of negative scenario of out of grid boundries value
        /// </summary>
        [TestMethod]
        public void ParseOutOfBoundriesPosition()
        {
            var position = Position.Parse("6 3 N", 5, 5);
            Assert.IsNull(position?.Item1);
            Assert.IsNotNull(position?.Item2);
        }

        /// <summary>
        /// Test of proper string formating functionality
        /// </summary>
        [TestMethod]
        public void StringFormat()
        {
            var position = Position.Parse("5 3 N", 5, 5);
            Assert.IsNotNull(position?.Item1);
            Assert.IsNull(position?.Item2);

            Assert.IsTrue(position?.Item1.ToString() == "5 3 N");            
        }
    }
}
