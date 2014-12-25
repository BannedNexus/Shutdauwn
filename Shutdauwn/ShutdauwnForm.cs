using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
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
            this.philipsHueIpTextBox.Text = Properties.Settings.Default.philipsHueIp;
            this.philipsHueUsernameTextBox.Text = Properties.Settings.Default.philipsHueUsername;
            this.turnOffCheckBox.Checked = Properties.Settings.Default.turnOffLightsOnShutdown;
        }

        public async void Shutdown()
        {
            if (this.turnOffCheckBox.Checked)
            {
                try
                {
                    await this.turnOffPhilipsHue();
                }
                catch { /* ignore */ }
            }
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

        private Task<HttpResponseMessage> turnOffPhilipsHue()
        {
            string path = String.Format("http://{0}/api/{1}/groups/0/action", this.philipsHueIpTextBox.Text, this.philipsHueUsernameTextBox.Text);

            string turnOffCommand = "";
            turnOffCommand += "{";
            turnOffCommand += "\"on\":false,";
            turnOffCommand += "\"transitiontime\":50";
            turnOffCommand += "}";

            HttpClient httpClient = new HttpClient();
            return httpClient.PutAsync(new Uri(path), new StringContent(turnOffCommand, Encoding.UTF8, "application/json"));
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
                VlcMonitor.StartMonitoring(this.vlcStatusLabel, this);
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
                ShutDownTimer.StartTimer(this.timerStatusLabel, this, (int)this.minutesUpDown.Value, (int)this.hoursUpDown.Value);
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
            Properties.Settings.Default.philipsHueIp = this.philipsHueIpTextBox.Text;
            Properties.Settings.Default.philipsHueUsername = this.philipsHueUsernameTextBox.Text;
            Properties.Settings.Default.turnOffLightsOnShutdown = this.turnOffCheckBox.Checked;
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
