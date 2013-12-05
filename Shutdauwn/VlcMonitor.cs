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
        private static Process[] vlcProcesses;

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

        private static Process[] getVlcProcesses()
        {
            Process[] vlc = Process.GetProcessesByName("vlc");
            return vlc.Length > 0 ? vlc : null;
        }

        private void monitorVlc(Label statusLabel)
        {
            this.isVideoPlaying = false;

            while(this.monitorRunning)
            {
                VlcMonitor.vlcProcesses = VlcMonitor.getVlcProcesses();
                
                if (VlcMonitor.vlcProcesses == null)
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
            bool isVlcIdle = this.IsVlcIdle;
            bool isVlcStalling = this.IsVlcStalling;

            if(this.hasProcessesExited())
            {
                VlcMonitor.vlcProcesses = null;
            }
            else if(this.isVideoPlaying)
            {
                if (!isVlcStalling)
                {
                    if (IsVlcIdle)
                    {
                        this.setStatus(VlcStatus.MediaStopped);
                        ShutdauwnForm.Shutdown();
                    }
                    else // not idle
                    {
                        this.setStatus(VlcStatus.MediaPlaying);
                    }
                } 
                else // stalling
                {
                    // Don't change state when video is playing and VLC is stalling
                }
            }
            else if (!this.isVideoPlaying) // same as writing 'else', but helps readability here
            {
                if (!isVlcStalling)
                {
                    if (IsVlcIdle)
                    {
                        this.setStatus(VlcStatus.Idle);
                    }
                    else // not idle
                    {
                        this.isVideoPlaying = true;
                        this.setStatus(VlcStatus.MediaStarted);
                    }
                }
                else //stalling
                {
                    this.setStatus(VlcStatus.Idle);
                }
            }
            /*else if (this.isVideoPlaying && isVlcIdle && !isVlcStalling)
            {
                this.setStatus(VlcStatus.MediaStopped);
                ShutdauwnForm.Shutdown();
            }
            else if (this.isVideoPlaying && isVlcStalling)
            {
                // do nothing, assume same state
            }
            else if (isVlcStalling && !this.isVideoPlaying)
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
            }*/
        }

        private bool hasProcessesExited()
        {
            foreach (Process process in VlcMonitor.vlcProcesses)
                if(process.HasExited)
                    return true;
            return false;
        }


        private bool IsVlcIdle
        {
            get
            {
                foreach (Process process in VlcMonitor.vlcProcesses)
                    if((process.MainWindowTitle.Length > 2 ? process.MainWindowTitle.Substring(0, 3) : process.MainWindowTitle) != "VLC")
                        return false;
                return true;
            }
        }

        private bool IsVlcStalling
        {
            get
            {
                foreach (Process process in VlcMonitor.vlcProcesses)
                {
                    string windowsTitlePrefix = process.MainWindowTitle.Length > 2 ? process.MainWindowTitle.Substring(0, 3) : process.MainWindowTitle;
                    if (process.Responding || windowsTitlePrefix != "")
                        return false;
                }
                return true;
            }
        }

        private void setStatus(VlcStatus newVlcStatus)
        {
            if (newVlcStatus == this.vlcStatus)
            {
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
