using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using Nature.Common;
using Nature.BaseWebform;
using Nature.MetaData.Manager;
using Nature.UI.WebControl.BaseControl.List;
using Nature.UI.WebControl.BaseControl.TextBox;

namespace NatureFramework.SupportingPlatform.Meta
{
    /// <summary>
    /// 添加功能节点的页面
    /// </summary>
    /// user:jyk
    /// time:2012/9/20 10:29
    public partial class AddModule : BasePageForm
    {
        private bool _isListChanged = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                const string sql = @"SELECT   TableID AS id, TableName AS txt FROM Manage_Table WHERE (IsDel = 0) ORDER BY DisOrder";

                lstTableID.DataSource = Dal.DalCustomer.ExecuteFillDataTable(sql);
                lstTableID.DataBind();
            }
            

            Functions.PageRegisterJavascript(Page, "parent.document.getElementById(\"span_title\").innerHTML=\"" + PageViewMeta.Title + "\";");
       
        }

        protected override void BtnSaveContinueClick(object sender, EventArgs e)
        {
            string err = Save();
            Functions.PageRegisterJavascript(Page, "ReloadForDel(false)");
        }

        #region 添加新节点

        protected override void BtnSaveClick(object sender, EventArgs e)
        {

            string err = Save();
            //Functions.PageRegisterJavascript(Page, "ReloadFirst(true)");
            string tmpModuleID = lstView.Items[0].Value;
            string url = @"/SupportingPlatform/Meta/AddColumn.aspx?mdid=130&mpvid=13001&fpvid=0&bid=12803&id=" + tmpModuleID + "&frid=2";

            Functions.PageRegisterJavascript(Page, "ReloadForDel(false)");
            Functions.PageRegisterJavascript(Page, "toChangeCol('" + url + "')");
        }

        #endregion

        #region 保存

        private string Save()
        {
            //获取用户输入的数据
            string tmp = FrmCommonForm.GetInputValue();
            bool isTure = tmp.Length == 0;
            if (isTure == false)
            {
                //录入的信息的格式不正确。
                return "录入的信息的格式不正确";
            }

            //把用户输入的数据填充到节点的实例。
            var module = new EntityModule();
            FrmCommonForm.ControlToEntity(module);

            //判断序号是否重复的。
            module.DisOrderIsSample(Dal.DalCustomer,DataID  );

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
                Dal = Dal,
                PageViewMeta = PageViewMeta
            };
            
            //保存数据
            string err = FrmCommonForm.SaveData(operateLog, dataChangeLog);

            if (err.Length > 0)
            {
                //有错误发生不能继续。
                Response.Write(err);
                return "保存数据是发生意外！";
            }

            #region 创建视图和按钮
            //string viewIDs = lstView.SelectedValue;
            string width = txtWindowWidth.TextTrimNone;
            if (!Functions.IsInt(width))
            {
                Functions.PageRegisterAlert(Page, "宽度必须是数字！");
                return "宽度必须是数字！";
            }
            string height = txtWindowHeight.TextTrimNone;
            if (!Functions.IsInt(height))
            {
                Functions.PageRegisterAlert(Page, "高度必须是数字！");
                return "高度必须是数字！";
            }
            string userID = MyUser.BaseUser.UserID;
            module.CreatePageView(Dal.DalCustomer, lstView, lstTableID.SelectedValue , userID);

            //创建按钮
            module.CreateButton(Dal.DalCustomer, lstButton, userID, width, height);

            #endregion

            return "";
        }

        #endregion

        #region 表单绑定后，调整表单内容

        protected override void FrmCommonFormFormBinded(object sender, EventArgs e)
        {
            base.FrmCommonFormFormBinded(sender, e);

            //加模块ID的onchange事件
            var txt = (MyTextBox) FrmCommonForm.GetControl("1000010");
            txt.AutoPostBack = true;
            txt.TextChanged += TxtTextChanged;

            //string tmpModuleID = txt.Text;
            //Response.Write(tmpModuleID + "<br>");
            //修改表单内容

            var module = new EntityModule();

            BindForm(module);

            #region 设置选中的节点，绑定视图列表，绑定按钮列表

            if (!Page.IsPostBack)
            {
                BindForm2(module);
            }

            #endregion

        }

        #region 根据是子节点还是同级节点，设置表单默认值
        private void BindForm(EntityModule module)
        {
            var txt = (MyTextBox) FrmCommonForm.GetControl("1000010");
            string tmpModuleID = txt.Text;

            //获取是要添加子节点，还是兄弟节点
            string addNoteKind = lstNewModuleType.SelectedValue;

            //设置默认值
            if (addNoteKind == "1")
                module.SetSonNote(Dal.DalCustomer, int.Parse(DataID), tmpModuleID);
            else
                module.SetBortherNote(Dal.DalCustomer, DataID, tmpModuleID);

            //绑定控件，把实体类的值赋值给控件
            FrmCommonForm.EntityToControl(module);
        }
        #endregion

        #region 生成按钮、视图的选项列表
        private void BindForm2(EntityModule module)
        {
            Session["oldID"] = module.ModuleID;
            //设置选中的节点
            var dt = (DataTable) ((MyDropDownList) FrmCommonForm.GetControl("1000020")).DataSource;

            if (dt != null)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "id=" + DataID;
                lblModuleName.Text = dv[0][1].ToString().Trim('　');
            }

            //生成视图列表
            string id = module.ModuleID.ToString(CultureInfo.InvariantCulture) + "0{0}";

            var view = new[] {"列表", "查询", "删除","表单/添加", "修改"};
            lstView.Items.Clear();
            for (int i = 1; i < 6; i++)
            {
                string tmpID = string.Format(id, i);
                var item = new ListItem
                               {
                                   Value = tmpID,
                                   Text = tmpID + "_" + view[i - 1],
                                   Selected = true
                               };
                lstView.Items.Add(item);
            }

            //生成按钮视图lstButton
            view = new[] {"查看", "添加", "修改", "删除", "查询", "导出Excel"};
            lstButton.Items.Clear();
            for (int i = 1; i <= view.Length; i++)
            {
                string tmpID = string.Format(id, i);
                var item = new ListItem
                               {
                                   Value = tmpID,
                                   Text = tmpID + "_" + view[i - 1],
                                   Selected = true
                               };
                lstButton.Items.Add(item);
            }
        }
        #endregion

        private void TxtTextChanged(object sender, EventArgs e)
        {
            //Response.Write(Request.Form["__EVENTTARGET"]);
            if (_isListChanged)
                return;

            var module = new EntityModule();

            //获取用户输入的信息，赋给实体类
            FrmCommonForm.ControlToEntity(module);

            //表单控件里的onchange事件，靠不住呀。赋值就算，没办法了，只好自己判断
            if (Session["oldID"] != null)
            {
                if (Session["oldID"].ToString() != module.ModuleID.ToString(CultureInfo.InvariantCulture))
                {
                    BindForm(module);
                    BindForm2(module);
                }
            }
            else
            {
                BindForm(module);
                BindForm2(module);
            }
            Session["oldID"] = module.ModuleID;
            
        }

        #endregion

        #region 选择添加的是子节点还是同级节点

        protected void LstNewModuleTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            var module = new EntityModule();
           
            //获取用户输入的信息，赋给实体类
            FrmCommonForm.ControlToEntity(module);

            BindForm(module);
            if (!Page.IsPostBack)
            {
                BindForm2(module);
            }
            _isListChanged = true;

        }
        #endregion

    }
}