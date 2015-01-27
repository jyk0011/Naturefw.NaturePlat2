<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tree.aspx.cs" Inherits="NatureFramework.SupportingPlatform.TreePage" %>
<%@ Import Namespace="Nature.MetaData.Entity.WebPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>功能节点菜单</title>
    <script language="javascript" type="text/javascript" src="/Scripts/jquery-1.8.2.min.js"></script>
    
     <style type="text/css">  
        #divkuang{   
            border: 1px solid #99bbe8;   
             
        }   
        #divDaoHang 
        {
            background: url("img/treebk.gif");
            height: 25px;
            border-bottom:1px solid #99bbe8;  
        }
        .tree1  {
            font-size: 14px;
            cursor: pointer;
            border-bottom:1px solid #99bbe8;  
            height: 25px;
            padding-left: 10px;
        }
        .tree2 {
            font-size: 14px;
            cursor: pointer;
            height: 26px;
            padding-left: 25px;
        }
        .tree1 {
            background-color: #e0ecff;
        }
       
    </style>  
    <script type="text/javascript" language="javascript">

        var oldNodeID = 0;
        function tree1Click(moduleId) {
            //$("#divTree_Kuang_" + oldNodeID).slideUp("fast");
            $("#divTree_Kuang_" + oldNodeID).hide();
            
            //alert($("#divTree_" + moduleID + "_Kuang"));
            if ($("#divTree_Kuang_" + moduleId)[0].style.display == "none")
                $("#divTree_Kuang_" + moduleId).slideDown("normal");
            else
                $("#divTree_Kuang_" + moduleId).slideUp("normal");

            oldNodeID = moduleId;
        }

        function tree2Click(moduleId, mpvid,fpvid,url,title) {
            //parent.ifrmURL(url + "?mdid=" + moduleId + "&mpvid=" + mpvid + "&fpvid=" + fpvid);
            parent.ifrmURL(moduleId, mpvid, fpvid, url, title);

        }

        window.onload = function () {
            oldNodeID = <%=((ModuleEntity)LstTree[Key[0]]).ModuleID %>;
            $("#divTree_Kuang_" + oldNodeID).slideDown("normal");
            
        };
    </script>
</head>
<body style="margin:0px; background-color:White;">
    <div id="divkuang">
        <div id="divDaoHang">导航菜单</div>
        
        <% int LastLevel = 1;
           int i = 0;
           int nodeID = 0;
          
           for (i=0;i<LstTree.Count ;i++)
           {
               ModuleEntity bInfo = (ModuleEntity)LstTree[Key[i]];
               if (bInfo.ModuleLevel == 1)
               {%>
                <div id="divTree_<%= bInfo.ModuleID %>" onclick="tree1Click(<%= bInfo.ModuleID %>)" class="tree1"><%=bInfo.ModuleName%></div>
                <% nodeID = bInfo.ModuleID;
               }else{ %>
                <div id="divTree_Kuang_<%= nodeID %>" style="display:none;">
                   <% while (true)
                      {
                          bInfo = (ModuleEntity)LstTree[Key[i]];
                          if (bInfo.ModuleLevel == 1)
                          {
                              i--;
                              break;
                          }
                           %>
                        <!--二级节点-->
                        <div id="divTree_<%= bInfo.ModuleID %>"  onclick="tree2Click(<%= bInfo.ModuleID %>,<%= bInfo.GridPageViewID  %>,<%= bInfo.FindPageViewID %>,'<%= bInfo.URL %>','<%= bInfo.ModuleName %>')"class="tree2"><%= bInfo.ModuleName%></div>
                        <%i++; 
                        if (i >= LstTree.Count) break;
                      } %>
                </div>
                <% }
               
           } %>
         
    </div>
      
</body>
</html>
