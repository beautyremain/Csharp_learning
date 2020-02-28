using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSpecialNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] num_list = { 2, 1, 4, 6, 3, 7, 8, 12 };
            int max = num_list[0];
            int min = num_list[0];
            int sum = 0;
            double ave;
            foreach (int num in num_list)
            {
                sum += num;
                if (num > max)
                    max = num;
                else if (num < min)
                    min = num;
            }
            //Console.Write(sum);
            Console.WriteLine("max number is {0},min number is {1},sum is {2},average is {3}", max, min, sum, sum / num_list.Length);
            Console.ReadLine();
        }
    }
}
