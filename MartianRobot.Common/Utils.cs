using System.Text.RegularExpressions;

namespace MartianRobot.Common
{
    public static class Utils
    {
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

        public static string AllEnumCharValuesRegexEscaped<TEnum>(string separator = "") where TEnum : struct, Enum
        {            
            return String.Join(separator, Enum.GetValues<TEnum>().Select(s => Convert.ChangeType(s, s.GetTypeCode())).Select(s => Regex.Escape(Convert.ToChar(s).ToString())));
        }

        public static string AllEnumCharValues<TEnum>(string separator = "") where TEnum : struct, Enum
        {
            return String.Join(separator, Enum.GetValues<TEnum>().Select(s => Convert.ChangeType(s, s.GetTypeCode())).Select(s => Convert.ToChar(s).ToString()));
        }
    }
}