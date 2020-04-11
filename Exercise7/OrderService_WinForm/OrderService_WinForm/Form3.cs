using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrderManagement;
namespace OrderService_WinForm
{
    public delegate void TransfDelegate();
    public partial class Form3 : Form
    {
        Order order;
        public Form3(ref Order order)
        {
            this.order = order;
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public event TransfDelegate TransfEvent;
        private void btn_add_Click(object sender, EventArgs e)
        {
            MyDictionary<string, string> dic = new MyDictionary<string, string>();
            string name = this.input_name.Text;
            string price = this.input_price.Text;
            dic["name"] = name;
            dic["price"] = price;
            OrderItem item = new OrderItem(dic);
            order.AddItem(item);
            TransfEvent();
            this.Close();
        }
    }
}
