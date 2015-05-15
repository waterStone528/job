// JavaScript source code
var M0905 = {
    FocusModule: null,
    Init: function () {
        this.STR.EVINIT();
    },
    Button: function (Sender) {
        var BtID = parseInt(Sender.getAttribute("data-btid"));
        this.FocusModule = Sender.parentElement.parentElement;
        ModuleID = Sender.parentElement.parentElement.id;
        switch (BtID) {
            case 1: this.ShowButton(2); break;//Edit Button
            case 2://Save Button
                this.ShowButton(1);
                switch (ModuleID) {
                    case "M090501": TransingStatus.SetStatus(1); var Inp = M090501.querySelectorAll("input"); M0905.STR.EVM090501(Inp[0].value, Inp[1].value, Inp[2].value, Inp[3].value, M090501.querySelector(".SlideButton").getAttribute("data-sw")); break;
                    default: break;
                }
                break;
            default:
                break;
        }
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
    UIP: {},
    STR: {
        EVINIT: function () {
            ///<summary>页面初始化</summary>
            //var Data = { C1: "http://vanch.maill.com", C2: "admin", C3: "123456", C4: "25", C5: "1" };
            //M0905.UID.M090501(Data);

            var busCode = "M0905INIT";
            var data = "busCode=" + busCode;

            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);

                var dataFt = {};
                dataFt.C1 = resObj.SMTP == null ? "" : resObj.SMTP.trim();
                dataFt.C2 = resObj.userName == null ? "" : resObj.userName.trim();
                dataFt.C3 = "******";
                dataFt.C4 = resObj.port == null ? "" : resObj.port;
                dataFt.C5 = resObj.switchStatus == true ? "1" : "0";

                M0905.UID.M090501(dataFt);
            }
            ajaxObj.start();
        },
        EVM090501: function (C1, C2, C3, C4, C5) {
            ///<summary>修改接口A</summary>
            //console.log(C1 + "__" + C2 + "__" + C3 + "__" + C4 + "__" + C5);
            //setTimeout("M0905.UID.M090502(true);", 200);

            var busCode = "M090501";
            var data = "busCode=" + busCode + "&smtp=" + C1 + "&userName=" + C2 + "&pwd=" + C3 + "&port=" + C4 + "&switchStatus=" + C5;

            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function () {
                M0905.UID.M090502(true);
            }
            ajaxObj.error = function () {
                M0905.UID.M090502(false);
            }
            ajaxObj.start();

        },
    },
    UID: {
        M090501: function (Data) {
            ///<summary>接口A</summary>
            M0905.FocusModule = M090501;
            M0905.ShowButton(1);
            M090501.querySelectorAll("input")[0].value = Data.C1;
            M090501.querySelectorAll("input")[1].value = Data.C2;
            M090501.querySelectorAll("input")[2].value = Data.C3;
            M090501.querySelectorAll("input")[3].value = Data.C4;
            M090501.querySelector(".SlideButton").setAttribute("data-sw", Data.C5);
        },
        M090502: function (Res) {
            ///<summary>保存接口A</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        }
    }
};