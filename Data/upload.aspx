<%@ Page Language="C#" AutoEventWireup="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>上传文件</title>
    <link href="http://natureservice.517.cn/Scripts/upload/css/default.css"  rel="stylesheet" type="text/css" />
    <link href="http://natureservice.517.cn/Scripts/upload/css/uploadify.css"  rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript" src="http://natureservice.517.cn/Scripts/jquery-1.8.2.min.js"></script>
    <script language="javascript" type="text/javascript" src="http://natureservice.517.cn/Scripts/NatureAjax/jQuery.loadJs.js"></script>
   
    <script language="javascript" type="text/javascript" src="http://natureservice.517.cn/Scripts/upload/swfobject.js"></script>
    <script language="javascript" type="text/javascript" src="http://natureservice.517.cn/Scripts/upload/jquery.uploadify.v2.1.0.min.js"></script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function() {
            $("#uploadify").uploadify({
                'uploader': '/Scripts/upload/uploadify.swf',
                'script': Nature.resourceUrl + '/data/UploadHandler.ashx',
                'cancelImg': Nature.resourceUrl + '/Scripts/upload/cancel.png',
                'folder': '11',
                'queueID': 'fileQueue',
                'auto': false,
                'multi': true,
                //'buttonText': '选择文件',
                //'buttonImg': '/aspnet_client/jquery/cancel.png',
                'fileDesc': '请选择图片文件',
                'fileExt': '*.jpg;*.gif',
                'onError': function() {
                    alert(fileObj.size);

                },
                'onSelect': function(e, queueId, fileObj) {
                    //alert("唯一标识:" + queueId + "\r\n" + "文件名：" + fileObj.name + "\r\n" + "文件大小：" + fileObj.size + "\r\n" + "创建时间：" + fileObj.creationDate + "\r\n" + "最后修改时间：" + fileObj.modificationDate + "\r\n" + "文件类型：" + fileObj.type);

                    //alert($("#uploadify")[0].value);
                    //$("#myImg")[0].src = $("#uploadify")[0].value;

                },
                'onComplete': function(event, queueId, fileObj, response, data) {
                    var a = response.toString().split('`');

                    $("#myImg")[0].src = a[0];
                    alert("上传完毕");
                    //alert(event);
                    //alert(queueId);
                    //alert(fileObj);
                    //alert(response);
                    //alert(data);

                    var controlID = "1002202";
                    alert(opener.document.getElementById(controlID).value);
                    var txt1 = opener.document.getElementById(controlID);
                    txt1.value = a[1];

                }

            });
        });  
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="float:left;">
        <div id="fileQueue"></div>
        <input type="file" name="uploadify" id="uploadify" />
        <p><a href="javascript:$('#uploadify').uploadifyUpload()">上传</a>| 
           <a href="javascript:$('#uploadify').uploadifyClearQueue()">取消上传</a></p>
    </div>
    <div id="divImg" style="float:left;overflow:auto ;width:300px;height:260px;">
        <img id="myImg"/>
    </div>

    </form>
</body>
</html>
