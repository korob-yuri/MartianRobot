namespace MartianRobot.Models.Commands
{
    public class ForwardCommand : BaseCommand
    {
        public override CommandEnum CommandName { get; } = CommandEnum.Forward;

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
