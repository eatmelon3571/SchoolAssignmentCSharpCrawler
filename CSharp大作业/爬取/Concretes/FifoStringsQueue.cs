using System.Collections.Generic;
using 爬取.Abstracts;

namespace 爬取.Concretes
{
    /// <summary>
    /// 先进先出的每项均为字符串的队列
    /// </summary>
    public class FifoStringsQueue : Queue<string>, IWaitingQueue<string>
    {
        /// <summary>
        /// 队列是否为空
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }

        /// <summary>
        /// 将item加入队列，以args作为参数
        /// </summary>
        public void Enqueue(string item, params object[] args)
        {
            Enqueue(item);
        }
    }
}
