

var txt201 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10" };   //单行
var txt202 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10", rows: "4" };  //多行
var txt203 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10" };   //密码
var txt204 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10" };   //日期
var txt205 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10" };   //上传文件
var txt206 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10" };   //上传图片
var txt207 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10" };   //选择记录
var txt208 = { modWidth: "10", modMaxLen: "10", findWidth: "10", findMaxLen: "10" };   //FCK


var lst250 = { itemType: "sql", sql: "", isChange: "", width: "" };  //下拉列表框
var lst251 = { itemType: "sql", sql: "", isChange: "", width: "" };  //登录人
var lst252 = { itemType: "sql", sql: "", isChange: "", width: "" };  //级联
var lst253 = { itemType: "sql", sql: "", isChange: "", width: "" };  //单选组
var lst254 = { itemType: "sql", sql: "", isChange: "", width: "" };  //多选组
var lst255 = { itemType: "sql", sql: "", isChange: "", width: "" };  //多选
var lst256 = { itemType: "sql", sql: "", isChange: "", width: "" };  //列表框


function txtHide() {
    $("#tr1080010,#tr1080020,#tr1080030,#tr1080040,#tr1080050,#tr1080060,#tr1080070,#tr1080080,#tr1080090,#tr1080100").hide();
}

function txt201() {
    txtHide();
    $("#tr1080010,#tr1080020,#tr1080030,#tr1080040").show();
}
function txt202() {
    txtHide();
    $("#tr1080010,#tr1080050,#tr1080030,#tr1080040").show();
}
function txt203() {
    txtHide();
    $("#tr1080010,#tr1080020,#tr1080030,#tr1080040").show();
}
function txt204() {
    txtHide();
    $("#tr1080010,#tr1080020,#tr1080030,#tr1080040").show();
}
function txt205() {
    $("#tr1080010,#tr1080020,#tr1080030,#tr1080040,#tr1080050,#tr1080060,#tr1080070,#tr1080080,#tr1080090,#tr1080100").show();
}
function txt206() {
    $("#tr1080010,#tr1080020,#tr1080030,#tr1080040,#tr1080050,#tr1080060,#tr1080070,#tr1080080,#tr1080090,#tr1080100").show();
}
function txt207() {
    txtHide();
    $("#tr1080010,#tr1080020,#tr1080030,#tr1080040").show();
}


function lst250() {
    $("#tr1081010,#tr1081020,#tr1081030,#tr1081040,#tr1081050,#tr1081060,#tr1081070,#tr1081080").show();
    $("#tr1081110,#tr1081120,#tr1081130,#tr1081140,#tr1081090,#tr1081100").hide();
}
function lst251() {
    $("#tr1081010,#tr1081020,#tr1081030,#tr1081040,#tr1081050,#tr1081060,#tr1081070,#tr1081080,#tr1081090,#tr1081100").hide();
    $("#tr1081110,#tr1081120,#tr1081130,#tr1081140").show();

}

function lst252() {
    $("#tr1081010,#tr1081020,#tr1081030,#tr1081040,#tr1081050,#tr1081060,#tr1081070,#tr1081080,#tr1081090,#tr1081100").hide();
    $("#tr1081110,#tr1081120,#tr1081130,#tr1081140").show();

}
function lst254() {
    $("#tr1081010,#tr1081020,#tr1081030,#tr1081040,#tr1081050,#tr1081060,#tr1081070,#tr1081080,#tr1081090,#tr1081100").hide();
    $("#tr1081110,#tr1081120,#tr1081130,#tr1081140").show();

}
function lst253() {
    lst252();
}
function lst255() {
    $("#tr1081010,#tr1081020,#tr1081030,#tr1081040,#tr1081050,#tr1081060,#tr1081070,#tr1081080,#tr1081090,#tr1081100").hide();
    $("#tr1081110,#tr1081120,#tr1081130,#tr1081140").show();

}
function lst256() {
    $("#tr1081010,#tr1081020,#tr1081030,#tr1081040,#tr1081050,#tr1081060,#tr1081070,#tr1081080,#tr1081090,#tr1081100").hide();
    $("#tr1081110,#tr1081120,#tr1081130,#tr1081140").show();

}
 