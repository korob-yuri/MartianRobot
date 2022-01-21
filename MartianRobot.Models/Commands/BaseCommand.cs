using MartianRobot.Common;
using System.Text.RegularExpressions;

namespace MartianRobot.Models.Commands
{
    public abstract class BaseCommand
    {
        public const int MAX_INSTRUCTIONS_LENGTH = 100;

        private static Dictionary<CommandEnum, BaseCommand> _baseCommands = new();

        static BaseCommand() {
            // make sure to create a unit test to validate that all possible commands types are registered
            RegisterCommand(new LeftCommand());
            RegisterCommand(new RightCommand());
            RegisterCommand(new ForwardCommand());
        }

        protected static void RegisterCommand<T>(T command) where T : BaseCommand
        {
            _baseCommands[command.CommandName] = command;
        }

        public abstract  CommandEnum CommandName { get; }
       
        public abstract Position Execute(Position position);

        public static BaseCommand GetCommand(CommandEnum commandName)
        {
            if (_baseCommands.TryGetValue(commandName, out BaseCommand? command) && command != null)
                return command;

            throw (new Exception($"Cannot find a command \"{commandName}\", please implement"));
        }       

        public static Tuple<CommandEnum[]?, string?> Parse(string? instructions)
        {
            if (String.IsNullOrWhiteSpace(instructions))
                return new Tuple<CommandEnum[]?, string?>(null, "Please enter instructions for the robot");            

            if(instructions.Length > MAX_INSTRUCTIONS_LENGTH)
                return new Tuple<CommandEnum[]?, string?>(null, $"Exceeds maximum instructions size of {MAX_INSTRUCTIONS_LENGTH}, but it is {instructions.Length}");

            var match = Regex.Match(instructions, @"^\s*([" + Utils.AllEnumCharValuesRegexEscaped<CommandEnum>() + @"]+)\s*$");

            if (!match.Success)
                return new Tuple<CommandEnum[]?, string?>(null, $"Invalid instructions must be a string with values: {Utils.AllEnumCharValuesRegexEscaped<CommandEnum>(", ")}");

            return new Tuple<CommandEnum[]?, string?>(match.Groups[1].Value.ToCharArray().Select(s => (CommandEnum)s).ToArray(), null);
        }
    }
}
