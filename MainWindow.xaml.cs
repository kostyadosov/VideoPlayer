using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace VideoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        TimeSpan totalTime;
        bool IsPaused;
        public MainWindow()
        {
           
            InitializeComponent();
          
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Wmv files (*.wmv)|*.wmv|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                mediaPl.Source = new Uri(selectedFileName);
                mediaPl.LoadedBehavior = MediaState.Manual;
                mediaPl.UnloadedBehavior = MediaState.Manual;
                mediaPl.Play();
                IsPaused = false;
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (IsPaused)
            {
                mediaPl.Play();
                IsPaused = false;
            }
            else
            {
                mediaPl.Pause();
                IsPaused = true;
            }
        }

        private void mediaPl_MediaOpened(object sender, RoutedEventArgs e)
        {
            totalTime = mediaPl.NaturalDuration.TimeSpan;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if(mediaPl.NaturalDuration.TimeSpan.TotalSeconds>0)
            {
                if(totalTime.TotalSeconds>0)
                {
                    sldTime.Value = mediaPl.Position.TotalSeconds / totalTime.TotalSeconds;
                }
            }
        }

        private void sldVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPl.Volume = sldVolume.Value;
        }

        private void sldTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPl.Position = TimeSpan.FromSeconds(sldTime.Value * totalTime.TotalSeconds);
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Wmv files (*.wmv)|*.wmv|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                mediaPl.Source = new Uri(selectedFileName);
                mediaPl.LoadedBehavior = MediaState.Manual;
                mediaPl.UnloadedBehavior = MediaState.Manual;
                mediaPl.Play();
                IsPaused = false;
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaPl.Stop();
            IsPaused = true;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is Video Player");

        }
    }
}
