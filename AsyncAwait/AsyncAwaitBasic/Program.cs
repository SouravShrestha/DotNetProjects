using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace AsyncAwaitBasic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var watch = Stopwatch.StartNew();

            //Note: Always make async method return type as Task, if void otherwise with return data type like Task<string>

            //RunDownload() //Runs Sync. UI is stuck unless action in completed in case of UI based applications

            //RunDownloadAsync(); //Without await just triggers and moves to next line. 

            //await RunDownloadAsync(); //Using async makes the UI usable while the tasks are being completed. The runtime is almost same as the Sync method.

            await RunDownloadParallelAsync(); //Using this parallel async approach makes the UI usable while the tasks are being completed. The runtime is improved sinces tasks execute in parallel.

            watch.Stop();
            Console.WriteLine($"Total execution time: { watch.ElapsedMilliseconds }");
            Console.ReadLine();
        }

        private static List<string> PrepareData()
        {
            List<string> output = new List<string>();

            output.Add("https://www.yahoo.com");
            output.Add("https://www.google.com");
            output.Add("https://www.microsoft.com");
            output.Add("https://www.cnn.com");
            output.Add("https://www.codeproject.com");
            output.Add("https://www.stackoverflow.com");

            return output;
        }

        private static WebsiteDataModel DownloadFiles(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = client.DownloadString(websiteURL);

            return output;
        }

        private static async Task<WebsiteDataModel> DownloadFilesAsync(string websiteURL)
        {
            WebsiteDataModel output = new WebsiteDataModel();
            WebClient client = new WebClient();

            output.WebsiteUrl = websiteURL;
            output.WebsiteData = await client.DownloadStringTaskAsync(websiteURL);

            return output;
        }

        private static void RunDownload()
        {
            var websites = PrepareData();
            foreach(var item in websites)
            {
                WebsiteDataModel data = DownloadFiles(item);
                Console.WriteLine($"{ data.WebsiteUrl } downloaded: { data.WebsiteData.Length } characters long.{ Environment.NewLine }");
            }
        }

        private static async Task RunDownloadAsync()
        {
            var websites = PrepareData();
            foreach (var item in websites)
            {
                //WebsiteDataModel data = await Task.Run(() => DownloadFiles(item)); //Forcing a sync method to behave like async
                WebsiteDataModel data = await DownloadFilesAsync(item);
                Console.WriteLine($"{ data.WebsiteUrl } downloaded: { data.WebsiteData.Length } characters long.{ Environment.NewLine }");
            }
        }

        private static async Task RunDownloadParallelAsync()
        {
            var websites = PrepareData();
            List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();
            foreach (var item in websites)
                tasks.Add(DownloadFilesAsync(item));

            var result = await Task.WhenAll(tasks);

            foreach(var data in result)
                Console.WriteLine($"{ data.WebsiteUrl } downloaded: { data.WebsiteData.Length } characters long.{ Environment.NewLine }");
        }
    }
}
