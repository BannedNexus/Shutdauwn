using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Shutdauwn
{
    public static class VlcMonitor
    {
        private enum VlcStatus { NotFound, Idle, MediaStopped, MediaStarted, MediaPlaying }

        private static VlcStatus vlcStatus = VlcStatus.MediaStopped;
        private static bool isVideoPlaying = false;
        private static Process vlcProcess;

        public static bool MonitorStarted { get; private set; }

        public static void StartMonitoring(TextBlock statusTextBlock)
        {
            VlcMonitor.MonitorStarted = true;

            // Start a thread that will handle the monitoring
            new Task(() => VlcMonitor.monitorVlc(statusTextBlock)).Start();
        }

        public static void StopMonitoring()
        {
            VlcMonitor.MonitorStarted = false;
        }

        private static Process getVlcProcess()
        {
            Process[] processList = Process.GetProcessesByName("vlc");
            return processList.Length > 0 ? processList[0] : null;
        }

        private static void monitorVlc(TextBlock statusTextBlock)
        {
            isVideoPlaying = false;

            while(VlcMonitor.MonitorStarted)
            {
                VlcMonitor.vlcProcess = VlcMonitor.getVlcProcess();
                
                if (VlcMonitor.vlcProcess == null)
                {
                    VlcMonitor.setStatus(statusTextBlock, VlcStatus.NotFound);
                    Thread.Sleep(500);
                    continue;
                }

                VlcMonitor.finiteAutomaton(statusTextBlock);

                Thread.Sleep(500);
            }

            // Clean up
            VlcMonitor.vlcProcess = null;
            VlcMonitor.isVideoPlaying = false;
            VlcMonitor.vlcStatus = VlcStatus.MediaStopped;
            VlcMonitor.setStatus(statusTextBlock, "");
        }

        private static void finiteAutomaton(TextBlock statusTextBlock)
        {
            string windowTitlePrefix = VlcMonitor.vlcProcess.MainWindowTitle.Length > 2 ? VlcMonitor.vlcProcess.MainWindowTitle.Substring(0, 3) : VlcMonitor.vlcProcess.MainWindowTitle;

            if(VlcMonitor.vlcProcess.HasExited)
            {
                VlcMonitor.vlcProcess = null;
            }
            if ((windowTitlePrefix == "VLC" || windowTitlePrefix == "") && VlcMonitor.isVideoPlaying)
            {
                // Shutdown
                VlcMonitor.setStatus(statusTextBlock, VlcStatus.MediaStopped);
#if DEBUG
                // do nothing
#else
                MainWindow.Shutdown();
#endif
            }
            else if ((windowTitlePrefix == "VLC" || windowTitlePrefix == "") && !VlcMonitor.isVideoPlaying)
            {
                VlcMonitor.setStatus(statusTextBlock, VlcStatus.Idle);
            }
            else if (!VlcMonitor.isVideoPlaying)
            {
                VlcMonitor.isVideoPlaying = true;
                VlcMonitor.setStatus(statusTextBlock, VlcStatus.MediaStarted);
            }
            else if (VlcMonitor.isVideoPlaying)
            {
                VlcMonitor.setStatus(statusTextBlock, VlcStatus.MediaPlaying);
            }
        }

        private static void setStatus(TextBlock statusTextBlock, VlcStatus vlcStatus)
        {
            if (vlcStatus == VlcMonitor.vlcStatus)
            {
                VlcMonitor.vlcStatus = vlcStatus;
                return;
            }

            VlcMonitor.vlcStatus = vlcStatus;

            string status;
            switch (vlcStatus)
            {
                case VlcStatus.MediaStopped:
                    status = "Media stopped, shutting down";
                    break;
                case VlcStatus.Idle:
                    status = "VLC is idle";
                    break;
                case VlcStatus.MediaStarted:
                    status = "Media started";
                    break;
                case VlcStatus.MediaPlaying:
                    status = "Media is playing";
                    break;
                case VlcStatus.NotFound:
                    status = "VLC process not found";
                    break;
                default:
                    status = "Unknown VLC event";
                    break;
            }

            VlcMonitor.setStatus(statusTextBlock, status);
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
