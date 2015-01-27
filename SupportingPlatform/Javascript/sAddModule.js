var sAddModule = function () {
    this.ctrl_1000010 = $("#ctrl_1000010");
    this.ctrl_1000060 = $("#ctrl_1000060");
    this.ctrl_1000100 = $("#ctrl_1000100");
    this.ctrl_1000080 = $("#ctrl_1000080");
    this.ctrl_1000090 = $("#ctrl_1000090");
    this.ctrl_1000070 = $("#ctrl_1000070");
    this.ctrl_1000140 = $("#ctrl_1000140");
    this.ctrl_1000113 = $("#ctrl_1000113");
    this.ctrl_1000120 = $("#ctrl_1000120");
    this.ctrl_1000115 = $("#ctrl_1000115");
    this.ctrl_1000015 = $("#ctrl_1000015");
};

sAddModule.prototype.init = function (id, dbid) {
    var self = this;
    $.ajaxGetData({
        data: { action: "one", mdid: 126, mpvid: 12603, id: id, bid: 12602, dbid: dbid },
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
        }
    });
};

sAddModule.prototype.addSelect = function (dbid) {
    var self = this;
    $("#divForm").append("请选择要操作的表:<select name=\"lstTableID\" id=\"lstTableID\" class=\"lst select01\" style=\"font-size:12pt;\"></select>(会根据选中的表,设置视图的属性值)");
    $.ajaxGetData({
        data: { action: "listkey", mdid: 124, mpvid: 12401, fpvid: 0, id: -2, dbid: dbid, pageno: 1, hasKey: 0 },
        title: "根据视图ID获取视图选择的字段列表",
        success: function (json) {
            if (json != null) {
                var option;
                for (var o in json.data) {
                    option = "<option id=\"" + o + "\">" + json.data[o]["1002020"] + "</option>";
                    $("#lstTableID").append(option);
                }
            }
        }
    });
}