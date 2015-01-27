<%@ Control   Language="C#" AutoEventWireup="true" CodeBehind="UCCodeTemplate.ascx.cs" Inherits="NatureFramework.SupportingPlatform.CodeGenerators.UC.UcCodeTemplate" %>
<%@ Import Namespace="Nature.MetaData.Entity" %>
<%@Import namespace="System.Collections.Generic"%>

    public class MyClass
    {
    #region 属性
<% 
    if (DicFormInfo == null) { return; }

    foreach (KeyValuePair<int, IColumn> info in DicFormInfo)
    {
        ColumnMeta columnMeta = (ColumnMeta)info.Value;
  %>
        #region <%=columnMeta.ColName  %>
        private <%=columnMeta.PropertyType%> _<%=columnMeta.ColSysName %>;
        /// <summary>
        /// <%=columnMeta.ColName%>
        /// </summary>
        [ColumnID(<%=columnMeta.ColumnID %>)]
        public <%=columnMeta.PropertyType%> <%=columnMeta.ColSysName %>
        {
            get { return _<%=columnMeta.ColSysName %>; }
            set { _<%=columnMeta.ColSysName %> = value; }
        }
        #endregion
  <%} %>
  #endregion
  
  }


