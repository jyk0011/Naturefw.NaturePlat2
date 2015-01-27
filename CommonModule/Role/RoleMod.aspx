<%@ Page Language="C#" AutoEventWireup="true"   %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色管理</title>
    <script language="javascript" type="text/javascript" src="/boot.js"></script>
 
    <style type="text/css"> 
        .treeM1 { padding-left: 1px; font-weight: bold; font-size: 14px; cursor:pointer; color: #336699; line-height: 150%; height: 20px; }
        .treeM2 { padding-left: 20px; font-size: 12px; padding-bottom: 5px; cursor: pointer; color: #335500; line-height: 150%; padding-top: 2px; }
        .treeM3 { padding-left: 35px; font-size: 12px; padding-bottom: 5px; color: #333333; line-height: 150%; padding-top: 2px; }
        .treeM4 { padding-left: 45px; font-size: 12px; padding-bottom: 5px; color: #555555; line-height: 100%; padding-top: 2px ;}
		
        a.csscol:link {
            font-size: 9px;
            color:#eee;
            text-decoration:underline;
        }
        a.csscol:visited     {
            font-size: 9px;
            color:#eee;
            text-decoration:underline;
        }    
        a.csscol:hover {
            font-size: 9px;
            color:#666;
        }
        a.csscol:active    
        {
            font-size: 9px;
            color:#000000;
            text-decoration:none;
        }
		a.csscol:hover {}
		
	</style>

<script type="text/javascript" language="javascript">

    var dbid = "";
    var control;

    var pageKind = "list";     //列表页面

    function jsReady() {
        dbid = "1," + $.cookie("ServicesDataBaseID");
        var loadJs = new Nature.loadFile(document);
        loadJs.loadJs("role1", "role2", function () {
            //Nature.Page.Form.Create();
            var para = $.getUrlParameter(document);
            Nature.CommonModule.ModuleForRole.CreateModuleGrid(para);
        });

    }

    function myCheck() {
        //alert("提交");
        Nature.CommonModule.ModuleForRole.SaveData(document);
    }
    
    
    
    function chkClick(me) {
        alert(me.id);
    }

</script>   
</head>
<body>
    <div id="divFormMain"><div id="spin"></div>
        <form id="dataForm">
            <div id="divRole"></div>
        </form> 
        <div style="text-align:center">
            <input type="button" name="BtnSave" value="保存权限设置" onclick="return myCheck();" id="BtnSave" class="input_01" />
        </div>
    </div>
   
</body>
</html>
