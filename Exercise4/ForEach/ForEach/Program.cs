using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEach
{
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }
        public Node(T t)
        {
            Next = null;
            Data = t;
        }
    }
    public class GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;
        public GenericList()
        {
            tail = null;
            head = tail;
        }
        public Node<T> Head
        {
            get => head;
        }
        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);
            if(tail==null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }
        public void ForEach(Action<T> action)
        {
            Node<T> p = head;
            while(p!=null)
            {
                action(p.Data);
                p = p.Next;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GenericList<int> intList = new GenericList<int>();
            int sum = 0;
            intList.Add(1);
            intList.Add(15);
            intList.Add(7);
            intList.Add(0);
            Console.WriteLine("遍历");
            intList.ForEach(m =>
            {
                Console.Write(m+" ");
            });
            Console.WriteLine("遍历结束");
            int max = intList.Head.Data;
            int min = intList.Head.Data;
            intList.ForEach(m =>
            {
                if(m>max)
                {
                    max = m;
                }
            });
            Console.WriteLine("max=" + max);
            intList.ForEach(m =>
            {
                if (m <min)
                {
                    min = m;
                }
            });
            Console.WriteLine("min=" + min);
            intList.ForEach(m =>
            {
                sum += m;
            });
            Console.WriteLine("sum=" + sum);
            Console.ReadLine();


        }
    }
}
