using MartianRobot.Common;
using System.Text.RegularExpressions;

namespace MartianRobot.Models
{
    public class Position
    {
        public OrientationEnum Orientation { get; private set; } = OrientationEnum.North;
        public int X { get; private set;  }
        public int Y { get; private set; }


        public override string ToString()
        {
            return $"{X} {Y} {(char)Orientation}" ;
        }       

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
