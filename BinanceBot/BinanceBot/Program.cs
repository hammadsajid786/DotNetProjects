using Microsoft.Win32;

namespace BinanceBot
{
    internal static class Program
    {

        public static string binanceAPIKey = string.Empty;
        public static string binanceAPISecret = string.Empty;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            ReadAPIKeys();
            if (string.IsNullOrEmpty(binanceAPIKey) || string.IsNullOrEmpty(binanceAPISecret))
            {
                MessageBox.Show("API keys required");
            }
            else
                Application.Run(new MainForm());
        }

        static void ReadAPIKeys()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\BinanceBot");

                if (key != null)
                {
                    binanceAPIKey = key.GetValue("binanceAPIKey").ToString();
                    binanceAPISecret = key.GetValue("binanceAPISecret").ToString();
                }
            }
            catch (Exception exc) { }
        }
    }
}