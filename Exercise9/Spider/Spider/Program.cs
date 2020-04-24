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
using System.Diagnostics;
namespace SimpleCrawler
{
    class SimpleCrawler
    {
        private void PrintInfo(string current)
        {
            Console.WriteLine(current);
        }
        private Hashtable urls = new Hashtable();
        private int count = 0;
        private string basic_url;
        private string limit;
        private static string startUrl = @"https://www.cnblogs.com/dstang2000/";
        static void Main(string[] args)
        {
            //Console.WriteLine(Regex.IsMatch(@"http://www.cnblogs.com/dstang2000/wlwmanifest.xml", @"[ ]*http[s]*://www.cnblogs.com/dstang2000/"));
            Stopwatch sw = new Stopwatch();
            sw.Start();
            SimpleCrawler myCrawler = new SimpleCrawler();
           
            myCrawler.basic_url = @"https://www.cnblogs.com";
            myCrawler.limit = @"^[ ]*http[s]*://www.cnblogs.com/dstang2000/";
            if (args.Length >= 1) startUrl = args[0];
            myCrawler.urls.Add(startUrl, false);//加入初始页面
            Thread thread=new Thread(myCrawler.Crawl);
            thread.Start();
            thread.Join();
            sw.Stop();
            Console.WriteLine($"持续时间:{sw.ElapsedMilliseconds}");
            Console.ReadLine();
        }
        public void testEach()
        {
            Console.Write("此时：");
            IDictionaryEnumerator myEnumerator = urls.GetEnumerator();
            bool flag = myEnumerator.MoveNext();
            while (flag)
            {
                string url = (string)myEnumerator.Key;
                Console.WriteLine(myEnumerator.Key + "-" + myEnumerator.Value);
                flag = myEnumerator.MoveNext();
            }
        }
        public void FasterCrawl()
        {
            urls[startUrl] = false; ;//加入初始页面
            PrintInfo("开始爬行了.... ");
            while (true)
            {
                List<string> onDo = new List<string>();
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url]) continue;
                    current = url;
                    onDo.Add(url);

                }
                List<Task> tlist = new List<Task>();
                foreach(string url in onDo)
                {
                    urls[url] = true;
                    Task t = new Task(() =>ThreadRun(url));
                    tlist.Add(t);
                    t.Start();

                }
                Task.WaitAll(tlist.ToArray());
                if (current == null) break;

            }
        }
        public void ThreadRun(string current)
        {
            PrintInfo("爬行" + current + "页面!");
            string type = "";
            string html = DownLoad(current, out type); // 下载

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
        private void Crawl()
        {
            Console.WriteLine("开始爬行了.... ");
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url]) continue;
                    current = url;
                }
                if (current == null ) break;
                Console.WriteLine("爬行" + current + "页面!");
                string type = "";
                string html = DownLoad(current,out type); // 下载
                urls[current] = true;
                if (type == "html")
                {
                    count++;
                    Parse(html,current);//解析,并加入新的链接
                    Console.WriteLine("爬行结束");
                }
                else
                {
                    Console.WriteLine("该页面不是html，停止爬行" + current);
                }
            }
        }
        public bool judge(string html)
        {
            string pattern = @"^<!DOCTYPE html>";
            return Regex.IsMatch(html,pattern);
        }
        public string DownLoad(string url,out string type)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                if(!judge(html))
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
        public bool urlJudge(ref string url,string current)
        {
            if (Regex.IsMatch(url, @"^http"))
            {
                
 
            }
            else if(Regex.IsMatch(url, @"^javascript:void"))
            {
                return false;
            }
            else if(Regex.IsMatch(url,@"^/"))
            {
                url = basic_url + url;
            }
            else if(Regex.IsMatch(url,@"^[0-9a-zA-Z]"))
            {
                url = current + url;
            }
            return Regex.IsMatch(url,limit);
        }
        private void Parse(string html,string current)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');
                if (strRef.Length == 0) continue;
                if(!urlJudge(ref strRef, current))
                {
                    //Console.WriteLine("跳过网址："+strRef);
                    continue;
                }
                if (urls[strRef] == null)
                {
                    urls[strRef] = false;
                }
            }
        }
    }
}
