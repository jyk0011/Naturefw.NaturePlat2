/*

加载js脚本的一种解决方法。

by 金色海洋 2013-7-11 

*/

//1毫秒后开始加载 js文件 
window.setTimeout(function() {

    //判断有无配置信息————没有的话，加载且缓存
    //判断有无js文件版本号——没有的话，加载且缓存
    //加载Nature.LoadJs.js，开始加载其他js
    //判断页面是否有jsReady，如果有则开始调用

    //var bootScript = $("script").frist(); //获取标签
    //var pageKind = bootScript.attr("pageKind"); //获取标签里的属性

    var kind = (typeof pageKind == "string") ? pageKind : "index"; /*默认的网页类型*/

    var loads = new loadscript(document);       //把本页面作为参数传递进去。因为涉及到复用，所以要new一下。

    if (typeof top.Nature == "undefined") {
        //没有配置信息，加载。用y_MM_dd_HH作为版本标志，一个小时更新一次。因为可以缓存配置信息，所以不是每次都让浏览器加载
        var date = new Date();
        var dateVer = date.getYear() + '_' + date.getMonth() + '_' + date.getDate() + '_' + date.getHours();
        
        //加载第一个主程序。分开的目的是为了可以控制更新。
        loads.js('/bootLoad.js?rnd=' + dateVer, function () {
            Nature.Top.topLoad(loads, kind);   //走top页面的流程
        });
    } else {
        //走子页面的流程，把子页面（window）作为参数传递进去，以便于和top页面区分。因为函数是放在top页面里的。
        //sonLoad不在子页面里，而是存在于top里，所以要 top.sonLoad
        top.Nature.Top.sonLoad(loads, kind, window);
    }


    /*实现动态加载js的函数，来自于互联网，做了一点修改，可以兼容IE10。IE11没测试。
    * 写在函数里面，不污染window了。
    * 增加加载css的函数
    * 原来是静态的，现在改成需要实例化的。
    */
    function loadscript(doc) {
        this.js = function(url, callback) {
            var s = doc.createElement('script');
            s.type = "text/javascript";
            s.src = url;
            s.expires = 1;
            load(s, callback);
        };
        this.css = function (url, callback) {
            var l = doc.createElement('link');
            l.type = "text/css";
            l.rel = "stylesheet";
            l.media = "screen";
            l.href = url;
            //doc.getElementsByTagName('head')[0].appendChild(l);
            load(l, callback);
        };

        function load(s, callback) {
            switch (doc.documentMode) {
            case 9:
            case 10:
            case 11:
                s.onerror = s.onload = loaded;
                break;
            default:
                s.onreadystatechange = ready;
                s.onerror = s.onload = loaded;
                break;
            }
            doc.getElementsByTagName('head')[0].appendChild(s);

            function ready() { /*IE7.0/IE10.0*/
                if (s.readyState == 'loaded' || s.readyState == 'complete') {
                    if (typeof callback == "function") callback();
                }
            }

            function loaded() { /*chrome/IE10.0*/
                if (typeof callback == "function") callback();
            }
        }
    }

}, 1);
