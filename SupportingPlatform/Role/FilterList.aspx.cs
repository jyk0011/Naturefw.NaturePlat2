using System;
using Nature.BaseWebform;
using Nature.Common;
using Nature.Data;

namespace NatureFramework.SupportingPlatform.Role
{
    /// <summary>
    /// 过滤方案适合的列表框
    /// </summary>
    /// user:jyk
    /// time:2012/12/10 15:47
    public partial class FilterList : BasePageList
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            CtlCommonPager1.ShowDataControl = GV_Table;   //绑定显示数据的控件
            //数据访问函数库的实例，使用基类里定义的。
            CtlCommonPager1.Dal = base.Dal.DalCustomer ;

            //定义QuickPager_SQL，设置Page属性
            CtlCommonPager1.PagerSql.Page = this;
            SetPagerInfo();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
            }
        }

        #region 给QuickPager_SQL 设置属性，以便拼接SQL
        private void SetPagerInfo()
        {
            //表名或者视图名，必须设置
            CtlCommonPager1.PagerSql.TableName = " Manage_Table";              //表名或者视图名称
            CtlCommonPager1.PagerSql.TablePKColumn = "TableID";             //主键名称，不支持复合主键
            CtlCommonPager1.PagerSql.TableOrderByColumns = "TableID "; //排序字段，根据分页算法而定，可以支持多个排序字段

            //查询条件
            CtlCommonPager1.PagerSql.TableQuery = "TypeID='u ' and TableID in (select TableID from Manage_Columns where ControlTypeID in (250,252,253,254,256) AND (ControlInfo LIKE N'%\"sql\"%'))";    
            
            //默认一页20条记录
            //Pager1.PageSize = 4;                                  //一页显示的记录数

            //设置分页方式，默认是Max_TopTop
            //Pager1.PagerSQL.SetPagerSQLKind = PagerSQLKind.Max_TopTop;

        }
        #endregion

        #region 选中了一个表，显示表里面的字段（列表框类型的，且SQL填充的）
        protected void GvTableSelectedIndexChanged(object sender, EventArgs e)
        {
            //Response.Write(GV_Table.SelectedIndex);
            string tableID = GV_Table.SelectedRow.Cells[0].Text;

            string sql = "select * from Manage_Columns where TableID = " + tableID + " and ControlTypeID in (250,252,253,254,256) AND (ControlInfo LIKE N'%\"sql\"%')";
            GV_Field.DataSource = Dal.DalCustomer.ExecuteFillDataTable(sql);
            GV_Field.DataBind();

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


            //保存到列表
            string sqlInsertViewCol = "insert into [Role_FilterListItem] (FilterCaseID, ColumnID ) " +
                                      " SELECT {0} as FilterCaseID, ColumnID " +
                                      " FROM Manage_Columns   where ColumnID in (" + columnIDs + ")" +
                                      " and ColumnID not in (select ColumnID from [Role_FilterListItem]  where FilterCaseID = {0} )";

            
            //添加
            string s = string.Format(sqlInsertViewCol, ForeignID);
            Dal.DalCustomer.ExecuteNonQuery(s);


        }

        #endregion

        
    }
}