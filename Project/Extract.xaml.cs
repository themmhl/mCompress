using System.Windows;
using System.Windows.Input;

namespace mCompress
{
    /// <summary>
    /// Interaction logic for Extract.xaml
    /// </summary>
    public partial class Extract : Window
    {
        public Extract()
        {
            InitializeComponent();
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
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompress.exe", "") + @"sevenzip\";
            if (passcode.Password.Length > 0)
            {
                if (CompressionAssistant.Extract(txtfrom.Text, txtto.Text, ziploc, passcode.Password))
                {
                    System.Windows.Forms.MessageBox.Show("Everything is Ok ;-)");
                }
               
            }
            else
            {
                if (CompressionAssistant.Extract(txtfrom.Text, txtto.Text, ziploc))
                {
                    System.Windows.Forms.MessageBox.Show("Somthing went wrong, Did you forgot a little thing?");
                }
            }
            
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
