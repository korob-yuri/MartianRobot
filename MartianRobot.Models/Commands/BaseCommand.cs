using MartianRobot.Common;
using System.Text.RegularExpressions;

namespace MartianRobot.Models.Commands
{
    /// <summary>
    /// base abstract type for all commands
    /// </summary>
    public abstract class BaseCommand
    {
        /// <summary>
        /// Maximum allowed lenght of instructions string 
        /// </summary>
        public const int MAX_INSTRUCTIONS_LENGTH = 100;

        private static Dictionary<CommandEnum, BaseCommand> _baseCommands = new();

        /// <summary>
        /// Registering all possible commands on the first use
        /// </summary>
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

        /// <summary>
        /// Name of a command as an enum value
        /// </summary>
        public abstract  CommandEnum CommandName { get; }

        /// <summary>
        /// Execute the command on a position
        /// </summary>
        /// <param name="position">a start position</param>
        /// <returns>new position after the command is applied to the start position</returns>
        public abstract Position Execute(Position position);

        /// <summary>
        /// Get a command by a command enum value
        /// </summary>
        /// <param name="commandName">enum value</param>
        /// <returns>Command implementation</returns>
        public static BaseCommand GetCommand(CommandEnum commandName)
        {
            if (_baseCommands.TryGetValue(commandName, out BaseCommand? command) && command != null)
                return command;

            throw (new Exception($"Cannot find a command \"{commandName}\", please implement"));
        }

        /// <summary>
        /// Parse a command from a string format
        /// 
        /// Example:
        /// 
        /// BaseCommand.Parse("LRF")
        /// 
        /// Result is an array of three enums:
        /// 
        /// [CommandEnum.Left,CommandEnum.Right,CommandEnum.Forward]
        ///         
        /// </summary>
        /// <param name="instructions">a string representation of the command, for example: LRF</param>
        /// <returns>Array of commnad enums</returns>
        public static Tuple<CommandEnum[]?, string?> Parse(string? instructions)
        {
            if (String.IsNullOrWhiteSpace(instructions))
                return new Tuple<CommandEnum[]?, string?>(null, "Please enter instructions for the robot");            

            if(instructions.Length > MAX_INSTRUCTIONS_LENGTH)
                return new Tuple<CommandEnum[]?, string?>(null, $"Exceeds maximum instructions size of {MAX_INSTRUCTIONS_LENGTH}, but it is {instructions.Length}");

            var match = Regex.Match(instructions, @"^\s*([" + Utils.AllEnumCharValuesRegexEscaped<CommandEnum>() + @"]+)\s*$");

            if (!match.Success)
                return new Tuple<CommandEnum[]?, string?>(null, $"Invalid instructions must be a string with values: {Utils.AllEnumCharValues<CommandEnum>(", ")}");

            return new Tuple<CommandEnum[]?, string?>(match.Groups[1].Value.ToCharArray().Select(s => (CommandEnum)s).ToArray(), null);
        }
    }
}
