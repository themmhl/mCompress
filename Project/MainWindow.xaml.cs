using System.Windows;
using System.Windows.Input;
namespace mCompress
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FluentWButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            new About().ShowDialog();
        }

        private void BtnView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            new View().ShowDialog();
        }

        private void BtnCompress_MouseUp(object sender, MouseButtonEventArgs e)
        {
            new Compress().ShowDialog();
        }

        private void BtnExtract_MouseUp(object sender, MouseButtonEventArgs e)
        {
            new Extract().ShowDialog();
        }


        private void FluentRetangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
            Close();
        }

        private void FluentRetangle_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void FluentRetangle_MouseUp_2(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void btnRepair_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
