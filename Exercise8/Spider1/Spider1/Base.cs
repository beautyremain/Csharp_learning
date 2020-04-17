using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Spider1
{
    public class SimpleCrawler
    {
        public delegate void PageDownloaded(string url);
        public PageDownloaded pageDownloaded;
        private Hashtable urls = new Hashtable();
        private int count = 0;
        public string startUrl;
        private string basic_url;
        private string limit;
        public string Limit { set
            {
                limit = value;
            }
        }
        public void setBasicUrl()
        {

            int end=startUrl.IndexOf("/", 8);
            basic_url = startUrl.Substring(0, end);

        }
        public void ClearHash()
        {
            Console.WriteLine("ClearHash has been called");
            urls = new Hashtable();
        }
        public void showHash()
        {
            Console.WriteLine("哈希表的内容");
            foreach (DictionaryEntry de in urls)
                Console.WriteLine("key="+de.Key+"; value="+de.Value);
        }
        public SimpleCrawler()
        {
            //Console.WriteLine(Regex.IsMatch(@"http://www.cnblogs.com/dstang2000/wlwmanifest.xml", @"[ ]*http[s]*://www.cnblogs.com/dstang2000/"));
            //this.startUrl = @"https://www.cnblogs.com/dstang2000/";
            ////str1.IndexOf("/",start,end);
            //this.basic_url = @"https://www.cnblogs.com";
            //this.limit = @"^[ ]*http[s]*://www.cnblogs.com/dstang2000/";

            //new Thread(this.Crawl).Start();
        }

        public void Crawl()
        {
            urls[startUrl] = false; ;//加入初始页面
            PrintInfo("开始爬行了.... ");
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url]) continue;
                    current = url;
                }
                if (current == null) break;
                PrintInfo("爬行" + current + "页面!");
                string type = "";
                string html = DownLoad(current, out type); // 下载
                urls[current] = true;
                if (type == "html")
                {
                    count++;
                    Parse(html, current);//解析,并加入新的链接
                    PrintInfo("爬行结束");
                }
                else
                {
                    PrintInfo("该页面不是html，停止爬行" + current);
                }
            }
        }

        private void PrintInfo(string current)
        {
            Console.WriteLine(current);
            this.pageDownloaded(current);
        }

        public bool judge(string html)
        {
            string pattern = @"^<!DOCTYPE html>";
            return Regex.IsMatch(html, pattern);
        }
        public string DownLoad(string url, out string type)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                if (!judge(html))
                {
                    throw new Exception("not a html");
                }
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                type = "html";
                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                type = "others";
                return "";
            }
        }
        public bool urlJudge(ref string url, string current)
        {
            if (Regex.IsMatch(url, @"^http"))
            {


            }
            else if (Regex.IsMatch(url, @"^javascript:void"))
            {
                return false;
            }
            else if (Regex.IsMatch(url, @"^/"))
            {
                url = basic_url + url;
            }
            else if (Regex.IsMatch(url, @"^[0-9a-zA-Z]"))
            {
                url = current + url;
            }
            return Regex.IsMatch(url, limit);
        }
        private void Parse(string html, string current)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');
                if (strRef.Length == 0) continue;
                if (!urlJudge(ref strRef, current))
                {
                    //Console.WriteLine("跳过网址："+strRef);
                    continue;
                }
                if (urls[strRef] == null)
                    urls[strRef] = false;
            }
        }
    }
}
