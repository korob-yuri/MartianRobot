namespace MartianRobot.Models.Commands
{
    public class RightCommand : BaseCommand
    {
        public override CommandEnum CommandName { get; } = CommandEnum.Right;

        public override Position Execute(Position position)
        {
            OrientationEnum newOrientation = position.Orientation switch
            {
                OrientationEnum.North => OrientationEnum.East,
                OrientationEnum.West => OrientationEnum.North,
                OrientationEnum.South => OrientationEnum.West,
                OrientationEnum.East => OrientationEnum.South,
                _ => throw new NotImplementedException($"Cannot get orientaion for \"{position.Orientation}\""),
            };

            return Position.CreatePositon(position.X, position.Y, newOrientation);
        }        
    }
}
