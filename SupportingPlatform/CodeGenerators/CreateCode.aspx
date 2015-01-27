<%@ Page Language="C#" validateRequest="false" AutoEventWireup="true" CodeBehind="CreateCode.aspx.cs" Inherits="NatureFramework.SupportingPlatform.CodeGenerators.CreateCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>代码生成器</title>
</head>
<body>
    <form id="form1" runat="server">
    <Nature:MyDropDownList ID="Lst_Function" runat="server" AutoPostBack="True" 
            onselectedindexchanged="LstFunctionSelectedIndexChanged"></Nature:MyDropDownList>
    &nbsp;<br />
    <Nature:MyTextBox ID="Txt_Code" runat="server" Columns="100" 
              Rows="30" TextMode="MultiLine"></Nature:MyTextBox>
    </form>
</body>
</html>
