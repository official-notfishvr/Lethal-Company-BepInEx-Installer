using System;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lethal_Company_BepInEx_Installer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please Put Your Lethal Company Path:");
                string lethalCompanyFolderPath = Console.ReadLine();

                string zipUrl = "https://github.com/BepInEx/BepInEx/releases/download/v5.4.22/BepInEx_x64_5.4.22.0.zip";
                string zipFileName = Path.Combine(lethalCompanyFolderPath, "BepInEx_x64_5.4.22.0.zip");

                using (WebClient client = new WebClient())
                {
                    Console.WriteLine("Downloading BepInEx...");
                    client.DownloadFile(zipUrl, zipFileName);
                    Console.WriteLine("Download complete.");

                    Console.WriteLine("Extracting BepInEx...");
                    ZipFile.ExtractToDirectory(zipFileName, lethalCompanyFolderPath);
                    Console.WriteLine("Extraction complete.");
                    Console.WriteLine("BepInEx installation successful.");
                    File.Delete(zipFileName);
                    Console.WriteLine("Opening Lethal Company");
                    Process lethalCompanyProcess = Process.Start(Path.Combine(lethalCompanyFolderPath, "Lethal Company.exe"));
                    Task.Delay(7500).Wait();
                    if (lethalCompanyProcess != null && !lethalCompanyProcess.HasExited)
                    {
                        lethalCompanyProcess.CloseMainWindow();
                        lethalCompanyProcess.WaitForExit(5000);
                        Console.WriteLine("Cloesing Lethal Company");
                    }
                    Task.Delay(1000).Wait();
                    string configFolderPath = Path.Combine(lethalCompanyFolderPath, "BepInEx", "config");
                    string configFileUrl = "https://notfishvr.dev/cdn/BepInEx.cfg";
                    string configFilePath = Path.Combine(configFolderPath, "BepInEx.cfg");
                    client.DownloadFile(configFileUrl, configFilePath);
                    Console.WriteLine("Done.");
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
