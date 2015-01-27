<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlInfo.aspx.cs" Inherits="NatureFramework.SupportingPlatform.Javascript.ControlInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>处理控件描述信息</title>
    <link rel="stylesheet" type="text/css" href="http://LCNatureService.nature.com/websiteStyle/MisStyle_v2.css" />

    <script language="javascript" type="text/javascript" src="/boot.js"></script>
    <script language="javascript" type="text/javascript">
        var fid = "";
        var myTxtID = "";
        
        var pageKind = "list";     //首页

        function jsReady() {
            var loadscript = new Nature.LoadScript(document);
            
            loadscript.js("/Scripts/json.js", function () {
                loadscript.js("ControlMetaJson.js?v=1", function () {
                    $("#divKuang10,#divText").show();
                    $("#divKuang20,#divList").hide();

                    //修改控件的ID
                    ChangeControlID();

                    //一级标签的click
                    $("#divCtl10").click(TextClick);
                    $("#divCtl20").click(ListClick);

                    //二级标签的click
                    var i;
                    for (i = 201; i <= 208; i++) {
                        $("#divCtl" + i).click(SetControlDefaultValue);
                        $("#divCtl" + i).click(SetControlShow);   //eval("txt" + i)
                    }

                    for (i = 250; i <= 256; i++) {
                        $("#divCtl" + i).click(SetControlDefaultValue);
                        $("#divCtl" + i).click(SetControlShow);
                    }
                    //设置控件失去焦点事件
                    $("input,textarea,select").blur(createMeta);


                    //提取控件描述信息 ctrl_1004120
                    var tmpMeta = opener.document.getElementById("ctrl_1004120").value;
                    var ctlKind = opener.document.getElementById("ctrl_1004080").value;
                    //alert('({ControlKind:' + ctlKind + ",ControlInfo:{" + tmpMeta + '}})');
                    //var meta = eval('({controlKind:' + ctlKind + ",meta:" + tmpMeta + '})');
                    thisMeta.controlKind = ctlKind * 1;
                    thisMeta.meta = eval('ctlMeta' + ctlKind);
                    tmpMeta = eval('(' + tmpMeta + ')');
                    var a = tmpMeta;
                    for (var item in a) {
                        thisMeta.meta[item] = a[item];
                    }

                    //设置内容
                    setControlValue();
                    //显示控件
                    //SetControlShow();
                });

            });
            
          
        }

      
        function setControlValue() {

            //第一次，给文本框赋值
            var meta = thisMeta.meta;
            for (var item in meta) {
                //alert("meta中" + item + "的值=" + meta[item]);
                $("#" + item)[0].value = meta[item];
            }

            //调用控件对应的单击事件，显示需要的控件
            if (thisMeta.controlKind < 250) {
                TextClick();
                SetControlShow();
            } else {
                ListClick();
                SetControlShow();
                
            }

        }

        function createMetaReturn() {
            createMeta();
            opener.document.getElementById("ctrl_1004120").value = $("#txtMeta")[0].value;
            window.close();
        }
        function createMeta() {
            var re = "";
            var a = thisMeta.meta;
            for (var item in a) {
                a[item] = $.trim($("#" + item).val());
            }

            re = JSON.stringify(a);
            re = re.replace("\"sql\":\"\"," , "");
            re = re.replace("\"item\":\"\"," , "");
            
            $("#txtMeta")[0].value = re;

        }

        
        
    </script>
    <style type="text/css">
        body {
            margin: 10px;
        }
         
        .left{
            float: left;
            border:1px solid #99bbe8;
        
        }
       
        #divKuang10,#divKuang20 {
            width: 210px;
        }
        .layout div {
            margin: 5px;
            cursor: pointer;
        }
        
    </style>
</head>
<body style="margin: 10px;">
    <form id="form1" runat="server">
    
        <div class="tabsbox" style="float: left">
            <div class="tabnav">
                
                <ul class="tabsmenu">
                    <li id="divCtl10" class="current" >文本框</li>
                    <li id="divCtl20">列表框</li>
                </ul>
            </div>
            <div class="tabcontent">
                <div id="divKuang10" class="layout">
                    <div id="divCtl201">单行文本框</div>
                    <div id="divCtl202">多行文本框</div>
                    <div id="divCtl203">密码框</div>
                    <div id="divCtl204">日期框</div>
                    <div id="divCtl205">上传图片</div>
                    <div id="divCtl206">上传文件</div>
                    <div id="divCtl207">选择记录</div>
                    <div id="divCtl208">FCK</div>
                </div>
                <div id="divKuang20" class="layout">
                    <div id="divCtl250">下拉列表框</div>
                    <div id="divCtl251">登录人</div>
                    <div id="divCtl252">级联框</div>
                    <div id="divCtl253">单选组</div>
                    <div id="divCtl254">复选组</div>
                    <div id="divCtl255">CheckBox</div>
                    <div id="divCtl256">列表框</div>
                </div>
         
            </div>
        </div>
    
        <div style="float: left">
            <div id="divText"  style="width:600px; padding:10px;" >
                <Nature:DataForm runat="server" ID="FrmCommonForm"/>
            </div>
            <div id="divList"   style="width:600px; padding:10px;" >
                <Nature:DataForm runat="server" ID="frmList"/>
            </div>
         </div>
        
        <div style="clear:both"></div>
        <div style="text-align:center">
            <input type="button" value=" 返 回 " onclick="createMetaReturn()"/>  
            <input type="button" value=" 生 成 " onclick="createMeta()"/> 
            <textarea id="txtMeta" cols="80" name="S1" rows="10"></textarea>
        </div>
     
    </form>
</body>
</html>