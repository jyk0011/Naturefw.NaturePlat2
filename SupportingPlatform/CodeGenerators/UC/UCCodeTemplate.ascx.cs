using System;
using System.Collections.Generic;
using Nature.Data;
using Nature.DebugWatch;
using Nature.MetaData.Entity;
using Nature.MetaData.ManagerMeta;

namespace NatureFramework.SupportingPlatform.CodeGenerators.UC
{
    public partial class UcCodeTemplate : System.Web.UI.UserControl
    {
        protected Dictionary<int,IColumn > DicFormInfo;
        
        /// <summary>
        /// 功能节点
        /// </summary>
        public string PageViewID { get; set; }

        /// <summary>
        /// 功能节点
        /// </summary>
        public DalCollection DalCollection { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
       
        }

        public void LoadData()
        {
            var debugInfo = new NatureDebugInfo { Title = "[Nature.Service.Ashx.BaseAshx]判断Url参数" };

            DalCollection dalCollection = DalCollection;
            dalCollection.DalMetadata = DalCollection.DalCustomer;

            var managerFormMeta = new ManagerFormMeta
                                      {
                                          DalCollection = dalCollection,
                                          PageViewID = int.Parse(PageViewID)
                                      };

            DicFormInfo = managerFormMeta.GetMetaData(debugInfo.DetailList);

            if (DicFormInfo == null)
                return;

            foreach (KeyValuePair<int, IColumn> info in DicFormInfo)
            {
                var bInfo = (ColumnMeta)info.Value;

                //修改类型，把数据库字段类型，变成.net类型
                switch (bInfo.ColType)
                {
                    case "nvarchar":
                    case "varchar":
                    case "nchar":
                    case "char":
                    case "text":
                    case "ntext":
                    case "uniqueidentifier":
                        bInfo.PropertyType = "string";
                        break;

                    case "bigint":
                        bInfo.PropertyType = "Int64";
                        break;
                    case "int":
                        bInfo.PropertyType = "Int32";
                        break;
                    case "smallint":
                    case "tinyint":
                        bInfo.PropertyType = "Int16";
                        break;

                    case "datetime":
                    case "smalldatetime":
                        bInfo.PropertyType = "DateTime";
                        break;

                    case "bit":
                        bInfo.PropertyType = "bool";
                        break;


                    case "money":
                    case "smallmoney":
                        bInfo.PropertyType = "decimal";
                        break;

                    default:
                        bInfo.PropertyType = bInfo.ColType;
                        break;
                    

                }
            }

            debugInfo.Stop();

        }


    }
}