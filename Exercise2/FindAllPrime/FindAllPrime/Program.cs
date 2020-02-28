using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAllPrime
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> ans = new List<int>();
            int input_max;
            if (!int.TryParse(Console.ReadLine(), out input_max))
            {
                Console.WriteLine("invalid input");
                return;
            }
            ans =calFind(input_max);
            foreach(int num in ans)
            {
                Console.Write(num + " ");
            }
            Console.ReadLine();
        }
        static List<int> calFind(int max)
        {
            int loc = 0;
            List<int> answer = new List<int>();
            for(int i=2;i<=max;i++)
            {
                answer.Add(i);
            }
            try
            {
                while (answer[loc]* answer[loc] < answer.Last())
                {
                    for (int j = loc + 1; j < answer.ToArray().Length; j++)
                    {
                        if (answer[j] % answer[loc] == 0)
                        {
                            answer.Remove(answer[j]);
                        }
                    }
                    loc++;
                    if (loc >= answer.ToArray().Length)
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(loc);
            }
            //answer.Remove
            return answer;
        }
    }
}
