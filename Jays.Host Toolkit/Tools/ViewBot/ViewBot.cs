using System;
using System.Drawing;
using System.Net;
using System.Threading;
using static Colorful.Console;

namespace Jays.Host_Toolkit.Tools.ViewBot
{
    public static class ViewBot
    {
        private static string _username;
        private static int _viewsSent;
        
        public static void Start()
        {
            tryAgain:
            try
            {
                Title = "Jays.Host Toolkit | Made by Verity | Version 1.0, Build 01/20/22 | Current Tool: ViewBot";
            
                Utils.LoadingSequence();
            
                WriteLine(" Page/Bio Name:", Color.DodgerBlue);
                Write(" --> ", Color.DarkSlateBlue); 
                _username = ReadLine();
                
                Console.WriteLine("");
                
                WriteLine(" Threads [5 Recommended]:", Color.DodgerBlue);
                Write(" --> ", Color.DarkSlateBlue);
                var threads = ReadLine();
                
                var convertThreads = Convert.ToInt32(threads);
                for (var i = 0; i < convertThreads; i++)
                {
                    new Thread(GetThoseViews).Start();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n Error, please enter the NUMBER of threads, its an integer not a string.");
                Console.ReadKey();
                Console.Clear();
                goto tryAgain;
            }

            Console.ReadLine();
        }

        private static async void GetThoseViews()
        {
            while (true)
            {
                try
                {
                    Title = "Jays.Host Toolkit | Made by Verity | Version 1.0, Build 01/20/22 | Current Tool: ViewBot | Views Added: " + _viewsSent;
                    var viewEndpoint = WebRequest.Create($"https://jays.host/api/v1/bios/view/{_username}");
                    viewEndpoint.Headers.Add("User-Agent", "Verity");
                    viewEndpoint.Method = WebRequestMethods.Http.Put;
                    var responseMessage = (HttpWebResponse)await viewEndpoint.GetResponseAsync();
                    var contentString = responseMessage.StatusCode;
                    WriteLine(contentString == HttpStatusCode.OK ? " #" + _viewsSent + " Views Sent." : " View failed to send.", Color.Green);
                    _viewsSent += 1;
                }
                catch (WebException e)
                {
                    if ((HttpStatusCode) e.Status == HttpStatusCode.TooManyRequests)
                    {
                        WriteLine(" Rate Limited.. (wait like a little bit)", Color.Red);
                    }
                    else
                    {
                        WriteLine(" Rate Limited.. (wait like a little bit) " + e.Message, Color.Red);
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}