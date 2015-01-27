function FormFunction() {
    //表单里的处理函数 FrmColumn_ctrl_1004120
    var ctlId = "#ctrl_1004120";
    if ($(ctlId).length > 0) FormCtrl_1004120(ctlId);

    //ctlId = "#FrmColumn_ctrl_1004120";
    if ($(ctlId).length > 0) FormCtrl_1004120(ctlId);
}

function FormCtrl_1004120(ctlId) {
    //字段的描述
    $(ctlId).dblclick(function () {
        //alert("a");
        var url = "/SupportingPlatform/Javascript/ControlInfo.aspx?mdid=125&mpvid=12504&fpvid=0&bid=12506&id=-2&frid=-2";
        window.open(url, "_blank", 'height=600,width=1100,top=100,left=200,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no');
    });
}
 