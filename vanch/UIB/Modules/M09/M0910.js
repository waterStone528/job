﻿// JavaScript source code
var M0910 = {
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
                    case "M091001": TransingStatus.SetStatus(1); var Inp = M091001.querySelectorAll("input"); this.STR.EVM091001(Inp[0].value);break;
                    case "M091002": TransingStatus.SetStatus(1); var Inp = M091002.querySelectorAll("input"); this.STR.EVM091002(Inp[0].value, Inp[1].value); break;
                    case "M091003": TransingStatus.SetStatus(1); var Inp = M091003.querySelectorAll("input"); this.STR.EVM091003((Inp[0].value / 100).ToNum(2, 4)); break;
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
            var Data = [{ C1: "2.00000" }, { C1: "3.00000", C2: "4.00000" }, { C1: "被购买后", C2: "0.07000" }];
            M0910.UID.M091001(Data[0]);
            M0910.UID.M091002(Data[1]);
            M0910.UID.M091003(Data[2]);
        },
        EVM091001: function (C1) {
            ///<summary>修改参数设置</summary>
            console.log(C1);
            setTimeout(" M0910.UID.M091004(true);", 200);
        },
        EVM091002: function (C1, C2) {
            ///<summary>修改收费设置</summary>
            console.log(C1 + "__" + C2);
            setTimeout(" M0910.UID.M091005(true);", 200);
        },
        EVM091003: function (C1) {
            ///<summary>修改账单设置</summary>
            console.log(C1);
            setTimeout(" M0910.UID.M091006(true);", 200);
        },
    },
    UID: {
        M091001: function (Data) {
            ///<summary>参数设置</summary>
            M0910.FocusModule = M091001;
            M0910.ShowButton(1);
            M091001.querySelectorAll("input")[0].value = Data.C1;
            FloatInp(M091001.querySelectorAll("input")[0], 2);
        },
        M091002: function (Data) {
            ///<summary>收费设置</summary>
            M0910.FocusModule = M091002;
            M0910.ShowButton(1);
            M091002.querySelectorAll("input")[0].value = Data.C1;
            M091002.querySelectorAll("input")[1].value = Data.C2;
            FloatInp(M091002.querySelectorAll("input")[0], 2);
            FloatInp(M091002.querySelectorAll("input")[1], 2);
        },
        M091003: function (Data) {
            ///<summary>账单设置</summary>
            M0910.FocusModule = M091003;
            M0910.ShowButton(1);
            M091003.querySelectorAll("td")[1].DspV = Data.C1;
            M091003.querySelectorAll("td")[3].querySelector("input").value = Data.C2 * 100;
            FloatInp(M091003.querySelectorAll("td")[3].querySelector("input"), 2);
        },
        M091004: function (Res) {
            ///<summary>保存参数设置</summary>
            if(Res){
                TransingStatus.SetStatus(3);
            }else{
                TransingStatus.SetStatus(2);
            }
        },
        M091005: function (Res) {
            ///<summary>保存收费设置</summary>
            if(Res){
                TransingStatus.SetStatus(3);
            }else{
                TransingStatus.SetStatus(2);
            }
        },
        M091006: function (Res) {
            ///<summary>保存账单设置</summary>
            if(Res){
                TransingStatus.SetStatus(3);
            }else{
                TransingStatus.SetStatus(2);
            }
        },
    }
};