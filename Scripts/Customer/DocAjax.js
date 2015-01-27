
/*
自动生成文档
获取元数据
*/


var load = new Nature.Data.Cache();

/*获取模块信息*/
function getModeInfo(modId, callback) {

    $.ajaxGetData({
        data: { action: "one", mdid: 126, mpvid: 12603, id: modId, bid: 12602, dbid: "1,"+dbid },
        title: "绑定表单数据",
        success: function(json) {
            //得到记录，绑定到控件
            var module = json.data[0];
            callback({
                ModuleID: module[1000010],
                ModuleName: module[1000060],
                URL: module[1000090],
                GridPageViewID: module[1000113],
                FindPageViewID: module[1000115]
                
            });
          
        }
    });
        
}

/*获取模块的按钮信息*/
function getModeButtonInfo(modId, callback) {
    load.ajaxGetMeta({
        data: { action: "button", mdid: modId, dbid: dbid, cacheKey: modId },
        title: "按钮信息",
        success: function (info) {
            callback(info);
        }
    });
}

/*获取页面列表视图的信息*/
function getPageViewListInfo(modId, mpvid, callback) {
    load.ajaxGetMeta({
        data: { action: "grid", mdid: modId, mpvid: mpvid, dbid: dbid, cacheKey: mpvid },
        title: "数据列表元数据",
        success: function (info) {
            callback(info);
        }
    });
}

/*获取页面查询视图的信息*/
function getPageViewFindInfo(modId, mpvid, fpvid, callback) {
    load.ajaxGetMeta({
        data: { action: "find", mdid: modId, mpvid: mpvid, fpvid: fpvid, dbid: dbid + ",4", cacheKey: fpvid },
        title: "查询控件元数据",
        success: function (info) {
            callback(info);
        }
    });
}


/*获取页面表单视图的信息*/
function getPageViewFormInfo(modId, mpvid, bid, callback) {
    load.ajaxGetMeta({
        data: { action: "form", mdid: modId, mpvid: mpvid, bid: bid, dbid: dbid + ",4", cacheKey: mpvid + "_" + bid },
        title: "表单信息",
        success: function (info) {
            callback(info);
        }
    });
}

/*获取文档里的字段的说明*/
function getColumnRemark(colIds, callback) {
    $.ajax({
        type: "GET",
        dataType: "json",
        cache: false,
        url: "/SupportingPlatform/Document/GetColRemark.ashx",
        data: { id: colIds.join(',') },
        error: function () {
            alert("获取字段说明的时候发生错误！");
        },

        success: function(msg) {
            if (typeof(parent.DebugSet) != "undefined")
                parent.DebugSet(msg.debug);

            callback(msg);

        }
    });
    
    
}

//http://nature.com/SupportingPlatform/Document/GetColRemark.ashx?id=5090010

var dicInfo = {
    checkKind: {
        101: "不验证",
        102: "自然数",
        103: "整数",
        104: "小数",
        105: "日期",
        106: "必填"
    },

    controlKind: {
        201: "单行文本",
        202: "多行文本",
        203: "密码框",
        204: "日期",
        205: "上传文件",
        206: "上传图片",
        207: "选择记录",
        208: "在线编辑FCK",
        250: "下拉列表框",
        251: "登录人",
        252: "级联列表",
        253: "单选组",
        254: "多选组",
        255: "多选",
        256: "列表框"
    },

    buttonKind: {
        401: "查看",
        402: "添加",
        403: "修改",
        404: "物理删除",
        405: "查询",
        406: "导出到Excel",
        407: "导出到Access",
        408: "添加后修改",
        411: "超链接",
        412: "逻辑删除"
    },

    findKind: {
        2001: "等于数字",
        2002: "等于文字",
        2003: "不等于数字",
        2004: "不等于文字",
        2005: "包含",
        2006: "不包含",
        2007: "开头是",
        2008: "结尾是",
        2011: "大于",
        2012: "小于",
        2013: "大于等于",
        2014: "小于等于",
        2101: "时间范围内",
        2102: "数字范围内",
        2103: "大于且小于等于",
        2104: "大于等于且小于",
        2105: "大于且小于",
        2201: "在数字集合中",
        2202: "在文字集合中",
        2301: "不处理",
        2302: "作为表名"
    }
}