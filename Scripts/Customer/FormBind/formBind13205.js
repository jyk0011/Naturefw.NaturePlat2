
/*
设置视图的默认值
2013-8-3  修改
*/

var isLoad = false;

var myId;
var myDbId;
var myDoc;

/* 给表单设置供应商ID */

function cusJsLoad(formEvent) {

    myDbId = formEvent.dataBaseId;
    myDoc = formEvent.win.document;
    
    if (isLoad)
        return;

    isLoad = true;
     
    /*获取url参数  */
    urlPara = $.getUrlParameter(myDoc);
    
    $("#ctrl_1010180", myDoc).dblclick(function () {
        var url = "/SupportingPlatform/Javascript/GridFormat.aspx?mdid=132&mpvid=13206&fpvid=0&bid=13203&id=-2&frid=-2&doc=" + myDoc;
        window.open(url, "_blank", 'height=600,width=1100,top=100,left=200,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no');

        //window.open("/SupportingPlatform/Javascript/GridFormat.aspx?mdid=132&mpvid=13206&fpvid=0&bid=13203&id=-2&frid=-2");

        top.dataToOpenWindow = {
            1010180:this.value
        };

        top.dataSetByOpenWindow = function(value) {
            $("#ctrl_1010180", myDoc).val(value);
        };
        
    });


}