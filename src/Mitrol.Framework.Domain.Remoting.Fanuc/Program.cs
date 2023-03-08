using System;
using System.Threading;
using System.Windows.Forms;

namespace Mitrol.Framework.Domain.Remoting.Fanuc
{
    static class Program
    {
        internal static readonly Mutex s_mutex = new Mutex(true, "{8573BD1A-1FE3-44D0-ACAE-E254A381B8B7}");

        internal static RemotingServerDialog s_frmMain = null;

        internal static void ShowNormal()
        {
            s_frmMain.TopMost = true;
            s_frmMain.ShowInTaskbar = true;
            s_frmMain.NotifyIcon.Visible = false;
            if (s_frmMain.IsDisposed) return;
            s_frmMain.Show();
            s_frmMain.Activate();
            s_frmMain.WindowState = FormWindowState.Normal;
            s_frmMain.TopMost = false;
        }

        internal static void ShowInTray()
        {
            s_frmMain.WindowState = FormWindowState.Minimized;
            s_frmMain.NotifyIcon.Visible = true;
            s_frmMain.Hide();
            s_frmMain.ShowInTaskbar = false;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (s_mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                s_frmMain = new RemotingServerDialog();
                Application.Run(s_frmMain);
                s_mutex.ReleaseMutex();
            }
        }
    }
}
