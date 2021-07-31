using Serilog;

namespace TripPinClient.Logs
{
    public class Logger
    {
        public static void Info(string message)
        {
            Log.Information(message);
        }

        public static void Error(string message)
        {
            Log.Error(message);
        }
    }
}
