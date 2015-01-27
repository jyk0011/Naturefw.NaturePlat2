using System;
using Nature.BaseWebform;
using Nature.Common;
using Nature.MetaData.Enum;
using Nature.MetaData.Manager;

namespace NatureFramework.SupportingPlatform.Meta
{
    /// <summary>
    /// 修改视图里的字段，控件
    /// </summary>
    /// user:jyk
    /// time:2012/9/28 14:20
    public partial class ModViewColum : BasePageForm
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

        
            //根据DataID参数，获取字段ID
            const string sql = @"SELECT ColumnID FROM Manage_PageViewCol WHERE (PVColID = {0})";
            string colID = Dal.DalCustomer.ExecuteString(string.Format(sql, DataID));

            //修改主键字段
            FrmCommonForm.DalCollection = Dal;
           
            FrmCommonForm.DataID = DataID;

            FrmColumn.PageViewID = 13604;
            FrmColumn.DalCollection = Dal;
            FrmColumn.OpenButonType = ButonType.UpdateData;  // ButonType.ViewData;
            FrmColumn.DataID = colID;
            FrmColumn.RepeatColumns = 1;  //表单的列数


        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected override void BtnSaveClick(object sender, EventArgs e)
        {
            var operateLog = new ManagerLogOperate
            {
                AddUserID = Int32.Parse(MyUser.BaseUser.UserID),
                Dal = Dal.DalCustomer,
                ModuleID = ModuleID,
                ButtonID = ButtonID,
                PageViewID = MasterPageViewID
            };

            //定义数据变更日志
            var dataChangeLog = new ManagerLogDataChange
            {
                AddUserID = Int32.Parse(MyUser.BaseUser.UserID),
                Dal = Dal
            };

            //保存数据
            string err = FrmCommonForm.SaveData(operateLog, dataChangeLog);

            if (err.Length > 0)
            {
                //有错误发生不能继续。
                Response.Write(err);
                Functions.PageRegisterAlert(Page, "保存字段基本信息时发生意外！");
                return;
            }

            err = FrmColumn.SaveData(operateLog, dataChangeLog);

            if (err.Length > 0)
            {
                //有错误发生不能继续。
                Response.Write(err);
                Functions.PageRegisterAlert(Page, "保存字段表单信息时发生意外！");
                return;
            }

            //保存后关闭
            Functions.PageRegisterJavascript(Page, "ReloadForUpdate(true)");

        }

        protected override void SetButtonID()
        {
            base.ButtonID = 13601;
        }
    }
}