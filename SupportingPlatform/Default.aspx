<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>自然框架支持平台</title>
    <script language="javascript" type="text/javascript" src="/Scripts/jquery-1.8.2.min.js"></script>
    <style type="text/css">  
        div
        {
            float:none;
        }
        #div_Show 
        {
            background: #d2e0f2;
            color: beige;
        }
       
        #tr_logo {
            background:url("img/bannerbk.gif")
        }
        #tdTitle {
            padding-left: 5px;
        }
        
        #divCopyright 
        {
            text-align: center;
            background-color:#99bbe8 ; 
            width:100%;   
            height:25px; 
        }
         
    </style>  

    <script type="text/javascript" language="javascript">
        $(window).ready(setHeight);
        $(window).resize(setHeight); 

        
        function setHeight() {
            //alert($(window).height());
            //alert($("tr_logo").height());
            //$("#div_Mod").hide();
            var h = 80;
            var winHeight = $(window).height() - 30;
            $("#tb_DataList").height(winHeight);
            $("#tr_TreeMain").height(winHeight - h);
            $("#ifrm_Tree").height(winHeight - h + 40);
            $("#ifrm_Main").height(winHeight - h + 40);
        }
        
        

        function ifrmURL(moduleId, mpvid, fpvid, url, title) {
            //$("#ifrm_Main").attr("src", url);
            $("#ifrm_Main")[0].contentWindow.createTab(moduleId, mpvid, fpvid, url, title);

        }

    </script>
</head>
<body style="margin:0px">
    <div id="div_Show">
    <table id="tb_DataList" cellspacing="0" rules="all" border="1" style="border-collapse:collapse;width:100%;">
        <tr id="tr_logo" style="height:26px"><td id="tdTitle" colspan="2">自然框架支持平台  <span onclick="divOpen()">显示</span></td></tr>
        <tr id="tr_TreeMain">
            <td style="width:200px"><iframe id="ifrm_Tree" src="tree.aspx" name="tree" style="width:97%;" frameborder="0"></iframe></td>
            <td><iframe id="ifrm_Main" src="main.aspx" name="main" style="width:99%;height:100%" frameborder="0"></iframe></td>
        </tr>
    </table>
    </div>
    <div id="divCopyright">by 自然框架之UI 仿easyUI </div>
   
</body>
</html>
