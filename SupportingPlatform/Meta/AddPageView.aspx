<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPageView.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Meta.AddPageView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加页面视图</title>
    <script language="javascript" type="text/javascript" src="/boot.js"></script>
    <style type="text/css">
    div{
        clear: both;
    }
    #divView,#divButton {
        float: left;
        width: 200px;
        border:1px solid #99bbe8;
        clear: none;

    }
    </style>
    <script language="javascript" type="text/javascript">
    var fid = "";

    function myCheck() {
        return CheckForm();
    }

    function jsReady() {
        if (myTxtID)
            if (document.getElementById(myTxtID))
                document.getElementById(myTxtID).focus();

        $("#tr1006110,#tr1006100,#tr1006150").hide();
               
        $("#FrmCommonForm_ctrl_1006030").change(function () {
            //alert(this.value);
            $("tr").show();
            switch (this.value) {
                case "701":   //列表视图
                    $("#tr1006110,#tr1006100,#tr1006150").hide();
                    break;
                case "702":   //查询视图
                    $("#tr1006050,#tr1006060,#tr1006090,#tr1006070,#tr1006100,#tr1006120,#tr1006130,#tr1006140,#tr1006150").hide();
                    break;
                case "703":   //表单视图
                    $("#tr1006120,#tr1006130,#tr1006150").hide();
                    break;
                case "704":   //删除
                    $("#tr1006110,#tr1006120,#tr1006130,#tr1006140,#tr1006150").hide();
                    break;
                case "705":   //导出
                    $("#tr1006060,#tr1006100,#tr1006110,#tr1006120,#tr1006130,#tr1006140,#tr1006150").hide();
                    break;
                case "706":   //选择
                    $("#tr1006060,#tr1006100,#tr1006110,#tr1006120,#tr1006130,#tr1006140,#tr1006150").hide();
                    break;
            }
        });

    }

    function openChoose(me, url, fid, w, h) {

        alert("a");
        if (h == 0) { h = screen.availHeight - 50; }
        if (w == 0) { w = screen.availWidth - 10; }
        var re = "height=" + h + ",width=" + w + ",top=" + (screen.availHeight - h - 30) / 2 + ",left=" + (screen.availWidth - w - 10) / 2 + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,toolbar=no,directories=no";

        var par = "&id=-2&frid=-2&bid=-2&cid=" + me.id;
        if (url.indexOf("?") >= 0) {
            if (url.indexOf("?fid=") >= 0)
                myWin = window.open(url + par, "_blank", re); //_blank
            else
                myWin = window.open(url + "&fid=" + fid + par, "_blank", re); //_blank

        }
        else
            myWin = window.open(url + "?fid=" + fid + par, "_blank", re);

        window.status = "";
        //alert(myWin.id);
        myWin.focus();

    }

   
    </script>
</head>
<body >
    <form id="form1" runat="server">
        <asp:Label runat="server" ID="LblTitle"></asp:Label>
   <div>
    <Nature:DataForm runat="server" ID="FrmCommonForm"/>
        <br />
       
    </div>
  
    <div style="text-align:center">
        <asp:Button  class="input_01" runat="server" ID="BtnSave" Text="保存后关闭" /> &nbsp; 
        <asp:Button class="input_01" runat="server" ID="BtnSaveContinue" Text="保存后继续" /> <br /><asp:Label  runat="server" ID="LblMsg"/>
    </div>

    </form>
</body>
</html>
