using mCompressdotNET6;

using MMHLco;
using MMHLco.FluentControls;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace mCompress
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Window
    {
        private readonly List<string> mm;
        private string pathCurrent;
        public View()
        {
            InitializeComponent();
            listView.FontFamily = new FontFamily("Open Sans");
            mm = FileContentsAssistant.GetFileNamesList(@"C:\Users\Martin-Jefferson\Pictures\Screenshots\target archive name.7z");
            listView.ItemsSource = mm;
            pathCurrent = @"C:\Users\Martin-Jefferson\Pictures\Screenshots\target archive name.7z";
        }

        private void closerec_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void openBTN_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openDLG = new System.Windows.Forms.OpenFileDialog()
            {
                Title = "Open a Compressed file",
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true
            };
            if (openDLG.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pathCurrent = openDLG.FileName;
                listView.ItemsSource = null;
                listView.ItemsSource = FileContentsAssistant.GetFileNamesList(openDLG.FileName);
            }
        }

        private void newBTN_MouseUp(object sender, MouseButtonEventArgs e)
        {
            new Compress().ShowDialog();
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void FluentWButton_MouseUp(object sender, MouseButtonEventArgs e)
        {//delete
            if (listView.SelectedIndex == -1)
            {
                new FluentMessageBox()
                {
                    Title = "Error",
                    Content = "Just Select one item to delete",
                    ButtonType = FluentMessageBox.MessageBoxButtonType.OK
                }.ShowMSGDialog();
            }
            else
            {
                int indexOfSelected = listView.SelectedIndex;
                int m = FileContentsAssistant.DeleteFile(pathCurrent, indexOfSelected);
                FluentMessageBox msg = new()
                {
                    ButtonType = FluentMessageBox.MessageBoxButtonType.OK
                };
                if (m == 0)
                {
                    labelStatus.Content = "Complated";
                }
                else
                {
                    msg.Title = "Something Happened";
                    msg.Content = "Something went wrong or\nThe type of you archive isn't supported yet, \nonly 7-zip archives are supported, \nthis application isn't winrar, \nIt's 7-zip";
                    msg.ShowMSGDialog();
                }

                listView.ItemsSource = null;
                listView.ItemsSource = FileContentsAssistant.GetFileNamesList(pathCurrent);
            }
        }

        private void FluentWButton_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            //copy
            if (listView.SelectedIndex == -1)
            {
                new FluentMessageBox()
                {
                    Title = "Error",
                    Content = "Just Select one item to copy",
                    ButtonType = FluentMessageBox.MessageBoxButtonType.OK
                }.ShowMSGDialog();
            }
            else
            {
                if (FileContentsAssistant.IsPasswordProtected(pathCurrent))
                {
                    EnterPassword m1pass = new EnterPassword();
                    m1pass.ShowDialog();
                    int indexOfSelected = listView.SelectedIndex;
                    string m = FileContentsAssistant.CopyFileToTemp(pathCurrent, indexOfSelected, m1pass.EnteredPassword);
                    FluentMessageBox msg = new()
                    {
                        ButtonType = FluentMessageBox.MessageBoxButtonType.OK
                    };

                    if (m != null)
                    {
                        labelStatus.Content = "Your file copied to clipboard, But It's on temp and will deleted after closing this app, you can change it in settings";
                        StringCollection s = new StringCollection
                        {
                           m
                        };
                        Clipboard.SetFileDropList(s);
                    }
                    else
                    {
                        msg.Title = "Something Happened";
                        msg.Content = "Something went wrong or\nThe type of you archive isn't supported yet, \nonly 7-zip archives are supported, \nthis application isn't winrar, \nIt's 7-zip";
                        msg.ShowMSGDialog();
                    }
                }
                else
                {
                    int indexOfSelected = listView.SelectedIndex;
                    string m = FileContentsAssistant.CopyFileToTemp(pathCurrent, indexOfSelected, null);
                    FluentMessageBox msg = new()
                    {
                        ButtonType = FluentMessageBox.MessageBoxButtonType.OK
                    };

                    if (m != null)
                    {
                        labelStatus.Content = "Your file copied to clipboard, But It's on temp and will deleted after closing this app, you can change it in settings";
                        StringCollection s = new StringCollection
                        {
                               m
                        };
                        Clipboard.SetFileDropList(s);
                    }
                    else
                    {
                        msg.Title = "Something Happened";
                        msg.Content = "Something went wrong or\nThe type of you archive isn't supported yet, \nonly 7-zip archives are supported, \nthis application isn't winrar, \nIt's 7-zip";
                        msg.ShowMSGDialog();
                    }
                }
            }
        }

        private void maxrec_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void ExtractClicked(object sender, MouseButtonEventArgs e)
        {
            if (listView.SelectedIndex != -1)
            {
                if (FileContentsAssistant.IsPasswordProtected(pathCurrent))
                {
                    EnterPassword m = new EnterPassword();
                    m.ShowDialog();
                    System.Windows.Forms.FolderBrowserDialog m1 = new System.Windows.Forms.FolderBrowserDialog()
                    {
                        Description = "Where to save?"
                    };
                    if (m.EnteredPassword != null)
                    {
                        string pathofSavingfile = null;
                        if (m1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            pathofSavingfile = m1.SelectedPath;
                        }
                        if (FileContentsAssistant.ExtractFile(pathCurrent, pathofSavingfile, listView.SelectedIndex, m.EnteredPassword))
                        {
                            labelStatus.Content = "Extracted.";
                        }
                    }
                }
                else
                {
                    string pathofSavingfile = null;
                    System.Windows.Forms.FolderBrowserDialog m = new System.Windows.Forms.FolderBrowserDialog()
                    {
                        Description = "Where to save?",
                    };
                    if (m.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        pathofSavingfile = m.SelectedPath;
                    }
                    if (FileContentsAssistant.ExtractFile(pathCurrent, pathofSavingfile, listView.SelectedIndex, null))
                    {
                        labelStatus.Content = "Extracted.";
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
        }

        private void TestClicked(object sender, MouseButtonEventArgs e)
        {
            if (listView.SelectedIndex != -1)
            {

                if (FileContentsAssistant.TestFile(pathCurrent, listView.SelectedIndex))
                {
                    labelStatus.Content = "This file hasen't any errors.";
                }
                else
                {
                    new FluentMessageBox()
                    {
                        Title = "Errors Found",
                        Content = "This file is corrupt and contains errors.",
                        ButtonType = FluentMessageBox.MessageBoxButtonType.OK
                    }.ShowMSGDialog();
                }
            }
        }

        private void TestArchiveClicked(object sender, MouseButtonEventArgs e)
        {
            if (listView.SelectedIndex != -1)
            {
                if (FileContentsAssistant.TestArchive(pathCurrent))
                {
                    labelStatus.Content = "This Archive hasen't any errors.";
                }
                else
                {
                    new FluentMessageBox()
                    {
                        Title = "Errors Found",
                        Content = "This Archive is corrupt and contains errors.",
                        ButtonType = FluentMessageBox.MessageBoxButtonType.OK
                    }.ShowMSGDialog();
                }
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            int indexOfSelected = listView.SelectedIndex;
            if (indexOfSelected != -1)
            {
                if (FileContentsAssistant.IsPasswordProtected(pathCurrent))
                {
                    EnterPassword m = new EnterPassword();
                    m.ShowDialog();
                    if (m.EnteredPassword != null)
                    {
                        string m2 = FileContentsAssistant.CopyFileToTemp(pathCurrent, indexOfSelected, m.EnteredPassword);
                        Process m3 = new Process();
                        m3.StartInfo.UseShellExecute = true;
                        m3.StartInfo.FileName = m2;
                        m3.Start();
                    }
                }
                else
                {
                    string m2 = FileContentsAssistant.CopyFileToTemp(pathCurrent, indexOfSelected, null);
                    Process m = new Process();
                    m.StartInfo.UseShellExecute = true;
                    m.StartInfo.FileName = m2;
                    m.Start();
                }
            }
        }
    }
}