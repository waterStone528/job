﻿
function DetectBrowser() {
    var userAgent = navigator.userAgent.toLowerCase();
    //qq
    if (userAgent.indexOf(" qqbrowser") > -1) {
        return "qq";
    }
    //手机qq
    if (userAgent.indexOf("mqqbrowser") > -1) {
        return "手机qq";
    }
    //ie
    if (userAgent.indexOf("trident") > -1) {
        if (userAgent.indexOf("msie 6.0") > -1) {
            return "ie6";
        }
        //包括360（4）
        if (userAgent.indexOf("msie 7.0") > -1) {
            return "ie7";
        }
        if (userAgent.indexOf("msie 8.0") > -1) {
            return "ie8";
        }
        if (userAgent.indexOf("msie 9.0") > -1) {
            return "ie9";
        }
        if (userAgent.indexOf("msie 10.0") > -1) {
            return "ie10";
        }
            //包括360（5），ie模式下的360（6）
        else {
            return "ie11";
        }
    }
    //firefox
    if (userAgent.indexOf("firefox") > -1) {
        return "firefox";
    }
    //搜狗
    if (userAgent.indexOf("metasr") > -1) {
        return "搜狗";
    }
    //猎豹
    if (userAgent.indexOf("lbbrowser") > -1) {
        return "猎豹";
    }
    //遨游
    if (userAgent.indexOf("maxthon") > -1) {
        return "遨游";
    }
    //世界之窗
    if (userAgent.indexOf("theworld") > -1) {
        return "世界之窗";
    }
    //uc   
    if (userAgent.indexOf(" ubrowser/") > -1) {
        return "uc";
    }
    //手机uc
    if (userAgent.indexOf("ucbrowser") > -1) {
        return "手机uc";
    }
    //百度 
    if (userAgent.indexOf("bidubrowser") > -1) {
        return "百度";
    }

    if (userAgent.indexOf(" opr/") > -1) {
        return "Opera";
    }

    if (userAgent.indexOf("chrome") > -1) {
        //360
        if (navigator.mimeTypes["application/x-shockwave-flash"].description.toLowerCase().indexOf("adobe") > -1) {
            return "360";
        }
            //chrome
        else {
            return "chrome";
        }
    }
        //other
    else {
        return "other";
    }
}

function DetectOS() {
    var userAgent = navigator.userAgent.toLowerCase();

    //windows
    if ((navigator.platform == "Win32") || (navigator.platform == "Win64") || (navigator.platform == "Windows")) {
        if (userAgent.indexOf("windows nt 5.0") > -1 || userAgent.indexOf("windows 2000") > -1) {
            return "Win2000";
        }
        if (userAgent.indexOf("windows nt 5.1") > -1 || userAgent.indexOf("windows xp") > -1) {
            return "WinXP";
        }
        if (userAgent.indexOf("windows nt 5.2") > -1 || userAgent.indexOf("windows 2003") > -1) {
            return "Win2003";
        }
        if (userAgent.indexOf("windows nt 6.0") > -1 || userAgent.indexOf("windows vista") > -1) {
            return "WinVista";
        }
        if (userAgent.indexOf("windows nt 6.1") > -1 || userAgent.indexOf("windows 7") > -1) {
            return "Win7";
        }
        if (userAgent.indexOf("windows nt 6.2") > -1 || userAgent.indexOf("windows nt 6.3") > -1 || userAgent.indexOf("windows 8") > -1) {
            return "Win8";
        }
    }
    //iphone
    if (userAgent.indexOf("iphone") > -1) {
        return "iphone";
    }
    //android
    if (userAgent.indexOf("android") > -1) {
        return "android";
    }
    //mobile phone
    if (userAgent.indexOf("windows phone") > -1) {
        return "windows phone";
    }
    //mac
    if ((navigator.platform == "Mac68K") || (navigator.platform == "MacPPC") || (navigator.platform == "Macintosh") || (navigator.platform == "MacIntel")) {
        return "Mac";
    }
    //unix
    if ((navigator.platform == "X11")) {
        return "Unix";
    }
    //linux
    if ((String(navigator.platform).indexOf("Linux") > -1)) {
        return "Linux";
    }

    return "other";
}
var Browser = DetectBrowser();
var OS = DetectOS();
var Pass = false;
if (Browser.indexOf("ie") > -1 && Browser.substr(2) >= 8) { Pass = true; }
if (Pass == false) { location.href = "BCK.html"; }