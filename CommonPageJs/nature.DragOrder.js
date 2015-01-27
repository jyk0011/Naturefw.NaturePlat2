/*
* 拖拽控件，进行排序。
* ajax提交数据
*  

*/


//控件描述信息的格式


function myDrag() {
    //alert("a");
    tmpColLeft = "[";
    $("#divDataList .spancol").drag( );
    $("#divDataList .spancol").click(function () {
        $(".spanThis").removeClass("spanThis");
        $(this).addClass("spanThis");
    }).mousemove(function () {
        //$("#msg1").html($(this).offset().left);
    }).each(function () {
        tmpColLeft += "{id:" + this.id + ",left:" + $(this).offset().left + "},";
    });

    tmpColLeft = tmpColLeft.substring(0, tmpColLeft.length - 1);
    tmpColLeft += "]";
    //alert(tmpColLeft);

    colLeft = eval(tmpColLeft);
}


$(document).mousemove(function (e) {

    if ($("#divdrop").length == 0)
        return;

    var left = e.pageX;
    var top = e.pageY;
    //遍历，查找
    var changeSpan = 0;

    for (var i = 0; i < colLeft.length; i++) {
        //var thisSpan = $(colLeft[i].id);
        var thisSpan = colLeft[i].id;
        if (top > thisSpan.offsetTop && left > thisSpan.offsetLeft) {
            //比左上角坐标大，判断宽高
            if (top <= thisSpan.offsetTop + thisSpan.offsetHeight && left <= thisSpan.offsetLeft + thisSpan.offsetWidth) {
                changeSpan = colLeft[i].id;
            }
        }

    }

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
    //$("#msg").append("<br>结束拖拽");
    //提交设置
    SetOrder();

}

function SetOrder() {

    if ($(".spanThis").length == 0 || $(".spanOver").length == 0)
        return;

    //修改排序
    var para = $.getUrlParameter();
    //orderData.mpvid = para.mpvid;
    orderData.col2ID = $(".spanThis")[0].id.replace("c", "");
    orderData.col1ID = $(".spanOver")[0].id.replace("c", "");

    //提交表单
    $.ajax({
        type: "POST",
        url: "/JsonServer/SetMeta.ashx?mpvid=" + para.mpvid,
        data: orderData,
        dataType: "text",
        cache: false,
        //timeout: 2000,
        error: function () {
            alert('提交排序信息的时候发生错误！');
        },

        success: function (msg) {
            //alert(msg);

            CreateDataGrid(para);
        }
    });
}