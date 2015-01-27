using System;
using System.Collections.Generic;
using Nature.BaseWebform;
using Nature.DebugWatch;
using Nature.MetaData.Entity;
using Nature.MetaData.ManagerMeta;

namespace NatureFramework.SupportingPlatform
{
    public partial class TreePage : PageUserInfo 
    {
        protected Dictionary<int, IColumn> LstTree;
        protected int[] Key;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindTree();
        }

        private void BindTree()
        {
            var debugInfo = new NatureDebugInfo { Title = "[NatureFramework.SupportingPlatform.BindTree] " };
 
            var mgrModule = new ManagerModule {DalCollection = Dal};


            //判断权限
            string query = "ModuleLevel <=2 ";

            
            if (MyUser.BaseUser.PersonID != "1")
            {
                string moduleIDs = MyUser.UserPermission.ModuleIDs;
                query += " and ModuleID in (" + moduleIDs + ") ";
            }

            mgrModule.Query = query;

            LstTree = mgrModule.GetMetaData(debugInfo.DetailList);

            Key = new int[LstTree.Count];

            
            int i = 0;
            foreach (KeyValuePair<int, IColumn> info in LstTree)
            {
                Key[i++] = info.Key;
            }
            debugInfo.Stop();
             

        }
    }
}