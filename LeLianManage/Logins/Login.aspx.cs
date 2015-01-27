using System;
using Nature.Common;
using Nature.DebugWatch;
using Nature.Service;
using Nature.User;

namespace NatureFramework.LeLianManage.Logins
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string aa = "123";
            //string mm = Functions.ToMD5(aa);
            //string mm2 = mm.Substring(8, 16).ToLower();
            //Response.Write(mm2 + "<br>");
            //byte[] bytes = Encoding.Default.GetBytes(mm2);
            //var bb = Convert.ToBase64String(bytes);
            //Response.Write(bb);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userCode = txtUserCode.Value; // Request.Form["userCode"];
            string userPsw = txtUserPsw.Value;

            //实现登录

            userCode = userCode.Replace("'", "''");
            userPsw = Functions.ToMD5(userPsw);

            var dal = CommonClass.SetMetadataDal();

            const string sql = "SELECT TOP 1 userID from Person_User_Info where UserCode='{0}' and LoginPsw ='{1}'";

            string userId = dal.DalUser.ExecuteString(string.Format(sql, userCode, userPsw));
            if (dal.DalUser.ErrorMessage.Length > 2)
            {
                //debugInfo.Remark = "到数据库验证登录账户和密码，出现异常！";
                Response.Write("<br>" + dal.DalUser.ErrorMessage);
            }

            if (dal.DalUser.ErrorMessage.Length > 2)
            {
                //debugInfo.Remark = "到数据库验证登录账户和密码，出现异常！";
                Response.Write("\"msg\":\"" + dal.DalUser.ErrorMessage + "\"");
                return;
            }

            if (string.IsNullOrEmpty(userId))
            {
                Response.Write("\"msg\":\"用户名和密码不匹配！\"");
                return;
            }

            Response.Write("<br>" + userId);

            var mUser = new ManageUser {Dal = dal};

            var debugInfo2 = new NatureDebugInfo {Title = "判断访问权限"};

            UserOnlineInfo user = mUser.CreateUser(userId, debugInfo2.DetailList);

            Response.Write("<br>" + user.BaseUser.UserID);
            Functions.PageRegisterJavascript(Page,"isLogin();");


        }
    }
}