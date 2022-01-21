using MartianRobot.Models;
using MartianRobot.Models.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MartianRobot.Tests
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void CommandRegistrations()
        {
            foreach (CommandEnum command in Enum.GetValues<CommandEnum>())
            {
                Assert.IsNotNull(BaseCommand.GetCommand(command), $"Cannot get a command for {command}");
            }
        }

        [TestMethod]
        public void ParseValidCommands()
        {
            var commands = BaseCommand.Parse("LRF");
            Assert.IsNotNull(commands?.Item1);
            Assert.IsNull(commands?.Item2);
        }

        [TestMethod]
        public void ParseMaxLengthValidation()
        {
            var commands = BaseCommand.Parse(new String('R', BaseCommand.MAX_INSTRUCTIONS_LENGTH + 1));
            Assert.IsNull(commands?.Item1);
            Assert.IsNotNull(commands?.Item2);
        }

        [TestMethod]
        public void ParseNonExistingCommands()
        {
            var commands = BaseCommand.Parse("LRFU");
            Assert.IsNull(commands?.Item1);
            Assert.IsNotNull(commands?.Item2);
        }

        [TestMethod]
        public void ParseEmptyCommands()
        {
            var commands = BaseCommand.Parse("");
            Assert.IsNull(commands?.Item1);
            Assert.IsNotNull(commands?.Item2);
        }
    }
}
