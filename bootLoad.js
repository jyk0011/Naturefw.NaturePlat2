/*

加载js脚本的一种解决方法。

by 金色海洋 2013-7-11 

2014-06-07 移植

*/
 
var Nature = {};/* 定义 一个很大的对象 */
Nature.Top = {};/* 加载js用的命名空间 */

/*数据库的设置*/
Nature.WebConfig = {
    isShowPeiZhi: true,         //页面里是否显示“配置”，程序员编辑时设置为 true。上线后设置为 false
    WebAppId: 1,
    dbid_236Write: "2,4",       //统一管理数据连接标识
    dbid_236WriteJM: "2,14",    //统一管理数据连接标识

    dbid_236SysLog: "2,6",       //wcf日志数据库

    dbid_Loan: "2,13",       //快易贷测试数据库
    //dbid_Loan: "2,12"        //快易贷正式数据库

};

/*ajax的设置*/
Nature.AjaxConfig = {
    Urlsso:         "http://LCNatureSSO.nature.com",    /*单点登录的网址*/
    UrlResource:    "http://LCNatureService.nature.com",  /*增删改查服务的网址*/
    Urljs:          "http://LCNatureResource.nature.com",  /*js文件的网址*/
    Urlcss:         "http://LCNatureResource.nature.com",          /*css文件的网址*/
    
    ajaxDataType: "json"                            /*跨域的时候用jsonp，不用跨域的话用json，cors跨域的话也是json*/
};

Nature.AjaxConfig.UrljsVer   = Nature.AjaxConfig.Urljs + "/Scripts/NatureAjax/ver.js",  /*存放js文件版本号的网址*/
Nature.AjaxConfig.UrlLoadJs  = Nature.AjaxConfig.Urljs + "/Scripts/NatureAjax/Nature.LoadJs.js", /*加载其他js文件的网址*/
Nature.AjaxConfig.UrlAdapter = Nature.AjaxConfig.Urljs + "/Scripts/NatureAjax/Nature.Adapter.js", /*适配的网址*/


/* 复用父页面里的js文件的时候使用。是否使用自己页的js。true：本页加载js文件。false：使用父页的js文件。*/
//Nature.isSelfJs = false;

Nature.Top.LoadCss = function(loadCss) {
    var cssUrl = Nature.AjaxConfig.Urlcss;
    loadCss.css(cssUrl + '/websiteStyle/mangoGlobal.css');
    loadCss.css(cssUrl + '/websiteStyle/mis-style-p.css');
    loadCss.css(cssUrl + '/websiteStyle/MisStyle_v2.css');
    loadCss.css(cssUrl + '/websiteStyle/debugCss.css');
    loadCss.css(Nature.AjaxConfig.UrlResource + '/Scripts/css/css2.css');
};

//外壳页的加载
Nature.Top.topLoad = function(loadscript, kind) {
    //判断有无配置信息————没有的话，加载且缓存      
    //判断有无js文件版本号——没有的话，加载且缓存  
    //加载Nature.LoadJs.js，开始加载其他js
    //判断页面是否有jsReady，如果有则开始调用

    /*加载css文件*/
    Nature.Top.LoadCss(loadscript);
    
    //加载js版本号
    
    var checkCount = 0;
    if (typeof Nature.jsVer == "undefined") {
        /*加载js文件的版本号，用于更新浏览器的js缓存文件。使用随机数作为参数，保证版本号是最新的。
        * 用随机数保证最新，因为可以缓存，所以不是每次访问都会去加载
        */
        loadscript.js(top.Nature.AjaxConfig.UrljsVer + '?rnd=' + Math.random(), function() { // 
            loadOtherJs();
        });
    } else {
        loadOtherJs();
    }

    //加载Nature.LoadJs
    function loadOtherJs() {
        /*得到了版本号，加载LoadJs.js，该文件加载其他需要的js文件*/
        loadscript.js(top.Nature.AjaxConfig.UrlLoadJs + Nature.jsVer, function () {
            var loadJs = new Nature.loadFile(document);
            loadJs.startLoadJs(kind, checkReady);
        });
    }

    /*检查页面是否有jsReady */
    function checkReady() {
        if (typeof jsReady == "function") {
            //执行页面里的函数
            jsReady();
        } else {
            //检查次数，尝试十次，超了就不玩了，避免死循环。*/
            if (checkCount < 10) {
                checkCount++;
                setTimeout(checkReady, 50);
            }
        }
    }

};

//子页的加载
Nature.Top.sonLoad = function(loadscript, kind, win) {
    var checkCount = 0;

    top.Nature.Top.LoadCss(loadscript);
    win.Nature = {};
    
    loadscript.js(top.Nature.AjaxConfig.UrlAdapter + top.Nature.jsVer, function () {
        win.Nature.Adapter(win);
        checkReady();
    });
   
    /*检查页面是否有jsReady */
    function checkReady() {
        if (typeof win.jsReady == "function") {
            //执行页面里的函数
            win.jsReady();
        } else {
            //检查次数，尝试十次，超了就不玩了，避免死循环。*/
            if (checkCount < 10) {
                checkCount++;
                setTimeout(checkReady, 50);
            }
        }
    }
};
  

/* 定义 配置信息 */
/*增删改查服务的网址*/
Nature.resourceUrl = "http://LCNatureResource.nature.com";
/*单点登录的网址*/
Nature.ssoUrl = "http://LCNatureSSO.nature.com";
/*css文件的网址*/
Nature.cssUrl = "http://LCNatureResource.nature.com";
/*跨域的时候用jsonp，不用跨域的话，设置json jsonp*/
Nature.sendDataType = "json";