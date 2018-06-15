using System;
using System.Collections;
using System.Xml.XPath;

namespace 爬取.Abstracts
{
    /// <summary>
    /// 下载好的页面的页面xml信息
    /// </summary>
    public interface IWebpage : ICloneable, IEnumerable, IXPathNavigable, IWebContent
    {
    }
}
