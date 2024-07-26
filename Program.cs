using NoGravEx_Entwurf2;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Waveplus.DaqExample
{
    static class Program
    {
        private static NoGravEx_V2 ngev2;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
                
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                ngev2 = new NoGravEx_V2();
                Application.Run(ngev2);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (ngev2 != null) ngev2.Dispose();
                Application.Exit();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Assigns a different color to the progressbar used in the GameForm
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="w"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}