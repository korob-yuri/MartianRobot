using MartianRobot.Models.Commands;

namespace MartianRobot.Models
{
    /// <summary>
    /// Class represenatin a robot
    /// </summary>
    public class Robot
    {
        /// <summary>
        /// Grid were the robot is located
        /// </summary>
        private Grid _grid { get; set; }

        /// <summary>
        /// Current robot's position
        /// </summary>
        private Position _position { get; set; }

        /// <summary>
        /// A flag identifying if a robot is lost
        /// </summary>
        public bool IsLost { get; private set; }

        /// <summary>
        /// Constructor to create a robot
        /// </summary>
        /// <param name="position">Initial robot's position</param>
        /// <param name="grid">A grid were the robot will be placed</param>
        public Robot(Position position, Grid grid)
        {
            _position = position;
            _grid = grid;
        }

        /// <summary>
        /// Mark the robot as lost
        /// </summary>
        public void MarkAsLost()
        {
            IsLost = true;
        }        

        /// <summary>
        /// Set robot's initial position
        /// </summary>
        /// <param name="position">Position object</param>
        public void SetInitialPosition(Position position)
        {
            _position = position;
        }

        /// <summary>
        /// Convert the robot position and lost status to a string for display purposes
        /// </summary>
        /// <returns>A string of the robot position and lost status, for example: 4 3 E LOST</returns>
        public override string ToString()
        {
            return $"{_position}{(IsLost ? " LOST" : "")}";
        }

        /// <summary>
        /// Move a robot on the grid
        /// </summary>
        /// <param name="instruction">A command instruction to move the robot</param>
        /// <returns>New robots position</returns>
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

        /// <summary>
        /// Do multiple moves of the robot
        /// </summary>
        /// <param name="commands">Commands for the moves</param>
        /// <returns>Final position of the robot after all the moves executed</returns>
        public Position Move(IEnumerable<CommandEnum> commands)
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
