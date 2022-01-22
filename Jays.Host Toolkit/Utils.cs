using System;
using System.Drawing;
using System.Net;
using System.Threading;

namespace Jays.Host_Toolkit
{
    public static class Utils
    {
        public static void LoadingSequence()
        {
            Console.Clear();
            Console.Write(" L");
            Thread.Sleep(200);
            Console.Write("\r Lo");
            Thread.Sleep(200);
            Console.Write("\r Loa");
            Thread.Sleep(200);
            Console.Write("\r Load");
            Thread.Sleep(200);
            Console.Write("\r Loadi");
            Thread.Sleep(200);
            Console.Write("\r Loadin");
            Thread.Sleep(200);
            Console.Write("\r Loading");
            Thread.Sleep(1000);
            Console.Clear();
            Logo();
        }
        
        private static void Logo()
        {
            Colorful.Console.WriteWithGradient(@"
      |                         |   |               |       __ __|              |  |    _)  |
      |   _` |  |   |   __|     |   |   _ \    __|  __|        |   _ \    _ \   |  |  /  |  __|
  \   |  (   |  |   | \__ \     ___ |  (   | \__ \  |          |  (   |  (   |  |    <   |  | 
 \___/  \__,_| \__, | ____/ _) _|  _| \___/  ____/ \__|       _| \___/  \___/  _| _|\_\ _| \__|
               ____/                                                                                            " + "\n" + "\n", Color.Red, Color.Purple, 14);
        }
        
        public static bool JaysHost()
        {
            try
            {
                var jaysRequest = WebRequest.Create("https://Jays.Host");
                var jaysResponse = (HttpWebResponse)jaysRequest.GetResponse();
                return jaysResponse.StatusCode == HttpStatusCode.OK;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}