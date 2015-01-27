<%@ Page Language="C#" AutoEventWireup="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>表单字段排序</title>
    <script language="javascript" type="text/javascript" src="/Scripts/jquery-1.8.2.min.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/NatureAjax/jQuery.command.js"></script>
    <script language="javascript" type="text/javascript" src="/Scripts/NatureAjax/jQuery.dragClick.js"></script>
    <script language="javascript" type="text/javascript" src="/commonPage/nature.DragOrder.js"></script>
 <style type="text/css">
     #msg,#msg1 {
         clear: both;
     }
     #divDataList 
     {
         margin-top: 10px;
         margin-bottom: 10px;
         border: 0px solid #99bb18;
         clear: both;
     }
      
      .spancol {
          border: 1px solid #99bbe8;
          padding: 5px;
          margin: 5px;
          height: 20px;
          min-width: 20px;
          float: left;
      }
      
      .spanThis {
          background-color: bisque;
      }
      
      .spanOver {
          background-color:coral;
      }
 </style>

<script type="text/javascript" language="javascript">
    var dataID = -2;
    var frid = -2;

    var info = { url: "", OpenModuleID: 0, OpenPageViewID: 0, FindPageViewID: 0, ButtonID: 0, WebWidth: 0, WebHeight: 0 };
    
    //提交的结构
    var orderData = { action: "datagrid", col1ID: 1, col2ID: 2, kind: "exchange" };
       
    var dataListMeta;
    var localDate = new Date();
    var masterPageViewID = 0;

    var colLeft = {};
    var tmpColLeft = "[";
    
    window.onload = function () {
        //moduleId, mpvid, fpvid, url
        //获取URL参数

        //alert("aa");
        var para = $.getUrlParameter();

        $(".css_Form td").drag();
        //CreateDataGrid(para);
         
    };

    function CreateDataGrid(para) {

        var dd = new Date();
        $.ajaxGetMeta({
            data: { action: "grid", mpvid: para.mpvid, v: dd.getSeconds() + "_" + dd.getMilliseconds() },
            title: "数据列表元数据",
            success: function (msg) {
                CreateDataGrid1(para, msg);
            }
        });
    }


    function CreateDataGrid1(para, msg) {

        //    var dataListMeta = {
        //        "getTime": "2012-10-11 10:00:19",
        //        "useTime": "0.71320毫秒",
        //        "data": {
        //            "1000010": { "ColumnID": 1000010, "ColTitle": "模块ID", "ColHelp": "", "HelpStation": 1, "ColWidth": 0, "ColAlign": "left", "Kind": 1, "IsSort": false, "Format": "", "MaxLength": 0 },
        //            "1000120": { "ColumnID": 1000120, "ColTitle": "是否隐藏", "ColHelp": "", "HelpStation": 1, "ColWidth": 0, "ColAlign": "left", "Kind": 1, "IsSort": false, "Format": "", "MaxLength": 0 }
        //        },
        //        "datakeys": [1000010, 1000060, 1000090, 1000116, 1000117, 1000140, 1000130, 1000120]
        //    };

        dataListMeta = msg;

        var col = "<div id='c{id}' class='spancol'>{title}</div>";
        var key = dataListMeta.datakeys;
        var tmpDiv = $("<div>");
        for (var i = 0; i < key.length; i++) {
            var dd = dataListMeta.data[key[i]];

            var tmpCol = col.replace(/\{id\}/g, dd.ColumnID);
            tmpCol = tmpCol.replace(/\{title\}/g, dd.ColTitle);

            tmpDiv.append(tmpCol);
     
        }

        $("#divDataList").html(tmpDiv.html());

        $("#css_Form").drag( );

    }



    $(document).mousemove(function (e) {

        if ($("#divdrop").length == 0)
            return;

        var left = e.pageX;
        var top = e.pageY;
        //遍历，查找
        var changeSpan = 0;

        $(".css_Form td").each(function () {
            var thisTd = this;
            if (top > thisTd.offsetTop && left > thisTd.offsetLeft) {
                //比左上角坐标大，判断宽高
                if (top <= thisTd.offsetTop + thisTd.offsetHeight && left <= thisTd.offsetLeft + thisTd.offsetWidth) {
                    changeSpan = thisTd;
                }
            }
        });
         

        //判断左中右
        var tmpSpan = $(changeSpan);

        if ($(".spanThis").length > 0 && tmpSpan.length > 0) {
            if ($(".spanThis")[0].id == tmpSpan[0].id) {
                $(".spanOver").removeClass("spanOver");
                return;
            }
        }

        var ss = 0;
        if (tmpSpan.length > 0) {
            ss = (left - tmpSpan.offset().left) / tmpSpan.width();
            if (ss > 0.67) {
                orderData.kind = "right";
                $("#divdrop").html("加到后面");
            } else if (ss > 0.33) {
                orderData.kind = "exchange";
                $("#divdrop").html("交换位置");
            } else {
                orderData.kind = "left";
                $("#divdrop").html("加到前面");
            }
        }

        $(".spanOver").removeClass("spanOver");
        tmpSpan.addClass("spanOver");

        var zb = "X:" + e.pageX + " Y:" + e.pageY
            + "<br>changeSpan:" + $(changeSpan).html()
            + "<br>ss:" + ss

            ;
        $("#msg").html(zb);
    });


    function dragEnd() {
        $("#msg").append("<br>结束拖拽");
        //提交设置


    }
    
    var wdDepth = 0;        //单独打开页面的话，递归无法退出，所以加上深度的判断。
    function writeDebug(msg) {
        wdDepth++;
        if (wdDepth > 1)
            return;

        if (parent.DebugSet)
            parent.DebugSet(msg);
        else
            parent.writeDebug(msg);
    }

   
    
</script>   
</head>
<body>
  <div id="divDataList">
      <table class="css_Form" rules="all" width="900px">
                <tr id="tr1000010">
                    <td align="right">
                        模块ID：</td>
                    <td align="left">
                        <input id="FrmCommonForm_ctrl_1000010" class="cssTxt" maxlength="10" 
                            name="FrmCommonForm$ctrl_1000010" 
                            onchange="javascript:setTimeout('__doPostBack(\'FrmCommonForm$ctrl_1000010\',\'\')', 0)" 
                            onkeypress="if (WebForm_TextBoxKeyHandler(event) == false) return false;" 
                            size="10" value="145" warning="请填写模块ID" /></td>
                    <td align="right">
                        父级ID：</td>
                    <td align="left">
                        <select id="FrmCommonForm_ctrl_1000020" class="lst" 
                            name="FrmCommonForm$ctrl_1000020" style="FONT-SIZE: 12pt">
                            <option selected="" value="1">默认</option>
                            <option value="1">系统管理</option>
                            <option value="100">　操作角色管理</option>
                            <option value="101">　　操作角色里的用户</option>
                            <option value="102">　资源角色管理</option>
                            <option value="103">　　资源角色里的用户</option>
                            <option value="104">　账户管理</option>
                            <option value="105">　　用户拥有的角色</option>
                            <option value="106">　登录日志</option>
                            <option value="107">　操作日志</option>
                            <option value="108">　个性化设置</option>
                            <option value="109">　桌面设置</option>
                            <option value="110">　修改密码</option>
                            <option value="2">元数据管理</option>
                            <option value="120">　数据库文档Excel</option>
                            <option value="121">　数据库文档PD</option>
                            <option value="122">　数据库信息</option>
                            <option value="123">　　数据库字段信息</option>
                            <option value="124">　元数据信息</option>
                            <option value="125">　　表的字段的元数据信息</option>
                            <option value="126">　项目配置管理</option>
                            <option value="127">　　按钮维护</option>
                            <option value="128">　　视图信息列表</option>
                            <option value="130">　　　添加视图字段</option>
                            <option value="132">　　　修改列表视图字段</option>
                            <option value="129">　　　修改查询视图字段</option>
                            <option value="136">　　　修改表单视图字段</option>
                            <option value="133">　资源角色管理</option>
                            <option value="134">　代码生成器</option>
                            <option value="3">部门管理</option>
                            <option value="140">　部门维护</option>
                            <option value="4">员工管理</option>
                            <option value="141">问卷管理</option>
                            <option value="142">　问卷管理</option>
                            <option value="143">　问题管理</option>
                            <option value="144">　　选项管理</option>
                        </select></td>
                </tr>
                <tr id="tr1000060">
                    <td align="right">
                        模块名称：</td>
                    <td align="left">
                        <input id="FrmCommonForm_ctrl_1000060" class="cssTxt" maxlength="20" 
                            name="FrmCommonForm$ctrl_1000060" warning="请填写模块名称" /></td>
                    <td align="right">
                        父级ID集合：</td>
                    <td align="left">
                        <input id="FrmCommonForm_ctrl_1000030" class="cssTxt" maxlength="500" 
                            name="FrmCommonForm$ctrl_1000030" value="0,1" warning="请填写父级ID集合" /></td>
                </tr>
                <tr id="tr1000100">
                    <td align="right">
                        模块打开目标：</td>
                    <td align="left">
                        <input id="FrmCommonForm_ctrl_1000100" class="cssTxt" maxlength="10" 
                            name="FrmCommonForm$ctrl_1000100" value="_self" warning="请填写模块打开目标" /></td>
                    <td align="right">
                        图标：</td>
                    <td align="left">
                        <input id="FrmCommonForm_ctrl_1000080" class="cssTxt" maxlength="10" 
                            name="FrmCommonForm$ctrl_1000080" value="0" warning="请填写图标" /></td>
                </tr>
                <tr id="tr1000090">
                    <td align="right">
                        链接地址：</td>
                    <td align="left">
                        <input id="FrmCommonForm_ctrl_1000090" class="cssTxt" maxlength="200" 
                            name="FrmCommonForm$ctrl_1000090" size="30" value="#" warning="请填写链接地址" /></td>
                    <td align="right">
                        模块层级：</td>
                    <td align="left">
                        <input id="FrmCommonForm_ctrl_1000070" class="cssTxt" maxlength="2" 
                            name="FrmCommonForm$ctrl_1000070" value="2" warning="请填写模块层级" /></td>
                </tr>
                <tr id="tr1000140">
                    <td align="right">
                        排序：</td>
                    <td align="left">
                        <input id="FrmCommonForm_ctrl_1000140" class="cssTxt" maxlength="10" 
                            name="FrmCommonForm$ctrl_1000140" value="10080" warning="请填写排序" /></td>
                    <td align="right">
                        是否叶节点：</td>
                    <td align="left">
                        <select id="FrmCommonForm_ctrl_1000110" class="lst" 
                            name="FrmCommonForm$ctrl_1000110" style="FONT-SIZE: 12pt">
                            <option selected="" value="0">否</option>
                            <option value="1">是</option>
                        </select></td>
                </tr>
                <tr id="tr1000113">
                    <td align="right">
                        数据列表视图ID：</td>
                    <td align="left">
                        <input id="FrmCommonForm_ctrl_1000113" class="cssTxt" maxlength="10" 
                            name="FrmCommonForm$ctrl_1000113" value="14501" warning="请填写数据列表视图ID" /></td>
                    <td align="right">
                        是否隐藏：</td>
                    <td align="left">
                        <select id="FrmCommonForm_ctrl_1000120" class="lst" 
                            name="FrmCommonForm$ctrl_1000120" style="FONT-SIZE: 12pt">
                            <option selected="" value="0">正常显示</option>
                            <option value="1">菜单隐藏</option>
                            <option value="2">不可更新</option>
                            <option value="3">提示不可用</option>
                        </select></td>
                </tr>
                <tr id="tr1000115">
                    <td align="right">
                        查询控件视图ID：</td>
                    <td align="left">
                        <input id="FrmCommonForm_ctrl_1000115" class="cssTxt" maxlength="10" 
                            name="FrmCommonForm$ctrl_1000115" value="14502" warning="请填写查询控件视图ID" /></td>
                    <td align="right">
                        是否锁定：</td>
                    <td align="left">
                        <select id="FrmCommonForm_ctrl_1000130" class="lst" 
                            name="FrmCommonForm$ctrl_1000130" style="FONT-SIZE: 12pt">
                            <option selected="" value="0">不锁定</option>
                            <option value="1">锁定</option>
                        </select></td>
                </tr>
            </table>
  </div>
  <div id="msg"></div>
  <div id="msg1"></div>

</body>
</html>
