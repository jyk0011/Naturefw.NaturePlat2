using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Nature.Attributes;
using Nature.Data;
using Nature.UI.WebControl.BaseControl.List;

namespace NatureFramework.SupportingPlatform.Meta
{
    /// <summary>
    /// 自然框架支持平台里的添加模块节点的页面
    /// 添加新节点，同时添加默认按钮和默认视图
    /// </summary>
    /// user:jyk
    /// time:2012/9/20 19:10
    public class EntityModule
    {
        #region 属性

        #region 模块ID

        /// <summary>
        /// 模块ID
        /// </summary>
        [ColumnID(1000010)]
        public int ModuleID { get; set; }

        #endregion

        #region 模块名称
        /// <summary>
        /// 模块名称
        /// </summary>
        [ColumnID(1000060)]
        public string ModuleName { get; set; }
        #endregion

        #region 父级ID

        /// <summary>
        /// 父级ID
        /// </summary>
        [ColumnID(1000020)]
        public int ParentID { get; set; }

        #endregion

        #region 父级ID集合

        /// <summary>
        /// 父级ID集合
        /// </summary>
        [ColumnID(1000030)]
        public string ParentIDAll { get; set; }

        #endregion

        #region 模块层级

        /// <summary>
        /// 模块层级
        /// </summary>
        [ColumnID(1000070)]
        public int ModuleLevel { get; set; }

        #endregion

        #region 图标
        /// <summary>
        /// 图标
        /// </summary>
        [ColumnID(1000080)]
        public Int32 IconID { get; set; }
        #endregion

      
        #region 链接地址

        /// <summary>
        /// 链接地址
        /// </summary>
        [ColumnID(1000090)]
        public string URL { get; set; }

        #endregion

        #region 模块打开目标

        /// <summary>
        /// 模块打开目标
        /// </summary>
        [ColumnID(1000100)]
        public string Target { get; set; }

        #endregion

       
        #region 数据列表视图ID

        /// <summary>
        /// 数据列表视图ID
        /// </summary>
        [ColumnID(1000113)]
        public int GridPageViewID { get; set; }

        #endregion

        #region 查询控件视图ID

        /// <summary>
        /// 查询控件视图ID
        /// </summary>
        [ColumnID(1000115)]
        public int FindPageViewID { get; set; }

        #endregion

      
     
        #region 排序

        /// <summary>
        /// 排序
        /// </summary>
        [ColumnID(1000140)]
        public int DisOrder { get; set; }

        #endregion

    
        #endregion

        #region 函数

        #region 根据父节点信息设置子节点的基本信息

        /// <summary>
        /// 根据父节点信息设置子节点的基本信息
        /// </summary>
        /// <param name="dal">数据访问函数库的实例 </param>
        /// <param name="parentID">父节点ID</param>
        /// <param name="moduleID">用户输入的模块ID </param>
        public void SetSonNote(DataAccessLibrary dal, int parentID, string moduleID)
        {
            #region 获取数据库里的最大的ModuleID
            //因为是功能节点管理，可以限制单人操作，所以这里不考虑并发的问题。
            if (moduleID.Length == 0)
            {
                string tmpModuleID =
                    dal.ExecuteString("select top 1 ModuleID from Manage_Module order by ModuleID desc ");
                ModuleID = int.Parse(tmpModuleID);
                ModuleID = ModuleID + 1;
            }
            //this.PowerMark = this.ModuleID;
            #endregion

            #region 设置其他信息
            //提取父节点信息
            //                       0           1           2          3       4     5
            string sql = "select ParentID, ParentIDAll, ModuleLevel, DisOrder, URL, Target FROM Manage_Module WHERE (ModuleID = {0})";
            string[] moduleInfo = dal.ExecuteStringsBySingleRow(string.Format(sql,parentID));
            if (moduleInfo != null)
            {
                ParentID = parentID;                            //父节点
                ParentIDAll = moduleInfo[1] + "," + parentID;   //父节点路径
                ModuleLevel = int.Parse(moduleInfo[2]) + 1;     //节点级数
                URL = moduleInfo[4];                            //网址
                Target = moduleInfo[5];                         //目标

                GridPageViewID = int.Parse(ModuleID.ToString(CultureInfo.InvariantCulture) + "01");
                FindPageViewID = int.Parse(ModuleID.ToString(CultureInfo.InvariantCulture) + "02");

                #region 设置序号，子节点的情况
                //获取指定的节点的所有子节点的最大序号序号。如果有则+10设置，等真正保存的时候再修改后面的序号。
                //如果没有则本序号 + 10。

                sql = "select top 1 DisOrder from Manage_Module where ParentIDAll + ',' like '{0},{1},%' and DisOrder > {2} order by DisOrder desc";
                string tmpDisOrder = dal.ExecuteString(string.Format(sql,moduleInfo[1],parentID,moduleInfo[3]));
                if (tmpDisOrder != null)
                {
                    //有子节点，设置序号
                    DisOrder = Int32.Parse(tmpDisOrder) + 10;
                }
                else
                {
                    //选中的节点没有子节点
                    DisOrder = Int32.Parse(moduleInfo[3]) + 10;

                }
                #endregion

            }
            #endregion

        }
        #endregion

        #region 根据同级上一个节点的信息，设置同级下一个节点的基本信息
        /// <summary>
        /// 添加同级节点
        /// </summary>
        public void SetBortherNote(DataAccessLibrary dal, string bortherID, string moduleID)
        {
            #region 获取数据库里的最大的ModuleID
            if (moduleID.Length == 0)
            {
                string tmpModuleID =
                    dal.ExecuteString("select top 1 ModuleID from Manage_Module order by ModuleID desc ");
                ModuleID = int.Parse(tmpModuleID);
                ModuleID = ModuleID + 1;
            }
            //this.PowerMark = ModuleID;
            #endregion

            #region 设置其他信息
            //获取选中节点的信息。
            //                       0           1           2         3       4       5
            string sql = "select ParentID, ParentIDAll, ModuleLevel, DisOrder, URL, Target FROM Manage_Module WHERE (ModuleID = {0})";
            string[] funInfo = dal.ExecuteStringsBySingleRow(string.Format(sql,bortherID ));
            if (funInfo != null)
            {
                //同级节点，下列信息一致
                ParentID = int.Parse(funInfo[0]);           //父节点
                ParentIDAll = funInfo[1];                   //父节点路径
                ModuleLevel = int.Parse(funInfo[2]);        //级数
                URL = funInfo[4];                           //网址
                Target = funInfo[5];                        //目标

                GridPageViewID = int.Parse(ModuleID.ToString(CultureInfo.InvariantCulture) + "01");
                FindPageViewID = int.Parse(ModuleID.ToString(CultureInfo.InvariantCulture) + "02");

                #region 设置序号，同级节点的情况
                //获取指定节点的下一个节点的序号。如果有下一个节点，则直接设置，等真正保存的时候在修改后面的序号。
                //如果没有则用指定节点的序号 + 100 。

                //获取指定节点的下一个同级节点的序号
                sql = "select top 1 DisOrder from Manage_Module where ParentID = " + funInfo[0] + " and DisOrder > " + funInfo[3] + " order by DisOrder";
                string tmpDisOrder = dal.ExecuteString(sql);// != null:指定的节点有下一个同级节点，设置序号
                if (tmpDisOrder == null)
                {
                    //指定的节点有没有下一个同级节点，判断是否是一级节点，是的话，指定节点的序号 + 10000。
                    if (funInfo[0] == "0")
                    {
                        //一级节点
                        DisOrder = Int32.Parse(funInfo[3]) + 10000;
                    }
                    else
                    {
                        //非一级节点，寻找指定节点的子节点的最大的序号
                        sql = "select top 1 DisOrder from Manage_Module where ParentIDAll + ',' like '" + funInfo[1] + ",%' order by DisOrder desc ";
                        tmpDisOrder = dal.ExecuteString(sql);
                        if (tmpDisOrder != null)
                            DisOrder = Int32.Parse(tmpDisOrder) + 10;
                    }
                }

                
                #endregion
            }
            #endregion
        }

        #endregion

        #region 判断序号是否重复
        public bool DisOrderIsSample(DataAccessLibrary dal, string moduleID)
        {
            if (dal.ExecuteExists("select top 1 1 from Manage_Module where DisOrder =" + DisOrder))
            {
                string parentPath =
                    dal.ExecuteString("select  ParentIDAll FROM Manage_Module WHERE (ModuleID = " + moduleID +  ")");
                if (parentPath != null)
                {
                    //向后移动序号
                    dal.ExecuteNonQuery(
                        "update Manage_Module set DisOrder = DisOrder + 10 where ParentIDAll + ',' like '" + parentPath +
                        ",%'  and DisOrder >= " + DisOrder);
                }

                string err = dal.ErrorMessage;
                if (err.Length >0)
                    return false;
            }

            return true;
        }
        #endregion

        #region 添加选中的视图的默认信息
        public bool CreatePageView(DataAccessLibrary dal, MyCheckBoxList lstPageView, string tableID, string userID)
        {
            string moduleID = lstPageView.Items[0].Value.Substring(0, 3);
            
            //                                                  0      1         2      3        4      5                 6            7            8     9     
            string sql = @"insert into Manage_PageView (PVID,ModuleID,PVTypeID,PVTitle,DisOrder,AddUserid,TableID_DataSource,TableID_Modifly,PKColumnID,ColumnCount) 
                                    values ({0},{1},{2},'{3}',{4},{5},{6},{7},{8},{9})";
            var pvTypeIDs = new[] { "701", "702", "704","703", "703" };
            var pvTitles = new[] { "列表视图", "查询视图", "删除视图","添加视图", "修改视图" };

             
            int i = 1;
            foreach (ListItem item in lstPageView.Items)
            {
                if (item.Selected)
                {
                    //选择了，创建视图                                  0           1              2            3            4        5         6     7     8
                    switch (item.Text.Split('_')[1])
                    {
                        case "列表":
                            dal.ExecuteNonQuery(string.Format(sql, item.Value, moduleID, pvTypeIDs[i - 1], pvTitles[i - 1], i * 10, userID, tableID, "0"    , tableID + "010",1));
                            CreatePageTurn(dal, item.Value, tableID);
                            break;
                        case "查询":
                            dal.ExecuteNonQuery(string.Format(sql, item.Value, moduleID, pvTypeIDs[i - 1], pvTitles[i - 1], i * 10, userID, "0"     ,"0"      ,"0",3));
                            break;
                        case "删除":
                            dal.ExecuteNonQuery(string.Format(sql, item.Value, moduleID, pvTypeIDs[i - 1], pvTitles[i - 1], i * 10, userID, "0"     , tableID, tableID + "010",1));
                            break;
                        case "表单/添加":
                        case "修改":
                            dal.ExecuteNonQuery(string.Format(sql, item.Value, moduleID, pvTypeIDs[i - 1], pvTitles[i - 1], i * 10, userID, tableID, tableID, tableID + "010",1));
                            break;
                    }
                }
                i++;
            }
            return true;
        }

        private void CreatePageTurn(DataAccessLibrary dal, string pvid,string tableID)
        {
            string sql ;
            
            string orderColumns;

            sql = "select top 1 1 from Manage_Columns where TableID ={0} and ColSysName='DisOrder'";
            if (dal.ExecuteExists(string.Format(sql,tableID)))
            {
                orderColumns = "DisOrder";
            }
            else
            {
                sql = "select top 1 ColSysName from Manage_Columns where TableID ={0} order by ColumnID ";
                orderColumns = dal.ExecuteString(string.Format(sql, tableID));
            }

            sql = "select top 1 1 from Manage_Pagination where PVID=" + pvid;
            if (!dal.ExecuteExists(sql))
            {
                sql = @"insert into Manage_Pagination (PVID,OrderColumns )  values ({0},'{1}' )";
                dal.ExecuteNonQuery(string.Format(sql, pvid, orderColumns));
        
            }
        
                           
        }
        #endregion

        #region 添加选中的按钮的默认信息
        public bool CreateButton(DataAccessLibrary dal, MyCheckBoxList lst,string userID,string width,string height)
        {
            string tmpModuleID = lst.Items[0].Value.Substring(0, 3);
           
            //                                              0        1         2            3           4            5             6        7         8      9    10       11
            string sql = @"insert into Manage_ButtonBar (ButtonID,ModuleID,OpenModuleID,BtnTitle,OpenPageViewID,FindPageViewID,BtnTypeID,DisOrder,AddUserid,URL,WebWidth,WebHeight,IsNeedSelect) 
                            values ({0},{1},{2},'{3}',{4},{5},{6},{7},{8},'{9}',{10},{11},{12})";

            int i = 1;
            foreach (ListItem item in lst.Items)
            {
                string btnTitle = item.Text.Split('_')[1];
                if (item.Selected)
                {
                    //选择了，创建按钮                                
                    switch (item.Text.Split('_')[1])
                    {
                           //                                           0           1           2           3                4           5       6        7        8                  9                10      11
                        case "添加":
                            dal.ExecuteNonQuery(string.Format(sql, item.Value, tmpModuleID, tmpModuleID, btnTitle, (tmpModuleID + "04"), "0", "40" + i, i * 10, userID, "DataForm.htm", width, height, "0"));
                            break;
                        case "查看":
                        case "修改":
                            dal.ExecuteNonQuery(string.Format(sql, item.Value, tmpModuleID, tmpModuleID, btnTitle,(tmpModuleID + "04"), "0", "40" + i, i * 10, userID, "DataForm.htm", width, height,"1"));
                            break;
                        case "删除":
                            dal.ExecuteNonQuery(string.Format(sql, item.Value, tmpModuleID, tmpModuleID, btnTitle, (tmpModuleID + "03"), "0", "40" + i, i * 10, userID, "/Data/DataDelete.ashx", 0, 0,"1"));
                            break;
                        case "查询":
                            dal.ExecuteNonQuery(string.Format(sql, item.Value, tmpModuleID, tmpModuleID, btnTitle, (tmpModuleID + "02"), "0", "40" + i, i * 10, userID, "", 0, 0, "0"));
                            break;
                        case "导出Excel":
                            dal.ExecuteNonQuery(string.Format(sql, item.Value, tmpModuleID, tmpModuleID, btnTitle, "0", "0", "40" + i, i * 10, userID, "", 0, 0, "0"));
                            break;
                    }
                }
                i++;
            }
            return true;
        }
        #endregion

        #endregion

    }

}