using System.Text.RegularExpressions;

namespace MartianRobot.Common
{
    public static class Utils
    {
        /// <summary>
        /// Read values from a console and parse the values using the provided parsing function
        /// </summary>
        /// <typeparam name="TResult">Type of the end result of the parsing</typeparam>
        /// <param name="label">A text to be displayed at the console before asking to enter a value</param>
        /// <param name="func">Parsing function, it accepts a string and returns a value of a specified class</param>
        /// <returns></returns>
        public static TResult ReadValuesFromConsole<TResult>(string label, Func<string?, Tuple<TResult?, string?>> func) where TResult : class
        {
            TResult? result;

            while (true)
            {
                Console.Out.Write(label);
                string? instructions = Console.In.ReadLine();
                (result, string? errorMsg) = func(instructions);
                if (result != null)
                    break;

                Console.Error.WriteLine(errorMsg);
            }

            return result;
        }

        /// <summary>
        /// Returns a string of all enum values converted to a char and joined using separator. The char value is being escaped to be regex-safe
        /// 
        /// Example: 
        /// 
        /// Utils.AllEnumCharValuesRegexEscaped<OrientationEnum>("|")
        /// 
        /// Result:
        /// 
        /// N|S|E|W
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="separator">Separator for the joining the chars together</param>
        /// <returns>list of all enum values as a string, joined using a provided separated. It is regex safe, i.e. if a char for an enum value is |, it will be escaped to \|</returns>
        public static string AllEnumCharValuesRegexEscaped<TEnum>(string separator = "") where TEnum : struct, Enum
        {            
            return String.Join(separator, Enum.GetValues<TEnum>().Select(s => Convert.ChangeType(s, s.GetTypeCode())).Select(s => Regex.Escape(Convert.ToChar(s).ToString())));
        }

        /// <summary>
        /// Returns a string of all enum values converted to a char and joined using separator. 
        /// 
        /// Example:
        /// 
        /// Utils.AllEnumCharValues<CommandEnum>(", ")
        /// 
        /// Result:
        /// 
        /// L, R, F
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="separator">Separator for the joining the chars together</param>
        /// <returns>list of all enum values as a string, joined using a provided separated</returns>
        public static string AllEnumCharValues<TEnum>(string separator = "") where TEnum : struct, Enum
        {
            return String.Join(separator, Enum.GetValues<TEnum>().Select(s => Convert.ChangeType(s, s.GetTypeCode())).Select(s => Convert.ToChar(s).ToString()));
        }
    }
}