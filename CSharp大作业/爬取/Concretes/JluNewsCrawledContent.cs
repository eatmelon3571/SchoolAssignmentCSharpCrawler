using System;
using System.Text;
using 爬取.Abstracts;

namespace 爬取.Concretes
{
    /// <summary>
    /// 爬取得来的jlu新闻的信息
    /// </summary>
    public class JluNewsCrawledContent : ICrawledContent
    {
        /// <summary>
        /// 页面有效信息的标题
        /// </summary>
        public string Title { get; protected set; }

        /// <summary>
        /// 页面源信息的地址
        /// </summary>
        public string SourceUrl { get; }

        /// <summary>
        /// 依据自身内容计算而得的，在本地存储上相对于爬虫根目录而言的相对路径文件名。
        /// 注意：尽管此值可被随时取得，取得此值时文件是否已经在对应路径被创建却并不确定
        /// </summary>
        public string LocalFileName
        {
            get
            {
                //TODO
                throw new NotImplementedException();

                //根据自身内容计算
            }
        }

        /// <summary>
        /// 资源在远程端的创建时间
        /// </summary>
        public DateTime RemoteCreatedTime { get; protected set; }

        /// <summary>
        /// 资源在远程端的最近修改时间
        /// </summary>
        public DateTime RemoteLastWriteTime { get; protected set; }

        /// <summary>
        /// 经过处理以后保存的有效信息的内容
        /// </summary>
        public byte[] Content
        {
            get
            {
                return Encoding.UTF8.GetBytes(ContentString);
            }

            set
            {
                ContentString = Encoding.UTF8.GetString(value);
            }
        }

        /// <summary>
        /// 经过处理以后保存的有效信息的内容的字符串
        /// </summary>
        protected string ContentString = "";

        public JluNewsCrawledContent(XmlWebpage remote)
        {
            SetFrom(remote);
        }

        public JluNewsCrawledContent(string localFileName)
        {
            ReadFromLocalFile(localFileName);
        }

        /// <summary>
        /// 从IWebContent设置自身的内容
        /// </summary>
        public void SetFrom(IWebContent content)
        {
            //TODO
            throw new NotImplementedException();

            //从content摘取新闻信息
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
            //TODO
            throw new NotImplementedException();

            if (localFileName == null) localFileName = LocalFileName;

            //TODO
            /*...*/
        }

        /// <summary>
        /// 按照LocalFileName将自身的内容写出至本地对应路径的文件中
        /// </summary>
        public void WriteToLocalFile()
        {
            //TODO
            throw new NotImplementedException();
        }

    }
}
