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
            //var Data = { C1: "1", C2: "2" };
            //M0907.UID.M090701(Data);

            var busCode = "M0907INIT";
            var data = "busCode=" + busCode;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                var dataFt = M0907.STR.DataBdToFt(resObj);

                M0907.UID.M090701(dataFt);
            }
            ajaxObj.start();
        },
        EVM090701: function (C1, C2) {
            ///<summary>修改收费设置</summary>
            //console.log(C1 + "__" + C2);
            //setTimeout("M0907.UID.M090702(true);", 200);

            var busCode = "M090701";
            var data = "busCode=" + busCode + "&openServerCost=" + C1 + "&assetsReserveCost=" + C2;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function () {
                M0907.UID.M090702(true);
            }
            ajaxObj.error = function () {
                M0907.UID.M090702(false);
            }
            ajaxObj.start();
        },

        //后-前 
        DataBdToFt: function (dataBd) {
            var dataFt = {};

            dataFt.C1 = dataBd.openServerCost == null ? "" : dataBd.openServerCost;
            dataFt.C2 = dataBd.assetsReserveCost == null ? "" : dataBd.assetsReserveCost;

            return dataFt;
        }
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