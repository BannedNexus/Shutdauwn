using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shutdauwn
{
    public class VlcMonitor
    {
        // An instance almost only consists of a bool determining whether the instance is running
        private static VlcMonitor instance;

        private enum VlcStatus { NotFound, Idle, MediaStopped, MediaStarted, MediaPlaying }

        private static VlcStatus vlcStatus = VlcStatus.MediaStopped;
        private static bool isVideoPlaying = false;
        private static Process vlcProcess;

        public static bool MonitorStarted { get { return VlcMonitor.instance == null ? false : VlcMonitor.instance.monitorRunning; } }

        private bool monitorRunning;

        private Label statusLabel;

        private VlcMonitor(Label statusLabel)
        {
            this.monitorRunning = true;
            this.statusLabel = statusLabel;
            VlcMonitor.instance = this;
            VlcMonitor.vlcStatus = VlcStatus.MediaStopped;

            this.monitorVlc(statusLabel);
        }

        public static void StartMonitoring(Label statusLabel)
        {
            // Start a thread that will handle the monitoring
            new Task(() => new VlcMonitor(statusLabel)).Start();
        }

        public static void StopMonitoring()
        {
            VlcMonitor.instance.monitorRunning = false;
        }

        private static Process getVlcProcess()
        {
            Process[] processList = Process.GetProcessesByName("vlc");
            return processList.Length > 0 ? processList[0] : null;
        }

        private void monitorVlc(Label statusLabel)
        {
            VlcMonitor.isVideoPlaying = false;

            while(this.monitorRunning)
            {
                VlcMonitor.vlcProcess = VlcMonitor.getVlcProcess();
                
                if (VlcMonitor.vlcProcess == null)
                {
                    VlcMonitor.setStatus(statusLabel, VlcStatus.NotFound);
                    Thread.Sleep(500);
                    continue;
                }

                VlcMonitor.finiteAutomaton(statusLabel);

                Thread.Sleep(500);
            }

            VlcMonitor.setStatus(statusLabel, "");
        }

        private static void finiteAutomaton(Label statusLabel)
        {
            string windowTitlePrefix = VlcMonitor.vlcProcess.MainWindowTitle.Length > 2 ? VlcMonitor.vlcProcess.MainWindowTitle.Substring(0, 3) : VlcMonitor.vlcProcess.MainWindowTitle;

            if(VlcMonitor.vlcProcess.HasExited)
            {
                VlcMonitor.vlcProcess = null;
            }
            if ((windowTitlePrefix == "VLC" || windowTitlePrefix == "") && VlcMonitor.isVideoPlaying)
            {
                VlcMonitor.setStatus(statusLabel, VlcStatus.MediaStopped);
#if DEBUG
                // do nothing
#else
                ShutdauwnForm.Shutdown();
#endif
            }
            else if ((windowTitlePrefix == "VLC" || windowTitlePrefix == "") && !VlcMonitor.isVideoPlaying)
            {
                VlcMonitor.setStatus(statusLabel, VlcStatus.Idle);
            }
            else if (!VlcMonitor.isVideoPlaying)
            {
                VlcMonitor.isVideoPlaying = true;
                VlcMonitor.setStatus(statusLabel, VlcStatus.MediaStarted);
            }
            else if (VlcMonitor.isVideoPlaying)
            {
                VlcMonitor.setStatus(statusLabel, VlcStatus.MediaPlaying);
            }
        }

        private static void setStatus(Label statusLabel, VlcStatus vlcStatus)
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

            VlcMonitor.setStatus(statusLabel, status);
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
