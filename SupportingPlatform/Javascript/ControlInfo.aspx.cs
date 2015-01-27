using System;
using Nature.BaseWebform;
using Nature.MetaData.Enum;

namespace NatureFramework.SupportingPlatform.Javascript
{
    /// <summary>
    /// 在线编辑字段元数据里的“控件描述”信息
    /// </summary>
    /// user:jyk
    /// time:2012/9/28 14:21
    public partial class ControlInfo : BasePageForm
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            frmList.PageViewID = 12505;
            frmList.DalCollection = Dal;
            frmList.OpenButonType = ButonType.AddData ;  // ButonType.ViewData;
            frmList.DataID = DataID;
            frmList.RepeatColumns = 1;  //表单的列数
             
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected override void SetButtonID()
        //{
        //    base.ButtonID = 13601;
        //}
    }
}