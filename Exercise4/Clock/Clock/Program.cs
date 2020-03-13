using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
    public delegate void ClockHandler(object sender,TimeArgs args);
    public class TimeArgs
    {
        public string X { get; set; }
        public string Y { get; set; }
    }
    public class Clock
    {
        public event ClockHandler Running;
        public void Run(string x, string y)
        {
            TimeArgs args = new TimeArgs() { X = x, Y = y };
            Console.WriteLine("Clock begin to run...");
            Running(this,args);
        }
        public void Tick(object sender, TimeArgs args)
        {
            Console.WriteLine("Tik tok tik tok...");
        }
        public void Alarm(object sender, TimeArgs args)
        {
            Console.WriteLine($"Alarm is ringing, now is {args.X}:{args.Y} !");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Clock clock = new Clock();
            clock.Running += clock.Tick;
            clock.Running += clock.Alarm;
            clock.Run("18", "00");
            Console.ReadLine();
        }
    }
}
