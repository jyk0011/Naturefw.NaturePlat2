using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Nature.Service;
using Nature.Service.Ashx;

namespace NatureFramework.SSOApp
{
    /// <summary>
    /// 获取登录人信息
    /// </summary>
    public class getUserName : BaseAshxCrud
    {

        public override void Process()
        {
            base.Process();

            if (MyUser == null)
            {
                //没有登录
            }
            else
            {
                //返回用户名和用户ID
                StringBuilder sb = new StringBuilder(500);
                sb.Append("\"name\":\"");
                sb.Append(MyUser.BaseUser.PersonName);
                sb.Append("\",\"id\":\"");
                sb.Append(MyUser.BaseUser.UserID);
                sb.Append("\",\"userCode\":\"");
                sb.Append(MyUser.BaseUser.UserCode);
                sb.Append("\"");

                Response.Write(sb.ToString());
            }
        }

       
    }
}