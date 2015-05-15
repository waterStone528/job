// JavaScript source code
var M0911 = {
    FocusModule: null,
    Init: function () {
        this.SetBtShowEditOnly();
        M0911.STR.EVInit();
    },
    ShowButton: function (BtID) {//1-Show Edit only;2-Show Save only.
        if (BtID == 1) {
            BtStatus(this.FocusModule.querySelectorAll(".ButtonBar>div[data-btst]")[0], "U");
            BtStatus(this.FocusModule.querySelectorAll(".ButtonBar>div[data-btst]")[1], "H");
            this.ReadOnlyMask(true);
        }
        if (BtID == 2) {
            BtStatus(this.FocusModule.querySelectorAll(".ButtonBar>div[data-btst]")[0], "H");
            BtStatus(this.FocusModule.querySelectorAll(".ButtonBar>div[data-btst]")[1], "U");
            this.ReadOnlyMask(false);
        }
    },
    ReadOnlyMask: function (ReadOnly) {
        if (ReadOnly == true) { this.FocusModule.querySelector(".ReadOnlyMask").setAttribute('data-mask', 1); }
        if (ReadOnly == false) { this.FocusModule.querySelector(".ReadOnlyMask").setAttribute('data-mask', 0); }
    },
    Button: function (Sender, Act) {
        var BtID = parseInt(Sender.getAttribute("data-btid"));
        this.FocusModule = Sender.parentElement.parentElement;
        ModuleID = Sender.parentElement.parentElement.id;
        switch (BtID) {
            case 1://Edit Button
                this.ShowButton(2);
                break;
            case 2://Save Button
                this.ShowButton(1);
                switch (ModuleID) {
                    case "M091101":
                        M0911.UIP.mode = this.FocusModule.querySelector('[data-sw="1"]').getAttribute("data-mode");
                        TransingStatus.SetStatus(1);
                        this.STR.EVSaveM091101(M0911.UIP.mode);
                        break;
                    case "M091102":
                        M0911.UIP.webDelay = this.FocusModule.querySelectorAll("input")[0].value;
                        M0911.UIP.maxCusSvrConnLevel = this.FocusModule.querySelectorAll("input")[1].value;
                        M0911.UIP.maxUserConnNum = this.FocusModule.querySelectorAll("input")[2].value;
                        M0911.UIP.cusSvrUserMaxAmount = this.FocusModule.querySelectorAll("input")[3].value;
                        TransingStatus.SetStatus(1);
                        this.STR.EVSaveM091102(M0911.UIP.webDelay, M0911.UIP.maxCusSvrConnLevel, M0911.UIP.maxUserConnNum, M0911.UIP.cusSvrUserMaxAmount);
                        break;
                    case "M091103":
                        M0911.UIP.countSizeLevel = this.FocusModule.querySelectorAll("input")[0].value;
                        M0911.UIP.showCountDownSizeLevel = this.FocusModule.querySelectorAll("input")[1].value;
                        this.STR.EVSaveM091103(M0911.UIP.countSizeLevel, M0911.UIP.showCountDownSizeLevel);
                        TransingStatus.SetStatus(1);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    },
    SetBtShowEditOnly:function(){
        this.FocusModule = M091101;
        this.ShowButton(1);
        this.FocusModule = M091102;
        this.ShowButton(1);
        this.FocusModule = M091103;
        this.ShowButton(1);
    },
    ModeChange:function(value){
        if(value == 0){
            M091102.querySelectorAll("tr")[3].style.display = "";
            M091102.querySelectorAll("tr")[3].querySelector("input").value = M0911.UIP.cusSvrUserMaxAmount;
            M091102.querySelectorAll("tr")[4].style.display = "none";
        }
        else {
            M091102.querySelectorAll("tr")[3].style.display = "none";
            M091102.querySelectorAll("tr")[4].style.display = "";
        }
    },
    SlideSwitch: function (Sender) {
        if (Sender.getAttribute('data-sw') == '1') {
            Sender.setAttribute('data-sw', 0);

            if (Sender.getAttribute("data-mode") == "0") {
                Sender.parentElement.parentElement.parentElement.querySelector('[data-mode="1"]').setAttribute('data-sw', 1);
            }
            else {
                Sender.parentElement.parentElement.parentElement.querySelector('[data-mode="0"]').setAttribute('data-sw', 1);
            }
        }
        else {
            Sender.setAttribute('data-sw', 1);

            if (Sender.getAttribute("data-mode") == "1") {
                Sender.parentElement.parentElement.parentElement.querySelector('[data-mode="0"]').setAttribute('data-sw', 0);
            }
            else {
                Sender.parentElement.parentElement.parentElement.querySelector('[data-mode="1"]').setAttribute('data-sw', 0);
            }
        }

        if (Sender.parentElement.parentElement.parentElement.querySelector('[data-mode="0"]').getAttribute("data-sw") == 1) {
            M091102.querySelectorAll("tr")[3].style.display = "";
            M091102.querySelectorAll("tr")[4].style.display = "none";
        }
        else {
            M091102.querySelectorAll("tr")[3].style.display = "none";
            M091102.querySelectorAll("tr")[4].style.display = "";
        }
    },
    //计算秒数
    CalSec:function(sender){
        sender.parentElement.querySelectorAll("label")[1].innerText = sender.value * M0911.UIP.levelSeconds;
    },

    UIP: {
        mode: "0",  //0：负载均衡 1：客户经理
        cusSvrUserMaxAmount: "1",
        webDelay: "2",
        maxCusSvrConnLevel: "3",
        maxUserConnNum: "4",
        countSizeLevel: "5",
        showCountDownSizeLevel: "2",
        levelSeconds: "10"
    },
    STR: {
        EVInit: function () {
            var busCode = "M0911INIT";
            var data = "busCode=" + busCode;

            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var jsonObjList = JSON.parse(res);
                M0911.UIP.mode = jsonObjList.cusSvrMode;
                M0911.UIP.cusSvrUserMaxAmount = jsonObjList.cusSvrUserMaxAmount;
                M0911.UIP.webDelay = jsonObjList.webDelay;
                M0911.UIP.maxCusSvrConnLevel = jsonObjList.maxCusSvrConnLevel;
                M0911.UIP.maxUserConnNum = jsonObjList.maxUserConnNum;
                M0911.UIP.countSizeLevel = jsonObjList.countSizeLevel;
                M0911.UIP.showCountDownSizeLevel = jsonObjList.showCountDownSizeLevel;
                M0911.UIP.levelSeconds = jsonObjList.levelSeconds;

                M0911.UID.ShowPageData();
            }
            ajaxObj.start();
        },
        EVSaveM091101: function (mode) {
            var busCode = "M091101";
            var data = "busCode=" + busCode + "&mode=" + mode;

            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function () {
                M0911.UID.SaveSucM091101(true);
            }
            ajaxObj.error = function () {
                M0911.UID.SaveSucM091101(false);
            }
            ajaxObj.start();
        },
        EVSaveM091102: function (webDelay, maxCusSvrConnLevel, maxUserConnNum, cusSvrUserMaxAmount) {
            var busCode = "M091102";
            var data = "busCode=" + busCode + "&webDelay=" + webDelay + "&maxCusSvrConnLevel=" + maxCusSvrConnLevel + "&maxUserConnNum=" + maxUserConnNum + "&cusSvrUserMaxAmount=" + cusSvrUserMaxAmount;

            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function () {
                M0911.UID.SaveSuc(true);
            }
            ajaxObj.error = function () {
                M0911.UID.SaveSuc(false);
            }
            ajaxObj.start();
        },
        EVSaveM091103: function (countSizeLevel, showCountDownSizeLevel) {
            var busCode = "M091103";
            var data = "busCode=" + busCode + "&countSizeLevel=" + countSizeLevel + "&showCountDownSizeLevel=" + showCountDownSizeLevel;

            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function () {
                M0911.UID.SaveSuc(true);
            }
            ajaxObj.error = function () {
                M0911.UID.SaveSuc(false);
            }
            ajaxObj.start();
        },
    },
    UID: {
        //show data when page load
        ShowPageData: function () {
            if (M0911.UIP.mode == "0") {
                M091101.querySelector('[data-mode="0"]').setAttribute("data-sw", "1");
                M091102.querySelectorAll("tr")[3].style.display = "";
            }
            else {
                M091101.querySelector('[data-mode="1"]').setAttribute("data-sw", "1");
                M091102.querySelectorAll("tr")[4].style.display = "";
                M091102.querySelectorAll("tr")[5].style.display = "";
                //M091102.querySelectorAll("tr")[6].style.display = "";
            }

            M091102.querySelectorAll("tr")[0].querySelector("input").value = M0911.UIP.webDelay;
            M091102.querySelectorAll("tr")[0].querySelectorAll("label")[0].innerText = M0911.UIP.levelSeconds;
            M091102.querySelectorAll("tr")[0].querySelectorAll("label")[1].innerText = M0911.UIP.webDelay * M0911.UIP.levelSeconds;
            M091102.querySelectorAll("tr")[1].querySelector("input").value = M0911.UIP.maxCusSvrConnLevel;
            M091102.querySelectorAll("tr")[1].querySelectorAll("label")[0].innerText = M0911.UIP.levelSeconds;
            M091102.querySelectorAll("tr")[1].querySelectorAll("label")[1].innerText = M0911.UIP.maxCusSvrConnLevel * M0911.UIP.levelSeconds;
            M091102.querySelectorAll("tr")[2].querySelector("input").value = M0911.UIP.maxUserConnNum;
            M091102.querySelectorAll("tr")[3].querySelector("input").value = M0911.UIP.cusSvrUserMaxAmount;
            M091103.querySelectorAll("tr")[0].querySelector("input").value = M0911.UIP.countSizeLevel;
            M091103.querySelectorAll("tr")[0].querySelectorAll("label")[0].innerText = M0911.UIP.levelSeconds;
            M091103.querySelectorAll("tr")[0].querySelectorAll("label")[1].innerText = M0911.UIP.countSizeLevel * M0911.UIP.levelSeconds;
            M091103.querySelectorAll("tr")[1].querySelector("input").value = M0911.UIP.showCountDownSizeLevel;
            M091103.querySelectorAll("tr")[1].querySelectorAll("label")[0].innerText = M0911.UIP.levelSeconds;
            M091103.querySelectorAll("tr")[1].querySelectorAll("label")[1].innerText = M0911.UIP.showCountDownSizeLevel * M0911.UIP.levelSeconds;
        },

        SaveSucM091101: function (isSuc) {
            if (isSuc == true) {
                M091102.querySelectorAll("tr")[0].querySelectorAll("label")[0].innerText = M0911.UIP.levelSeconds;
                M091102.querySelectorAll("tr")[0].querySelectorAll("label")[1].innerText = M0911.UIP.webDelay * M0911.UIP.levelSeconds;
                M091102.querySelectorAll("tr")[1].querySelectorAll("label")[0].innerText = M0911.UIP.levelSeconds;
                M091102.querySelectorAll("tr")[1].querySelectorAll("label")[1].innerText = M0911.UIP.maxCusSvrConnLevel * M0911.UIP.levelSeconds;
                M091103.querySelectorAll("tr")[0].querySelectorAll("label")[0].innerText = M0911.UIP.levelSeconds;
                M091103.querySelectorAll("tr")[0].querySelectorAll("label")[1].innerText = M0911.UIP.countSizeLevel * M0911.UIP.levelSeconds;
                M091103.querySelectorAll("tr")[1].querySelectorAll("label")[0].innerText = M0911.UIP.levelSeconds;
                M091103.querySelectorAll("tr")[1].querySelectorAll("label")[1].innerText = M0911.UIP.showCountDownSizeLevel * M0911.UIP.levelSeconds;

                setTimeout("TransingStatus.SetStatus(3);", 200);
            }
            else {

            }
        },

        SaveSuc: function (isSuc) {
            if (isSuc == true) {
                TransingStatus.SetStatus(3);
            }
            else {
                alert("error");
            }
        },
    },
};