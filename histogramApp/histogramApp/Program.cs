using System;
using System.Windows.Forms;

namespace HistogramApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Uruchamianie g��wnego formularza
            Application.Run(new Form1());
        }
    }
}
