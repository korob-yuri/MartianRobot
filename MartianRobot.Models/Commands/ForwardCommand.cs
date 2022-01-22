namespace MartianRobot.Models.Commands
{
    /// <summary>
    /// Implementation of forward command
    /// </summary>
    public class ForwardCommand : BaseCommand
    {
        /// <summary>
        /// Name of a command as "Forward", encoded by char "F"
        /// </summary>
        public override CommandEnum CommandName { get; } = CommandEnum.Forward;

        /// <summary>
        /// Execute the "forward" command on a position
        /// </summary>
        public override Position Execute(Position position)
        {
            int newX = position.X;
            int newY = position.Y;

            switch(position.Orientation)
            {
                case OrientationEnum.North:
                    newY++;
                    break;

                case OrientationEnum.South:
                    newY--;
                    break;

                case OrientationEnum.West:
                    newX--;
                    break;

                case OrientationEnum.East:
                    newX++;
                    break;

                default:
                    throw (new Exception($"Unsupported orientation \"{position.Orientation}\""));
            };

            return Position.CreatePositon(newX, newY, position.Orientation);
        }
    }
}
