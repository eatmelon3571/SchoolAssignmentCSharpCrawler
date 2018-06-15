using System.Collections;
using System.Collections.Generic;

namespace 爬取.Abstracts
{
    /// <summary>
    /// 待爬取页面的队列
    /// </summary>
    public interface IWaitingQueue<T> : IEnumerable<T>, IEnumerable where T : class
    {
        /// <summary>
        /// 队列是否为空
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// 队列的长度
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 将item加入队列，以args作为参数
        /// </summary>
        void Enqueue(T item, params object[] args);

        /// <summary>
        /// 取出队首内容，并将其从队列中除去
        /// </summary>
        /// <returns>队首的内容；若队列为空，返回null</returns>
        T Dequeue();
    }
}
