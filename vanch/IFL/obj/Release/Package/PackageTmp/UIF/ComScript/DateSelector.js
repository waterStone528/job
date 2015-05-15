// JavaScript source code

var CControl = {
    NowYear: (new Date).getFullYear(),
    NowMonth: (new Date).getMonth() + 1,
    NowDay: (new Date).getDate(),
    DMin: 1,
    DMax: (new Date((new Date).getFullYear(), (new Date).getMonth() + 1, 0)).getDate(),
    YMin: 1900,
    YMax: 2030,
    MMin: 1,
    MMax: 12,
    Interval: 0,
    RebateIntvID: 0,
    FocusItem: Object,
    YearInit: function () {
        var YearDiv = "";
        for (y = this.YMin; y <= this.YMax; y++) {
            YearDiv += "<div>" + y + "</div>";
        }
        this.FocusItem.querySelector(".Year").innerHTML = YearDiv;
        this.FocusItem.querySelector(".Year").style.top = (this.NowYear - this.YMin) * -60 + "px";
    },
    MonthInit: function () {
        var MonthDiv = "";
        for (m = this.MMin; m <= this.MMax; m++) {
            MonthDiv += "<div>" + m + "</div>";
        }
        this.FocusItem.querySelector(".Month").innerHTML = MonthDiv;
        this.FocusItem.querySelector(".Month").style.top = (this.NowMonth - this.MMin) * -60 + "px";
    },
    DayInit: function () {
        var DayDiv = "";
        for (d = this.DMin; d <= this.DMax; d++) {
            DayDiv += "<div>" + d + "</div>";
        }
        this.FocusItem.querySelector(".Day").innerHTML = DayDiv;
        this.FocusItem.querySelector(".Day").style.top = (this.NowDay - this.DMin) * -60 + "px";
    },

    Close: function (Obj) {
        Obj.removeNode(true);
        this.FocusItem.querySelector("[data-showtime='1']").removeNode(true);
        var Month;
        var Day;
        if (this.NowMonth < 10) { Month = "0" + this.NowMonth; } else { Month = this.NowMonth; }
        if (this.NowDay < 10) { Day = "0" + this.NowDay; } else { Day = this.NowDay; }
        this.FocusItem.querySelectorAll(".ItemDateInp")[0].innerText = this.NowYear + "/" + Month + "/" + Day;
    },

    DateCK:function(){
        IRC.IntervalDate(this.FocusItem.querySelector("div"), this.FocusItem.querySelector("div").innerText);
    },

    ItemInfoDsp: function (PostionObj, Position, CallBack) {
        var InfoChild = document.createElement("div");
        if (!Position) { Position = "right: 120px;" } else { Position = " left: 0px;"; }
        InfoChild.style.cssText = "position:absolute;vertical-align:bottom;top:10px;" + Position ;
        InfoChild.innerHTML = DateSelectori.innerHTML;

        var ContainerDiv = document.createElement("div");
        ContainerDiv.style.cssText = "position:relative;width:0px;height:0px;float:left;";
        ContainerDiv.setAttribute("data-showtime", "1");
        ContainerDiv.appendChild(InfoChild);

        var MaskLayer = document.createElement("div");
        MaskLayer.style.cssText = "position:fixed; top:0px;left:0px;right:0px;bottom:0px; z-index:10;background-color: black; -moz-opacity: 0; opacity:0; filter: alpha(opacity=0);background-color:rgba(0, 0, 0, 0);";
        MaskLayer.setAttribute("onmouseup", "CControl.Close(this);" + CallBack);
        MaskLayer.setAttribute('data-mask', '1');
        ContainerDiv.appendChild(MaskLayer);

        PostionObj.parentElement.appendChild(ContainerDiv);
        this.FocusItem = PostionObj;
        CControl.FocusItem = PostionObj.parentElement;
        CControl.YearInit(); CControl.MonthInit(); CControl.DayInit();
    },
    YearSelector: function (Add, Act) {
        if (Act == 1) { window.clearInterval(this.RebateIntvID); };
        var Top = this.FocusItem.querySelector(".Year").style.top.replace("px", "");

        if (Act == 0) {
            window.clearInterval(this.RebateIntvID);
            if (this.NowYear <= this.YMax) { if (Add == false && this.NowYear > this.YMin) { this.YearSelector(false, 2); this.RebateIntvID = window.setInterval("CControl.YearSelector(false,2)", 200); } }
            if (this.NowYear >= this.YMin) { if (Add == true && this.NowYear < this.YMax) { this.YearSelector(true, 2); this.RebateIntvID = window.setInterval("CControl.YearSelector(true,2)", 200); } }
        }
        if (Act == 2) {
            if (Add == true) {
                if (-Top / 60 <= this.YMax - this.YMin - 1) {
                    this.FocusItem.querySelector(".Year").style.top = Top - 60 + "px";
                    this.NowYear = (-Top / 60 + 1) + this.YMin;
                }
            };
            if (Add == false) {
                if (Top / 60 < 0) {
                    this.FocusItem.querySelector(".Year").style.top = (parseInt(Top) + 60) + "px";
                    this.NowYear = (-Top / 60 - 1) + this.YMin;
                }
            };
            if (this.NowYear >= this.YMax || this.NowYear <= this.YMin) { window.clearInterval(this.RebateIntvID); };
        }
    },
    MonthSelector: function (Add, Act) {
        if (Act == 1) { window.clearInterval(this.RebateIntvID); };
        var Top = this.FocusItem.querySelector(".Month").style.top.replace("px", "");
        if (Act == 0) {
            window.clearInterval(this.RebateIntvID);
            if (this.NowMonth <= this.MMax) { if (Add == false && this.NowMonth >= this.MMin) { this.MonthSelector(false, 2); this.RebateIntvID = window.setInterval("CControl.MonthSelector(false,2)", 200); } }
            if (this.NowMonth >= this.MMin) { if (Add == true && this.NowMonth <= this.MMax) { this.MonthSelector(true, 2); this.RebateIntvID = window.setInterval("CControl.MonthSelector(true,2)", 200); } }
        }
        if (Act == 2) {
            if (Add == true) {
                if (-Top / 60 + this.MMin < this.MMax) {
                    this.FocusItem.querySelector(".Month").style.top = Top - 60 + "px";
                    this.NowMonth = (-Top / 60 + 2);
                }
            };
            if (Add == false) {
                if (Top / 60 * this.MMin < 0) {
                    this.FocusItem.querySelector(".Month").style.top = (parseInt(Top) + 60) + "px";
                    this.NowMonth = (-Top / 60);
                }
            };
            if (this.NowMonth >= this.MMax || this.NowMonth <= this.MMin) { window.clearInterval(this.RebateIntvID); };
        }
        if (this.Interval == 0) { this.DMax = (new Date(this.NowYear, this.NowMonth, 0)).getDate(); }
        var NowDay = this.FocusItem.querySelector(".Day").style.top.replace("px", "");
        if (-NowDay / 60 + this.DMin > this.DMax) { this.NowDay = this.DMax; } else { this.NowDay = -NowDay / 60 + this.DMin; }
        this.FocusItem.querySelector(".Day").style.top = (this.NowDay - this.DMin) * -60 + "px";
        this.DayInit();

    },
    DaySelector: function (Add, Act) {
        if (Act == 1) { window.clearInterval(this.RebateIntvID); };
        var Top = this.FocusItem.querySelector(".Day").style.top.replace("px", "");
        this.NowDay = (-Top / 60) + this.DMin;
        if (Act == 0) {
            window.clearInterval(this.RebateIntvID);
            if (this.NowDay <= this.DMax) { if (Add == false && this.NowDay > this.DMin) { this.DaySelector(false, 2); this.RebateIntvID = window.setInterval("CControl.DaySelector(false,2)", 200); } }
            if (this.NowDay >= this.DMin) { if (Add == true && this.NowDay < this.DMax) { this.DaySelector(true, 2); this.RebateIntvID = window.setInterval("CControl.DaySelector(true,2)", 200); } }
        }
        if (Act == 2) {
            if (Add == true) {
                if (-Top / 60 + this.DMin < this.DMax) {
                    this.FocusItem.querySelector(".Day").style.top = Top - 60 + "px";
                }
            };
            if (Add == false) {
                if (Top / 60 + this.DMin< this.DMin) {
                    this.FocusItem.querySelector(".Day").style.top = (parseInt(Top) + 60) + "px";
                }
            };
            if (this.NowDay >= this.DMax || this.NowDay <= this.DMin) { window.clearInterval(this.RebateIntvID); };
        }
    },
};