using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;

using Nature.BaseWebform;
using Nature.Common;
using Nature.User;


namespace NatureFramework
{
    /// <summary>
    /// ajax + Json + ashx版本的后台管理的登录
    /// </summary>
    /// user:jyk
    /// time:2012/10/8 14:00
    public partial class _Default :BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            btn_Logon.Click += BtnLogonClick;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("url:" + Request.Url + "<br>");
            //Response.Write("AbsolutePath:" + Request.Url.AbsolutePath + "<br>");
            //Response.Write("AbsoluteUri:" + Request.Url.AbsoluteUri + "<br>");
            //Response.Write("Authority:" + Request.Url.Authority + "<br>");
            //Response.Write("DnsSafeHost:" + Request.Url.DnsSafeHost + "<br>");
            //Response.Write("Host:" + Request.Url.Host + "<br>");
            //Response.Write("HostNameType:" + Request.Url.HostNameType + "<br>");
            //Response.Write("LocalPath:" + Request.Url.LocalPath + "<br>");
            //Response.Write("PathAndQuery:" + Request.Url.PathAndQuery + "<br>");
            //Response.Write("Query:" + Request.Url.Query + "<br>");
            //Response.Write("Scheme:" + Request.Url.Scheme + "<br>");
            //Response.Write("Segments:" + Request.Url.Segments + "<br>");
            //Response.Write("UserInfo:" + Request.Url.UserInfo + "<br>");
                           

            //if (!Page.IsPostBack)
            //{
            //    lstProject.DataSource = Dal.DalMetadata.ExecuteFillDataTable("SELECT   ProjectGUID AS id, ProjectName AS txt FROM Manage_Project");
            //    lstProject.DataBind();
            //}


        }

        private void BtnLogonClick(object sender, EventArgs e)
        {
            //登录
            string userCode = txt_User.Text.Trim().Replace("'","");
            string userPassword = txt_Pwd.Text.Trim().Replace("'", "");

            var userManage = new ManageUser { Dal = Dal };

            //string userID = userManage.Logon(userCode, userPassword);

            if (Dal.DalUser.ErrorMessage.Length > 0)
            {
                Functions.PageRegisterAlert(Page, "访问数据库出现异常！");
                return;
            }

            //if (userID == null)
            //{
            //    Functions.PageRegisterAlert(Page, "用户名与密码不匹配，请重新输入！");
            //    return;
            //}

            //把项目ID写入cookies
            var ck = new HttpCookie("DataBaseID") {Value = lstProject.SelectedValue};
            Response.Cookies.Add(ck);

            //可以进入 
            //Response.Redirect("_main/index.htm");
            Response.Redirect("CommonMainJs/");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string s = TextBox1.Text;
            string s1 = DesUrl.Encrypt(s, "12345678");
            string s2 = DesUrl.Decrypt(s1, "12345678");

            TextBox2.Text = s1;
            TextBox3.Text = s2;
        }

        protected void btn_JM_Click(object sender, EventArgs e)
        {
            string miwen = TextBox4.Text;
            string ssokey = TextBox5.Text;

            string yuanwen = DesUrl.Decrypt(miwen, ssokey);

            Response.Write(yuanwen );

        }

        protected void TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnToJson_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            Aa aa1 = new Aa { A1 = "aa1", A2 = "bb1", A3 = "cc1" };

            StringBuilder sb = new StringBuilder(1000);
            sb.Append("{");
            sb.Append("\"A1\":\"");
            sb.Append(aa1.A1);
            sb.Append("\"");

            sb.Append(",\"A2\":\"");
            sb.Append(aa1.A2);
            sb.Append("\"");

            sb.Append(",\"A3\":\"");
            sb.Append(aa1.A3);
            sb.Append("\"");

            sb.Append("}");

            txtJsonMsg.Text += sb.ToString();
            sw.Stop();

         
            txtJsonMsg.Text += "\n==========================\nStringBuilder用时:" + Functions.TimeSpantoFloat(sw.Elapsed);

            sw.Reset();
            sw.Start();
            Aa aa = new Aa {A1 = "aa",A2= "bb",A3="cc"};

            //string strJson = JsonConvert.SerializeObject(aa);
            //txtJsonMsg.Text += strJson;

            //sw.Stop();

            //txtJsonMsg.Text += "\n==========================\nJsonConvert用时:" + Functions.TimeSpantoFloat(sw.Elapsed) + "\n";

            
        }

        protected void btnToObject_Click(object sender, EventArgs e)
        {
            //string json = txtJson.Text;
            //Aa aa =  (Aa)JsonConvert.DeserializeObject(json,typeof(Aa));

        }
    }

    public class Aa
    {
        public string A1 { get; set; }
        public string A2 { get; set; }
        public string A3 { get; set; }
    }
}