<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectList.aspx.cs" Inherits="NatureFramework.SupportingPlatform.ProjectList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    <nature:MyDropDownList runat="server" ID="lstProject" AutoPostBack="True"></nature:MyDropDownList>
    </div>
    </form>
</body>
</html>
