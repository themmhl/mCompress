using mCompress;

using System.IO;
using System.Windows;
using System.Windows.Input;

namespace mCompressdotNET6
{
    /// <summary>
    /// Interaction logic for MakeSFX.xaml
    /// </summary>
    public partial class MakeSFX : Window
    {
        public MakeSFX()
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
                Description = "Where to Save SFX Archive to? (only a folder)"
            };
            destination.ShowDialog();
            txtto.Text = destination.SelectedPath;
        }

        private void FluentWButton_Loaded_1(object sender, RoutedEventArgs e)
        {
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";
            if (justpack.IsChecked == true)
            {
                if (CompressionAssistant.CompressSFX(txtfrom.Text, Path.Combine(txtto.Text, txtto_Copy.Text + ".exe"), ziploc, CompressionRatio.JustPack))
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
                if (CompressionAssistant.CompressSFX(txtfrom.Text, Path.Combine(txtto.Text, txtto_Copy.Text + ".exe"), ziploc, CompressionRatio.Normal))
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
                if (CompressionAssistant.CompressSFX(txtfrom.Text, Path.Combine(txtto.Text, txtto_Copy.Text + ".exe"), ziploc, CompressionRatio.Ultra))
                {
                    label.Content = "Status: Complated Compressing and saved in \n" + txtto.Text;
                }
                else
                {
                    label.Content = "Status: Somthing went wrong";
                }
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
