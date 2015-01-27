/*
设置按钮的默认值
2013-8-3 
*/


var button; /*按钮ID的文本框*/
var buttonId;/*按钮ID的ID值*/
var frids;/**/

var module;/*所属模块*/
var openModule;/*打开模块*/

var url;/*打开的网址*/
var divWidth;/*打开的宽度*/
var divHeight;/*打开的高度*/

var openMasterPV;/*打开的主视图*/
var openFindPV;/*打开的查询图*/

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
    buttonId = frids.split(",")[0];
    
    button = $("#ctrl_1012010", myDoc);
    if (button.val() == "") button.val(buttonId);
    
    module = $("#ctrl_1012020", myDoc);
    if (module.val() == "-10") module.val(buttonId);

    openModule = $("#ctrl_1012022", myDoc);
    if (openModule.val() == "-10") openModule.val(buttonId);

    openMasterPV = $("#ctrl_1012030", myDoc);
    if (openMasterPV.val() == "") openMasterPV.val(buttonId);
    
    openFindPV = $("#ctrl_1012033", myDoc);
    if (openFindPV.val() == "") openFindPV.val(buttonId);

    //url = $("#ctrl_1012070", myDoc);
    //url.val("DataList.htm");

    //divWidth = $("#ctrl_1012080", myDoc);
    //divWidth.val("800");

    //divHeight = $("#ctrl_1012090", myDoc);
    //divHeight.val("500");

    //$("#ctrl_1012100_0", myDoc)[0].checked = true; /*是否需要选择记录*/
    
    //$("#ctrl_1012110", myDoc).val("150"); /*是否需要选择记录*/

    


}