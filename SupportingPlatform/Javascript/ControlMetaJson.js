
var thisMeta = {controlKind:201,meta:""};           //当前选择的控件类型，对于的元数据
       
var ctlMeta201 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10" };   //单行
var ctlMeta202 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10", rows: "4" };  //多行
var ctlMeta203 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10" };   //密码
var ctlMeta204 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10", event: "onClick", parameter: "" };   //日期
var ctlMeta205 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10", uploadType: "", fileKind: "", fileNameKind: "", filePath: "", folderKind: "",fileExt:""};   //上传文件
var ctlMeta206 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10", uploadType: "", fileKind: "", fileNameKind: "", filePath: "", folderKind: "",fileExt:""};    //上传图片
var ctlMeta207 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10", openWidth: "", openHeight: ""  };   //选择记录
var ctlMeta208 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10" };   //FCK


var ctlMeta250 = { itemType: "sql", width: "0", sql: "", item: "", isChange: "" };   //下拉列表框
var ctlMeta252 = { itemType: "sql", width: "0", sql: "", isChange: "", IsFristList:true,ListCount: 2, ListOtherColumnIDs: "", ListHtml: "" };  //级联
var ctlMeta253 = { itemType: "sql", width: "0", sql: "", item: "", repeatColumns: 0, repeatDirection: 1 };  //单选组
var ctlMeta254 = { itemType: "sql", width: "0", sql: "", item: "", repeatColumns: 0, repeatDirection: 1 };  //多选组
var ctlMeta256 = { itemType: "sql", width: "0", sql: "", item: "" };  //列表框
var ctlMeta251 = { itemType: "sql", width: "0", sql: "" };  //登录人
var ctlMeta255 = { item: "" };  //复选


function ChangeControlID() {
    var j;
    var num;
    var txtarr = ["modWidth", "modMaxLen", "findWidth", "findMaxLen", "rows", "openWidth", "openHeight", "uploadType", "fileNameKind", "filePath", "event", "parameter", "fileKind", "folderKind", "fileExt"];
    for (j = 0; j < txtarr.length; j++) {
        num = (1080000 + (j + 1) * 10);
        $("#FrmCommonForm_ctrl_" + num)[0].id = txtarr[j];
        $("#tr" + num)[0].id = "tr" + txtarr[j];
    }

    txtarr = ["itemType", "sql", "item", "cacheName", "jsonKey", "width", "isChange", "repeatColumns", "repeatDirection", "listCount", "isFristList", "colIDs", "listHtml"];
    for (j = 0; j < txtarr.length; j++) {
        num = (1081000 + (j + 1) * 10);
        $("#frmList_ctrl_" + num)[0].id = txtarr[j];
        $("#tr" + num)[0].id = "tr" + txtarr[j];
    }
     
}

function txtHide() {
    $("#trmodWidth,#trmodMaxLen,#trfindWidth,#trfindMaxLen,#trrows,#trevent,#trparameter,#tropenWidth,#tropenHeight,#truploadType,#trfileNameKind,#trfilePath,#trfileKind,#trfolderKind,#fileExt").hide();
}

function lstHide() {
    $("#tritemType,#trsql,#tritem,#trcacheName,#trjsonKey,#trwidth,#trisChange,#trisChange,#trrepeatColumns,#trrepeatDirection,#trlistCount,#trisFristList,#trcolIDs,#trlistHtml").hide();
}

function TextClick() {
    $("#divCtl10")[0].className = "current";
    $("#divCtl20")[0].className = "";

    $("#divKuang10,#divText").show();
    $("#divKuang20,#divList").hide();
    SetControlDefaultValueDe(201);
    SetControlShow();
}

function ListClick() {
    $("#divCtl10")[0].className = "";
    $("#divCtl20")[0].className = "current";

    $("#divKuang10,#divText").hide();
    $("#divKuang20,#divList").show();
    SetControlDefaultValueDe(250);
    SetControlShow();

}

function SetControlDefaultValue() {
    //alert(this.id);
    var cID = this.id.substring(6, 9); //控件类型，元数据编号
    SetControlDefaultValueDe(cID);
}

function SetControlDefaultValueDe(ctlId){

    thisMeta.controlKind = ctlId;
    thisMeta.meta = eval("ctlMeta" + ctlId);
    
    var a = thisMeta.meta;
    for (var item in a) {
        //alert( "#" + item);
        if ($("#" + item).val() == "")
            $("#" + item).val(a[item]);

    }
}

function SetControlShow() {
    var a = thisMeta.meta;
    txtHide();
    lstHide();
    for (var item in a) {
        //alert( "#tr" + item);
        $("#tr" + item).show();
    }
}
