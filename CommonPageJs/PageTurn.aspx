<%@ Page Language="C#" AutoEventWireup="true"   %>
<%@ Import Namespace="NatureFramework.CommonPageJs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>分页控件的测试</title>
    <link rel="stylesheet" type="text/css" href="/Scripts/QuickPager/skin/default/pager.css"/>
    <link rel="stylesheet" type="text/css" href="/Scripts/css/cssList.css"/>
    <script language="javascript" type="text/javascript" src="/Scripts/jquery-1.8.2.min.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/NatureAjax/nature.head.js"></script>
	<script language="javascript" type="text/javascript" src="/Scripts/NatureAjax/nature.QuickPager2.0.js"></script>
	
 <style type="text/css">
       #divFloatSearch{   
            background-color:#d2eefd ;   
            background:url("/Scripts/css/img/formbk.gif");
            border: 1px solid #99bbe8;   
            padding:5px;
            position:absolute;   
            
            color: #15428b;
        } 
        #divSearchDetail {
            float: left;
        }
        .cssFind {
            float: left;
        }
        #divDataList {
            clear: both;
        }
 </style>

<script type="text/javascript" language="javascript">
    var dataID = -2;
    var frid = -2; 
    
    var info = { url: "", OpenModuleID: 0, OpenPageViewID: 0, FindPageViewID: 0, ButtonID: 0, WebWidth: 0, WebHeight: 0 };


    window.onload = function () {

        var page = new Nature.Page.QuickPager();

        //设置相关属性
        page.pagerInfo = {
            recordCount: 102,       //总记录数
            //pageSize: 10,           //一页记录数

            pageTurnDivIDs: "divQuickPage,divQuickPage1",         //放置分页控件的div的id，可以是多个，用半角逗号分隔

            LoadData: function () { },      //加载数据的事件
            BindTable: function () { },      //绑定表格的事件
            //翻页的事件
            OnPageChange: function (oldPageIndex, thisPageIndex) {
                alert("翻页前页号：" + oldPageIndex);
                alert("翻页后页号：" + thisPageIndex);
                
                //可以自行获取记录，创建table
            }   

        };
        
        //开始
        page.Start(function () {
            alert("加载分页模板成功");
            //显示第一页的数据
        });

    };

    function loadFirst() {
        //alert("loadFirst");
        Nature.Page.QuickPager.BindGrid(1);
    }
    function loadThis() {
        //alert("loadThis");
        Nature.Page.QuickPager.BindGrid(Nature.Page.QuickPager.thisPageIndex());
    }
    function closeSon() {
        //alert("closeSon");
    }

       

</script>   
</head>
<body style="overflow:inherit ; ">
    <div id="divMain"> 
        <div id="divButton"></div>
        <div id="divSearch"><div class="cssFind">当前查询条件：</div>
            <div ><form id="dataForm1"><div id="divSearchDetail"></div><div id="divSearchBtn"></div></form></div>
        </div>
        <div id="divQuickPage"></div>
        <div id="divDataList"></div>
        <div id="divQuickPage1"></div>
        <div id="divFloatSearch"  style="display:none;">
            <span id="span_title" style="width:95%; float:left;">查询条件 <a href="javascript:void(0)" id="A2">X</a></span>
            <form id="dataForm2"><div id="divFloatSearchDetail"  ></div></form>
        </div>
    </div>
     
</body>
</html>
