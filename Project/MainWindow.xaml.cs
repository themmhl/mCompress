using mCompressdotNET6;

using System.Diagnostics;
using System.IO;
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
            if (!Directory.Exists("C:\\Users\\Public\\###MMHLco\\"))
            {
                Directory.CreateDirectory("C:\\Users\\Public\\###MMHLco\\");
            }
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";
            if (!File.Exists(ziploc + "no.ans"))
            {
                File.Create(ziploc + "no.ans");
            }
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
            Directory.Delete("C:\\Users\\Public\\###MMHLco\\", true);
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";
            File.Delete(Path.Combine(ziploc, "list.dlll"));
            if (File.Exists(ziploc + "no.ans"))
            {
                File.Delete(ziploc + "no.ans");
            }

            Process procKillApp = new();
            procKillApp.StartInfo.CreateNoWindow = true;
            procKillApp.StartInfo.FileName = "cmd.exe";
            procKillApp.StartInfo.Arguments = " /c taskkill /IM mCompressDotNET6.exe /f";
            procKillApp.Start();
            procKillApp.WaitForExit();
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
            new MakeSFX().ShowDialog();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            FluentRetangle_MouseUp(null, null);
        }
    }
}
