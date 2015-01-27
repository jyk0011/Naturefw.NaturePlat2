using System;
using System.Data.OleDb;
using System.Web;
using System.Web.UI.WebControls;
using Nature.Common;
using Nature.Data;
using Nature.BaseWebform;

namespace NatureFramework.SupportingPlatform.Document
{
    /// <summary>
    /// 处理excel格式的数据库文档
    /// </summary>
    /// user:jyk
    /// time:2012/9/1 14:58
    public partial class Excel : BasePageList
    {
        /// <summary>
        /// 建立访问Access的访问库
        /// </summary>
        private DataAccessLibrary _acc;

        private DataAccessLibrary _dalCustomer;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Pager1.PagerSql.Page = this;
            Pager1.ShowDataControl = GV_Table;
        }

        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
          
            HttpCookie ck = Request.Cookies["DataBaseID"];

            string excelPath = "";
            if (ck == null)
            {
                //没有cookies
            }
             

            string sql = "";

            if (!Page.IsPostBack)
            {
                //获取选择文档的下拉列表
                sql = @"SELECT  DataBaseID AS id, DataTitle AS txt
                                FROM    Manage_DataBase
                                WHERE     (KindID = 3) AND (DataName = N'{0}')";

                string dataName = ck.Value;

                if (!Functions.IsInt(dataName))
                {
                    dataName = "1";
                }

                lstData.DataSource = Dal.DalMetadata.ExecuteFillDataTable(string.Format(sql, dataName));
                lstData.DataBind();
            }

            sql = "SELECT TOP 1 ConnString FROM   Manage_DataBase WHERE  (DataBaseID = {0})";

            excelPath = Dal.DalMetadata.ExecuteString(string.Format(sql, this.lstData.SelectedValue));

            //const string cnString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=|DataDirectory|客户项目数据库设计.xls; Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            //string cnString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + excelPath + "; Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            string cnString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + excelPath + "; Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            _acc = DalFactory.CreateDal(cnString, "System.Data.OleDb");

            Pager1.Dal = _acc;
           
            _dalCustomer = Dal.DalCustomer;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Heidden();
                GetTableName();
                Btn_TableName.SelectedIndex = 0;

                //Response.Write(userInfo.UserID);
            }

            SetPagerInfo();
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            //遍历，去掉最后带_ 的表
            foreach (ListItem item in Btn_TableName.Items)
            {
                if (item.Text.Substring(item.Text.Length - 1, 1) == "_")
                {
                    item.Attributes.Add("style", "display:none");

                }
            }
        }

        //单选组相关的代码

        #region 获取Excel里面的表名，绑定单选组
        private void GetTableName()
        {
            var oleConn = (OleDbConnection)_acc.Command.Connection;
            oleConn.Open();
            var dtExcelSchema = oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            oleConn.Close();

            Btn_TableName.DataSource = dtExcelSchema;
            Btn_TableName.DataBind();
            
        }
        #endregion

        #region 单选组的事件，显示工作簿里的数据
        protected void BtnTableNameSelectedIndexChanged(object sender, EventArgs e)
        {
            string tableName = Btn_TableName.SelectedValue;

            Pager1.PagerSql.TableName = "[" + tableName.Replace("'","") + "]";
            Pager1.PagerSql.CreateSQL();
            Pager1.BindFirstPage();
            
        }
        #endregion

        #region 隐藏
        private void Heidden()
        {
            GV_Column.Visible = false;
            Txt_BuildTable.Visible = false;
            Btn_add.Visible = false;

        }
        #endregion

        #region 设置分页控件的属性，显示Excel里面的“表”的信息
        private void SetPagerInfo()
        {
            string tableName = "[" + Btn_TableName.SelectedValue.Replace("'", "") + "]";

            Pager1.PagerSql.TableName = tableName;
            Pager1.PagerSql.TableShowColumns = "*";
            Pager1.PagerSql.TablePKColumn = "[表编号]";
            Pager1.PagerSql.TableOrderByColumns = "[表编号]";
            Pager1.PagerSql.TableQueryAlways = "[字段编号] = 0 ";       //<>0 的是字段信息

            Pager1.PageSize = 10;

        }
        #endregion

        #region GV_Table 触发的事件，用于查看字段，建表等
        protected void GvTableRowCommand(object sender, GridViewCommandEventArgs e)
        {
            string kind = e.CommandName;

            int index = Convert.ToInt32(e.CommandArgument); // Int32.Parse(EVENTARGUMENT.Split('$')[1]);
            string id = GV_Table.Rows[index].Cells[0].Text;

            GV_Table.SelectedIndex = index;

            Heidden();

            switch (kind)
            {
                case "查看字段":
                    ShowColumnsInfo(id);
                    GV_Column.Visible = true;
                    break;

                case "建表":
                    BuildTable(id);
                    Txt_BuildTable.Visible = true;
                    Btn_add.Visible = true;
                    break;

                case "添加表":
                    AddTableInfo();
                    break;

                case "添加字段":
                    AddColumnsInfo();
                    break;
            }
        }
        #endregion

        protected void LstDataSelectedIndexChanged(object sender, EventArgs e)
        {
             string sql = "SELECT TOP 1 ConnString FROM   Manage_DataBase WHERE  (DataBaseID = {0})";

             string excelPath = Dal.DalMetadata.ExecuteString(string.Format(sql, this.lstData.SelectedValue));

            string cnString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + excelPath + "; Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            _acc = DalFactory.CreateDal(cnString, "System.Data.OleDb");

            Pager1.Dal = _acc;

            Heidden();
            GetTableName();
            Btn_TableName.SelectedIndex = 0;
        }

        #region 追加字段的备注
        protected void GV_Column_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = e.NewEditIndex;

            string col = this.GV_Column.Rows[index].Cells[2].Text;
            string colBeiZhu = this.GV_Column.Rows[index].Cells[7].Text;

            string tableName = GV_Table.SelectedRow.Cells[1].Text;
          
            string colBeiZhus = "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'{0}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'{1}'";

            string sql = string.Format(colBeiZhus, colBeiZhu,col);

             _dalCustomer.ExecuteNonQuery(sql);
            if (_dalCustomer.ErrorMessage.Length > 2)
            {
                string err = _dalCustomer.ErrorMessage;
                Response.Write(  err);
            }
            else
                Response.Write("添加备注成功！");

             

        }
        #endregion
    }
}