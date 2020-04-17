using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Spider1
{
    public partial class Form1 : Form
    {
        SimpleCrawler crawler = new SimpleCrawler();
        public Form1()
        {
            InitializeComponent();
            //string limitStr = @"https://www.cnblogs.com/dstang2000/";
            //string[] array = limitStr.Split(':');
            //limitStr = @"^[ ]*http[s]?:" + array[1];
            //Console.WriteLine("limit:"+limitStr);
            //Console.WriteLine("second string:"+array[1]);
            crawler.pageDownloaded += Crawler_PageDownloaded;
        }

        private void Crawler_PageDownloaded(string url)
        {
            if (this.listBox1.InvokeRequired)
            {
                Action<String> action = this.AddUrl;
                this.Invoke(action, new object[] { url });
            }
            else
            {
                AddUrl(url);
            }
        }

        private void AddUrl(string url)
        {
            listBox1.Items.Add(url);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Url = this.txtUrl.Text.Trim();
            string Limit = this.txtLimit.Text.Trim();
            //这里还差一个对crawler中的哈希表的清空
            crawler.ClearHash();
            if (Url != "" && Limit != "")
            {
                crawler.startUrl = Url;
                try
                {
                    crawler.setBasicUrl();
                    string limitStr = Limit;
                    string[] array = limitStr.Split(':');
                    limitStr = @"^[ ]*http[s]?:" + array[1];
                    crawler.Limit = limitStr;
                    listBox1.Items.Clear();
                    new Thread(crawler.Crawl).Start();
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"网址输入有误，报错为：{ex.Message}", "注意");
                }

            }
            else
            {
                MessageBox.Show("没有完整的输入！","注意");
            }
        }

    }

    
}
