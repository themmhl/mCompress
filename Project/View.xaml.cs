using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MMHLco;
namespace mCompress
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Window
    {
        public View()
        {
            InitializeComponent();
            listView.FontFamily = new FontFamily("Open Sans");
        }

        private void closerec_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void openBTN_MouseUp(object sender, MouseButtonEventArgs e)
        {/*
          *May Work in another updates ;-)
            var openDLG = new System.Windows.Forms.OpenFileDialog()
            {
                Title="Open a Compressed file",
                Multiselect=false,
                CheckFileExists = true,
                CheckPathExists = true
            };
            if (openDLG.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                listView.ItemsSource = FileContentsAssistant.GetFileNamesList(openDLG.FileName);
            }
            
          */
        }

        private void newBTN_MouseUp(object sender, MouseButtonEventArgs e)
        {
            new Compress().ShowDialog();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
