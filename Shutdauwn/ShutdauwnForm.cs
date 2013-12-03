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
        public ShutdauwnForm()
        {
            InitializeComponent();

            this.minutesUpDown.Value = Properties.Settings.Default.minutesUpDown;
            this.hoursUpDown.Value = Properties.Settings.Default.hoursUpDown;
        }

        public static void Shutdown()
        {
            // The argument /s is to shut down the computer
            // The argument /t 0 is to tell the process that the specified operation needs to be completed after 0 seconds
#if DEBUG
            // do nothing
#else
            Process.Start("shutdown", "/s /t 0");
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
            Properties.Settings.Default.minutesUpDown = (int)this.minutesUpDown.Value;
            Properties.Settings.Default.hoursUpDown = (int)this.hoursUpDown.Value;
            Properties.Settings.Default.Save();
        }
    }
}
