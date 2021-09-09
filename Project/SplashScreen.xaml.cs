using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace mCompress
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        int count = 200;
        Timer timer = new Timer()
        {
            Interval = 2500,
        };
      //  Timer timercolor = new Timer()
      //  {
       //     Interval = 1000,
      //  };
        Timer timercolorchange = new Timer()
        {
            Interval = 35
        };
        public SplashScreen()
        {
            InitializeComponent();
       //     timercolor.Tick += Timercolor_Tick;
            timer.Tick += Timer_Tick;
            timercolorchange.Tick += Timercolorchange_Tick;
        }

        private void Timercolorchange_Tick(object sender, EventArgs e)
        {
            count++;
            if (count < 245)
            {

                SolidColorBrush solidColor = new SolidColorBrush();
                var m = Convert.ToByte(count);
                solidColor.Color = Color.FromRgb(m, m, m);
                gridmain.Fill = solidColor;
            }
        }

     

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timercolorchange.Enabled = false;
            new MainWindow().Show();
            this.Close();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Enabled = true;
            timercolorchange.Start();
        }
    }
}
