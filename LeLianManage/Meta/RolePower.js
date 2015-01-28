

function RoleManage(pagerList, grid) {

    this.grid = grid;

    this.urlPara = pagerList.cmdInfo.urlPara;

    this.roleId = this.urlPara.dataID;

    this.dataTable = pagerList.grid.gridEvent.dataSet.data;

    this.url = "/LeLianManage/json/RoleManage.ashx";
    
}



//设置模块的列表
RoleManage.prototype.setList = function (callback) {

    var self = this;

    //记录集，数组形式
    var dataTable = self.dataTable;

    $("#divButtondivSearch,#divLeft,#divMid").hide();


    var tblHead = $("#divHead #grid");

    $("#divHead").css("height", "34px");

    //名字的缩进

    grid.find("tr").each(function(i) {
        var tr = $(this);

        if (i == 0) {
            tr.find("th:eq(0),th:eq(1),th:eq(2),th:eq(3)").hide();
            tblHead.find("tr:eq(0)").find("th:eq(0),th:eq(1),th:eq(2),th:eq(3)").hide();
            return true;
        } else {
            tr.find("td:eq(0),td:eq(1),td:eq(2),td:eq(3)").hide();
            tblHead.find("tr:eq(" + i + ")").find("td:eq(0),td:eq(1),td:eq(2),td:eq(3)").hide();
        }
        var drModle = dataTable[i - 1];


        //设置属性
        tr.attr("flag", "p" + drModle["1000030"]);

        var modleId = tr.find("#td1000010").html();
        var chkTd = tr.find("#td1000120");

        var levelTd = tr.find("#td1000070");
        var level = levelTd.html();

        var titleTd = tr.find("#td1000060");
        titleTd.find("div").attr("style", "padding-left:" + level * 10 + "px");

        //第一级粗体
        if (level == "1") {
            titleTd.find("div").attr("style", "font-size:14px;font-weight:bold;padding-left:" + level * 10 + "px");

        }

        //展开和合拢 slideDown
        titleTd.click({ dr: drModle }, function (info) {
            showHideTr(info.data.dr,false);

        });

        //选择按钮
        var chk = $('<input type="checkbox" id="chk_' + modleId + '" name="modleId">');
        chkTd.html(chk);
        
        //选择按钮的联动
        chk.click({ tr: tr, modleId: modleId,dr: drModle }, function (info) {
            var chk1 = $(this);
            var tr1 = info.data.tr;
            var mid = info.data.modId;

            var dr = info.data.dr;
            
            //设置右面的按钮都选中
            setBtncheck(tr1, chk1[0].checked); 
            
            //找到子节点，展开并且设置选中
            showHideTr(dr, true);
            
            //设置子节点的右面的按钮都选中
            sonBtnCheck(dr, chk1[0].checked);

            //设置父节点都被选中
            var pids = dr["1000030"].split(',');
            for (var a = 0; a < pids.length; a++) {
                var tr2 = self.grid.find("#tr" + pids[a]);
                var chks = tr2.find("#td1000120 input");
                if (chks.length > 0) {
                    chks[0].checked = true;
                    setBtncheck(tr2, true);
                }
            }

        });

        // 代码结束
        


        //设置子节点的按钮
        function sonBtnCheck(dr, isCheck) {
            var pIdPath = dr["1000030"] + "," + dr["1000010"];

            var son = $("#grid tr[flag ^='p" + pIdPath + "']");

            son.each(function () {
                var tr2 = $(this);

                var moduleChk = tr2.find("#td1000120 input")[0];
                moduleChk.checked = isCheck;
                setBtncheck(tr2, isCheck);

            });
            
        }

        //设置对应的按钮选中
        function setBtncheck(trBtn,isCheck) {
            //设置右面的按钮都选中
            var btnChks = trBtn.find("#td1000116 input");

            btnChks.each(function (a) {
                this.checked = isCheck;
            });
        }
        
        //展开和收拢tr
        function showHideTr(dr,isCheck) {
            var pIdPath = dr["1000030"] + "," + dr["1000010"];

            var son = $("#grid tr[flag ^='p" + pIdPath + "']");

            var isShow = dr.isShow;
            if (typeof isShow == "undefined")
                dr.isShow = true;

            if (dr.isShow) {
                if (isCheck == false) {
                    son.hide(400);
                    //son.slideUp(400);
                    dr.isShow = false;
                }
            } else {
                son.show(400);
                //son.slideUp(400);
                dr.isShow = true;
            }
        }

    });

    callback();

};

//加载已经选择的模块
RoleManage.prototype.setModuleChanged = function (callback) {

    var self = this;

    self.loadRoleModleData(function (roleModuleIds) {
        //把选择的模块设置上

        for (var key in roleModuleIds) {
            var modId = roleModuleIds[key]["1101030"];

            var chk = self.grid.find("#tr" + modId + " #td1000120 input[type='checkbox'] ");

            if (chk.length != 0) {
                chk[0].checked = true;
            }

        }

    });
};

//加载全部按钮
RoleManage.prototype.loadButton = function (callback) {
    this.loadButtonData(function (btnInfo) {
        for (var key in btnInfo) {
            var btn = btnInfo[key];

            var modleId = btn["1012020"];
            var btnId = btn["1012010"];
            var btnName = btn["1012040"];

            btnName = btnName.split('_')[0];

            var td = grid.find("#tr" + modleId + " #td1000116");

            var btnChk = $('<span style="margin:5px;"><input id="btn_' + btnId + '" type="checkbox" ><label for="btn_' + btnId + '">' + btnName + '</label>');

            td.append(btnChk);


        }

        callback();

    });
};


//加载已经选择的按钮
RoleManage.prototype.loadButtonChanged = function(callback) {
    var self = this;

    self.loadRoleButtonChangeData(function(btnInfo) {
        for (var key in btnInfo) {
            var btn = btnInfo[key];

            var modleId = btn["1102030"];
            var btnIds = btn["1102050"];
              

            var chks = grid.find("#tr" + modleId + " #td1000116 input");

            chks.each(function() {
                var id = this.id;

                var mid = id.split('_')[1];

                if (btnIds.indexOf(mid) >= 0) {
                    this.checked = true;
                }

            });
              

        }

        callback();
    });

};


//添加角色的模块
RoleManage.prototype.saveRoleModule = function (callback) {

    //收集选择的按钮

    var modIds = [];
    var modIdDels = [];

    this.grid.find("tr").each(function (index) {

        if (index == 0)
            return true;
        
        var tr = $(this);
        var chk = tr.find("#td1000120 input");
        
        if (chk[0].checked) {
            modIds.push(this.id.replace("tr",''));
        } else {
            modIdDels.push(this.id.replace("tr", ''));
        } 
    });
    
    this.saveRoleModuleData(modIds.join(","), modIdDels.join(","), function () {
        callback();
    });
    
};
 

//添加角色的按钮
RoleManage.prototype.saveRoleButton = function (callback) {
    var btnInfoArr = [];
    this.grid.find("tr").each(function (index) {

        if (index == 0)
            return true;
        
        var tr = $(this);
        var btnInfo = {
            moduleId: tr.find("#td1000010").html(),
            buttonIDs: ""
        };

        var chks = tr.find("#td1000116 input");
        var tmp = [];
        
        chks.each(function() {
            if (this.checked) {
                tmp.push(this.id.replace("btn_", ''));
            }
        });

        btnInfo.buttonIDs = tmp.join(',');
        btnInfoArr.push(btnInfo);
        
    });

    this.saveRoleButtonData(btnInfoArr, function () {
        callback();
    });
    
};
 