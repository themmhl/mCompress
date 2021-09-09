using System;
using System.Windows;
using System.Windows.Input;

namespace mCompress
{
    /// <summary>
    /// Interaction logic for QuickExtraction.xaml
    /// </summary>
    public partial class QuickExtraction : Window
    {
        public string from;
        public QuickExtraction()
        {
            InitializeComponent();
            txtfrom.Text = from;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Close();
        }
        private void FluentWButton_Loaded(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void FluentWButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //source
            System.Windows.Forms.OpenFileDialog source = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Where is the archive to compress from?"
            };
            source.ShowDialog();
            txtfrom.Text = source.FileName;
        }

        private void FluentWButton_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            //destination
            System.Windows.Forms.FolderBrowserDialog destination = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Where to Save Extracted files to? (only a folder)"
            };
            destination.ShowDialog();
            txtto.Text = destination.SelectedPath;
        }

        private void FluentWButton_Loaded_1(object sender, RoutedEventArgs e)
        {
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";
            if (passcode.Password.Length > 0)
            {
                if (CompressionAssistant.Extract(txtfrom.Text, txtto.Text, ziploc, passcode.Password))
                {
                    System.Windows.MessageBox.Show("Everything is Ok ;-)");
                }
            }
            else
            {
                if (CompressionAssistant.Extract(txtfrom.Text, txtto.Text, ziploc))
                {
                    System.Windows.MessageBox.Show("All right!");
                }
                else
                {
                    System.Windows.MessageBox.Show("Somthing went wrong, Did you forgot a little thing?");
                }
            }
        }
    }
}
