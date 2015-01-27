<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataBaseToMeta.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Document.DataBaseToMeta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>根据数据库信息，创建元数据</title>
</head>
<body style="background-color:white">
    <form id="form1" runat="server">
    <div>
    
        表编号：<asp:TextBox ID="txtTableNo" runat="server"></asp:TextBox>
        <br />
        主键字段编号：<asp:TextBox ID="txtColNo" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="btnAdd" runat="server" onclick="BtnAddClick" Text="开始生成" />
        <br />
    
    </div>
    </form>
</body>
</html>
