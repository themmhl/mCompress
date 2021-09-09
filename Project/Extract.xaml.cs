using mCompressdotNET6;

using MMHLco;
using MMHLco.FluentControls;

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
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";
            if (FileContentsAssistant.IsPasswordProtected(txtfrom.Text))
            {
                EnterPassword m = new();
                m.ShowDialog();
                if (m.EnteredPassword != null)
                {
                    if (CompressionAssistant.Extract(txtfrom.Text, txtto.Text, ziploc, m.EnteredPassword))
                    {
                        new FluentMessageBox()
                        {
                            Title = "OK",
                            Content = "Extracted.",
                            ButtonType = FluentMessageBox.MessageBoxButtonType.OK
                        }.ShowMSGDialog();
                    }
                    else
                    {
                        new FluentMessageBox()
                        {
                            Title = "Something Happened",
                            Content = "Something went wrong.",
                            ButtonType = FluentMessageBox.MessageBoxButtonType.OK
                        }.ShowMSGDialog();
                    }
                }
            }
            else
            {
                if (CompressionAssistant.Extract(txtfrom.Text, txtto.Text, ziploc))
                {
                    new FluentMessageBox()
                    {
                        Title = "OK",
                        Content = "Extracted.",
                        ButtonType = FluentMessageBox.MessageBoxButtonType.OK
                    }.ShowMSGDialog();
                }
                else
                {
                    new FluentMessageBox()
                    {
                        Title = "Something Happened",
                        Content = "Something went wrong.",
                        ButtonType = FluentMessageBox.MessageBoxButtonType.OK
                    }.ShowMSGDialog();
                }
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
