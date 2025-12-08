using System;
using System.Windows.Forms;
using GradskiTransport.Presentation.Forms;

namespace GradskiTransport.Presentation
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Kreiranje glavne forme
            var mainForm = new MainForm();
            Application.Run(mainForm);
        }
    }
}
