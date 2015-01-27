using System;
using Nature.Common;
using Nature.BaseWebform;

namespace NatureFramework.SupportingPlatform.Meta
{
    /// <summary>
    /// 添加页面视图的页面
    /// </summary>
    /// user:jyk
    /// time:2012/9/20 10:29
    public partial class AddPageView : BasePageForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Functions.PageRegisterJavascript(Page,""+ForeignID  );
        }

        protected override void BtnSaveContinueClick(object sender, EventArgs e)
        {
            string err = Save();
        }

        #region 添加新节点
        protected override void BtnSaveClick(object sender, EventArgs e)
        {

            string err = Save();
            Functions.PageRegisterJavascript(Page, "myReturn()");

        }

        #endregion

        #region 保存
        private string Save()
        {
            //设置选定表的
            //MyDropDownList lst = (MyDropDownList)FrmCommonForm.GetControl("1002045");     //外键的字段ID
            //var entityPageView = new EntityPageView();
             

            //保存信息到数据库
            string err = FrmCommonForm.SaveData();

            if (err.Length > 0)
            {
                //录入的信息的格式不正确。 
                Response.Write(err);
                return "录入的信息的格式不正确";
            }

            Functions.PageRegisterJavascript(Page, "myReturn()");

            return "";
        }
        #endregion 

        #region 表单绑定后，调整表单内容
        protected override void FrmCommonFormFormBinded(object sender, EventArgs e)
        {
            base.FrmCommonFormFormBinded(sender, e);

            //设置“选择表”的下拉列表框的回发事件
            //var lst = (MyDropDownList)FrmCommonForm.GetControl("1006020"); //选择表
           // lst.AutoPostBack = true;
           // lst.TextChanged += LstTextChanged;

            var entityPageView = new EntityPageView();
            
            if (!Page.IsPostBack)
            {
                entityPageView.SetInfo(Dal.DalCustomer, int.Parse(ForeignID));
 
                //绑定控件
                FrmCommonForm.EntityToControl(entityPageView);

            }
        }

        #endregion

        #region 选择添加的是子节点还是同级节点
        /// <summary>
        /// 根据表ID获取表名
        /// </summary>
        /// <param name="tableID"></param>
        /// <returns></returns>
        private string GetTableName(string tableID)
        {
            string sql = "select TableName from Manage_Table where TableID = " + tableID;
            string tmpStr =  Dal.DalCustomer.ExecuteString(sql);
            return tmpStr;
        }

        /// <summary>
        /// 根据字段ID获取字段名
        /// </summary>
        /// <param name="columnID"></param>
        /// <returns></returns>
        private string GetColumnName(string columnID)
        {
            string sql = "select ColSysName from Manage_Columns where ColumnID = " + columnID;
            string tmpStr = Dal.DalCustomer.ExecuteString(sql);
            return tmpStr;
        }


        #endregion

    }
}