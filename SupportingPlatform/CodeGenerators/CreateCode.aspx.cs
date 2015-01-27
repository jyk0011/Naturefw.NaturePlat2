using System;
using System.Web.UI;
using Nature.BaseWebform;
using NatureFramework.SupportingPlatform.CodeGenerators.UC;

namespace NatureFramework.SupportingPlatform.CodeGenerators
{
    /// <summary>
    /// 代码生成器，生成实体类
    /// </summary>
    /// user:jyk
    /// time:2012/9/20 9:32
    public partial class CreateCode : BasePageList   
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                const string sql = @"SELECT  PVID AS id, PVTitle AS txt
                                FROM   V_Frame_List_PageView
                                WHERE  (PVTypeID = 703)
                                ORDER BY DisOrder";

                Lst_Function.DataSource = Dal.DalCustomer.ExecuteFillDataTable(sql);
                Lst_Function.DataBind();
            }
        }

        protected void LstFunctionSelectedIndexChanged(object sender, EventArgs e)
        {
            //显示代码

            var code = (UcCodeTemplate)Page.LoadControl("UC/CodeEntity.ascx");
            //UC_CodeTemplate code = (UC_CodeTemplate)Page.LoadControl("UC/UCCode1.ascx");
            code.PageViewID = Lst_Function.SelectedValue;
            code.DalCollection = Dal;
            code.LoadData();

            var tw = new System.IO.StringWriter();
            var hw = new HtmlTextWriter(tw);
            code.RenderControl(hw);

            string tmp = tw.ToString();
            Txt_Code.Text = tmp;

        }
    }
}