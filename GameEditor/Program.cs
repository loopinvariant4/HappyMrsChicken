using System;
using System.Windows.Forms;

namespace GameEditor
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GameEditor game = null;
            try
            {
                game = new GameEditor();
                InfoForm infoForm = new InfoForm(game);
                infoForm.Left = 2570;
                infoForm.Top = 100;
                infoForm.Show();
                game.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unhandled exception: " + ex.ToString());
            }
            finally
            {
                game.Dispose();
            }
        }
    }
#endif
}
