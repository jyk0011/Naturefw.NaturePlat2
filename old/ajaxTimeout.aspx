<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxTimeout.aspx.cs" Inherits="NatureFramework.ajaxTimeout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script language="javascript" type="text/javascript">
        var timespace = 0;      //最后两次按键的时间间隔
        var isFrist = true;     //是不是第一次按键
        var isOnKeydown = false;//是否按过按键
        var isAjax = false;     //是否提交过信息
        
        document.onkeydown = function() {
            var msg = document.getElementById("msg");
            msg.innerHTML += event.keyCode + "_";

            if (isOnKeydown == false) {
                //没有按过按键
                isFrist = true;
            } else {
                //按过按键
                isFrist = false;
            }
            
            isOnKeydown = true; //有按键
            
            if (!isFrist) {
                //不是第一次按键，计算间隔
                timespace = 10;
            }
            
            switch (event.keyCode) {
                case 65:
                    //测试用，模拟ajax返回，重置状态
                    isAjax = false;
                    isOnKeydown = false;
                    break;
            }

        };

        window.onload = function() {

            setTimeout(function aa() {
                var msg = document.getElementById("msg");

                if (!isAjax) {
                    //没提交信息
                    if (isOnKeydown) {
                        //有按键，判断是不是第一次按键
                        if (isFrist) {
                            //第一次按键
                            msg.innerHTML += "<br>1、第一次按键";
                            send();
                        } else {
                            //不是第一次按钮，判断时间间隔
                            msg.innerHTML += "<br>2、不是第一次按键";
                            send();
                        }
                    }
                }
                
                setTimeout(aa, 500);

            }, 500);
        };
        
        function send() {
            //ajax提交信息
            isAjax = true;
            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input id="timeout" type="text"/>
    </div>
        <div id="msg"></div>
    </form>
</body>
</html>
