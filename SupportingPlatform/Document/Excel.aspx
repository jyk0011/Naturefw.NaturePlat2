<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Excel.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Document.Excel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据库文档Excel</title>
    <script type="text/javascript" language="javascript">
        var isShowSearch = "0";

        function jsReady() {
  
        }
    </script>

    <script language="javascript" type="text/javascript" src="/boot.js"></script>
  

</head>
<body style="padding:5px;">
    <form id="form1" runat="server">
    <div>
        <nature:MyDropDownList runat="server" ID="lstData" AutoPostBack="True" 
            onselectedindexchanged="LstDataSelectedIndexChanged"></nature:MyDropDownList>
    </div>
    <div>
    <asp:RadioButtonList ID="Btn_TableName" runat="server" AutoPostBack="True" 
            CellPadding="0" CellSpacing="0" DataTextField="TABLE_NAME" 
            DataValueField="TABLE_NAME" 
            onselectedindexchanged="BtnTableNameSelectedIndexChanged" RepeatColumns="10" 
            RepeatDirection="Horizontal">
        </asp:RadioButtonList>
    <div id="div_Search">查询即将出现</div>
    
        <asp:GridView ID="GV_Table" runat="server" AutoGenerateColumns="False" 
            onrowcommand="GvTableRowCommand" Width="100%" CssClass="table_default1">
            <SelectedRowStyle BackColor="#cbfeed"  />
            <Columns>
                <asp:BoundField DataField="表编号" HeaderText="表编号" />
                <asp:BoundField DataField="字段名" HeaderText="表名" />
                <asp:BoundField DataField="中文名" HeaderText="中文名" />
                <asp:BoundField DataField="说明" HeaderText="说明" />
                <asp:ButtonField CommandName="查看字段" Text="查看字段" HeaderText="查看字段" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>
                <asp:ButtonField CommandName="建表" HeaderText="建表" Text="建表">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>
                <asp:ButtonField CommandName="添加表" HeaderText="添加表" Text="添加表">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>
                <asp:ButtonField CommandName="添加字段" HeaderText="添加字段" Text="添加字段">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>
            </Columns>
        </asp:GridView>
    
    </div>
    <Nature:QuickPager ID="Pager1" runat="server" CssClass="pagersize"   />
    <div>
    
     
        <asp:GridView ID="GV_Column" runat="server" AutoGenerateColumns="False" 
            onselectedindexchanged="GvColumnSelectedIndexChanged" 
            EnableModelValidation="True" onrowediting="GV_Column_RowEditing" CssClass="table_default1">
            <Columns>
                <asp:BoundField DataField="表编号" HeaderText="表编号" Visible="False" />
                <asp:BoundField DataField="字段编号" HeaderText="字段序号" />
                <asp:BoundField DataField="字段名" HeaderText="字段名" />
                <asp:BoundField DataField="中文名" HeaderText="中文名" />
                <asp:BoundField DataField="类型" HeaderText="字段类型" />
                <asp:BoundField DataField="大小" HeaderText="字段大小" />
                <asp:BoundField DataField="默认值" HeaderText="默认值" />
                <asp:BoundField DataField="说明" HeaderText="说明" ><ItemStyle Width="300px"></ItemStyle></asp:BoundField>
                <asp:BoundField DataField="关联表" HeaderText="关联表" />
                <asp:BoundField DataField="关联字段" HeaderText="关联字段" />
                <asp:BoundField DataField="是否添加到配置信息" HeaderText="加配置？" />
                <asp:BoundField DataField="是否建立字段" HeaderText="建字段？" />
                <asp:CommandField ShowSelectButton="True" HeaderText="追加字段" SelectText="加" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
             
                <asp:CommandField EditText="加" HeaderText="追加备注" ShowEditButton="True" />
             
            </Columns>
        </asp:GridView>
    
    </div>
    <div >
        
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Btn_add" runat="server" onclick="BtnAddClick" Text=" 建 表 " />
        <br />
        <Nature:MyTextBox ID="Txt_BuildTable" runat="server" Columns="90" Rows="25" 
            TextMode="MultiLine"></Nature:MyTextBox>
        
    </div>
    </form>
</body>
</html>
