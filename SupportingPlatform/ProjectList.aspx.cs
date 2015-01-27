using System;

using System.Web;
using Nature.BaseWebform;

namespace NatureFramework.SupportingPlatform
{
    /// <summary>
    /// 显示和设置当前管理的项目
    /// </summary>
    /// user:jyk
    /// time:2012/12/26 15:10
    public partial class ProjectList : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            lstProject.SelectedIndexChanged += new EventHandler(LstProjectSelectedIndexChanged);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string sql = @"SELECT  DataBaseID AS id, DataTitle AS txt
                                FROM   Manage_DataBase
                                WHERE     (KindID = 1) order by DataBaseID";

                lstProject.DataSource = Dal.DalMetadata.ExecuteFillDataTable(sql);
                lstProject.DataBind();

                //从cookies里获取项目id
                HttpCookie ck = HttpContext.Current.Request.Cookies["DataBaseID"];

                if (ck == null)
                {
                    //没有cookies，取第一个
                    var ck2 = new HttpCookie("DataBaseID") { Value = lstProject.SelectedValue };
                    Response.Cookies.Add(ck2);
                }
                else
                {
                    //有cookies，获取项目ID 
                    lstProject.SetSelectedByValue(ck.Value);
                }

                WriteServiceDbid(lstProject.SelectedValue);


            }
        }

        private void LstProjectSelectedIndexChanged(object sender, EventArgs e)
        {
            //把项目ID写入cookies
            var ck = new HttpCookie("DataBaseID") { Value = lstProject.SelectedValue };
            Response.Cookies.Add(ck);

            WriteServiceDbid(lstProject.SelectedValue);

        }

        private void WriteServiceDbid(string serviceDbid)
        {
            string sql = @"SELECT top 1  DataBaseID  FROM Manage_DataBase WHERE  (DataBaseID = {0})";
            serviceDbid = Dal.DalMetadata.ExecuteString(string.Format(sql, lstProject.SelectedValue));
            var ck3 = new HttpCookie("ServicesDataBaseID") { Value = serviceDbid };
            Response.Cookies.Add(ck3);
        }
    }
}