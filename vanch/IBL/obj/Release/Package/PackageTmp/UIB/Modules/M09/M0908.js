var M0908 = {
    FocusModule: null,
    Init: function () {
        this.STR.EVINIT();
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
    Button: function (Sender, Act) {
        var BtID = parseInt(Sender.getAttribute("data-btid"));
        this.FocusModule = Sender.parentElement.parentElement;
        ModuleID = Sender.parentElement.parentElement.id;
        switch (BtID) {
            case 1: this.ShowButton(2); break;//Edit Button
            case 2://Save Button
                this.ShowButton(1);
                switch (ModuleID) {
                    case "M090801": TransingStatus.SetStatus(1); var Inp = M090801.querySelectorAll("input"); this.STR.EVM090801(Inp[0].value, (Inp[1].value / 100).ToNum(2, 4)); break;
                    case "M090802": TransingStatus.SetStatus(1); var Inp = M090802.querySelectorAll("input"); this.STR.EVM090802(Inp[0].value); break;
                    case "M090803": TransingStatus.SetStatus(1); var Inp = M090803.querySelectorAll("input"); this.STR.EVM090803((Inp[0].value / 100).ToNum(2, 4)); break;
                    default: break;
                }
                break;
            default:
                break;
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
            var Data = [{ C1: "5", C2: "10" }, { C1: "5" }, { C1: "5" }];
            //M0908.UID.M090801(Data[0]);
            //M0908.UID.M090802(Data[1]);
            //M0908.UID.M090803(Data[2]);

            var busCode = "M0908INIT";
            var data = "busCode=" + busCode;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                var dataFt = M0908.STR.DataBdToFt(resObj);

                M0908.UID.M090801(dataFt[0]);
                M0908.UID.M090802(dataFt[1]);
                M0908.UID.M090803(Data[2]);
            }
            ajaxObj.start();
        },
        EVM090801: function (C1, C2) {
            ///<summary>修改应收账款</summary>
            //console.log(C1 + "__" + C2);
            //setTimeout("M0908.UID.M090804(true);", 200);

            var busCode = "M090801";
            var data = "busCode=" + busCode + "&needPayDays=" + C1 + "&overdueRateDaily=" + C2;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function () {
                M0908.UID.M090804(true);
            }
            ajaxObj.error = function () {
                M0908.UID.M090804(false);
            }
            ajaxObj.start();
        },
        EVM090802: function (C1) {
            ///<summary>修改附加费用</summary>
            //console.log(C1);
            //setTimeout("M0908.UID.M090805(false);", 200);

            var busCode = "M090802";
            var data = "busCode=" + busCode + "&shortMessageCost=" + C1;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function () {
                M0908.UID.M090805(true);
            }
            ajaxObj.error = function () {
                M0908.UID.M090805(false);
            }
            ajaxObj.start();
        },
        EVM090803: function (C1) {
            ///<summary>修改充值参数</summary>
            console.log(C1);
            setTimeout("M0908.UID.M090806(true);", 200);
        },

        //后-前 
        DataBdToFt: function (dataBd) {
            var data1 = {};
            var data2 = {};

            data1.C1 = dataBd.needPayDays == null ? "" : dataBd.needPayDays;
            data1.C2 = dataBd.overdueRateDaily == null ? "" : dataBd.overdueRateDaily;
            data2.C1 = dataBd.shortMessageCost == null ? "" : dataBd.shortMessageCost;

            return [data1, data2];
        }
    },
    UID: {
        M090801: function (Data) {
            ///<summary>应收账款</summary>
            M0908.FocusModule = M090801;
            M0908.ShowButton(1);
            M090801.querySelectorAll("input")[0].value = Data.C1;
            M090801.querySelectorAll("input")[1].value = Data.C2 * 100;
            FloatInp(M090801.querySelectorAll("input")[0], 0);
            FloatInp(M090801.querySelectorAll("input")[1], 2);
        },
        M090802: function (Data) {
            ///<summary>附加收费</summary>
            M0908.FocusModule = M090802;
            M0908.ShowButton(1);
            M090802.querySelectorAll("input")[0].value = Data.C1;
            FloatInp(M090802.querySelectorAll("input")[0], 2);
        },
        M090803: function (Data) {
            ///<summary>充值参数</summary>
            M0908.FocusModule = M090803;
            M0908.ShowButton(1);
            M090803.querySelectorAll("input")[0].value = Data.C1 * 100;
            FloatInp(M090803.querySelectorAll("input")[0], 2);
        },
        M090804: function (Res) {
            ///<summary>保存应收账款</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
        M090805: function (Res) {
            ///<summary>保存附加费用</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
        M090806: function (Res) {
            ///<summary>保存充值参数</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
    }
};