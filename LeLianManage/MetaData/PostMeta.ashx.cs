using System.Data;
using Nature.DebugWatch;

namespace Nature.Service.MetaData
{
    /// <summary>
    /// 元数据视图里的字段的排序
    /// </summary>
    /// user:jyk
    /// time:2013/3/9 14:16
    public class PostMeta : BaseAshxCrud
    {
        /// <summary>
        /// 元数据视图里的字段的排序
        /// </summary>
        public override void Process()
        {
            base.Process();

            switch (Action)
            {
                case "datagrid": //数据列表字段的排序
                    BaseDebug.Title = "元数据视图里的字段的排序";
                    Datagrid();
                    break;

            }

        }
         
        #region 数据列表的元数据的修改

        private void Datagrid()
        {
            //修改排序，交换和插入前、插入后
            string col1ID = Request["col1ID"];
            string col2ID = Request["col2ID"];
            string kind = Request["kind"];

            int col1Order;
            int col2Order;

            var debugInfo = new NatureDebugInfo { Title = "修改排序。" + kind + "_" + col1ID + "_" + col2ID };
            BaseDebug.DetailList.Add(debugInfo);
            
            const string sql =
                @"SELECT  PVColID, DisOrder FROM Manage_PageViewCol WHERE (PVID = {0}) AND  (PVColID  in ({1}) ) ORDER BY DisOrder";

            DataTable dtColOrder =
                Dal.DalCustomer.ExecuteFillDataTable(string.Format(sql, MasterPageViewID, col1ID + "," + col2ID));

            if (dtColOrder.Rows.Count != 2)
                return;

            if (dtColOrder.Rows[0][0].ToString() == col1ID)
            {
                col1Order = int.Parse(dtColOrder.Rows[0][1].ToString());
                col2Order = int.Parse(dtColOrder.Rows[1][1].ToString());
            }
            else
            {
                col1Order = int.Parse(dtColOrder.Rows[1][1].ToString());
                col2Order = int.Parse(dtColOrder.Rows[0][1].ToString());

            }
            const string sqlUpdate ="update Manage_PageViewCol set DisOrder = {0} WHERE  (PVColID = {1} )"; //设置
            //前面c
            const string sqlInsertFront =
                "update Manage_PageViewCol set DisOrder = DisOrder - 10  WHERE (PVID = {0}) AND (DisOrder >= {1} and DisOrder < {2} )";

            //后面c
            const string sqlInsertAfter =
                "update Manage_PageViewCol set DisOrder = DisOrder - 10  WHERE (PVID = {0}) AND (DisOrder > {1} and DisOrder <= {2} )";

            //前面
            const string sqlInsertFront2 =
                "update Manage_PageViewCol set DisOrder = DisOrder + 10  WHERE (PVID = {0}) AND (DisOrder >= {1} and DisOrder <{2} )";

            //后面
            const string sqlInsertAfter2 =
                "update Manage_PageViewCol set DisOrder = DisOrder + 10  WHERE (PVID = {0}) AND (DisOrder > {1} and DisOrder <={2} )";
            
            string sql1;
            string sql2;

            switch (kind)
            {
                case "1": //加在上面      
                case "left": //加在左面      
                    if (col1Order < col2Order)
                    {
                        sql1 = string.Format(sqlInsertFront, MasterPageViewID, col1Order, col2Order);
                        sql2 = string.Format(sqlUpdate, col2Order - 10, col1ID);
                        //后面的插到前面  c
                        Dal.DalCustomer.ExecuteNonQuery(sql1);
                        Dal.DalCustomer.ExecuteNonQuery(sql2);
                        debugInfo.Remark = "left< <br>" + sql1 + "<br>" + sql2;
                    }
                    else if (col1Order > col2Order)
                    {
                        sql1 = string.Format(sqlInsertFront2, MasterPageViewID, col2Order, col1Order);
                        sql2 = string.Format(sqlUpdate, col2Order, col1ID);
                        //前面的查到后面
                        Dal.DalCustomer.ExecuteNonQuery(sql1);
                        Dal.DalCustomer.ExecuteNonQuery(sql2);
                        debugInfo.Remark = "left> <br>" + sql1 + "<br>" + sql2;
                    }
                    break;

                case "3": //加在下面      
                case "right": //加在右面
                    if (col1Order < col2Order)
                    {
                        sql1 = string.Format(sqlInsertAfter, MasterPageViewID, col1Order, col2Order);
                        sql2 = string.Format(sqlUpdate, col2Order , col1ID);
                        //后面的插到前面  r
                        Dal.DalCustomer.ExecuteNonQuery(sql1);
                        Dal.DalCustomer.ExecuteNonQuery(sql2);
                        debugInfo.Remark = "right< <br>" + sql1 + "<br>" + sql2;
                  
                    }
                    else if (col1Order > col2Order)
                    {
                        sql1 = string.Format(sqlInsertAfter2, MasterPageViewID, col2Order, col1Order);
                        sql2 = string.Format(sqlUpdate, col2Order + 10, col1ID);
                        //前面的查到后面 r
                        Dal.DalCustomer.ExecuteNonQuery(sql1);
                        Dal.DalCustomer.ExecuteNonQuery(sql2);
                        debugInfo.Remark = "right> <br>" + sql1 + "<br>" + sql2;
                    }
                    break;

                case "2": //交换位置
                case "exchange": //交换位置
                    sql1 = string.Format(sqlUpdate, col1Order, col2ID);
                    sql2 = string.Format(sqlUpdate, col2Order, col1ID);
                    Dal.DalCustomer.ExecuteNonQuery(sql1);
                    Dal.DalCustomer.ExecuteNonQuery(sql2);
                    debugInfo.Remark = "exchange <br>" + sql1 + "<br>" + sql2;
                  
                    break;

            }

            Response.Write("\"s\":" + kind);

            debugInfo.Stop();
            
        }

        #endregion

    }
}