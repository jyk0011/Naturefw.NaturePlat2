using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Web;
using Nature.Common;
using Nature.DebugWatch;
using Nature.MetaData.Entity;
using Nature.MetaData.Enum;
using Nature.MetaData.ManagerMeta;
using Nature.Service.Ashx;

namespace Nature.Service.MetaData
{
    /// <summary>
    /// 获取元数据
    /// </summary>
    public class GetMeta : BaseAshxCrud
    {
        /// <summary>
        /// 
        /// </summary>
        /// user:jyk
        /// time:2012/10/18 17:41
        /// user:jyk
        /// time:2013/1/12 10:33
        public override void Process()
        {
            base.Process();
  
            switch (Action)
            {
                case "tree":       //树
                    BaseDebug.Title = "获取功能节点";
                    TreeMeta();
                    break;
                case "button":       //按钮
                    BaseDebug.Title = "获取操作按钮元数据";
                    ButtonMeta();
                    break;
                case "grid":       //列表
                    BaseDebug.Title = "获取列表元数据";
                    GridMeta();
                    break;
                case "find":       //查询
                    BaseDebug.Title = "获取查询元数据";
                    FindMeta();
                    break;
                case "form":       //表单
                    BaseDebug.Title = "获取表单元数据";
                    FormMeta();
                    break;
                case "tablecolumn":       //表里的字段
                    BaseDebug.Title = "获取表里的字段（id和名称）";
                    TableColumnMeta();
                    break;
                case "datachange":       //获取模块、视图、按钮的元数据（id和名称）
                    BaseDebug.Title = "获取模块、视图、按钮的元数据（id和名称）";
                    ModuleViewButtonMeta();
                    break;
            }

        }

        #region 获取树的元数据
        private void TreeMeta()
        {
            var debugInfo = new NatureDebugInfo { Title = "获取树的元数据" };
            BaseDebug.DetailList.Add(debugInfo);
            debugInfo.Remark = "";
                    

            //返回模块ID的json
            const string sql = @"SELECT   ModuleID, ParentID, ParentIDAll, ModuleName, ModuleLevel, IconID, URL, Target, IsLeaf, GridPageViewID, FindPageViewID, IsHidden, IsLock, DisOrder
                            FROM    Manage_Module
                            WHERE   ModuleLevel in (1,2)  {0} 
                            Order by disOrder";

            string query = "";  //查询条件

            string projectID = Request.QueryString["ProjectID"];
            if (!string.IsNullOrEmpty(projectID))
            {
                if (!Functions.IsIDString(projectID))
                {
                    Response.Write("projectID参数不正确！" + projectID);
                    debugInfo.Remark += "<br/>projectID参数不正确！" + projectID;
                    return;
                }
                //加上项目的查询条件
                query = " and ProjectID in (" + projectID + ")";
            }

            //判断权限
            string roleModuleID = MyUser.UserPermission.ModuleIDs;
            if ( MyUser.BaseUser.PersonID == "1")
            {
                //管理员
                debugInfo.Remark += "<br/>管理员";
            }
            else
            {
                //不是管理员，加上权限限制
                query += " and ModuleID in (" + roleModuleID + ")";
                debugInfo.Remark += "<br/>不是管理员，" + query;
            }

            string data = Dal.DalMetadata.ManagerJson.ExecuteFillJsonByColName(string.Format(sql, query));

            Response.Write(data);

            debugInfo.Stop();
          
        }
        #endregion

        #region 获取按钮的元数据
        private void ButtonMeta()
        {
            var debugInfo = new NatureDebugInfo { Title = "获取操作按钮的元数据" };

            //返回模块ID的json
            string sql =
                @"SELECT    ButtonID, ModuleID, OpenModuleID, OpenPageViewID, FindPageViewID, BtnTitle, BtnTypeID, BtnKind, URL, WebWidth, WebHeight, IsNeedSelect
                            FROM      Manage_ButtonBar
                            WHERE     (ModuleID = {0})
                            ORDER BY DisOrder";


            string data = Dal.DalMetadata.ManagerJson.ExecuteFillJsonByColNameKey(string.Format(sql, ModuleID));
            var sb = new StringBuilder(data.Length + 500);
           
            if (MyUser.BaseUser.PersonID == "1")
            {
                //超级管理员，可以访问全部的按钮
                sb.Append("\"buttonRole\":\"admin\",");
                debugInfo.Remark = "管理员";
            }
            else
            {
                //获取可以操作的按钮
                //sql = "SELECT TOP 1 ButtonIDs FROM Role_RoleButtonPV WHERE RoleID in ({0}) AND ModuleID = {1}";
                //string buttonIDs = "";      //当前用户可以访问的按钮ID集合
                //buttonIDs = Dal.DalMetadata.ExecuteString(string.Format(sql, MyUser.UserPermission.RoleIDs, ModuleID));

                string buttonIDs = MyUser.UserPermission.GetUserButtonID(ModuleID, Dal.DalMetadata);
              
                sb.Append("\"buttonRole\":[");
                sb.Append(buttonIDs);
                sb.Append("], ");
                debugInfo.Remark = "不是管理员，" + buttonIDs + "。";

            }

            sb.Append(data);
          
            Response.Write(sb.ToString());

            debugInfo.Stop();
            BaseDebug.DetailList.Add(debugInfo);

        }
        #endregion

        #region 获取列表的元数据
        private void GridMeta()
        {
            var debugInfo = new NatureDebugInfo { Title = "获取列表元数据" };

            //返回模块ID的json
            //            string sql = @"SELECT ColumnID, CASE ColTitle WHEN '' THEN ColName ELSE ColTitle END AS ColTitle, ColHelp, HelpStation, ColWidth, ColAlign, Kind, IsSort, Format, MaxLength
            //                            FROM    V_Frame_List_DataGridListCol
            //                            WHERE   (PVID = {0})
            //                            ORDER BY DisOrder";

            const string sql = @"SELECT ColumnID, ColTitle, ColName , ColHelp, HelpStation, ColWidth, ColAlign, Kind, IsSort, Format, MaxLength
                            FROM    V_Frame_List_DataGridListCol
                            WHERE   (PVID = {0})
                            ORDER BY DisOrder";

            string data = Dal.DalMetadata.ManagerJson.ExecuteFillJsonByColNameKey(string.Format(sql, MasterPageViewID));

            var sb = new StringBuilder(data.Length + 300);
           
            //设置锁定行列的字段
            #region 获取页面视图元数据
            GetPageViewMeta(MasterPageViewID, debugInfo.DetailList);

            #endregion

            sb.Append("\"LockColumns\":" + PageViewMeta.LockColumns + "");
            sb.Append(",\"LockRows\":" + PageViewMeta.LockRows + "");
            sb.Append(",\"TableWidth\":" + PageViewMeta.TableWidth + " ");
            sb.Append(",\"ViewExtend\":\"" + PageViewMeta.ViewExtend + "\" ");

            //设置可以访问的字段
            SetCanUseCol(sb, MasterPageViewID);
         
            sb.Append(data);
         
            Response.Write(sb.ToString());

            debugInfo.Stop();
            BaseDebug.DetailList.Add(debugInfo);

        }
        #endregion


    
        #region 获取查询的元数据
        private void FindMeta()
        {
            var debugInfo = new NatureDebugInfo { Title = "获取查询元数据" };

            //获取页面视图元数据
            GetPageViewMeta(FindPageViewID, debugInfo.DetailList);

            int colCount = PageViewMeta.ColumnCount;
          
            const string sql = @"SELECT  ColumnID, ColTitle , ColName , ColHelp, HelpStation, DefaultValue, ControlState, 
                                   Ser_FindKindID, ControlTypeID, ControlInfo,ClearTDStart,ClearTDEnd,TDColspan
                            FROM   V_Frame_List_BaseFormCol
                            WHERE  (PVID = {0})
                            ORDER BY DisOrder";

            Dal.DalMetadata.ManagerJson.JsonName = "controlInfo";
            string data = Dal.DalMetadata.ManagerJson.ExecuteFillJsonByColNameKey(string.Format(sql, FindPageViewID));
              
            var sb = new StringBuilder(data.Length + 3000);
            sb.Append("\"columnCount\":" + colCount);
            //设置可以访问的字段
            SetCanUseCol(sb, FindPageViewID);
            //输出数据
            sb.Append(data);
         
            //处理ControlExtend
            GetControlExtend(sb, false, FindPageViewID);
              
            Response.Write(sb.ToString());

            debugInfo.Stop();
            BaseDebug.DetailList.Add(debugInfo);

        }
        #endregion

        #region 获取表单的元数据
        private void FormMeta()
        {
            var debugInfo = new NatureDebugInfo { Title = "获取表单元数据" };

            //获取页面视图元数据
            GetPageViewMeta(MasterPageViewID, debugInfo.DetailList);
             
            const string sql = @"SELECT  ColumnID, ColTitle , ColName , ColHelp, HelpStation,
                                DefaultValue, ControlState, ControlTypeID,IsClear,ClearTDStart,ClearTDEnd,TDColspan,CheckTypeID,CheckUserDefined,CheckTip
                            FROM   V_Frame_List_BaseFormCol
                            WHERE  (PVID = {0})
                            ORDER BY DisOrder";

            Dal.DalMetadata.ManagerJson.JsonName = "controlInfo";
            string data = Dal.DalMetadata.ManagerJson.ExecuteFillJsonByColNameKey(string.Format(sql, MasterPageViewID));

            var sb = new StringBuilder(data.Length + 3000);
            sb.Append(data);
           
            GetControlExtend(sb, true, MasterPageViewID);

            //设置可以访问的字段
            SetCanUseCol(sb, MasterPageViewID);
         
            const string sql2 = @"SELECT  BtnTypeID FROM Manage_ButtonBar WHERE (ButtonID = {0})";
            var butonType = (ButonType)Dal.DalMetadata.ExecuteScalar<int>(string.Format(sql2, ButtonID));
            sb.Append("\"type\":" + (int)butonType);

            sb.Append(",\"ViewExtend\":\"" + PageViewMeta.ViewExtend + "\"");
            sb.Append(",\"isLoadCustomerJs\":\"" + PageViewMeta.LockRows + "\"");
            sb.Append(",\"columnCount\":" + PageViewMeta.ColumnCount  );

            Response.Write(sb.ToString());

            debugInfo.Stop();
            BaseDebug.DetailList.Add(debugInfo);
        }


        #endregion

        #region 获取可以访问的字段
        private void SetCanUseCol(StringBuilder sb, int pageViewID)
        {
            if (MyUser.BaseUser.PersonID == "1")
            {
                //超级管理员，可以访问全部的列
                sb.Append(",\"colRole\":\"admin\",");
            }
            else
            {
                //获取可以操作的列
                const string sql = @"SELECT TOP 1 ColumnIDs FROM Role_RoleColumn
                            WHERE RoleID in ({0}) AND ModuleID = {1}  AND PVID = {2}";

                string colIDs = "";      //当前用户可以访问的按钮ID集合
                colIDs = Dal.DalRole.ExecuteString(string.Format(sql, MyUser.UserPermission.RoleIDs, ModuleID, pageViewID));

                sb.Append(",\"colRole\":[");
                sb.Append(colIDs);
                sb.Append("] ,");

            }
        }
        #endregion


        #region 处理控件扩展信息

        #region 入口
        /// <summary>
        /// 表单控件的扩展信息
        /// </summary>
        /// <returns></returns>
        /// user:jyk
        /// time:2012/10/23 9:31
        private string GetControlExtend(StringBuilder sb, bool isForm, int pvID)
        {

            //返回模块ID的json
            const string sql = @"SELECT  ColumnID,ControlTypeID,  ControlInfo
                            FROM   V_Frame_List_BaseFormCol
                            WHERE  (PVID = {0})
                            ORDER BY DisOrder";

            DataTable dt = Dal.DalMetadata.ExecuteFillDataTable(string.Format(sql, pvID));

            sb.Append(",\"controlExtend\":{");

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("\"");
                    sb.Append(dr["ColumnID"].ToString());
                    sb.Append("\":{");

                    if (isForm)
                        SetForm(sb, dr["ControlInfo"].ToString());
                    else
                        SetFind(sb, dr["ControlInfo"].ToString());

                    if (sb[sb.Length - 1] == ',')
                    {
                        sb[sb.Length - 1] = '}';
                    }
                    else
                    {
                        sb.Append("}");
                    }

                    sb.Append(",");
                }

                sb[sb.Length - 1] = '}';
            }
            else
            {
                //没有记录
                sb.Append("}");
            }

            //读取数据
            return "";
        }

        private void SetFind(StringBuilder sb, string extend)
        {
            var jsons = Json.JsonToDictionary(extend);

            shuxing(jsons, sb, "findWidth", "size");
            shuxing(jsons, sb, "findMaxLen", "maxlen");
            shuxing(jsons, sb, "rows", "rows");
            shuxing(jsons, sb, "findWidth", "cols");
            shuxing(jsons, sb, "parameter", "my97");    //{ dateFmt: 'yyyy-MM-dd HH:mm' },
            shuxing(jsons, sb, "4", "itemSize");         //下拉框的选项显示的数量
            shuxing(jsons, sb, "6", "itemRows");             ////radioBox、checkbox 选项的列数

            shuxing(jsons, sb, "type", "type");
            shuxing(jsons, sb, "city", "city");
         

            shuxing(jsons, sb, "index", "index");     //联动列表框
            shuxing(jsons, sb, "union", "union");     //联动列表框
            shuxing(jsons, sb, "para", "para");
            shuxing(jsons, sb, "size", "size");
            shuxing(jsons, sb, "width", "width");

            shuxing(jsons, sb, "isChange", "isChange");

            //选择记录需要的模块ID和按钮ID
            shuxing(jsons, sb, "buttonID", "buttonID");
            shuxing(jsons, sb, "moduleID", "moduleID");

            shuxing(jsons, sb, "query", "query");   //打开列表的自定义url参数
 
            if (jsons.ContainsKey("itemType"))
                ListItem(jsons, sb, false);

        }

        private void SetForm(StringBuilder sb, string extend)
        {
            var jsons = Json.JsonToDictionary(extend);

            shuxing(jsons, sb, "modWidth", "size");
            shuxing(jsons, sb, "modMaxLen", "maxlen");
            shuxing(jsons, sb, "rows", "rows");
            shuxing(jsons, sb, "modWidth", "cols");
            shuxing(jsons, sb, "openWidth", "openWidth");
            shuxing(jsons, sb, "openHeight", "openHeight");

            shuxing(jsons, sb, "batchRows", "batchRows");   // 批量修改里的多行文本框的行数
            shuxing(jsons, sb, "batchWidth", "batchSize");   // 批量修改里的多行文本框的字符宽度
         
            //shuxing(jsons, sb, "event", "event");               //my97的触发事件
            shuxing(jsons, sb, "parameter", "my97");            //{ dateFmt: 'yyyy-MM-dd HH:mm' },
            shuxing(jsons, sb, "4", "itemSize");                //下拉框的选项显示的数量
            shuxing(jsons, sb, "6", "itemRows");                //radioBox、checkbox 选项的列数

            shuxing(jsons, sb, "uploadType", "uploadType");     //上传文件的上传方式 单文件、多文件等
            shuxing(jsons, sb, "fileKind", "fileKind");
            shuxing(jsons, sb, "fileExt", "fileExt");
            shuxing(jsons, sb, "editId", "editId");
            shuxing(jsons, sb, "type", "type");
            shuxing(jsons, sb, "city", "city");
          
            shuxing(jsons, sb, "index", "index");     //联动列表框
            shuxing(jsons, sb, "union", "union");     //联动列表框
            shuxing(jsons, sb, "para", "para");
            shuxing(jsons, sb, "size", "size");
            shuxing(jsons, sb, "width", "width");

            shuxing(jsons, sb, "isChange", "isChange");

            //选择记录需要的模块ID和按钮ID
            shuxing(jsons, sb, "buttonID", "buttonID");
            shuxing(jsons, sb, "moduleID", "moduleID");

            shuxing(jsons, sb, "query", "query");   //打开列表的自定义url参数

            shuxing(jsons, sb, "fun", "fun");   //计算公式，表单里的几个控件做运算，给另一个控件赋值

            if (jsons.ContainsKey("itemType"))
                ListItem(jsons, sb, true);

        }

        private void shuxing(Dictionary<string, string> json, StringBuilder sb, string key1, string key2)
        {
            if (json.ContainsKey(key1))
            {
                if (!string.IsNullOrEmpty(json[key1]))
                {
                    sb.Append("\"");
                    sb.Append(key2);
                    sb.Append("\":");
                    Json.StringToJson(json[key1],sb);
                    sb.Append(",");
                }
            }
        }

        private void ListItem(Dictionary<string, string> json, StringBuilder sb, bool isForm)
        {

            if (json.ContainsKey("sql"))
            {
                //访问数据库
                string sql = json["sql"];
                if (sql.Length > 5)
                {
                    DataTable dt = Dal.DalCustomer.ExecuteFillDataTable(sql);
                    sb.Append("\"item\":[");
                    if (!isForm)
                    {
                        sb.Append("{\"val\":\"-99999\",\"txt\":\"全部\"},");
                    }

                    if (dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            sb.Append("{\"val\":\"");
                            sb.Append(dr[0].ToString().Trim(' '));
                            sb.Append("\",");
                            sb.Append("\"txt\":\"");
                            sb.Append(dr[1]);
                            sb.Append("\"},");
                        }
                    }

                    if (sb[sb.Length - 1] == ',')
                    {
                        sb[sb.Length - 1] = ']';
                    }
                    else
                    {
                        sb.Append(']');
                    }
                }
            }

            if (json.ContainsKey("item"))
            {
                string[] items = json["item"].Split('~');
                int halfLen = items.Length / 2;
                sb.Append("\"item\":[");

                if (!isForm)
                {
                    sb.Append("{\"val\":\"-99999\",\"txt\":\"全部\"},");
                }

                for (int i = 0; i < halfLen; i++)
                {
                    sb.Append("{\"val\":\"");
                    sb.Append(items[i]);
                    sb.Append("\",");
                    sb.Append("\"txt\":\"");
                    sb.Append(items[i + halfLen]);
                    sb.Append("\"},");

                }
                if (sb[sb.Length - 1] == ',')
                {
                    sb[sb.Length - 1] = ']';
                }
                else
                {
                    sb.Append(']');
                }
            }

            if (json.ContainsKey("ajax"))
            {
                sb.Append("\"ajax\":");
                sb.Append(json["ajax"]);
                //sb.Append('}');
            }

        }

        #endregion
        #endregion


        //日志显示部分
        #region 获取表里的字段
        private void TableColumnMeta()
        {
            var debugInfo = new NatureDebugInfo { Title = "[Nature.Service.MetaData.GetMeta.TableColumnMeta]获取表里的字段" };
            BaseDebug.DetailList.Add(debugInfo);

            //获取页面视图元数据
            //GetPageViewMeta(MasterPageViewID, debugInfo.DetailList);

            var managerTableColumnMeta = new ManagerTableColumnMeta
            {
                DalCollection = Dal,
                PageViewID = ModuleID //这里ModuleID传递过来的是表id
            };

            var sb = new StringBuilder(3000);
            sb.Append("\"colMeta\":{ ");

            Dictionary<int, IColumn> dictFormColumnMeta = managerTableColumnMeta.GetMetaData(debugInfo.DetailList);

            //遍历元数据，给dic_ColumnsValue赋值——字段值
            foreach (KeyValuePair<int, IColumn> info in dictFormColumnMeta)
            {
                var colMeta = (ColumnMeta)info.Value;
                if (colMeta.ColumnKind != 15)
                {
                    sb.Append("\"");
                    sb.Append(colMeta.ColumnID);
                    sb.Append("\":\"");
                    sb.Append(colMeta.ColName);
                    sb.Append("\",");
                }
            }


            sb[sb.Length - 1] = '}';

            Response.Write(sb.ToString());

            debugInfo.Stop();
           
        }


        #endregion

        #region 获取模块、视图、按钮
        private void ModuleViewButtonMeta()
        {
            var debugInfo = new NatureDebugInfo { Title = "[Nature.Service.MetaData.GetMeta.ModuleViewButtonMeta]获取模块、视图、按钮的名称" };
            BaseDebug.DetailList.Add(debugInfo);

            #region 定义sql
            var debugInfo2 = new NatureDebugInfo { Title = "定义sql" };
          
            const string sqlModule = "SELECT ModuleID, ModuleName, ParentIDAll FROM Manage_Module ORDER BY DisOrder";
            const string sqlPageView = "SELECT PVID, PVTitle,ModuleID,PVTypeID FROM Manage_PageView ORDER BY PVID";
            const string sqlButton = "SELECT ButtonID, BtnTitle,ModuleID FROM Manage_ButtonBar ORDER BY ButtonID";
            const string sqlTable = "SELECT TableID, TableName, [Content] FROM Manage_Table ORDER BY TableID";
            debugInfo2.Stop();
            #endregion

            #region new StringBuilder(10000)
            debugInfo2 = new NatureDebugInfo { Title = "new StringBuilder(10000)" };
            debugInfo.DetailList.Add(debugInfo2);
            var sb = new StringBuilder(10000);
            debugInfo2.Stop();
            #endregion

            #region 获取Module
            debugInfo2 = new NatureDebugInfo { Title = "获取Module" };
            debugInfo.DetailList.Add(debugInfo2);

            Dal.DalMetadata.ManagerJson.JsonName = "Module";
            string json = Dal.DalMetadata.ManagerJson.ExecuteFillJsonByColNameKey(sqlModule);
            sb.Append(json);
            sb.Append(",");
            debugInfo2.Stop();
             #endregion

            #region 获取PageView
            debugInfo2 = new NatureDebugInfo { Title = "获取PageView" };
            debugInfo.DetailList.Add(debugInfo2);

            Dal.DalMetadata.ManagerJson.JsonName = "PageView";
            json = Dal.DalMetadata.ManagerJson.ExecuteFillJsonByColNameKey(sqlPageView);
            sb.Append(json);
            sb.Append(",");
            debugInfo2.Stop();
            #endregion

            #region 获取Button
            debugInfo2 = new NatureDebugInfo { Title = "获取Button" };
            debugInfo.DetailList.Add(debugInfo2);
            Dal.DalMetadata.ManagerJson.JsonName = "Button";
            json = Dal.DalMetadata.ManagerJson.ExecuteFillJsonByColNameKey(sqlButton);
            sb.Append(json);
            sb.Append(",");
            debugInfo2.Stop();
            #endregion

            #region 获取Table
            debugInfo2 = new NatureDebugInfo { Title = "获取Table" };
            debugInfo.DetailList.Add(debugInfo2);
            Dal.DalMetadata.ManagerJson.JsonName = "Table";
            json = Dal.DalMetadata.ManagerJson.ExecuteFillJsonByColNameKey(sqlTable);
            sb.Append(json);

            debugInfo2.Stop();
            #endregion

            #region Response.Write
            debugInfo2 = new NatureDebugInfo { Title = "Response.Write" };
            debugInfo.DetailList.Add(debugInfo2);

            Response.Write(sb.ToString());
            debugInfo2.Stop();
            #endregion

            debugInfo.Stop();
        }

        #endregion

    }
}