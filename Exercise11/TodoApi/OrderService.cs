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
namespace TodoApi
{
    [Serializable]
    [XmlRoot("order")]
    public class Order : IComparable
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
        public int sum = 0;
        [Key, Column(Order = 1)]
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
        public List<OrderItem> Items { get; set; }
        [XmlElement(ElementName = "orderItems")]
        public List<OrderItem> orderItems = new List<OrderItem>();

        public Order() { }
        public Order(int n, string o, string a)
        {
            num = n;
            buyer = o;
            addr = a;
            foreach (OrderItem item in orderItems)
            {
                sum += int.Parse(item.GetValue("price"));
            }
        }
        public bool AddItem(OrderItem i)
        {
            foreach (OrderItem item in orderItems)
            {
                if (item.Equals(i))
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
            string res = "";
            foreach (OrderItem item in orderItems)
            {
                res += item.ToString() + ";";
            }
            return $"{{ num:{num},buyer:{buyer},addr:{addr},date:{date},sum:{sum},orderItems:{{ {res} }}}}";
        }

    }
    public class OrderService
    {
        // private static IQueryable<Order> AllOrders(OrderContext db)
        // {
        //     return db.Orders.Include(o => o.Items.Select(i => i));
        // }
        // public static List<Order> GetAllOrders()
        // {
        //     using (var db = new OrderContext())
        //     {
        //         return AllOrders(db).ToList();
        //     }
        // }

        // public static Order GetOrder(string id)
        // {
        //     using (var db = new OrderContext())
        //     {
        //         return AllOrders(db).FirstOrDefault(o => o.OrderId.ToString() == id);
        //     }
        // }

        // public static Order AddOrder(Order order)
        // {
        //     try
        //     {
        //         using (var db = new OrderContext())
        //         {
        //             db.Orders.Add(order);
        //             db.SaveChanges();
        //         }
        //         return order;
        //     }
        //     catch (Exception e)
        //     {
        //         //TODO 需要更加错误类型返回不同错误信息
        //         throw new ApplicationException($"添加错误: {e.Message}");
        //     }
        // }

        // public static void RemoveOrder(string id)
        // {
        //     try
        //     {
        //         using (var db = new OrderContext())
        //         {
        //             var order = db.Orders.Include("Items").Where(o => o.OrderId.ToString() == id).FirstOrDefault();
        //             db.Orders.Remove(order);
        //             db.SaveChanges();
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         //TODO 需要更加错误类型返回不同错误信息
        //         throw new ApplicationException($"删除订单错误!");
        //     }
        // }

        // public static void UpdateOrder(Order newOrder)
        // {
        //     RemoveItems(newOrder.OrderId.ToString());
        //     using (var db = new OrderContext())
        //     {
        //         db.Entry(newOrder).State = EntityState.Modified;
        //         db.OrderItems.AddRange(newOrder.Items);
        //         db.SaveChanges();
        //     }
        // }

        // private static void RemoveItems(string orderId)
        // {
        //     using (var db = new OrderContext())
        //     {
        //         var oldItems = db.OrderItems.Where(item => item.OrderId.ToString() == orderId);
        //         db.OrderItems.RemoveRange(oldItems);
        //         db.SaveChanges();
        //     }
        // }

        // public static void Export(String fileName)
        // {
        //     XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
        //     using (FileStream fs = new FileStream(fileName, FileMode.Create))
        //     {
        //         xs.Serialize(fs, GetAllOrders());
        //     }
        // }

        // public static void Import(string path)
        // {
        //     XmlSerializer xs = new XmlSerializer(typeof(List<Order>));
        //     using (FileStream fs = new FileStream(path, FileMode.Open))
        //     {
        //         List<Order> temp = (List<Order>)xs.Deserialize(fs);
        //         temp.ForEach(order => {
        //             try
        //             {
        //                 AddOrder(order);
        //             }
        //             catch
        //             {
        //                 //ignore errors
        //             }
        //         });
        //     }
        // }
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
        public OrderItem(MyDictionary<string, string> d)
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
            foreach (string key in dic.Keys)
            {
                yield return key;
            }
        }

        public override int GetHashCode()
        {
            return 149728143 + EqualityComparer<MyDictionary<string, string>>.Default.GetHashCode(dic);
        }

        public string GetValue(string key)
        {
            return dic[key];
        }

        public override string ToString()
        {
            String res = "";
            foreach (string key in dic.Keys)
            {
                res += $"{{ {key}:{dic[key]}}},";
            }
            return res;
        }
    }
   
    class myProgram
    {
        static void myProgramRun()
        {
            // int newOrderId = 0;
            // int newOrderItemId = 0;
            // using (var context = new OrderContext())
            // {
            //     var order = new Order { Buyer = "cjk" };
            //     order.Items = new List<OrderItem>() {
            //   new OrderItem() { name = "Test1", price = "Hello"},
            //   new OrderItem() { name = "Test2", price = "Hello"}
            // };
            //     context.Orders.Add(order);
            //     context.SaveChanges();
            //     newOrderId = order.OrderId;
            // }
            // Console.WriteLine(newOrderId);
            // using (var context = new OrderContext())
            // {
            //     var orderItem = new OrderItem()
            //     {
            //         name = "Test3",
            //         price = "Hell0",
            //         OrderItemId = newOrderItemId
            //     };
            //     context.Entry(orderItem).State = EntityState.Added;
            //     context.SaveChanges();
            //     newOrderId = orderItem.OrderId;
            // }

            // using (var context = new OrderContext())
            // {
            //     var order = context.Orders
            //         .SingleOrDefault(o => o.OrderId == newOrderId);
            //     if (order != null) Console.WriteLine(order.Buyer);
            // }


            // using (var context = new OrderContext())
            // {
            //     var query = context.Orders.Include("Orders")
            //         .Where(o => o.Buyer == "cjk")
            //         .OrderBy(o => o.OrderId);
            //     foreach (var o in query)
            //     {
            //         Console.WriteLine(o.OrderId);
            //     }
            // }
            // using (var context = new OrderContext())
            // {
            //     var query = context.OrderItems
            //         .Where(p => p.order.Buyer == "cjk")
            //         .OrderBy(p => p.OrderItemId);
            //     foreach (var p in query)
            //     {
            //         Console.WriteLine(p.name);
            //     }
            // }

            // using (var context = new OrderContext())
            // {
            //     var orderitem = new OrderItem()
            //     {
            //         OrderId = newOrderId,
            //         name = "Test3",
            //         price = "Hello world",
            //         OrderItemId = newOrderItemId,
            //         //Comment = "just a test！"
            //     };
            //     context.Entry(orderitem).State = EntityState.Modified;
            //     context.SaveChanges();
            // }


            // using (var context = new OrderContext())
            // {
            //     var orderitem = context.OrderItems.FirstOrDefault(p => p.OrderId == newOrderId);
            //     if (orderitem != null)
            //     {
            //         orderitem.name = "Hello world,EF!";
            //         orderitem.price = "EF test！";
            //         context.SaveChanges();
            //     }
            // }

            // using (var context = new OrderContext())
            // {
            //     // add
            //     context.Orders.Add(new Order { Buyer = "http://example.com/blog_one" });
            //     context.Orders.Add(new Order { Buyer = "http://example.com/blog_two" });
            //     // update
            //     var firstBlog = context.Orders.FirstOrDefault(o => o.OrderId == newOrderId);
            //     if (firstBlog != null) firstBlog.Buyer = "http://example.com/blog_three";
            //     context.SaveChanges();
            // }


            // using (var context = new OrderContext())
            // {
            //     var order = context.Orders.FirstOrDefault(p => p.OrderId == newOrderId);
            //     if (order != null)
            //     {
            //         context.Orders.Remove(order);
            //         context.SaveChanges();
            //     }
            // }

            // using (var context = new OrderContext())
            // {
            //     var order = context.Orders.Include(o => o.Items).FirstOrDefault(p => p.OrderId == newOrderId);
            //     if (order != null)
            //     {
            //         context.Orders.Remove(order);
            //         context.SaveChanges();
            //     }
            // }


            Console.ReadLine();
        }
    }
}
