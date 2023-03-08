using Mitrol.Fanuc.Rpc;
using System;
using System.Windows.Forms;

namespace Mitrol.Framework.Domain.Remoting.Fanuc
{
    public partial class RemotingServerDialog : Form
    {
        public NotifyIcon NotifyIcon => notifyIcon;

        public FanucRpcServer _fanucRpcServer = null;

        public static string FanucIpAddress { get; private set; }
        public static int FanucIpPort { get; private set; }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                _fanucRpcServer.Stop();
                _fanucRpcServer.Dispose();
            }
            base.Dispose(disposing);
        }


        public RemotingServerDialog()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.polaris_logo_transparent_dark;
            this.notifyIcon.Icon = Properties.Resources.polaris_logo_transparent_dark;
            this.notifyIcon.Text = "FANUC remoting server";
            this.notifyIcon.Click += (s, e) => Program.ShowNormal();

            FanucIpAddress = Common.FanucIpAddress;
            FanucIpPort = int.Parse(Common.FanucIpPort);
        }

        private void RemotingServerDialog_Load(object sender, EventArgs e)
        {
            Program.ShowInTray();

            try
            {
                Log("Starting server instance");
                _fanucRpcServer = new FanucRpcServer(FanucIpAddress);
                _fanucRpcServer.LogMessage += (s, e2) =>
                {
                    // scrivo solo messaggi > Info oppure tutti
                    if (e2.Level > LogLevelEnum.Info || checkBoxDetailedLog.Checked)
                    {
                        Log($"{e2.Level}: {e2.Message}");
                    }
                };
                _fanucRpcServer.Start();

                Log($"Server started, listening on port 8193");
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error starting server on port 8193: {ex.Message}";
                Log(errorMessage);
                MessageBox.Show(
                    this,
                    errorMessage,
                    "Error starting Rpc server",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Close();
            }
        }

        /// <summary>
        /// Scrive nella textbox
        /// </summary>
        public void Log(string text)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => Log(text)));
                return;
            }
            txtLog.AppendText(text);
            txtLog.AppendText(Environment.NewLine);
        }

        private void ExitUserControl_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MinimizeUserControl_Click(object sender, EventArgs e) => Program.ShowInTray();

        private void RestartUserControl_Click(object sender, EventArgs e)
        {
        }
    }
}
