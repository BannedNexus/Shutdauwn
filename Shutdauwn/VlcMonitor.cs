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

        private static string[] allowedVlcTitles = {
                                                       "VLC (Direct3D output)", // happens when video isn't emedded in the main window
                                                       "vlc" // happens when user clicks the top menu items
                                                   };
        private VlcStatus vlcStatus = VlcStatus.MediaStopped;
        private bool isVideoPlaying = false;
        private Process[] vlcProcesses;

        public static bool MonitorStarted { get { return VlcMonitor.instance == null ? false : VlcMonitor.instance.monitorRunning; } }

        private bool monitorRunning;
        private Label statusLabel;

        private ShutdauwnForm shutdawunForm;

        private VlcMonitor(Label statusLabel, ShutdauwnForm shutdawunForm)
        {
            this.monitorRunning = true;
            this.statusLabel = statusLabel;
            this.vlcStatus = VlcStatus.MediaStopped;
            VlcMonitor.instance = this;
            this.shutdawunForm = shutdawunForm;

            this.monitorVlc(statusLabel);
        }


        public static void StartMonitoring(Label statusLabel, ShutdauwnForm shutdauwnForm)
        {
            // Start a thread that will handle the monitoring
            new Task(() => new VlcMonitor(statusLabel, shutdauwnForm)).Start();
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
                this.vlcProcesses = VlcMonitor.getVlcProcesses();

                if (this.vlcProcesses == null)
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
                this.vlcProcesses = null;
            }
            else if(this.isVideoPlaying)
            {
                if (!isVlcStalling)
                {
                    if (IsVlcIdle)
                    {
                        this.setStatus(VlcStatus.MediaStopped);
                        this.shutdawunForm.Shutdown();
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
        }

        /// <summary>
        /// Determines whether all VLC processes has exited
        /// </summary>
        private bool hasProcessesExited()
        {
            foreach (Process process in this.vlcProcesses)
                if(!process.HasExited)
                    return false;
            return true;
        }

        /// <summary>
        /// Determines whether all VLC processes are idle
        /// </summary>
        private bool IsVlcIdle
        {
            get
            {
                foreach (Process process in this.vlcProcesses)
                    if(this.isTitleActive(process.MainWindowTitle))
                        return false;
                return true;
            }
        }

        /// <summary>
        /// Determines whether the specified title is an active title
        /// </summary>
        /// <param name="title"></param>
        private bool isTitleActive(string title)
        {
            // Allowed titles contains some static titles that are allowed
            foreach (string allowedTitle in VlcMonitor.allowedVlcTitles)
                if(title == allowedTitle)
                    return true;
            return (title.Length > 2 ? title.Substring(0, 3) : title) != "VLC" && title.Contains(" - VLC");
        }

        /// <summary>
        /// Determines whether all VLC processes are responding
        /// </summary>
        private bool IsVlcStalling
        {
            get
            {
                foreach (Process process in this.vlcProcesses)
                {
                    string windowsTitlePrefix = process.MainWindowTitle.Length > 2 ? process.MainWindowTitle.Substring(0, 3) : process.MainWindowTitle;
                    if (process.Responding && windowsTitlePrefix != "") // sometime VLC title is an empty string. At least happens when VLC is closed
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Set the current state of the VLC monitoring, and change the visual status to an appropiate decsription
        /// </summary>
        /// <param name="newVlcStatus"></param>
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
