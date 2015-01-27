using System;
using System.Diagnostics;
using Nature.BaseWebform;
using Nature.Common;

namespace NatureFramework.SupportingPlatform
{
    /// <summary>
    /// 自然框架支持平台的后台总页面
    /// </summary>
    /// user:jyk
    /// time:2012/9/1 14:22
    public partial class Default : BasePage  
    {
        protected Stopwatch sw;

        protected override void OnPreInit(EventArgs e)
        {
            sw = new Stopwatch();
            sw.Start();
            base.OnInit(e);
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            sw.Stop();
            Response.Write(Functions.TimeSpantoFloat(sw.Elapsed));
            base.OnUnload(e);
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}