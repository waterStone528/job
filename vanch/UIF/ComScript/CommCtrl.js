// JavaScript source code
var SysUIVer = "2.00"+ Math.random();
function bodyresize() {
    MainZoneResize();
    ReCalcItemSize();
    //BottomBarLeftInfo.innerText = "MainZone Height:" + MainZone.style.height + "  Height:" + document.documentElement.clientHeight + "  Width:" + document.documentElement.clientWidth;//-----------debug script------
    bodyResizeScript();
}
function bodyLoad() {
    MainZoneResize();
    ReCalcItemSize();
    bodyLoadScript();
    //setInterval("AddItem()", 1000);
}
var LastItem = 0;//0,have remain items not be loaded, 1-no remain items,all has loaded.
function MainZoneResize() {
    MainZone.style.height = document.documentElement.clientHeight - RedLine.clientHeight - TopBar.clientHeight - BottomBar.clientHeight + "px";
}

var ItemWidthMin = 440;//this is important value, adujst carefull.
var ItemWidthMax = 580;
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
    //BottomBarRightInfo.innerText = "ItemWidth Set:" + ItemWidth + ' padding:' + padding + " border:" + border + " ItemWidthMin:" + ItemWidthMin;
}
function AdjTbWidth(TbWidth) {
    document.styleSheets[0].rules[1].style.width = TbWidth + "px";
}
function ReCalcItemSize() {
    if (document.querySelectorAll("#ItemContainer").length == 0) { return; }
    var CalcErrValue = 8;//best value is #MainZone > #ItemContainer 's margin-left, the value cause the result like #ItemContainer 's margin-right.
    var ContentWidth = ItemContainer.clientWidth - CalcErrValue; if (ContentWidth <= 0) { return; }
    ItemCnt = parseInt(ContentWidth / ItemWidthMin);
    ItemWidthCur = ContentWidth / ItemCnt;
    if (ItemWidthCur > ItemWidthMax) { ItemWidthCur = ItemWidthMax; }
    AdjTbWidth(ContentWidth);
    AdjItemWidth(ItemWidthCur);
    //BottomBarLeftInfo.innerText = "Auto Display Subsystem," + ItemCnt + " items every Row" + " Width:" + document.documentElement.clientWidth + " Scroll Width:" + document.documentElement.scrollWidth;
}

var RemItemsLoading = false;
var ScrollBarAppreared = false;
function ScrollTracker(Obj) {
    var PreLoadLength = 60;
    //BottomBarRightInfo.innerText = Obj.scrollHeight + ":" + Obj.scrollTop + "   " + (Obj.scrollHeight - Obj.scrollTop) + "  Obj Height:" + Obj.clientHeight;
    if (Obj.scrollHeight - Obj.scrollTop <= Obj.clientHeight + PreLoadLength) {
        if (LastItem == 0 && RemItemsLoading == false) {
            //LoadingBoxCtl(1);
            LoadRemItems();
            //continue load items
        }
    }
}

//function LoadingBoxCtl(Act) {
//    if (Act == 1) {
//      
//    } else { LoadingBoxi.style.display = "none"; }
//}


//数据加载 
function LoadingBoxCtl(Act, msg) {
    var loadingBOX = document.querySelector('.LoadingBox');
    var loadingBOXDisappearIns = null;
    clearTimeout(loadingBOXDisappearIns);
    LoadingBoxi.style.display = "";
    loadingBOX.setAttribute('data-status', '1')//加载中...                  

    if (Act == 0) {//加载中消失
        loadingBOX.setAttribute('data-status', '01');
        loadingBOXDisappearIns = setTimeout(" document.querySelector('.LoadingBox').setAttribute('data-status', '02');", 500);

    }
    if (Act == 2) {//加载失败，请重试...
        loadingBOX.setAttribute('data-status', '2')
        loadingBOX.focus();
        loadingBOX.setAttribute('data-status', '2a');
    }
}

var FreeInfoBtObj;
function FreeInfo(BtObj, InfoID) {
    if (BtStatusClass(BtObj, 4) == 4) { return; } else { BtStatusClass(BtObj, 4); }
    if (FreeInfoBtObj != null) { BtStatusClass(FreeInfoBtObj, 1); }
    LoadingBoxCtl(1);
    if (document.querySelectorAll("#MainSvrItem").length > 0) { MainZone.removeChild(MainSvrItem); }
    if (document.querySelectorAll(".LocationBar").length > 0) { MainZone.removeChild(document.querySelector(".LocationBar")); }
    if (document.querySelectorAll("#ToolBar").length > 0) { MainZone.removeChild(ToolBar); }
    ItemWidthMin = 500;
    ItemWidthMax = 1000;
    switch (InfoID) {
        case 1:
            LoadMod("FreeInfo01", "", "FreeInfo"); break;
        case 2:
            LoadMod("FreeInfo02", "", "FreeInfo"); break;
        case 3:
            LoadMod("FreeInfo03", "", "FreeInfo"); break;
        case 4:
            LoadMod("FreeInfo04", "", "FreeInfo"); break;
        case 5:
            LoadMod("FreeInfo05", "", "FreeInfo"); break;
        case 6:
            LoadMod("FreeInfo06", "", "FreeInfo"); break;
        case 7:
            LoadMod("FreeInfo07", "", "FreeInfo"); break;
        case 8:
            LoadMod("FreeInfo08", "", "FreeInfo"); break;
        default: break;
    }
    FreeInfoBtObj = BtObj;
}

function LoadMod(ModuleName, CallBack, ModFloder) {
    if (!CallBack) { CallBack = ""; }; if (!ModFloder) { ModFloder = ""; } else { ModFloder = ModFloder + "/"; }
    var OldItemSi = document.querySelector("#ItemSi");
    OldItemSi.removeAttribute("onload"); OldItemSi.removeAttribute("src");//Clear relative settings,Don't Remove this line.
    var TempItemSi = OldItemSi.cloneNode(true); OldItemSi.parentNode.removeChild(OldItemSi);
    TempItemSi.setAttribute("onload", CallBack); TempItemSi.src = "Modules/" + ModFloder + ModuleName + ".html?v=" + SysUIVer; MainZone.appendChild(TempItemSi);
}

function OpenWin(Url) {
    window.open(Url, '_blank', 'menubar=no,toolbar=no,location=no,directories=no,scrollbars=yes,status=yes,resizable=yes,height=785,width=1200')
}

function LoadJS(JSModName, ModFloder) {
    if (!ModFloder) { ModFloder = ""; } else { ModFloder = ModFloder + "/"; }
    document.write("<script type=\"text\/javascript\" src=\"ComScript/" + ModFloder + JSModName + ".js?v=" + SysUIVer + "\">" + "<" + "/script>");
}

function LoadTP(TemplateName, CallBack, ModFloder) {
    if (!CallBack) { CallBack = ""; }
    if (!ModFloder) { ModFloder = ""; } else { ModFloder = ModFloder + "/"; }
    document.write(" <div style=\"height:0px;width:0px;display:none;\" id=\"" + TemplateName + "i\"><iframe onload=\"this.parentElement.innerHTML=this.contentDocument.body.innerHTML;" + CallBack + ";\" style=\"height:0px;width:0px;visibility:collapse;display:none;\" src=\"Modules/" + ModFloder + TemplateName + ".html?v=" + SysUIVer + "\"></iframe></div>");
}

function BtStatusClass(Obj, BtStatus) {
    ///<summary></summary>
    ///<param name="BtStatus">0:just return value;1:To be normal;2:mousedown status;3:To be disabled;4:To be selected;5:To Hidden;</param>
    var cName = "easycoding"; cName = Obj.className;
    switch (BtStatus) {
        case 0://just return Button status by class name
            if (cName.substr(cName.length - 1, 1) == "D") { return 2; }
            if (cName.substr(cName.length - 9, 9) == "-Disabled") { return 3; }
            if (cName.substr(cName.length - 9, 9) == "-Selected") { return 4; }
            if (cName.substr(cName.length - 7, 7) == "-Hidden") { return 5; }
            return 1;
            break;
        case 1://reset to normal
            if (cName.substr(cName.length - 1, 1) == "D") { cName = cName.substr(0, cName.length - 1); }
            if (cName.substr(cName.length - 9, 9) == "-Disabled") { cName = cName.substr(0, cName.length - 9); }
            if (cName.substr(cName.length - 9, 9) == "-Selected") { cName = cName.substr(0, cName.length - 9); }
            if (cName.substr(cName.length - 7, 7) == "-Hidden") { cName = cName.substr(0, cName.length - 7); }
            Obj.className = cName;
            break;
        case 2://button be pressed and keep in mousedown status
            if (BtStatusClass(Obj, 0) == 3) { return; }
            if (cName.indexOf("D") == cName.length - 1) { return; } else { Obj.className = cName + "D"; }
            break;
        case 3://button be disabled
            if (cName.substr(cName.length - 1, 1) == "D") { cName = cName.substr(0, cName.length - 1); }
            if (cName.indexOf("-Disabled") == cName.length - 9) { return; } else { Obj.className = cName + "-Disabled"; }
            break;
        case 4://button be selected
            if (cName.substr(cName.length - 1, 1) == "D") { cName = cName.substr(0, cName.length - 1); }
            if (cName.indexOf("-Selected") == cName.length - 9) { return; } else { Obj.className = cName + "-Selected"; }
            break;
        case 5://button be hidden
            if (cName.substr(cName.length - 1, 1) == "D") { cName = cName.substr(0, cName.length - 1); }
            if (cName.indexOf("-Hidden") == cName.length - 9) { return; } else { Obj.className = cName + "-Hidden"; }
            break;
        default:
            break;
    }
}

function AddFavorite() {
    if (document.documentElement) { window.external.addFavorite('http://www.vanch.com', '投资理财'); }
    else if (window.sidebar) { window.sidebar.addPanel('凡奇P2P借贷', 'http://www.vanch.com', ""); }
}

function LoginAction(UserName) {
    ///<summary>用户登陆后执行的事情</summary><param name="UserName">用户姓名</param>
    TopBarButtons.querySelectorAll(".TopBarButtonsText")[2].querySelectorAll("div")[1].innerText = UserName;
    TopBarButtons.querySelectorAll(".TopBarButtonsText")[3].setAttribute("class", "TopBarButtonsText Hidden");
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
    ///<param name="Method">0:四舍五入;1:按长度截取;2:最大数截取;3:最大数四舍五入;</param>
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
        default:
            alert("程序错误\r\n \r\n动作：尝试在未格化的目标上进行格式化操作,\r\n格式化代码为：" + DataFMT + "\r\n控件ID：" + Obj.id + "\r\n样式名：" + Obj.className + "\r\n当前内容：" + Obj.innerText); break;
    }
    //Obj.title = "格式代码:" + DataFMT + "  原值:" + Value;// 此为UI检视时使用，正式环境需删除此行。
};

Object.defineProperty(HTMLTableCellElement.prototype, "DspV", { set: function (val) { _Format_(val, this); }, get: function () { return this.innerText; } });
Object.defineProperty(HTMLLabelElement.prototype, "DspV", { set: function (val) { _Format_(val, this); }, get: function () { return this.innerText; } });
Object.defineProperty(HTMLSpanElement.prototype, "DspV", { set: function (val) { _Format_(val, this); }, get: function () { return this.innerText; } });

function _IRC_Format_(Value, Obj) {
    var WrongFMT = "格式出错";
    if (Value) {
        try {
            if (isNaN(Value.Min) || isNaN(Value.Max)) {
                if (Value.Min != null || Value.Max != null) {
                Obj.parentNode.querySelector("div>div").innerText = WrongFMT;
                Obj.parentNode.querySelector("div>div").setAttribute("onmousedown", "");
                }
            }
        } catch (err) { alert("错误信息：" + err.message); }
    }
};

Object.defineProperty(HTMLInputElement.prototype, "SDMM", { set: function (val) { _IRC_Format_(val, this); }, get: function () { } });

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

function DateFormat(Param) {
    ///<summary>时间格式规定</summary>
    var arr, RetDate;
    if (Param.indexOf("-") > 0) { arr = Param.split("-"); }
    if (Param.indexOf("/") > 0) { arr = Param.split("/"); }
    RetDate = arr[0] + "/" + (arr[1].length == 1 ? "0" + arr[1] : arr[1]) + "/" + (arr[2].length == 1 ? "0" + arr[2] : arr[2]);
    return RetDate;
}

function FloatInp(Obj, Length) {
    ///<summary>输入框允许小数的最大位数</summary><param name="Obj">输入框</param><param name="Length">最大的位数</param>
    if (Length == 0) { Obj.value = parseInt(parseFloat(Obj.value)); }
    else if (Obj.value.indexOf(".") >= 0) { if (Obj.value.split(".")[1].length > Length) { Obj.value = (parseInt(parseFloat(Obj.value) * Math.pow(10, Length)) / Math.pow(10, Length)).toFixed(Length); } }
}
function DateObjFormate(datatime) {
    var year = datatime.getFullYear();
    var month = datatime.getMonth();
    var day = datatime.getDate();

    return (year + "/" + (month < 10 ? "0" + month : month) + "/" + (day < 10 ? "0" + day : day));
}

//SD start
function DateObjFormate(datatime) {
    var year = datatime.getFullYear();
    var month = datatime.getMonth() + 1;
    var day = datatime.getDate();

    return (year + "/" + (month < 10 ? "0" + month : month) + "/" + (day < 10 ? "0" + day : day));
}

function BdDateStrFormate(jsonObjDate) {
    var publishTimeObj = eval(jsonObjDate);
    var datatime = eval('(new ' + publishTimeObj.source + ')');

    var year = datatime.getFullYear();
    var month = datatime.getMonth() + 1;
    var day = datatime.getDate();

    return (year + "/" + (month < 10 ? "0" + month : month) + "/" + (day < 10 ? "0" + day : day));
}

function AJAXC() {
    this.xmlhttp = null;
    this.url = "../FBF/Control.ashx";
    this.data = null;
    this.type = "post";
    this.async = true;
    this.timeout = 0;  //0：无超时判断
    this.success = function (res) { };
    this.defaultError = function (xmlhttpObj) {
        alert("ajax error!\nreadyState:" + xmlhttpObj.readyState + "\nstatus:" + xmlhttpObj.status + "\nstatusText:" + xmlhttpObj.statustext);
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
                alert("ajax error!\nreadyState:" + this.xmlhttp.readyState + "\nstatus:" + this.xmlhttp.status + "\nstatusText:" + this.xmlhttp.statustext + "\nerror:" + err);
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
