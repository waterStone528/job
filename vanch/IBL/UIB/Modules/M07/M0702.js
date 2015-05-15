// JavaScript source code
var M0702 = {
    FocusButton: null,
    DataMaxLength: 0,
    Init: function () {
        LoadJSM("M07C1", "M0702.STR.EVINIT();", "M07");
    },
    Button: function (Obj, Act) {
        if (this.FocusButton != null) { this.FocusButton.setAttribute("data-slt", "0"); };
        this.FocusButton = Obj; Obj.setAttribute("data-slt", "1"); LoadingBoxCtl(1);
        switch (Act) {
            case 0: M0702.STR.EVM070201(this.DataMaxLength); break;
            case 1: M0702.STR.EVM070202(this.DataMaxLength); break;
            case 2: M0702.STR.EVM070203(this.DataMaxLength); break;
            default: break;
        }
    },
    UIP: {},
    STR: {
        EVINIT: function () {
            ///<summary>页面初始化</summary>
            M0702.UID.M070201();
        },
        EVM070201: function (MaxNum) {
            ///<summary>按天统计</summary><param name="MaxNum">数组可显示的最大数</param>
            var Data = {
                XLabel: ["2014/06/01", "2014/06/02", "2014/06/03", "2014/06/04", "2014/06/05", "2014/06/06", "2014/06/07", "2014/06/08", "2014/06/09", "2014/06/10", "2014/06/11", "2014/06/12", "2014/06/13", "2014/06/14", "2014/06/15", "2014/06/16", "2014/06/17", "2014/06/18", "2014/06/19", "2014/06/20", "2014/06/21", "2014/06/22", "2014/06/23", "2014/06/24", "2014/06/25", "2014/06/26", "2014/06/27", "2014/06/28", "2014/06/29", "2014/06/30"],
                datasets: [
                    { DLabel: "债权发布数量", Dataset: [{ Num: 10 }, { Num: 20 }, { Num: 15 }, { Num: 12 }, { Num: 7 }, { Num: 50 }, { Num: 21 }, { Num: 43 }, { Num: 23 }, { Num: 37 }, { Num: 46 }, { Num: 61 }, { Num: 26 }, { Num: 15 }, { Num: 28 }, { Num: 19 }, { Num: 37 }, { Num: 41 }, { Num: 29 }, { Num: 34 }, { Num: 16 }, { Num: 50 }, { Num: 33 }, { Num: 25 }, { Num: 16 }, { Num: 46 }, { Num: 20 }, { Num: 53 }, { Num: 27 }, { Num: 9 }] },
                    { DLabel: "债权预约数量", Dataset: [{ Num: 21 }, { Num: 43 }, { Num: 23 }, { Num: 37 }, { Num: 46 }, { Num: 61 }, { Num: 26 }, { Num: 10 }, { Num: 20 }, { Num: 15 }, { Num: 19 }, { Num: 37 }, { Num: 28 }, { Num: 19 }, { Num: 37 }, { Num: 41 }, { Num: 29 }, { Num: 34 }, { Num: 16 }, { Num: 25 }, { Num: 16 }, { Num: 46 }, { Num: 20 }, { Num: 50 }, { Num: 21 }, { Num: 43 }, { Num: 23 }, { Num: 37 }, { Num: 41 }, { Num: 54 }] },
                    { DLabel: "债权成交数量", Dataset: [{ Num: 10 }, { Num: 9 }, { Num: 5 }, { Num: 4 }, { Num: 14 }, { Num: 8 }, { Num: 5 }, { Num: 7 }, { Num: 6 }, { Num: 16 }, { Num: 10 }, { Num: 9 }, { Num: 5 }, { Num: 9 }, { Num: 5 }, { Num: 4 }, { Num: 14 }, { Num: 8 }, { Num: 5 }, { Num: 7 }, { Num: 6 }, { Num: 16 }, { Num: 13 }, { Num: 8 }, { Num: 8 }, { Num: 5 }, { Num: 7 }, { Num: 12 }, { Num: 5 }, { Num: 9 }] },
                    { DLabel: "债权还款数量", Dataset: [{ Num: 5 }, { Num: 4 }, { Num: 14 }, { Num: 8 }, { Num: 5 }, { Num: 7 }, { Num: 6 }, { Num: 16 }, { Num: 13 }, { Num: 8 }, { Num: 5 }, { Num: 10 }, { Num: 9 }, { Num: 10 }, { Num: 9 }, { Num: 5 }, { Num: 4 }, { Num: 14 }, { Num: 8 }, { Num: 5 }, { Num: 7 }, { Num: 6 }, { Num: 16 }, { Num: 13 }, { Num: 8 }, { Num: 10 }, { Num: 8 }, { Num: 11 }, { Num: 3 }, { Num: 7 }] },
                    { DLabel: "债权逾期数量", Dataset: [{ Num: 2 }, { Num: 3 }, { Num: 6 }, { Num: 4 }, { Num: 1 }, { Num: 0 }, { Num: 0 }, { Num: 2 }, { Num: 1 }, { Num: 5 }, { Num: 1 }, { Num: 6 }, { Num: 3 }, { Num: 6 }, { Num: 3 }, { Num: 2 }, { Num: 8 }, { Num: 10 }, { Num: 1 }, { Num: 4 }, { Num: 3 }, { Num: 7 }, { Num: 2 }, { Num: 8 }, { Num: 9 }, { Num: 10 }, { Num: 5 }, { Num: 2 }, { Num: 4 }, { Num: 3 }] },
                ]
            };
            M0702.UID.M070202(Data);
        },
        EVM070202: function (MaxNum) {
            ///<summary>按月统计</summary><param name="MaxNum">数组可显示的最大数</param>
            var Data = {
                XLabel: ["2014/01", "2014/02", "2014/03", "2014/04", "2014/05", "2014/06", "2014/07", "2014/08"],
                datasets: [
                    { DLabel: "债权发布数量", Dataset: [{ Num: 460 }, { Num: 340 }, { Num: 413 }, { Num: 480 }, { Num: 500 }, { Num: 389 }, { Num: 265 }, { Num: 520 }] },
                    { DLabel: "债权预约数量", Dataset: [{ Num: 350 }, { Num: 150 }, { Num: 359 }, { Num: 140 }, { Num: 165 }, { Num: 273 }, { Num: 302 }, { Num: 210 }] },
                    { DLabel: "债权成交数量", Dataset: [{ Num: 50 }, { Num: 73 }, { Num: 30 }, { Num: 29 }, { Num: 97 }, { Num: 13 }, { Num: 49 }, { Num: 68 }] },
                    { DLabel: "债权还款数量", Dataset: [{ Num: 205 }, { Num: 314 }, { Num: 138 }, { Num: 184 }, { Num: 209 }, { Num: 219 }, { Num: 170 }, { Num: 163 }] },
                    { DLabel: "债权逾期数量", Dataset: [{ Num: 170 }, { Num: 163 }, { Num: 279 }, { Num: 149 }, { Num: 276 }, { Num: 233 }, { Num: 175 }, { Num: 137 }] },
                ]
            };
            M0702.UID.M070202(Data);
        },
        EVM070203: function (MaxNum) {
            ///<summary>按年统计</summary><param name="MaxNum">数组可显示的最大数</param>
            var Data = {
                XLabel: ["2010", "2011", "2012", "2013", "2014"],
                datasets: [
                    { DLabel: "债权发布数量", Dataset: [{ Num: 1230 }, { Num: 1890 }, { Num: 2176 }, { Num: 2350 }, { Num: 4580 }] },
                    { DLabel: "债权预约数量", Dataset: [{ Num: 546 }, { Num: 988 }, { Num: 1507 }, { Num: 2579 }, { Num: 3897 }] },
                    { DLabel: "债权成交数量", Dataset: [{ Num: 231 }, { Num: 654 }, { Num: 987 }, { Num: 1238 }, { Num: 1544 }] },
                    { DLabel: "债权还款数量", Dataset: [{ Num: 648 }, { Num: 795 }, { Num: 945 }, { Num: 1347 }, { Num: 1580 }] },
                    { DLabel: "债权逾期数量", Dataset: [{ Num: 502 }, { Num: 853 }, { Num: 961 }, { Num: 1312 }, { Num: 1654 }] }
                ]
            };
            M0702.UID.M070202(Data);
        },

    },
    UID: {
        M070201: function () {
            ///<summary>折线图参数设置</param>
            Chart.CanvasObj = can;
            Chart.Title = "债权数量统计";
            Chart.Unit = "个";
            can.width = MainZone.clientWidth;
            can.height = MainZone.clientHeight;
            M0702.DataMaxLength = Chart.ChartDefault.DataMaxLength;
            MainZone.querySelector(".DataRangeSW").setAttribute("data-nslt", "1");
        },
        M070202: function (Data) {
            ///<summary>绘图</param>
            Chart.XLabel = Data.XLabel;
            Chart.datasets = Data.datasets;
            Chart.Draw(false);
            MainZone.querySelector(".DataRangeSW").setAttribute("data-nslt", "0");
            LoadingBoxCtl(0);
        },
    },
};

