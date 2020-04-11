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
    public partial class Form2 : Form
    {
        private Order NewOrder=new Order();
        OrderService orderService;
        public event TransfDelegate TransfEvent;
        public Form2(ref OrderService orderService)
        {
            this.orderService = orderService;
            InitializeComponent();
        }
        public Form2(ref OrderService orderService,Order OldOrder){
            this.orderService = orderService;
            NewOrder.addr = OldOrder.addr;
            NewOrder.buyer = OldOrder.buyer;
            NewOrder.num = OldOrder.num;
            NewOrder.sum = OldOrder.sum;
            NewOrder.date = OldOrder.date;
            NewOrder.orderItems = OldOrder.orderItems;
            InitializeComponent();
            bindingSource1.DataSource = NewOrder.orderItems;
            this.input_addr.Text = OldOrder.addr;
            this.input_buyer.Text = OldOrder.buyer;
            this.input_num.Text = OldOrder.num.ToString();
            btn_create.Text = "confirm";
        }
        public Form2(){
            InitializeComponent();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            NewOrder.addr = this.input_addr.Text;
            NewOrder.buyer = this.input_buyer.Text;
            NewOrder.num = int.Parse(this.input_num.Text);
            bindingSource1.DataSource = NewOrder.orderItems;

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(ref NewOrder);
            form3.TransfEvent += frm_TransfEvent;
            form3.ShowDialog();

        }
        void frm_TransfEvent()
        {
            bindingSource1.ResetBindings(false);
            bindingSource1.DataSource = NewOrder.orderItems;
        }
        private void btn_submit_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Exit");
            Console.WriteLine("edit result:" + NewOrder.ToString());
            orderService.AddOrder(NewOrder);
            TransfEvent();
            this.Close();
        }
    }
}
