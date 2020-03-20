using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//部分地方使方法返回布尔值判定操作是否成功
//具体控制台测试样例没有完全卸除，但我已测试过所有的功能，在program中按照返回值稍加判定条件后均为正常运行
//代码量不到300行，故没有将类单独分开。
namespace OrderManagement
{

    class Order:IComparable
    {
        public int num;
        public string buyer;
        public string addr;
        public string date = DateTime.Now.ToString();
        public int sum=0;
        public List<OrderItem> orderItems=new List<OrderItem>();
        public Order(int n,string b,string a)
        {
            num = n;
            buyer = b;
            addr = a;
            foreach(OrderItem item in orderItems)
            {
                sum+=int.Parse(item.GetValue("price"));
            }
        }
        public bool AddItem(OrderItem i)
        {
            foreach(OrderItem item in orderItems)
            {
                if(item.Equals(i))
                    return false;
            }
            orderItems.Add(i);
            return true;
            
        }
        public int CompareTo(object obj)
        {
            Order o = obj as Order;
            if (o == null)
            {
                throw new System.ArgumentException();
            }
            return this.num.CompareTo(o.num);

        }

        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return order != null &&
                   num == order.num &&
                   buyer == order.buyer &&
                   addr == order.addr &&
                   date == order.date &&
                   EqualityComparer<List<OrderItem>>.Default.Equals(orderItems, order.orderItems);
        }

        public override int GetHashCode()
        {
            var hashCode = 190345214;
            hashCode = hashCode * -1521134295 + num.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(buyer);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(addr);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(date);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<OrderItem>>.Default.GetHashCode(orderItems);
            return hashCode;
        }

        public override string ToString()
        {
            string res="";
            foreach(OrderItem item in orderItems)
            {
                res += item.ToString()+";";
            }
            return $"{{ num:{num},buyer:{buyer},addr:{addr},date:{date},sum:{sum},orderItems:{{ {res} }}}}";
        }

    }
    class OrderService
    {
        public List<Order> orders = new List<Order>();
        public bool AddOrder(Order o)
        {
        
            foreach (Order or in orders)
            {
                if (or.Equals(o))
                    return false;
            }         
            orders.Add(o);
            orders.Sort();
            return true;
        }
        public bool Del(int num)
        {
            foreach (Order o in orders)
            {
                if (num == o.num)
                {
                    orders.Remove(o);
                    return true;
                }
            }
            return false;
        }
        public bool Modify(int num, Order new_o)
        {
            foreach (Order o in orders)
            {
                if (num == o.num)
                {
                    orders.Remove(o);
                    orders.Add(new_o);
                    return true;
                }
            }
            return false;
        }
        public string Search(int key,string key_word)
        {
            string res = "";
            switch(key)
            {
                case 0:
                    {
                        var query = from order in orders
                                    where order.addr == key_word
                                    orderby order.sum
                                    select order;
                        List<Order> list = query.ToList();
                        int i = 1;
                        foreach (Order o in list)
                        {
                            res += "订单" + i +": " +o.ToString()+"  \n";
                            i++;
                        }
                        break;
                    }
                case 1:
                    {
                        var query = from order in orders
                                    where order.buyer == key_word
                                    orderby order.sum
                                    select order;
                        List<Order> list = query.ToList();
                        int i = 1;
                        foreach (Order o in list)
                        {
                            res += "订单" + i + ": " + o.ToString() + "  \n";
                            i++;
                        }
                        break;
                    }
                case 2:
                    {
                        var query = from order in orders
                                    where order.date == key_word
                                    orderby order.sum
                                    select order;
                        List<Order> list = query.ToList();
                        int i = 1;
                        foreach (Order o in list)
                        {
                            res += "订单" + i + ": " + o.ToString() + "  \n";
                            i++;
                        }
                        break;
                    }
                case 3:
                    {
                        var query = from order in orders
                                    where order.num.ToString() == key_word
                                    orderby order.sum
                                    select order;
                        int i = 1;
                        List<Order> list = query.ToList();
                        foreach (Order o in list)
                        {
                            res += "订单" + i + ": " + o.ToString() + "  \n";
                            i++;
                        }
                        break;
                    }
                default:
                    {
                        throw new Exception();
                    }
            }

            return res;
        }
 
        

    }
    class OrderItem
    {
        public Dictionary<String, String> dic = new Dictionary<string, string>();
        public OrderItem(Dictionary<string,string> d)
        {
            dic = d;
        }

        public override bool Equals(object obj)
        {
            var item = obj as OrderItem;
            return item != null &&
                   EqualityComparer<Dictionary<string, string>>.Default.Equals(dic, item.dic);
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach(string key in dic.Keys)
            {
                yield return key;
            }
        }

        public override int GetHashCode()
        {
            return 149728143 + EqualityComparer<Dictionary<string, string>>.Default.GetHashCode(dic);
        }

        public string  GetValue(string key)
        {
            return dic[key];
        }

        public override string ToString()
        {
            String res = "";
            foreach(string key in dic.Keys)
            {
                res += $"{{ {key}:{dic[key]}}},";
            }
            return res;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dic1=new Dictionary<string, string>();
            dic1["price"] = "1000";
            dic1["name"] = "abc";
            Dictionary<string, string> dic2 = new Dictionary<string, string>();
            dic2["price"] = "2000";
            dic2["name"] = "cav";
            Dictionary<string, string> dic3 = new Dictionary<string, string>();
            dic3["price"] = "500";
            dic3["name"] = "ghf";
            OrderItem item1 = new OrderItem(dic1);
            OrderItem item2 = new OrderItem(dic2);
            OrderItem item3 = new OrderItem(dic3);
            List<OrderItem> items1=new List<OrderItem>();
            List<OrderItem> items2 = new List<OrderItem>();
            Order order1 = new Order(123456, "ca", "ar");
            order1.AddItem(item1);
            order1.AddItem(item2);
            Order order2 = new Order(654321, "cb", "br");
            order2.AddItem(item2);
            order2.AddItem(item3);
            Order order3 = new Order(534321, "cb", "br");
            order3.AddItem(item1);
            order3.AddItem(item3);
            OrderService orderService=new OrderService();
            orderService.AddOrder(order2);
            orderService.AddOrder(order1);
            orderService.AddOrder(order3);
            try
            {
                string res;
                //orderService.Del(654321);
                res = orderService.Search(0, "br");
                if (res != "")
                    Console.WriteLine(res);
                else
                    Console.WriteLine("not found");
            }
            catch(Exception e)
            {
                Console.WriteLine("Something wrong");
            }
            Console.ReadLine();
        }
    }
}
