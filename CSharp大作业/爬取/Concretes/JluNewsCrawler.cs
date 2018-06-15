using System;
using System.IO;
using 爬取.Abstracts;

namespace 爬取.Concretes
{
    /// <summary>
    /// 爬虫主类，控制子线程之间的协作
    /// </summary>
    public class JluNewsCrawler : ICrawler<XmlWebpage, JluNewsForwarder>
    {
        /// <summary>
        /// 消息记录的写出目的地
        /// </summary>
        public StreamWriter LogDestination { get; }

        /// <summary>
        /// 爬虫过程是否已经启动
        /// </summary>
        public bool IsStarted { get; protected set; }

        /// <summary>
        /// 爬虫过程是否正处于暂停
        /// </summary>
        public bool IsPaused { get; protected set; }

        /// <summary>
        /// 初始化爬虫
        /// </summary>
        /// <param name="logDestination"></param>
        public JluNewsCrawler(StreamWriter logDestination)
        {
            LogDestination = logDestination;
            IsStarted = false;
            IsPaused = false;

            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// 启动爬虫
        /// </summary>
        /// <param name="seedUrls">作为爬虫起始点的一些路径</param>
        public void Start(string[] seedUrls)
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// 向爬虫过程发送暂停信号
        /// </summary>
        public void Pause()
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// 向爬虫过程发送停止信号
        /// </summary>
        /// <param name="immidiately">是否紧急停止（无视流缓存、无视子线程结束等）</param>
        public void Stop(bool immidiately = false)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
