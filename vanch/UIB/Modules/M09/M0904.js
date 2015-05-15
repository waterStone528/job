// JavaScript source code
var M0904 = {
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
    Button: function (Sender, Act) {
        var BtID = parseInt(Sender.getAttribute("data-btid"));
        this.FocusModule = Sender.parentElement.parentElement;
        ModuleID = Sender.parentElement.parentElement.id;
        switch (BtID) {
            case 1: this.ShowButton(2); break;//Edit Button
            case 2://Save Button
                this.ShowButton(1);
                switch (ModuleID) {
                    case "M090401": TransingStatus.SetStatus(1); var State = M090401.querySelector(".SlideButton").getAttribute("data-sw"); M0904.STR.EVM090401(State); break;
                    default: break;
                }
                break;
            default: break;
        }
    },
    UIP: {},
    STR: {
        EVINIT: function () {
            ///<summary>页面初始化</summary>
            var Data = { C1: "XXXXXXXXXXXX", C2: "正常", C3: "1" };
            M0904.UID.M090401(Data);
        },
        EVM090401: function (C1) {
            ///<summary>修改接口A</summary>
            console.log(C1);
            setTimeout("M0904.UID.M090402(true);", 200);
        },
    },
    UID: {
        M090401: function (Data) {
            ///<summary>接口A</summary>
            M0904.FocusModule = M090401;
            M0904.ShowButton(1);
            M090401.querySelectorAll("td")[1].querySelector("label").DspV = Data.C1;
            M090401.querySelectorAll("td")[3].querySelector("label").DspV = Data.C2;
            M090401.querySelector(".SlideButton").setAttribute("data-sw", Data.C3);
        },
        M090402: function (Res) {
            ///<summary>保存接口A</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
    }
};