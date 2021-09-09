using System.Windows;
using System.Windows.Input;

namespace mCompress
{
    /// <summary>
    /// Interaction logic for CompressOneFile.xaml
    /// </summary>
    public partial class CompressOneFile : Window
    {
        public string format = ".7z";

        public CompressOneFile()
        {
            InitializeComponent();
        }

        private void FluentWButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //execute

            OneFileCompressionFormat compressionFormat = OneFileCompressionFormat.xz;
            if (RDXZ.IsChecked == true)
            {
                compressionFormat = OneFileCompressionFormat.xz;
                format = ".xz";
            }
            else if (RDBZIP2.IsChecked == true)
            {
                compressionFormat = OneFileCompressionFormat.bzip2;
                format = ".bz2";
            }
            else if (RDGZIP.IsChecked == true)
            {
                compressionFormat = OneFileCompressionFormat.gzip;
                format = ".gz";
            }

            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";
            if (justpack.IsChecked == true)
            {
                if (CompressionAssistant.CompressOneFile(new System.IO.FileInfo(txtfrom.Text), new System.IO.FileInfo(txtto.Text), ziploc, CompressionRatio.JustPack, compressionFormat))
                {
                    MessageBox.Show("Complated Compressing and saved in \n" + txtto.Text);
                }
                else
                {
                    MessageBox.Show("Somthing went wrong");
                }
            }
            else if (normal.IsChecked == true)
            {
                if (CompressionAssistant.CompressOneFile(new System.IO.FileInfo(txtfrom.Text), new System.IO.FileInfo(txtto.Text), ziploc, CompressionRatio.Normal, compressionFormat))
                {
                    MessageBox.Show("Complated Compressing and saved in \n" + txtto.Text);
                }
                else
                {
                    MessageBox.Show("Somthing went wrong");
                }
            }
            else if (ultra.IsChecked == true)
            {
                if (CompressionAssistant.CompressOneFile(new System.IO.FileInfo(txtfrom.Text), new System.IO.FileInfo(txtto.Text), ziploc, CompressionRatio.Ultra, compressionFormat))
                {
                    MessageBox.Show("Complated Compressing and saved in \n" + txtto.Text);
                }
                else
                {
                    MessageBox.Show("Somthing went wrong");
                }
            }

        }

        private void FluentWButton_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            //source
            System.Windows.Forms.OpenFileDialog source = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Where to compress from? (only a file)"
            };
            source.ShowDialog();
            txtfrom.Text = source.FileName;
        }

        private void FluentWButton_MouseUp_2(object sender, MouseButtonEventArgs e)
        {
            //destination
            System.Windows.Forms.SaveFileDialog destination = new System.Windows.Forms.SaveFileDialog
            {
                Title = "Where to Save Archive to? (only a folder)"
            };
            destination.ShowDialog();
            txtto.Text = destination.FileName;
        }

        private void FluentWButton_MouseUp_3(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
