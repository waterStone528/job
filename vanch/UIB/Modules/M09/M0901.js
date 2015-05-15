// JavaScript source code
var M0901 = {
    FocusModule: null,
    Init: function () {
        this.STR.EVINIT();
    },
    SetDay: function () {
        M090103.querySelectorAll("input[data-type='p']")[0].value = parseInt(M090103.querySelectorAll("input[data-type='p']")[0].value) + 1;
    },
    Button: function (Sender) {
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
                    case "M090101": TransingStatus.SetStatus(1); var Inp = M090101.querySelectorAll("input"); M0901.STR.EVM090101(Inp[0].value, Inp[1].value, Inp[2].value); break;
                    case "M090102": TransingStatus.SetStatus(1); var Inp = M090102.querySelectorAll("input"); M0901.STR.EVM090102(Inp[0].value, Inp[1].value, Inp[2].value); break;
                    case "M090103": TransingStatus.SetStatus(1); var Inp = M090103.querySelectorAll("input"); M0901.STR.EVM090103((Inp[0].value / 100).ToNum(2, 4), (Inp[1].value / 100).ToNum(2, 4), (Inp[2].value / 100).ToNum(2, 4)); break;
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
            var Data = [{ C1: "1.0000000", C2: "2.0000000", C3: "3.0000000" }, { C1: "4.0000000", C2: "3.0000000", C3: "2.0000000" }, { C1: "被投资后", C2: "0.00300000", C3: "0.02000000", C4: "0.01000000" }];
            M0901.UID.M090101(Data[0]);
            M0901.UID.M090102(Data[1]);
            M0901.UID.M090103(Data[2]);
        },
        EVM090101: function (C1, C2, C3) {
            ///<summary>修改参数设置</summary>
            console.log(C1 + "__" + C2 + "__" + C3);
            setTimeout("M0901.UID.M090104(true);", 200);
        },
        EVM090102: function (C1, C2, C3) {
            ///<summary>修改收费设置</summary>
            console.log(C1 + "__" + C2 + "__" + C3);
            setTimeout("M0901.UID.M090105(false);", 200);
        },
        EVM090103: function (C1, C2, C3) { 
            ///<summary>修改账单设置</summary>
            console.log(C1 + "__" + C2 + "__" + C3);
            setTimeout("M0901.UID.M090106(true);", 200);
        },
    },
    UID:{
        M090101: function (Data) {
            ///<summary>参数设置</summary>
            M0901.FocusModule = M090101;
            M0901.ShowButton(1);
            M090101.querySelectorAll("input")[0].value = Data.C1;
            M090101.querySelectorAll("input")[1].value = Data.C2;
            M090101.querySelectorAll("input")[2].value = Data.C3;
            FloatInp(M090101.querySelectorAll("input")[0], 0);
            FloatInp(M090101.querySelectorAll("input")[1], 0);
            FloatInp(M090101.querySelectorAll("input")[2], 2);
        },
        M090102: function (Data) {
            ///<summary>收费设置</summary>
            M0901.FocusModule = M090102;
            M0901.ShowButton(1);
            M090102.querySelectorAll("input")[0].value = Data.C1;
            M090102.querySelectorAll("input")[1].value = Data.C2;
            M090102.querySelectorAll("input")[2].value = Data.C3;
            FloatInp(M090102.querySelectorAll("input")[0], 2);
            FloatInp(M090102.querySelectorAll("input")[1], 2);
            FloatInp(M090102.querySelectorAll("input")[2], 2);
        },
        M090103: function (Data) {
            ///<summary>账单设置</summary>
            M0901.FocusModule = M090103;
            M0901.ShowButton(1);
            M090103.querySelectorAll("td")[1].DspV = Data.C1;
            M090103.querySelectorAll("td")[3].querySelector("input").value = Data.C2 * 100;
            M090103.querySelectorAll("td")[5].querySelector("input").value = Data.C3 * 100;
            M090103.querySelectorAll("td")[7].querySelector("input").value = Data.C4 * 100;
            FloatInp(M090103.querySelectorAll("td")[3].querySelector("input"), 2);
            FloatInp(M090103.querySelectorAll("td")[5].querySelector("input"), 2);
            FloatInp(M090103.querySelectorAll("td")[7].querySelector("input"), 2);
        },
        M090104: function (Res) {
            ///<summary>保存参数设置</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            }
            else {
                TransingStatus.SetStatus(2);
            }
        },
        M090105: function (Res) { 
            ///<summary>保存收费设置</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            }
            else {
                TransingStatus.SetStatus(2);
            }
        },
        M090106: function (Res) { 
            ///<summary>保存账单设置</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            }
            else {
                TransingStatus.SetStatus(2);
            }
        }
    }
};