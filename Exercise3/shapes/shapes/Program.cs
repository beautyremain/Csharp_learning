using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shapes
{
    public interface Shape
    {
        double height { get; set; }
        double width { get; set; }
        double Area { get; }
        bool Judge();
    }
    class Rectangle : Shape
    {
        public double height { get; set; }
        public double width  { get; set; }
        public virtual bool Judge()
        {
            if (height > 0 && width > 0)
            {
                Console.WriteLine("Rectangle is built legally");
                return true;
            }
            else
            {
                Console.WriteLine("Rectangle illegal");
                return false;
            }
        }
        public Rectangle(double h, double w)
        {
            this.height = h;
            this.width = w;
        }
        public double Area
        {
            get
            {
                if (height > 0 && width > 0)
                    return height * width;
                else
                {
                    Judge();
                    return -1;
                }
            }
        }
    }
    class Square : Rectangle
    {
        public Square(double s) : base(s, s)
        {
        }
        override public bool Judge()
        {
            if (height > 0 && width > 0)
            {
                Console.WriteLine("Square is built legally");
                return true;
            }
            else
            {
                Console.WriteLine("Square illegal");
                return false;
            }
        }

    }
    class Triangle : Shape
    {
        public double height { get; set; }
        public double width { get; set; }
        public bool Judge()
        {
            if (height > 0 && width > 0)
            {
                Console.WriteLine("Triangle is built legally");
                return true;
            }
            else
            {
                Console.WriteLine("Triangle illegal");
                return false;
            }
        }
        public Triangle(double h, double w)
        {
            this.height = h;
            this.width = w;
        }
        public double Area
        {
            get
            {
                if (height > 0 && width > 0)
                    return height * width / 2;
                else
                {
                    Judge();
                    return -1;
                }
            }
        }
    }
    class Factory
    {
        public static Shape GetShape(int num)
        {
            Shape S;
            Random rd = new Random();
            switch (num)
            {
                case 1:
                    {
                        S = new Square(rd.NextDouble() + rd.Next(0, 10));
                        Console.WriteLine($"Square {S.height},{S.width} is successfully built");
                        break;
                    }
                case 2:
                    {
                        S = new Rectangle(rd.NextDouble() + rd.Next(0, 10), rd.NextDouble() + rd.Next(0, 10));
                        Console.WriteLine($"Rectangle {S.height},{S.width} is successfully built");
                        break;
                    }
                default:
                    {
                        S = new Triangle(rd.NextDouble() + rd.Next(0, 10), rd.NextDouble() + rd.Next(0, 10));
                        Console.WriteLine($"Triangle {S.height},{S.width} is successfully built");
                        break;
                    }
            }
            return S;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            double sum=0;
                Random rd = new Random();
            for(int i=0;i<10;i++)
            {
                Shape S;
                int num = rd.Next(0,3);;
                S=Factory.GetShape(num);
                if(S.Judge())
                    sum += S.Area;

            }
            Console.WriteLine(sum);
            Console.ReadLine();
        }
    }
}
