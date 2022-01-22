using System.Text.RegularExpressions;

namespace MartianRobot.Models
{
    /// <summary>
    /// Implementation of a grid
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Maximum supported coordinates for the grid
        /// </summary>
        public const int MAX_COORDINATE = 50;

        /// <summary>
        /// Upper corner of the grid
        /// </summary>
        public int Upper { get; set; }

        /// <summary>
        /// Right corner of the grid
        /// </summary>
        public int Right { get; set; }

        /// <summary>
        /// List of the moves after which the robots were lost
        /// </summary>
        private List<MoveInfo> _lostMoves = new();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="upper">Upper corner of the grid</param>
        /// <param name="right">Right corner of the grid</param>
        public Grid(int upper, int right)
        {
            Upper = upper;
            Right = right;  
        }

        public bool IsLost(Position postion)
        {
            return (postion.X > Right) || (postion.Y > Upper);
        }

        /// <summary>
        /// Parse a string to the grid object
        /// 
        /// Example:
        /// 
        /// var grid = Grid.Parse("5 3");
        /// 
        /// Result:
        /// 
        /// A grid object with Upper=5 and Right=3
        /// </summary>
        /// <param name="s">a string</param>
        /// <returns>A grid object</returns>
        public static Tuple<Grid?, string?> Parse(string? s)
        {
            if (String.IsNullOrWhiteSpace(s))
                return new Tuple<Grid?, string?>(null, "Please enter initial coordinates");

            var match = Regex.Match(s, @"^\s*(\d+)\s+(\d+)\s*$");

            if (!match.Success)
                return new Tuple<Grid?, string?>(null, $"Coordinates must be in format:\r\n" +
                    $"   NNN NNN\r\n" +
                    $"   where NNN is a number up to {MAX_COORDINATE}");

            int upper = Int32.Parse(match.Groups[1].Value);

            if(upper <= 0)
                return new Tuple<Grid?, string?>(null, $"Cannot be zero or negative value");

            if (upper > MAX_COORDINATE)
                return new Tuple<Grid?, string?>(null, $"Cannot be more than {MAX_COORDINATE}, but it is \"{upper}\"");

            int right = Int32.Parse(match.Groups[2].Value);
            if (right <= 0)
                return new Tuple<Grid?, string?>(null, $"Cannot be zero or negative value");

            if (right > MAX_COORDINATE)
                return new Tuple<Grid?, string?>(null, $"Cannot be more than {MAX_COORDINATE}, but it is \"{right}\"");

            return new Tuple<Grid?, string?>(new Grid(upper, right), null);
        }

        /// <summary>
        /// Save the last robot's scent if after the move, the robot will be lost
        /// </summary>
        /// <param name="position">The last valid robot's position</param>
        /// <param name="command">A command that was applied after which the robot was lost</param>
        internal void SaveRobotsScent(Position position, CommandEnum command)
        {
            _lostMoves.Add(new MoveInfo() { Position = position, Command = command });
        }

        /// <summary>
        /// Validate if a robot will be lost after the move
        /// </summary>
        /// <param name="position">Current robot's position</param>
        /// <param name="command">A command that will be applied</param>
        /// <returns></returns>
        public bool WillLoseRobot(Position position, CommandEnum command)
        {
            return _lostMoves.Any(w => w.Position.X == position.X
                                    && w.Position.Y == position.Y
                                    && w.Position.Orientation == position.Orientation
                                    && w.Command == command);
        }        
    }
}