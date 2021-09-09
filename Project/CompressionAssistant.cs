using System.Diagnostics;
using System.IO;
using System.Windows;

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
           // string temp = "only timing ;-)";
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
            File.Delete(tempfilelocation);
            return File.Exists(target + "\\" + filename);
        }
        //*********************************************************************************************************
        public static bool CompressOneFileForREGISTRY(FileInfo source, DirectoryInfo target, string sevenziplocation)
        {
            string targetdirectory = Path.GetFullPath(target.FullName).Replace(Path.GetFileName(target.FullName) , "");
            string sourcefilename = source.FullName;
            string targetfilename = targetdirectory + Path.GetFileNameWithoutExtension(source.FullName ) + ".7z";
            string tempfilelocation = "C:\\Users\\Public\\###MMHLco\\text.bat";
            File.WriteAllText(tempfilelocation, "echo off" + "\n");
            File.AppendAllText(tempfilelocation, "cd \"" + sevenziplocation + "\" "+"\n");
            string value = "7z a \"" + targetfilename + "\" \"" + sourcefilename + "\"";
            string temp = "only timing ;-)";
            value += " -mx5";
            value += " -t7z";
            File.AppendAllText(tempfilelocation, value);

            Process p = new Process();
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit(100);
            while (p.HasExited == false)
            {
                temp += ".";
                if (p.HasExited)
                {
                    break;
                }
            }
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
            string temp = "only timing ;-)";
            value += " -mx5";
            value += " -t7z";
            File.AppendAllText(tempfilelocation, value);

            Process p = new Process();
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\text.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit(100);
            while (p.HasExited == false)
            {
                temp += ".";
                if (p.HasExited)
                {
                    break;
                }
            }
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
                File.WriteAllText("C:\\Users\\Public\\###MMHLco\\no.ans" , "u \n u \n u \n u");
            }
            string destdir = Path.GetDirectoryName(destenation) +"\\"+ Path.GetFileNameWithoutExtension(destenation)+"\\";
            if (File.Exists("C:\\Users\\Public\\###MMHLco\\mmhl.txt"))
            {
                File.Delete("C:\\Users\\Public\\###MMHLco\\mmhl.txt");
            }
            File.WriteAllText("C:\\Users\\Public\\###MMHLco\\extracttext.bat", "cd " + "\"" + sevenziplocation + "\"" + "\n");
            File.AppendAllText("C:\\Users\\Public\\###MMHLco\\extracttext.bat",
           "7z x \"" + archivelocation + "\" -o\"" + destdir + "\" < C:\\Users\\Public\\###MMHLco\\no.ans >>C:\\Users\\Public\\###MMHLco\\mmhl.txt");
            string temp = "only timing ;-)";
            Process p = new Process();
            bool output;
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\extracttext.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            while (p.HasExited == false)
            {
                temp += ".";
                if (p.HasExited)
                {
                    break;
                }
            }

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
            string temp = "only timing ;-)";
            Process p = new Process();
            bool output;
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\extracttext.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            while (p.HasExited == false)
            {
                temp += ".";
                if (p.HasExited)
                {
                    break;
                }
            }

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
           "7z x " + "\"" + archivelocation + "\"" + " -o" + "\"" + destenation + "\"" + "-p" + password + " >>C:\\Users\\Public\\###MMHLco\\mmhl.txt");
            //    Clipboard.SetText("7z x " + "\"" + archivelocation + "\"" + " -o" + "\"" + destenation + "\"" + " -p" + password + " >>C:\\Users\\Public\\###MMHLco\\mmhl.txt");
            string temp = "only timing ;-)";
            Process p = new Process();
            bool output;
            p.StartInfo.FileName = "C:\\Users\\Public\\###MMHLco\\extracttext.bat";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            while (p.HasExited == false)
            {
                temp += ".";
                if (p.HasExited)
                {
                    break;
                }
            }

            output = File.ReadAllText("C:\\Users\\Public\\###MMHLco\\mmhl.txt").Contains("Everything is");
            return output;
        }


    }
}