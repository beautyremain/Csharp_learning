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
    //{ Num:0,buyer:ca,addr:ar,date:2020-03-27 10:39:28,sum:0,orderItems:{ { price:1000},{ name:abc},;{ price:2000},{ name:cav},; }}{ Num:0,buyer:cb,addr:br,date:2020-03-27 10:39:28,sum:0,orderItems:{ { price:1000},{ name:abc},;{ price:500},{ name:ghf},; }}{ Num:0,buyer:cb,addr:br,date:2020-03-27 10:39:28,sum:0,orderItems:{ { price:2000},{ name:cav},;{ price:500},{ name:ghf},; }}asdasdas:      { price:1000},{ name:abc},
    public partial class Form1 : Form
    {
        private OrderService orderService;
        private string path = "test.xml";
        private string out_path = "test.xml";
        public Form1()
        {
            orderService = new OrderService();
            orderService.Import(this.path);

            //Console.WriteLine("auishduiahfuiahs:"+bindingSource1);       
            InitializeComponent();
            
            bindingSource1.DataSource = orderService.orders;
            Order current = bindingSource1.Current as Order;
            bindingSource2.DataSource = current.orderItems;


        }

        private void dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Order current = bindingSource1.Current as Order;
            bindingSource2.DataSource = current.orderItems;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("真的删除?", "确认删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // code here
                    
                Order current = bindingSource1.Current as Order;
                orderService.Del(current.num);
                //以下为关键重置代码
                bindingSource1.ResetBindings(false);
            }


        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认保存?", "保存", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                orderService.Export(this.out_path);
            }
        }
        void frm_TransfEvent()
        {
            bindingSource1.ResetBindings(false);
            bindingSource1.DataSource = orderService.orders;
            Console.WriteLine("form1 recieved form2");
        }
        void frm_TransfEvent2()
        {
            Order current = bindingSource1.Current as Order;
            orderService.Del(current.num);
            //以下为关键重置代码
            bindingSource1.ResetBindings(false);
            bindingSource1.DataSource = orderService.orders;
            Console.WriteLine("form1 recieved form2 trans2");
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(ref orderService);          
            form2.TransfEvent += frm_TransfEvent;
            form2.ShowDialog();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Order current = bindingSource1.Current as Order;
            Form2 form2 = new Form2(ref orderService,current);
            form2.TransfEvent += frm_TransfEvent2;
            form2.ShowDialog();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            Console.WriteLine("click");
            string currentName = "";
            string query = this.string_query.Text;
            if (query == "")
            {
                bindingSource1.DataSource = orderService.orders;
                return;
            }
            foreach (Control control in groupBox_radio.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton current = control as RadioButton;
                    if (current.Checked)
                    {
                        currentName = current.Text;
                        break;
                    }
                }
            }
            if (currentName == "")
            {
                Console.WriteLine("return");
                return;
            }
            Console.WriteLine(orderService.orders.Where(o => o.GetType().GetProperty(currentName).GetValue(o, null).ToString() == query).ToList().Count);
            //User u = new User();
            //u.Name = "lily";
            //var propName = "Name";
            //var propNameVal = u.GetType().GetProperty(propName).GetValue(u, null);

            //Console.WriteLine(propNameVal);// "lily"
            bindingSource1.DataSource = orderService.orders.Where(o => o.GetType().GetProperty(currentName).GetValue(o,null).ToString() == query).ToList();
        }
    }
}
