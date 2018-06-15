using System;

namespace 爬取.Abstracts
{
    /// <summary>
    /// 远程资源完成到本地的下载后的在本地存储的内容信息
    /// </summary>
    public interface IWebContent
    {
        /// <summary>
        /// 内容信息的文件名，包含扩展名
        /// </summary>
        string ContentFileFullName { get; }

        /// <summary>
        /// 内容对应的远程资源的url
        /// </summary>
        string Url { get; }

        /// <summary>
        /// 资源在远程端的创建时间
        /// </summary>
        DateTime RemoteCreatedTime { get; }

        /// <summary>
        /// 资源在远程端的最近修改时间
        /// </summary>
        DateTime RemoteLastWriteTime { get; }

        /// <summary>
        /// 无加工的资源内容
        /// </summary>
        byte[] RawContent { get; }

        /// <summary>
        /// 根据Url以远程内容更新自身的内容。返回自身内容是否产生了修改。
        /// </summary>
        /// <returns>若自身内容发生了更改，返回true；否则返回false</returns>
        /// <exception cref="CannotAccessRemoteContentException">对应的Url无法访问时抛出</exception>
        bool UpdateFromUrl();
    }

    /// <summary>
    /// 无法访问远程内容的异常
    /// </summary>
    public class CannotAccessRemoteContentException : Exception
    {
    }
}
