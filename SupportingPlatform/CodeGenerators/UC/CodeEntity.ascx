<%@ Control   Language="C#" AutoEventWireup="true"   Inherits="NatureFramework.SupportingPlatform.CodeGenerators.UC.UcCodeTemplate" %>
<%@ Import Namespace="Nature.MetaData.Entity" %>
<%@ Import namespace="System.Collections.Generic"%>

    public class MyClass
    {
    #region 属性
<%  if (DicFormInfo == null) { return; }
    foreach (KeyValuePair<int, IColumn> info in DicFormInfo)
    {
        ColumnMeta columnMeta = (ColumnMeta)info.Value;%>
        #region <%=columnMeta.ColName  %>
        /// <summary>
        /// <%=columnMeta.ColName%>
        /// </summary>
        [ColumnID(<%=columnMeta.ColumnID %>)]
        public <%=columnMeta.PropertyType%> <%=columnMeta.ColSysName %> { get ; set; } 
        #endregion
        <%} %>
   #endregion
  
  }

  s