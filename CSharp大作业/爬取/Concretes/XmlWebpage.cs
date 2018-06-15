using System;
using System.Xml;
using System.Text;
using 爬取.Abstracts;

namespace 爬取.Concretes
{
    /// <summary>
    /// Xml格式的远程页面，例如html等
    /// </summary>
    public class XmlWebpage : XmlDocument, IWebpage
    {
        /// <summary>
        /// 内容信息的文件名，包含扩展名
        /// </summary>
        public string ContentFileFullName { get; protected set; }

        /// <summary>
        /// 内容对应的远程资源的url
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// 资源在远程端的创建时间
        /// </summary>
        public DateTime RemoteCreatedTime { get; protected set; }

        /// <summary>
        /// 资源在远程端的最近修改时间
        /// </summary>
        public DateTime RemoteLastWriteTime { get; protected set; }

        /// <summary>
        /// 无加工的资源内容
        /// </summary>
        public byte[] RawContent { get; protected set; }

        public XmlWebpage(string Url)
        {
            this.Url = Url;
            UpdateFromUrl();
        }

        /// <summary>
        /// 根据Url以远程内容更新自身的内容。返回自身内容是否产生了修改。
        /// 此类实现中将内容写入自身继承的XmlDocument可访问的部分，外界主要通过此XmlDocument或其相关接口来访问网页的内容
        /// </summary>
        /// <returns>若自身内容发生了更改，返回true；否则返回false</returns>
        /// <exception cref="CannotAccessRemoteContentException">对应的Url无法访问时抛出</exception>
        public bool UpdateFromUrl()
        {
            //TODO
            throw new NotImplementedException();

            //TODO
            //从远程下载内容到FullName,Url,RemoteCreatedTime,RemoteLastWriteTime和RawContent

            string rawContentString = Encoding.UTF8.GetString(RawContent);

            //TODO 是否需要加工？
            this.LoadXml(rawContentString);

            return true;
        }
    }
}
