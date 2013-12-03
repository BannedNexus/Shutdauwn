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

        private VlcStatus vlcStatus = VlcStatus.MediaStopped;
        private bool isVideoPlaying = false;
        private static Process vlcProcess;

        public static bool MonitorStarted { get { return VlcMonitor.instance == null ? false : VlcMonitor.instance.monitorRunning; } }

        private bool monitorRunning;

        private Label statusLabel;

        private VlcMonitor(Label statusLabel)
        {
            this.monitorRunning = true;
            this.statusLabel = statusLabel;
            this.vlcStatus = VlcStatus.MediaStopped;
            VlcMonitor.instance = this;

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
            this.isVideoPlaying = false;

            while(this.monitorRunning)
            {
                VlcMonitor.vlcProcess = VlcMonitor.getVlcProcess();
                
                if (VlcMonitor.vlcProcess == null)
                {
                    this.setStatus(VlcStatus.NotFound);
                    Thread.Sleep(500);
                    continue;
                }

                this.finiteAutomaton();

                Thread.Sleep(500);
            }

            VlcMonitor.setStatus(statusLabel, "");
        }

        private void finiteAutomaton()
        {
            string windowTitlePrefix = VlcMonitor.vlcProcess.MainWindowTitle.Length > 2 ? VlcMonitor.vlcProcess.MainWindowTitle.Substring(0, 3) : VlcMonitor.vlcProcess.MainWindowTitle;

            if(VlcMonitor.vlcProcess.HasExited)
            {
                VlcMonitor.vlcProcess = null;
            }
            if ((windowTitlePrefix == "VLC" || windowTitlePrefix == "") && this.isVideoPlaying)
            {
                this.setStatus(VlcStatus.MediaStopped);
                ShutdauwnForm.Shutdown();
            }
            else if ((windowTitlePrefix == "VLC" || windowTitlePrefix == "") && !this.isVideoPlaying)
            {
                this.setStatus(VlcStatus.Idle);
            }
            else if (!this.isVideoPlaying)
            {
                this.isVideoPlaying = true;
                this.setStatus(VlcStatus.MediaStarted);
            }
            else if (this.isVideoPlaying)
            {
                this.setStatus(VlcStatus.MediaPlaying);
            }
        }

        private void setStatus(VlcStatus newVlcStatus)
        {
            if (newVlcStatus == this.vlcStatus)
            {
                this.vlcStatus = newVlcStatus;
                return;
            }

            this.vlcStatus = newVlcStatus;

            string status;
            switch (newVlcStatus)
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

            VlcMonitor.setStatus(this.statusLabel, status);
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
