using STOTP;
using System;
using System.Configuration;
using System.Linq;

namespace SMFA
{
    class Program
    {
        /// <summary>
        /// Bare bones One Time Password generator using TOTP
        /// The key and optional related info are stored in app.config
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // TotpKey: this is provided when you set up the authenticator as a Base32 encoded string
            // OtpDigits: Number of digits to use (default is 6)
            // OtpWindow: Seconds in window for TOTP (rate codes refresh, default is 30s)
            var totp = new TOTP(
                ConfigurationManager.AppSettings.Get("TotpKey"),
                Int32.Parse(ConfigurationManager.AppSettings.Get("OtpDigits") ?? TOTP.DefaultLength.ToString()),
                Int32.Parse(ConfigurationManager.AppSettings.Get("OtpWindow") ?? TOTP.DefaultTimeInterval.ToString()));

            // Account name is not needed but if given set the title
            Console.Title = ConfigurationManager.AppSettings.Get("TotpAccountName") ?? "TOTP OTP";

            while (true)
            {
                EraseConsole();
                Console.WriteLine($"{totp.Otp} : {SecondsRemaining(totp.SecondsRemaining)} [{RemainingProgressBar(totp.SecondsRemaining, totp.Window)}]");
                System.Threading.Thread.Sleep(250);
            } 
        }

        static void EraseConsole()
        {
            // Not efficient but good enough
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(String.Concat(Enumerable.Repeat(" ", 80)));
            Console.SetCursorPosition(0, 0);
        }

        static string SecondsRemaining(int secondsRemaining)
        {
            return Convert.ToString(secondsRemaining).PadLeft(2, '0');
        }

        static string RemainingProgressBar(int secondsRemaining, int window, int maxChars = 15)
        {
            // returns string of #s as a length proportional to time remaining
            var percentageRemaining = (decimal)secondsRemaining / (decimal)window;
            return String.Concat(Enumerable.Repeat("#", (int)(percentageRemaining * maxChars))).PadRight(maxChars, ' ');
        }
    }
}
