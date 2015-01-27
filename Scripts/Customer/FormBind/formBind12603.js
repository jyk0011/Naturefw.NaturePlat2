/*
 设置模块的默认属性
 2013-11-26

*/

var myId;
var myDbId;
var myDoc;

/* 给下拉列表框加change事件，ajax获取品牌和类型 */
function cusJsLoad(  formEvent ) {

    myDbId = formEvent.dataBaseId;
    myDoc = formEvent.win.document;
   
    var manMd = new manageModle(formEvent.win.doc); //var prodCate = new ProductCategory(formEvent, commandControlInfo);

    manMd.init(formEvent);

    
    ///*获取指定的记录*/
    //prodCate.getData(prodCate.dataId, function (data) {
    //    var buttonId = prodCate.urlPara.bid;

    //    if (buttonId == 34202) {
    //        //添加下级分类
    //        prodCate.addSonNote(prodCate.dataId, data);
    //    }
    //    else if (buttonId == 34201) {
    //        /*添加同级分类*/
    //        prodCate.getNextOrder(prodCate.dataId, data["5507080"], data["5507060"], function (data2) {
    //            prodCate.addBrotherNote(data, data2.order * 1);
    //        });

    //    }
    //});

    //prodCate.parentChange();

    //formEvent.window.saveBefore = prodCate.saveBefore;

}


var manageModle = function (doc) {
    this.loadM = new Nature.Data.Manager();

    this.doc = doc;
    this.ctrl_1000010 = $("#ctrl_1000010", doc);
    this.ctrl_1000060 = $("#ctrl_1000060", doc);
    this.ctrl_1000100 = $("#ctrl_1000100", doc);
    this.ctrl_1000080 = $("#ctrl_1000080", doc);
    this.ctrl_1000090 = $("#ctrl_1000090", doc);
    this.ctrl_1000070 = $("#ctrl_1000070", doc);
    this.ctrl_1000140 = $("#ctrl_1000140", doc);
    this.ctrl_1000113 = $("#ctrl_1000113", doc);
    this.ctrl_1000120 = $("#ctrl_1000120", doc);
    this.ctrl_1000115 = $("#ctrl_1000115", doc);
    this.ctrl_1000015 = $("#ctrl_1000015", doc);
};

manageModle.prototype.init = function (formEvent) {
    var self = this;
    self.loadM.ajaxGetData({
        data: { action: "one", mdid: 126, mpvid: 12603, id: formEvent.urlPara.dataID, bid: 12602, dbid: myDbId },
        title: "绑定表单数据",
        success: function (json) {
            if (json != null) {
                self.ctrl_1000010.val(json.data[0]["1000010"]);
                self.ctrl_1000060.val(json.data[0]["1000060"]);
                self.ctrl_1000100.val(json.data[0]["1000100"]);
                self.ctrl_1000080.val(json.data[0]["1000080"]);
                self.ctrl_1000090.val(json.data[0]["1000090"]);
                self.ctrl_1000070.val(json.data[0]["1000070"]);
                self.ctrl_1000113.val(json.data[0]["1000113"]);
                self.ctrl_1000120.val(json.data[0]["1000120"]);
                self.ctrl_1000115.val(json.data[0]["1000115"]);
                self.ctrl_1000015.val(json.data[0]["1000015"]);
            }

            self.addSelect();

        }
    });
};

manageModle.prototype.addSelect = function () {
    var self = this;
    $("#divForm", self.doc).append("请选择要操作的表:<select name=\"lstTableID\" id=\"lstTableID\" class=\"lst select01\" style=\"font-size:12pt;\"></select>(会根据选中的表,设置视图的属性值)");
    self.loadM.ajaxGetData({
        data: { action: "listkey", mdid: 124, mpvid: 12401, fpvid: 0, id: -2, dbid: myDbId, pageno: 1, hasKey: 0 },
        title: "根据视图ID获取视图选择的字段列表",
        success: function (json) {
            if (json != null) {
                var option;
                for (var o in json.data) {
                    option = "<option id=\"" + o + "\">" + json.data[o]["1002020"] + "</option>";
                    $("#lstTableID", self.doc).append(option);
                }

            }
            self.addView();
            self.addBut();
            self.addKeyUp();
        }
    });
};

manageModle.prototype.addView = function () {
    var self = this;
    var div = $("<div id=\"addView\"></div>");
    var table = $("<table id=\"viewT\"></table>");
    var select = "<select  class=\"select_s1\" name=\"c1006030\"  warning=\"请填写视图类型\" disabled=\"disabled\">" +
        "<option value=\"701\">数据列表</option><option value=\"702\">查询表单</option>" +
        "<option value=\"703\">添加修改</option><option value=\"704\">删除</option></select>";
    var trText = "<tr><td><input id=\"lstView_0\" type=\"checkbox\" name=\"lstView$0\" checked=\"checked\"></td><td>" + select + "</td><td>" + self.ctrl_1000010.val() + "01_</td>" +
                     "<td><input type=\"text\" class=\"cssTxt input_t1\"   name=\"default\"  value=\"" + self.ctrl_1000060.val() + "列表\" title=\"{0}列表\"></td></tr>";
    trText += "<tr><td><input id=\"lstView_1\" type=\"checkbox\" name=\"lstView$1\" checked=\"checked\"></td><td>" + select + "</td><td>" + self.ctrl_1000010.val() + "02_</td>" +
                    "<td><input type=\"text\" class=\"cssTxt input_t1\"    name=\"default\" value=\"" + self.ctrl_1000060.val() + "查询\" title=\"{0}查询\"></td></tr>";
    trText += "<tr><td><input id=\"lstView_2\" type=\"checkbox\" name=\"lstView$2\" checked=\"checked\"></td><td>" + select + "</td><td>" + self.ctrl_1000010.val() + "03_</td>" +
                    "<td><input type=\"text\" class=\"cssTxt input_t1\"    name=\"default\" value=\"" + self.ctrl_1000060.val() + "删除\" title=\"{0}删除\"></td></tr>";
    trText += "<tr><td><input id=\"lstView_3\" type=\"checkbox\" name=\"lstView$3\" checked=\"checked\"></td><td>" + select + "</td><td>" + self.ctrl_1000010.val() + "04_</td>" +
                    "<td><input type=\"text\" class=\"cssTxt input_t1\"    name=\"default\" value=\"" + self.ctrl_1000060.val() + "表单/添加\" title=\"{0}表单/添加\"></td></tr>";
    trText += "<tr><td><input id=\"lstView_4\" type=\"checkbox\" name=\"lstView$4\" checked=\"checked\"></td><td>" + select + "</td><td>" + self.ctrl_1000010.val() + "05_</td>" +
                    "<td><input type=\"text\" class=\"cssTxt input_t1\"  s  name=\"default\" value=\"" + self.ctrl_1000060.val() + "修改\" title=\"{0}修改\"></td></tr>";

    table = table.append(trText);
    div = div.append(table);
    //$("div[name='butDiv']", self.doc).before(div);
    var divPV = $("#divAddPageView", self.doc);
    divPV.append(div);
    
    var but = "<input type=\"button\"  class=\"input_01\" value=\"添加视图\" id=\"add_view\" />";
    divPV.append(but);
    $("#addView select", self.doc).each(function (i) {
        if (i == 0)
            $(this).children("option:eq(0)").attr('selected', 'selected');
        if (i == 1)
            $(this).children("option:eq(1)").attr('selected', 'selected');
        if (i == 2)
            $(this).children("option:eq(3)").attr('selected', 'selected');
        if (i == 3)
            $(this).children("option:eq(2)").attr('selected', 'selected');
        if (i == 4)
            $(this).children("option:eq(2)").attr('selected', 'selected');
    });


};

manageModle.prototype.addBut = function () {
    var self = this;
    var div = $("<div id=\"addBut\">创建哪些按钮？</div>");
    var table = $("<table id=\"viewB\"></table>");
    var select = "<select  class=\"select_s1\" name=\"c1012050\"  warning=\"请填写按钮类型\" disabled=\"disabled\">" +
        "<option value=\"401\">查看</option><option value=\"402\">添加</option>" +
        "<option value=\"403\">修改</option><option value=\"404\">物理删除</option>" +
        "<option value=\"405\">查询</option><option value=\"406\">导出到Excel</option>" +
        "<option value=\"407\">导出到Access</option><option value=\"408\">添加后修改</option>" +
        "<option value=\"411\">超链接</option><option value=\"412\">逻辑删除</option></select>";
    var trText = "<tr><td><input id=\"lstView_0\" type=\"checkbox\" name=\"lstView$0\" checked=\"checked\"></td><td>" + select + "</td><td>" + self.ctrl_1000010.val() + "01_</td>" +
                     "<td><input type=\"text\" class=\"cssTxt input_t1\"  name=\"default\" value=\"" + self.ctrl_1000060.val() + "查看\" title=\"{0}查看\"></td></tr>";
    trText += "<tr><td><input id=\"lstView_1\" type=\"checkbox\" name=\"lstView$1\" checked=\"checked\"></td><td>" + select + "</td><td>" + self.ctrl_1000010.val() + "02_</td>" +
                    "<td><input type=\"text\" class=\"cssTxt input_t1\"    name=\"default\" value=\"" + self.ctrl_1000060.val() + "添加_a\" title=\"{0}添加_a\"></td></tr>";
    trText += "<tr><td><input id=\"lstView_2\" type=\"checkbox\" name=\"lstView$2\" checked=\"checked\"></td><td>" + select + "</td><td>" + self.ctrl_1000010.val() + "03_</td>" +
                    "<td><input type=\"text\" class=\"cssTxt input_t1\"    name=\"default\" value=\"" + self.ctrl_1000060.val() + "修改_u\" title=\"{0}修改_u\"></td></tr>";
    trText += "<tr><td><input id=\"lstView_3\" type=\"checkbox\" name=\"lstView$3\" checked=\"checked\"></td><td>" + select + "</td><td>" + self.ctrl_1000010.val() + "04_</td>" +
                    "<td><input type=\"text\" class=\"cssTxt input_t1\"    name=\"default\" value=\"" + self.ctrl_1000060.val() + "删除_d\" title=\"{0}删除_d\"></td></tr>";
    trText += "<tr><td><input id=\"lstView_4\" type=\"checkbox\" name=\"lstView$4\" checked=\"checked\"></td><td>" + select + "</td><td>" + self.ctrl_1000010.val() + "05_</td>" +
                    "<td><input type=\"text\" class=\"cssTxt input_t1\"    name=\"default\" value=\"" + self.ctrl_1000060.val() + "查询_f\" title=\"{0}查询_f\"></td></tr>";
    trText += "<tr><td><input id=\"lstView_5\" type=\"checkbox\" name=\"lstView$5\" checked=\"checked\"></td><td>" + select + "</td><td>" + self.ctrl_1000010.val() + "06_</td>" +
                    "<td><input type=\"text\" class=\"cssTxt input_t1\"    name=\"default\" value=\"" + self.ctrl_1000060.val() + "导出Excel\" title=\"{0}导出Excel\"></td></tr>";

    table = table.append(trText);
    div = div.append(table);
    //$("div[name='butDiv']", self.doc).before(div);
    var divButton = $("#divAddButton", self.doc);
    divButton.append(div);

    var but = "<input type=\"button\"  class=\"input_01\" value=\"添加按钮\" id=\"add_but\" />";
    //$("div[name='butDiv']", self.doc).before(but);
    divButton.append(but);


    $("#addBut select", self.doc).each(function (i) {
        if (i == 0)
            $(this).children("option:eq(0)").attr('selected', 'selected');
        if (i == 1)
            $(this).children("option:eq(1)").attr('selected', 'selected');
        if (i == 2)
            $(this).children("option:eq(2)").attr('selected', 'selected');
        if (i == 3) {
            $(this).children("option:eq(9)").attr('selected', 'selected');
            $(this).attr("disabled", null);
        }
        if (i == 4)
            $(this).children("option:eq(4)").attr('selected', 'selected');
        if (i == 5)
            $(this).children("option:eq(5)").attr('selected', 'selected');
    });


};
manageModle.prototype.addKeyUp = function () {
    var self = this;
    $("#ctrl_1000060",self.doc).bind("keyup", function () {
        var text = this.value;

        $("#viewT input[type='text']", self.doc).each(function () {
            var title = $(this).attr("title");
            title = title.replace("{0}", text);
            $(this).val(title);
        });
        $("#viewB input[type='text']", self.doc).each(function () {
            var title = $(this).attr("title");
            title = title.replace("{0}", text);
            $(this).val(title);
        });
    });
};


/*产品分类的表单js类*/
function ProductCategory(formEvent ) {
    this.formEvent = formEvent;
    this.commandControlInfo = formEvent;

    var doc = this.doc = formEvent.win.document;

    /*获取url参数*/
    this.urlPara = $.getUrlParameter(doc);

    this.dataId = this.urlPara.id.replace(/\"/g, "");
    //alert(dataId);

    myId = this.dataId;

    this.parentCategory = $("#ctrl_5507050", doc);  /*上级分类列表框*/
    this.parentIdAll = $("#ctrl_5507060", doc);     /*上级分类ID集合*/
    this.disOrder = $("#ctrl_5507080", doc);        /*排序*/
    this.level = $("#ctrl_5507045", doc);           /*等级*/

}

/*获取指定的记录*/
ProductCategory.prototype.getData = function (cateId, callback) {
    var self = this;

    self.loadM.ajaxGetData({
        data: {
            action: "one",
            mdid: self.formEvent.urlPara.moduleID,
            mpvid: self.formEvent.urlPara.masterPageViewID,
            id: cateId,
            bid: self.formEvent.urlPara.buttonId,
            dbid: self.formEvent.dataBaseId
        },
        title: "绑定表单数据",
        success: function (json) {
            /*得到记录，绑定到控件*/
            var value = self.formEvent.data = json.data[0];
            callback(value);
        }
    });

};


/*获取同级下一个节点的序号*/
ProductCategory.prototype.getNextOrder = function (cateId, order, pidall, callback) {
    var self = this;
    self.loadM.ajaxGetData({
        data: {
            action: "nextorder",
            order: order,
            pidall: pidall,
            mdid: 342,
            mpvid: 34205,
            id: cateId,
            dbid: self.commandControlInfo.dataBaseId
        },
        title: "绑定表单数据",
        success: function (json) {
            /*得到记录，绑定到控件*/

            callback(json);
        }
    });

};

/*添加下级分类*/
ProductCategory.prototype.addSonNote = function (cateId, data) {
    var self = this;
    self.parentCategory.val(cateId);
    self.parentIdAll.val(data["5507060"] + "," + cateId);
    var lvl = data["5507045"] * 1;
    self.level.val(lvl + 1);

    var order = data["5507080"] * 1;
    self.disOrder.val(order + 10);

};

/*添加同级分类*/
ProductCategory.prototype.addBrotherNote = function (data, order) {
    var self = this;
    self.parentCategory.val(data["5507050"]);
    self.parentIdAll.val(data["5507060"]);
    self.level.val(data["5507045"]);

    self.disOrder.val(order);
};


/*保存前修改其他记录的排序*/
ProductCategory.prototype.saveBefore = function (callback) {

    var urlPara = {
        action: "disorder+",
        order: $("#ctrl_5507080", myDoc).val(),
        mdid: 342,
        mpvid: 34205,
        id: myId,
        dbid: myDbId
    };

    self.loadM.ajaxGetData({
        data: urlPara,
        url: "/data/PostData.ashx",
        title: "修改排序",
        success: function (data) {
            if (data.err != "") {
                //出错了。
                callback(false);
            } else {
                //成功
                callback(true);

            }

        }
    });
};



/*保存前修改其他记录的排序*/
ProductCategory.prototype.parentChange = function (callback) {
    var self = this;

    self.parentCategory.change(function () {
        var id = $(this).val();
        alert(id);

        /*获取指定的记录*/
        self.getData(id, function (data) {
            /*设置默认值*/
            self.addSonNote(id, data);

            /*修改排序*/
            self.getNextOrder(id, data["5507080"], data["5507060"], function (data2) {
                self.disOrder.val(data2.order * 1);
            });
        });

    });

};