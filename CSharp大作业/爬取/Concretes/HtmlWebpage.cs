using System;
using System.Xml;
using System.Text;
using System.Net;
using System.IO;
using 爬取.Abstracts;

namespace 爬取.Concretes
{
    /// <summary>
    /// Xml格式的远程页面，例如html等
    /// </summary>
    public class HtmlWebpage : IWebpage
    {
        /// <summary>
        /// 内容信息的文件名，包含扩展名
        /// 如index.html
        /// </summary>
        public string ContentFileFullName
        {
            get
            {
                return Url.Substring(Url.LastIndexOf('/') + 1);
            }
        }

        /// <summary>
        /// 内容对应的远程资源的url
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// 资源在远程端的创建时间
        /// TODO: 发现html并不会告诉客户端自己文件的创建时间=_=，此域报废，取得的内容和RemoteLastWriteTime一致
        /// </summary>
        [Obsolete]
        public DateTime RemoteCreatedTime { get { return RemoteLastWriteTime; } }

        /// <summary>
        /// 资源在远程端的最近修改时间
        /// </summary>
        public DateTime RemoteLastWriteTime { get; protected set; }

        /// <summary>
        /// 无加工的资源内容，未取得内容时为null
        /// </summary>
        public byte[] RawContent { get; protected set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public HtmlWebpage()
        {
            this.Url = "";
            this.RemoteLastWriteTime = DateTime.MinValue;
            this.RawContent = null;

            //UpdateFromUrl();
        }

        /// <summary>
        /// 根据Url以远程内容更新自身的内容。返回自身内容是否产生了修改。
        /// 此类实现中将内容写入自身继承的XmlDocument可访问的部分，外界主要通过此XmlDocument或其相关接口来访问网页的内容
        /// </summary>
        /// <param name="remoteUrl">访问的Url</param>
        /// <returns>若自身内容发生了更改，返回true；否则返回false</returns>
        /// <exception cref="CannotAccessRemoteContentException">对应的Url无法访问时抛出</exception>
        public virtual bool UpdateFromRemote(string remoteUrl)
        {
            try
            {
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(remoteUrl);
                HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
                if (this.Url == remoteUrl && RemoteLastWriteTime == httpResp.LastModified)
                {
                    return false;
                }
                else
                {
                    this.Url = remoteUrl;
                    this.RemoteLastWriteTime = httpResp.LastModified;
                    using (Stream respStream = httpResp.GetResponseStream())
                    using (MemoryStream respStreamMemory = new MemoryStream())
                    {
                        respStream.CopyTo(respStreamMemory);
                        RawContent = respStreamMemory.ToArray();
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                throw new CannotAccessRemoteContentException();
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("[XmlWebpage.cs测试：]");
            HtmlWebpage page = new HtmlWebpage();
            page.UpdateFromRemote("https://www.baidu.com/index.html");
            Console.WriteLine($"ContentFileFullName:{page.ContentFileFullName}");
            Console.Write($"RawContent:{Encoding.UTF8.GetString(page.RawContent)}");
        }

    }
}
