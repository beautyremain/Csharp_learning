namespace PaintTree
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.depth_text = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.select_color = new System.Windows.Forms.Button();
            this.color = new System.Windows.Forms.Label();
            this.color_text = new System.Windows.Forms.Label();
            this.leng = new System.Windows.Forms.TextBox();
            this.leng_text = new System.Windows.Forms.Label();
            this.right_th = new System.Windows.Forms.TextBox();
            this.th_text_right = new System.Windows.Forms.Label();
            this.left_th = new System.Windows.Forms.TextBox();
            this.th_text_left = new System.Windows.Forms.Label();
            this.right_per = new System.Windows.Forms.TextBox();
            this.right_text = new System.Windows.Forms.Label();
            this.left_per = new System.Windows.Forms.TextBox();
            this.left_text = new System.Windows.Forms.Label();
            this.depth = new System.Windows.Forms.Label();
            this.plus1 = new System.Windows.Forms.Button();
            this.minus1 = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(113, 290);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // depth_text
            // 
            this.depth_text.AutoSize = true;
            this.depth_text.Location = new System.Drawing.Point(42, 38);
            this.depth_text.Name = "depth_text";
            this.depth_text.Size = new System.Drawing.Size(41, 12);
            this.depth_text.TabIndex = 4;
            this.depth_text.Text = "深度：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.select_color);
            this.groupBox1.Controls.Add(this.color);
            this.groupBox1.Controls.Add(this.color_text);
            this.groupBox1.Controls.Add(this.leng);
            this.groupBox1.Controls.Add(this.leng_text);
            this.groupBox1.Controls.Add(this.right_th);
            this.groupBox1.Controls.Add(this.th_text_right);
            this.groupBox1.Controls.Add(this.left_th);
            this.groupBox1.Controls.Add(this.th_text_left);
            this.groupBox1.Controls.Add(this.right_per);
            this.groupBox1.Controls.Add(this.right_text);
            this.groupBox1.Controls.Add(this.left_per);
            this.groupBox1.Controls.Add(this.left_text);
            this.groupBox1.Controls.Add(this.depth);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.depth_text);
            this.groupBox1.Controls.Add(this.plus1);
            this.groupBox1.Controls.Add(this.minus1);
            this.groupBox1.Location = new System.Drawing.Point(672, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 338);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // select_color
            // 
            this.select_color.Location = new System.Drawing.Point(148, 212);
            this.select_color.Name = "select_color";
            this.select_color.Size = new System.Drawing.Size(75, 28);
            this.select_color.TabIndex = 22;
            this.select_color.Text = "选择颜色";
            this.select_color.UseVisualStyleBackColor = true;
            this.select_color.Click += new System.EventHandler(this.select_color_Click);
            // 
            // color
            // 
            this.color.AutoSize = true;
            this.color.BackColor = System.Drawing.Color.Blue;
            this.color.Location = new System.Drawing.Point(95, 218);
            this.color.Name = "color";
            this.color.Size = new System.Drawing.Size(29, 12);
            this.color.TabIndex = 21;
            this.color.Text = "Blue";
            // 
            // color_text
            // 
            this.color_text.AutoSize = true;
            this.color_text.Location = new System.Drawing.Point(54, 218);
            this.color_text.Name = "color_text";
            this.color_text.Size = new System.Drawing.Size(35, 12);
            this.color_text.TabIndex = 20;
            this.color_text.Text = "颜色:";
            // 
            // leng
            // 
            this.leng.Location = new System.Drawing.Point(123, 169);
            this.leng.Name = "leng";
            this.leng.Size = new System.Drawing.Size(100, 21);
            this.leng.TabIndex = 19;
            this.leng.Text = "100";
            // 
            // leng_text
            // 
            this.leng_text.AutoSize = true;
            this.leng_text.Location = new System.Drawing.Point(42, 172);
            this.leng_text.Name = "leng_text";
            this.leng_text.Size = new System.Drawing.Size(59, 12);
            this.leng_text.TabIndex = 18;
            this.leng_text.Text = "主干长度:";
            // 
            // right_th
            // 
            this.right_th.Location = new System.Drawing.Point(123, 142);
            this.right_th.Name = "right_th";
            this.right_th.Size = new System.Drawing.Size(100, 21);
            this.right_th.TabIndex = 17;
            this.right_th.Text = "30";
            // 
            // th_text_right
            // 
            this.th_text_right.AutoSize = true;
            this.th_text_right.Location = new System.Drawing.Point(40, 145);
            this.th_text_right.Name = "th_text_right";
            this.th_text_right.Size = new System.Drawing.Size(71, 12);
            this.th_text_right.TabIndex = 16;
            this.th_text_right.Text = "右分支角度:";
            // 
            // left_th
            // 
            this.left_th.Location = new System.Drawing.Point(123, 115);
            this.left_th.Name = "left_th";
            this.left_th.Size = new System.Drawing.Size(100, 21);
            this.left_th.TabIndex = 15;
            this.left_th.Text = "40";
            // 
            // th_text_left
            // 
            this.th_text_left.AutoSize = true;
            this.th_text_left.Location = new System.Drawing.Point(40, 118);
            this.th_text_left.Name = "th_text_left";
            this.th_text_left.Size = new System.Drawing.Size(71, 12);
            this.th_text_left.TabIndex = 14;
            this.th_text_left.Text = "左分支角度:";
            // 
            // right_per
            // 
            this.right_per.Location = new System.Drawing.Point(123, 88);
            this.right_per.Name = "right_per";
            this.right_per.Size = new System.Drawing.Size(100, 21);
            this.right_per.TabIndex = 13;
            this.right_per.Text = "0.7";
            // 
            // right_text
            // 
            this.right_text.AutoSize = true;
            this.right_text.Location = new System.Drawing.Point(40, 91);
            this.right_text.Name = "right_text";
            this.right_text.Size = new System.Drawing.Size(83, 12);
            this.right_text.TabIndex = 12;
            this.right_text.Text = "右分支长度比:";
            // 
            // left_per
            // 
            this.left_per.Location = new System.Drawing.Point(123, 61);
            this.left_per.Name = "left_per";
            this.left_per.Size = new System.Drawing.Size(100, 21);
            this.left_per.TabIndex = 11;
            this.left_per.Text = "0.6";
            // 
            // left_text
            // 
            this.left_text.AutoSize = true;
            this.left_text.Location = new System.Drawing.Point(40, 64);
            this.left_text.Name = "left_text";
            this.left_text.Size = new System.Drawing.Size(83, 12);
            this.left_text.TabIndex = 10;
            this.left_text.Text = "左分支长度比:";
            // 
            // depth
            // 
            this.depth.AutoSize = true;
            this.depth.Cursor = System.Windows.Forms.Cursors.Cross;
            this.depth.Location = new System.Drawing.Point(121, 38);
            this.depth.Name = "depth";
            this.depth.Size = new System.Drawing.Size(17, 12);
            this.depth.TabIndex = 1;
            this.depth.Text = "10";
            // 
            // plus1
            // 
            this.plus1.Location = new System.Drawing.Point(168, 33);
            this.plus1.Name = "plus1";
            this.plus1.Size = new System.Drawing.Size(23, 22);
            this.plus1.TabIndex = 2;
            this.plus1.Text = "+";
            this.plus1.UseVisualStyleBackColor = true;
            this.plus1.Click += new System.EventHandler(this.plus1_Click);
            // 
            // minus1
            // 
            this.minus1.Location = new System.Drawing.Point(197, 33);
            this.minus1.Name = "minus1";
            this.minus1.Size = new System.Drawing.Size(23, 22);
            this.minus1.TabIndex = 3;
            this.minus1.Text = "-";
            this.minus1.UseVisualStyleBackColor = true;
            this.minus1.Click += new System.EventHandler(this.minus1_Click);
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(0, -1);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(651, 601);
            this.panel.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 612);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label depth_text;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label depth;
        private System.Windows.Forms.Button plus1;
        private System.Windows.Forms.Button minus1;
        private System.Windows.Forms.Label color_text;
        private System.Windows.Forms.TextBox leng;
        private System.Windows.Forms.Label leng_text;
        private System.Windows.Forms.TextBox right_th;
        private System.Windows.Forms.Label th_text_right;
        private System.Windows.Forms.TextBox left_th;
        private System.Windows.Forms.Label th_text_left;
        private System.Windows.Forms.TextBox right_per;
        private System.Windows.Forms.Label right_text;
        private System.Windows.Forms.TextBox left_per;
        private System.Windows.Forms.Label left_text;
        private System.Windows.Forms.Button select_color;
        private System.Windows.Forms.Label color;
        private System.Windows.Forms.Panel panel;
    }
}

