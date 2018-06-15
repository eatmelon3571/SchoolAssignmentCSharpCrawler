namespace 爬取.Abstracts
{
    /// <summary>
    /// 页面遍历逻辑
    /// </summary>
    public interface IForwarder<PageType> where PageType : IWebpage
    {
        /// <summary>
        /// 解析这次遍历的页面结果page，并提供接下来轮到的下次遍历的页面的地址
        /// </summary>
        /// <param name="page">在这次遍历中已分析结束的页面</param>
        /// <returns>接下来该轮到的页面的地址，若没有需要继续遍历的页面则返回null</returns>
        string ForwardFrom(PageType page);
    }
}
