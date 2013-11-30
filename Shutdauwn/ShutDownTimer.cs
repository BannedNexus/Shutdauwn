using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;

namespace Shutdauwn
{
    public static class ShutDownTimer
    {
        public static bool TimerStarted { get; private set; }
        private static DateTime shutdownDateTime;


        public static void StartTimer(TextBlock statusTextBlock, int minutes, int hours)
        {
            TimeSpan duration = new TimeSpan(hours, minutes, 0);
            ShutDownTimer.shutdownDateTime = DateTime.Now + duration;
            ShutDownTimer.TimerStarted = true;

            new Task(() => ShutDownTimer.timer(statusTextBlock)).Start();
        }

        public static void StopTimer()
        {
            ShutDownTimer.TimerStarted = false;
            
        }

        private static void timer(TextBlock statusTextBlock)
        {
            while(ShutDownTimer.TimerStarted)
            {
                if (DateTime.Now >= ShutDownTimer.shutdownDateTime)
                {
#if DEBUG
                    // do nothing
#else
                    ShutDownTimer.setStatus(statusTextBlock, "Shutting down");
                    MainWindow.Shutdown();
#endif
                    return;
                }
                ShutDownTimer.setStatus(statusTextBlock, ShutDownTimer.getTimeLeftString());
                Thread.Sleep(100);
            }
            ShutDownTimer.setStatus(statusTextBlock, "");
        }

        private static string getTimeLeftString()
        {
            TimeSpan timeLeft = ShutDownTimer.shutdownDateTime - DateTime.Now;

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

        private static void setStatus(TextBlock statusTextBlock, string status)
        {
            statusTextBlock.Dispatcher.BeginInvoke(new Action(delegate()
            {
                statusTextBlock.Text = status;
            }));
        }
    }
}
