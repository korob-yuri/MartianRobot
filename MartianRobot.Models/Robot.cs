using MartianRobot.Models.Commands;

namespace MartianRobot.Models
{
    public class Robot
    {
        private Grid _grid { get; set; }

        private Position _position { get; set; }

        public bool IsLost { get; private set; }

        public Robot(Position position, Grid grid)
        {
            _position = position;
            _grid = grid;
        }

        public void MarkAsLost()
        {
            IsLost = true;
        }        

        public void SetInitialPosition(Position position)
        {
            _position = position;
        }

        public override string ToString()
        {
            return $"{_position}{(IsLost ? " LOST" : "")}";
        }

        public Position Move(CommandEnum instruction)
        {
            if (_grid.WillLoseRobot(_position, instruction))
                return _position;

            BaseCommand command = BaseCommand.GetCommand(instruction);

            Position newPosition = command.Execute(_position);

            // per requirement, if we know that we will lose the robot, we ignore teh move.

            if (_grid.IsLost(newPosition))
            {
                // An instruction to move “off” the world from a grid point from which a robot has been previously lost is simply ignored by the current robot
                MarkAsLost();

                _grid.SaveRobotsScent(_position, instruction);
            }
            else
            {
                _position = newPosition;
            }

            return _position;
        }

        public Position Move(CommandEnum[] commands)
        {
            foreach(CommandEnum command in commands)
            {
                Move(command);
                if (IsLost)
                    break;
            }

            return _position;
        }
    }
}
