using MMHLco.FluentControls;

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

namespace mCompressdotNET6
{
    /// <summary>
    /// Interaction logic for EnterPassword.xaml
    /// </summary>
    public partial class EnterPassword : Window
    {
        public EnterPassword()
        {
            InitializeComponent();
        }

        private string pass;

        public string EnteredPassword
        {
            get
            {
                pass = passbox.Password;
                return pass;
            }
            set
            {
                pass = value;
            }
        }


        private void OKClicked(object sender, MouseButtonEventArgs e)
        {
            EnteredPassword = passbox.Password;
            Close();
        }

        private void Canceled(object sender, MouseButtonEventArgs e)
        {
            EnteredPassword = null;
            new FluentMessageBox()
            {
                Title = "No matter",
                Content = "we can't open it without password",
                ButtonType = FluentMessageBox.MessageBoxButtonType.OK
            }.ShowMSGDialog();
            Close();
        }
    }
}
