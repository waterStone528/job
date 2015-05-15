// JavaScript source code
var M0601 = {
    FocusItem: null,
    L1DERParam: ",",
    FirstLoadNum: 35,
    LoadParam: { From: 0, Num: 10 },
    GetFocusUserNum: function () {
        return this.FocusItem.parentElement.querySelector("[data-btid='0']").innerText;
    },
    Init: function () {
        LoadingBoxCtl(1);
        this.STR.EVINIT(this.FirstLoadNum);
    }, //加载更多数据
    LoadMoreInfo: function () {
        if (parseInt(Continue.innerText) == 1) {
            Continue.innerText = 0;
            M0601.STR.EVM060101(M0601.LoadParam.From, M0601.LoadParam.Num, this.L1DERParam);
        }
    },
    HButton: function (Sender) {
        var BtID = parseInt(Sender.getAttribute("data-btid"));
        switch (BtID) {
            case 1:
                var arr = this.L1DERParam.split(",");
                for (var i in arr) { if (arr[i].indexOf("1#") > -1) { this.L1DERParam = this.L1DERParam.replace("," + arr[i], ""); } }
                if (Sender.getAttribute("data-ft") == "0") { Sender.setAttribute("data-ft", "1"); Sender.querySelector("input").focus(); }
                else { Sender.setAttribute("data-ft", "0"); this.L1DERParam += "1#" + Sender.querySelector("input").value + ","; this.L1Filter(); }
                break;
            case 2: this.L1DERParam = this.SortStatus(Sender, 2, this.L1DERParam); this.L1Filter(); break;
            case 3: this.L1DERParam = this.SortStatus(Sender, 3, this.L1DERParam); this.L1Filter(); break;
            case 4: this.L1DERParam = this.SortStatus(Sender, 4, this.L1DERParam); this.L1Filter(); break;
            case 5: this.L1DERParam = this.SortStatus(Sender, 5, this.L1DERParam); this.L1Filter(); break;
            case 6: this.L1DERParam = this.SortStatus(Sender, 6, this.L1DERParam); this.L1Filter(); break;
        }
    },
    SortStatus: function (Sender, ID, Str) {
        Str = Str.replace("," + ID + "#A,", ",");
        Str = Str.replace("," + ID + "#D,", ",");
        switch (Sender.getAttribute("data-ft")) {
            case "2": Sender.setAttribute("data-ft", "3"); Str += ID + "#A,"; break;
            case "3": Sender.setAttribute("data-ft", "4"); Str += ID + "#D,"; break;
            case "4": Sender.setAttribute("data-ft", "2"); break;
            default: break;
        }
        return Str;
    },
    L1Filter: function () { LoadingBoxCtl(1); this.STR.EVM060102(this.L1DERParam, this.FirstLoadNum); },

    Button: function (Sender) { this.SltItem(Sender); },
    SltItem: function (TheItem) {
        if (this.FocusItem != null && TheItem != this.FocusItem) { this.FocusItem.setAttribute("data-slt", 0); };
        if (TheItem == null) { this.FocusItem.setAttribute("data-slt", 0); this.FocusItem = TheItem; return; };
        this.FocusItem = TheItem;
        if (this.FocusItem.getAttribute("data-slt") == "1") { this.FocusItem.setAttribute("data-slt", 0); }
        else { this.FocusItem.setAttribute("data-slt", 1); this.DspList(this.FocusItem.getAttribute("data-btid")); };
    },
    DspList: function (BtID) {
        MVB.Template = document.querySelector(".TP" + BtID).children[0];
        switch (BtID) {
            case "1": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0601.SltItem(null)", "用户" + this.GetFocusUserNum() + "-应收账单"); this.STR.EVM060103(this.GetFocusUserNum()); break;
        }
    },
    STR: {
        EVINIT: function (Num) {
            ///<summary>初始化页面</summary><param name="Num">首次加载信息的数量</param>
            M0601.UID.M060103();
            setTimeout("for(var i=0;i<" + Num + ";i++){M0601.UID.M060101(\"U005321\", \"Admin1\", \"姓名1\", 6, 10000, 8000, 2000, 3, 3500, 1000);};M0601.UID.M060102(false," + Num + ");", 1000);
            var Data = { C1: 10000, C2: 20000, C3: 30000, C4: 40000, C5: 50000, C6: 60000 };
            M0601.UID.M060105(Data);
        },//初始化页面
        EVM060101: function (Form, Num, Sort) {
            ///<summary>加载更多数据</summary><param name="From">从第N条开始加载</param><param name="Num">加载信息的数量</param><param name="Sort">排序字符串</param>
            console.log(Form + "__" + Num + "__" + Sort);
            setTimeout("for(var i=0;i<" + Num + ";i++){M0601.UID.M060101(\"U005321\", \"Admin1\", \"姓名1\", 6, 10000, 8000, 2000, 3, 3500, 1000);};M0601.UID.M060102(true," + Num + ");", 1000);
        },//加载更多数据
        EVM060102: function (Sort, Num) {
            ///<summary>查找，排序</summary>
            console.log(Sort);
            M0601.UID.M060103();
            setTimeout("for(var i=0;i<" + Num + ";i++){M0601.UID.M060101(\"U005321\", \"Admin1\", \"姓名1\", 6, 10000, 8000, 2000, 3, 3500, 1000);};M0601.UID.M060102(false," + Num + ");", 1000);
            var Data = { C1: 10000, C2: 20000, C3: 30000, C4: 40000, C5: 50000, C6: 60000 };
            M0601.UID.M060105(Data);
        },
        EVM060103: function (UserSN) {//应收账单
            var ItemInfo = [
                { C1: "U3123327", C2: "U00055", C3: "资产出售", C4: 8654, C5: "2014/03/14", C6: 500, C7: "2014/07/14", C8: 9154 },
                { C1: "U3123327", C2: "U00055", C3: "债权融资", C4: 8654, C5: "2014/03/14", C6: 500, C7: "2014/07/14", C8: 9154 },
                { C1: "U3123327", C2: "U00055", C3: "资产出售", C4: 8654, C5: "2014/03/14", C6: 500, C7: "2014/07/14", C8: 9154 },
                { C1: "U3123327", C2: "U00055", C3: "债权融资", C4: 8654, C5: "2014/03/14", C6: 500, C7: "2014/07/14", C8: 9154 },
                { C1: "U3123327", C2: "U00055", C3: "资产出售", C4: 8654, C5: "2014/03/14", C6: 500, C7: "2014/07/14", C8: 9154 },
                { C1: "U3123327", C2: "U00055", C3: "债权融资", C4: 8654, C5: "2014/03/14", C6: 500, C7: "2014/07/14", C8: 9154 },
                { C1: "U3123327", C2: "U00055", C3: "资产出售", C4: 8654, C5: "2014/03/14", C6: 500, C7: "2014/07/14", C8: 9154 },
                { C1: "U3123327", C2: "U00055", C3: "资产出售", C4: 8654, C5: "2014/03/14", C6: 500, C7: "2014/07/14", C8: 9154 }];
            for (var i in ItemInfo) { M0601.UID.M060104(ItemInfo[i]); }
        },
    },
    UID: {
        M060101: function (c1, c2, c3, c4, c5, c6, c7, c8, c9, c10) {//变量按主表顺序，C14为备注，无在位数据。
            ///<summary>用户数据</summary>
            var NewRow = document.querySelector(".TbList .Template").cloneNode(true); NewRow.removeAttribute("class");
            var NewRowTDs = NewRow.querySelectorAll("td");
            NewRowTDs[0].innerText = c1; NewRowTDs[1].innerText = c2; NewRowTDs[2].innerText = c3; NewRowTDs[3].innerText = c4; NewRowTDs[4].innerText = c5; NewRowTDs[5].innerText = c6; NewRowTDs[6].innerText = c7;
            NewRowTDs[7].innerText = c8; NewRowTDs[8].innerText = c9; NewRowTDs[9].innerText = c10; document.querySelector(".TbList>tbody").appendChild(NewRow);
        },
        M060102: function (IsLast, Num) {
            ///<summary>数据加载完毕后执行</summary><param name="IsLast">是否是最后一条数据;true:是;false:否;</param><param name="Num">加载信息的数量</param>
            M0601.LoadParam.From += Num;
            Continue.innerText = 1;
            if (IsLast) { LastItem = 1; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 0); } else { LastItem = 0; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 1); }
            LoadingBoxCtl(0);
        },//数据加载完毕后执行
        M060103: function () {
            document.querySelectorAll(".TbList>tbody")[0].innerHTML = "";
        },
        M060104: function (V) {
            ///<summary>应收账单</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.C1; TDs[1].DspV = V.C2; TDs[2].DspV = V.C3; TDs[3].DspV = V.C4; TDs[4].DspV = V.C5; TDs[5].DspV = V.C6; TDs[6].DspV = V.C7; TDs[7].DspV = V.C8;
            FBoxContainer.appendChild(NewBox);
        },//应收账单
        M060105: function (V) {
            ///<summary>统计数据</summary>
            var Tds = document.querySelectorAll(".TbList>tfoot>.Summary>td");
            Tds[4].DspV = V.C1; Tds[5].DspV = V.C2; Tds[6].DspV = V.C3; Tds[7].DspV = V.C4; Tds[8].DspV = V.C5; Tds[9].DspV = V.C6;
            LoadingBoxCtl(0);
        },
    }
};