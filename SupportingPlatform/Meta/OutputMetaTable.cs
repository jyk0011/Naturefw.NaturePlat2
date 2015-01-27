using System.Data;
using System.Diagnostics;
using Nature.Common;
using Nature.Data;
using Nature.Data.Part;

namespace NatureFramework.SupportingPlatform.Meta
{
    public partial class OutputMetaTable
    {
        #region 导入表
        private string IntoTable(DataAccessLibrary dalSource, DataAccessLibrary dalTarget, DataAccessLibrary dalTargetSelect)
        {
            //提取数据
            const string sqlGvSource = @"SELECT * FROM [Manage_Table] where [TableID] in ({0}) order by [TableID]";

            Stopwatch sw = new Stopwatch();

            sw.Start();
            DataTable dt = dalSource.ExecuteFillDataTable(string.Format(sqlGvSource, DataIDs));
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            sw.Stop();

            txtMsg.Text += "\n==========================\n提取源数据库里的“表”用时:" + Functions.TimeSpantoFloat(sw.Elapsed);
            //遍历确认是否有记录，没有记录则添加；有记录则修改

            sw.Reset();
            sw.Start();
            #region 定义SQL
            const string sqlExists = "SELECT TOP 1 1 FROM [Manage_Table] WHERE TableID = {0}";
            const string sqlInsert = @"INSERT INTO [Manage_Table](
                                        [TableID]
                                       ,[TableName]
                                       ,[PKColumnID]
                                       ,[TypeID]
                                       ,[HaveTableIDs]
                                       ,[Content]
                                       ,[ExcelTableName]
                                       ,[PDGuid]
                                       ,[DisOrder]
                                       ,[AddUserid]
                                       ,[AddTime]
                                       ,[IsDel]              )
                                 VALUES(
                                        @TableID
                                       ,@TableName
                                       ,@PKColumnID
                                       ,@TypeID
                                       ,@HaveTableIDs
                                       ,@Content
                                       ,@ExcelTableName
                                       ,@PDGuid
                                       ,@DisOrder
                                       ,@AddUserid
                                       ,@AddTime
                                       ,@IsDel 
                                        ) ";

            const string sqlUpdate = @"UPDATE [Manage_Table] SET 
                                     [TableName]= @TableName
                                    ,[PKColumnID]= @PKColumnID
                                    ,[TypeID]= @TypeID
                                    ,[HaveTableIDs]= @HaveTableIDs
                                    ,[Content]= @Content
                                    ,[ExcelTableName]= @ExcelTableName
                                    ,[PDGuid]= @PDGuid
                                    ,[DisOrder]= @DisOrder
                                    ,[AddUserid]= @AddUserid
                                    ,[AddTime]= @AddTime
                                    ,[IsDel]= @IsDel
                                    
                                WHERE TableID = @TableID ";
            #endregion

            #region 定义参数
            var para = dalTarget.ManagerParameter;
            para.ClearParameter();
            para.AddNewInParameter("TableID", 0);
            para.AddNewInParameter("TableName","",60);
            para.AddNewInParameter("PKColumnID", 0);
            para.AddNewInParameter("TypeID", "",2);
            para.AddNewInParameter("HaveTableIDs", "",500);
            para.AddNewInParameter("Content", "", 50);
            para.AddNewInParameter("ExcelTableName", "",50);
            para.AddNewInParameter("PDGuid", "", 36);
            para.AddNewInParameter("DisOrder", 0);
            para.AddNewInParameter("AddUserid", 0);
            para.AddNewInParameter("AddTime", "", 20);
            para.AddNewInParameter("IsDel", true);
            
            #endregion

            dalTarget.ManagerTran.TranBegin();

            //遍历记录，导入数据
            foreach (DataRow dr in dt.Rows)
            {
                IntoData(para, dr, dalTarget, dalTargetSelect, sqlExists, sqlUpdate, sqlInsert, "表");
            }
            dalTarget.ManagerTran.TranCommit();

            sw.Stop();

            txtMsg.Text += "\n导入表用时:" + Functions.TimeSpantoFloat(sw.Elapsed);

            return "";
        }
        #endregion

        #region 导入字段
        private string IntoColumn(DataAccessLibrary dalSource, DataAccessLibrary dalTarget, DataAccessLibrary dalTargetSelect)
        {
            //提取数据
            const string sqlGvSource = @"SELECT * FROM [Manage_Columns] where [TableID] in ({0}) order by [TableID]";

            Stopwatch sw = new Stopwatch();

            sw.Start();
            DataTable dt = dalSource.ExecuteFillDataTable(string.Format(sqlGvSource, DataIDs));
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            sw.Stop();

            txtMsg.Text += "\n==========================\n提取源数据库里的“字段”用时:" + Functions.TimeSpantoFloat(sw.Elapsed);
            //遍历确认是否有记录，没有记录则添加；有记录则修改

            sw.Reset();
            sw.Start();
            #region 定义SQL
            const string sqlExists = "SELECT TOP 1 1 FROM [Manage_Columns] WHERE ColumnID = {0}";
            const string sqlInsert = @"INSERT INTO [Manage_Columns](
                                        [ColumnID]
                                       ,[TableID]
                                       ,[ColumnKind]
                                       ,[ColSysName]
                                       ,[ColName]
                                       ,[ColType]
                                       ,[ColSize]
                                       ,[ControlTypeID]
                                       ,[CheckTypeID]
                                       ,[CheckUserDefined]
                                       ,[CheckTip]
                                       ,[ControlInfo]
                                       ,[ForeignTableID]
                                       ,[ForeignColumnID]
                                       ,[PDGuid]
                                       ,[DisOrder]
                                       ,[AddUserid]
                                       ,[AddTime]
                                       ,[IsDel]              )
                                 VALUES(
                                        @ColumnID
                                       ,@TableID
                                       ,@ColumnKind
                                       ,@ColSysName
                                       ,@ColName
                                       ,@ColType
                                       ,@ColSize
                                       ,@ControlTypeID
                                       ,@CheckTypeID
                                       ,@CheckUserDefined
                                       ,@CheckTip
                                       ,@ControlInfo
                                       ,@ForeignTableID
                                       ,@ForeignColumnID
                                       ,@PDGuid
                                       ,@DisOrder
                                       ,@AddUserid
                                       ,@AddTime
                                       ,@IsDel 
                                        ) ";

            const string sqlUpdate = @"UPDATE [Manage_Columns] SET 
                                     [TableID]= @TableID
                                    ,[ColumnKind]= @ColumnKind
                                    ,[ColSysName]= @ColSysName
                                    ,[ColName]= @ColName
                                    ,[ColType]= @ColType
                                    ,[ColSize]= @ColSize
                                    ,[ControlTypeID]= @ControlTypeID
                                    ,[CheckTypeID]= @CheckTypeID
                                    ,[CheckUserDefined]= @CheckUserDefined
                                    ,[CheckTip]= @CheckTip
                                    ,[ControlInfo]= @ControlInfo
                                    ,[ForeignTableID]= @ForeignTableID
                                    ,[ForeignColumnID]= @ForeignColumnID
                                    ,[PDGuid]= @PDGuid
                                    ,[DisOrder]= @DisOrder
                                    ,[AddUserid]= @AddUserid
                                    ,[AddTime]= @AddTime
                                    ,[IsDel]= @IsDel
                                    
                                WHERE ColumnID = @ColumnID ";
            #endregion

            #region 定义参数
            var para = dalTarget.ManagerParameter;
            para.ClearParameter();
            para.AddNewInParameter("ColumnID", 0);
            para.AddNewInParameter("TableID", 0);
            para.AddNewInParameter("ColumnKind", 0);
            para.AddNewInParameter("ColSysName","",50);
            para.AddNewInParameter("ColName","",50);
            para.AddNewInParameter("ColType", "", 20);
            para.AddNewInParameter("ColSize", 0);
            para.AddNewInParameter("ControlTypeID", 0);
            para.AddNewInParameter("CheckTypeID", 0);
            para.AddNewInParameter("CheckUserDefined", "", 50);
            para.AddNewInParameter("CheckTip", "", 255);
            para.AddNewInParameter("ControlInfo", "", 500);
            para.AddNewInParameter("ForeignTableID", 0);
            para.AddNewInParameter("ForeignColumnID", 0);
            para.AddNewInParameter("PDGuid", "", 36);
            para.AddNewInParameter("DisOrder", 0);
            para.AddNewInParameter("AddUserid", 0);
            para.AddNewInParameter("AddTime", "", 20);
            para.AddNewInParameter("IsDel", true);

            #endregion

            dalTarget.ManagerTran.TranBegin();

            //遍历记录，导入数据
            foreach (DataRow dr in dt.Rows)
            {
                IntoData(para, dr, dalTarget, dalTargetSelect, sqlExists, sqlUpdate, sqlInsert, "字段");
            }

            dalTarget.ManagerTran.TranCommit();

            sw.Stop();

            txtMsg.Text += "\n导入字段用时:" + Functions.TimeSpantoFloat(sw.Elapsed);

            return "";
        }
        #endregion


        #region 遍历参数，输出参数名称和参数值
        private void WritePara(ManagerParameter para)
        {
            for (int i = 0; i < para.Count; i++)
            {
                txtMsg.Text += "\n" + para[i].ParameterName + ":" + para[i].Value;
            }
        }
        #endregion

        #region 遍历参数，从dr里赋值，并且实现添加或者修改
        private void IntoData(ManagerParameter para, DataRow dr, DataAccessLibrary dalTarget, DataAccessLibrary dalTargetSelect, string sqlExists, string sqlUpdate, string sqlInsert, string title)
        {
            for (int i = 0; i < para.Count; i++)
            {
                var parameterName = para[i].ParameterName.Replace("@", "");

                switch (parameterName)
                {
                    case "AddTime":
                    case "UpdateTime":
                        para[i].Value =  System.Convert.ToDateTime(dr[parameterName]).ToString("yyyy-MM-dd HH:mm:ss");
                        break;

                    default:
                        para[i].Value = dr[parameterName].ToString();
                        break;
                }
            }

            #region 添加或者修改
            if (dalTargetSelect.ExecuteExists(string.Format(sqlExists, dr[0])))
            {
                //有记录，修改
                dalTarget.ExecuteNonQuery(sqlUpdate);
                txtMsg.Text += "\n修改一条" + title + "记录:" + dr[0];

            }
            else
            {
                //没有记录，添加
                dalTarget.ExecuteNonQuery(sqlInsert);
                txtMsg.Text += "\n添加一条" + title + "记录:" + dr[0];

            }

            //判断是否出错
            if (dalTarget.ErrorMessage.Length > 1)
            {
                //导入数据时出错
                txtMsg.Text += "\n导入" + title + "信息的时候出错！" + dr[0];
                txtMsg.Text += "\n" + dalTarget.ErrorMessage + "\n==========\n";
                WritePara(para);
            }
            #endregion
        }
        #endregion
   
    }
}