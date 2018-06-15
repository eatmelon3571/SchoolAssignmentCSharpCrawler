using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace 爬取.Abstracts
{
    public interface ICrawler<out PageType, out ForwarderType> where PageType : IWebpage where ForwarderType : IForwarder<PageType>
    {

        /// <summary>
        /// 消息记录的写出目的地
        /// </summary>
        StreamWriter LogDestination { get; }

        /// <summary>
        /// 爬虫过程是否已经启动
        /// </summary>
        bool IsStarted { get; }

        /// <summary>
        /// 爬虫过程是否正处于暂停
        /// </summary>
        bool IsPaused { get; }

        /// <summary>
        /// 启动爬虫
        /// </summary>
        /// <param name="seedUrls">作为爬虫起始点的一些路径</param>
        void Start(string[] seedUrls);

        /// <summary>
        /// 向爬虫过程发送暂停信号
        /// </summary>
        void Pause();

        /// <summary>
        /// 向爬虫过程发送停止信号
        /// </summary>
        /// <param name="immidiately">是否紧急停止（无视流缓存、无视子线程结束等）</param>
        void Stop(bool immidiately = false);
    }
}
