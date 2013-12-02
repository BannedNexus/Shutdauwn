using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Shutdauwn
{
    public class ShutDownTimer
    {
        private static ShutDownTimer instance; // the only instance of the shutdown timer

        public static bool TimerStarted { get { return ShutDownTimer.instance == null ? false : ShutDownTimer.instance.timerRunning; } }

        private bool timerRunning;
        private DateTime shutdownDateTime;
        private Label statusLabel;

        private ShutDownTimer(Label statusLabel, DateTime shutdownDateTime)
        {
            ShutDownTimer.instance = this;

            this.shutdownDateTime = shutdownDateTime;
            this.statusLabel = statusLabel;
            this.timerRunning = true;

            this.timer();
        }

        public static void StartTimer(Label statusLabel, int minutes, int hours)
        {
            TimeSpan duration = new TimeSpan(hours, minutes, 0);

            new Task(() => new ShutDownTimer(statusLabel, DateTime.Now + duration)).Start();
        }

        public static void StopTimer()
        {
            // The instance that is currently running is stopped
            ShutDownTimer.instance.timerRunning = false;
        }

        private void timer()
        {
            while(this.timerRunning)
            {
                if (DateTime.Now >= this.shutdownDateTime)
                {
                    ShutDownTimer.setStatus(this.statusLabel, "Shutting down");
#if DEBUG
                    // do nothing
#else
                    ShutdauwnForm.Shutdown();
#endif
                    return;
                }
                ShutDownTimer.setStatus(this.statusLabel, ShutDownTimer.getTimeLeftString(this.shutdownDateTime));
                Thread.Sleep(100);
            }
            ShutDownTimer.setStatus(this.statusLabel, "");
        }

        private static string getTimeLeftString(DateTime shutdownDateTime)
        {
            TimeSpan timeLeft = shutdownDateTime - DateTime.Now;

            string result;

            if (timeLeft.Hours > 0)
            {
                result = String.Format("Shutdown in {0} hour{1}", timeLeft.Hours, timeLeft.Hours > 1 ? "s" : "");
                if (timeLeft.Minutes > 0)
                {
                    result += String.Format(" and {0} minute{1}", timeLeft.Minutes, timeLeft.Minutes > 1 ? "s" : "");
                }
            }
            else if(timeLeft.Minutes > 0)
            {
                result = String.Format("Shutdown in {0} minute{1}", timeLeft.Minutes, timeLeft.Minutes > 1 ? "s" : "");
                if (timeLeft.Minutes > 0)
                {
                    result += String.Format(" and {0} second{1}", timeLeft.Seconds, timeLeft.Seconds > 1 ? "s" : "");
                }
            }
            else
            {
                result = String.Format("Shutdown in {0} second{1}", timeLeft.Seconds, timeLeft.Seconds != 1 ? "s" : "");
            }
            return result;
        }

        private static void setStatus(Label statusLabel, string status)
        {
            statusLabel.Invoke((MethodInvoker)delegate
            {
                statusLabel.Text = status;
            });
        }
    }
}
