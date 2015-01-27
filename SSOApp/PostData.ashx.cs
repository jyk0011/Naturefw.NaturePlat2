using System.Globalization;
using System.Web;
using Nature.Client.SSOApp;
using Nature.Common;

namespace Nature.Service.Ashx
{
    /// <summary>
    /// 转发post申请
    /// </summary>
    /// user:jyk
    /// time:2012/10/18 17:37
    public class Post1 : BaseAshx
    {
         
        /// <summary>
        /// 通过实现 <see cref="T:System.Web.IHttpHandler"/> 接口的自定义 HttpHandler 启用 HTTP Web 请求的处理。
        /// </summary>
        /// <param name="context"><see cref="T:System.Web.HttpContext"/> 对象，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session 和 Server）的引用。</param>
        /// user:jyk
        /// time:2012/10/18 17:41
        public override void ProcessRequest(HttpContext context)
        {
            base.ProcessRequest(context);

           
            //获取隐藏在cookie里面的服务中心的用户ID
            HttpCookie ck = HttpContext.Current.Request.Cookies["userIDserviceByck"];

            if (ck == null)
            {
                context.Response.Write("{\"msg\":\"没有cookie\"}");
                return;
            }

            if( string.IsNullOrEmpty(ck.Value))
            {
                //没有用户ID，不能转发
                context.Response.Write("{\"msg\":\"没有cookie的用户ID\"}");
                return;
            }

            string url = context.Request.QueryString["url"];
            if (string.IsNullOrEmpty(url))
            {
                //没有url。
                url = "";
            }
            //context.Response.Write(url + "<br>");

            string query = context.Request.Url.Query;

            query = string.IsNullOrEmpty(query) ? "?userAppCookie=" + ck.Value : query + "&userAppCookie=" + ck.Value;

            query += "&action=savedata";

            //switch (url.ToLower())
            //{
            //    case "data":    //客户数据
            //        url = AppConfig.ResourceURL + "/data/PostData.ashx" + query;
            //        break;
            //    case "meta":    //元数据
            //        url = AppConfig.ResourceURL + "/data/PostMeta.ashx" + query;
            //        break;
                 
            //    default:        //按照当前的网页地址转发
            //        if (AppConfig.DataServiceUrl != "http://" +  context.Request.Url.Host + ":" + context.Request.Url.Port.ToString(CultureInfo.InvariantCulture))
            //            url = AppConfig.ResourceURL + context.Request.Url.PathAndQuery + query;

            //        break;
                
                    
            //}
            //context.Response.Write(url + "<br>");

            string msg = "";
            string re = MyWebClient.PostAjax(url, out msg);

            var aa = Json.JsonToDictionary(re);

            if (aa == null)
            {
                context.Response.Write("{\"msg\":\"远程服务出现异常！" + url + "\"}");
                return;
            }

            if (aa.ContainsKey("err"))
            {
                msg = aa["err"];
            }

            if (msg.Length >0)
            {
                //出现错误
                context.Response.Write("{\"msg\":\"" + msg + "\"}");
            }
            else
            {
                //正常
                context.Response.Write("{\"msg\":\"\"}");
            
            }


        }

       


    }
}
 
