<%@Control Language="C#" AutoEventWireup="true"  Inherits="NatureFramework.SupportingPlatform.CodeGenerators.UC.UcCodeTemplate" %>
 <%@Import namespace="System.Collections.Generic"%>
 <%@ Import Namespace="Nature.MetaData.Entity" %>

 <% foreach (KeyValuePair<int, IColumn> info in DicFormInfo)
    {
       var bInfo = (ColumnMeta)info.Value;
       %>
       <%=bInfo.ColName %> : <%= "<asp:TextBox ID=\"" + bInfo.ColumnID + "\" runat=\"server\"></asp:TextBox>"%>
  
 <%} %>