<%@ Page Language="C#" AutoEventWireup="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>列表字段排序</title>
    <script language="javascript" type="text/javascript" src="/Scripts/jquery-1.8.2.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/NatureAjax/jQuery.command.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/NatureAjax/jQuery.dragClick.js"></script>
    <script language="javascript" type="text/javascript" src="/commonPage/nature.DragOrder.js"></script>
 <style type="text/css">
     #msg,#msg1 {
         clear: both;
     }
     #divDataList 
     {
         margin-top: 10px;
         margin-bottom: 10px;
         border: 0px solid #99bb18;
         clear: both;
     }
      
      .spancol {
          border: 1px solid #99bbe8;
          padding: 5px;
          margin: 5px;
          height: 20px;
          min-width: 20px;
          float: left;
      }
      
      .spanThis {
          background-color: bisque;
      }
      
      .spanOver {
          background-color:coral;
      }
 </style>

<script type="text/javascript" language="javascript">
    var dataID = -2;
    var frid = -2;

    var info = { url: "", OpenModuleID: 0, OpenPageViewID: 0, FindPageViewID: 0, ButtonID: 0, WebWidth: 0, WebHeight: 0 };
    
    //提交的结构
    var orderData = { action: "datagrid", col1ID: 1, col2ID: 2, kind: "exchange" };
       
    var dataListMeta;
    var localDate = new Date();
    var masterPageViewID = 0;

    var colLeft = {};
    var tmpColLeft = "[";
    
    window.onload = function () {
        //moduleId, mpvid, fpvid, url
        //获取URL参数

        //alert("aa");
        var para = $.getUrlParameter();
      
        CreateDataGrid(para);
         
    };

    function CreateDataGrid(para) {

        var dd = new Date();
        $.ajaxGetMeta({
            data: { action: "grid", mpvid: para.mpvid, v: dd.getSeconds() + "_" + dd.getMilliseconds() },
            title: "数据列表元数据",
            success: function (msg) {
                CreateDataGrid1(para, msg);
            }
        });
    }


    function CreateDataGrid1(para, msg) {

        //    var dataListMeta = {
        //        "getTime": "2012-10-11 10:00:19",
        //        "useTime": "0.71320毫秒",
        //        "data": {
        //            "1000010": { "ColumnID": 1000010, "ColTitle": "模块ID", "ColHelp": "", "HelpStation": 1, "ColWidth": 0, "ColAlign": "left", "Kind": 1, "IsSort": false, "Format": "", "MaxLength": 0 },
        //            "1000120": { "ColumnID": 1000120, "ColTitle": "是否隐藏", "ColHelp": "", "HelpStation": 1, "ColWidth": 0, "ColAlign": "left", "Kind": 1, "IsSort": false, "Format": "", "MaxLength": 0 }
        //        },
        //        "datakeys": [1000010, 1000060, 1000090, 1000116, 1000117, 1000140, 1000130, 1000120]
        //    };

        dataListMeta = msg;

        var col = "<div id='c{id}' class='spancol'>{title}</div>";
        var key = dataListMeta.datakeys;
        var tmpDiv = $("<div>");
        for (var i = 0; i < key.length; i++) {
            var dd = dataListMeta.data[key[i]];

            var tmpCol = col.replace(/\{id\}/g, dd.ColumnID);
            tmpCol = tmpCol.replace(/\{title\}/g, dd.ColTitle);

            tmpDiv.append(tmpCol);
     
        }

        $("#divDataList").html(tmpDiv.html());
        myDrag();

    }

    var wdDepth = 0;        //单独打开页面的话，递归无法退出，所以加上深度的判断。
    function writeDebug(msg) {
        wdDepth++;
        if (wdDepth > 1)
            return;

        if (parent.DebugSet)
            parent.DebugSet(msg);
        else
            parent.writeDebug(msg);
    }

   
    
</script>   
</head>
<body>
  <div id="divDataList"></div>
  <div id="msg"></div>
  <div id="msg1"></div>

</body>
</html>
