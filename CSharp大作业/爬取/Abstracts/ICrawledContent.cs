using System;
using System.IO;

namespace 爬取.Abstracts
{
    /// <summary>
    /// 爬取后的页面经过处理以后保留的有效信息
    /// </summary>
    public interface ICrawledContent
    {
        /// <summary>
        /// 页面有效信息的标题
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 页面源信息的地址
        /// </summary>
        string SourceUrl { get; }

        /// <summary>
        /// 依据自身内容计算而得的，在本地存储上相对于爬虫根目录而言的相对路径文件名。
        /// 注意：尽管此值可被随时取得，取得此值时文件是否已经在对应路径被创建却并不确定
        /// </summary>
        string LocalFileName { get; }

        /// <summary>
        /// 资源在远程端的创建时间
        /// </summary>
        DateTime RemoteCreatedTime { get; }

        /// <summary>
        /// 资源在远程端的最近修改时间
        /// </summary>
        DateTime RemoteLastWriteTime { get; }

        /// <summary>
        /// 经过处理以后保存的有效信息的内容
        /// </summary>
        byte[] Content { get; }

        /// <summary>
        /// 从IWebContent设置自身的内容
        /// </summary>
        void SetFrom(IWebContent content);

        /// <summary>
        /// 按照LocalFileName从本地对应路径的文件中读取信息，设置自身的内容
        /// </summary>
        void ReadFromLocalFile();

        /// <summary>
        /// 按照LocalFileName将自身的内容写出至本地对应路径的文件中
        /// </summary>
        void WriteToLocalFile();
    }
}
