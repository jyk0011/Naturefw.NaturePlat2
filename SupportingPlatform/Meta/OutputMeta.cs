using System.Data;
using System.Diagnostics;
using Nature.Common;
using Nature.Data;
using Nature.Data.Part;

namespace NatureFramework.SupportingPlatform.Meta
{
    /// <summary>
    /// 导出模块、按钮、分页、视图和视图详细的配置信息
    /// </summary>
    public partial class OutputMeta
    {
        #region 导入模块
        private string IntoModule(DataAccessLibrary dalSource, DataAccessLibrary dalTarget, DataAccessLibrary dalTargetSelect)
        {
            //提取数据
            const string sqlGvSource = @"SELECT * FROM Manage_Module where ModuleID in ({0}) order by ModuleID";

            Stopwatch sw = new Stopwatch();

            sw.Start();
            DataTable dt = dalSource.ExecuteFillDataTable(string.Format(sqlGvSource, DataIDs));
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            sw.Stop();

            txtMsg.Text += "\n==========================\n提取源数据库里的模块列表用时:" + Functions.TimeSpantoFloat(sw.Elapsed);
            //遍历确认是否有记录，没有记录则添加；有记录则修改

            sw.Reset();
            sw.Start();
            #region 定义SQL
            string sqlExists = "SELECT TOP 1 1 FROM [Manage_Module] WHERE ModuleID = {0}";
            string sqlInsert = @"INSERT INTO [Manage_Module](
                                       [ModuleID]
                                       ,[ProjectID]
                                       ,[ParentID]
                                       ,[ParentIDAll]
                                       ,[FarmIDs]
                                       ,[ModuleAloneID]
                                       ,[ModuleName]
                                       ,[PowerMark]
                                       ,[ModuleLevel]
                                       ,[IconID]
                                       ,[URL]
                                       ,[Target]
                                       ,[IsLeaf]
                                       ,[GridPageViewID]
                                       ,[FindPageViewID]
                                       ,[IsHidden]
                                       ,[IsLock]
                                       ,[DisOrder]
                                       ,[AddUserid]
                                       ,[AddTime]
                                       ,[IsDel]
                                       ,[UpdateTime]
                                       ,[UpdateUserID])
                                 VALUES(
                                        @ModuleID
                                       ,@ProjectID
                                       ,@ParentID
                                       ,@ParentIDAll
                                       ,@FarmIDs
                                       ,@ModuleAloneID
                                       ,@ModuleName
                                       ,@PowerMark
                                       ,@ModuleLevel
                                       ,@IconID
                                       ,@URL
                                       ,@Target
                                       ,@IsLeaf
                                       ,@GridPageViewID
                                       ,@FindPageViewID
                                       ,@IsHidden
                                       ,@IsLock
                                       ,@DisOrder
                                       ,@AddUserid
                                       ,@AddTime
                                       ,@IsDel
                                       ,@UpdateTime
                                       ,@UpdateUserID  ) ";

            string sqlUpdate = @"UPDATE [Manage_Module] SET 
                                    [ProjectID]= @ProjectID
                                    ,[ParentID]= @ParentID
                                    ,[ParentIDAll]= @ParentIDAll
                                    ,[FarmIDs]= @FarmIDs
                                    ,[ModuleAloneID]= @ModuleAloneID
                                    ,[ModuleName]= @ModuleName
                                    ,[PowerMark]= @PowerMark
                                    ,[ModuleLevel]= @ModuleLevel
                                    ,[IconID]= @IconID
                                    ,[URL]= @URL
                                    ,[Target]= @Target
                                    ,[IsLeaf]= @IsLeaf
                                    ,[GridPageViewID]= @GridPageViewID
                                    ,[FindPageViewID]= @FindPageViewID
                                    ,[IsHidden]= @IsHidden
                                    ,[IsLock]= @IsLock
                                    ,[DisOrder]= @DisOrder
                                    ,[AddUserid]= @AddUserid
                                    ,[AddTime]= @AddTime
                                    ,[IsDel]= @IsDel
                                    ,[UpdateTime]= @UpdateTime
                                    ,[UpdateUserID]= @UpdateUserID  
                                WHERE ModuleID = @ModuleID ";
            #endregion

            #region 定义参数
            var para = dalTarget.ManagerParameter;
            para.ClearParameter();
            para.AddNewInParameter("ModuleID", 0);
            para.AddNewInParameter("ProjectID", 0);
            para.AddNewInParameter("ParentID", 0);
            para.AddNewInParameter("ParentIDAll", "", 300);
            para.AddNewInParameter("FarmIDs", "", 300);
            para.AddNewInParameter("ModuleAloneID", 0);
            para.AddNewInParameter("ModuleName", "", 20);
            para.AddNewInParameter("PowerMark", "", 50);
            para.AddNewInParameter("ModuleLevel", 0);
            para.AddNewInParameter("IconID", 0);
            para.AddNewInParameter("URL", "", 200);
            para.AddNewInParameter("Target", "", 10);
            para.AddNewInParameter("IsLeaf", true);
            para.AddNewInParameter("GridPageViewID", 0);
            para.AddNewInParameter("FindPageViewID", 0);
            para.AddNewInParameter("IsHidden", 0);
            para.AddNewInParameter("IsLock", true);
            para.AddNewInParameter("DisOrder", 0);
            para.AddNewInParameter("AddUserid", 0);
            para.AddNewInParameter("AddTime", "", 20);
            para.AddNewInParameter("IsDel", true);
            para.AddNewInParameter("UpdateTime", "", 20);
            para.AddNewInParameter("UpdateUserID", 0);
            #endregion

            dalTarget.ManagerTran.TranBegin();

            //遍历记录，导入数据
            foreach (DataRow dr in dt.Rows)
            {
                IntoData(para, dr, dalTarget, dalTargetSelect,sqlExists, sqlUpdate, sqlInsert, "模块");
            }
            dalTarget.ManagerTran.TranCommit();

            sw.Stop();

            txtMsg.Text += "\n导入目标数据库里的模块列表用时:" + Functions.TimeSpantoFloat(sw.Elapsed);

            return "";
        }
        #endregion

        #region 导入按钮
        private string IntoButton(DataAccessLibrary dalSource, DataAccessLibrary dalTarget, DataAccessLibrary dalTargetSelect)
        {
            //提取数据
            const string sqlGvSource = @"SELECT * FROM Manage_ButtonBar where ModuleID in ({0}) order by ButtonID";

            Stopwatch sw = new Stopwatch();

            sw.Start();
            DataTable dt = dalSource.ExecuteFillDataTable(string.Format(sqlGvSource, DataIDs));
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            sw.Stop();

            txtMsg.Text += "\n\n==========================\n提取源数据库里的按钮用时:" + Functions.TimeSpantoFloat(sw.Elapsed);
            //遍历确认是否有记录，没有记录则添加；有记录则修改

            sw.Reset();
            sw.Start();
            #region 定义SQL
            string sqlDelete = string.Format("delete FROM [Manage_ButtonBar] WHERE ModuleID in ({0})",DataIDs);

            const string sqlExists = "SELECT TOP 1 1 FROM [Manage_ButtonBar] WHERE ButtonID = {0}";
            const string sqlInsert = @"INSERT INTO [Manage_ButtonBar](
                                         [ButtonID]
                                        ,[ModuleID]
                                        ,[OpenModuleID]
                                        ,[OpenPageViewID]
                                        ,[FindPageViewID]
                                        ,[BtnTitle]
                                        ,[BtnTypeID]
                                        ,[BtnKind]
                                        ,[URL]
                                        ,[WebWidth]
                                        ,[WebHeight]
                                        ,[IsNeedSelect]
                                        ,[DisOrder]
                                        ,[AddUserid]
                                        ,[AddTime]
                                        ,[IsDel]
                                        ,[UpdateTime]
                                        ,[UpdateUserID]  )
 
                                 VALUES(
                                        @ButtonID
                                       ,@ModuleID
                                       ,@OpenModuleID
                                       ,@OpenPageViewID
                                       ,@FindPageViewID
                                       ,@BtnTitle
                                       ,@BtnTypeID
                                       ,@BtnKind
                                       ,@URL
                                       ,@WebWidth
                                       ,@WebHeight
                                       ,@IsNeedSelect
                                       ,@DisOrder
                                       ,@AddUserid
                                       ,@AddTime
                                       ,@IsDel
                                       ,@UpdateTime
                                       ,@UpdateUserID
                                        ) ";

            const string sqlUpdate = @"UPDATE [Manage_ButtonBar] SET 
                                       [ModuleID]          = @ModuleID
                                      ,[OpenModuleID]      = @OpenModuleID
                                      ,[OpenPageViewID]    = @OpenPageViewID
                                      ,[FindPageViewID]    = @FindPageViewID
                                      ,[BtnTitle]          = @BtnTitle
                                      ,[BtnTypeID]         = @BtnTypeID
                                      ,[BtnKind]           = @BtnKind
                                      ,[URL]               = @URL
                                      ,[WebWidth]          = @WebWidth
                                      ,[WebHeight]         = @WebHeight
                                      ,[IsNeedSelect]      = @IsNeedSelect
                                      ,[DisOrder]          = @DisOrder
                                      ,[AddUserid]         = @AddUserid
                                      ,[AddTime]           = @AddTime
                                      ,[IsDel]             = @IsDel
                                      ,[UpdateTime]        = @UpdateTime
                                      ,[UpdateUserID]      = @UpdateUserID
                                WHERE ButtonID = @ButtonID ";
            #endregion

            #region 删除原有的按钮，2014-2-8 增加
            dalTarget.ExecuteNonQuery(sqlDelete);
            txtMsg.Text += "\n删除原有按钮数据用时:" + Functions.TimeSpantoFloat(sw.Elapsed);
            txtMsg.Text += "。删除记录：" + dalTarget.ExecuteRowCount;
            sw.Stop();
            sw.Reset();
            #endregion

            #region 定义参数
            var para = dalTarget.ManagerParameter;
            para.ClearParameter();

            para.AddNewInParameter("ButtonID", 0);
            para.AddNewInParameter("ModuleID", 0);
            para.AddNewInParameter("OpenModuleID", 0);
            para.AddNewInParameter("OpenPageViewID", 0);
            para.AddNewInParameter("FindPageViewID", 0);
            para.AddNewInParameter("BtnTitle", "", 50);
            para.AddNewInParameter("BtnTypeID", 0);
            para.AddNewInParameter("BtnKind", 0);
            para.AddNewInParameter("URL", "", 240);
            para.AddNewInParameter("WebWidth", 0);
            para.AddNewInParameter("WebHeight", 0);
            para.AddNewInParameter("IsNeedSelect",true);
            para.AddNewInParameter("DisOrder", 0);
            para.AddNewInParameter("AddUserid", 0);
            para.AddNewInParameter("AddTime", "", 20);
            para.AddNewInParameter("IsDel", true);
            para.AddNewInParameter("UpdateTime", "", 20);
            para.AddNewInParameter("UpdateUserID", 0);
            #endregion
           
            sw.Start();

            dalTarget.ManagerTran.TranBegin();

            //遍历记录，导入数据
            foreach (DataRow dr in dt.Rows)
            {
                IntoData(para, dr, dalTarget,dalTargetSelect, sqlExists, sqlUpdate, sqlInsert, "按钮");
            }
            dalTarget.ManagerTran.TranCommit();

            sw.Stop();

            txtMsg.Text += "\n导入按钮数据用时:" + Functions.TimeSpantoFloat(sw.Elapsed);

            return "";
        }
        #endregion

        #region 导入分页
        private string IntoPager(DataAccessLibrary dalSource, DataAccessLibrary dalTarget, DataAccessLibrary dalTargetSelect)
        {
            //提取数据
            const string sqlGvSource = @"SELECT * FROM [Manage_Pagination] where PVID in (
                                            SELECT PVID FROM Manage_PageView WHERE ModuleID in ({0})
                                         ) order by PVID";

            Stopwatch sw = new Stopwatch();

            sw.Start();
            DataTable dt = dalSource.ExecuteFillDataTable(string.Format(sqlGvSource, DataIDs));
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            sw.Stop();

            txtMsg.Text += "\n\n==========================\n提取源数据库里的分页用时:" + Functions.TimeSpantoFloat(sw.Elapsed);
            //遍历确认是否有记录，没有记录则添加；有记录则修改

            sw.Reset();
            sw.Start();
            #region 定义SQL
            const string sqlExists = "SELECT TOP 1 1 FROM [Manage_Pagination] WHERE [PVID] = {0}";
            const string sqlInsert = @"INSERT INTO [Manage_Pagination](
                                         [PVID]
                                        ,[OrderColumns]
                                        ,[PageSize]
                                        ,[QueryAlways]
                                        ,[Query]
                                        ,[PageTurnTypeID]
                                        ,[NaviCount]
                                       
                                        ,[DisOrder]
                                        ,[AddUserid]
                                        ,[AddTime]
                                        ,[IsDel]          )
                                 VALUES(
                                        @PVID
                                       ,@OrderColumns
                                       ,@PageSize
                                       ,@QueryAlways
                                       ,@Query
                                       ,@PageTurnTypeID
                                       ,@NaviCount
                                       ,@DisOrder
                                       ,@AddUserid
                                       ,@AddTime
                                       ,@IsDel
                                        ) ";

            const string sqlUpdate = @"UPDATE [Manage_Pagination] SET 
                                       [OrderColumns]      = @OrderColumns
                                      ,[PageSize]          = @PageSize
                                      ,[QueryAlways]       = @QueryAlways
                                      ,[Query]             = @Query
                                      ,[PageTurnTypeID]    = @PageTurnTypeID
                                      ,[NaviCount]         = @NaviCount
                                   
                                      ,[DisOrder]          = @DisOrder
                                      ,[AddUserid]         = @AddUserid
                                      ,[AddTime]           = @AddTime
                                      ,[IsDel]             = @IsDel
                                WHERE PVID = @PVID ";
            #endregion

            #region 定义参数
            var para = dalTarget.ManagerParameter;
            para.ClearParameter();

            para.AddNewInParameter("PVID", 0);
            para.AddNewInParameter("OrderColumns","", 200);
            para.AddNewInParameter("PageSize", 0);
            para.AddNewInParameter("QueryAlways", "", 300);
            para.AddNewInParameter("Query", "", 300);
            para.AddNewInParameter("PageTurnTypeID", 0);
            para.AddNewInParameter("NaviCount", 0);
            
            para.AddNewInParameter("DisOrder", 0);
            para.AddNewInParameter("AddUserid", 0);
            para.AddNewInParameter("AddTime", "", 20);
            para.AddNewInParameter("IsDel", true);
            #endregion

            dalTarget.ManagerTran.TranBegin();

            //遍历记录，导入数据
            foreach (DataRow dr in dt.Rows)
            {
                IntoData(para, dr, dalTarget,dalTargetSelect, sqlExists, sqlUpdate, sqlInsert, "分页信息");
            }
            dalTarget.ManagerTran.TranCommit();

            sw.Stop();

            txtMsg.Text += "\n导入分页信息数据用时:" + Functions.TimeSpantoFloat(sw.Elapsed);

            return "";

        }
        #endregion

        #region 导入视图

        private string IntoPV(DataAccessLibrary dalSource, DataAccessLibrary dalTarget,
                              DataAccessLibrary dalTargetSelect)
        {
            //提取数据
            const string sqlGvSource = @"SELECT * FROM [Manage_PageView] where ModuleID in ({0}) order by PVID";

            Stopwatch sw = new Stopwatch();

            sw.Start();
            DataTable dt = dalSource.ExecuteFillDataTable(string.Format(sqlGvSource, DataIDs));
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            sw.Stop();

            txtMsg.Text += "\n\n==========================\n提取源数据库里的视图用时:" + Functions.TimeSpantoFloat(sw.Elapsed);
            //遍历确认是否有记录，没有记录则添加；有记录则修改

            sw.Reset();
            sw.Start();

            #region 定义SQL
            string sqlDelete = string.Format("delete FROM [Manage_PageView] WHERE ModuleID in ({0})", DataIDs);
            const string sqlExists = "SELECT TOP 1 1 FROM [Manage_PageView] WHERE [PVID] = {0}";
            const string sqlInsert = @"INSERT INTO [Manage_PageView](
                                         [PVID]
                                        ,[ModuleID]
                                        ,[PVTypeID]
                                        ,[PVTitle]
                                        ,[TableID_DataSource]
                                        ,[TableID_Modifly]
                                        ,[PKColumnID]
                                        ,[ForeignColumnID]
                                        ,[T_SQLTypeID]
                                        ,[ViewExtend]
                                        ,[ColumnCount]
                                        ,[LockRowCount]
                                        ,[LockColumnCount]
                                        ,[TableWidth]
                                        ,[DisOrder]
                                        ,[AddUserid]
                                        ,[AddTime]
                                        ,[IsDel]
                                        ,[UpdateTime]
                                        ,[UpdateUserID]       )
                                 VALUES(
                                        @PVID
                                       ,@ModuleID
                                       ,@PVTypeID
                                       ,@PVTitle
                                       ,@TableID_DataSource
                                       ,@TableID_Modifly
                                       ,@PKColumnID
                                       ,@ForeignColumnID
                                       ,@T_SQLTypeID
                                       ,@ViewExtend
                                       ,@ColumnCount
                                       ,@LockRowCount
                                       ,@LockColumnCount
                                       ,@TableWidth
                                       ,@DisOrder
                                       ,@AddUserid
                                       ,@AddTime
                                       ,@IsDel
                                       ,@UpdateTime
                                       ,@UpdateUserID
                                        ) ";

            const string sqlUpdate = @"UPDATE [Manage_PageView] SET 
                                      [ModuleID]          = @ModuleID
                                     ,[PVTypeID]          = @PVTypeID
                                     ,[PVTitle]           = @PVTitle
                                     ,[TableID_DataSource]= @TableID_DataSource
                                     ,[TableID_Modifly]   = @TableID_Modifly
                                     ,[PKColumnID]        = @PKColumnID
                                     ,[ForeignColumnID]   = @ForeignColumnID
                                     ,[T_SQLTypeID]       = @T_SQLTypeID
                                     ,[ViewExtend]        = @ViewExtend
                                     ,[ColumnCount]       = @ColumnCount
                                     ,[LockRowCount]      = @LockRowCount
                                     ,[LockColumnCount]   = @LockColumnCount
                                     ,[TableWidth]        = @TableWidth
                                   
                                     ,[DisOrder]          = @DisOrder
                                     ,[AddUserid]         = @AddUserid
                                     ,[AddTime]           = @AddTime
                                     ,[IsDel]             = @IsDel
                                     ,[UpdateTime]        = @UpdateTime
                                     ,[UpdateUserID]      = @UpdateUserID
                                WHERE PVID = @PVID ";

            #endregion

            #region 删除原有的视图，2014-2-8 增加
            dalTarget.ExecuteNonQuery(sqlDelete);
            txtMsg.Text += "\n删除原有视图数据用时:" + Functions.TimeSpantoFloat(sw.Elapsed);
            txtMsg.Text += "。删除记录：" + dalTarget.ExecuteRowCount;
            sw.Stop();
            sw.Reset();
            #endregion

            #region 定义参数

            var para = dalTarget.ManagerParameter;
            para.ClearParameter();

            para.AddNewInParameter("PVID", 0);
            para.AddNewInParameter("ModuleID", 0);
            para.AddNewInParameter("PVTypeID", 0);
            para.AddNewInParameter("PVTitle", "", 20);
            para.AddNewInParameter("TableID_DataSource", "", 20);
            para.AddNewInParameter("TableID_Modifly", 0);
            para.AddNewInParameter("PKColumnID", 0);
            para.AddNewInParameter("ForeignColumnID", 0);
            para.AddNewInParameter("T_SQLTypeID", 0);
            para.AddNewInParameter("ViewExtend", "", 500);
            para.AddNewInParameter("ColumnCount", 0);
            para.AddNewInParameter("LockRowCount", 0);
            para.AddNewInParameter("LockColumnCount", 0);
            para.AddNewInParameter("TableWidth", 0);

            para.AddNewInParameter("DisOrder", 0);
            para.AddNewInParameter("AddUserid", 0);
            para.AddNewInParameter("AddTime", "", 20);
            para.AddNewInParameter("IsDel", true);
            para.AddNewInParameter("UpdateTime", "", 20);
            para.AddNewInParameter("UpdateUserID", 0);

            #endregion

            sw.Start();

            dalTarget.ManagerTran.TranBegin();

            //遍历记录，导入数据
            foreach (DataRow dr in dt.Rows)
            {
                IntoData(para, dr, dalTarget, dalTargetSelect, sqlExists, sqlUpdate, sqlInsert, "视图");
            }
            dalTarget.ManagerTran.TranCommit();

            sw.Stop();

            txtMsg.Text += "\n导入视图数据用时:" + Functions.TimeSpantoFloat(sw.Elapsed);

            return "";

        }

        #endregion

        #region 导入视图详细
        private string IntoPVCol(DataAccessLibrary dalSource, DataAccessLibrary dalTarget, DataAccessLibrary dalTargetSelect)
        {

            //DataAccessLibrary dal = DalFactory.CreateDal(dalTarget.Command.Connection.ConnectionString ,dalTarget.Command.Connection.)

            //提取数据
            const string sqlGvSource = @"SELECT * FROM [Manage_PageViewCol] where PVID in (
                                            SELECT PVID FROM Manage_PageView WHERE ModuleID in ({0})
                                         ) order by PVColID";

            Stopwatch sw = new Stopwatch();

            sw.Start();
            DataTable dt = dalSource.ExecuteFillDataTable(string.Format(sqlGvSource, DataIDs));
            if (dt.Rows.Count == 0)
            {
                return "0";
            }
            sw.Stop();

            txtMsg.Text += "\n\n==========================\n提取源数据库里的视图详细用时:" + Functions.TimeSpantoFloat(sw.Elapsed);
            //遍历确认是否有记录，没有记录则添加；有记录则修改

            sw.Reset();
            sw.Start();
            #region 定义SQL
            string sqlDelete = string.Format("delete FROM [Manage_PageViewCol] WHERE [PVID] in ( SELECT PVID FROM Manage_PageView WHERE ModuleID in ({0})) ", DataIDs);

            const string sqlExists = "SELECT TOP 1 PVColID FROM [Manage_PageViewCol] WHERE [PVID] = {0} AND ColumnID = {1}";
            const string sqlInsert = @"INSERT INTO [Manage_PageViewCol](
                                         [PVID]
                                        ,[ColumnID]
                                        ,[ColTitle]
                                        ,[ColHelp]
                                        ,[HelpStation]
                                        ,[DefaultValue]
                                        ,[ClearTDStart]
                                        ,[ClearTDEnd]
                                        ,[TDColspan]
                                        ,[ColWidth]
                                        ,[ColAlign]
                                        ,[ControlState]
                                        ,[Ser_IsSave]
                                        ,[IsClear]
                                        ,[Kind]
                                        ,[IsSort]
                                        ,[Format]
                                        ,[MaxLength]
                                        ,[Ser_FindKindID]
                                        ,[Ser_CustomerFindKind]
                                        ,[Json]
                                       
                                        ,[DisOrder]
                                        ,[AddUserid]
                                        ,[AddTime]
                                        ,[IsDel]          )
                                 VALUES(
                                        @PVID
                                       ,@ColumnID
                                       ,@ColTitle
                                       ,@ColHelp
                                       ,@HelpStation
                                       ,@DefaultValue
                                       ,@ClearTDStart
                                       ,@ClearTDEnd
                                       ,@TDColspan
                                       ,@ColWidth
                                       ,@ColAlign
                                       ,@ControlState
                                       ,@Ser_IsSave
                                       ,@IsClear
                                       ,@Kind
                                       ,@IsSort
                                       ,@Format
                                       ,@MaxLength
                                       ,@Ser_FindKindID
                                       ,@Ser_CustomerFindKind
                                       ,@Json
                                       
                                       ,@DisOrder
                                       ,@AddUserid
                                       ,@AddTime
                                       ,@IsDel
                                        ) ";

            const string sqlUpdate = @"UPDATE [Manage_PageViewCol] SET 
                                       [ColTitle]      = @ColTitle
                                      ,[ColHelp]      = @ColHelp
                                      ,[HelpStation]      = @HelpStation
                                      ,[DefaultValue]      = @DefaultValue
                                      ,[ClearTDStart]      = @ClearTDStart
                                      ,[ClearTDEnd]      = @ClearTDEnd
                                      ,[TDColspan]      = @TDColspan
                                      ,[ColWidth]      = @ColWidth
                                      ,[ColAlign]      = @ColAlign
                                      ,[ControlState]      = @ControlState
                                      ,[Ser_IsSave]      = @Ser_IsSave
                                      ,[IsClear]      = @IsClear
                                      ,[Kind]      = @Kind
                                      ,[IsSort]      = @IsSort
                                      ,[Format]      = @Format
                                      ,[MaxLength]      = @MaxLength
                                      ,[Ser_FindKindID]      = @Ser_FindKindID
                                      ,[Ser_CustomerFindKind]      = @Ser_CustomerFindKind
                                      ,[Json]      = @Json
                                      
                                      ,[DisOrder]          = @DisOrder
                                      ,[AddUserid]         = @AddUserid
                                      ,[AddTime]           = @AddTime
                                      ,[IsDel]             = @IsDel
                                     
                                WHERE PVColID = @PVColID ";
            #endregion

            #region 删除原有的视图里的字段，2014-2-8 增加
            dalTarget.ExecuteNonQuery(sqlDelete);
            txtMsg.Text += "\n删除原有视图里的字段数据用时:" + Functions.TimeSpantoFloat(sw.Elapsed);
            txtMsg.Text += "。删除记录：" + dalTarget.ExecuteRowCount;
            sw.Stop();
            sw.Reset();
            #endregion

            #region 定义参数
            var para = dalTarget.ManagerParameter;
            para.ClearParameter();

            para.AddNewInParameter("PVColID", 0);
            para.AddNewInParameter("PVID", 0);
            para.AddNewInParameter("ColumnID", 0);
            para.AddNewInParameter("ColTitle","",30);
            para.AddNewInParameter("ColHelp", "", 100);
            para.AddNewInParameter("HelpStation", 0);
            para.AddNewInParameter("DefaultValue", "",50);
            para.AddNewInParameter("ClearTDStart", 0);
            para.AddNewInParameter("ClearTDEnd", 0);
            para.AddNewInParameter("TDColspan", 0);
            para.AddNewInParameter("ColWidth", 0);
            para.AddNewInParameter("ColAlign", "", 10);
            para.AddNewInParameter("ControlState", 0);
            para.AddNewInParameter("Ser_IsSave", 0);
            para.AddNewInParameter("IsClear", true);
            para.AddNewInParameter("Kind", 0);
            para.AddNewInParameter("IsSort", true);
            para.AddNewInParameter("Format", "",800);
            para.AddNewInParameter("MaxLength", 0);
            para.AddNewInParameter("Ser_FindKindID", 0);
            para.AddNewInParameter("Ser_CustomerFindKind", "", 255);
            para.AddNewInParameter("Json","",1000);
            
            para.AddNewInParameter("DisOrder", 0);
            para.AddNewInParameter("AddUserid", 0);
            para.AddNewInParameter("AddTime", "", 20);
            para.AddNewInParameter("IsDel", true);
            #endregion

            sw.Start();

            //dalTarget.ManagerTran.TranBegin();

            //遍历记录，导入数据
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < para.Count; i++)
                {
                    //para[i].Value = dr[para[i].ParameterName.Replace("@", "")].ToString();

                    var parameterName = para[i].ParameterName.Replace("@", "");

                    switch (parameterName)
                    {
                        case "AddTime":
                        case "UpdateTime":
                            para[i].Value = System.Convert.ToDateTime(dr[parameterName]).ToString("yyyy-MM-dd HH:mm:ss");
                            break;

                        default:
                            para[i].Value = dr[parameterName].ToString();
                            break;
                    }
                }

                #region 添加或者修改

                string pvColID = dalTargetSelect.ExecuteString(string.Format(sqlExists, dr[1], dr[2]));

                if (!string.IsNullOrEmpty(pvColID))
                {
                    //有记录，修改
                    para["PVColID"].Value = pvColID;
                    dalTarget.ExecuteNonQuery(sqlUpdate);
                    txtMsg.Text += "\n修改一条视图详细记录:" + pvColID + "_" + dr[1] + "_" + dr[2];

                }
                else
                {
                    //没有记录，添加
                    dalTarget.ExecuteNonQuery(sqlInsert);
                    txtMsg.Text += "\n添加一条视图详细记录:" + dr[1] + "_" + dr[2];

                }

                //判断是否出错
                if (dalTarget.ErrorMessage.Length > 1)
                {
                    //导入数据时出错
                    txtMsg.Text += "\n导入视图详细信息的时候出错！" + dr[0];
                    txtMsg.Text += "\n" + dalTarget.ErrorMessage + "\n==========\n";
                    WritePara(para);
                }
                #endregion
            }
            
            //dalTarget.ManagerTran.TranCommit();

            sw.Stop();

            txtMsg.Text += "\n导入视图详细用时:" + Functions.TimeSpantoFloat(sw.Elapsed);

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
        private void IntoData(ManagerParameter para, DataRow dr, DataAccessLibrary dalTarget,DataAccessLibrary dalTargetSelect, string sqlExists, string sqlUpdate, string sqlInsert,string title)
        {
            for (int i = 0; i < para.Count; i++)
            {
                var parameterName = para[i].ParameterName.Replace("@", "");

                switch (parameterName)
                {
                    case "AddTime":
                    case "UpdateTime":
                        para[i].Value = System.Convert.ToDateTime(dr[parameterName]).ToString("yyyy-MM-dd HH:mm:ss");
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