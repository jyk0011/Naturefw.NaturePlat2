



GridFormat.prototype.defaultValue = {
    //"": [tr1083010, tr1083020, tr1083030, tr1083040, tr1083050, tr1083060, tr1083070, tr1083080, tr1083090, tr1083100, tr1083110, tr1083120, tr1083130, tr1083140, tr1083150, tr1083160, tr1083170, tr1083180, tr1083190, tr1083200, tr1083210],
    //"": [],
    "toTxtByCus": ["1083020"],
    "toTxtByMeta": ["1083030", "1083040", "1083050", "1083060", "1083070"],
    "toTxtByData": ["1083080", "1083090", "1083100", "1083050", "1083060", "1083070", "1083040", "1083110", "1083120", "1083130", "1083140", "1083150"],
    "timeFormat": ["1083160"],
    "numberFormat": ["1083160"],
    "toSpace": [],
    "imgName": ["1083170", "1083180"],
    "imgCode": ["1083170", "1083180"],
    "imgTmp": ["1083170", "1083180"],
    "link": [],
    "linkByCol": ["1083180", "1083190"],
    "div": ["1083200", "1083210"],
    "addCss": ["1083200", "1083210"]
};

GridFormat.prototype.keyForUpdate = {
    "kind": 1083010,    //格式化类型
    "item": 1083020,    //下拉列表框的item
    "metaKind": 1083030,    //元数据类型
    "columnKey": 1083040,    //列标识
    "isShowFlag": 1083050,    //是否显示ID
    "defKey": 1083060,    //默认值的id
    "defValue": 1083070,    //默认值的text
    "mdid": 1083080,    //模块ID
    "mpvid": 1083090,    //列表视图ID
    "fpvid": 1083100,    //查询视图ID
    "cacheKey": 1083110,    //缓存标识
    "colID": 1083120,    //对应字段ID
    "query": 1083130,    //查询条件

    "format": 1083160,    //格式化信息
    "height": 1083170,    //图片的高度，宽度自动
    "url": 1083180,    //显示图片的地址，域名加路径
    "urlPara": 1083190,    //替换的参数
    "maxHeight": 1083200,    //最大的高度
    "maxWidth": 1083210     //最大的宽度
};
GridFormat.prototype.needchange = {
    1: "mdid",
    2: "mpvid",
    3: "fpvid"
};

GridFormat.prototype.formatInfo = {
    1083010: {
        jsonFlag: "kind",       //格式化类型
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083020: {
        jsonFlag: "item",       //下拉列表框的item
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083030: {
        jsonFlag: "metaKind",       //元数据类型
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083040: {
        jsonFlag: "columnKey",      //列标识
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083050: {
        jsonFlag: "isShowFlag",//是否显示ID
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083060: {
        jsonFlag: "defKey",//默认值的id
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083070: {
        jsonFlag: "defValue",//默认值的text
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083080: {
        jsonFlag: "mdid",//模块ID
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083090: {
        jsonFlag: "mpvid",//列表视图ID
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083100: {
        jsonFlag: "fpvid",//查询视图ID
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083110: {
        jsonFlag: "cacheKey",//缓存标识
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083120: {
        jsonFlag: "colID",//对应字段ID
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083130: {
        jsonFlag: "query",
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083140: {
        jsonFlag: "",
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083150: {
        jsonFlag: "",
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083160: {
        jsonFlag: "format",//格式化信息
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083170: {
        jsonFlag: "height",//图片的高度，宽度自动
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083180: {
        jsonFlag: "url",//显示图片的地址，域名加路径
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083190: {
        jsonFlag: "urlPara",//替换的参数
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083200: {
        jsonFlag: "maxHeight",//最大的高度
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    },
    1083210: {
        jsonFlag: "maxWidth",//最大的宽度
        value: "",                  /*输入的值*/
        modValue: undefined,        /*原值*/
        isSimple: true              /*是否简单绑定*/
    }
};

/*根据原先的值*/
GridFormat.prototype.setValue = function () {
    var self = this;
    var value = opener.top.dataToOpenWindow["1010180"];
    var valueJson = eval("(" + value + ")");
    for (var key in valueJson) {
        if (typeof valueJson[key] != "undefined") {
            var colId = self.keyForUpdate[key];
            var colValue = valueJson[key];
            if (key == "query") {//将数据中的返回值 进行转换
                var json_str = "[";
                for (var i = 0; i < colValue.length;i++) {
                    json_str+= "\"" + colValue[i] + "\",";
                }
                colValue = (json_str + "]").replace("\",]", "\"]");
            }
            if (key == "item") {
                colValue = JSON.stringify(colValue);
            }
            self.formatInfo[colId].modValue = colValue;
            $("#FrmCommonForm_ctrl_" + colId).val(colValue);
            var jsonFlag = self.formatInfo[colId].jsonFlag;
            for (var o in self.needchange) {
                if (jsonFlag == self.needchange[o])
                    $("#FrmCommonForm_ctrl_" + colId).change();
            }
        }
    }
};

/*
    var defaults = {};

    var textObj = {
        kind: $("#FrmCommonForm_ctrl_1083010"),//格式化方式
        item: $("#FrmCommonForm_ctrl_1083020"),//选项
        metaKind: $("#FrmCommonForm_ctrl_1083030"),//元数据类型
        columnKey: $("#FrmCommonForm_ctrl_1083040"),//列标识
        isShowFlag: $("#FrmCommonForm_ctrl_1083050"),//是否显示ID
        defKey: $("#FrmCommonForm_ctrl_1083060"),//默认值的id
        defVal: $("#FrmCommonForm_ctrl_1083070"),//默认值的text
        mdid: $("#FrmCommonForm_ctrl_1083080"),//模块ID
        mpvid: $("#FrmCommonForm_ctrl_1083090"),//列表视图ID
        fpvid: $("#FrmCommonForm_ctrl_1083100"),//查询视图ID
        cacheKey: $("#FrmCommonForm_ctrl_1083110"),//缓存标识
        colID: $("#FrmCommonForm_ctrl_1083120"),//对应的字段ID
        query: $("#FrmCommonForm_ctrl_1083130"),//查询
        queryColID: $("#FrmCommonForm_ctrl_1083140"),//查询字段ID
        queryColValue: $("#FrmCommonForm_ctrl_1083150"),//查询字段ID和值
        format: $("#FrmCommonForm_ctrl_1083160"),//格式化信息
        height: $("#FrmCommonForm_ctrl_1083170"),//图片的高度，宽度自动
        url: $("#FrmCommonForm_ctrl_1083180"),//显示图片的地址，域名加路径
        urlPara: $("#FrmCommonForm_ctrl_1083190"),//替换的参数
        maxHeight: $("#FrmCommonForm_ctrl_1083200"),//最大的高度
        maxWidth: $("#FrmCommonForm_ctrl_1083210"),//最大的宽度
    };

    var format = new GridFormat();
    var xuanzhong = function (o,text) {
        var sel_val = "";
        o.children("option").each(function () {
            sel_val = $(this).text() == text || $(this).val() == text ? $(this).val() : null;
            if (sel_val != null) {
                if (o == textObj.kind) {
                    format.KindInit(sel_val);
                }
                o.children("option[value='" + sel_val + "']").attr("selected", "selected");
            }
        });
    };

var assign_init = function () {
        var str_j = opener.top.dataToOpenWindow["1010180"];
        var obj_j = eval("(" + str_j + ")");
        for (var key in obj_j) {
            if (typeof (obj_j[key] != "function")) {
                switch (key) {
                    case "kind":
                        xuanzhong(textObj.kind, obj_j[key]);
                        textObj.kind.change();
                        break;
                    case "item":
                        textObj.item.val(obj_j[key]);
                        break;
                    case "metaKind":
                        textObj.metaKind.val(obj_j[key]);
                        break;
                    case "columnKey":
                        textObj.columnKey.val(obj_j[key]);
                        break;
                    case "isShowFlag":
                        textObj.isShowFlag.val(obj_j[key]);
                        break;
                    case "defKey":
                        textObj.defKey.val(obj_j[key]);
                        break;
                    case "defVal":
                        textObj.defVal.val(obj_j[key]);
                        break;
                    case "mdid":
                        xuanzhong(textObj.mdid, obj_j[key]);
                        textObj.mdid.change();
                        //textObj.mdid.val(obj_j[key]);
                        break;
                    case "mpvid":
                        xuanzhong(textObj.mpvid, obj_j[key]);
                        textObj.mpvid.change();
                        //textObj.mpvid.val(obj_j[key]);
                        break;
                    case "fpvid":
                        xuanzhong(textObj.fpvid, obj_j[key]);
                        textObj.fpvid.change();
                        //textObj.fpvid.val(obj_j[key]);
                        break;
                    case "cacheKey":
                        textObj.cacheKey.val(obj_j[key]);
                        break;
                    case "colID":
                        xuanzhong(textObj.colID, obj_j[key]);
                        //textObj.colID.val(obj_j[key]);
                        break;
                    case "query":
                        textObj.query.val(obj_j[key]);
                        break;
                    case "queryColID":
                        textObj.queryColID.val(obj_j[key]);
                        break;
                    case "queryColValue":
                        textObj.queryColValue.val(obj_j[key]);
                        break;
                    case "format":
                        textObj.format.val(obj_j[key]);
                        break;
                    case "height":
                        textObj.height.val(obj_j[key]);
                        break;
                    case "url":
                        textObj.url.val(obj_j[key]);
                        break;
                    case "urlPara":
                        textObj.urlPara.val(obj_j[key]);
                        break;
                    case "maxHeight":
                        textObj.maxHeight.val(obj_j[key]);
                        break;
                    case "maxWidth":
                        textObj.maxWidth.val(obj_j[key]);
                        break;
                }
            }

        }
    };

    var GridAssign = function (info) {
        
    };

    GridAssign.Assign = function () {
            assign_init();
    };
    */