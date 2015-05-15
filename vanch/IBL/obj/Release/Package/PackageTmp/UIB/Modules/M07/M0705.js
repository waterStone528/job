// JavaScript source code
var M0705 = {
    FocusButton: null,
    DataMaxLength: 0,
    Init: function () {
        LoadJSM("M07C1", "M0705.STR.EVINIT();", "M07");
    },
    Button: function (Obj, Act) {
        if (this.FocusButton != null) { this.FocusButton.setAttribute("data-slt", "0"); };
        this.FocusButton = Obj; Obj.setAttribute("data-slt", "1"); LoadingBoxCtl(1);
        switch (Act) {
            case 0: M0705.STR.EVM070501(this.DataMaxLength); break;
            case 1: M0705.STR.EVM070502(this.DataMaxLength); break;
            case 2: M0705.STR.EVM070503(this.DataMaxLength); break;
            default: break;
        }
    },
    UIP: {},
    STR: {
        EVINIT: function () {
            ///<summary>页面初始化</summary>
            M0705.UID.M070501();
        },
        EVM070501: function (MaxNum) {
            ///<summary>按天统计</summary><param name="MaxNum">数组可显示的最大数</param>
            var Data = {
                XLabel: ["2014/06/01", "2014/06/02", "2014/06/03", "2014/06/04", "2014/06/05", "2014/06/06", "2014/06/07", "2014/06/08", "2014/06/09", "2014/06/10", "2014/06/11", "2014/06/12", "2014/06/13", "2014/06/14", "2014/06/15", "2014/06/16", "2014/06/17", "2014/06/18", "2014/06/19", "2014/06/20", "2014/06/21", "2014/06/22", "2014/06/23", "2014/06/24", "2014/06/25", "2014/06/26", "2014/06/27", "2014/06/28", "2014/06/29", "2014/06/30"],
                datasets: [
                    { DLabel: "消费合计", Dataset: [{ Num: 551 }, { Num: 406 }, { Num: 415 }, { Num: 482 }, { Num: 317 }, { Num: 450 }, { Num: 521 }, { Num: 543 }, { Num: 484 }, { Num: 437 }, { Num: 546 }, { Num: 561 }, { Num: 426 }, { Num: 515 }, { Num: 428 }, { Num: 519 }, { Num: 437 }, { Num: 441 }, { Num: 429 }, { Num: 534 }, { Num: 516 }, { Num: 450 }, { Num: 593 }, { Num: 425 }, { Num: 516 }, { Num: 446 }, { Num: 420 }, { Num: 413 }, { Num: 527 }, { Num: 409 }] },
                    { DLabel: "应收合计", Dataset: [{ Num: 221 }, { Num: 343 }, { Num: 323 }, { Num: 237 }, { Num: 246 }, { Num: 361 }, { Num: 226 }, { Num: 310 }, { Num: 220 }, { Num: 315 }, { Num: 319 }, { Num: 237 }, { Num: 328 }, { Num: 219 }, { Num: 337 }, { Num: 341 }, { Num: 229 }, { Num: 234 }, { Num: 316 }, { Num: 225 }, { Num: 316 }, { Num: 346 }, { Num: 220 }, { Num:350 }, { Num: 321 }, { Num: 243 }, { Num: 323 }, { Num: 237 }, { Num: 341 }, { Num: 354 }] },
                    { DLabel: "逾期合计", Dataset: [{ Num: 110 }, { Num: 219 }, { Num: 115 }, { Num: 240 }, { Num: 214 }, { Num: 238 }, { Num: 265 }, { Num: 179 }, { Num: 266 }, { Num: 116 }, { Num: 310 }, { Num: 199 }, { Num: 215 }, { Num: 189 }, { Num: 215 }, { Num:194 }, { Num: 214 }, { Num: 180 }, { Num: 257 }, { Num: 287 }, { Num: 296 }, { Num: 116 }, { Num: 113 }, { Num: 198 }, { Num: 238 }, { Num: 295 }, { Num: 127 }, { Num: 236 }, { Num: 154 }, { Num: 198 }] }
                ]
            };
            M0705.UID.M070502(Data);
        },
        EVM070502: function (MaxNum) {
            ///<summary>按月统计</summary><param name="MaxNum">数组可显示的最大数</param>
            var Data = {
                XLabel: ["2014/01", "2014/02", "2014/03", "2014/04", "2014/05", "2014/06", "2014/07", "2014/08"],
                datasets: [
                    { DLabel: "消费合计", Dataset: [{ Num: 4460 }, { Num: 5340 }, { Num: 4413 }, { Num: 4480 }, { Num: 4500 }, { Num: 5389 }, { Num: 5265 }, { Num: 4520 }] },
                    { DLabel: "应收合计", Dataset: [{ Num: 3350 }, { Num: 3150 }, { Num: 3359 }, { Num: 3140 }, { Num: 3165 }, { Num: 3273 }, { Num: 3302 }, { Num: 3210 }] },
                    { DLabel: "逾期合计", Dataset: [{ Num: 1500 }, { Num: 1730 }, { Num: 1300 }, { Num: 1290 }, { Num: 1970 }, { Num: 1130 }, { Num: 1490 }, { Num: 1680 }] }
                ]
            };
            M0705.UID.M070502(Data);
        },
        EVM070503: function (MaxNum) {
            ///<summary>按年统计</summary><param name="MaxNum">数组可显示的最大数</param>
            var Data = {
                XLabel: ["2010", "2011", "2012", "2013", "2014"],
                datasets: [
                    { DLabel: "消费合计", Dataset: [{ Num: 143000 }, { Num: 189000 }, { Num: 217060 }, { Num: 235000 }, { Num: 358000 }] },
                    { DLabel: "应收合计", Dataset: [{ Num: 54600 }, { Num: 98800 }, { Num: 50700 }, { Num: 57900 }, { Num: 89700 }] },
                    { DLabel: "逾期合计", Dataset: [{ Num: 23100 }, { Num: 65400 }, { Num: 48700 }, { Num: 23800 }, { Num: 54400 }] }
                ]
            };
            M0705.UID.M070502(Data);
        },
    },
    UID: {
        M070501: function () {
            ///<summary>折线图参数设置</param>
            Chart.CanvasObj = can;
            Chart.Title = "资产金额统计";
            Chart.Unit = "万元";
            can.width = MainZone.clientWidth;
            can.height = MainZone.clientHeight;
            M0705.DataMaxLength = Chart.ChartDefault.DataMaxLength;
            MainZone.querySelector(".DataRangeSW").setAttribute("data-nslt", "1");
        },
        M070502: function (Data) {
            ///<summary>绘图</param>
            Chart.XLabel = Data.XLabel;
            Chart.datasets = Data.datasets;
            Chart.Draw(false);
            MainZone.querySelector(".DataRangeSW").setAttribute("data-nslt", "0");
            LoadingBoxCtl(0);
        },
    },
};

