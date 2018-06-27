using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using 爬取.Abstracts;

namespace 爬取.Concretes
{
    /// <summary>
    /// 爬取得来的jlu新闻的信息
    /// </summary>
    public class JluNewsCrawledContent : ICrawledContent
    {
        /// <summary>
        /// 存储爬取到的文件的根目录
        /// </summary>
        public static string FileStorageRootDirectory = ".";

        /// <summary>
        /// 页面有效信息的标题
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// 新闻的发布时间
        /// </summary>
        public DateTime NewsDate { get; protected set; }

        /// <summary>
        /// 新闻发布方的部门
        /// </summary>
        public string Department { get; protected set; }

        /// <summary>
        /// 页面源信息的地址
        /// </summary>
        public string SourceUrl { get; protected set; }

        /// <summary>
        /// 资源在远程端的最近修改时间
        /// </summary>
        public DateTime RemoteLastWriteTime { get; protected set; }

        /// <summary>
        /// 经过处理以后保存的有效信息的内容的字符串
        /// </summary>
        public string ContentString { get; protected set; }

        /// <summary>
        /// 依据自身内容计算而得的，在本地存储上相对于FileStorageRootDirectory而言的相对路径文件名。
        /// 注意：尽管此值可被随时取得，取得此值时文件是否已经在对应路径被创建却并不确定
        /// </summary>
        public string LocalFileName
        {
            get
            {
                return this.NewsDate.ToString("yyyy-MM-dd") + "/" + this.Title + ".newsfile";
            }
        }

        /// <summary>
        /// 经过处理以后保存的有效信息的内容
        /// </summary>
        public byte[] Content
        {
            get
            {
                return Encoding.UTF8.GetBytes(ContentString);
            }

            protected set
            {
                ContentString = Encoding.UTF8.GetString(value);
            }
        }

        public JluNewsCrawledContent(HtmlWebpage remote)
        {
            this.RemoteLastWriteTime = DateTime.MinValue;
            SetFrom(remote);
        }

        public JluNewsCrawledContent(string localFileName)
        {
            ReadFromLocalFile(localFileName);
        }

        /// <summary>
        /// 从IWebContent设置自身的内容，返回是否更新了自己
        /// </summary>
        /// <exception cref="FormatException">当WebContent内容的格式不符合JluNews的格式</exception>
        /// <returns>是否更新，不需更新时返回false，成功更新返回true</returns>
        public virtual bool SetFrom(IWebContent content)
        {
            if (this.RemoteLastWriteTime < content.RemoteLastWriteTime)
            {
                _SetFromHtmlString(Encoding.UTF8.GetString(content.RawContent), content.Url, content.RemoteLastWriteTime);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 按照LocalFileName从本地对应路径的文件中读取信息，设置自身的内容
        /// </summary>
        public void ReadFromLocalFile()
        {
            ReadFromLocalFile(LocalFileName);
        }

        /// <summary>
        /// 按照localFileName从本地对应路径的文件中读取信息，设置自身的内容
        /// </summary>
        public void ReadFromLocalFile(string localFileName)
        {
            if (localFileName == null) localFileName = this.LocalFileName;
            {
                string[] split = localFileName.Split('/');
                this.Title = split[split.Length - 1];
                int tempDotIndex = this.Title.LastIndexOf('.');
                if (tempDotIndex != -1) this.Title = this.Title.Substring(0, tempDotIndex);
            }
            using (FileStream stream = File.OpenRead(localFileName))
            {
                //前四行是自定内容(SourceUrl,RemoteLastWriteTime,NewsDate,Department)，之后是正文
                StreamReader reader = new StreamReader(stream);
                string urlLine = reader.ReadLine();
                this.SourceUrl = urlLine.Substring(urlLine.IndexOf(':') + 2);
                string lastWriteLine = reader.ReadLine();
                this.RemoteLastWriteTime = DateTime.ParseExact(lastWriteLine.Substring(lastWriteLine.IndexOf(':') + 2), "yyyy-MM-dd@HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                string newsDateLine = reader.ReadLine();
                this.NewsDate = DateTime.ParseExact(newsDateLine.Substring(newsDateLine.IndexOf(':') + 2), "yyyy-MM-dd@HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                string deptLine = reader.ReadLine();
                this.Department = deptLine.Substring(deptLine.IndexOf(':') + 2);
                this.ContentString = reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 按照LocalFileName将自身的内容写出至本地对应路径的文件中
        /// </summary>
        public void WriteToLocalFile()
        {
            string targetPath = FileStorageRootDirectory + "/" + LocalFileName;
            string targetDirectory = targetPath.Substring(0, targetPath.LastIndexOf('/'));
            Directory.CreateDirectory(targetDirectory);
            using (StreamWriter writer = File.CreateText(targetPath))
            {
                //前四行是自定内容(SourceUrl,RemoteLastWriteTime,NewsDate,Department)，之后是正文
                writer.WriteLine($"SourceUrl: {this.SourceUrl}");
                writer.WriteLine($"RemoteLastWriteTime: { this.RemoteLastWriteTime.ToString("yyyy-MM-dd@HH:mm:ss")}");
                writer.WriteLine($"NewsDate: {this.NewsDate.ToString("yyyy-MM-dd@HH:mm")}");
                writer.WriteLine($"Department: {this.Department}");
                writer.Write(ContentString);
                writer.Flush();
            }
        }

        /// <summary>
        /// 从htmlContext中设置自身各属性值
        /// </summary>
        ///<exception cref="FormatException">格式不符合JluNews的格式</exception>
        private void _SetFromHtmlString(string htmlContext, string url, DateTime htmlRemoteLastWriteTime)
        {
            try
            {
                //用作抽取的正则
                string titleRegexStr = @"<div class=""content_t""[^>]*?>(.*?)</div>";
                string timeAndDeptRegexStr = @"<div class=""content_time""[^>]*?>(.*?)</div>";
                string contentRegexStr = @"<div class=""content_font fontsize immmge""[^>]*?>([\w\W]*?)</div>";

                //用作替换和删除的正则，在content抽取好以后，将能被contentReplaceRegexStr匹配的成分替换为\n，将contentExceptionRegexStr匹配的成分删除
                string contentReplaceRegexStr = @"</p>";
                string contentExceptionRegexStr = @"<[^>]*?>|&[^;]*?;";

                Regex titleRegex = new Regex(titleRegexStr);
                Regex timeAndDeptRegex = new Regex(timeAndDeptRegexStr);
                Regex contentRegex = new Regex(contentRegexStr, RegexOptions.Multiline);

                //获取title
                string tempTitle = titleRegex.Match(htmlContext).Groups[1].Value;

                //获取time和dept
                Match timeAndDeptMatch = timeAndDeptRegex.Match(htmlContext);
                string timeAndDeptStr = timeAndDeptMatch.Groups[1].Value;
                string[] timeAndDeptSplits = timeAndDeptStr.Split(new string[] { "<span>", "</span>" }, StringSplitOptions.RemoveEmptyEntries);
                string timeStr = timeAndDeptSplits[0].Replace("&nbsp;&nbsp;", "");
                DateTime tempNewsdate = DateTime.ParseExact(timeStr, "yyyy年MM月dd日 HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                string tempDepartment = timeAndDeptSplits[1];

                //获取content
                Match contentMatch = contentRegex.Match(htmlContext);
                string tempContent = contentMatch.Groups[1].Value;
                tempContent = Regex.Replace(tempContent, contentReplaceRegexStr, "\n");
                tempContent = Regex.Replace(tempContent, contentExceptionRegexStr, "").Trim();

                //统一赋值
                this.Title = tempTitle;
                this.NewsDate = tempNewsdate;
                this.Department = tempDepartment;
                this.ContentString = tempContent;
                this.RemoteLastWriteTime = htmlRemoteLastWriteTime;
                this.SourceUrl = url;
            }
            catch (Exception e)
            {
                throw new FormatException(e.Message, e);
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("[JluNewsCrawledContent.cs测试：]");

            //从互联网的网页上取得新闻的例子
            HtmlWebpage html = new HtmlWebpage();
            html.UpdateFromRemote("http://oa.jlu.edu.cn/defaultroot/PortalInformation!getInformation.action?id=5702457&channelName=%E6%A0%A1%E5%9B%AD%E5%BF%AB%E8%AE%AF&categoryName=%E6%A0%A1%E5%9B%AD%E5%BF%AB%E8%AE%AF&fromflag=login&channelId=179578");
            JluNewsCrawledContent crawledContent = new JluNewsCrawledContent(html);
            Console.WriteLine($"标题：{crawledContent.Title}\n" +
                        $"新闻时间：{crawledContent.NewsDate}\n" +
                        $"发布部门：{crawledContent.Department}\n" +
                        $"源地址：{crawledContent.SourceUrl}\n" +
                        $"远程最近修改时间：{crawledContent.RemoteLastWriteTime}\n" +
                        $"对应保存路径：{crawledContent.LocalFileName}\n" +
                        $"新闻正文：{crawledContent.ContentString}");
            Console.WriteLine("----");

            //保存到文件
            crawledContent.WriteToLocalFile();

            //从文件读取
            JluNewsCrawledContent anotherCrawledContent = new JluNewsCrawledContent(crawledContent.LocalFileName);
            Console.WriteLine($"标题：{anotherCrawledContent.Title}\n" +
                        $"新闻时间：{anotherCrawledContent.NewsDate}\n" +
                        $"发布部门：{anotherCrawledContent.Department}\n" +
                        $"源地址：{anotherCrawledContent.SourceUrl}\n" +
                        $"远程最近修改时间：{anotherCrawledContent.RemoteLastWriteTime}\n" +
                        $"对应保存路径：{anotherCrawledContent.LocalFileName}\n" +
                        $"新闻正文：{anotherCrawledContent.ContentString}");
        }
    }
}
