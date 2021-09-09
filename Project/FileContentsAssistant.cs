using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MMHLco
{
    internal class FileContentsAssistant
    {
        private static string[] contentLastFile;
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
        /// <summary>
        /// extracts a file from an archive
        /// </summary>
        /// <param name="archiveName">what is archive name</param>
        /// <param name="destenation">where to save</param>
        /// <param name="fileindex">index of file in archive</param>
        /// <param name="password">set to null if hasen't</param>
        /// <returns></returns>
        public static bool ExtractFile(string archiveName, string destenation, int fileindex, string password)
        {
            string indexed = contentLastFile[fileindex];
            string pathSaved = indexed.Substring(indexed.IndexOf("Path") + 7, indexed.IndexOf("\r\n") - 7);
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";

            if (File.Exists(ziploc + "\\list.dll.l"))
            {
                File.Delete(ziploc + "\\list.dll.l");
            }

            Process p = new Process();
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "echo off" + "\n");
            File.AppendAllText(tempfilelocation, "cd " + "\"" + ziploc + "\"" + "\n");
            if (password == null)
            {
                File.AppendAllText(tempfilelocation, $"7z e \"{archiveName}\" -o\"{destenation}\" \"{pathSaved}\" -r > list.dll.l");
            }
            else
            {
                File.AppendAllText(tempfilelocation, $"7z e \"{archiveName}\" -p\"{password}\" -o\"{destenation}\" \"{pathSaved}\" -r > list.dll.l");
            }
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            p.WaitForExit();
            string output = File.ReadAllText(ziploc + @"\list.dll.l");

            try
            {
                Process procKillSevenZip = new();
                procKillSevenZip.StartInfo.CreateNoWindow = true;
                procKillSevenZip.StartInfo.FileName = "cmd.exe";
                procKillSevenZip.StartInfo.Arguments = " /c taskkill /IM 7z.exe /f";
                procKillSevenZip.Start();
                procKillSevenZip.WaitForExit();
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Somthing went wrong, try again");
                System.Windows.Application.Current.Shutdown();
            }
            if (output.Contains("Everything is Ok"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool TestFile(string archiveName, int fileIndex)
        {
            string indexed = contentLastFile[fileIndex];
            string pathDeleted = indexed.Substring(indexed.IndexOf("Path") + 7, indexed.IndexOf("\r\n") - 7);
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";

            if (File.Exists(ziploc + "\\list.dll.l"))
            {
                File.Delete(ziploc + "\\list.dll.l");
            }

            Process p = new();
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "echo off" + "\n");
            File.AppendAllText(tempfilelocation, "cd " + "\"" + ziploc + "\"" + "\n");
            File.AppendAllText(tempfilelocation, "7z t \"" + archiveName + "\" \"" + pathDeleted + "\" -r > list.dll.l");
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();

            p.WaitForExit();
            string output = File.ReadAllText(ziploc + @"\list.dll.l");

            try
            {
                //this procces kills current 7-zip thread for future usage
                Process procKillSevenZip = new();
                procKillSevenZip.StartInfo.CreateNoWindow = true;
                procKillSevenZip.StartInfo.FileName = "cmd.exe";
                procKillSevenZip.StartInfo.Arguments = " /c taskkill /IM 7z.exe /f";
                procKillSevenZip.Start();
                procKillSevenZip.WaitForExit();
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Somthing went wrong, try again");
                System.Windows.Application.Current.Shutdown();
            }
            if (output.Contains("Everything is Ok"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool TestArchive(string archiveName)
        {
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";

            if (File.Exists(ziploc + "\\list.dll.l"))
            {
                File.Delete(ziploc + "\\list.dll.l");
            }

            Process p = new();
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "echo off" + "\n");
            File.AppendAllText(tempfilelocation, "cd " + "\"" + ziploc + "S\"" + "\n");
            File.AppendAllText(tempfilelocation, $"7z t \"{archiveName}\" * -r > list.dll.l");
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();

            p.WaitForExit();
            string output = File.ReadAllText(ziploc + @"\list.dll.l");

            try
            {
                //this procces kills current 7-zip thread for future usage
                Process procKillSevenZip = new();
                procKillSevenZip.StartInfo.CreateNoWindow = true;
                procKillSevenZip.StartInfo.FileName = "cmd.exe";
                procKillSevenZip.StartInfo.Arguments = " /c taskkill /IM 7z.exe /f";
                procKillSevenZip.Start();
                procKillSevenZip.WaitForExit();
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Somthing went wrong, try again");
                System.Windows.Application.Current.Shutdown();
            }
            if (output.Contains("Everything is Ok"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string CopyFileToTemp(string archiveName, int fileindex, string password)
        {
            string indexed = contentLastFile[fileindex];
            string pathCopied = indexed.Substring(indexed.IndexOf("Path") + 7, indexed.IndexOf("\r\n") - 7);
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";
            if (File.Exists(ziploc + "\\list.dll.l"))
            {
                File.Delete(ziploc + "\\list.dll.l");
            }

            Process p = new Process();
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "echo off" + "\n");
            File.AppendAllText(tempfilelocation, "cd " + "\"" + ziploc + "\"" + "\n");
            if (password == null)
            {
                File.AppendAllText(tempfilelocation, "7z e \"" + archiveName + "\" -o\"C:\\Users\\Public\\###MMHLco\\\" \"" + pathCopied + "\"  -r <no.ans");
            }
            else
            {
                File.AppendAllText(tempfilelocation, $"7z e \"{archiveName}\" -o\"C:\\Users\\Public\\###MMHLco\\\" \"{pathCopied}\" -p\"{password}\" -r <no.ans");
            }
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            try
            {
                //this procces kills current 7-zip thread for future usage
                Process procKillSevenZip = new();
                procKillSevenZip.StartInfo.CreateNoWindow = true;
                procKillSevenZip.StartInfo.FileName = "cmd.exe";
                procKillSevenZip.StartInfo.Arguments = " /c taskkill /IM 7z.exe /f";
                procKillSevenZip.Start();
                procKillSevenZip.WaitForExit();
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Somthing went wrong, try again");
                System.Windows.Application.Current.Shutdown();
            }
            if (output.Contains("Everything"))
            {
                return "C:\\Users\\Public\\###MMHLco\\" + Path.GetFileName("c:" + pathCopied);
            }
            else
            {
                return "C:\\Users\\Public\\###MMHLco\\" + Path.GetFileName("c:" + pathCopied);
            }
        }

        public static int DeleteFile(string archiveName, int fileindex)
        {
            string indexed = contentLastFile[fileindex];
            string pathDeleted = indexed.Substring(indexed.IndexOf("Path") + 7, indexed.IndexOf("\r\n") - 7);
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";

            if (File.Exists(ziploc + "\\list.dll.l"))
            {
                File.Delete(ziploc + "\\list.dll.l");
            }

            Process p = new Process();
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "echo off" + "\n");
            File.AppendAllText(tempfilelocation, "cd " + "\"" + ziploc + "\"" + "\n");
            File.AppendAllText(tempfilelocation, "7z d \"" + archiveName + "\" \"" + pathDeleted + "\" -r <no.ans");
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            try
            {
                //this procces kills current 7-zip thread for future usage
                Process procKillSevenZip = new();
                procKillSevenZip.StartInfo.CreateNoWindow = true;
                procKillSevenZip.StartInfo.FileName = "cmd.exe";
                procKillSevenZip.StartInfo.Arguments = " /c taskkill /IM 7z.exe /f";
                procKillSevenZip.Start();
                procKillSevenZip.WaitForExit();
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Somthing went wrong, try again");
                System.Windows.Application.Current.Shutdown();
            }
            if (output.Contains("Everything"))
            {
                return 0;
            }
            else if (output.Contains("Not implemented"))
            {
                return -2;
            }
            else
            {
                return -1;
            }

        }
        //****************************************************************
        public static List<string> GetFileNamesList(string filelocation)
        {
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";
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
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            string str1 = "";
            try
            {
                Process procKillSevenZip = new();
                procKillSevenZip.StartInfo.CreateNoWindow = true;
                procKillSevenZip.StartInfo.FileName = "cmd.exe";
                procKillSevenZip.StartInfo.Arguments = " /c taskkill /IM 7z.exe /f";
                procKillSevenZip.Start();
                procKillSevenZip.WaitForExit();
                str1 = File.ReadAllText(ziploc + "list.dlll");
            }
            catch (Exception)
            {

                System.Windows.MessageBox.Show("Somthing went wrong, try again");
                System.Windows.Application.Current.Shutdown();
            }
            //find file counts
            //by stackoverflow
            int length = str1.Length;
            int cOunt = str1.Select((c, pi) => str1.Substring(pi))
                .Count(sub => sub.StartsWith("Path")) + 1;

            int[] index = new int[cOunt];
            int[] index2 = new int[cOunt];
            int indexname = 0;
            int indexname2 = 0;
            int i = 1;
            int i2 = 1;

            string[] contents = new string[++cOunt];
            int[] index1 = new int[2];
            for (int count = i; count < str1.Length + 1; count++)
            {//this 'for' is for path
                try
                {
                    index[count] = str1.IndexOf("Path", indexname + 4);
                    indexname = index[count];
                    i = count;
                }
                catch { break; }
            }
            for (int count = i2; count < str1.Length + 1; count++)
            {//this 'for' is for modified
                try
                {
                    index2[count] = str1.IndexOf("Modified", indexname2 + 8);
                    indexname2 = index2[count];
                    i2 = count;
                }
                catch { break; }
            }
            for (int i22 = 1; i22 < str1.Length; i22++)
            {
                try
                {
                    contents[i22] = str1.Substring(index[i22], index2[i22 + 1] - index[i22]);
                }
                catch { break; }
            }
            //**********************************************************

            for (int ir3 = 0; ir3 < contents.Length; ir3++)
            {
                try
                {
                    contents[ir3] = contents[ir3].Replace("\r\n", "               ");
                }
                catch { break; }
            }
            try
            {
                contents[0] = str1.Substring(0, index2[1]);
            }
            catch { }

            //**********************************************************

            List<string> lst = new List<string>();
            for (int line = 0; line < cOunt; line++)
            {
                try
                {
                    lst.Add(contents[line]);
                }
                catch { break; }
            }
            contentLastFile = contents;
            return lst;
        }
        public static bool IsPasswordProtected(string filename)
        {//vai Stackoverflow
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";

            bool result = false;

            Process p = new Process();
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "cd " + "\"" + ziploc + "\"" + "\n");
            File.AppendAllText(tempfilelocation, $"7z l -slt \"{filename}\"\n");
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            string stdout = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            if (stdout.Contains("Encrypted = +"))
            {
                result = true;
            }
            return result;
        }
    }
}
