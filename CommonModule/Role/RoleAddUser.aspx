<%@ Page Language="C#" AutoEventWireup="true"   %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>角色里面选择用户</title>
    <script language="javascript" type="text/javascript" src="/boot.js"></script>
    <style type="text/css">
       #divFloatSearch{   
            background-color:#d2eefd ;   
            border: 1px solid #99bbe8;   
            padding:5px;
            position:absolute;   
            
            color: #15428b;
        } 
        #divSearchDetail {
            float: left;
        }
        .cssFind {
            float: left;
        }
        #divDataList {
            clear: both;
        }
 </style>

<script type="text/javascript" language="javascript">

    var para ;
    var dbid = "";
    var fwdbid = dbid;
    var aQuery = { c10084020: 7};
    
    var pageKind = "list";     //列表页面
    var pagerList = {};

    function jsReady() {
        dbid = "2," + $.cookie("ServicesDataBaseID");
        pagerList = new Nature.Pager.DataList(window, dbid);

        pagerList.ListLoad();
    }
     
    //保存本页里选中的记录
    function addCheck(callback) {

        var roleUser = {roleId:1,userIDs:1 };
        
        //提交数据
        var para = $.getUrlParameter(document);

        roleUser.roleId = para.frids.replace(/"/g, "").split(",")[0];
        roleUser.userIDs = pagerList.grid.getDataIds();
        
        //保存角色到模块、按钮
        //var urlPara = "action=RoleAddUser&mdid=" + para.mdid + "&mpvid=" + para.mpvid + "&fpvid=" + para.fpvid + "&bid=" + para.bid + "&id=" + para.id + "&frid=" + para.frid;
        var urlPara = "action=RoleAddUser&mdid=" + para.mdid + "&mpvid=" + para.mpvid + "&fpvid=" + para.fpvid + "&bid=" + para.bid + "&id=" + para.id + "&ids=" + pagerList.grid.getDataIds() + "&frids=" + para.frids;

        $.ajax({
            type: "POST",
            url: "/CommonModule/Role/PostRoleData.ashx?" + urlPara,
            data: roleUser,
            dataType: "text",
            cache: false,
            //timeout: 2000,
            error: function () {
                alert('提交表单信息的时候发生错误！');
            },

            success: function (msg) {
                //alert(msg);
                var re = eval("(" + msg + ")");
                if (re.err.length == 0) {
                    alert("保存成功！");
                    //清空表单
                } else {
                    alert(re.err);
                }

                if (typeof (callback) != "undefined") {
                    callback();
                }
            }
        });

    }

      
</script>   
</head>
<body style="overflow:inherit ; ">
    <div id="divMain"> 
        <div id="divButton"></div>
        
        <div id="divDataList"></div>
        <div id="divQuickPage"></div>
        <div id="divFloatSearch"  style="display:none;">
            <span id="span_title" style="width:95%; float:left;">查询条件 <a href="javascript:void(0)" id="A2">X</a></span>
            <form id="dataForm2"><div id="divFloatSearchDetail"  ></div></form>
        </div>
        
         <div style="text-align:left">
            <input type="button" name="BtnSave" value="添加选中的用户" onclick="return addCheck();" id="BtnSave" class="btn_disabled" />
            <input type="button" name="BtnSave2" value="添加本页用户" onclick="return addAll();" id="BtnSave2" class="btn_disabled" />
        </div>
    </div>
     
</body>
</html>
