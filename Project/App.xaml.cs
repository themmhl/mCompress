using System.IO;
using System.Windows;

namespace mCompress
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string arg0, arg1, arg2, arg3;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                arg0 = e.Args[0];
                arg1 = e.Args[1];
                arg2 = e.Args[2];
            }
            catch { }

            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompress.exe", "") + @"sevenzip\";
            SplashScreen sp = new SplashScreen();
            //*************************************************************

            if (arg0 == "-e" || arg0 == "--extract")
            {
                if (CompressionAssistant.ExtractForREG(arg1, arg2, ziploc))
                {
                    System.Windows.Forms.MessageBox.Show("All right!");
                    Shutdown();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("it looks somthing went wrong");
                    Shutdown();
                }
            }
            //*************************************************************
            else if (arg0 == "-cn" || arg0 == "--compressnodialog")
            {
                if (CompressionAssistant.CompressOneFileForREGISTRY(new FileInfo(arg1), new DirectoryInfo(arg2), ziploc))
                {
                    System.Windows.Forms.MessageBox.Show("All right!");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("it looks somthing went wrong at compression");
                }
                Shutdown();
            }
            //*************************************************************
            else if (arg0 == "-c" || arg0 == "--compress")
            {
                QuickExtraction extraction = new QuickExtraction
                {
                    from = arg1
                };
                extraction.Show();
            }
            //*************************************************************
            else if (arg0 == "--help" || arg0 == "-?")
            {
                System.Windows.Forms.MessageBox.Show("mcompress -e 'file location' 'where to extract' \nmcompress -c 'source location' 'where to export comrpessed to (with file name and extension)'");
                Shutdown();
            }
            //*************************************************************
            else
            {
                sp.Show();
            }
        }
    }
}
