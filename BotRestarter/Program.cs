﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace BotRestarter
{
    public class Program
    {
        private string[] filePaths;
        private static void Main()
            => new Program().StartAsync().GetAwaiter().GetResult();

        private async Task StartAsync()
        {
            while (true)
            {
                try
                {
                    GetFiles();
                    StartPrograms();
                    await Task.Delay(Config.ConfigData.RestartTime * 1000 * 60);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        private void GetFiles()
        {
            filePaths = Directory.GetFiles("bots", "*.lnk", SearchOption.AllDirectories);
            foreach (var file in filePaths)
            {
                Console.WriteLine($"{DateTime.Now:G} : Found {file}!");
            }
        }

        private void StartPrograms()
        {
            Console.WriteLine($"{DateTime.Now:G} : Restart time = {Config.ConfigData.RestartTime} minuten ({Config.ConfigData.RestartTime * 1000 * 60}ms)");
            foreach (var file in filePaths)
            {
                Console.WriteLine($"{DateTime.Now:G} : Starting {file}");
                Process.Start(file);
            }
        }
    }
}
