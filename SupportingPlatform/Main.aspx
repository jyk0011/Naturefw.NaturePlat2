<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>标签页</title>
    <script language="javascript" type="text/javascript" src="/Scripts/jquery-1.8.2.min.js"></script>
    <style type="text/css">  
        body {
            margin: 0px;
            background-color:White;
        }
        #divTab ,#tab{
            float: left; 
             
        }   
        #divModuleId {
            clear:both ;
        }
        .divSelected {
            float: left;
            color: #1ee;
            border-right:1px solid #99bbe8;  
            border-top:1px solid #99bbe8; 
            border-left:1px solid #99bbe8;

            height: 30px;

            padding: 4px;
            margin-right: 2px;
            cursor: pointer;
        }
        
        .divNotSelected {
            float: left ; 
            color: #111;
            border-right:1px solid #99bbe8;  
            border-top:1px solid #99bbe8; 
            border-left:1px solid #99bbe8; 
            border-bottom:1px solid #99bbe8;  
            
            height: 30px;
            padding: 4px;
            margin-right: 2px;
            cursor: pointer;
            
            
        }
        iframe {
            width: 100%;
            height: 100%;
            border-right:1px solid #99bbe8;  
            border-left:1px solid #99bbe8; 
            border-bottom:1px solid #99bbe8;  
        }
    </style>

    <script type="text/javascript" language="javascript">

        function createTab(moduleId, mpvid, fpvid, url, title) {
            $("#ifrm0").hide();
            //把以前选中的变成未选中
            $(".divSelected").addClass("divNotSelected");
            $(".divSelected").removeClass("divSelected");
                
            //创建一个标签
            if ($("#tab" + moduleId).length != 0 ) {
                //有标签，设置为已选择
                //alert(moduleId);
                $("#tab" + moduleId).removeClass("divNotSelected");
                $("#tab" + moduleId).addClass("divSelected");
            } else {
                //没有标签，创建
                //alert("aa");
                $("#divTab").append("<div id=\"tab" + moduleId + "\" class=\"divSelected\" onclick=\"tabClick(" + moduleId + ")\">" + title + "</div>");
            }


            //创建一个iframe
            //把其他的iframe隐藏
            $("iframe").hide();
            var tmpUrl= "";
            if ($("#ifrm" + moduleId).length != 0) {
                //有标签
                //alert(moduleId);
                $("#ifrm" + moduleId).show();
                tmpUrl = url + "?mdid=" + moduleId + "&mpvid=" + mpvid + "&fpvid=" + fpvid;
                $("#ifrm" + moduleId).attr("src", tmpUrl);
            } else {
                //没有标签，创建
                //alert("aa");
                tmpUrl = url + "?mdid=" + moduleId + "&mpvid=" + mpvid + "&fpvid=" + fpvid;
                $("#divModuleId").append("<iframe id=\"ifrm" + moduleId + "\" src=\"" + tmpUrl + "\" frameborder=\"0\">");
                $("#ifrm" + moduleId).show();
                setIframe(moduleId);
            }

        }
        function tabClick(moduleId) {
            $("iframe").hide();
            $("#ifrm0").hide();

            $(".divSelected").addClass("divNotSelected");
            $(".divSelected").removeClass("divSelected");
            
            $("#tab" + moduleId).addClass("divSelected");
            $(".divSelected").removeClass("divNotSelected");
            
            $("#ifrm" + moduleId).show();
        }

        function setIframe(moduleId) {
            var winHeight = $(window).height();
            $("#ifrm" + moduleId).height(winHeight - 60 + "px");
        }

        
    </script>

</head>
<body >
    <div id="divTab"><div id="tab0" class="divSelected"  onclick="tabClick(0)">欢迎体验</div></div>
    <div id="divModuleId"> 
        <div id="ifrm0">欢迎使用自然框架支持平台！</div>
    </div>
    
</body>
</html>
