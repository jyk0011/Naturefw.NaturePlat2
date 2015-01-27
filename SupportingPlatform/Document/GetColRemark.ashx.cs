using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web;
using Nature.Data;
using Nature.Service;

namespace NatureFramework.SupportingPlatform.Document
{
    /// <summary>
    /// 获取指定字段的说明，从excel文档
    /// </summary>
    public class GetColRemark : BaseAshxCrud
    {

        public override void Process()
        {
            base.Process();

            DataAccessLibrary dal = GetDal();
             
            var oleConn = (OleDbConnection)dal.Command.Connection;
            oleConn.Open();
            DataTable  dtExcelSchema = oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            oleConn.Close();

            string colId = Request["id"];

          
            string remarks = "{";
            

            if (dtExcelSchema != null)
            {
                string[] ids = colId.Split(',');

                int index = 0;
                foreach (string id in ids)
                {
                    string remark = "";
                    string tableId = id.Substring(0, 4);
                    string colIndex = id.Substring(4, 3);

                    foreach (DataRow row in dtExcelSchema.Rows)
                    {
                        string tableName = row[2].ToString();
                        remark = getColRemark(dal, tableName, tableId, colIndex);

                        if (!string.IsNullOrEmpty(remark))
                            break;
                    }

                    remarks += "\"" + index++ + "\":\"" + remark + "\",";
                }

                remarks = remarks.TrimEnd(',') + "}";
                
            }

            Response.Write("\"remark\":");
            Response.Write(remarks );

        }

        /// <summary>
        /// 获取指定的字段的说明
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="tableName"></param>
        /// <param name="tableId"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        private string getColRemark(DataAccessLibrary dal,string tableName, string tableId, string colIndex)
        {
            tableName = tableName.Trim('\'');
            colIndex = colIndex.TrimStart('0');
            
            string sql = "select top 1 [说明] from [{0}] where [表编号] = {1} and [字段编号] = {2}";

            string remark = dal.ExecuteString(string.Format(sql, tableName, tableId, colIndex));

            return remark;

        }

        /// <summary>
        /// 创建访问excel的实例
        /// </summary>
        /// <returns></returns>
        private DataAccessLibrary GetDal()
        {
            const string sql = "SELECT TOP 1 ConnString FROM   Manage_DataBase WHERE  (DataBaseID = 6)";

            string excelPath = Dal.DalMetadata.ExecuteString(sql);

            string cnString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + excelPath + "; Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
            DataAccessLibrary acc = DalFactory.CreateDal(cnString, "System.Data.OleDb");

            return acc;

        }

      
    }
}