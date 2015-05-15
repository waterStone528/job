// JavaScript source code
var M0707 = {
    FocusButton: null,
    DataMaxLength: 0,
    Init: function () {
        LoadJSM("M07C1", "M0707.STR.EVINIT();", "M07");
    },
    Button: function (Obj, Act) {
        if (this.FocusButton != null) { this.FocusButton.setAttribute("data-slt", "0"); };
        this.FocusButton = Obj; Obj.setAttribute("data-slt", "1"); LoadingBoxCtl(1);
        switch (Act) {
            case 0: M0707.STR.EVM070701(this.DataMaxLength); break;
            case 1: M0707.STR.EVM070702(this.DataMaxLength); break;
            case 2: M0707.STR.EVM070703(this.DataMaxLength); break;
            default: break;
        }
    },
    UIP: {},
    STR: {
        EVINIT: function () {
            ///<summary>页面初始化</summary>
            M0707.UID.M070701();
        },
        EVM070701: function (MaxNum) {
            ///<summary>按天统计</summary><param name="MaxNum">数组可显示的最大数</param>
            var Data = {
                XLabel: ["2014/06/01", "2014/06/02", "2014/06/03", "2014/06/04", "2014/06/05", "2014/06/06", "2014/06/07", "2014/06/08", "2014/06/09", "2014/06/10", "2014/06/11", "2014/06/12", "2014/06/13", "2014/06/14", "2014/06/15", "2014/06/16", "2014/06/17", "2014/06/18", "2014/06/19", "2014/06/20", "2014/06/21", "2014/06/22", "2014/06/23", "2014/06/24", "2014/06/25", "2014/06/26", "2014/06/27", "2014/06/28", "2014/06/29", "2014/06/30"],
                datasets: [
                    { DLabel: "资产发布金额", Dataset: [{ Num: 151 }, { Num: 206 }, { Num: 215 }, { Num: 282 }, { Num: 317 }, { Num: 250 }, { Num: 121 }, { Num: 343 }, { Num: 284 }, { Num: 237 }, { Num: 146 }, { Num: 161 }, { Num: 226 }, { Num: 315 }, { Num: 228 }, { Num: 319 }, { Num: 237 }, { Num: 241 }, { Num: 229 }, { Num: 134 }, { Num: 316 }, { Num: 250 }, { Num: 193 }, { Num: 225 }, { Num: 316 }, { Num: 246 }, { Num: 220 }, { Num: 413 }, { Num: 327 }, { Num: 409 }] },
                    { DLabel: "资产预约金额", Dataset: [{ Num: 221 }, { Num: 143 }, { Num: 323 }, { Num: 237 }, { Num: 246 }, { Num: 161 }, { Num: 226 }, { Num: 310 }, { Num: 220 }, { Num: 315 }, { Num: 319 }, { Num: 237 }, { Num: 128 }, { Num: 219 }, { Num: 137 }, { Num: 141 }, { Num: 229 }, { Num: 234 }, { Num: 316 }, { Num: 225 }, { Num: 316 }, { Num: 146 }, { Num: 220 }, { Num: 150 }, { Num: 321 }, { Num: 243 }, { Num: 323 }, { Num: 237 }, { Num: 141 }, { Num: 154 }] },
                    { DLabel: "资产成交金额", Dataset: [{ Num: 410 }, { Num: 219 }, { Num: 315 }, { Num: 240 }, { Num: 214 }, { Num: 238 }, { Num: 265 }, { Num: 179 }, { Num: 266 }, { Num: 316 }, { Num: 310 }, { Num: 199 }, { Num: 215 }, { Num: 189 }, { Num: 215 }, { Num: 394 }, { Num: 214 }, { Num: 180 }, { Num: 257 }, { Num: 287 }, { Num: 296 }, { Num: 316 }, { Num: 113 }, { Num: 198 }, { Num: 238 }, { Num: 295 }, { Num: 127 }, { Num: 236 }, { Num: 354 }, { Num: 198 }] }
                ]
            };
            M0707.UID.M070702(Data);
        },
        EVM070702: function (MaxNum) {
            ///<summary>按月统计</summary><param name="MaxNum">数组可显示的最大数</param>
            var Data = {
                XLabel: ["2014/01", "2014/02", "2014/03", "2014/04", "2014/05", "2014/06", "2014/07", "2014/08"],
                datasets: [
                    { DLabel: "资产发布金额", Dataset: [{ Num: 2460 }, { Num: 2340 }, { Num: 2413 }, { Num: 2480 }, { Num: 2500 }, { Num: 2389 }, { Num: 2265 }, { Num: 2520 }] },
                    { DLabel: "资产预约金额", Dataset: [{ Num: 2350 }, { Num: 2150 }, { Num: 2359 }, { Num: 2140 }, { Num: 2165 }, { Num: 2273 }, { Num: 2302 }, { Num: 2210 }] },
                    { DLabel: "资产成交金额", Dataset: [{ Num: 1500 }, { Num: 1730 }, { Num: 1300 }, { Num: 1290 }, { Num: 1970 }, { Num: 1130 }, { Num: 1490 }, { Num: 1680 }] }
                ]
            };
            M0707.UID.M070702(Data);
        },
        EVM070703: function (MaxNum) {
            ///<summary>按年统计</summary><param name="MaxNum">数组可显示的最大数</param>
            var Data = {
                XLabel: ["2010", "2011", "2012", "2013", "2014"],
                datasets: [
                    { DLabel: "资产发布金额", Dataset: [{ Num: 123000 }, { Num: 189000 }, { Num: 217060 }, { Num: 235000 }, { Num: 458000 }] },
                    { DLabel: "资产预约金额", Dataset: [{ Num: 54600 }, { Num: 98800 }, { Num: 150700 }, { Num: 257900 }, { Num: 389700 }] },
                    { DLabel: "资产成交金额", Dataset: [{ Num: 23100 }, { Num: 65400 }, { Num: 98700 }, { Num: 123800 }, { Num: 154400 }] }
                ]
            };
            M0707.UID.M070702(Data);
        },
    },
    UID: {
        M070701: function () {
            ///<summary>折线图参数设置</param>
            Chart.CanvasObj = can;
            Chart.Title = "资产金额统计";
            Chart.Unit = "万元";
            can.width = MainZone.clientWidth;
            can.height = MainZone.clientHeight;
            M0707.DataMaxLength = Chart.ChartDefault.DataMaxLength;
            MainZone.querySelector(".DataRangeSW").setAttribute("data-nslt", "1");
        },
        M070702: function (Data) {
            ///<summary>绘图</param>
            Chart.XLabel = Data.XLabel;
            Chart.datasets = Data.datasets;
            Chart.Draw(false);
            MainZone.querySelector(".DataRangeSW").setAttribute("data-nslt", "0");
            LoadingBoxCtl(0);
        },
    },
};

