using MartianRobot.Common;
using System.Text.RegularExpressions;

namespace MartianRobot.Models
{
    /// <summary>
    /// Clas representing a robot's position
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Robot's origentation
        /// </summary>
        public OrientationEnum Orientation { get; private set; } = OrientationEnum.North;

        /// <summary>
        /// X coordinated of a robot on the grid
        /// </summary>
        public int X { get; private set;  }

        /// <summary>
        /// Y coordinated of a robot on the grid
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Conver a robots position to a string format for display purposes
        /// </summary>
        /// <returns>A robot's postion, for example: 5 3 E</returns>
        public override string ToString()
        {
            return $"{X} {Y} {(char)Orientation}" ;
        }

        /// <summary>
        /// Parse a string to the position object
        /// 
        /// Example:
        /// 
        /// var position = Position.Parse("4 3 N", 5, 5);
        /// 
        /// Result:
        /// 
        /// A position object with, X = 4, Y = 3, Orientaiton = "N"orth
        /// </summary>
        /// <param name="s">input string to parse</param>
        /// <param name="maxRight">Maximum valid X position on the grid</param>
        /// <param name="maxUpper">Maximum valid Y position on the grid</param>
        /// <returns>A positon object</returns>
        public static Tuple<Position?, string?> Parse(string? s, int maxRight, int maxUpper)
        {
            if (String.IsNullOrWhiteSpace(s))
                return new Tuple<Position?, string?>(null, "Please enter initial coordinates");            
            
            var match = Regex.Match(s, @"^\s*(\d+)\s+(\d+)\s+(" + Utils.AllEnumCharValuesRegexEscaped<OrientationEnum>("|") + @")\s*$");

            if (!match.Success)
                return new Tuple<Position?, string?>(null, $"Position must be in format:\r\n" +
                    $"  XXX YYY O\r\n" +
                    $"  where XXX is a number representing X  cordinate and is up to {maxRight}\r\n" +
                    $"  YYYY is a number representing Y coordinate and is up to {maxUpper}\r\n" +
                    $"  O is orientation and is one of {Utils.AllEnumCharValues<OrientationEnum>(", ")}");

            int x = Int32.Parse(match.Groups[1].Value);
            int y = Int32.Parse(match.Groups[2].Value);

            if(x > maxRight)
                return new Tuple<Position?, string?>(null, $"X value cannot be more than \"{maxRight}\", but it is \"{x}\"");

            if (y > maxUpper)
                return new Tuple<Position?, string?>(null, $"Y value cannot be more than \"{maxUpper}\", but it is \"{y}\"");

            string orientation = match.Groups[3].Value;
            if (String.IsNullOrEmpty(orientation))
                return new Tuple<Position?, string?>(null, "Must provide an orientation value");

            return new Tuple<Position?, string?>(new Position() {  X = x, Y = y, Orientation = (OrientationEnum)orientation[0] }, null);
        }

        /// <summary>
        /// Create a new positoin object
        /// 
        /// Example:
        /// 
        /// Position.CreatePositon(4, 3, OrientationEnum.West)
        /// 
        /// Result:
        /// 
        /// A position object with X = 4, Y = 3 and Orientation = "W"est
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="orientation">Orientation</param>
        /// <returns>A position object</returns>
        internal static Position CreatePositon(int x, int y, OrientationEnum orientation)
        {
            return new Position()
            {
                X = x,
                Y = y,
                Orientation = orientation
            };
        }
    }
}
