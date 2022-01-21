namespace MartianRobot.Models.Commands
{
    public class LeftCommand : BaseCommand
    {
        public override CommandEnum CommandName { get; } = CommandEnum.Left;

        public override Position Execute(Position position)
        {
            OrientationEnum newOrientation = position.Orientation switch
            {
                OrientationEnum.North => OrientationEnum.West,
                OrientationEnum.West => OrientationEnum.South,
                OrientationEnum.South => OrientationEnum.East,
                OrientationEnum.East => OrientationEnum.North,
                _ => throw new NotImplementedException($"Cannot get orientaion for \"{position.Orientation}\""),
            };

            return Position.CreatePositon(position.X, position.Y, newOrientation);
        }        
    }
}
