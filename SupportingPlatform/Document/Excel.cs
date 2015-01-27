using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using Nature.Common;

namespace NatureFramework.SupportingPlatform.Document
{
    /// <summary>
    /// 处理excel格式的数据库文档
    /// </summary>
    /// user:jyk
    /// time:2012/9/1 14:58
    public partial class Excel 
    {
        #region 查看选中表里的字段信息
        private void ShowColumnsInfo(string id)
        {
            string excelTableName = Btn_TableName.SelectedValue;
            //"select * from [" + excelTableName + "] where [表编号] = " + id + " and [字段编号]<>0"
            const string sql = "select * from [{0}] where [表编号] = {1} and [字段编号]<>0";
            DataTable dt = _acc.ExecuteFillDataTable(string.Format(sql, excelTableName, id));

            GV_Column.DataSource = dt;
            GV_Column.DataBind();
        }
        #endregion

        //建表
        #region 显示建表语句表
        private void BuildTable(string id)
        {
            var str = new System.Text.StringBuilder();
            //建表语句示范
            //CREATE TABLE [dbo].[LogError] (
            //	[LogID] [int] IDENTITY (1, 1) NOT NULL ,
            //	[UserCode] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
            //	[FunctionName] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
            //	[Content] [nvarchar] (300) COLLATE Chinese_PRC_CI_AS NOT NULL ,
            //	[AddedDate] [datetime] NOT NULL 
            //) ON [PRIMARY]
            //GO
            //
            //ALTER TABLE [dbo].[LogError] ADD 
            //	CONSTRAINT [DF_LogError_UserCode] DEFAULT ('') FOR [UserCode],
            //	CONSTRAINT [DF_LogError_FunctionName] DEFAULT ('') FOR [FunctionName],
            //	CONSTRAINT [DF_LogError_Content] DEFAULT ('') FOR [Content],
            //	CONSTRAINT [DF_Manage_LogError_AddedDate] DEFAULT (getdate()) FOR [AddedDate],
            //	CONSTRAINT [PK_Manage_LogError] PRIMARY KEY  CLUSTERED 
            //	(
            //		[LogID]
            //	)  ON [PRIMARY] 
            //GO
            //EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SSO_UserSSO', @level2type=N'COLUMN',@level2name=N'UserCode'
            //GO

            string tableName = GV_Table.SelectedRow.Cells[1].Text;
            #region 获取表里的字段信息
            string excelTableName = Btn_TableName.SelectedValue;
            string sql = "select * from [{0}] where [表编号] = {1} and [字段编号] <> 0 and [是否建立字段] = 1 order by [字段编号]";
            DataTable dt = _acc.ExecuteFillDataTable(string.Format(sql, excelTableName,id));
            if (_acc.ErrorMessage.Length > 2)
            {
                Functions.PageRegisterString(Page, _acc.ErrorMessage);
                return;
            }
            #endregion

            int i = 0;
            string tmp;

            //CREATE TABLE [dbo].[LogError] (
            str.Append("\nCREATE TABLE [dbo].[");
            str.Append(tableName);
            str.Append("] (");
            //	[LogID] [int] IDENTITY (1, 1) NOT NULL ,		//主键
            str.Append("\n	[");
            str.Append(dt.Rows[0]["字段名"]);

            #region 判断字段类型
            switch (dt.Rows[0]["类型"].ToString())
            {
                case "int":
                    //int 类型的主键
                    str.Append("] [int] IDENTITY (1, 1) NOT NULL ");
                    break;

                case "uniqueidentifier":
                    //GUID 类型的主键
                    str.Append("] [uniqueidentifier] NOT NULL ");
                    break;

                default:
                    //其他
                    str.Append("] [");
                    str.Append(dt.Rows[i]["类型"]);
                    str.Append("] ");

                    str.Append(" (");
                    str.Append(dt.Rows[i]["大小"]);
                    str.Append(") ");

                    str.Append(" NOT NULL ");
                    break;
            }
            #endregion

            //	[UserCode] [nvarchar] (50)  NOT NULL ,			// 其它字段

            for (i = 1; i < dt.Rows.Count; i++)
            {
                #region 字段
                tmp = dt.Rows[i]["类型"].ToString().ToLower();	//类型
                str.Append(",\n	[");
                str.Append(dt.Rows[i]["字段名"]);
                str.Append("] [");

                if (tmp.Length >= 7 && (tmp.Substring(0, 7) == "decimal" || tmp.Substring(0, 7) == "numeric"))
                    str.Append(tmp.Substring(0, 7));
                else
                    str.Append(tmp);

                str.Append("]");
                if (tmp == "int" || tmp == "smallint" || tmp == "tinyint" || tmp == "datetime" || tmp == "bit" || tmp == "ntext" || tmp == "text" || tmp == "smalldatetime" || tmp == "smallmoney" || tmp == "float" || tmp == "money" || tmp == "uniqueidentifier")
                { }
                else if (tmp.Length >= 7 && (tmp.Substring(0, 7) == "decimal" || tmp.Substring(0, 7) == "numeric"))
                {
                    str.Append(tmp.Substring(7, tmp.Length - 7));
                }
                else
                {
                    str.Append(" (");
                    str.Append(dt.Rows[i]["大小"]);
                    str.Append(") ");
                }
                str.Append(" NOT NULL ");
                #endregion
            }
            str.Append("\n) ON [PRIMARY]");
            //str.Append("\nGO");
            str.Append("\n");

            //ALTER TABLE [dbo].[LogError] ADD 
            str.Append("\nALTER TABLE [dbo].[");
            str.Append(tableName);
            str.Append("] ADD ");


            //	CONSTRAINT [DF_LogError_UserCode] DEFAULT ('') FOR [UserCode],
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0 && dt.Rows[i]["类型"].ToString() == "int")
                    //int自增的主键，不设置默认值
                    continue;

                #region 默认值
                tmp = dt.Rows[i]["默认值"].ToString().ToLower();			//默认值

                str.Append("\n	CONSTRAINT [DF_");
                str.Append(tableName);
                str.Append("_");
                str.Append(dt.Rows[i]["字段名"]);
                str.Append("] DEFAULT (");
                switch (tmp)
                {
                    case " ":
                    case "":
                    case "_":
                        str.Append("''");
                        break;
                    case "newsequentialid()":
                        str.Append("NewSequentialid()");
                        break;
                    case "getdate()":
                        str.Append("GetDate()");
                        break;
                    case "newid()":
                        str.Append("NewID()");
                        break;
                    default:
                        if (Functions.IsInt(tmp))
                        {
                            str.Append(tmp);
                        }
                        else
                        {
                            str.Append("'");
                            str.Append(tmp);
                            str.Append("'");
                        }
                        break;
                }
                str.Append(") FOR [");
                str.Append(dt.Rows[i]["字段名"]);
                str.Append("],");

                #endregion
            }

            //	CONSTRAINT [PK_Manage_LogError] PRIMARY KEY  CLUSTERED 
            #region 主键
            str.Append("\n	CONSTRAINT [PK_");
            str.Append(tableName);
            str.Append("] PRIMARY KEY  CLUSTERED ");
            str.Append("\n	(");
            str.Append("\n		[");
            str.Append(dt.Rows[0]["字段名"]);
            str.Append("]");
            str.Append("\n	)  ON [PRIMARY]\n GO ");
            #endregion

            #region 字段的备注

            string colBeiZhu = "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'{0}' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'" + tableName + "', @level2type=N'COLUMN',@level2name=N'{1}'\n GO";
            for (i = 0; i < dt.Rows.Count; i++)
            {
                str.Append("\n");
                str.Append(string.Format(colBeiZhu, dt.Rows[i]["说明"], dt.Rows[i]["字段名"]));
            }
            str.Append("\n");

            #endregion

            Txt_BuildTable.Text = str.ToString();
        }
        #endregion

        #region 建表
        protected void BtnAddClick(object sender, EventArgs e)
        {
            _dalCustomer.ExecuteNonQuery(Txt_BuildTable.Text);
            if (_dalCustomer.ErrorMessage.Length > 2)
            {
                string err = _dalCustomer.ErrorMessage;
                Response.Write(err.IndexOf("数据库中已存在名为", StringComparison.Ordinal) > 0 ? "数据库里已经有这个表了。" : err);
            }
            else
                Response.Write("建表成功！");


        }
        #endregion

        //配置信息
        #region 添加表的配置信息

        private void AddTableInfo()
        {
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

            var str = new string[12];
            #region 取值

            GridViewRow gvr = GV_Table.SelectedRow;
            str[0] = gvr.Cells[0].Text.ToString(CultureInfo.InvariantCulture);         //主键，1000的形式
            str[1] = gvr.Cells[1].Text.ToString(CultureInfo.InvariantCulture);         //表名
            //主键字段的名称，默认第一个字段是主键
            //str[2] = _acc.ExecuteString("select top 1 [字段名] from [" + Btn_TableName.SelectedValue + "] where [表编号] = " + str[0] + " and [字段编号] <> 0 order by [字段编号]"); 
            str[2] = _acc.ExecuteString("select top 1 [字段编号] from [" + Btn_TableName.SelectedValue + "] where [表编号] = " + str[0] + " and [字段编号] <> 0 order by [字段编号]"); 
            str[3] = "U";                                                   //类型
            str[5] = GV_Table.SelectedRow.Cells[2].Text.ToString(CultureInfo.InvariantCulture);         //表说明
            str[6] = Btn_TableName.SelectedValue.Replace("'", "");          //工作簿，用于修改Excel里面的信息
            str[7] = "";//                                                  //PDGuid
            str[8] = "";//                                                  //DisOrder
            str[9] = base.MyUser.BaseUser.UserID;                             //AddUserid
            
            str[10] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");         //记录添加日期
            str[11] = "";
            #endregion

            if (str[5] == "&nbsp;")
                str[5] = "";

            bool isAdd = true;

            #region 保存
            //检查是否已经添加过了
            string tableID = _dalCustomer.ExecuteString("select top 1 TableID from Manage_Table where TableID=" + str[0]);
            if (tableID != null)
            {
                //添加过了，修改数据
                str[10] = null;
                str[9] = null;
                _dalCustomer.ModifyData.UpdateData("Manage_Table", str1, str, " TableID=" + tableID);

                isAdd = false;
            }
            else
            {
                //表的描述表里面没有记录，添加记录
                _dalCustomer.ModifyData.InsertData("Manage_Table", str1, str);

            }

            if (_dalCustomer.ErrorMessage.Length > 2)
            {
                Response.Write(_dalCustomer.ErrorMessage);
                return;
            }
            #endregion

            Functions.PageRegisterString(Page, isAdd ? "表信息添加成功！" : "表信息修改成功！");
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
            string tableID = GV_Table.SelectedRow.Cells[0].Text;

            //字段的ID

            var str = new string[19];
            #region 固定值
            str[1] = tableID;                                               //表的ID
            str[8] = "";                                                    //自定义验证

            str[10] = "0";                                                  //IsDel
            str[11] = "";                                                   //PDGuid
            str[12] = "";                                                   //DisOrder
            str[13] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");         //添加时间
            str[14] = base.MyUser.BaseUser.UserID ;                           //添加人
            #endregion

            //判断是否已经添加了表的信息
            str[0] = _dalCustomer.ExecuteString("select TableID from manage_Table where tableID =" + tableID);
            if (str[0] == null)
            {
                Functions.PageRegisterAlert(Page, "请先添加表的信息。");
                return;
            }

            string excelTableName = Btn_TableName.SelectedValue;

            int i = 0;
            // int j = 0;

            //开始循环添加
            //string sql = "select * from [" + excelTableName + "] where [表编号] = " + GV_Table.SelectedRow.Cells[0].Text + " and [字段编号] <> 0 and [是否添加到配置信息] = 1 ";
            string sql = "select * from [{0}] where [表编号] = {1} and [字段编号] <> 0 and [是否添加到配置信息] = 1 order by [字段编号]";
            DataTable dt = _acc.ExecuteFillDataTable(string.Format(sql, excelTableName, GV_Table.SelectedRow.Cells[0].Text));
            foreach (DataRow dr in dt.Rows)
            {
                string columnID = tableID + Functions.FillZero(dr["字段编号"].ToString(),3);

                #region 设置常规数据
                str[0] = columnID;
                string colSysName = dr["字段名"].ToString();
                string colName = dr["中文名"].ToString();

                str[2] = colSysName;		            			//ColSysName
                str[3] = colName;                                   //ColName 中文名称
                str[4] = dr["类型"].ToString();						//ColType
                str[5] = dr["大小"].ToString();			            //ColSize
               
                str[6] = "201";                                     //控件类型
                str[7] = "101";                                     //验证类型
                //自定义验证
                str[9] = "{\"modWidth\":\"50\",\"modMaxLen\":\"" + str[5] + "\",\"findWidth\":\"15\",\"findMaxLen\":\"20\"}";

                //未通过验证的提示信息
                str[16] = "请填写" + colName;      //"CheckTip"; 
                #endregion

                //新增加的内容
                str[17] = dr["关联表"].ToString();   // "ForeignTable";              //ColumnKind=3有效。外键对应的表
                str[18] = dr["关联字段"].ToString();   //"ForeignColumn";             //ColumnKind=3有效。外键对应的字段名
                //转换成ID

                str[15] = "14";     // "ColumnKind";                //14：正常；11：主键；12：外键；13：索引

                if (i == 0)
                {
                    //第一个是主键
                    str[9] = "{\"modWidth\":\"10\",\"modMaxLen\":\"10\",\"findWidth\":\"10\",\"findMaxLen\":\"10\"}";
                    str[15] = "11";     // "ColumnKind";                //14：正常；11：主键；12：外键；13：索引

                }
                else
                {
                    //其他的判断是否设置了“关联表”，是的话设置为下拉列表框的形式，并且寻找关联表，关联字段的编号
                    if (str[17].Length > 1)
                    {
                        string jsonSql = "{\"itemType\":\"sql\",\"width\":\"0\",\"sql\":\"select {0} as id ,{{0}} as txt from {1}\",\"isChange\":\"-1\"}";
                        str[9] = string.Format(jsonSql, str[18], str[17]);
                        str[6] = "250";
                        str[15] = "12";     // "ColumnKind";                //14：正常；11：主键；12：外键；13：索引

                        str[17] = _acc.ExecuteString("select 表编号 from [" + excelTableName + "] where [字段名] = '" + str[17] + "' ");

                        if (str[17] == null)
                        {
                            //可能不在一个工作表
                            str[17] = "0";
                            str[18] = "0";
                        }
                        else
                        {
                            str[18] = _acc.ExecuteString("select [字段编号]  from [" + excelTableName + "] where [表编号] = " + str[17] + " and [字段名] = '" + str[18] + "'");
                            //寻找下一个字段
                            string tmp = _acc.ExecuteString("select top 1 [字段名] from [" + excelTableName + "] where [表编号]= " + str[17] + " and  [字段编号] > " + str[18] + " order by [字段编号]  ");
                            str[9] = string.Format(str[9], tmp);

                            //修改表编号
                            if (str[18].Length == 1)
                                str[18] = str[17] + "00" + str[18];
                            else if (str[18].Length == 2)
                                str[18] = str[17] + "0" + str[18];
                            else
                                str[18] = str[17] + str[18];

                        }

                    }
                }
                i++;

                if (_dalCustomer.ExecuteExists("select top 1 '1' from manage_Columns where [ColumnID]=" + columnID))
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

        //修改表里的字段
        #region 添加到SQL数据库。添加一个字段
        protected void GvColumnSelectedIndexChanged(object sender, EventArgs e)
        {
            var sql = new System.Text.StringBuilder(200);
            string tmp = GV_Column.SelectedRow.Cells[4].Text;
            string defaultValue = GV_Column.SelectedRow.Cells[6].Text;

            sql.Append("ALTER TABLE ");
            sql.Append(GV_Table.SelectedRow.Cells[1].Text);
            sql.Append(" ADD ");
            sql.Append(GV_Column.SelectedRow.Cells[2].Text);
            sql.Append(" ");
            sql.Append(tmp);

            if (tmp == "int" || tmp == "datetime" || tmp == "bit" || tmp == "ntext" || tmp == "text" || tmp == "smalldatetime" || tmp == "float" || tmp == "" || tmp == "" || tmp == "")
            {
                if (defaultValue.Length > 0)		//有默认值
                    sql.Append(" not ");

                sql.Append(" null ");

            }
            else if (tmp == " decimal ")
            {
                sql.Append(" (18,2) ");

                if (defaultValue.Length > 0)		//有默认值
                    sql.Append(" (18,2) not ");

                sql.Append(" null ");
            }
            else
            {
                sql.Append(" (" + GV_Column.SelectedRow.Cells[5].Text + ") ");
                if (defaultValue.Length > 0)
                    sql.Append(" not ");

                sql.Append(" null ");

            }

            //默认值
            //sql.Append(" CONSTRAINT ");
            //sql.Append(DG_Column.SelectedRow.Cells[1].Text);
            //sql.Append("Dflt  ");

            sql.Append(" DEFAULT ");


            switch (defaultValue.ToLower())
            {
                case "":

                case "_":
                    sql.Append("''");
                    break;
                case "getdate()":
                    sql.Append("GetDate()");
                    break;
                default:
                    if (Functions.IsNumeric(defaultValue))
                    {
                        //数字类型的
                        sql.Append(defaultValue);
                    }
                    else
                    {
                        sql.Append("'");
                        sql.Append(defaultValue);
                        sql.Append("'");
                    }
                    break;
            }

            sql.Append("  ");
            sql.Append(" WITH VALUES ");

            _dalCustomer.ExecuteNonQuery(sql.ToString());
            if (_dalCustomer.ErrorMessage.Length > 2)
            {
                Response.Write(_dalCustomer.ErrorMessage);

                return;
            }

            Functions.PageRegisterString(Page, "添加字段成功！");

        }

        #endregion
    }
}