
/*
设置视图的默认值
2013-8-3  修改
*/

var button; /*按钮ID的文本框*/
var pvId;/*按钮ID的ID值*/
var frids;/**/

var pv;
var module;

var formEvent;
var commandControlInfo;
var urlPara;


var myId;
var myDbId;
var myDoc;

/* 给表单设置供应商ID */

function cusJsLoad(formEvent) {
    
    myDbId = formEvent.dataBaseId;
    myDoc = formEvent.win.document; 

    /*获取url参数  */
    urlPara = $.getUrlParameter(myDoc);

    frids = urlPara.frids.replace(/\"/g, "");
    //alert(frids);
    pvId = frids.split(",")[0];

    pv = $("#ctrl_1006010", myDoc);
    if (pv.val() == "") pv.val(pvId); /*视图id*/
    
    module = $("#ctrl_1006020", myDoc);
    module.val(pvId); /*模块id*/

    $("#tblForm tr td", myDoc).eq(0).css("width", "300px");

    /*外键设置为 0 ctrl_1006070*/
    var foreign = $("#ctrl_1006070", myDoc);
    if (foreign.val() == "") foreign.val(0); /*视图id*/

    /*视图类型的下拉列表框*/
    var viewKind = $("#ctrl_1006030", myDoc);
    viewKind.change(function () {
        showTr(this.value, myDoc);
    });

    showTr(viewKind.val(), myDoc);
    
    /*表ID的下拉列表框*/
    $("#ctrl_1006050", myDoc).change(function () {

        showTable(this.value, myDoc);

    });
    
    /*获取类型*/
    var kind = viewKind.val();
    
    if (typeof formEvent.window.parent.setList == "function")
        formEvent.window.parent.setList(kind);

}


function showTable(value, doc) {
    $("#ctrl_1006060", doc).val(value + ""); /*维护数据用表 */
    $("#ctrl_1006090", doc).val(value + "010"); /*主键 ctrl_1006090*/

}


/*模块ID的下拉列表框*/

function showTr(value, doc) {
    
    switch (value) {
        case "701":
            //列表视图
            trHide("#tr1006110,#tr1006100,#tr1006150", doc);
            trShow("#tr1006050,#tr1006060,#tr1006090,#tr1006070,#tr1006120,#tr1006130,#tr1006140", doc);
            break;
        case "702":
            //查询视图
            trHide("#tr1006050,#tr1006060,#tr1006090,#tr1006070,#tr1006110,#tr1006120,#tr1006130,#tr1006140,#tr1006150", doc);
            trShow("#tr1006100", doc);
            break;
        case "703":
            //表单视图
            trHide("#tr1006130,#tr1006140,#tr1006150", doc);
            trShow("#tr1006050,#tr1006060,#tr1006090,#tr1006070,#tr1006100,#tr1006110,#tr1006120", doc);
            break;
        case "704":
            //删除
            trHide("#tr1006070,#tr1006100,#tr1006110,#tr1006120,#tr1006130,#tr1006140#tr1006150", doc);
            trShow("#tr1006050,#tr1006060,#tr1006090", doc);
            break;
        case "705":
            //导出
            //$("#tr1006060,#tr1006100,#tr1006110,#tr1006120,#tr1006130,#tr1006140,#tr1006150", doc);
            break;
        case "706":
            //选择
            //$("#tr1006060,#tr1006100,#tr1006110,#tr1006120,#tr1006130,#tr1006140,#tr1006150", doc);
            break;
    }
}

/*显示*/
function trShow(id,doc) {
    $(id, doc).fadeIn("fast");
}
/*隐藏*/
function trHide(id, doc) {
    $(id, doc).fadeOut("fast");
}