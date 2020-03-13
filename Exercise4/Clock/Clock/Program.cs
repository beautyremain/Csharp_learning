using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Clock
{
    public delegate void ClockHandler(object sender,TimeArgs args,TimeArgs args2);
    public class TimeArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class Clock
    {
        public event ClockHandler Running;
        public void Run(TimeArgs args, TimeArgs args2)
        {
            Running(this,args,args2);
        }

    }

    class Program
    {
        public static void Tick(object sender, TimeArgs args, TimeArgs args2)
        {
            Console.WriteLine("Tik tok tik tok...");
        }
        public static void Alarm(object sender, TimeArgs args,TimeArgs args2)
        {
            int h = args2.X;
            int m = args2.Y;
            if(args.X==h&&args.Y==m)
                Console.WriteLine($"Alarm is ringing, now is {h}:{m} !");
        }
        static void Main(string[] args)
        {
            Clock clock = new Clock();
            clock.Running += Tick;
            clock.Running += Alarm;
            int terminate_x=2;
            int terminate_y=10;
            TimeArgs t=new TimeArgs() { X = terminate_x, Y = terminate_y };
            for (int h=0;h<24;h++)
            {
                for(int i=0;i<60;i++)
                {
                    TimeArgs a = new TimeArgs() { X = h, Y = i };
                    clock.Run(a,t);
                    Thread.Sleep(1000);
                    if (i == 59)
                        i = 0;
                }
                if(h==23)
                {
                    h = 0;
                }
            }

            Console.ReadLine();
        }
    }
}
