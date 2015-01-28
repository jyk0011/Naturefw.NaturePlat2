using System;
using System.Collections.Generic;
using System.Web;
using Nature.Service.Ashx;

namespace Nature.Service.MetaData
{
    /// <summary>
    /// 清除缓存
    /// </summary>
    public class ClearCache : BaseAshx
    {
        public override void Process()
        {
            HttpContext.Current.Cache.Remove("DataBaseConnInfo");

        }
    }
}