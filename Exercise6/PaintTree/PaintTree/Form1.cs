using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaintTree
{
    public partial class Form1 : Form
    {
        System.Drawing.Color lineColor;
        public Form1()
        {
            InitializeComponent();

        }
        
        private Graphics graphics;
        double th1 = 30 * Math.PI / 180;
        double th2 = 20 * Math.PI / 180;
        double per1 = 0.6;
        double per2 = 0.7;
        int dep = 10;
        double l;
        private void button1_Click(object sender, EventArgs e)
        {
            if (graphics == null) graphics = this.panel.CreateGraphics();
            graphics.Clear(panel.BackColor);
            if(!double.TryParse(left_th.Text,out th1))
            {
                MessageBox.Show("左角度输入有误");
                return;
            }
            if (!double.TryParse(right_th.Text, out th2))
            {
                MessageBox.Show("右角度输入有误");
                return;
            }
            if (!double.TryParse(left_per.Text, out per1))
            {
                MessageBox.Show("左分支长度比输入有误");
                return;
            }
            if(per1>1)
            {
                MessageBox.Show("左分支长度比不能大于1");
                return;
            }
            if (!double.TryParse(right_per.Text, out per2))
            {
                MessageBox.Show("右分支长度比输入有误");
                return;
            }
            if (per2 > 1)
            {
                MessageBox.Show("右分支长度比不能大于1");
                return;
            }
            if(!double.TryParse(leng.Text,out l))
            {
                MessageBox.Show("主干长度有误");
            }
            th1 = th1 * Math.PI / 180;
            th2 = th2 * Math.PI / 180;
            drawCayleyTree(dep, 300, 500, l, -Math.PI / 2);
        }
        void drawCayleyTree(int n,double x0,double y0,double leng,double th)
        {
            if (n == 0)
                return;
            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);
            drawLine(x0, y0, x1, y1);

            drawCayleyTree(n - 1, x1, y1, per2 * leng , th + th1);
            drawCayleyTree(n - 1, x1, y1,per1 * leng, th - th2);

        }
        void drawLine(double x0,double y0,double x1,double y1)
        {
            lineColor = color.BackColor;
            Pen p = new Pen(lineColor);
            graphics.DrawLine(p, (int)x0, (int)y0, (int)x1, (int)y1);
        }

        private void select_color_Click(object sender, EventArgs e)
        {
            ColorDialog ColorForm = new ColorDialog();
            if (ColorForm.ShowDialog() == DialogResult.OK)
            {
                Color GetColor = ColorForm.Color;
                color.BackColor = GetColor;
                String c = System.Drawing.ColorTranslator.ToHtml(GetColor);
                color.Text = c;
            }
        }

        private void plus1_Click(object sender, EventArgs e)
        {
            if(int.TryParse(depth.Text,out dep))
            {
                dep++;
                depth.Text = dep.ToString();
            }
            else
            {
                MessageBox.Show("深度出现未知错误");
            }
        }

        private void minus1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(depth.Text, out dep))
            {
                if(dep>2)
                {
                    dep--;
                    depth.Text = dep.ToString();
                }
                else
                {
                    MessageBox.Show("深度不能小于2");
                }
            }
            else
            {
                MessageBox.Show("深度出现未知错误");
            }
        }
    }
}
