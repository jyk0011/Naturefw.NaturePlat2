/*
自动生成文档

*/

var docInfo = [
    {
        kind: "",   /*span、div、table*/
        id: 0,      /*标识*/
        para: {},   /*单击事件传递的参数*/
        click:"",   /*设置一个单击事件*/
        data: {}    /*数据集合，根据kind，生成table、div或者span*/
    }];


/*输出文档*/
function writeDoc(div) {

    if (typeof div == "undefined")
        div = $("#doc");
    
    var docDetail = "";
    for (var i = 0; i < docInfo.length; i++) {
        switch (docInfo[i].kind) {
            case "div":
                docDetail = "<div style='padding:5px;'>" + docInfo[i].data + "</div>";
                docDetail = $(docDetail);
                break;
            case "table":
                docDetail = writeDocTable(docInfo[i].data);
                break;
            case "span":
                docDetail = "<span>" + docInfo[i].data + "</span>";
                docDetail = $(docDetail);
                break;
        }

        
        if (typeof docInfo[i].click == "function")
            docDetail.click(docInfo[i].para, docInfo[i].click);
        
        div.append(docDetail);
        
    }
    
    //edit.sync();
    
}

/*输出table*/
function writeDocTable(data) {
    var title = data.title;
    var dataSource = data.dataSource;
    
    var table = $("<table id=\"grid\" rules=\"all\" class=\"table_default1\"></table>");
    var trth = $("<tr id=\"tr\" class=\"first\" >");
    var trtd = "<tr id=\"tr\" class=\"\" >";
    var th = "<th align=\"center\" >{d}</th>";
    var td = "<td >{d}</td>";
    
    var i;
    for (i = 0; i < title.length; i++) {
        trth.append(th.replace("{d}", title[i]));
    }

    table.append(trth);

    for (i = 0; i < dataSource.length; i++) {
        var trtdj = $(trtd);
        for (var j = 0; j < dataSource[i].length; j++) {
            trtdj.append(td.replace("{d}", dataSource[i][j]));
        }
        table.append(trtdj);
    }
    
    return table;
}

/*列表页面*/
function listPage(modInfo, callback) {

    var modId = modInfo.ModuleID;
    
    /*页面的基础信息*/
    docInfo.push({ kind: "div", id: modId, data: "<h3>页面基础信息：</h3>" });
    var modBaseInfo = "页面名称：" + modInfo.ModuleName
        + "<br/><br/>网址：" + modInfo.URL
        + "?v=1.***&mdid=" + modInfo.ModuleID + "&mpvid=" + modInfo.GridPageViewID + "&fpvid=" + modInfo.FindPageViewID + "";

    docInfo.push({ kind: "div", id: modId, data: modBaseInfo });

    /*页面列表字段*/
    showList(function() {
        /*页面查询字段*/
        showFind(function() {
            /*页面的功能按钮*/
            showButton(function() {
                callback();
            });
        });
    });

    /*页面的功能按钮对应的页面的详细信息*/

    /*页面列表字段*/
    function showList(callbackList) {
        
        getPageViewListInfo(modId, modInfo.GridPageViewID, function (info) {
            var data = info.data;
            var datakeys = info.datakeys;
            
            docInfo.push({ kind: "div", id: modInfo.GridPageViewID, data: "<h3>页面列表显示的字段：</h3>" });
            var index = 1;
            var title = ["序号", "字段名"];
            var dataSource = [];
            
            for (index = 0; index < datakeys.length; index++) {
                dataSource.push([index + 1, data[datakeys[index]].ColName]);
            }
            
            docInfo.push({
                kind: "table",
                id: modInfo.GridPageViewID,
                data: { title: title, dataSource: dataSource }
            });
            
            callbackList();

        });
    }

    /*页面查询字段*/
    function showFind(callbackFind) {
        getPageViewFindInfo(modId, modInfo.GridPageViewID, modInfo.FindPageViewID, function (info) {
            var data = info.controlInfo;
            var datakeys = info.controlInfokeys;
            
            docInfo.push({ kind: "div", id: modInfo.FindPageViewID, data: "<h3>页面的查询字段：</h3>" });

            var index ;
            var title = ["序号", "字段名", "控件类型", "查询方式"];
            var dataSource = [];

            for (index = 0; index < datakeys.length; index++) {
                var find = data[datakeys[index]];
                dataSource.push([index + 1, find.ColName, dicInfo.controlKind[find.ControlTypeID], dicInfo.findKind[find.Ser_FindKindID]]);
            }

            docInfo.push({
                kind: "table",
                id: modInfo.FindPageViewID,
                data: { title: title, dataSource: dataSource }
            });

            callbackFind();

        });
    }

    /*页面的功能按钮*/
    function showButton(callbackBtn) {
        getModeButtonInfo(modId, function (info) {
            var data = info.data;
            var datakeys = info.datakeys;

            docInfo.push({ kind: "div", id: modId, data: "<h3>页面包括的功能：</h3>" });
            var index = 1;
            for (index = 0; index < datakeys.length; index++) {
                var btnInfo = data[datakeys[index]];
                docInfo.push({
                    kind: "div",
                    id: modId,
                    data: (index + 1) + "、" + btnInfo.BtnTitle.split('_')[0] + " (详细)",
                    para: { btnInfo: btnInfo },
                    click: function (button) {
                        /*情况记录*/
                        docInfo.length = 0;
                        
                        var btn = $(this);
                        btnClick(btn, button.data.btnInfo);
                    }
                });
            }

            callbackBtn();
        });
    }
    
    /*单击按钮后显示那种页面*/
    function btnClick(btn,buttonInfo) {
        var url = buttonInfo.URL;
        
        if (url.indexOf("DataList.htm") > 0) {
            /*列表页面*/
            /*获取模块的信息*/
            getModeInfo(buttonInfo.OpenModuleID, function (modInfo2) {
                /*列表页面的信息*/
                listPage(modInfo2, function () {
                    /*生成完毕，输出文档*/
                    var div = $("#doc_" + buttonInfo.ButtonID);
                    if (div.length == 0)
                        div = $("<div id=\"doc_" + buttonInfo.ButtonID + "\" style=\"padding:20px;border:1px #eee solid;\">");
                    else {
                        div.html("");
                    }

                    btn.after(div);
                    writeDoc(div);
                });

            });
        } else {
            /*表单页面*/
            formPage(btn,buttonInfo);
        }
    }
}

/*表单页面*/
function formPage(btn,buttonInfo) {
   
    getPageViewFormInfo(buttonInfo.OpenModuleID, buttonInfo.OpenPageViewID, buttonInfo.ButtonID, function (info) {
        docInfo.push({ kind: "div", id: buttonInfo.ButtonID, data: "<h3>页面基础信息：</h3>" });
        var modBaseInfo = "页面名称：" + buttonInfo.BtnTitle
          + "<br/><br/>网址：" + buttonInfo.URL
          + "?v=1.***&mdid=" + buttonInfo.OpenModuleID + "&ppvid=" + buttonInfo.ModuleID
          + "&mpvid=" + buttonInfo.OpenPageViewID + "&fpvid=" + buttonInfo.FindPageViewID + "&bid=" + buttonInfo.ButtonID + "&bid=" + buttonInfo.ButtonID;

        docInfo.push({ kind: "div", id: modId, data: modBaseInfo });

        var data = info.controlInfo;
        var datakeys = info.controlInfokeys;

        docInfo.push({ kind: "div", id: buttonInfo.ButtonID, data: "<h3>页面的表单字段：</h3>" });

        var index = 1;
        var title = ["序号", "字段名", "控件类型", "验证方式", "提示信息", "字段说明"];
        var dataSource = [];

        var colIds = [];
        
        for (index = 0; index < datakeys.length; index++) {
            var form = data[datakeys[index]];
            dataSource.push([(index + 1), form.ColName, dicInfo.controlKind[form.ControlTypeID], dicInfo.checkKind[form.CheckTypeID], form.ColHelp, ""]);
            colIds.push(form.ColumnID);
        }

        docInfo.push({
            kind: "table",
            id: buttonInfo.ButtonID,
            data: { title: title, dataSource: dataSource }
        });
         
        var div = $("#doc_" + buttonInfo.ButtonID);
        if (div.length == 0)
            div = $("<div id=\"doc_" + buttonInfo.ButtonID + "\" style=\"padding:20px;border:1px #eee solid;\">");
        else {
            div.html("");
        }

        getColumnRemark(colIds, function (info2) {
            var remark = info2.remark;
            
            for (var key in remark) {
                dataSource[key * 1][5] = remark[key];
            }

            btn.after(div);
            writeDoc(div);
        });
        

    });
}


/*开始运行*/

/*获取传递过来的模块ID*/
var modId = para.id.replace(/\"/g, "");

/*获取模块的信息*/
getModeInfo(modId, function (modInfo) {
    /*列表页面的信息*/
    listPage(modInfo, function () {
        /*生成完毕，输出文档*/
        writeDoc();
    });
     
});
