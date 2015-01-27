<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridFormat.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Javascript.GridFormat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>对grid的td进行格式化的辅助</title>

    <link rel="stylesheet" type="text/css" href="http://LCNatureService.nature.com/websiteStyle/MisStyle_v2.css" />

    <script language="javascript" type="text/javascript" src="/boot.js"></script>
    <script language="javascript" type="text/javascript">
        var fid = "";
        var myTxtID = "";
        var dbid;

        var pageKind = "list";     //首页

        function jsReady() {

            dbid = "1," + $.cookie("ServicesDataBaseID");

            var loadscript = new Nature.LoadScript(document);
            loadscript.js("/Scripts/json.js", function () {
                loadscript.js("GridFormat.js?v=1", function () {
                    loadscript.js("GridFormatAssign.js", function () {
                        var gridFormat = new GridFormat(dbid);
                        gridFormat.init(function () {
                        
                        });
                    });
                });
            });

            //$("#FrmCommonForm_ctrl_1083010").change(function() {
            //    var kind = this.value;
            //    $(this).HideTr(kind);

            //});
            //$("input[value=' 生 成 ']").click(function () {
            //    var selected_value = $("#FrmCommonForm_ctrl_1083010").val();
            //    $.ShengCheng(selected_value);
            //});


        }
    </script>

</head>
<body style="margin: 10px;">
    <form id="form1" runat="server">

        <div style="">
            <div id="divText" style="width: 700px; padding: 10px;">
                <Nature:DataForm runat="server" ID="FrmCommonForm" />
            </div>

        </div>

        <div style="clear: both"></div>
        <div style="text-align: center">
            <input type="button" value=" 返 回 " onclick="createMetaReturn()" />
            <%-- <input type="button" value=" 生 成 " onclick="createMeta()"/> --%>
            <input type="button" value=" 生 成 " />
            <textarea id="txtMeta" cols="80" name="S1" rows="10"></textarea>
        </div>

    </form>
</body>
</html>
