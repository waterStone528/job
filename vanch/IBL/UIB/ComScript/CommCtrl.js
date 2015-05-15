// JavaScript source code
var SysUIVer = "2.10." + Math.random();
function bodyresize() {
    ReCalcItemSize();
    bodyResizeScript();
}
function bodyLoad() {
    ReCalcItemSize();
    bodyLoadScript();
    //setInterval("AddItem()", 1000);
}
var LastItem = 1;//0,have remain items not be loaded, 1-no remain items,all has loaded.

var ItemWidthMin = parseFloat(document.styleSheets[0].rules[0].style.width);//this is important value, adujst carefull. min 360 max 520 for reference.
var ItemWidthMax = parseFloat(document.styleSheets[0].rules[1].style.width);
function AdjItemWidth(ItemWidth) {
    var padding = 0;
    var border = 0;
    padding = parseFloat(document.styleSheets[0].rules[0].style.padding.replace("px", ""));
    border = parseFloat(document.styleSheets[0].rules[0].style.borderWidth.replace("px", ""));
    var MarginR = parseFloat(document.styleSheets[0].rules[0].style.marginRight.replace("px", ""));
    var MarginL = parseFloat(document.styleSheets[0].rules[0].style.marginLeft.replace("px", ""));
    if (isNaN(MarginR)) { MarginR = 0; }
    if (isNaN(MarginL)) { MarginL = 0; }
    var MarginRL = MarginR + MarginL;
    if (isNaN(padding)) { padding = 0; }
    if (isNaN(border)) { border = 0; }
    document.styleSheets[0].rules[0].style.width = ItemWidth - ((padding + border) * 2) - MarginRL + "px"; //较正padding border margin导致的宽度偏移
}

function ReCalcItemSize() {
    if (document.querySelectorAll(".ItemSize").length == 0) { return; }
    var CalcErrValue = 1;//just for correcting calculate fault
    var ContentWidth = document.querySelectorAll(".ItemSize")[0].parentElement.clientWidth - CalcErrValue;
    ItemCnt = parseInt(ContentWidth / ItemWidthMin);
    ItemWidthCur = ContentWidth / ItemCnt;
    if (ItemWidthCur > ItemWidthMax) { ItemWidthCur = ItemWidthMax; }
    AdjItemWidth(ItemWidthCur);
}

var RemItemsLoading = false;
var ScrollBarAppreared = false;
function ScrollTracker(Obj, CallBack) {
    var PreLoadLength = 0;
    if (Obj.scrollHeight - Obj.scrollTop <= Obj.clientHeight + PreLoadLength) {
        if (LastItem == 0 && RemItemsLoading == false) {
            LoadingBoxCtl(1);
            eval(CallBack);
            //continue load items
        }
    }
}

//数据加载
function LoadingBoxCtl(Act) {
    var loadingBOX = document.querySelector('.LoadingBox');
    var loadingBOXDisappearIns = null;
    clearTimeout(loadingBOXDisappearIns);
    loadingBOX.setAttribute('data-status', '1')//加载中...

    if (Act == 0) {//加载中消失
        loadingBOX.setAttribute('data-status', '01');
        loadingBOXDisappearIns = setTimeout("document.querySelector('.LoadingBox').setAttribute('data-status', '02');", 500);

    }
    if (Act == 2) {//加载失败，请重试...
        loadingBOX.setAttribute('data-status', '2')
        loadingBOX.focus();
        loadingBOX.setAttribute('data-status', '2a');
    }
}

function OpenWin(Url) {
    window.open(Url, '_blank', 'menubar=no,toolbar=no,location=no,directories=no,scrollbars=yes,status=yes,resizable=yes,height=785,width=1200')
}

function LoadMod(ModuleName, CallBack, ModFloder) {
    OutTrackInfor(1, ModuleName, "Load Start");
    if (!CallBack) { CallBack = ""; }; if (ModFloder == undefined) { ModFloder = ""; } else { ModFloder = ModFloder + "/"; }
    var OldModi = MainZone.querySelector("#Modi"); if (OldModi) { OldModi.parentNode.removeChild(OldModi); };
    var Modi = document.createElement("iframe");
    Modi.style.cssText = "height:0px;width:0px;display:none;";
    //Modi.setAttribute("onload", "try {MainZone.innerHTML=this.contentDocument.body.innerHTML} catch (e) {OutTrackInfor(1, '" + ModuleName + "', 'Not Found');};ReCalcItemSize();" + CallBack);
    Modi.setAttribute("onload", "try {MainZone.innerHTML=this.contentDocument.body.innerHTML} catch (e) {OutTrackInfor(1, '" + ModuleName + "', 'Not Found');};ReCalcItemSize();LoadJS(\"" + ModuleName + "\", \"" + ModuleName + ".Init();\", \"../Modules/" + ModFloder + "\");" + CallBack);
    Modi.id = "Modi"; Modi.src = "Modules/" + ModFloder + ModuleName + ".html?v=" + SysUIVer; MainZone.appendChild(Modi);
}

function OutTrackInfor(TypeId, name, status) {
    switch (TypeId) {
        case 1:
            document.querySelector(".MonitorInfor").innerHTML = "Load Module:" + name + " " + status + "<br>" + document.querySelector(".MonitorInfor").innerHTML;
            break;
        case 2:
            document.querySelector(".MonitorInfor").innerHTML = "Load Control Module:" + name + " " + status + "<br>" + document.querySelector(".MonitorInfor").innerHTML;
            break;
        case 3:
            document.querySelector(".MonitorInfor").innerHTML = "Load Template:" + name + " " + status + "<br>" + document.querySelector(".MonitorInfor").innerHTML;
            break;
        case 4:
            document.querySelector(".MonitorInfor").innerHTML = "Clear MainZone: Completed" + "<br>" + document.querySelector(".MonitorInfor").innerHTML;
            break;
        default:
            break;
    }
}

function LoadJS(JSModName, CallBack, ModFloder) {
    if (!ModFloder) { ModFloder = ""; } else { ModFloder = ModFloder + "/"; }
    if (!CallBack) { CallBack = ""; } else { CallBack = CallBack + ";"; }
    var JSElement = document.createElement("script"); JSElement.setAttribute("type", "text/javascript"); JSElement.src = "ComScript/" + ModFloder + JSModName + ".js?v=" + SysUIVer; JSElement.setAttribute("onload", CallBack + "document.body.removeChild(this);"); document.body.appendChild(JSElement);
    OutTrackInfor(2, JSModName, "Completed");
}

function FloatInp(Obj, Length) {
    ///<summary>输入框允许小数的最大位数</summary><param name="Obj">输入框</param><param name="Length">最大的位数</param>
    if (Obj.value == "") { return; }
    if (Length == 0) { Obj.value = parseInt(parseFloat(Obj.value)); }
    else if (Obj.value.indexOf(".") >= 0) { if (Obj.value.split(".")[1].length > Length) { Obj.value = (parseInt(parseFloat(Obj.value) * Math.pow(10, Length)) / Math.pow(10, Length)).toFixed(Length); } }
}

function LoadJSM(jsname, CallBack, ModFloder) {
    if (!CallBack && CallBack.length < 2) { CallBack = ""; } else { CallBack = CallBack + ";"; }
    if (!ModFloder) { ModFloder = ""; } else { ModFloder = ModFloder + "/"; }
    var sr = document.createElement("script");
    sr.type = "text/javascript";
    sr.src = "modules/" + ModFloder + jsname + ".js?v=" + SysUIVer;
    sr.setAttribute("onload", CallBack + "document.body.removeChild(this);");
    document.body.appendChild(sr);
}

function LoadTP(TemplateName, CallBack, ModFloder) {
    if (document.querySelector("#" + TemplateName + "i")) { if (CallBack.length > 1) { eval(CallBack); }; return; }
    if (!CallBack) { CallBack = ""; }
    if (!ModFloder) { ModFloder = ""; } else { ModFloder = ModFloder + "/"; }
    var Div = document.createElement("div");
    Div.setAttribute("style", "height:0px;width:0px;display:none;");
    Div.setAttribute("id", TemplateName + "i");
    Div.innerHTML = "<iframe onload=\"this.parentElement.innerHTML=this.contentDocument.body.innerHTML;" + CallBack + ";\" style=\"height:0px;width:0px;visibility:collapse;display:none;\" src=\"Modules/" + ModFloder + TemplateName + ".html?v=" + SysUIVer + "\"></iframe>";
    document.body.appendChild(Div);
    OutTrackInfor(3, TemplateName, "Completed");
}

function BtStatus(Obj, SetSt) {
    ///<summary>按钮样式控制</summary>
    ///<param name="BtStatus">R:just return value;E:To be normal;D:To be disabled;S:To be selected;H:To Hidden;U:to Un Hidden</param>
    if (Obj == null) { return; };
    var stattname = "data-btst";
    if (SetSt == "R") { return Obj.getAttribute(stattname); };
    if (BtStatus(Obj, "R") == "H" && SetSt != "U") { return; }
    if (SetSt == "U") { SetSt = "E"; };
    Obj.setAttribute(stattname, SetSt);
}

function AddFavorite() {
    if (document.documentElement) { window.external.addFavorite('http://www.vanch.com', '投资理财'); }
    else if (window.sidebar) { window.sidebar.addPanel('凡奇P2P借贷', 'http://www.vanch.com', ""); }
}

//Date extend function===================================================
if (!Date.minValue) Date.minValue = new Date("1970/01/01 00:00:00");
if (!Date.maxValue) Date.maxValue = new Date("2099/12/31 23:59:59");
Date.prototype.isLeapYear = function () { return (0 == this.getYear() % 4 && ((this.getYear() % 100 != 0) || (this.getYear() % 400 == 0))); };
Date.prototype.DayInYear = function () { return this.isLeapYear ? 366 : 365; };
Date.prototype.DayInMonth = function () {
    var d = this.getTrueMonth(); if (d == 2) { if (this.isLeapYear) return 29; else return 28; }
    else { if (d in { 1: 1, 3: 3, 5: 5, 7: 7, 8: 8, 10: 10, 12: 12 }) { return 31; } else { return 30; } }
};

//time add
Date.prototype.getTrueMonth = function () { return this.getMonth() + 1; }
Date.prototype.addYear = function (num) { if (!isNaN(num)) var d = new Date(this); d.setFullYear(this.getFullYear() + parseInt(num)); return d; }
Date.prototype.addMonth = function (num) { if (!isNaN(num)) var d = new Date(this); d.setMonth(this.getMonth() + parseInt(num)); return d; }
Date.prototype.addDay = function (num) { if (!isNaN(num)) var d = new Date(this); d.setDate(this.getDate() + parseInt(num)); return d; }
Date.prototype.addHour = function (num) { if (!isNaN(num)) var d = new Date(this); d.setHours(this.getHours() + parseInt(num)); return d; }
Date.prototype.addMinute = function (num) { if (!isNaN(num)) var d = new Date(this); d.setMinutes(this.getMinutes() + parseInt(num)); return d; }
Date.prototype.addSecond = function (num) { if (!isNaN(num)) var d = new Date(this); d.setSeconds(this.getSeconds() + parseInt(num)); return d; }
Date.prototype.addMS = function (num) { if (!isNaN(num)) var d = new Date(this); d.setMilliseconds(this.getMilliseconds() + parseInt(num)); return d; }
Date.prototype.addWeek = function (num) { if (!isNaN(num)) var d = new Date(this); d.setDate(this.getDate() + parseInt(num) * 7); return d; }
Date.prototype.toYMD = function () { return this.getFullYear() + "/" + this.getTrueMonth() + "/" + this.getDate() };
Date.prototype.toYM = function () { return this.getFullYear() + "/" + this.getTrueMonth() };
//Date extend function End================================================

Number.prototype.ToNum = String.prototype.ToNum = function (Method, Leng) {
    if (Method == 0) { return parseFloat(this).toFixed(Leng); }
    if (Method == 1) { return (parseInt(parseFloat(this) * Math.pow(10, Leng)) / Math.pow(10, Leng)).toFixed(Leng); }
    if (Method == 2) { var data = parseInt(parseFloat(this) * Math.pow(10, Leng + 1)); var dataDt = data.toString(); data = parseInt(data / 10) * 10; dataDt = dataDt.substr(dataDt.length - 1, 1); dataDt = parseInt(dataDt); if (dataDt > 0) { data = data + 10; }; return (data / Math.pow(10, Leng + 1)).toFixed(Leng); }
    if (Method == 3) { var data = parseInt(parseFloat(this) * Math.pow(10, Leng + 1)); if (parseFloat(this) * Math.pow(10, Leng + 1) > parseInt(parseFloat(this) * Math.pow(10, Leng + 1))) { data = data + 10; }; return (data / Math.pow(10, Leng + 1)).toFixed(Leng); }
};

function _Format_(Value, Obj) {
    //Obj = Obj || document.createElement("div");
    var WrongFMT = "格式代码错";
    if (Obj.hasAttribute("data-FMT")) { } else { Obj.setAttribute("data-FMT", Obj.innerText); }; var DataFMT = Obj.getAttribute("data-FMT");
    switch (DataFMT.substr(0, 1)) {
        case "D":
            if (Value.length == 10) { Obj.innerText = Value; } else { Obj.innerText = WrongFMT; }; break;
        case "N":
            var Method = parseInt(DataFMT.substr(3, 1)); var len = parseInt(DataFMT.substr(1, 1)); Obj.innerText = Value.ToNum(Method, len); break;
        case "X":
            Obj.innerText = Value; break;
        case "P":
            var Method = parseInt(DataFMT.substr(3, 1)); var len = parseInt(DataFMT.substr(1, 1)); Obj.innerText = (Value * 100).ToNum(Method, len) + "%"; break;
        case "Q":
            var Method = parseInt(DataFMT.substr(3, 1)); var len = parseInt(DataFMT.substr(1, 1)); Obj.innerText = (Value * 1000).ToNum(Method, len) + "‰"; break;
        default:
            alert("程序错误\r\n \r\n动作：尝试在未格化的目标上进行格式化操作,\r\n格式化代码为：" + DataFMT + "\r\n控件ID：" + Obj.id + "\r\n样式名：" + Obj.className + "\r\n当前内容：" + Obj.innerText); break;
    }
    Obj.title = "格式代码:" + DataFMT + "  原值:" + Value;// 此为UI检视时使用，正式环境需删除此行。
};

Object.defineProperty(HTMLTableCellElement.prototype, "DspV", { set: function (val) { _Format_(val, this); }, get: function () { } });
Object.defineProperty(HTMLLabelElement.prototype, "DspV", { set: function (val) { _Format_(val, this); }, get: function () { } });
Object.defineProperty(HTMLSpanElement.prototype, "DspV", { set: function (val) { _Format_(val, this); }, get: function () { } });

var CEC = {//Comm Element Control Script
    SlideSwitch: function (Sender, xeon, xeoff) {
        if (Sender.getAttribute('data-sw') == '1') { Sender.setAttribute('data-sw', 0); if (xeoff != undefined) { eval(xeoff); } }
        else { Sender.setAttribute('data-sw', 1); if (xeon != undefined) { eval(xeon); } }
    }
};

//SD start
function AJAXC() {
    this.xmlhttp = null;
    this.url = "/FBF/Control.ashx";
    this.data = null;
    this.type = "post";
    this.async = true;
    this.timeout = 0;  //0：无超时判断
    this.success = function (res) { };
    this.defaultError = function (xmlhttpObj) {
        //alert("ajax error!\nreadyState:" + xmlhttpObj.readyState + "\nstatus:" + xmlhttpObj.status + "\nstatusText:" + xmlhttpObj.statustext);
    };
    this.ontimeout = function () {
        alert("请求超时");
    };
    this.error = this.defaultError;
    this.onReadystate = function () {
        if (this.xmlhttp.readyState == 4) {
            //ie8超时会报错
            try {
                //ie10,ie11超时
                if (this.xmlhttp.status == 0) { return; }
                if (this.xmlhttp.status == 200) {
                    this.success(this.xmlhttp.responseText);
                }
                else {
                    this.error(this.xmlhttp);
                }
            }
            catch (err) {
                alert("error!\nreadyState:" + this.xmlhttp.readyState + "\nstatus:" + this.xmlhttp.status + "\nstatusText:" + this.xmlhttp.statustext + "\nerror:" + err);
            }
        }
    };
    this.start = function () {
        this.xmlhttp = new XMLHttpRequest();
        if (this.xmlhttp != null) {
            try {
                var loader = this;
                this.xmlhttp.onreadystatechange = function () {
                    loader.onReadystate.call(loader);
                }
                this.xmlhttp.ontimeout = function () {
                    loader.ontimeout();
                }
                this.xmlhttp.open(this.type, this.url, this.async);
                this.xmlhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                this.xmlhttp.timeout = this.timeout;
                this.xmlhttp.send(this.data);
            }
            catch (err) {
                this.error(this.xmlhttp);
            }
        }
        else {
            alert("请使用IE8及IE8以上版本浏览器");
            return;
        }
    };
}

function DateObjFormate(datatime) {
    var year = datatime.getFullYear();
    var month = datatime.getMonth() + 1;
    var day = datatime.getDate();

    return (year + "/" + (month < 10 ? "0" + month : month) + "/" + (day < 10 ? "0" + day : day));
}

//从后台通过ajax传过来的时间字符串
function BdDateStrFormate(jsonObjDate) {
    var publishTimeObj = eval(jsonObjDate);
    var datatime = eval('(new ' + publishTimeObj.source + ')');

    var year = datatime.getFullYear();
    var month = datatime.getMonth() + 1;
    var day = datatime.getDate();

    return (year + "/" + (month < 10 ? "0" + month : month) + "/" + (day < 10 ? "0" + day : day));
}

String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, '');
}
String.prototype.ltrim = function () {
    return this.replace(/(^\s*)/g, "");
}
String.prototype.rtrim = function () {
    return this.replace(/(\s*$)/g, "");
}

//把从c#传回的json格式的时间转化为js中的时间
function jsonObjDateToJsDate(jsonObjDate) {
    var publishTimeObj = eval(jsonObjDate);
    return eval('(new ' + publishTimeObj.source + ')');
}

//计算两个date相差的天数，不足一天算一天
function DateDiffDayCeil(date1, date2) {
    //相差的毫秒数
    var diff = date1.getTime() - date2.getTime();
    return Math.ceil(diff / (1000 * 3600 * 24));
}
//SD end