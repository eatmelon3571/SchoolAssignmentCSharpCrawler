using System;
using 爬取.Abstracts;

namespace 爬取.Concretes
{
    /// <summary>
    /// 对jlu新闻的页面遍历逻辑
    /// </summary>
    public class JluNewsForwarder : IForwarder<XmlWebpage>
    {
        /// <summary>
        /// 解析这次遍历的页面结果page，并提供接下来轮到的下次遍历的页面的地址
        /// </summary>
        /// <param name="page">在这次遍历中已分析结束的页面</param>
        /// <returns>接下来该轮到的页面的地址，若没有需要继续遍历的页面则返回null</returns>
        public string ForwardFrom(XmlWebpage page)
        {
            //TODO
            throw new NotImplementedException();

            //若是新闻正文页面
            //不把任何页放入等候队列

            //否则
            {
                //若是新闻列表页面
                //分析page页面中新闻的日期
                /*...*/

                //若自身覆盖的日期新于2017/6/1到2018/6/1并有交集，则
                {/*...*/

                    //将页面上各新闻链接放入等候队列
                    //找到"下一页"并将下一页放入等候队列
                }
            }

            return /*下一页的地址*/ "";
        }
    }
}
