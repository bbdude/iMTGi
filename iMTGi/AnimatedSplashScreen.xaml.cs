using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace iMTGi
{
    /// <summary>
    /// Interaction logic for AnimatedSplashScreen.xaml
    /// </summary>
    public partial class AnimatedSplashScreen : Window
    {
        //Image
        MtgParse iMtg = new MtgParse();
        BackgroundWorker bw = new BackgroundWorker();
        bool loadImages = false;
        bool running = false;
        public AnimatedSplashScreen()
        {
            InitializeComponent();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //when window is activated check if user has already loaded all cards
            //then check if the user wants to load images if they dont have cards loaded
            StreamReader reader = File.OpenText("config.txt");
            string line = null;
            bool firstTime = true;
            while ((line = reader.ReadLine()) != null)
            {
                if (line == "loadedcards false")
                {
                    firstTime = true;
                }
                else if (line == "loadedcards true")
                {
                    firstTime = false;
                }
            }
            if (!running)
            {
                MessageBoxResult result = MessageBoxResult.No;
                if (firstTime)
                    result = MessageBox.Show("This is your first time loading?\nWould you like to download images?\nThis will take a long time.", "Initial Loading...", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    loadImages = true;
                }
                else
                {
                    loadImages = false;
                }
                running = true;
                bw.RunWorkerAsync();
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if ((worker.CancellationPending == true))
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                iMtg.parse(worker,loadImages);
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.pbStatus.Value = e.ProgressPercentage;
            int percentage = (int)((e.ProgressPercentage/ (float)this.pbStatus.Maximum) * 100);
            //percentage = percentage * 100;
            this.pbText.Text = e.ProgressPercentage.ToString();
            this.pbTextP.Text = percentage.ToString() + "%";
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                //this.tbProgress.Text = "Canceled!";
            }

            else if (!(e.Error == null))
            {
                //this.tbProgress.Text = ("Error: " + e.Error.Message);
            }

            else
            {
                using (StreamWriter writer = new StreamWriter("config.txt",false))
                {
                    writer.WriteLine("loadedcards true");
                    if (loadImages)
                        writer.WriteLine("loadedimages true");
                    else
                        writer.WriteLine("loadedimages false");
                    writer.Close();
                }
                MainWindow w2 = new MainWindow();
                w2.Activate();
                w2.iMtg = iMtg;
                w2.ShowDialog();
                this.Hide();
                /*MainWindow f2 = new MainWindow(AnimatedSplashScreen);
                f2.ShowDialog();
                form2.Hide();*/
            }
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (bw.WorkerSupportsCancellation == true)
            {
                bw.CancelAsync();
            }
        }

    }
}
