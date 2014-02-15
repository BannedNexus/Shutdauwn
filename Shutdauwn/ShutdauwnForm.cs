using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shutdauwn
{
    public partial class ShutdauwnForm : Form
    {
        private static ShutdauwnForm instance;

        public ShutdauwnForm()
        {
            ShutdauwnForm.instance = this;
            InitializeComponent();

            this.minutesUpDown.Value = Properties.Settings.Default.minutesUpDown;
            this.hoursUpDown.Value = Properties.Settings.Default.hoursUpDown;
            this.minimizeCheckBox.Checked = Properties.Settings.Default.minimizeToTray;
        }

        public static void Shutdown()
        {
#if DEBUG
            // do nothing
#else
            ShutdauwnForm.instance.saveSettings();

            ProcessStartInfo processStartInfo = new ProcessStartInfo("shutdown", "/s /f /t 0");
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(processStartInfo);

            Environment.Exit(0); // Microsoft does not end child processes when main process is terminated
#endif
        }

        private void monitorVlcButton_Click(object sender, EventArgs e)
        {
            if (VlcMonitor.MonitorStarted)
            {
                VlcMonitor.StopMonitoring();
                this.monitorVlcButton.Text = "Start monitoring VLC";
                this.vlcStatusLabel.Text = "";
            }
            else
            {
                VlcMonitor.StartMonitoring(this.vlcStatusLabel);
                this.monitorVlcButton.Text = "Stop monitoring VLC";
            }
        }

        private void timerButton_Click(object sender, EventArgs e)
        {
            if (ShutDownTimer.TimerStarted)
            {
                ShutDownTimer.StopTimer();
                this.timerButton.Text = "Start shutdown timer";
                this.timerStatusLabel.Text = "";
            }
            else if (minutesUpDown.Value > 0 || hoursUpDown.Value > 0)
            {
                ShutDownTimer.StartTimer(this.timerStatusLabel, (int)this.minutesUpDown.Value, (int)this.hoursUpDown.Value);
                this.timerButton.Text = "Stop shutdown timer";
            }
        }

        private void ShutdauwnForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.saveSettings();
        }

        private void saveSettings()
        {
            Properties.Settings.Default.minutesUpDown = (int)this.minutesUpDown.Value;
            Properties.Settings.Default.hoursUpDown = (int)this.hoursUpDown.Value;
            Properties.Settings.Default.minimizeToTray = this.minimizeCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void ShutdauwnForm_Resize(object sender, EventArgs e)
        {
            if (this.minimizeCheckBox.Checked)
            {
                if (FormWindowState.Minimized == this.WindowState)
                {
                    this.notifyIcon.Visible = true;
                    this.Hide();
                }

                else if (FormWindowState.Normal == this.WindowState)
                {
                    this.notifyIcon.Visible = false;
                }
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.notifyIcon.Visible = false;
        }
    }
}
