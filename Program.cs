using Serilog;
using TripPinClient.UI;

namespace TripPinClient
{
    class Program
    {

        private static readonly ConsoleHelper _consoleHelper = new();

        static void Main(string[] args)
        {
            //configure Serilog
            Log.Logger = new LoggerConfiguration()
                        .WriteTo.File("TripPinClient.log")
                        .CreateLogger();

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = _consoleHelper.PrintMenu().Result;
            }
        }

    }
}
