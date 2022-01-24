using System;
using System.Drawing;
using Jays.Host_Toolkit.Tools.Crashers;
using Jays.Host_Toolkit.Tools.ViewBot;
using static Colorful.Console;

namespace Jays.Host_Toolkit
{
    public static class Program
    {
        private static void Main()
        {
            Title = "Jays.Host Toolkit | Made by Verity | Version 2.0, Build 01/24/22 | Current Tool: Main Menu ";
            Utils.LoadingSequence();
            ToolkitOptions();
        }

        private static void ToolkitOptions()
        {
            WriteLine(" Jays.Host Online Status: " + Utils.JaysHost() + "\n", Color.DodgerBlue);
            WriteLine(" [1] Crasher Method: Plans", Color.DodgerBlue);
            WriteLine(" [2] Crasher Method: Pages (Better)", Color.DodgerBlue);
            WriteLine(" [3] Crasher Method: Method 3", Color.DodgerBlue);
            WriteLine(" [4] Crasher Method: Method 4", Color.DodgerBlue);
            WriteLine(" [5] Crasher Method: Method 5", Color.DodgerBlue);
            WriteLine(" [6] User Dumper", Color.DodgerBlue);
            WriteLine(" [7] Bio View Bot \n", Color.DodgerBlue);
            Write(" --> ", Color.DarkSlateBlue);
            var option = ReadLine();
            switch (option)
            {
                case "1":
                    PlansCrasher.Start();
                    break;
                case "2":
                    PageCrasher.Start();
                    break;
                case "3":
                    DomainsCrasher.Start();
                    break;
                case "4":
                    GalleryCrasher.Start();
                    break;
                case "5":
                    BiosCrasher.Start();
                    break;
                case "6":
                    UserDumper.Start();
                    break;
                case "7":
                    ViewBot.Start();
                    break;
            }
        }
    }
}