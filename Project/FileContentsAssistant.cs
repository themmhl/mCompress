using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MMHLco
{
    internal class FileContentsAssistant
    {
        public enum ArchiveFormat
        {
            sevenzip,
            zip,
            tar,
            rar,
            wim
        }
        public enum OneFileArchiveFormat
        {
            xz,
            gzip,
            bzip2
        }
        public static List<string> GetFileNamesList(string filelocation)
        {
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompress.exe", "") + @"sevenzip\";
            if (File.Exists(ziploc + "list.dll"))
            {
                File.Delete(ziploc + "list.dll");
            }
            Process p = new Process();
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "echo off" + "\n");
            File.AppendAllText(tempfilelocation, "cd " + "\"" + ziploc + "\"" + "\n");
            File.AppendAllText(tempfilelocation, "7z l -ba -slt " + "\"" + filelocation + "\"" + ">list.dlll" + "\n");
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            string str1 = "";
            try
            {

                str1 = File.ReadAllText(ziploc + "list.dlll");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Somthing went wrong, try again");
                mCompress.App.Current.Shutdown();
            }
            //find file counts
            //by stackoverflow
            int length = str1.Length;
            int cOunt = str1.Select((c, pi) => str1.Substring(pi))
                .Count(sub => sub.StartsWith("Path"));

            int[] index = new int[cOunt];
            int[] index2 = new int[cOunt];
            int indexname = 0;
            int indexname2 = 0;
            int i = 1;
            int i2 = 1;

            string[] contents = new string[cOunt++];

            bool isfirst = true;
            int[] index1 = new int[2];
            if (!isfirst)
            {
                isfirst = false;
                for (int count = i; count < str1.Length; count++)
                {//this 'for' is for path
                    try
                    {
                        index[count] = str1.IndexOf("Path", indexname + 4);
                        indexname = index[count];
                        i = count;
                        if (str1.IndexOf("Modified", indexname + 4) == -1)
                        {
                            break;
                        }
                    }
                    catch
                    {
                        break;
                    }
                }


                for (int count = i2; count < str1.Length; count++)
                {//this 'for' is for modified
                    try
                    {
                        if (str1.IndexOf("Modified", indexname2 + 4) == -1)
                        {
                            break;
                        }
                        index2[count] = str1.IndexOf("Modified", indexname2 + 4);
                        indexname2 = index2[count];
                        i2 = count;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }
            else
            {
                isfirst = false;
                index1[0] = 0;
                index1[1] = str1.IndexOf("Modified");
                contents[0] = str1.Substring(index1[0], index1[1]);
                System.Windows.Forms.MessageBox.Show(contents[0].ToString());
            }



            for (int i22 = 1; i22 < str1.Length; i22++)
            {
                try
                {
                    contents[i22] = str1.Substring(index[i22--], index2[i22--] - index[i22--]);
                }
                catch (Exception)
                {

                    break;
                }

            }
            //**********************************************************

            for (int ir3 = 0; ir3 < contents.Length; ir3++)
            {
                try
                {
                    contents[ir3] = contents[ir3].Replace("\r\n", "               ");
                }
                catch (Exception)
                {
                    break;

                }
            }
            //**********************************************************

            List<string> lst = new List<string>();
            for (int line = 1; line > 0; line++)
            {
                try
                {
                    lst.Add(contents[line]);
                }
                catch (Exception)
                {

                    break;
                }
            }

            // System.Windows.Forms.MessageBox.Show(lst[0].ToString() + "\n\n\n\n" + lst[1].ToString() + "\n\n\n\n" + lst[2].ToString() + "\n\n\n\n");
            return lst;
        }



    }
}
