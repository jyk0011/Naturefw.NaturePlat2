using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nature.BaseWebform;
using Nature.Common;
using Nature.Data;

namespace NatureFramework.SupportingPlatform.Document
{
    /// <summary>
    /// 根据数据库信息，创建元数据
    /// </summary>
    /// user:jyk
    /// time:2013/3/19 10:38
    public partial class DataBaseToMeta : BasePageForm
    {
        private DataAccessLibrary  _dalCustomer;

        protected void Page_Load(object sender, EventArgs e)
        {
            _dalCustomer = Dal.DalCustomer;

            if (!Page.IsPostBack )
            {
                //获取数据库里最大的表编号
                string sql = @"SELECT  TOP 1 TableID FROM Manage_Table WHERE (TableID < 1000000) ORDER BY TableID DESC";

                int tableNo = _dalCustomer.ExecuteScalar<int>(sql);

                txtTableNo.Text = (tableNo + 5).ToString(CultureInfo.InvariantCulture);
                txtColNo.Text = txtTableNo.Text + "010";

            }
        }

        #region 开始创建元数据
        protected void BtnAddClick(object sender, EventArgs e)
        {
            CreateTableMeta();

            AddColumnsInfo();
        }
        #endregion

        #region 创建表的元数据
        private string  CreateTableMeta()
        {
            //获取表的信息             
            string sql = "select top 1   [name]   from sysobjects where  [id] = " + DataID ;

            string tableName = Dal.DalCustomer.ExecuteString(sql);

            //保存
            var str1 = new string[12];
            #region 定义字段
            str1[0] = "TableID";                 //主键，1000的形式
            str1[1] = "TableName";               //表名
            str1[2] = "PKColumnID";              //主键字段ID
            str1[3] = "TypeID";                  //类型
            str1[4] = "HaveTableIDs";            //如果是视图、存储过程的话，记录涉及到的表
            str1[5] = "Content";                 //表说明
            str1[6] = "ExcelTableName";          //用于修改Excel里面的信息
            str1[7] = "PDGuid";                  //PD设计文档里的GUID
            str1[8] = "DisOrder";                //PD设计文档里的序号
            str1[9] = "AddUserid";               //内部管理，记录哪个程序员添加的
            str1[10] = "AddTime";                //内部管理，记录添加日期
            str1[11] = "IsDel";                  //是否删除。0 未删除，1删除

            #endregion

            #region 取值
            var str = new string[12];

            str[0] = txtTableNo.Text.Trim().Replace("'","");         //主键，1000的形式
            str[1] = tableName;         //表名
            //主键字段的名称，默认第一个字段是主键
            str[2] = txtTableNo.Text.Trim().Replace("'", "");
            str[3] = "U";                                                   //类型
            str[5] = "";         //表说明
            str[6] = "根据数据库建立";          //工作簿，用于修改Excel里面的信息
            str[7] = "";//                                                  //PDGuid
            str[8] = "";//                                                  //DisOrder
            str[9] = base.MyUser.BaseUser.UserID;                             //AddUserid

            str[10] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");         //记录添加日期
            str[11] = "0";
            #endregion

            if (str[5] == "&nbsp;")
                str[5] = "";

            bool isAdd = true;

            #region 保存
            //检查是否已经添加过了
            string tableID = Dal.DalCustomer.ExecuteString("select top 1 TableID from Manage_Table where TableID=" + str[0]);
            if (tableID != null)
            {
                //添加过了，修改数据
                str[10] = null;
                str[9] = null;
                Dal.DalCustomer.ModifyData.UpdateData("Manage_Table", str1, str, " TableID=" + tableID);

                isAdd = false;
            }
            else
            {
                //表的描述表里面没有记录，添加记录
                Dal.DalCustomer.ModifyData.InsertData("Manage_Table", str1, str);

            }

            if (Dal.DalCustomer.ErrorMessage.Length > 2)
            {
                Response.Write(Dal.DalCustomer.ErrorMessage);
                return Dal.DalCustomer.ErrorMessage;
            }
            #endregion

            Functions.PageRegisterString(Page, isAdd ? "表信息添加成功！<br>" : "表信息修改成功！<br>");

            return "";
        }
        #endregion


        #region 添加字段的配置信息
        private void AddColumnsInfo()
        {
            //添加字段信息
            var str1 = new string[19];
            #region 定义字段
            str1[0] = "ColumnID";                   //字段编号 
            str1[1] = "TableID";                    //表ID
            str1[2] = "ColSysName";                 //字段名称
            str1[3] = "ColName";                    //对外名称
            str1[4] = "ColType";                    //字段类型
            str1[5] = "ColSize";                    //字段大小

            str1[6] = "ControlTypeID";              //控件类型
            str1[7] = "CheckTypeID";                //验证类型
            str1[8] = "CheckUserDefined";           //自定义验证
            str1[9] = "ControlInfo";                //控件描述
            str1[10] = "IsDel";                     // 
            str1[11] = "PDGuid";                    //PDGuid
            str1[12] = "DisOrder";                  //DisOrder
            str1[13] = "AddTime";                   //添加日期
            str1[14] = "AddUserid";                 //添加人

            //新增加的内容
            str1[15] = "ColumnKind";                //1：正常；2：主键；3：外键
            str1[16] = "CheckTip";                  //未通过验证的提示信息
            str1[17] = "ForeignTableID";              //ColumnKind=3有效。外键对应的表
            str1[18] = "ForeignColumnID";             //ColumnKind=3有效。外键对应的字段名

            #endregion

            //表的ID
            string tableID = txtTableNo.Text.Trim().Replace("'", "");

            //字段的ID

            var str = new string[19];
            #region 固定值
            str[1] = tableID;                                               //表的ID
            str[8] = "";                                                    //自定义验证

            str[10] = "0";                                                  //IsDel
            str[11] = "";                                                   //PDGuid
            str[12] = "";                                                   //DisOrder
            str[13] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");         //添加时间
            str[14] = base.MyUser.BaseUser.UserID;                           //添加人
            //新增加的内容
            str[17] = "";   // "ForeignTable";              //ColumnKind=3有效。外键对应的表
            str[18] = "";   //"ForeignColumn";             //ColumnKind=3有效。外键对应的字段名
              
            #endregion

            //判断是否已经添加了表的信息
            str[0] = _dalCustomer.ExecuteString("select TableID from manage_Table where tableID =" + tableID);
            if (str[0] == null)
            {
                Functions.PageRegisterAlert(Page, "请先添加表的信息。");
                return;
            }

            int i = 0;

            //开始循环添加
            string sql = "select  [id] , [ColumnName],[ColumnTypeName] ,[length],[descr]  from V_FU_List_SysColumn  where ObjectID={0} order by id";
            DataTable dt = _dalCustomer.ExecuteFillDataTable(string.Format(sql, DataID ));
            
            foreach (DataRow dr in dt.Rows)
            {
                string columnID = tableID + Functions.FillZero(((i+1)*10).ToString(CultureInfo.InvariantCulture), 3);

                #region 设置常规数据
                str[0] = columnID;
                string colSysName = dr["ColumnName"].ToString();
                string colName = dr["ColumnName"].ToString();

                str[2] = colSysName;		            			//ColSysName
                str[3] = colName;                                   //ColName 中文名称
                str[4] = dr["ColumnTypeName"].ToString();						//ColType
                str[5] = dr["length"].ToString();			            //ColSize

                str[6] = "201";                                     //控件类型
                str[7] = "101";                                     //验证类型
                //自定义验证
                str[9] = "modWidth:\"50\",modMaxLen:\"" + str[5] + "\",findWidth:\"15\",findMaxLen:\"20\"";

                //未通过验证的提示信息
                str[16] = "请填写" + colName;      //"CheckTip"; 
                #endregion

               //转换成ID

                str[15] = "1";     // "ColumnKind";                //1：正常；2：主键；3：外键

                if (i == 0)
                {
                    //第一个是主键
                    str[9] = "modWidth:\"10\",modMaxLen:\"10\",findWidth:\"10\",findMaxLen:\"10\"";
                    str[15] = "2";     // "ColumnKind";                //1：正常；2：主键；3：外键
                }
                
                i++;

                if (_dalCustomer.ExecuteExists("select top 1 1 from manage_Columns where [ColumnID]=" + columnID))
                {
                    //已经添加过了，不改动已有的记录
                    Functions.PageRegisterString(Page, columnID + "字段已经添加过了！" + "- [" + colName + "]<BR>");
                }
                else
                {
                    //添加记录
                    _dalCustomer.ModifyData.InsertData("Manage_Columns", str1, str);
                    Functions.PageRegisterString(Page, columnID + "字段添加完成！" + "- [" + colName + "]<BR>");

                }

                if (_dalCustomer.ErrorMessage.Length > 2)
                {
                    Functions.PageRegisterString(Page, _dalCustomer.ErrorMessage + "<br>");
                    return;
                }

                //j++;
            }
        }


        #endregion



    }
}