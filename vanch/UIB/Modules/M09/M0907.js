var M0907 = {
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
            case 1://Edit Button
                this.ShowButton(2);
                break;
            case 2://Save Button
                this.ShowButton(1);
                switch (ModuleID) {
                    case "M090701": TransingStatus.SetStatus(1); var Inp = M090701.querySelectorAll("input"); this.STR.EVM090701(Inp[0].value, Inp[1].value); break;
                    default:  break;
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
            var Data = { C1: "1.0000000", C2: "2.0000000" };
            M0907.UID.M090701(Data);
        },
        EVM090701: function (C1, C2) {
            ///<summary>修改收费设置</summary>
            console.log(C1 + "__" + C2);
            setTimeout("M0907.UID.M090702(true);", 200);
        },
    },
    UID: {
        M090701: function (Data) {
            ///<summary>收费设置</summary>
            M0907.FocusModule = M090701;
            M0907.ShowButton(1);
            M090701.querySelectorAll("input")[0].value = Data.C1;
            M090701.querySelectorAll("input")[1].value = Data.C2;
            FloatInp(M090701.querySelectorAll("input")[0], 2);
            FloatInp(M090701.querySelectorAll("input")[1], 2);
        },
        M090702: function (Res) { 
            ///<summary>保存收费设置</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
    },
};