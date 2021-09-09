using System;
using System.Diagnostics;
using System.IO;

namespace mCompress
{
    public enum CompressionRatio
    {
        JustPack,
        Normal,
        Ultra
    }

    public enum CompressionFormat
    {
        sevenzip,
        zip,
        tar,
        rar,
        wim
    }
    public enum OneFileCompressionFormat
    {
        xz,
        gzip,
        bzip2
    }
    internal class CompressionAssistant
    {
        public static bool CompressOneFile(FileInfo source, FileInfo targetfilename, string sevenziplocation, CompressionRatio ratio, OneFileCompressionFormat format)
        {
            if (!Directory.Exists("C:\\Users\\Public\\###MMHLco\\"))
            {
                Directory.CreateDirectory("C:\\Users\\Public\\###MMHLco\\");
            }
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "echo off" + "\n");
            File.AppendAllText(tempfilelocation, "cd " + "\"" + sevenziplocation + "\"" + "\n");
            string value = "7z" + " a " + "\"" + targetfilename.FullName + "\"" + " " + "\"" + source.FullName + "\"";

            switch (ratio)
            {
                case CompressionRatio.JustPack:
                    value += " -mx0";
                    break;
                case CompressionRatio.Normal:
                    value += " -mx5";
                    break;
                case CompressionRatio.Ultra:
                    value += " -mx9";
                    break;
            }
            switch (format)
            {
                case OneFileCompressionFormat.xz:
                    value += " -txz";
                    break;
                case OneFileCompressionFormat.gzip:
                    value += " -tgzip";
                    break;
                case OneFileCompressionFormat.bzip2:
                    value += " -tbzip2";
                    break;
            }
            File.AppendAllText(tempfilelocation, value);

            Process p = new Process();

            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            string formatt = "";
            switch (format)
            {
                case OneFileCompressionFormat.xz:
                    formatt = ".xz";
                    break;
                case OneFileCompressionFormat.gzip:
                    formatt = ".gz";
                    break;
                case OneFileCompressionFormat.bzip2:
                    formatt = ".bz2";
                    break;
                default:
                    break;
            }
            //      File.Delete(tempfilelocation);
            return File.Exists(targetfilename.FullName + formatt);
        }
        //*********************************************************************************************************
        //*********************************************************************************************************

        public static bool Compress(string source, string target, string sevenziplocation, string filename, CompressionRatio ratio, CompressionFormat format)
        {
            if (!Directory.Exists("C:\\Users\\Public\\###MMHLco\\"))
            {
                Directory.CreateDirectory("C:\\Users\\Public\\###MMHLco\\");
            }
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "echo off" + "\n");
            File.AppendAllText(tempfilelocation, "cd " + "\"" + sevenziplocation + "\"" + "\n");
            string value = "7z" + " a " + "\"" + target + "\\" + filename + "\"" + " " + "\"" + source + "\"";
            switch (ratio)
            {
                case CompressionRatio.JustPack:
                    value += " -mx0";
                    break;
                case CompressionRatio.Normal:
                    value += " -mx5";
                    break;
                case CompressionRatio.Ultra:
                    value += " -mx9";
                    break;
            }
            switch (format)
            {
                case CompressionFormat.sevenzip:
                    value += " -t7z";
                    break;
                case CompressionFormat.zip:
                    value += " -tzip";
                    break;

                case CompressionFormat.rar:
                    value += " -trar";
                    break;
                case CompressionFormat.tar:
                    value += " -ttar";
                    break;
                case CompressionFormat.wim:
                    value += " -twim";
                    break;
            }
            File.AppendAllText(tempfilelocation, value);

            Process p = new Process();

            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            //     File.Delete(tempfilelocation);
            return File.Exists(target + "\\" + filename);
        }
        //*********************************************************************************************************

        public static bool CompressSFX(string source, string target, string sevenZipLocation, CompressionRatio ratio)
        {
            string ziploc = System.Windows.Forms.Application.ExecutablePath.Replace("mCompressdotNET6.exe", "") + @"sevenzip\";
            string compressionLevel = "-mx9";
            if (File.Exists(ziploc + "\\list.dll.l"))
            {
                File.Delete(ziploc + "\\list.dll.l");
            }

            switch (ratio)
            {
                case CompressionRatio.JustPack:
                    compressionLevel = "-mx9";
                    break;
                case CompressionRatio.Normal:
                    compressionLevel = "-mx0";
                    break;
                case CompressionRatio.Ultra:
                    compressionLevel = "-mx5";
                    break;
                default:
                    compressionLevel = "-mx9";
                    break;

            }

            Process p = new Process();
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "echo off" + "\n");
            File.AppendAllText(tempfilelocation, "cd " + "\"" + ziploc + "\"" + "\n");
            File.AppendAllText(tempfilelocation, $"7z a -sfx7z.sfx \"{target}\" \"{source}\" {compressionLevel} > list.dll.l");

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

        public static bool CompressOneFileForREGISTRY(FileInfo source, DirectoryInfo target, string sevenziplocation)
        {
            string targetdirectory = Path.GetFullPath(target.FullName).Replace(Path.GetFileName(target.FullName), "");
            string sourcefilename = source.FullName;
            string targetfilename = targetdirectory + Path.GetFileNameWithoutExtension(source.FullName) + ".7z";
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "echo off" + "\n");
            File.AppendAllText(tempfilelocation, "cd \"" + sevenziplocation + "\" " + "\n");
            string value = "7z a \"" + targetfilename + "\" \"" + sourcefilename + "\"";

            value += " -mx5";
            value += " -t7z";
            File.AppendAllText(tempfilelocation, value);

            Process p = new Process();
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();

            //   File.Delete(tempfilelocation);
            return File.Exists(targetfilename);
        }
        //*********************************************************************************************************
        //*********************************************************************************************************
        public static bool CompressOneFolderForREGISTRY(FileInfo source, DirectoryInfo target, string sevenziplocation)
        {
            if (!Directory.Exists("C:\\Users\\Public\\###MMHLco\\"))
            {
                Directory.CreateDirectory("C:\\Users\\Public\\###MMHLco\\");
            }
            string targetdirectory = Path.GetFullPath(target.FullName).Replace(Path.GetFileName(target.FullName), "");
            string sourcefilename = source.FullName;
            string targetfilename = targetdirectory + Path.GetFileNameWithoutExtension(source.FullName) + ".7z";
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "echo off" + "\n");
            File.AppendAllText(tempfilelocation, "cd \"" + sevenziplocation + "\" " + "\n");
            string value = "7z a \"" + targetfilename + "\" \"" + sourcefilename + "\"";

            value += " -mx5";
            value += " -t7z";
            File.AppendAllText(tempfilelocation, value);

            Process p = new Process();
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();

            //   File.Delete(tempfilelocation);
            return File.Exists(targetfilename);
        }
        /*******************************************************************************/
        public static bool ExtractForREG(string archivelocation, string destenation, string sevenziplocation)
        {
            if (!Directory.Exists("C:\\Users\\Public\\###MMHLco\\"))
            {
                Directory.CreateDirectory("C:\\Users\\Public\\###MMHLco\\");
            }
            if (File.Exists("C:\\Users\\Public\\###MMHLco\\mmhl.txt"))
            {
                File.Delete("C:\\Users\\Public\\###MMHLco\\mmhl.txt");
            }
            if (!File.Exists("C:\\Users\\Public\\###MMHLco\\no.ans"))
            {
                File.WriteAllText("C:\\Users\\Public\\###MMHLco\\no.ans", "u \n u \n u \n u");
            }
            string destdir = Path.GetDirectoryName(destenation) + "\\" + Path.GetFileNameWithoutExtension(destenation) + "\\";
            if (File.Exists("C:\\Users\\Public\\###MMHLco\\mmhl.txt"))
            {
                File.Delete("C:\\Users\\Public\\###MMHLco\\mmhl.txt");
            }
            File.WriteAllText("C:\\Users\\Public\\###MMHLco\\extracttext.bat", "cd " + "\"" + sevenziplocation + "\"" + "\n");
            File.AppendAllText("C:\\Users\\Public\\###MMHLco\\extracttext.bat",
           "7z x \"" + archivelocation + "\" -o\"" + destdir + "\" < C:\\Users\\Public\\###MMHLco\\no.ans >>C:\\Users\\Public\\###MMHLco\\mmhl.txt");

            Process p = new Process();
            bool output;
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\extracttext.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();

            output = File.ReadAllText("C:\\Users\\Public\\###MMHLco\\mmhl.txt").Contains("Everything is");
            return output;
        }
        /*******************************************************************************/
        public static bool Extract(string archivelocation, string destenation, string sevenziplocation)
        {
            if (!Directory.Exists("C:\\Users\\Public\\###MMHLco\\"))
            {
                Directory.CreateDirectory("C:\\Users\\Public\\###MMHLco\\");
            }
            if (!File.Exists("C:\\Users\\Public\\###MMHLco\\no.ans"))
            {
                File.WriteAllText("C:\\Users\\Public\\###MMHLco\\no.ans", "u \n u \n u \n u");
            }
            if (File.Exists("C:\\Users\\Public\\###MMHLco\\mmhl.txt"))
            {
                File.Delete("C:\\Users\\Public\\###MMHLco\\mmhl.txt");
            }
            File.WriteAllText("C:\\Users\\Public\\###MMHLco\\extracttext.bat", "cd " + "\"" + sevenziplocation + "\"" + "\n");
            File.AppendAllText("C:\\Users\\Public\\###MMHLco\\extracttext.bat",
           "7z x " + "\"" + archivelocation + "\"" + " -o" + "\"" + destenation + "\"" + " < C:\\Users\\Public\\###MMHLco\\no.ans >>C:\\Users\\Public\\###MMHLco\\mmhl.txt");

            Process p = new Process();
            bool output;
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\extracttext.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();

            output = File.ReadAllText("C:\\Users\\Public\\###MMHLco\\mmhl.txt").Contains("Everything is");
            return output;
        }
        /// <summary>
        /// extracting with password
        /// </summary>
        /// <param name="archivelocation"></param>
        /// <param name="destenation"></param>
        /// <param name="sevenziplocation"></param>
        /// <param name="password">the password(if it hasen't password no matter, file will be extracted</param>
        /// <returns></returns>
        public static bool Extract(string archivelocation, string destenation, string sevenziplocation, string password)
        {
            if (!Directory.Exists("C:\\Users\\Public\\###MMHLco\\"))
            {
                Directory.CreateDirectory("C:\\Users\\Public\\###MMHLco\\");
            }
            if (File.Exists("C:\\Users\\Public\\###MMHLco\\mmhl.txt"))
            {
                File.Delete("C:\\Users\\Public\\###MMHLco\\mmhl.txt");
            }
            File.WriteAllText("C:\\Users\\Public\\###MMHLco\\extracttext.bat", "cd " + "\"" + sevenziplocation + "\"" + "\n");
            File.AppendAllText("C:\\Users\\Public\\###MMHLco\\extracttext.bat",
           $"7z x \"{archivelocation}\" -o\"{destenation}\" -p\"{password}\"<{sevenziplocation}\\no.ans");
            Process p = new();
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\extracttext.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            string strout = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            try
            {
                return strout.Contains("Everything is Ok");
            }
            catch
            {
                return false;
            }
        }

    }
}