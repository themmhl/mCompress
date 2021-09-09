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
using System.Windows.Shapes;

namespace mCompress
{
    /// <summary>
    /// Interaction logic for QuickCompression.xaml
    /// </summary>
    public partial class QuickCompression : Window
    {
        public string from;
        public QuickCompression()
        {
            InitializeComponent();
            txtfrom.Content = from;
        }
        private void FluentWButton_Loaded(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FluentWButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //source
            System.Windows.Forms.FolderBrowserDialog source = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Where to compress from? (only a folder)"
            };
            source.ShowDialog();
            txtfrom.Text = source.SelectedPath;
        }

        private void FluentWButton_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            //destination
            System.Windows.Forms.FolderBrowserDialog destination = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Where to Save Archive to? (only a folder)"
            };
            destination.ShowDialog();
            txtto.Text = destination.SelectedPath;
        }

        private void FluentWButton_Loaded_1(object sender, RoutedEventArgs e)
        {
            string format = ".7z";
            CompressionFormat compressionFormat = CompressionFormat.sevenzip;
            if (zip.IsChecked == true)
            {
                compressionFormat = CompressionFormat.zip;
                format = ".zip";
            }
            else if (sevenzip.IsChecked == true)
            {
                compressionFormat = CompressionFormat.sevenzip;
                format = ".7z";
            }
            else if (wim.IsChecked == true)
            {
                compressionFormat = CompressionFormat.wim;
                format = ".wim";
            }
            else if (tar.IsChecked == true)
            {
                compressionFormat = CompressionFormat.tar;
                format = ".tar";
            }


            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompress.exe", "") + @"sevenzip\";
            if (justpack.IsChecked == true)
            {
                if (CompressionAssistant.Compress(txtfrom.Text, txtto.Text, ziploc, txtto_Copy.Text + format, CompressionRatio.JustPack, compressionFormat))
                {
                    label.Content = "Status: Complated Compressing and saved in \n" + txtto.Text;
                }
                else
                {
                    label.Content = "Status: Somthing went wrong";
                }
            }
            else if (normal.IsChecked == true)
            {
                if (CompressionAssistant.Compress(txtfrom.Text, txtto.Text, ziploc, txtto_Copy.Text + format, CompressionRatio.Normal, compressionFormat))
                {
                    label.Content = "Status: Complated Compressing and saved in \n" + txtto.Text;
                }
                else
                {
                    label.Content = "Status: Somthing went wrong";
                }
            }
            else if (ultra.IsChecked == true)
            {
                if (CompressionAssistant.Compress(txtfrom.Text, txtto.Text, ziploc, txtto_Copy.Text + format, CompressionRatio.Ultra, compressionFormat))
                {
                    label.Content = "Status: Complated Compressing and saved in \n" + txtto.Text;
                }
                else
                {
                    label.Content = "Status: Somthing went wrong";
                }
            }
        }

        private void Zip_Copy_Checked(object sender, RoutedEventArgs e)
        {

        }



   

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
