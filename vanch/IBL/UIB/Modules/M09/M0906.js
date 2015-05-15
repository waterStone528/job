// JavaScript source code
var M0906 = {
    FocusModule: null,
    Init: function () {
        this.STR.EVInit();
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
                    case "M090601": TransingStatus.SetStatus(1); var Inp = M090601.querySelectorAll("input"); this.STR.EVM090601(Inp[0].value, (Inp[1].value / 1000).ToNum(2, 6), (Inp[2].value / 1000).ToNum(2, 6)); break;
                    case "M090602": TransingStatus.SetStatus(1); var Inp = M090602.querySelectorAll("input"); this.STR.EVM090602(Inp[0].value, Inp[1].value, Inp[2].value); break;
                    default: break;
                }
                break;
            default:
                break;
        }
    },
    UIP: {},
    STR: {
        EVInit: function () {
            //Data = [{ C1: "1", C2: "2", C3: "3" }, { C1: "1", C2: "2", C3: "3", C4: "免费" }];
            //M0906.UID.M090601(Data[0]);
            //M0906.UID.M090602(Data[1]);

            var busCode = "M0906INIT";
            var data = "busCode=" + busCode;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                var dataFt = M0906.STR.InitDataBdToFt(resObj);

                M0906.UID.M090601(dataFt[0]);
                M0906.UID.M090602(dataFt[1]);
            }
            ajaxObj.start();
        },
        EVM090601: function (C1, C2, C3) {
            ///<summary>修改参数设置</summary>
            //console.log(C1 + "__" + C2 + "__" + C3);
            //setTimeout("M0906.UID.M090603(true);", 200);

            var busCode = "M090601";
            var data = "busCode=" + busCode + "&minInvestMoneyAmount=" + C1 + "&minDailyRate=" + C2 + "&maxDailyRate=" + C3;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function () {
                M0906.UID.M090603(true);
            }
            ajaxObj.error = function () {
                M0906.UID.M090603(false);
            }
            ajaxObj.start();
        },
        EVM090602: function (C1, C2, C3) {
            ///<summary>修改收费设置</summary>
            //console.log(C1 + "__" + C2 + "__" + C3);
            //setTimeout("M0906.UID.M090604(false);", 200);

            var busCode = "M090602";
            var data = "busCode=" + busCode + "&openServerCost=" + C1 + "&consultantReserveCost=" + C2 + "&financingReserveCost=" + C3;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function () {
                M0906.UID.M090604(true);
            }
            ajaxObj.error = function () {
                M0906.UID.M090604(false);
            }
            ajaxObj.start();
        },

        //后-前 initData
        InitDataBdToFt: function (dataBd) {
            var data1 = {};
            var data2 = {};

            data1.C1 = dataBd.minInvestMoneyAmount == null ? "" : dataBd.minInvestMoneyAmount;
            data1.C2 = dataBd.minDailyRate == null ? "" : dataBd.minDailyRate;
            data1.C3 = dataBd.maxDailyRate == null ? "" : dataBd.maxDailyRate;
            data2.C1 = dataBd.openServerCost == null ? "" : dataBd.openServerCost;
            data2.C2 = dataBd.consultantReserveCost == null ? "" : dataBd.consultantReserveCost;
            data2.C3 = dataBd.financingReserveCost == null ? "" : dataBd.financingReserveCost;
            data2.C4 = "免费";

            return [data1, data2];
        }
    },
    UID: {
        M090601: function (Data) {
            ///<summary>参数设置</summary>
            M0906.FocusModule = M090601;
            M0906.ShowButton(1);
            M090601.querySelectorAll("input")[0].value = Data.C1;
            M090601.querySelectorAll("input")[1].value = Data.C2 * 1000;
            M090601.querySelectorAll("input")[2].value = Data.C3 * 1000;
            FloatInp(M090601.querySelectorAll("input")[0], 2);
            FloatInp(M090601.querySelectorAll("input")[1], 2);
            FloatInp(M090601.querySelectorAll("input")[2], 2);
        },
        M090602: function (Data) {
            ///<summary>收费设置</summary>
            M0906.FocusModule = M090602;
            M0906.ShowButton(1);
            M090602.querySelectorAll("td")[1].querySelector("input").value = Data.C1;
            M090602.querySelectorAll("td")[3].querySelector("input").value = Data.C2;
            M090602.querySelectorAll("td")[5].querySelector("input").value = Data.C3;
            M090602.querySelectorAll("td")[7].querySelector("label").DspV = Data.C4;
            FloatInp(M090602.querySelectorAll("td")[1].querySelector("input"), 0);
            FloatInp(M090602.querySelectorAll("td")[3].querySelector("input"), 0);
            FloatInp(M090602.querySelectorAll("td")[5].querySelector("input"), 0);
        },
        M090603: function (Res) {
            ///<summary>保存参数设置</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
        M090604: function (Res) {
            ///<summary>保存收费设置</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
    }
};