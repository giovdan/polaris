namespace Mitrol.Framework.Domain.Remoting.Fanuc
{
    partial class RemotingServerDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.actionsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ExitUserControl = new Mitrol.Framework.StartPolaris.Models.Controls.ButtonUserControl();
            this.MinimizeUserControl = new Mitrol.Framework.StartPolaris.Models.Controls.ButtonUserControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.checkBoxDetailedLog = new System.Windows.Forms.CheckBox();
            this.tableLayoutMain.SuspendLayout();
            this.actionsFlowLayoutPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Visible = true;
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutMain.ColumnCount = 2;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.5415F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.4585F));
            this.tableLayoutMain.Controls.Add(this.actionsFlowLayoutPanel, 0, 0);
            this.tableLayoutMain.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 1;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutMain.Size = new System.Drawing.Size(996, 740);
            this.tableLayoutMain.TabIndex = 11;
            // 
            // actionsFlowLayoutPanel
            // 
            this.actionsFlowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.actionsFlowLayoutPanel.Controls.Add(this.ExitUserControl);
            this.actionsFlowLayoutPanel.Controls.Add(this.MinimizeUserControl);
            this.actionsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.actionsFlowLayoutPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.actionsFlowLayoutPanel.Location = new System.Drawing.Point(5, 5);
            this.actionsFlowLayoutPanel.Name = "actionsFlowLayoutPanel";
            this.actionsFlowLayoutPanel.Size = new System.Drawing.Size(207, 730);
            this.actionsFlowLayoutPanel.TabIndex = 9;
            // 
            // ExitUserControl
            // 
            this.ExitUserControl.BackColor = System.Drawing.Color.DimGray;
            this.ExitUserControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExitUserControl.ForeColor = System.Drawing.Color.White;
            this.ExitUserControl.Highlighted = false;
            this.ExitUserControl.ImageIcon = global::Mitrol.Framework.Domain.Remoting.Fanuc.Properties.Resources.close;
            this.ExitUserControl.Location = new System.Drawing.Point(3, 17);
            this.ExitUserControl.Margin = new System.Windows.Forms.Padding(3, 17, 3, 17);
            this.ExitUserControl.Name = "ExitUserControl";
            this.ExitUserControl.Size = new System.Drawing.Size(204, 59);
            this.ExitUserControl.TabIndex = 9;
            this.ExitUserControl.TextCaption = "Exit Application";
            this.ExitUserControl.Click += new System.EventHandler(this.ExitUserControl_Click);
            // 
            // MinimizeUserControl
            // 
            this.MinimizeUserControl.BackColor = System.Drawing.Color.DimGray;
            this.MinimizeUserControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MinimizeUserControl.ForeColor = System.Drawing.Color.White;
            this.MinimizeUserControl.Highlighted = false;
            this.MinimizeUserControl.ImageIcon = global::Mitrol.Framework.Domain.Remoting.Fanuc.Properties.Resources.minimize;
            this.MinimizeUserControl.Location = new System.Drawing.Point(3, 108);
            this.MinimizeUserControl.Margin = new System.Windows.Forms.Padding(3, 15, 3, 15);
            this.MinimizeUserControl.Name = "MinimizeUserControl";
            this.MinimizeUserControl.Size = new System.Drawing.Size(204, 59);
            this.MinimizeUserControl.TabIndex = 7;
            this.MinimizeUserControl.TextCaption = "Minimize";
            this.MinimizeUserControl.Click += new System.EventHandler(this.MinimizeUserControl_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.checkBoxDetailedLog);
            this.flowLayoutPanel1.Controls.Add(this.txtLog);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(220, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(771, 730);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.Gray;
            this.txtLog.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.ForeColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(3, 52);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(771, 702);
            this.txtLog.TabIndex = 9;
            this.txtLog.WordWrap = false;
            // 
            // checkBox2
            // 
            this.checkBoxDetailedLog.AutoSize = true;
            this.checkBoxDetailedLog.Font = new System.Drawing.Font("Saira", 12F);
            this.checkBoxDetailedLog.ForeColor = System.Drawing.Color.White;
            this.checkBoxDetailedLog.Location = new System.Drawing.Point(10, 10);
            this.checkBoxDetailedLog.Margin = new System.Windows.Forms.Padding(10);
            this.checkBoxDetailedLog.Name = "checkBox2";
            this.checkBoxDetailedLog.Size = new System.Drawing.Size(123, 29);
            this.checkBoxDetailedLog.TabIndex = 10;
            this.checkBoxDetailedLog.Text = "Detailed Log";
            this.checkBoxDetailedLog.UseVisualStyleBackColor = true;
            // 
            // RemotingServerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1008, 752);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "RemotingServerDialog";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.RemotingServerDialog_Load);
            this.tableLayoutMain.ResumeLayout(false);
            this.actionsFlowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.FlowLayoutPanel actionsFlowLayoutPanel;
        private StartPolaris.Models.Controls.ButtonUserControl ExitUserControl;
        private StartPolaris.Models.Controls.ButtonUserControl MinimizeUserControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.CheckBox checkBoxDetailedLog;
    }
}

