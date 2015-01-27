using System;
using Nature.Common;
using Nature.BaseWebform;
using Nature.Data;
using Nature.UI.WebControl.QuickPagerSQL;

namespace NatureFramework.SupportingPlatform.Meta
{
    /// <summary>
    /// 批量添加视图里需要的字段
    /// </summary>
    /// user:jyk
    /// time:2012/9/19 10:32
    public partial class AddColumn : BasePageList
    {
        protected int IndexID = 0;
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            CtlCommonPager.ShowDataControl = GV_Table;   //绑定显示数据的控件
            //数据访问函数库的实例，使用基类里定义的。
            CtlCommonPager.Dal = Dal.DalCustomer;
            //定义QuickPager_SQL，设置Page属性
            CtlCommonPager.PagerSql.Page = this;
            SetPagerInfo();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string sql = "select ParentIDAll from Manage_Module where ModuleID =" + DataID;
                string parentIDAll = Dal.DalCustomer.ExecuteString(sql);

                if (parentIDAll != null)
                {
                    //sql = " declare @s nvarchar(200) set @s = '' ";
                    //sql += " select @s = @s + '-' + ModuleName from Manage_Module where ModuleID in (" + ParentIDAll + "," + this.DataID + ") order by DisOrder ";
                    //sql += " select @s ";

                    sql = " select  ModuleName from Manage_Module where ModuleID in (" + parentIDAll + "," + DataID + ") order by DisOrder ";
                    string title = Dal.DalCustomer.ExecuteString(sql);
                    Lbl_Title.Text = "功能节点：" + title.Trim('-');

                  
                }

                sql = "SELECT ModuleID FROM Manage_PageView where PVID ={0}";
                string pvModuleID = Dal.DalCustomer.ExecuteString(string.Format(sql, DataID));

                sql = @"SELECT  PVID as id, cast(PVID as nvarchar(100)) + '_'+ PVTitle as txt FROM Manage_PageView where  ModuleID = {0}";
                lst_PageView.DataSource = Dal.DalCustomer.ExecuteFillDataTable(string.Format(sql, pvModuleID));
                lst_PageView.DataBind();
            }
        }

        #region 给QuickPager_SQL 设置属性，以便拼接SQL
        private void SetPagerInfo()
        {
            //表名或者视图名，必须设置
            CtlCommonPager.PagerSql.TableName = " Manage_Table";              //表名或者视图名称
            //一些分页算法必须设置主键。
            CtlCommonPager.PagerSql.TablePKColumn = "TableID";             //主键名称，不支持复合主键
            //排序字段也是必须设置的，否则就无法准确分页
            CtlCommonPager.PagerSql.TableOrderByColumns = "TableID "; //排序字段，根据分页算法而定，可以支持多个排序字段

            //默认TableShowColumns是 * ，可以不设置
            //Pager1.PagerSQL.TableShowColumns = "*";                //需要显示的字段
            //没有查询条件，那就不用设置了嘛。
            //Pager1.PagerSQL.TableQuery = "";                      //查询条件

            //默认一页20条记录
            //Pager1.PageSize = 4;                                  //一页显示的记录数
            
            //设置分页方式，默认是Max_TopTop
            //CtlCommonPager.PagerSql.SetPagerSQLKind = PagerSQLKind.MaxMin;

        }
        #endregion

        #region 选中了一个表，显示表里面的字段
        protected void GvTableSelectedIndexChanged(object sender, EventArgs e)
        {
            string tableID = GV_Table.SelectedRow.Cells[0].Text;

            string sql = "select * from Manage_Columns where TableID = " + tableID;
            GV_Field.DataSource = Dal.DalCustomer.ExecuteFillDataTable(sql);
            GV_Field.DataBind();
            //Response.Write(Dal.DalCustomer.ConnectionString );
            
        }
        #endregion

        #region 添加信息
        protected void BtnListClick(object sender, EventArgs e)
        {
            //
            string columnIDs = Request.Form["chk_ColumnID"];
            #region 检查接收到的字段ID是否正确
            if (!Functions.IsIDString(columnIDs))
            {
                Response.Write("ColumnID参数不正确！");
                return;
            }
            #endregion

            //设置节点ID和用户ID
            string personID = MyUser.BaseUser.PersonID;

            //确定添加到哪里，列表、表单、查询、导出
            

            //保存到列表
            string sqlInsertViewCol = "insert into [Manage_PageViewCol] (PVID, ColumnID, DisOrder, AddUserid ) " +
                    " SELECT {0} as PVID, ColumnID,  1  as DisOrder," + personID + " as AddedPersonID " +
                    " FROM Manage_Columns   where ColumnID in (" + columnIDs + ")" +
                        " and ColumnID not in (select ColumnID from [Manage_PageViewCol]  where PVID = {0} )";

            const string sqlGetMax = "select top 1 DisOrder from [Manage_PageViewCol] where PVID = {0} order by DisOrder desc";
            const string sqlUpdate = "declare @i int set @i = {0} update [Manage_PageViewCol] set DisOrder = @i,@i = @i + 20 where PVID = {1} and DisOrder = 1 ";
                
            string pvid = lst_PageView.GetSelectedItemValue();

            foreach (string id in pvid.Split(','))
            {
                //添加
                string s = string.Format(sqlInsertViewCol, id);
                Dal.DalCustomer.ExecuteNonQuery(s);

                //修改排序
                string maxDisOrder = Dal.DalCustomer.ExecuteString(string.Format(sqlGetMax,id));

                Dal.DalCustomer.ExecuteNonQuery(string.Format(sqlUpdate, maxDisOrder,id));

                int rowCount = Dal.DalCustomer.ExecuteRowCount;

                Functions.PageRegisterString(Page, id + "添加了" + rowCount + "条记录！<br>");
            }
        }
        #endregion

        

    }
}