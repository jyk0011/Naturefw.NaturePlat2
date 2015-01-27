<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddColumn.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Meta.AddColumn" %>
<%@ Import Namespace="System.Data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择列表、表单、查询、导出到Excel使用的字段</title>
    <script language="javascript" type="text/javascript" src="/boot.js"></script>
    <script type="text/javascript" language="javascript">
        function chkColClick(me) {
            for (var i = 0; i < 100; i++) {
                if (document.getElementById("chk_ColumnID" + i))
                    document.getElementById("chk_ColumnID" + i).checked = me.checked;
                else break;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%; font-size:12pt; text-align: center;vertical-align:middle;">
        <asp:Label ID="Lbl_Title" runat="server" style="font-size:14pt; "></asp:Label>
    </div>
    <table width="100%" class="table_default1"><tr><td width="350" valign="top">
        <Nature:QuickPager ID="CtlCommonPager" runat="server" 
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
                <asp:TemplateField HeaderText="<input type='checkbox' id='chkCol' onclick='chkColClick(this)'>选择">
                    <ItemTemplate>
                        <input type="checkbox" id="chk_ColumnID<%=IndexID++ %>" name="chk_ColumnID" value='<%# (Container.DataItem as DataRowView)["ColumnID"]%>' />
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
        <Nature:MyCheckBoxList runat="server" ID="lst_PageView"/><br />
        <asp:Button ID="Btn_List" runat="server" onclick="BtnListClick" Text="添加到视图" /></td>
    
    </tr></table>
    </form>
</body>
</html>
