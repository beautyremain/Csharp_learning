using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindPrime
{
    class Program
    {
        static void Main(string[] args)
        {
            int input;
            List<int> answer=new List<int>();
            if (!int.TryParse(Console.ReadLine(), out input))
                Console.WriteLine("invalid input");
            else
            {
                answer=judge(input);
            }
            foreach(int num in answer)
                Console.Write(num + " ");
            Console.ReadLine();

        }
        static List<int> judge(int num)
        {
            List<int> ans=new List<int>();
            for(int i=2;i<num/2;)
            {
                if (num % i == 0)
                {
                    num /= i;
                    if (ans.Contains(i))
                        continue;
                    else
                        ans.Add(i);            
                }
                else
                {
                    i++;
                }
            }
            return ans;
        }
    }
}
