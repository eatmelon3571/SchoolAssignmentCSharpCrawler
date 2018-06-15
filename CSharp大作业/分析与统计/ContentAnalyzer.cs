using System;

namespace 分析与统计
{
    public class ContentAnalyzer
    {
        /// <summary>
        /// 存放分析目标数据的数据库的路径
        /// </summary>
        public string DbSourcePath { get; protected set; }

        /// <summary>
        /// 将数据库中的内容分析并保存至folderPath文件夹
        /// </summary>
        /// <param name="folderPath"></param>
        public void DoAnalyzerToFolder(string folderPath)
        {
            //TODO
            throw new NotImplementedException();
        }

        public ContentAnalyzer(string dbSourcePath)
        {
            DbSourcePath = dbSourcePath;
        }
    }
}
