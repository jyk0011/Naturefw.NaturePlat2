


//加载已经选择的模块
RoleManage.prototype.loadRoleModleData = function (callback) {
     
    var ajaxUrlPara = {
        dbid: dbid,//数据库连接字符串
        mdid: 12,//模块ID
        mpvid: 1203,//显示用视图类
        fpvid: 1204,//查询视图类
        bid: 1210,//按钮标志做权限判断
        id: -2,//记录ID，修改时用
        frid: this.roleId,//外键
        frids: -2,//外键
        action: 'listkey',//后台调用函数

        hasKey: 0//有没有查询条件
    };
    var loadM = new Nature.Data.Manager();
    //提交表单
    loadM.ajaxGetData({
        urlPara: ajaxUrlPara,
        title: "加载已经选择的模块",
        success: function (data) {
            
            callback(data.data);

        }
    });

};




//加载全部按钮
RoleManage.prototype.loadButtonData = function (callback) {
    
    //看看缓存
    var btnCache = top.__cache["btnCacheforRole"];
    if (typeof btnCache != "undefined") {
        //有缓存
        callback(btnCache);
        return;
    }

    var ajaxUrlPara = {
        dbid: dbid,//数据库连接字符串
        mdid: 12,//模块ID
        mpvid: 1201,//显示用视图类
        fpvid: 1202,//查询视图类
        bid: 1210,//按钮标志做权限判断
        id: -2,//记录ID，修改时用
        frid: -2,//外键
        frids: -2,//外键
        action: 'listkey',//后台调用函数

        hasKey: 0//有没有查询条件
    };
    var loadM = new Nature.Data.Manager();
    //提交表单
    loadM.ajaxGetData({
        urlPara: ajaxUrlPara,
        title: "加载全部按钮",
        success: function (data) {
            top.__cache["btnCacheforRole"] = data.data;
            callback(data.data);
        }
    });
};


//加载已经选择的按钮
RoleManage.prototype.loadRoleButtonChangeData = function(callback) {
    var ajaxUrlPara = {
        dbid: dbid,//数据库连接字符串
        mdid: 12,//模块ID
        mpvid: 1205,//显示用视图类
        fpvid: 1206,//查询视图类
        bid: 1210,//按钮标志做权限判断
        id: -2,//记录ID，修改时用
        frid: this.roleId,//外键
        frids: this.roleId,//外键
        action: 'listkey',//后台调用函数

        hasKey: 0//有没有查询条件
    };
    var loadM = new Nature.Data.Manager();
    //提交表单
    loadM.ajaxGetData({
        urlPara: ajaxUrlPara,
        title: "加载已经选择的按钮",
        success: function(data) {
            callback(data.data);

        }
    });
};

//添加角色的模块
RoleManage.prototype.saveRoleModuleData = function (modIds,modDels,callback) {

    var jsonValue = {
        mids: modIds,    //模块ID集合
        midDel: modDels    //要删除的模块ID集合
};

    //
    var ajaxUrlPara = {
        action: "saveRoleModule",
        dbid: dbid,
        mdid: 12,
        mpvid: 1201,
        fpvid: 0,
        bid: 12,
        id: this.roleId,
        frid: -2
    };
    var loadM = new Nature.Data.Manager();

    //提交表单
    loadM.ajaxSaveData({
        url:this.url,
        formData: jsonValue,
        urlPara: ajaxUrlPara,
        title: "添加角色的模块",
        success: function (data) {
            //alert(msg);
            self.newOrderId = data.id;
            if (data.err.length == 0) {
                //alert("保存成功！");
                callback(self.newOrderId);
            } else {
                alert(data.err);
            }
        }
    });
    

};
 
//添加角色的按钮
RoleManage.prototype.saveRoleButtonData = function (btnInfos, callback) {

    var self = this;
    save(0);

    //递归提交表单
    function save(index) {

        if (index >= btnInfos.length) {
            callback();
            return;
        }

        var btnInfo = btnInfos[index];
        if (btnInfo.buttonIDs == "") btnInfo.buttonIDs = "0";
        
        //
        var ajaxUrlPara = {
            action: "saveRoleModuleButton",
            dbid: dbid,
            mdid: 12,
            mpvid: 1201,
            fpvid: 0,
            bid: 12,
            id: self.roleId,
            frid: btnInfo.moduleId,       //ModuleID
            frids: btnInfo.buttonIDs               //ButtonIDs
        };
        var loadM = new Nature.Data.Manager();

        //提交表单
        loadM.ajaxSaveData({
            url: self.url,
            formData: {},
            urlPara: ajaxUrlPara,
            title: "添加角色的模块",
            success: function(data) {
                //alert(msg);
                self.newOrderId = data.id;
                if (data.err.length == 0) {
                    //alert("保存成功！");
                    save(index + 1);
                    
                } else {
                    alert(data.err);
                }
            }
        });

    }
    
};
 