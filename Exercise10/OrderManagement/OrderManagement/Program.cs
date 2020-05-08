using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
//部分地方使方法返回布尔值判定操作是否成功
//具体控制台测试样例没有完全卸除，但我已测试过所有的功能，在program中按照返回值稍加判定条件后均为正常运行
//代码量不到300行，故没有将类单独分开。
namespace OrderManagement
{
    [Serializable]
    [XmlRoot("order")]
    public class Order:IComparable
    {
        [XmlElement(ElementName = "num")]
        public int num;
        [XmlElement(ElementName = "buyer")]
        public string buyer;
        [XmlElement(ElementName = "addr")]
        public string addr;
        [XmlElement(ElementName = "date")]
        public string date = DateTime.Now.ToString();
        [XmlElement(ElementName = "sum")]
        public int sum=0;
        [Key,Column(Order =1)]
        public int OrderId 
        {
            get
            {
                return num;
            }
            set
            {
                num = value;
            }
        }
        [Required]
        public string Buyer
        {
            get
            {
                return buyer;
            }
            set
            {
                buyer = value;
            }
        }
        public string Address
        {
            get
            {
                return addr;
            }
            set
            {
                addr = value;
            }
        }
        public int Sum
        {
            get
            {
                return sum;
            }
            set
            {
                sum = value;
            }
        }
        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }
        public List<OrderItem> OrderItems { get; set; }
        [XmlElement(ElementName = "orderItems")]
        public List<OrderItem> orderItems=new List<OrderItem>();
       
        public Order() { }
        public Order(int n,string o,string a)
        {
            num = n;
            buyer = o;
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
    public class OrderService
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
        public void Export(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                XmlSerializer xS = new XmlSerializer(typeof(List<Order>));
                xS.Serialize(fs,orders);
               
            }
        }
        public void Import(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                XmlSerializer xS = new XmlSerializer(typeof(List<Order>));
                List<Order> file_order = (List<Order>)xS.Deserialize(fs);
                foreach(var o in file_order)
                {
                    this.AddOrder(o); Console.Write(o.ToString());
                }
               

            }
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
    [XmlRoot("dictionary")]
    public class MyDictionary<TKey, TValue>

         : Dictionary<TKey, TValue>, IXmlSerializable

    {

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()

        {

            return null;

        }



        public void ReadXml(System.Xml.XmlReader reader)

        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
            {
                Console.WriteLine("wasEmpty");
                return;
            }
            Console.WriteLine("wasNotEmpty");
            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)

            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                this[key] = value;
                reader.ReadEndElement();
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }
        public void WriteXml(System.Xml.XmlWriter writer)

        {

            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));

            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            foreach (TKey key in this.Keys)

            {

                writer.WriteStartElement("item");



                writer.WriteStartElement("key");

                keySerializer.Serialize(writer, key);

                writer.WriteEndElement();



                writer.WriteStartElement("value");

                TValue value = this[key];

                valueSerializer.Serialize(writer, value);

                writer.WriteEndElement();



                writer.WriteEndElement();

            }

        }
        #endregion
    }
    [Serializable]
    public class OrderItem
    {
        public OrderItem() { }
        public MyDictionary<String, String> dic = new MyDictionary<string, string>();
        public int OrderItemId { get; set; }//自动识别为主键
        [Required]
        public string price
        {
            set
            {
                dic["price"] = value;
            }
            get
            {
                return dic["price"];
            }
        }
        [Required]
        public string name
        {
            set
            {
                dic["name"] = value;
            }
            get
            {
                return dic["name"];
            }
        }
        public int OrderId { get; set; }
        public Order order { get; set; }
        public OrderItem(MyDictionary<string,string> d)
        {
            dic = d;
        }

        public override bool Equals(object obj)
        {
            var item = obj as OrderItem;
            return item != null &&
                   EqualityComparer<MyDictionary<string, string>>.Default.Equals(dic, item.dic);
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
            return 149728143 + EqualityComparer<MyDictionary<string, string>>.Default.GetHashCode(dic);
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
    [TestClass]
    public class Test
    {
        public MyDictionary<string, string> dic1;
        public MyDictionary<string, string> dic2;
        public MyDictionary<string, string> dic3;
        public OrderItem item1;
        public OrderItem item2;
        public OrderItem item3;
        List<OrderItem> items1 = new List<OrderItem>();
        List<OrderItem> items2 = new List<OrderItem>();
        Order order1 = new Order(123456, "ca", "ar");
        Order order2 = new Order(654321, "cb", "br");
        Order order3 = new Order(534321, "cb", "br");
        OrderService orderService=new OrderService();
        public Test()
        {
            dic1=new MyDictionary<string, string>();
            dic1["price"] = "1000";
            dic1["name"] = "abc";
            dic2 = new MyDictionary<string, string>();
            dic2["price"] = "2000";
            dic2["name"] = "cav";
            dic3 = new MyDictionary<string, string>();
            dic3["price"] = "500";
            dic3["name"] = "ghf";
            item3 = new OrderItem(dic3);
            item2 = new OrderItem(dic2);
            item1 = new OrderItem(dic1);
            order1.AddItem(item1);
            order1.AddItem(item2);
            order2.AddItem(item2);
            order2.AddItem(item3);
            order3.AddItem(item1);
            order3.AddItem(item3);
        }
        [TestMethod]
        public void AddTest()
        {
            orderService.AddOrder(order1);
            Assert.AreEqual(orderService.orders[0], order1);
        }
        [TestMethod]
        public void DelTest()
        {
            orderService.orders.Add(order1);
            orderService.orders.Add(order2);
            orderService.orders.Add(order3);
            orderService.Del(654321);
            Assert.AreEqual(orderService.orders[1],order3);
            Assert.AreEqual(orderService.orders[0],order1);
        }
        [TestMethod]
        public void ModifyTest()
        {
            Order order = new Order(123456, "new ca", "new ar");
            orderService.orders.Add(order1);
            orderService.Modify(123456, order);
            foreach(var o in orderService.orders)
            {
                if(o.num==123456)
                {
                    Assert.AreEqual(o, order);
                    Assert.AreNotEqual(o, order1);
                    break;
                }
            }
        }
        [TestMethod]
        public void SearchTest()
        {
            orderService.orders.Add(order1);
            string s = orderService.Search(0, "ar");
            Assert.AreEqual(s.Contains("订单1"),true);
         }
        [TestMethod]
        public void Export_and_Import_Test()
        {
        orderService.orders.Add(order1);
            orderService.Export("test2.xml");
            orderService.orders.Remove(order1);
            orderService.Import("test2.xml");
            Assert.AreEqual(orderService.orders[0].ToString(),order1.ToString());
        }
    }
   class Program
    {
        static void Main(string[] args)
        {
            int newOrderId = 0;
            int newOrderItemId = 0;
            using (var context = new OrderContext())
            {
                var order = new Order { Buyer="cjk" };
                order.OrderItems = new List<OrderItem>() {
              new OrderItem() { name = "Test1", price = "Hello"},
              new OrderItem() { name = "Test2", price = "Hello"}
            };
                context.Orders.Add(order);
                context.SaveChanges();
                newOrderId = order.OrderId;
            }
            //Console.WriteLine(newOrderId);
            using (var context = new OrderContext())
            {
                var orderItem = new OrderItem()
                {
                    name = "Test3",
                    price = "Hell0",
                    OrderItemId = newOrderItemId
                };
                context.Entry(orderItem).State = EntityState.Added;
                context.SaveChanges();
                newOrderId = orderItem.OrderId;
            }

            using (var context = new OrderContext())
            {
                var order = context.Orders
                    .SingleOrDefault(o => o.OrderId == newOrderId);
                if (order != null) Console.WriteLine(order.Buyer);
            }


            using (var context = new OrderContext())
            {
                var query = context.Orders.Include("Orders")
                    .Where(o => o.Buyer=="cjk")
                    .OrderBy(o => o.OrderId);
                foreach (var o in query)
                {
                    Console.WriteLine(o.OrderId);
                }
            }
            using (var context = new OrderContext())
            {
                var query = context.OrderItems
                    .Where(p => p.order.Buyer == "cjk")
                    .OrderBy(p => p.OrderItemId);
                foreach (var p in query)
                {
                    Console.WriteLine(p.name);
                }
            }

            using (var context = new OrderContext())
            {
                var orderitem = new OrderItem()
                {
                    OrderId = newOrderId,
                    name = "Test3",
                    price = "Hello world",
                    OrderItemId = newOrderItemId,
                    //Comment = "just a test！"
                };
                context.Entry(orderitem).State = EntityState.Modified;
                context.SaveChanges();
            }


            using (var context = new OrderContext())
            {
                var orderitem = context.OrderItems.FirstOrDefault(p => p.OrderId == newOrderId);
                if (orderitem != null)
                {
                    orderitem.name = "Hello world,EF!";
                    orderitem.price = "EF test！";
                    context.SaveChanges();
                }
            }

            using (var context = new OrderContext())
            {
                // add
                context.Orders.Add(new Order { Buyer = "http://example.com/blog_one" });
                context.Orders.Add(new Order { Buyer = "http://example.com/blog_two" });
                // update
                var firstBlog = context.Orders.FirstOrDefault(o => o.OrderId == newOrderId);
                if (firstBlog != null) firstBlog.Buyer = "http://example.com/blog_three";
                context.SaveChanges();
            }


            using (var context = new OrderContext())
            {
                var order = context.Orders.FirstOrDefault(p => p.OrderId == newOrderId);
                if (order != null)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
            }

            using (var context = new OrderContext())
            {
                var order = context.Orders.Include(o => o.OrderItems).FirstOrDefault(p => p.OrderId == newOrderId);
                if (order != null)
                {
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
            }


            Console.ReadLine();
        }
    }
}
