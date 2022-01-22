using MartianRobot.Models;
using MartianRobot.Models.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MartianRobot.Tests
{
    /// <summary>
    /// Test command class
    /// </summary>
    [TestClass]
    public class CommandTests
    {
        /// <summary>
        /// Validate that all enum values in the CommandEnum are registered 
        /// </summary>
        [TestMethod]
        public void CommandRegistrations()
        {
            foreach (CommandEnum command in Enum.GetValues<CommandEnum>())
            {
                Assert.IsNotNull(BaseCommand.GetCommand(command), $"Cannot get a command for {command}");
            }
        }

        /// <summary>
        /// Validate parse functonality
        /// </summary>
        [TestMethod]
        public void ParseValidCommands()
        {
            var commands = BaseCommand.Parse("LRF");
            Assert.IsNotNull(commands?.Item1);
            Assert.IsNull(commands?.Item2);
        }

        /// <summary>
        /// Test proper validation of maximum lenght of the input string
        /// </summary>
        [TestMethod]
        public void ParseMaxLengthValidation()
        {
            var commands = BaseCommand.Parse(new String('R', BaseCommand.MAX_INSTRUCTIONS_LENGTH + 1));
            Assert.IsNull(commands?.Item1);
            Assert.IsNotNull(commands?.Item2);
        }

        /// <summary>
        /// Negative test scenartio of parse functonality validation, i.e. non-existing command in the input string
        /// </summary>
        [TestMethod]
        public void ParseNonExistingCommands()
        {
            var commands = BaseCommand.Parse("LRFU");
            Assert.IsNull(commands?.Item1);
            Assert.IsNotNull(commands?.Item2);
        }

        /// <summary>
        /// Test an empty string validation for the command parsing
        /// </summary>
        [TestMethod]
        public void ParseEmptyCommands()
        {
            var commands = BaseCommand.Parse("");
            Assert.IsNull(commands?.Item1);
            Assert.IsNotNull(commands?.Item2);
        }
    }
}
