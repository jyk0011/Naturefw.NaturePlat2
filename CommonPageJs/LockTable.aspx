<%@ Page Language="C#" AutoEventWireup="true"   %>
<%@ Import Namespace="NatureFramework.CommonPageJs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>锁定行列</title>
    <link rel="stylesheet" type="text/css" href="/Scripts/QuickPager/skin/default/pager.css"/>
    <link rel="stylesheet" type="text/css" href="/Scripts/css/cssList.css"/>
    <%=PageHead.CreateJs("list")%>
 <style type="text/css">
       .cssDataList{   
            background-color:#d2eefd ;   
            border: 1px solid #99bbe8;   
            padding:0px;
           width: 500px;
           height: 400px;
           overflow: auto;
            
        } 
       #grid {
           width: 1000px;
           
           
       }
 </style>

<script type="text/javascript" language="javascript">


    window.onload = function () {

        $("#grid").lock({lockRowCount:1,lockColCount:1,divWidth:300});
    };
    
 
    var wdDepth = 0;        //单独打开页面的话，递归无法退出，所以加上深度的判断。
    function writeDebug(msg) {
        wdDepth++;
        if (wdDepth > 14)
            return;
        
        if (parent.DebugSet)
            parent.DebugSet(msg);
        else
            parent.writeDebug(msg);
    }
     

</script>   
</head>
<body>
    <div id="divMain1"> 
       
        <div id="divDataList" class ="cssDataList">
            <table id="grid" class="cssTable" rules="all"   >
               
                    <tr id="tr0" class="css_tr_th"     myclass="css_tr_th">
                        <th>
                            表号</th>
                        <th>
                            表名</th>
                        <th>
                            主键</th>
                        <th>
                            类型</th>
                        <th>
                            拥表</th>
                        <th>
                            说明</th>
                        <th>
                            工作表</th>
                        <th>
                            GUID</th>
                    </tr>
                    <tr id="tr20" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1000</td>
                        <td>
                            Manage_Module</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            序描述100程序描述</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr id="tr1" class="css_tr_c2" myclass="css_tr_c2"  >
                        <td>
                            1002</td>
                        <td>
                            Manage_Table</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表的信息</td>
                        <td>
                                  100程序描述$100程序描述100程序描述100程序描述100程序描述100程序描述100程序描述100程序描述100程序描述100程序描述100程序描述100程序描述100程序描述100程100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr id="tr2" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr3" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr4" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr5" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr6" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr7" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr8" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr9" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr10" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr11" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr12" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr13" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr14" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr15" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr16" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr17" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr18" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr19" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr21" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr22" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr23" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr24" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr25" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr26" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr27" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr28" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr29" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr30" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                   <tr id="tr31" class="css_tr_c1" myclass="css_tr_c1" >
                        <td>
                            1004</td>
                        <td>
                            Manage_Columns</td>
                        <td>
                            10</td>
                        <td>
                            U</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            表里的字段的信息</td>
                        <td>
                            100程序描述$</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                 
                   
                
            </table>
            

        </div>
       
    </div>
     
</body>
</html>
