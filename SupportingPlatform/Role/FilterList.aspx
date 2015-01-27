<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FilterList.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Role.FilterList" %>
<%@ Import Namespace="System.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源过滤方案的适合的列表框</title>
</head>
<body style="background:#fff">
    <form id="form1" runat="server">
     <table width="100%"><tr><td width="350" valign="top">
        <Nature:QuickPager ID="CtlCommonPager1" runat="server" 
            PageUIAllCount="表:&lt;font style=&quot;color:Red;&quot;&gt;{0}&lt;/font&gt;" 
            PageUIFirst="&lt;&lt;" PageUIGO="" PageUILast="&gt;&gt;" PageUINext="&gt;" 
            PageUIPrev="&lt;" 
            
            PageUIPageSize="每页:&lt;font style=&quot;color:Red;&quot;&gt;{0}&lt;/font&gt;" />
            <asp:GridView ID="GV_Table" runat="server" AutoGenerateColumns="False" 
            onselectedindexchanged="GvTableSelectedIndexChanged">
         <SelectedRowStyle BackColor="#cbfeed"  />
            <Columns>
                <asp:BoundField DataField="TableID" HeaderText="编号" />
                <asp:BoundField DataField="TableName" HeaderText="表名" />
                <asp:CommandField HeaderText="表名" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        
        </td>
    <td valign="top"><br />
        <asp:GridView ID="GV_Field" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <ItemTemplate>
                        <input type="checkbox" id="chk_ColumnID" name="chk_ColumnID" value='<%# (Container.DataItem as DataRowView)["ColumnID"]%>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="ColumnID" HeaderText="ColumnID" />
                <asp:BoundField DataField="ColSysName" HeaderText="字段名" />
                <asp:BoundField DataField="ColName" HeaderText="显示名" />
                <asp:BoundField DataField="ColType" HeaderText="类型" />
                
            </Columns>
        </asp:GridView>
        <br />
        选择的字段要加入哪个视图？<br />
        <asp:Button ID="Btn_List" runat="server" onclick="BtnListClick" Text=" 添 加 " /></td>
    
    </tr></table>
    </form>
</body>
</html>
