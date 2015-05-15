// JavaScript source code
var Chart = {
    Title: "",
    Unit: "",
    CanvasObj: null,
    XLabel: [""],
    datasets: [{ DLabel: "", Dataset: [] }, { DLabel: "", Dataset: [] }, { DLabel: "", Dataset: [] }, { DLabel: "", Dataset: [] }, { DLabel: "", Dataset: [] }, { DLabel: "", Dataset: [] }],
    ChartDefault: {
        TextFont: "12px Arial",
        FontColor: "rgba(255,255,255,0.5)",
        X_LineOffset: 20,
        StarPoint_X: 50,
        StarPoint_Y: 85,
        OffsetH: 80,
        DataMaxLength: 50,
        PolylineWidth: 1,
        PolylineColor: ["rgba(255, 255, 255, 1)", "rgba(232, 84, 192, 1)", "rgba(150, 255, 99, 1)", "rgba(0, 255, 240, 1)", "rgba(220, 102, 66, 1)", "rgba(255, 0, 0, 1)"], // 设置折线的颜色
        IsShowNum: true,
        Step: 0
    },
    TempVar: {
        MaxValue: 0,
        MinValue: 0,
        hasMaxMinValue: false
    },
    MISC: function () {
        Chart.ChartDefault.CW = Chart.CanvasObj.width - Chart.ChartDefault.StarPoint_X + 40;
        Chart.ChartDefault.CH = Chart.CanvasObj.height - Chart.ChartDefault.OffsetH - Chart.ChartDefault.StarPoint_Y;

        var ctx = Chart.CanvasObj.getContext("2d");
        var TLeng = Chart.Title.length * 10
        ctx.beginPath();

        ctx.clearRect(0, 0, Chart.CanvasObj.width, Chart.CanvasObj.height);     //Canvas初始化
        ctx.moveTo(Chart.ChartDefault.StarPoint_X, Chart.ChartDefault.CH + Chart.ChartDefault.StarPoint_Y);
        ctx.lineTo(Chart.ChartDefault.CW, Chart.ChartDefault.CH + Chart.ChartDefault.StarPoint_Y + 0.5);        //x坐标

        ctx.font = "Bold 22px Arial";
        ctx.fillStyle = "white";
        ctx.fillText(Chart.Title, Chart.ChartDefault.CW / 2 - TLeng, Chart.ChartDefault.StarPoint_Y - 55);

        ctx.font = "Bold 12px Arial";
        ctx.fillText("单位：" + Chart.Unit, 0, Chart.ChartDefault.StarPoint_Y - 30);

        ctx.strokeStyle = "rgba(255,255,255,0.5)";
        ctx.stroke();

        Chart.CanvasObj.setAttribute("onmouseup", "Chart.CM();")
    },//基本框架
    X_Line: function () {
        ///<summary>设置网格</summary>
        ///<param name="Max">最大值</param>
        var ctx = Chart.CanvasObj.getContext("2d");
        var nowheight = 0;
        var nowwidth = 0;
        var nowNum
        var AVG = (Chart.GetMaxMin("Max") - Chart.GetMaxMin("Min")) / Chart.ChartDefault.X_LineOffset;
        ctx.beginPath();
        ctx.lineWidth = 1;
        ctx.fillStyle = "rgba(255,255,255,0.5)";
        ctx.strokeStyle = "rgba(255,255,255,0.2)";
        for (var i = 0; i < Chart.ChartDefault.X_LineOffset; i++) {
            nowheight = ((Chart.ChartDefault.CH) * i / Chart.ChartDefault.X_LineOffset) + Chart.ChartDefault.StarPoint_Y;
            nowNum = Chart.GetMaxMin("Max") - AVG * i;
            ctx.moveTo(~~(Chart.ChartDefault.StarPoint_X), ~~(nowheight) + 0.5);
            ctx.lineTo(~~(Chart.ChartDefault.CW), ~~(nowheight) + 0.5);
            ctx.fillText(parseInt(nowNum), ~~(Chart.ChartDefault.StarPoint_X - 50), ~~(nowheight));
        }
        ctx.stroke();

    },//网格
    Polyline: function (Data) {
        ///<summary>绘制折线图</summary>
        var ctx = Chart.CanvasObj.getContext("2d");
        ctx.font = Chart.ChartDefault.TextFont;
        ctx.beginPath(); // 开始路径绘制折线
        var firstone = true;
        for (var i in Data) {
            if (Data[i].Num == null) { continue; }
            nowwidth = ((Chart.ChartDefault.CW - Chart.ChartDefault.StarPoint_X) * i / this.XLabel.length) + 20;
            nowheight = (Chart.GetMaxMin("Max") - Data[i].Num) / (Chart.GetMaxMin("Max") - Chart.GetMaxMin("Min")) * Chart.ChartDefault.CH + Chart.ChartDefault.StarPoint_Y;
            if (firstone) { ctx.moveTo(~~(nowwidth + Chart.ChartDefault.StarPoint_X), nowheight); firstone = false; } else { ctx.lineTo(~~(nowwidth + Chart.ChartDefault.StarPoint_X), nowheight); }
        }
        ctx.lineWidth = Chart.ChartDefault.PolylineWidth; // 设置线宽
        ctx.strokeStyle = Chart.ChartDefault.PolylineColor[this.ChartDefault.Step];
        ctx.stroke(); // 进行线的着色，这时整条线才变得

    },
    DataLabel: function (Name) {
        ///<summary>数组名称</summary>
        var ctx = Chart.CanvasObj.getContext("2d");
        ctx.beginPath();
        ctx.font = "Bold 13px Arial";
        ctx.fillStyle = Chart.ChartDefault.PolylineColor[this.ChartDefault.Step];
        ctx.moveTo((Chart.ChartDefault.CW - 140) - 170 * (this.ChartDefault.Step % 2), Chart.ChartDefault.StarPoint_Y - 35 - 20 * ~~(this.ChartDefault.Step / 2) + 7.5);
        ctx.lineTo((Chart.ChartDefault.CW - 90) - 170 * (this.ChartDefault.Step % 2), Chart.ChartDefault.StarPoint_Y - 35 - 20 * ~~(this.ChartDefault.Step / 2) + 7.5);
        ctx.fillText(Name, (Chart.ChartDefault.CW - 80) - 170 * (this.ChartDefault.Step % 2), Chart.ChartDefault.StarPoint_Y - 35 - 20 * ~~(this.ChartDefault.Step / 2) + 12);
        ctx.strokeStyle = Chart.ChartDefault.PolylineColor[this.ChartDefault.Step];
        ctx.stroke();
    },
    Dotted: function (Data) {
        ///<summary>绘制圆点</summary>
        var ctx = Chart.CanvasObj.getContext("2d");
        ctx.fillStyle = Chart.ChartDefault.PolylineColor[this.ChartDefault.Step];
        for (var i in Data) {
            if (Data[i].Num != null) {
                ctx.beginPath(); // 开始路径绘制圆点
                nowheight = (Chart.GetMaxMin("Max") - Data[i].Num) / (Chart.GetMaxMin("Max") - Chart.GetMaxMin("Min")) * Chart.ChartDefault.CH + Chart.ChartDefault.StarPoint_Y;
                nowwidth = ((Chart.ChartDefault.CW - Chart.ChartDefault.StarPoint_X) * i / this.XLabel.length) + 20;
                ctx.arc(~~(nowwidth + Chart.ChartDefault.StarPoint_X), nowheight, Chart.ChartDefault.PolylineWidth + 2, 0, Math.PI * 2, false);
                ctx.fill();
            }
        }
    },
    ToolTipA: function (Date) {
        ///<summary>显示点的数据</summary>
        var ctx = Chart.CanvasObj.getContext("2d");
        ctx.font = "13px Arial";
        for (var i in Date) {
            if (Date[i].Num != null) {
                nowheight = (Chart.GetMaxMin("Max") - Date[i].Num) / (Chart.GetMaxMin("Max") - Chart.GetMaxMin("Min")) * Chart.ChartDefault.CH + Chart.ChartDefault.StarPoint_Y;
                nowwidth = ((Chart.ChartDefault.CW - Chart.ChartDefault.StarPoint_X) * i / this.XLabel.length) + 20;
                ctx.fillStyle = "rgba(0,0,0,0.3)";
                ctx.fillRect(~~(nowwidth + Chart.ChartDefault.StarPoint_X), nowheight - 18, (Date[i].Num + "").length * 8, 15);
                ctx.fillStyle = Chart.ChartDefault.PolylineColor[Chart.ChartDefault.Step];
                ctx.fillText(Date[i].Num, ~~(nowwidth + Chart.ChartDefault.StarPoint_X), nowheight - 5);
            }
        }

    },
    X_Label: function () {
        ///<summary>X轴上的名称</summary>
        var ctx = Chart.CanvasObj.getContext("2d");
        ctx.fillStyle = Chart.ChartDefault.FontColor;
        ctx.font = Chart.ChartDefault.TextFont;
        for (var i in this.XLabel) {// 显示数组名
            nowwidth = ((Chart.ChartDefault.CW - Chart.ChartDefault.StarPoint_X) * i / Chart.XLabel.length) + Chart.ChartDefault.StarPoint_X;
            nowheight = Chart.ChartDefault.CH + Chart.ChartDefault.OffsetH - 20 + Chart.ChartDefault.StarPoint_Y;
            ctx.save();
            ctx.translate(nowwidth, nowheight);
            ctx.rotate(-0.6);
            ctx.fillText(Chart.XLabel[i], 0, 0);
            ctx.restore();
        }
    },
    CM: function () {
        ///<summary>鼠标点击事件</summary>
        if (Chart.ChartDefault.IsShowNum) {
            Chart.Draw(true);
            Chart.ChartDefault.IsShowNum = false;
        } else {
            Chart.Draw(false);
            Chart.ChartDefault.IsShowNum = true;
        }
    },
    GetMaxMin: function (Act) {
        ///<summary>获取最大值和最小值</summary>
        if (!Chart.TempVar.hasMaxMinValue) {
            var Max = 0, Min = 9999999999;
            for (var i in this.datasets) {
                for (var j in this.datasets[i].Dataset) {
                    if (this.datasets[i].Dataset[j].Num > Max) { Max = this.datasets[i].Dataset[j].Num; }
                    if (this.datasets[i].Dataset[j].Num < Min) { Min = this.datasets[i].Dataset[j].Num; }
                }
            }
            Chart.TempVar.MaxValue = Max;
            Chart.TempVar.MinValue = Min;
        }
        if (Act == "Max") { return Chart.TempVar.MaxValue; }
        if (Act == "Min") { return Chart.TempVar.MinValue; }
    },
    Draw: function (Act) {
        if (this.XLabel.length > this.DataMaxLength) { return; }
        if (this.datasets.length > 6) { return; }

        Chart.MISC();
        Chart.X_Label();
        Chart.X_Line();
        for (var i in this.datasets) {
            this.ChartDefault.Step = i;
            Chart.Dotted(this.datasets[i].Dataset);
            Chart.Polyline(this.datasets[i].Dataset);
            Chart.DataLabel(Chart.datasets[i].DLabel);
            if (Act) { Chart.ToolTipA(Chart.datasets[i].Dataset); }
        }
    }
}