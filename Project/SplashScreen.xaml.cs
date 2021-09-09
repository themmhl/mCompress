using System;
using System.Windows;
using System.Windows.Threading;

namespace mCompress
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        private readonly DispatcherTimer timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(2500)
        };

        public SplashScreen()
        {
            InitializeComponent();
            timer.Tick += Timer_Tick;

            //only disabled becuse i'm testing and don't want to waste my time
            //timer.IsEnabled = true;
            new MainWindow().Show();
            Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.IsEnabled = false;
            new MainWindow().Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.IsEnabled = true;
        }
    }
}
