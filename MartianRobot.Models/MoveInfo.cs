namespace MartianRobot.Models
{
    /// <summary>
    /// The move information
    /// </summary>
    public class MoveInfo
    {
        public Position Position { get; set; } = new();
        public CommandEnum Command { get; set; } = CommandEnum.Forward;
    }
}
