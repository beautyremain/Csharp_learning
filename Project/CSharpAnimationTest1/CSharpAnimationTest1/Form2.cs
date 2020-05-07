using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpAnimationTest1
{

    public partial class Form2 : Form
    {
        public Basic basic;
        List<ListItem> items = new List<ListItem>();
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        List<AngleCollection> historys = new List<AngleCollection>();
        public Form2()
        {
            InitializeComponent();
            //button2.Click += button1_Click;
            basic = new Basic();
            for(int i=0;i<5;i++)
            {
                historys.Add(new AngleCollection(0, 0, 0));
            }
            foreach (Control control in this.Controls)
            {
                if(control is PictureBox)
                {
                    pictureBoxes.Add((PictureBox)control);

                }
            }

            items.Add(new ListItem("0", "P0"));
            items.Add(new ListItem("1", "P1"));
            items.Add(new ListItem("2", "P2"));
            items.Add(new ListItem("3", "P3"));
            items.Add(new ListItem("4", "P4"));
            //items.Add(new ListItem("5", "Item_5_Text"));
            comboBox1.DisplayMember = "Text";        //显示
            comboBox1.ValueMember = "Value";        //值
            comboBox1.DataSource = items;        //绑定数据
            label_AM.Text = basic.available.A.ToString();
            label_BM.Text = basic.available.B.ToString();
            label_CM.Text = basic.available.C.ToString();
            trackBarA.Maximum = basic.available.A;
            trackBarB.Maximum = basic.available.B;
            trackBarC.Maximum = basic.available.C;
            init_draw();
        }
        private static void activePieEvent(Graphics graphics, SolidBrush solidBrush, SolidBrush solidBrush2, Rectangle rectangle, float angle1, float angle2, Timer timer, ref int Tick)
        {
            //Console.WriteLine("被调用");
            if (Tick > angle2)
            {
                timer.Stop();
                //Console.WriteLine("Finish");
                return;
            }
            if (Tick < 270-angle1)
                graphics.FillPie(solidBrush, rectangle, angle1 + Tick, angle2 > Tick + 5 ? 5 : angle2 - Tick);
            else
                graphics.FillPie(solidBrush2, rectangle, angle1 + Tick, angle2 > Tick + 5 ? 5 : angle2 - Tick);
            Tick += 5;
        }
        public static void activeDrawPie(Graphics graphics, SolidBrush solidBrush, SolidBrush solidBrush2, Rectangle rectangle, float angle1, float angle2, int interval)
        {
            Timer timer = new Timer();
            timer.Interval = interval;
            int Tick = 0;
            timer.Tick += new EventHandler((o, e) => activePieEvent(graphics, solidBrush, solidBrush2, rectangle, angle1, angle2, timer, ref Tick));
            timer.Start();
        }
        public void init_draw()
        {
            SolidBrush fix_brush = new SolidBrush(Color.LightGreen);
            SolidBrush change_brush = new SolidBrush(Color.LightSalmon);
            SolidBrush warn_brush = new SolidBrush(Color.Red);
            Rectangle rectangle1 = new Rectangle(10, 10, 100, 100);
            Rectangle rectangle2 = new Rectangle(150, 10, 100, 100);
            Rectangle rectangle3 = new Rectangle(290, 10, 100, 100);
            for (int i = 0; i < 5; i++)
            {
                Resourse Max = basic.ProcessList[i].Max;
                Resourse Allocation = basic.ProcessList[i].Allocation;

                //Graphics g = pictureBoxes[4 - i].CreateGraphics();
                Bitmap img = new Bitmap(pictureBoxes[i].Width, pictureBoxes[i].Height);
                pictureBoxes[ i].Image = img;
                Graphics g = Graphics.FromImage(img);
                g.Clear(Color.White);
                //Graphics g = pictureBox.CreateGraphics();
                g.FillPie(fix_brush, rectangle1, 0, 360);
                if(Max.A==0)
                    g.FillPie(change_brush, rectangle1, 0, 360);
                g.DrawString(Allocation.A.ToString()+"/"+Max.A.ToString(), new Font("等线", 14), new SolidBrush(Color.Black), rectangle1.X+30, 115);
                g.FillPie(fix_brush, rectangle2, 0, 360);
                if (Max.B == 0)
                    g.FillPie(change_brush, rectangle2, 0, 360);
                g.DrawString(Allocation.B.ToString() + "/" + Max.B.ToString(), new Font("等线", 14), new SolidBrush(Color.Black), rectangle2.X + 30, 115);
                g.FillPie(fix_brush, rectangle3, 0, 360);
                if (Max.C == 0)
                    g.FillPie(change_brush, rectangle3, 0, 360);
                g.DrawString(Allocation.C.ToString() + "/" + Max.C.ToString(), new Font("等线", 14), new SolidBrush(Color.Black), rectangle3.X + 30, 115);


            }
        }
            private void button1_Click(object sender, EventArgs e)
        {
            basic.printPro(basic.ProcessList, basic.available);
            SolidBrush fix_brush = new SolidBrush(Color.LightGreen);
            SolidBrush change_brush = new SolidBrush(Color.LightSalmon);
            SolidBrush warn_brush = new SolidBrush(Color.Red);
            Rectangle rectangle1 = new Rectangle(10, 10, 100, 100);
            Rectangle rectangle2 = new Rectangle(150, 10, 100, 100);
            Rectangle rectangle3 = new Rectangle(290, 10, 100, 100);
            for (int i = 0; i < 5; i++)
            {
                AngleCollection history = historys[i];
                Resourse Max=basic.ProcessList[i].Max;
                Resourse Allocation = basic.ProcessList[i].Allocation;
                float angleA = Allocation.A * 360 / (Max.A == 0 ? 1 : Max.A);
                float angleB = Allocation.B * 360 / (Max.B == 0 ? 1 : Max.B);
                float angleC = Allocation.C * 360 / (Max.C == 0 ? 1 : Max.C);
                //this.listBox1.Items.Add(angleC);
                Graphics g=pictureBoxes[i].CreateGraphics();
                //Bitmap img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                //pictureBoxes[4 - i].Image = img;
                //Graphics g = Graphics.FromImage(img);
                //pictureBoxes[4 - i].Image= getBitMapFile(pictureBox1.Width, pictureBox1.Height);
               // g.FillPie(fix_brush, rectangle1, 0, 360);
                activeDrawPie(g,change_brush,warn_brush, rectangle1, -90+history.A, angleA - history.A,100);
                g.FillRectangle(new SolidBrush(Color.White), rectangle1.X, 115, 100, 20);
                g.DrawString(Allocation.A.ToString() + "/" + Max.A.ToString(), new Font("等线", 14), new SolidBrush(Color.Black), rectangle1.X + 30, 115);
                //g.FillPie(fix_brush, rectangle2, 0, 360);
                activeDrawPie(g, change_brush, warn_brush, rectangle2, -90+history.B, angleB - history.B, 100);
                g.FillRectangle(new SolidBrush(Color.White), rectangle2.X, 115, 100, 20);
                g.DrawString(Allocation.B.ToString() + "/" + Max.B.ToString(), new Font("等线", 14), new SolidBrush(Color.Black), rectangle2.X + 30, 115);
                //g.FillPie(fix_brush, rectangle3, 0, 360);
                activeDrawPie(g, change_brush, warn_brush, rectangle3, -90+history.C, angleC - history.C,100);
                g.FillRectangle(new SolidBrush(Color.White), rectangle3.X, 115, 100, 20);
                g.DrawString(Allocation.C.ToString() + "/" + Max.C.ToString(), new Font("等线", 14), new SolidBrush(Color.Black), rectangle3.X + 30, 115);
                history.A = angleA;
                history.B = angleB;
                history.C = angleC;


            }
              //panels[3].CreateGraphics().DrawLine(new Pen(Color.Blue,10), 1, 1, 100, 100);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(basic.addRequest(trackBarA.Value,trackBarB.Value,trackBarC.Value, comboBox1.SelectedItem.ToString()))
            {
                trackBarA.Maximum = basic.available.A;
                trackBarB.Maximum = basic.available.B;
                trackBarC.Maximum = basic.available.C;
                label_AM.Text = basic.available.A.ToString();
                label_BM.Text = basic.available.B.ToString();
                label_CM.Text = basic.available.C.ToString();
                button1_Click(sender, e);
            }
            else
            {
                MessageBox.Show("请重新分配资源","分配失败");
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label_Bval.Text=trackBarB.Value.ToString();
        }

        private void trackBarA_ValueChanged(object sender, EventArgs e)
        {
            label_Aval.Text = trackBarA.Value.ToString();
        }

        private void trackBarC_ValueChanged(object sender, EventArgs e)
        {
            label_Cval.Text = trackBarC.Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
              //listBox1.Items.Add( comboBox1.SelectedItem.ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is TrackBar)
                {
                    TrackBar tb = (TrackBar)control;
                    tb.Value = 0;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach(var pb in pictureBoxes)
            {
                listBox1.Items.Add(pb.Name);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            basic.addRequest(1, 1, 1, "P4");
            basic.addRequest(1, 1, 1, "P0");
            Form3 form3 = new Form3(this.basic,new Resourse(trackBarA.Value, trackBarB.Value, trackBarC.Value), comboBox1.SelectedItem.ToString());
            form3.Show();
        }
    }
    public class AngleCollection
    {
        public float A { get; set; }
        public float B { get; set; }
        public float C { get; set; }
        public AngleCollection(float a, float b, float c)
        {
            A = a;
            B = b;
            C = c;
        }
    }
}
