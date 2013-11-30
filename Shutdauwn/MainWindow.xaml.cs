using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shutdauwn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // dll ind i exe!
            // http://www.digitallycreated.net/Blog/61/combining-multiple-assemblies-into-a-single-exe-for-a-wpf-application

            this.minutesUpDown.Value = Properties.Settings.Default.minutesUpDown;
            this.hoursUpDown.Value = Properties.Settings.Default.hoursUpDown;
        }

        public static void Shutdown()
        {
            // The argument /s is to shut down the computer
            // The argument /t 0 is to tell the process that the specified operation needs to be completed after 0 seconds
            Process.Start("shutdown", "/s /t 0");
        }

        private void monitorVlcButton_Click(object sender, RoutedEventArgs e)
        {
            if (VlcMonitor.MonitorStarted)
            {
                VlcMonitor.StopMonitoring();
                this.monitorVlcButton.Content = "Start monitoring VLC";
                this.vlcStatusTextBlock.Text = "";
            }
            else
            {
                VlcMonitor.StartMonitoring(this.vlcStatusTextBlock);
                this.monitorVlcButton.Content = "Stop monitoring VLC";
            }
        }

        private void timerButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShutDownTimer.TimerStarted)
            {
                ShutDownTimer.StopTimer();
                this.timerButton.Content = "Start shutdown timer";
                this.timerStatusTextBlock.Text = "";
            }
            else
            {
                ShutDownTimer.StartTimer(this.timerStatusTextBlock, (int)this.minutesUpDown.Value, (int)this.hoursUpDown.Value);
                this.timerButton.Content = "Stop shutdown timer";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.minutesUpDown = (int)this.minutesUpDown.Value;
            Properties.Settings.Default.hoursUpDown = (int)this.hoursUpDown.Value;
            Properties.Settings.Default.Save();
        }
    }
}
