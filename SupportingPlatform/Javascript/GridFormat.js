

/*列表的格式化的信息的管理*/
var GridFormat = function (dbid) {
    var self = this;
    var dataBaseId = dbid;
    var metaDataId = dbid.split(",")[1];
    var load = new Nature.Data.Cache();
    var metaInfo = {};

    this.init = function (callback) {
        $("#tr1083010 td[align='right']").css("width", "220px");

        getMetaInfo(function () {
            //绑定
            bind_lst_ModuleId();
            //设置下拉列表框的change事件
            listChange();
            //赋值
            self.setValue();
            //显示
            trShow();
        });

    };

    /*设置下拉列表框的change事件*/
    var listChange = function () {

        //绑定格式化(下拉列表框)
        dropDownList.lst_FormatKind.bind("change", trShow);

        //绑定列表视图ID(下拉列表框)
        dropDownList.lst_GridPageViewId.bind("change", loadGridColumn);

        //绑定查询视图ID(下拉列表框)
        dropDownList.lst_FindPageViewId.bind("change", loadFindColumn);

        $("input[value=' 生 成 ']").bind("click", generate);


        //模块的change。
        dropDownList.lst_ModuleId.bind("change", function () { //给联动的下拉列表绑定数据
            dropDownList.lst_GridPageViewId[0].options.length = 0;
            dropDownList.lst_FindPageViewId[0].options.length = 0;
            for (var obj in metaInfo.PageView) {
                if (metaInfo.PageView[obj].ModuleID == dropDownList.lst_ModuleId.val()) {
                    appendoption(obj, "1", dropDownList.lst_GridPageViewId);
                    appendoption(obj, "2", dropDownList.lst_FindPageViewId);
                }
            }
            loadFindColumn();
            loadGridColumn();
        });

        var appendoption = function (obj, n, obj2) {
            var pv = metaInfo.PageView[obj];
            if (pv.PVTypeID == defaultFenLeiId[n]) {
                obj2.append("<option value=\"" + pv.PVID + "\">" + pv.PVID + "_" + pv.PVTitle + "</option>");
            }
        };
        $("#divText input").blur(generate);
        $("#divText select").blur(generate);

    };



    /**/
    var defaultFenLeiId = {
        1: "701",//列表视图
        2: "702",//查询视图
        3: "703"//添加修改视图  注意: 701和703 暂时可以通用
    };

    /*给下拉列表框起个名*/
    var dropDownList = {
        //下拉列表框对象
        lst_FormatKind: $("#FrmCommonForm_ctrl_1083010"),
        lst_ModuleId: $("#FrmCommonForm_ctrl_1083080"),
        lst_GridPageViewId: $("#FrmCommonForm_ctrl_1083090"),
        lst_FindPageViewId: $("#FrmCommonForm_ctrl_1083100"),
        lst_GridColumId: $("#FrmCommonForm_ctrl_1083120"),
    };

    /*根据格式化类型，显示需要的属性*/
    var trShow = function () { //显示
        $("tbody tr").hide();
        $("#butTab").remove();//删除添加一行的table
        $("#tr1083010").show();

        var key = dropDownList.lst_FormatKind.val();

        var tmp = self.defaultValue[key];
        if (typeof tmp != "undefined") {
            for (var i = 0; i < tmp.length; i++) {
                $("#tr" + tmp[i]).show();
                if (tmp[i] == "1083020") {
                    creatTabText();
                }
            }
        }

    };



    /*根据元数据信息绑定模块*/
    var bind_lst_ModuleId = function () { //绑定模块ID(下拉列表框)
        dropDownList.lst_ModuleId[0].options.length = 0;
        for (var i = 0; i < metaInfo.Modulekeys.length; i++) {
            var moduleId = metaInfo.Modulekeys[i];
            var module = metaInfo.Module[moduleId];

            var quanJiaoKongGe = "";
            var quanJiaoKongGeLength = module.ParentIDAll.split(',').length;
            for (var k = 0; k < quanJiaoKongGeLength; k++) {
                quanJiaoKongGe += "　"; //拼接全角空格
            }
            dropDownList.lst_ModuleId.append("<option value=\"" + module.ModuleID + "\">" + module.ModuleID + quanJiaoKongGe + "" + module.ModuleName + "</option>");
        }

    };

    var generate = function () { //生成功能
        var key = dropDownList.lst_FormatKind.val();

        var strB = "{\"kind\":\"" + key + "\"";
        var strM = "";

        var ids = self.defaultValue[key];
        for (var i = 0; i < ids.length; i++) {
            var typeid = ids[i];
            var text = $("#FrmCommonForm_ctrl_" + typeid).val();
            if (text != "") {
                strM += ",\"" + self.formatInfo[typeid].jsonFlag + "\":\"" + text + "\"";
            }
        }
        $("#txtMeta").val(strB + strM.replace("\"[\"", "[\"").replace("\"]\"", "\"]").replace("\"{\"", "{\"").replace("\"}\"", "\"}") + "}");
    };

    /*拼写的方法 */
    var pinXieChaXun = function () {
        var inputtextChildren;
        var TihuanChaXun = "[";
        var str_mid = "";
        $("input[type='checkbox']").each(function () {
            inputtextChildren = $("input[class='cssTxt inputT01'][name='" + $(this).val() + "']");
            if (inputtextChildren.val() != null) {
                str_mid = "\"c" + $(this).val() + "," + inputtextChildren.val() + "\",";
            } else {
                str_mid = "\"c" + $(this).val() + "\",";
            }
            if (this.checked == true) {
                TihuanChaXun += str_mid;
            }
        });
        TihuanChaXun += "]";

        $("#FrmCommonForm_ctrl_1083130").val(guolvstr(TihuanChaXun));
    };

    /*过滤字符串 */
    var guolvstr = function (str) {
        if (str.indexOf(",\",]") > -1) {
            str = str.replace(",\",]", "\"]");
        }
        if (str.indexOf(",\",\"") > -1) {
            str = str.replace(",\",\"", "\", \"");
        }
        if (str.indexOf("\",]") > -1) {
            str = str.replace("\",]", "\"]");
        }
        if (str == "[]") {
            str = "";
        }
        return str;
    };


    /*获取元数据信息*/
    var getMetaInfo = function (callback) {
        var cacheKey = "metaForLog_" + dbid;

        if (typeof top.__cache[cacheKey] != "undefined") {
            metaInfo = top.__cache[cacheKey];
            callback();
        } else {
            load.ajaxGetMeta({
                urlPara: { action: "datachange", mdid: 0, mpvid: -3, bid: -3, dbid: metaDataId, cacheKey: "metaForLog_" + metaDataId },
                title: "模块、视图、按钮等的名称",
                success: function (info) {
                    metaInfo = info;
                    callback();
                }
            });

        }
    };

    //加载查询的元数据
    var loadFindColumn = function () {

        //到父页面的缓存里查找，是否有缓存。有缓存直接用。
        load.ajaxGetMeta({
            urlPara: {
                action: "find",
                mdid: dropDownList.lst_ModuleId.val(),                         //模块ID
                mpvid: dropDownList.lst_GridPageViewId.val(),        //列表视图ID
                fpvid: dropDownList.lst_FindPageViewId.val(),              //查询视图ID
                dbid: metaDataId,                                      //外面的参数
                cacheKey: dropDownList.lst_FindPageViewId.val()        //查询视图ID
            },
            title: "查询控件元数据",
            success: function (info) {
                if (info != null) { //给下拉列表框(对应的字段ID)绑定数据 

                    /*创建table,给里面的属性赋值*/
                    creatTable(info);

                    //加初始设置,根据原有信息,设置是否选中
                    settable();
                    //加click事件
                    $("input[type='checkbox']").bind("click", function () {
                        var inputtext;
                        $(this).each(function () {
                            inputtext = $("input[class='cssTxt inputT01'][name='" + $(this).val() + "']");
                            if (this.checked == true) {
                                inputtext.attr("readOnly", false);
                            } else {
                                inputtext.attr("readOnly", true);
                                inputtext.val("");
                            };
                        });
                        pinXieChaXun();
                    });

                    //文本框的keyup
                    $("#CxTab input[type='text']").bind("keyup", function () {
                        pinXieChaXun();
                    });

                }
            }
        });
    };


    /*创建table,给其内部属性赋值和添加事件*/
    var creatTable = function (info) {
        var CxTab = $("#CxTab");
        if (CxTab != null) {
            $(CxTab).remove();
        }

        var newTab = $("<table id=\"CxTab\"></table>");
        var newTr = "";
        var newTd = "";
        for (var obj in info.controlInfo) {
            newTr = $("<tr></tr>");
            newTd = "";
            newTd += "<td>c" + info.controlInfo["" + obj + ""].ColumnID + "</td><td>" + info.controlInfo["" + obj + ""].ColName + "</td><td>"
                + info.controlInfo["" + obj + ""].Ser_FindKindID + "</td><td><input type=\"checkbox\"  value=\"" + info.controlInfo["" + obj + ""].ColumnID +
                "\" />选择</td><td><input type=\"text\" class=\"cssTxt inputT01\" name=\"" + info.controlInfo["" + obj + ""].ColumnID + "\"></td>";
            $(newTr).append(newTd);
            $(newTab).append(newTr);
        }
        if (dropDownList.lst_FormatKind.val() == "toTxtByData") {
            $("div[style='']").append(newTab);
        }
    };


    /*设置table的默认值*/
    var settable = function () {
        $("input[type='checkbox']").each(function () {//
            $("input[class='cssTxt inputT01'][name='" + $(this).val() + "']").attr("readOnly", true);
        });
        var str_array = $("#FrmCommonForm_ctrl_1083130").val();
        if (str_array != "") {
            str_array = str_array.replace("[\"", "");
            str_array = str_array.replace("\"]", "");

            if (str_array.indexOf("\",\"") > -1) {
                var split_array = str_array.split("\",\"");
                var str_01 = split_array[i].split(',')[0].replace("c", "");
                var str_02 = split_array[i].replace("c", "");

                for (var i = 0; i < split_array.length; i++) {
                    if (split_array[i].indexOf(',') > -1) {
                        $("input[type='checkbox'][value='" + str_01 + "']")[0].checked = true;//执行checkbox的cilck事件让其选中,文本框只读
                        $("input[type='text'][name='" + str_01 + "']").val(split_array[i].split(',')[1]);//将值付给文本框
                        $("input[type='text'][name='" + str_01 + "']").attr("readOnly", false);

                    } else {
                        $("input[type='checkbox'][value='" + str_02 + "']")[0].checked = true;//执行checkbox的cilck事件让其选中,文本框只读(有多个选项的时候)
                        $("input[type='text'][name='" + str_02 + "']").attr("readOnly", false);
                    }
                }
            } else {
                var str_03 = str_array.replace("c", "");
                $("input[type='checkbox'][value='" + str_03 + "']")[0].checked = true;//执行checkbox的cilck事件让其选中,文本框只读(只有一个选项的时候)
                $("input[type='text'][name='" + str_03 + "']").attr("readOnly", false);
            }
        } 
        //else {

        //}
    };


    var setoption = function (obj, value) {//设置下拉列表选中项
        $(obj).val(value);
    };


    //加载列表的元数据
    var loadGridColumn = function () {
        //到父页面的缓存里查找，是否有缓存。有缓存直接用。

        load.ajaxGetMeta({
            urlPara: {
                action: "grid",
                mdid: dropDownList.lst_ModuleId.val(),                       //模块ID
                mpvid: dropDownList.lst_GridPageViewId.val(),      //列表视图ID
                dbid: metaDataId,  //外面的参数
                cacheKey: dropDownList.lst_GridPageViewId.val() //列表视图ID
            },
            title: "数据列表元数据",
            success: function (info) {
                if (info != null) {
                    dropDownList.lst_GridColumId.html("");
                    for (var obj in info.data) { //下拉列表框(对应的字段ID)绑定数据
                        dropDownList.lst_GridColumId.append("<option value=\"" + info.data[obj].ColumnID + "\">" + info.data[obj].ColumnID + "_" + info.data[obj].ColName + "</option>");
                    }
                    if (self.formatInfo[1083120].modValue != undefined) {
                        setoption(dropDownList.lst_GridColumId, self.formatInfo[1083120].modValue);
                    }
                }
            }
        });
    };


    /*创建item对应的键值对文本框*/
    var creatTabText = function () {
        var Itemval = $("#FrmCommonForm_ctrl_1083020").val();//item选项加载时,获取其文本
        if (Itemval != null && Itemval != "") {//对文本进行拆解
            Itemval = Itemval.substring(1, Itemval.length - 1);
            Itemval = Itemval.split(',');
        }

        if ($("#ItemTab") != null) {
            $("#ItemTab").remove();
        }
        
        
        var tabItem = $("<table id=\"ItemTab\"></table>");//创建一个table
        if (Itemval.length > 0) {//当文本有值时 循环创建"tr",并给其赋值
            for (var i = 0; i < Itemval.length; i++) {
                var Tr_new = returnTr(Itemval[i]);
                tabItem = tabItem.append(Tr_new);
            }
        } else {//文本时空的时候,创建一行"tr"
            var Tr_new = returnTr();
            tabItem = tabItem.append(Tr_new);
        }
        var bt_add_Table = $("<input type=\"button\" id=\"addbut\" class=\"input_01\" value=\"添加一行\" name=\"ibutton\">"); //创建一个添加按钮
        bindTianJiaYiHang(bt_add_Table);//给添加按钮绑定事件
        bt_add_Table = $("<table id=\"butTab\"><tr><td></td></tr></table>").append(bt_add_Table); //给添加按钮套一层table
        $("div[style='']").append(bt_add_Table).append(tabItem);//将生成的子元素,添加到div中

    };
    var returnTr = function (info) {
        /*对字符串进行拆分获取key和value*/
        var key = "";
        var value = "";
        if (info != null && info != "") {
            var split = info.split(':');
            if (split.length > 0) {
                key = split[0].substring(1, split[0].length - 1);
                value = split[1].substring(1, split[1].length - 1);
            }
        }
        var Tr_new1 = $("<tr></tr>");   //创建tr
        var Td_new1 = $("<td></td>");//创建td
        var Td_new2 = $("<td></td>");//创建td
        var Td_new3 = $("<td></td>");//创建td
        var ip_key_new = $("<input type=\"text\" class=\"cssTxt inputT01\" name=\"ikey\"  value=\"" + key + "\">");
        var ip_value_new = $("<input type=\"text\" class=\"cssTxt inputT01\" name=\"ivalue\"  value=\"" + value + "\">");
        bindTextItem(ip_key_new);//绑定key文本框
        bindTextItem(ip_value_new);//绑定value文本框
        var bt_del_new = $("<input type=\"button\" class=\"input_01\" value=\"删除\" name=\"ibutton\">");
        bindShanChu(bt_del_new);//删除按钮绑定事件
        Tr_new1 = Tr_new1.append(Td_new1.append("key:").append(ip_key_new)).append(Td_new2.append("value:").append(ip_value_new)).append(Td_new3.append(bt_del_new));//将元素添加到tr中
        return Tr_new1;

    };

    var bindTextItem = function (obj) {
        obj.bind("keyup", function () {
            var finalValue = "{";
            $("#ItemTab tr").each(function () {//遍历含有key和value的文本框的table
                var ikey = $(this).children("td").children("input[name='ikey']").val();
                var ivalue = $(this).children("td").children("input[name='ivalue']").val();
                if (ikey != "" && ivalue != "") {
                    finalValue += "\"" + ikey + "\":\"" + ivalue + "\",";
                }
            });
            finalValue += "}";
            finalValue = finalValue.replace(",}", "}");//过滤字符串
            finalValue = finalValue.replace("{}", "");
            $("#FrmCommonForm_ctrl_1083020").val(finalValue);//设置"选项"的文本
        });
    };

    var bindShanChu = function (obj) {//给删除按钮绑定
        obj.bind("click", function () {
            if ($("#ItemTab tr input[type='button']").length > 1) {//判断 当前的行数,只有在行数大于1的情况下才可以执行删除功能
                var fatTr = $(this).parent().parent().remove();//获取删除按钮所在行
                var inpkey = fatTr.children("td").children("input[name='ikey']").val();
                var inpvalue = fatTr.children("td").children("input[name='ivalue']").val();
                if (inpkey != null && inpvalue != null) {
                    var replaceStr = "\"" + inpkey + "\":" + "\"" + inpvalue + "\"";
                    var valItem = $("#FrmCommonForm_ctrl_1083020").val();
                    valItem = valItem.replace(replaceStr, "");
                    valItem = valItem.replace(",}", "}");//过滤字符串
                    valItem = valItem.replace("{,\"", "{\"");
                    valItem = valItem.replace("{}", "");
                    valItem = valItem.replace("\",,\"", "\",\"");
                    
                    
                    $("#FrmCommonForm_ctrl_1083020").val(valItem);//设置"选项"的文本
                }
                fatTr.parent().parent().remove();//删除行
            }
        });
    };

    var bindTianJiaYiHang = function (obj) {//绑定添加按钮
        obj.bind("click", function () {
            var newTr = returnTr();//.clone(true);//克隆一行
            newTr.find("input[type='text']").val("");//清空克隆的文本框的文本内容
            $("#ItemTab").append(newTr); //添加行

        });
    };


};
