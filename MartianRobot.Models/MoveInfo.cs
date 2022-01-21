namespace MartianRobot.Models
{
    public class MoveInfo
    {
        public Position Position { get; set; } = new();
        public CommandEnum Command { get; set; } = CommandEnum.Forward;
    }
}
