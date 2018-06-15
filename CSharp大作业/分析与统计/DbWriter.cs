using System;
using 爬取.Abstracts;
using 爬取.Concretes;

namespace 分析与统计
{
    public class DbWriter
    {
        /// <summary>
        /// 目标的数据库地址
        /// </summary>
        public string DbDestinationPath { get; protected set; }

        /// <summary>
        /// 将单条爬虫结果信息content分析并记录至数据库
        /// </summary>
        public void WriteCrawledContentToDb(ICrawledContent content)
        {
            //TODO
            throw new NotImplementedException();
        }

        public DbWriter(string dbDestinationPath)
        {
            DbDestinationPath = dbDestinationPath;
        }
    }
}
