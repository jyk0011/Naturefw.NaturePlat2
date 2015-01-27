using System;
using System.Data;
using System.Web.UI.WebControls;
using Nature.BaseWebform;

namespace NatureFramework.SupportingPlatform.Role
{
    public partial class FilterPageView : BasePageForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindPageView();

                GetFilterForPageView();
            }
        }

        #region 加载列表的页面视图
        private void BindPageView()
        {
            const string sql = @"SELECT    PVID, ModuleID, ModuleName, PVTitle, DisOrder, ParentIDAll
                            FROM     V_FU_List_FilterRoleListPV
                            ORDER BY DisOrder";

            GV.DataSource = Dal.DalCustomer.ExecuteFillDataTable(sql);
            GV.DataBind();
        }
        #endregion

        #region 提取过滤方案适应的节点
        private void GetFilterForPageView()
        {
            const string sqlSelect = "SELECT FilterCaseID,PVID FROM Role_FilterPageView WHERE FilterCaseID={0} ";
            string filterCaseID = DataID;

            DataTable dt = Dal.DalCustomer.ExecuteFillDataTable(string.Format(sqlSelect, filterCaseID));
            DataView dv = dt.DefaultView;

            int index = 0;
            foreach (GridViewRow row in GV.Rows)
            {
                var dataKey = GV.DataKeys[index];
                if (dataKey != null)
                {
                    if (!string.IsNullOrEmpty(dataKey.Value.ToString()))
                    {
                        string pvid = dataKey.Value.ToString();

                        dv.RowFilter = "PVID = " + pvid;
                        if (dv.Count > 0)
                        {
                            ((CheckBox) row.Cells[2].FindControl("chkPVID")).Checked = true;
                        }
                    }
                }
                index++;
            }

        }

        #endregion

        #region 保存过滤方案与列表页面视图
        protected void BtnSaveClick(object sender, EventArgs e)
        {
            //遍历gv，获取选择的页面视图，然后保存到数据库
            const string sqlInsert = "INSERT INTO Role_FilterPageView (FilterCaseID,PVID) values ({0},{1})";
            const string sqlDelete = "DELETE FROM Role_FilterPageView WHERE FilterCaseID={0} and PVID = {1}";
            const string sqlExtits = "SELECT top 1 1 FROM Role_FilterPageView WHERE FilterCaseID={0} and PVID = {1}";

            string filterCaseID = DataID;
            int index = 0;
            foreach (GridViewRow row in GV.Rows)
            {
                var dataKey = GV.DataKeys[index];
                if (dataKey != null)
                {
                    string pvid = dataKey.Value.ToString();
                    var chk = (CheckBox)row.Cells[2].FindControl("chkPVID");
                    if (chk.Checked )
                    {
                        //选中了，判断是否添加过，没添加就添加；添加了就不管了
                        if (!Dal.DalCustomer.ExecuteExists(string.Format(sqlExtits,filterCaseID,pvid)))
                        {
                            Dal.DalCustomer.ExecuteNonQuery(string.Format(sqlInsert,filterCaseID,pvid));
                        }
                    }
                    else
                    {
                        //没选中，删除
                        Dal.DalCustomer.ExecuteNonQuery(string.Format(sqlDelete, filterCaseID, pvid));
                    }
                }

                index++;
            }
        }
        #endregion
    }
}