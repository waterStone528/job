// JavaScript source code
var M0903 = {
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
    ReadOnlyMask: function (ReadOnly) {
        if (ReadOnly == true) { this.FocusModule.querySelector(".ReadOnlyMask").setAttribute('data-mask', 1); }
        if (ReadOnly == false) { this.FocusModule.querySelector(".ReadOnlyMask").setAttribute('data-mask', 0); }
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
                    case "M090301": TransingStatus.SetStatus(1); var Inp = M090301.querySelectorAll("input"); this.STR.EVM090301(Inp[0].value); break;
                    default: break;
                }
                break;
            default:
                break;
        }
    },
    UIP: {},
    STR: {
        EVINIT: function () {
            ///<summary>页面初始化</summary>
            var C1 = 500.1511515;
            M0903.UID.M090301(C1);
        },
        EVM090301: function (C1) {
            ///<summary>修改收费设置</summary>
            console.log(C1);
            setTimeout("M0903.UID.M090302(false);", 200);
        }
    },
    UID: {
        M090301: function (V) {
            ///<summary>收费设置</summary>
            M0903.FocusModule = M090301;
            M0903.ShowButton(1);
            M090301.querySelectorAll("input")[0].value = V;
            FloatInp(M090301.querySelectorAll("input")[0], 2);
        },
        M090302: function (Res) {
            ///<summary>保存收费设置</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            }
            else {
                TransingStatus.SetStatus(2);
            }
        }
    }
};