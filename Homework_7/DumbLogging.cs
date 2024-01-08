using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    class DumbLogging
    {
        private void reset() 
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void successLog(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now.ToLocalTime()} | {message}");
            reset();
        }

        public void failureLog(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now.ToLocalTime()} | {message}");
            reset();
        }

        public void infoLog(string message)
        {
            Console.ForegroundColor = ConsoleColor.White; 
            Console.WriteLine($"{DateTime.Now.ToLocalTime()} | {message}");
            reset();
        }
    }
}
