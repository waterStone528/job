// JavaScript source code
var M0701 = {
    FocusButton: null,
    DataMaxLength: 0,
    Init: function () {
        LoadJSM("M07C1", "M0701.STR.EVINIT();", "M07");
    },
    Button: function (Obj, Act) {
        if (this.FocusButton != null) { this.FocusButton.setAttribute("data-slt", "0"); };
        this.FocusButton = Obj; Obj.setAttribute("data-slt", "1"); LoadingBoxCtl(1);
        switch (Act) {
            case 0: M0701.STR.EVM070101(this.DataMaxLength); break;
            case 1: M0701.STR.EVM070102(this.DataMaxLength); break;
            case 2: M0701.STR.EVM070103(this.DataMaxLength); break;
            default: break;
        }
    },
    UIP: {},
    STR: {
        EVINIT: function () {
            ///<summary>页面初始化</summary>
            M0701.UID.M070101();
        },
        EVM070101: function (MaxNum) {
            ///<summary>按天统计</summary><param name="MaxNum">数组可显示的最大数</param>
            var Data = {
                XLabel: ["2014/06/01", "2014/06/02", "2014/06/03", "2014/06/04", "2014/06/05", "2014/06/06", "2014/06/07", "2014/06/08", "2014/06/09", "2014/06/10", "2014/06/11", "2014/06/12", "2014/06/13", "2014/06/14", "2014/06/15", "2014/06/16", "2014/06/17", "2014/06/18", "2014/06/19", "2014/06/20", "2014/06/21", "2014/06/22", "2014/06/23", "2014/06/24", "2014/06/25", "2014/06/26", "2014/06/27", "2014/06/28", "2014/06/29", "2014/06/30"],
                datasets: [
                    { DLabel: "访问量", Dataset: [{ Num: 1000 }, { Num: 1100 }, { Num: 1200 }, { Num: 1300 }, { Num: 1400 }, { Num: 1500 }, { Num: 1600 }, { Num: 1700 }, { Num: 1800 }, { Num: 1900 }, { Num: 2000 }, { Num: 2100 }, { Num: 2200 }, { Num: 2300 }, { Num: 2400 }, { Num: 2500 }, { Num: 2600 }, { Num: 2700 }, { Num: 2800 }, { Num: 2900 }, { Num: 3000 }, { Num: 3000 }, { Num: 3000 }, { Num: 3000 }, { Num: 3000 }, { Num: 3000 }, { Num: 3000 }, { Num: 3000 }, { Num: 3000 }, { Num: 3000 }] },
                    { DLabel: "注册数", Dataset: [{ Num: 552 }, { Num: 742 }, { Num: 1004 }, { Num: 660 }, { Num: 1271 }, { Num: 38 }, { Num: 1083 }, { Num: 747 }, { Num: 1234 }, { Num: 1034 }, { Num: 1458 }, { Num: 755 }, { Num: 303 }, { Num: 1301 }, { Num: 1281 }, { Num: 868 }, { Num: 407 }, { Num: 868 }, { Num: 1512 }, { Num: 676 }, { Num: 408 }, { Num: 1104 }, { Num: 1206 }, { Num: 366 }, { Num: 1107 }, { Num: 214 }, { Num: 777 }, { Num: 891 }, { Num: 867 }, { Num: 1375 }] }
                ]
            };
            M0701.UID.M070102(Data);
        },
        EVM070102: function (MaxNum) {
            ///<summary>按月统计</summary><param name="MaxNum">数组可显示的最大数</param>
            var Data = {
                XLabel: ["2013/06", "2013/07", "2013/08", "2013/09", "2013/10", "2013/11", "2013/12", "2014/01", "2014/02", "2014/03", "2014/04", "2014/05", "2014/06", "2014/07", "2014/08"],
                datasets: [
                    { DLabel: "访问量", Dataset: [{ Num: 30000 }, { Num: 33000 }, { Num: 36000 }, { Num: 39000 }, { Num: 42000 }, { Num: 45000 }, { Num: 48000 }, { Num: 30000 }, { Num: 33000 }, { Num: 36000 }, { Num: 39000 }, { Num: 42000 }, { Num: 45000 }, { Num: 48000 }, {Num:30000}] },
                    { DLabel: "注册数", Dataset: [{ Num: 16560 }, { Num: 22260 }, { Num: 30120 }, { Num: 19800 }, { Num: 38130 }, { Num: 1140 }, { Num: 32490 }, { Num: 22410 }, { Num: 37020 }, { Num: 31020 }, { Num: 43740 }, { Num: 22650 }, { Num: 39030 }, { Num: 38430 }, { Num: 26040 }] }
                ]
            };
            M0701.UID.M070102(Data);
        },
        EVM070103: function (MaxNum) {
            ///<summary>按年统计</summary><param name="MaxNum">数组可显示的最大数</param>
            var Data = {
                XLabel: ["2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024"],
                datasets: [
                    { DLabel: "访问量", Dataset: [{ Num: 360000 }, { Num: 396000 }, { Num: 432000 }, { Num: 468000 }, { Num: 504000 }, { Num: 540000 }, { Num: 576000 }, { Num: 360000 }, { Num: 396000 }, { Num: 432000 }, { Num: 468000 }, { Num: 504000 }, { Num: 540000 }, { Num: 576000 }, { Num: 360000 }] },
                    { DLabel: "注册数", Dataset: [{ Num: 198720 }, { Num: 267120 }, { Num: 361440 }, { Num: 237600 }, { Num: 457560 }, { Num: 13680 }, { Num: 389880 }, { Num: 268920 }, { Num: 444240 }, { Num: 372240 }, { Num: 524880 }, { Num: 271800 }, { Num: 468360 }, { Num: 461160 }, { Num: 312480 }] }
                ]
            };
            M0701.UID.M070102(Data);
        },

    },
    UID: {
        M070101: function () {
            ///<summary>折线图参数设置</param>
            Chart.CanvasObj = can;
            Chart.Title = "注册统计";
            Chart.Unit = "人";
            can.width = MainZone.clientWidth;
            can.height = MainZone.clientHeight;
            M0701.DataMaxLength = Chart.ChartDefault.DataMaxLength;
            MainZone.querySelector(".DataRangeSW").setAttribute("data-nslt", "1");
        },
        M070102: function (Data) {
            ///<summary>绘图</param>
            Chart.XLabel = Data.XLabel;
            Chart.datasets = Data.datasets;
            Chart.Draw(false);
            MainZone.querySelector(".DataRangeSW").setAttribute("data-nslt", "0");
            LoadingBoxCtl(0);
        },
    },
};

