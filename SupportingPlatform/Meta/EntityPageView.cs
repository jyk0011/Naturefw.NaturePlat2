using System;
using Nature.Attributes;
using Nature.Data;
using Nature.MetaData.Enum;

namespace NatureFramework.SupportingPlatform.Meta
{
    /// <summary>
    /// 添加页面视图，设置初始值
    /// </summary>
    /// user:jyk
    /// time:2012/9/21 14:23
    public class EntityPageView
    {
        #region 属性

        #region 视图ID
        /// <summary>
        /// 视图ID
        /// </summary>
        [ColumnID(1006010)]
        public Int32 PageViewID { get ; set; } 
        #endregion
        
        #region 模块ID
        /// <summary>
        /// 模块ID
        /// </summary>
        [ColumnID(1006020)]
        public Int32 ModuleID { get ; set; } 
        #endregion
        
        #region 视图类型
        /// <summary>
        /// 视图类型
        /// </summary>
        [ColumnID(1006030)]
        public PageViewType PageViewTypeID { get; set; } 
        #endregion
        
        #region 视图标题
        /// <summary>
        /// 视图标题
        /// </summary>
        [ColumnID(1006040)]
        public string PageViewTitle { get; set; } 
        #endregion
        
        #region 读取数据用表
        /// <summary>
        /// 读取数据用表
        /// </summary>
        [ColumnID(1006050)]
        public Int32 TableIDDataSource { get ; set; } 
        #endregion
        
        #region 维护数据用表
        /// <summary>
        /// 维护数据用表
        /// </summary>
        [ColumnID(1006060)]
        public Int32 TableIDModifly { get ; set; } 
        #endregion
        
        #region 主键名
        /// <summary>
        /// 主键名
        /// </summary>
        [ColumnID(1006090)]
        public Int32 PKColumnID { get ; set; } 
        #endregion
        
        #region 外键的字段ID
        /// <summary>
        /// 外键的字段ID
        /// </summary>
        [ColumnID(1006070)]
        public Int32 ForeignColumnID { get ; set; } 
        #endregion
        
        #region 操作方式
        /// <summary>
        /// 操作方式
        /// </summary>
        [ColumnID(1006100)]
        public SQLType SqlTypeID { get; set; } 
        #endregion
        
        #region 列数
        /// <summary>
        /// 列数
        /// </summary>
        [ColumnID(1006110)]
        public Int32 ColumnCount { get ; set; } 
        #endregion
        
        #region 锁定行数
        /// <summary>
        /// 锁定行数
        /// </summary>
        [ColumnID(1006120)]
        public Int16 LockRowCount { get ; set; } 
        #endregion
        
        #region 锁定列数
        /// <summary>
        /// 锁定列数
        /// </summary>
        [ColumnID(1006130)]
        public Int16 LockColumnCount { get ; set; } 
        #endregion
        
        #region table的宽度
        /// <summary>
        /// table的宽度
        /// </summary>
        [ColumnID(1006140)]
        public Int32 TableWidth { get ; set; } 
        #endregion
        
        #region 排序
        /// <summary>
        /// 排序
        /// </summary>
        [ColumnID(1006150)]
        public Int32 DisOrder { get ; set; } 
        #endregion
        
        #endregion

        #region 函数
        #region 根据选择的表设置初始信息
        /// <summary>
        /// 设置初始信息
        /// </summary>
        /// <param name="dal">访问元数据</param>
        /// <param name="moduleID">所属模块 </param>
        public void SetInfo(DataAccessLibrary dal,int moduleID)
        {
            ModuleID = moduleID;
            
            //寻找模块里最大的视图ID
            string sql = "select top 1 PVID from Manage_PageView where ModuleID=" + moduleID;
            var tmpViewID = dal.ExecuteScalar<string>(sql);

            if (tmpViewID == null)
                PageViewID = int.Parse(moduleID + "01");
            else 
                PageViewID = int.Parse(tmpViewID) + 1;

            PageViewTitle = "";
          
            ColumnCount = 1;
            ForeignColumnID = 0;
            LockColumnCount = 0;
            LockRowCount = 0;
            PKColumnID = 0;
            SqlTypeID = SQLType.ParameterSQL;

            TableIDDataSource = 0;
            TableIDModifly = 0;
            TableWidth = 0;

        }
        #endregion
        #endregion
    }


}