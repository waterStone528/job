// JavaScript source code
var M0909 = {
    FocusModule: null,
    Init: function () {
        this.STR.EVINIT();
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
                    case "M090901": TransingStatus.SetStatus(1); var Inp = M090901.querySelectorAll("input"); M0909.STR.EVM090901(Inp[0].value, (Inp[1].value / 100).ToNum(2, 4)); break;
                    case "M090902": TransingStatus.SetStatus(1); var Inp = M090902.querySelectorAll("input"); M0909.STR.EVM090902(Inp[0].value, (Inp[1].value / 100).ToNum(2, 4)); break;
                    case "M090903": TransingStatus.SetStatus(1); var Inp = M090903.querySelectorAll("input"); M0909.STR.EVM090903(Inp[0].value, (Inp[1].value / 100).ToNum(2, 4)); break;
                    case "M090904": TransingStatus.SetStatus(1); var Inp = M090904.querySelectorAll("input"); M0909.STR.EVM090904(Inp[0].value, (Inp[1].value / 100).ToNum(2, 4)); break;
                    case "M090905": TransingStatus.SetStatus(1); var Inp = M090905.querySelectorAll("input"); M0909.STR.EVM090905(Inp[0].value, (Inp[1].value / 100).ToNum(2, 4)); break;
                    case "M090906": TransingStatus.SetStatus(1); var Inp = M090906.querySelectorAll("input"); M0909.STR.EVM090906(Inp[0].value, (Inp[1].value / 100).ToNum(2, 4)); break;
                    case "M090907": TransingStatus.SetStatus(1); var Inp = M090907.querySelectorAll("input"); M0909.STR.EVM090907(Inp[0].value, (Inp[1].value / 100).ToNum(2, 4)); break;
                    case "M090908": TransingStatus.SetStatus(1); var Inp = M090908.querySelectorAll("input"); M0909.STR.EVM090908(Inp[0].value, (Inp[1].value / 100).ToNum(2, 4)); break;
                    default: break;
                }
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
    UIP: { },
    STR: {
        EVINIT: function () {
            ///<summary>页面初始化</summary>
            var Data = [{ C1: "1.0000000", C2: "0.000" }, { C1: "1000.0000", C2: "0.001" }, { C1: "2000.000", C2: "0.002" }, { C1: "3000.000", C2: "0.003" }, { C1: "4000.000", C2: "0.004" }, { C1: "5000.000", C2: "0.005" }, { C1: "6000.000", C2: "0.006" }, { C1: "7000.000", C2: "0.007" }];
            M0909.UID.M090901(Data[0]);
            M0909.UID.M090902(Data[1]);
            M0909.UID.M090903(Data[2]);
            M0909.UID.M090904(Data[3]);
            M0909.UID.M090905(Data[4]);
            M0909.UID.M090906(Data[5]);
            M0909.UID.M090907(Data[6]);
            M0909.UID.M090908(Data[7]);
        },
        EVM090901: function (C1, C2) {
            ///<summary>修改VIP0</summary>
            console.log(C1 + "__" + C2);
            setTimeout("M0909.UID.M090909(true);", 200);
        },
        EVM090902: function (C1, C2) {
            ///<summary>修改VIP1</summary>
            console.log(C1 + "__" + C2);
            setTimeout("M0909.UID.M090910(false);", 200);
        },
        EVM090903: function (C1, C2) {
            ///<summary>修改VIP2</summary>
            console.log(C1 + "__" + C2);
            setTimeout("M0909.UID.M090911(true);", 200);
        },
        EVM090904: function (C1, C2) {
            ///<summary>修改VIP3</summary>
            console.log(C1 + "__" + C2);
            setTimeout("M0909.UID.M090912(true);", 200);
        },
        EVM090905: function (C1, C2) {
            ///<summary>修改VIP4</summary>
            console.log(C1 + "__" + C2);
            setTimeout("M0909.UID.M090913(true);", 200);
        },
        EVM090906: function (C1, C2) {
            ///<summary>修改VIP5</summary>
            console.log(C1 + "__" + C2);
            setTimeout("M0909.UID.M090914(true);", 200);
        },
        EVM090907: function (C1, C2) { 
            ///<summary>修改VIP6</summary>
            console.log(C1 + "__" + C2);
            setTimeout("M0909.UID.M090915(true);", 200);
        },
        EVM090908: function (C1, C2) {
            ///<summary>修改VIP7</summary>
            console.log(C1 + "__" + C2);
            setTimeout("M0909.UID.M090916(true);", 200);
        },
    },
    UID: {
        M090901: function (Data) {
            ///<summary>VIP0</summary>
            M0909.FocusModule = M090901;
            M0909.ShowButton(1);
            M090901.querySelectorAll("input")[0].value = Data.C1;
            M090901.querySelectorAll("input")[1].value = Data.C2 * 100;
            FloatInp(M090901.querySelectorAll("input")[0], 2);
            FloatInp(M090901.querySelectorAll("input")[1], 4);
        },
        M090902: function (Data) {
            ///<summary>VIP1</summary>
            M0909.FocusModule = M090902;
            M0909.ShowButton(1);
            M090902.querySelectorAll("input")[0].value = Data.C1;
            M090902.querySelectorAll("input")[1].value = Data.C2 * 100;
            FloatInp(M090902.querySelectorAll("input")[0], 2);
            FloatInp(M090902.querySelectorAll("input")[1], 4);
        },
        M090903: function (Data) {
            ///<summary>VIP2</summary>
            M0909.FocusModule = M090903;
            M0909.ShowButton(1);
            M090903.querySelectorAll("input")[0].value = Data.C1;
            M090903.querySelectorAll("input")[1].value = Data.C2 * 100;
            FloatInp(M090903.querySelectorAll("input")[0], 2);
            FloatInp(M090903.querySelectorAll("input")[1], 4);
        },
        M090904: function (Data) {
            ///<summary>VIP3</summary>
            M0909.FocusModule = M090904;
            M0909.ShowButton(1);
            M090904.querySelectorAll("input")[0].value = Data.C1;
            M090904.querySelectorAll("input")[1].value = Data.C2 * 100;
            FloatInp(M090904.querySelectorAll("input")[0], 2);
            FloatInp(M090904.querySelectorAll("input")[1], 4);
        },
        M090905: function (Data) {
            ///<summary>VIP4</summary>
            M0909.FocusModule = M090905;
            M0909.ShowButton(1);
            M090905.querySelectorAll("input")[0].value = Data.C1;
            M090905.querySelectorAll("input")[1].value = Data.C2 * 100;
            FloatInp(M090905.querySelectorAll("input")[0], 2);
            FloatInp(M090905.querySelectorAll("input")[1], 4);
        },
        M090906: function (Data) {
            ///<summary>VIP5</summary>
            M0909.FocusModule = M090906;
            M0909.ShowButton(1);
            M090906.querySelectorAll("input")[0].value = Data.C1;
            M090906.querySelectorAll("input")[1].value = Data.C2 * 100;
            FloatInp(M090906.querySelectorAll("input")[0], 2);
            FloatInp(M090906.querySelectorAll("input")[1], 4);
        },
        M090907: function (Data) {
            ///<summary>VIP6</summary>
            M0909.FocusModule = M090907;
            M0909.ShowButton(1);
            M090907.querySelectorAll("input")[0].value = Data.C1;
            M090907.querySelectorAll("input")[1].value = Data.C2 * 100;
            FloatInp(M090907.querySelectorAll("input")[0], 2);
            FloatInp(M090907.querySelectorAll("input")[1], 4);
        },
        M090908: function (Data) {
            ///<summary>VIP7</summary>
            M0909.FocusModule = M090908;
            M0909.ShowButton(1);
            M090908.querySelectorAll("input")[0].value = Data.C1;
            M090908.querySelectorAll("input")[1].value = Data.C2 * 100;
            FloatInp(M090908.querySelectorAll("input")[0], 2);
            FloatInp(M090908.querySelectorAll("input")[1], 4);
        },
        M090909: function (Res) {
            ///<summary>保存VIP0</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
        M090910: function (Res) {
            ///<summary>保存VIP1</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
        M090911: function (Res) {
            ///<summary>保存VIP2</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
        M090912: function (Res) {
            ///<summary>保存VIP3</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
        M090913: function (Res) {
            ///<summary>保存VIP4</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
        M090914: function (Res) {
            ///<summary>保存VIP5</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
        M090915: function (Res) {
            ///<summary>保存VIP6</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
        M090916: function (Res) {
            ///<summary>保存VIP7</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
    }
};