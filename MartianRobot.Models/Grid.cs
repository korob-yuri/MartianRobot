using System.Text.RegularExpressions;

namespace MartianRobot.Models
{
    public class Grid
    {
        public const int MAX_COORDINATE = 50;

        public int Upper { get; set; }
        public int Right { get; set; }

        private List<MoveInfo> _lostMoves = new();

        public Grid(int upper, int right)
        {
            Upper = upper;
            Right = right;  
        }

        public bool IsLost(Position postion)
        {
            return (postion.X > Right) || (postion.Y > Upper);
        }

        public void AddToLostMoves(MoveInfo moveInfo)
        {
            _lostMoves.Add(moveInfo);
        }

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

        internal void SaveRobotsScent(Position position, CommandEnum command)
        {
            _lostMoves.Add(new MoveInfo() { Position = position, Command = command });
        }

        public bool WillLoseRobot(Position position, CommandEnum command)
        {
            return _lostMoves.Any(w => w.Position.X == position.X
                                    && w.Position.Y == position.Y
                                    && w.Position.Orientation == position.Orientation
                                    && w.Command == command);
        }        
    }
}