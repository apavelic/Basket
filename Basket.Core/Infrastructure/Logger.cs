using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Infrastructure
{
    public class Logger
    {
        public static void Log(string message, LogType type)
        {
            var color = GetForegroundColor(type);
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static ConsoleColor GetForegroundColor(LogType type)
        {
            switch (type)
            {
                case LogType.Error:
                    return ConsoleColor.Red;
                case LogType.Information:
                    return ConsoleColor.Yellow;
                case LogType.Success:
                    return ConsoleColor.Green;
                case LogType.Warning:
                    return ConsoleColor.DarkYellow;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}
