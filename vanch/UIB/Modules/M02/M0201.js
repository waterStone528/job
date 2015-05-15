﻿// JavaScript source code
var M0201 = {
    FocusItem: null,
    FocusBox: null,
    L1DERParam: ",",
    FirstLoadNum: 35,
    LoadParam: { From: 0, Num: 10 },
    GetFocusUserNum: function () {
        return this.FocusItem.parentElement.querySelector("[data-btid = '1']").innerText;
    },
    Init: function () {
        LoadingBoxCtl(1);
        M0201.STR.EVINIT(this.FirstLoadNum);
    },
    //加载更多数据
    LoadMoreInfo: function () {
        if (parseInt(Continue.innerText) == 1) {
            Continue.innerText = 0;
            M0201.STR.EVM020113(this.L1DERParam, M0201.LoadParam.From, M0201.LoadParam.Num);
        }
    },
    HButton: function (Sender) {
        var BtID = parseInt(Sender.getAttribute("data-btid"));
        switch (BtID) {
            case 1:
                var arr = this.L1DERParam.split(",");
                for (var i in arr) { if (arr[i].indexOf("1#") > -1) {this.L1DERParam = this.L1DERParam.replace("," + arr[i], ""); } }
                if (Sender.getAttribute("data-ft") == "0") { Sender.setAttribute("data-ft", "1"); Sender.querySelector("input").focus(); }
                else { Sender.setAttribute("data-ft", "0"); this.L1DERParam += "1#" + Sender.querySelector("input").value + ","; this.L1Filter(); }
                break;
            case 2: this.L1DERParam = this.SortStatus(Sender, 2, this.L1DERParam); this.L1Filter(); break;
            case 3: this.L1DERParam = this.SortStatus(Sender, 3, this.L1DERParam); this.L1Filter(); break;
            case 4: this.L1DERParam = this.SortStatus(Sender, 4, this.L1DERParam); this.L1Filter(); break;
            case 5: this.L1DERParam = this.SortStatus(Sender, 5, this.L1DERParam); this.L1Filter(); break;
            case 6: this.L1DERParam = this.SortStatus(Sender, 6, this.L1DERParam); this.L1Filter(); break;
            case 7: this.L1DERParam = this.SortStatus(Sender, 7, this.L1DERParam); this.L1Filter(); break;
            case 8: this.L1DERParam = this.SortStatus(Sender, 8, this.L1DERParam); this.L1Filter(); break;
        }
    },
    SortStatus: function (Sender, ID, Str) {
        Str = Str.replace("," + ID + "#A,", ",");
        Str = Str.replace("," + ID + "#D,", ",");
        switch(Sender.getAttribute("data-ft")){
            case "2": Sender.setAttribute("data-ft", "3"); Str += ID + "#A,"; break;
            case "3": Sender.setAttribute("data-ft", "4"); Str += ID + "#D,"; break;
            case "4": Sender.setAttribute("data-ft", "2"); break;
            default: break;
        }
        return Str;
    },
    L1Filter: function () {
        LoadingBoxCtl(1);
        var NewRow = document.querySelector(".TbList .Template").cloneNode(true);
        document.querySelector(".TbList>tbody").innerHTML = "";
        document.querySelector(".TbList>tbody").appendChild(NewRow);
        this.STR.EVM020112(this.L1DERParam, M0201.FirstLoadNum);
    },
    Button: function (Sender) { this.SltItem(Sender); },
    SlideSwitch: function (Sender) {
        var Switch = null;
        TransingStatus.SetStatus(1);
        this.FocusItem = Sender.parentElement;
        if (Sender.getAttribute('data-sw') == '1') { Switch = 0; } else { Switch = 1; }
        M0201.STR.EVM020114(this.GetFocusUserNum(), Switch);
    },
    SltItem: function (TheItem) {
        if (this.FocusItem != null && TheItem != this.FocusItem) { this.FocusItem.setAttribute("data-slt", 0); };
        if (TheItem == null) { this.FocusItem.setAttribute("data-slt", 0); this.FocusItem = TheItem; return; };
        this.FocusItem = TheItem;
        if (this.FocusItem.getAttribute("data-slt") == "1") { this.FocusItem.setAttribute("data-slt", 0); }
        else { this.FocusItem.setAttribute("data-slt", 1); this.DspList(this.FocusItem.getAttribute("data-btid")); };
    },
    SltBox: function (TheItem) {
        if (this.FocusBox != null && TheItem != this.FocusBox) { this.FocusBox.setAttribute("data-slt", 0); };
        if (TheItem == null) { this.FocusBox.setAttribute("data-slt", 0); this.FocusBox = TheItem; document.querySelector(".MovingBox>.ButtonBar>div[data-btst]").setAttribute("data-btst", "D"); return; };
        this.FocusBox = TheItem;
        if (this.FocusBox.getAttribute("data-slt") == "1") { this.FocusBox.setAttribute("data-slt", 0); document.querySelector(".MovingBox>.ButtonBar>div[data-btst]").setAttribute("data-btst", "D"); }
        else { this.FocusBox.setAttribute("data-slt", 1); document.querySelector(".MovingBox>.ButtonBar>div[data-btst]").setAttribute("data-btst", "E"); };
    },
    DelBox: function () {
        var ItemSN = document.querySelector(".MovingBox>.Container>.FBoxContainer>[data-slt='1']").querySelector(".Hidden").innerText;
        document.querySelector(".MovingBox>.ButtonBar>div[data-btst]").setAttribute("data-btst", "D");
        TransingStatus.SetStatus(1);
        this.STR.EVM020102(ItemSN);
    },
    DspList: function (BtID) {
        MVB.Template = document.querySelector(".TP" + BtID).children[0];
        switch (BtID) {
            case "1": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0201.SltItem(null)", "用户信息"); this.STR.EVM020101(this.GetFocusUserNum()); break;
            case "2": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0201.SltItem(null)", "用户" + this.GetFocusUserNum() + "-取消发布"); this.STR.EVM020103(this.GetFocusUserNum()); break;
            case "3": MVB.ContainerWidth = 400; MVB.Buttons = ["取消", "M0201.DelBox()"]; MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0201.SltItem(null)", "用户" + this.GetFocusUserNum() + "-己发布债权"); this.STR.EVM020104(this.GetFocusUserNum()); break;
            case "4": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0201.SltItem(null)", "用户" + this.GetFocusUserNum() + "-取消预约"); this.STR.EVM020105(this.GetFocusUserNum()); break;
            case "5": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0201.SltItem(null)", "用户" + this.GetFocusUserNum() + "-拒绝预约"); this.STR.EVM020106(this.GetFocusUserNum()); break;
            case "6": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0201.SltItem(null)", "用户" + this.GetFocusUserNum() + "-预约中债权"); this.STR.EVM020107(this.GetFocusUserNum()); break;
            case "7": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0201.SltItem(null)", "用户" + this.GetFocusUserNum() + "-还款中债权"); this.STR.EVM020108(this.GetFocusUserNum()); break;
            case "8": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0201.SltItem(null)", "用户" + this.GetFocusUserNum() + "-己还款债权"); this.STR.EVM020109(this.GetFocusUserNum()); break;
            case "9": MVB.ContainerWidth = 400; MVB.Buttons = ["保存", "M0201.ClkSaveNote();"]; MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0201.SltItem(null)", "用户" + this.GetFocusUserNum() + "-备注信息"); this.STR.EVM020110(this.GetFocusUserNum()); break;
        }
    },
    EnableBt: function (bt) {
        var btObj = document.querySelector(".MovingBox>.ButtonBar").children[1];
        btObj.setAttribute("data-btst", "E");
    },
    ClkSaveNote: function () {
        TransingStatus.SetStatus(1);
        var noteText = document.querySelector(".MovingBox>.Container textarea").innerText;
        this.STR.EVM020111(this.GetFocusUserNum(), noteText);
    },
    UIP: {

    },
    STR: {
        EVINIT: function(Num) {
            ///<summary>初始化页面</summary><param name="Num">首次加载信息的数量</param>
            setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { if(i%4!=0){M0201.UID.M020113(\"USSSS0\" + i, \"姓名\" + i, 5, 2, 3, 2, 1, 1, 0, 1, 1, 1, \"609\", \"通过\", 1);}else{M0201.UID.M020113(\"USSSS0\" + i, \"姓名\" + i, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, \"609\", \"未通过\", 1);} };M0201.UID.M020112(false, " + Num + ");", 500);
        },//初始化页面
        EVM020101: function (UserSN) {
            var UserInfo = { No: "U25184488", Name: "牛德华", BirthDay: "1967/04/09", Sex: "男", CardAddress: "浙江宁波", CardID: "330723*", Mobile: "137****8888", Email: "niudehua@163.com", Marry: "已婚", Breed: "已育", Health: "健康", IsSafety: "是", LivePro: "浙江", LiveCity: "宁波", LiveStreet: "高新区", School: "宁波江东实验小学", Edu: "小学", EduNo: "No.1215877", FriendName: "小王", FriendTel: "0547-9999999", Company: "中国大陆娱乐圈", ComProperty: "国有企业", JobTime: "2000/01/01", JobTel: "0574-88888888", Job: "程序员", CompanyTel: "0574-88888888", CompanyWeb: "www.baidu.com", ColleagueName: "张学友", ColleagueTel: "137****9999", SpouseName: "张曼玉", SpouseTel: "137****7777", SpouseCardID: "330729*", SpouseCompany: "中国大陆娱乐圈", FamilyName: "刘星", FamilyTel: "137****6666", FamilyCardID: "330729*", FamilyCompany: "中国大陆娱乐圈", MTotalIn: "1.2", MTotalOut: "0.7", MClearIn: "0.3", TotalAsset: "200", TotalDebt: "0", ClearAsset: "100", CourtsExe: "无", CreditState: "正常", CaseNum: "9", CaseTotalAmt: "183", NowDebtNum: "0", NowTotalDebtAmt: "0", NowOverdue: "1", OverdueAmt: "15", HistoryOverdue: "2", RegTime: "2009/09/12" };
            M0201.UID.M020101(UserInfo); //UI演示数据，正式使用需删除此行
        },//用户信息
        EVM020102: function (ItemSN) {
            console.log(ItemSN);
            setTimeout("M0201.UID.M020102(true)", 1000);//UI演示用，正式使用需删除此行
        },//取消发布，特定己发布债权被击点取消命令后
        EVM020103: function (UserSN) {
            var ItemInfo = { No: "U13777059604", BorrowAmt: 515, AssureWay: "信用", BorrowLimit: 365, PaymentWay: "等额本息", DayRate: 0.25, PawnName: "汽车", PawnRate: 0.15, BorrowBody: "个人", Area1: "浙江", Area2: "宁波", FundPurpose: "日常经营", PaymentSource: "工资收入" };
            M0201.UID.M020103(ItemInfo); M0201.UID.M020103(ItemInfo); M0201.UID.M020103(ItemInfo);//UI演示数据，正式使用需删除此行
        },//已取消发布
        EVM020104: function (UserSN) {
            var ItemInfo = { ItemSN: "A0001", No: "U13777059604", BorrowAmt: 515, AssureWay: "信用", BorrowLimit: 365, PaymentWay: "等额本息", DayRate: 0.25, PawnName: "汽车", PawnRate: 0.15, BorrowBody: "个人", Area1: "浙江", Area2: "宁波", FundPurpose: "日常经营", PaymentSource: "工资收入" };
            M0201.UID.M020104(ItemInfo); M0201.UID.M020104(ItemInfo); M0201.UID.M020104(ItemInfo);//UI演示数据，正式使用需删除此行
        },//已发布债权
        EVM020105: function (UserSN) {
            var ItemInfo = { No: "U35848324", BorrowAmt: 23, AssureWay: "抵押", BorrowLimit: 120, PaymentWay: "等额本息", DayRate: 0.22, Investor: "U51588444", AdviserNo: "U3215844", Pawn: "股权", PaymentSource: "日常经营" };
            M0201.UID.M020105(ItemInfo); M0201.UID.M020105(ItemInfo); M0201.UID.M020105(ItemInfo);//UI演示数据，正式使用需删除此行
        },//已取消预约
        EVM020106: function (UserSN) {
            console.log(UserSN);
            var ItemInfo = { No: "U35848324", BorrowAmt: 23, AssureWay: "抵押", BorrowLimit: 120, PaymentWay: "等额本息", DayRate: 0.22, Investor: "U51588444", AdviserNo: "U3215844", Pawn: "股权", PaymentSource: "日常经营" };
            M0201.UID.M020106(ItemInfo); M0201.UID.M020106(ItemInfo); M0201.UID.M020106(ItemInfo);//UI演示数据，正式使用需删除此行
        },//已拒绝预约
        EVM020107: function (UserSN) {
            var ItemInfo = { No: "U35848324", BorrowAmt: 23, AssureWay: "抵押", BorrowLimit: 120, PaymentWay: "等额本息", DayRate: 0.22, Investor: "U51588444", AdviserNo: "U3215844", Pawn: "股权", PaymentSource: "日常经营" };
            M0201.UID.M020107(ItemInfo); M0201.UID.M020107(ItemInfo); M0201.UID.M020107(ItemInfo);//UI演示数据，正式使用需删除此行
        },//预约中债权
        EVM020108: function (UserSN) {
            var ItemInfo = { No: "U35848328", BorrowAmt: 420, AssureWay: "抵押", BorrowLimit: 90, PaymentWay: "等额本息", DayRate: 0.29, InvestTime: "2012/05/01", Investor: "U3215324", Pawn: "无", OverdueTime: "2012/06/01" };
            M0201.UID.M020108(ItemInfo); M0201.UID.M020108(ItemInfo); M0201.UID.M020108(ItemInfo);//UI演示数据，正式使用需删除此行
        },//还款中债权
        EVM020109: function (UserSN) {
            var ItemInfo = { No: "U35848321", BorrowAmt: 500, AssureWay: "信用", BorrowLimit: 365, PaymentWay: "等额本息", DayRate: 0.25, PaymentTime: "2014/03/11", Investor: "U32151844", OverdueTime: "2014/12/12", PaymentState: "逾期" };
            M0201.UID.M020109(ItemInfo); M0201.UID.M020109(ItemInfo); M0201.UID.M020109(ItemInfo);//UI演示数据，正式使用需删除此行
        },//已还款债权
        EVM020110: function (UserSN) {
            M0201.UID.M020110("看，灰机~~");//UI演示数据，正式使用需删除此行
        },//备注信息
        EVM020111: function (UserSN, noteText) {
            setTimeout("M0201.UID.M020111(true);", 1000); //演示
        },//保存备注信息
        EVM020112: function (L1DERParam, Num) {
            ///<summary>数据排序</summary>
            ///<param name="L1DERParam">排序字符串</param><param name="Num">已加载的信息数据</param>
            console.log(L1DERParam);
            setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { M0201.UID.M020113(\"USSSS0\" + i, \"姓名\" + i, 5, 2, 3, 2, 1, 1, 0, 1, 1, 1, \"609\", \"通过\", 1); };M0201.UID.M020112(false, " + Num + ");", 500);
        },
        EVM020113: function (L1DERParam, Form, Num) {
            ///<summary>加载更多数据</summary>
            ///<param name="L1DERParam">排序字符串</param><param name="From">从第N条开始加载</param><param name="Num">加载信息的数量</param>
            console.log(L1DERParam);

            setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { M0201.UID.M020113(\"USSSS0\" + i, \"姓名\" + i, 5, 2, 3, 2, 1, 1, 0, 1, 1, 1, \"609\", \"通过\", 1);}; M0201.UID.M020112(true, " + Num + ");", 1000);
        },//加载更多数据
        EVM020114: function (UserSN, Act) {
            ///<summary>服务状态</summary><param name="UserSN">用户编号</param><param name="Act">状态值</param>
            console.log(UserSN);
            setTimeout("M0201.UID.M020114(true," + Act + ")", 200);
        }//服务状态
    },
    UID: {
        MCLRows: function () {//清除主表内容，为重新显示数据。
            document.querySelectorAll(".TbList>tbody")[0].innerHTML = "";
        },
        M020113: function (c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c15, c16) {//变量按主表顺序，C14为备注，无在位数据。
            ///<summary>用户数据</summary>
            var NewRow = document.querySelector(".TbList .Template").cloneNode(true); NewRow.removeAttribute("class");
            var NewRowTDs = NewRow.querySelectorAll("td");
            NewRowTDs[0].DspV = c1; NewRowTDs[1].DspV = c2; NewRowTDs[2].DspV = c3; NewRowTDs[3].DspV = c4; NewRowTDs[4].DspV = c5; NewRowTDs[5].DspV = c6; NewRowTDs[6].DspV = c7;
            NewRowTDs[7].DspV = c8; NewRowTDs[8].DspV = c9; NewRowTDs[9].DspV = c10; NewRowTDs[10].DspV = c11;
            NewRowTDs[11].DspV = c12; NewRowTDs[12].DspV = c13; NewRowTDs[14].DspV = c15; NewRowTDs[15].children[0].setAttribute('data-sw', c16);
            document.querySelector(".TbList>tbody").appendChild(NewRow);
        },
        M020101: function (V) {
            ///<summary>用户信息</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Name; TDs[2].DspV = V.BirthDay; TDs[3].DspV = V.Sex; TDs[4].DspV = V.CardAddress; TDs[5].DspV = V.CardID; TDs[6].DspV = V.Mobile; TDs[7].DspV = V.Email; TDs[8].DspV = V.Marry; TDs[9].DspV = V.Breed; TDs[10].DspV = V.Health; TDs[11].DspV = V.IsSafety; TDs[12].DspV = V.LivePro + " " + V.LiveCity + " " + V.LiveStreet; TDs[13].DspV = V.School; TDs[14].DspV = V.Edu; TDs[15].DspV = V.EduNo; TDs[16].DspV = V.FriendName; TDs[17].DspV = V.FriendTel;
            TDs[18].DspV = V.SpouseName; TDs[19].DspV = V.SpouseTel; TDs[20].DspV = V.SpouseCardID; TDs[21].DspV = V.SpouseCompany; TDs[22].DspV = V.FamilyName; TDs[23].DspV = V.FamilyTel; TDs[24].DspV = V.FamilyCardID; TDs[25].DspV = V.FamilyCompany;
            TDs[26].DspV = V.Company; TDs[27].DspV = V.ComProperty; TDs[28].DspV = V.JobTime; TDs[29].DspV = V.JobTel; TDs[30].DspV = V.Job; TDs[31].DspV = V.CompanyTel; TDs[32].DspV = V.CompanyWeb; TDs[33].DspV = V.ColleagueName; TDs[34].DspV = V.ColleagueTel;
            TDs[35].querySelector("label").DspV = V.MTotalIn; TDs[36].querySelector("label").DspV = V.MTotalOut; TDs[37].querySelector("label").DspV = V.MClearIn; TDs[38].querySelector("label").DspV = V.TotalAsset; TDs[39].querySelector("label").DspV = V.TotalDebt; TDs[40].querySelector("label").DspV = V.ClearAsset; TDs[41].DspV = V.CourtsExe; TDs[42].DspV = V.CreditState;
            TDs[43].DspV = V.CaseNum; TDs[44].querySelector("label").DspV = V.CaseTotalAmt; TDs[45].DspV = V.NowDebtNum;TDs[46].querySelector("label").DspV = V.NowTotalDebtAmt; TDs[47].DspV = V.NowOverdue; TDs[48].querySelector("label").DspV = V.OverdueAmt; TDs[49].DspV = V.HistoryOverdue; TDs[50].DspV = V.RegTime;
            FBoxContainer.appendChild(NewBox);
        },//用户信息
        M020102: function (Res) {
            ///<summary>取消发布</summary>
            if (Res) { TransingStatus.SetStatus(3); MVB.MBC.querySelector(".Container>.FBoxContainer").removeChild(M0201.FocusBox); M0201.FocusBox = null; } else { TransingStatus.SetStatus(3); }
        },//特定己发布债权被击点取消命令后,执行成功
        M020103: function (V) {
            ///<summary>已取消债权</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].querySelector("label").DspV = V.BorrowAmt; TDs[2].DspV = V.AssureWay; TDs[3].querySelector("label").DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.PawnName; TDs[7].DspV = V.PawnRate; TDs[8].DspV = V.BorrowBody; TDs[9].querySelectorAll("label")[0].DspV = V.Area1; TDs[9].querySelectorAll("label")[1].DspV = V.Area2; TDs[10].DspV = V.FundPurpose; TDs[11].DspV = V.PaymentSource;

            FBoxContainer.appendChild(NewBox);
        },
        M020104: function (V) {
            ///<summary>已发布债权</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.BorrowAmt; TDs[2].DspV = V.AssureWay; TDs[3].DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.PawnName; TDs[7].DspV = V.PawnRate; TDs[8].DspV = V.BorrowBody; TDs[9].querySelectorAll("label")[0].DspV = V.Area1; TDs[9].querySelectorAll("label")[1].DspV = V.Area2; TDs[10].DspV = V.FundPurpose; TDs[11].DspV = V.PaymentSource;
            NewBox.querySelector(".Hidden").DspV = V.ItemSN;
            FBoxContainer.appendChild(NewBox);
        },
        M020105: function (V) {
            ///<summary>已取消预约</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].querySelector("label").DspV = V.BorrowAmt; TDs[2].DspV = V.AssureWay; TDs[3].querySelector("label").DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.Investor; TDs[7].DspV = V.AdviserNo; TDs[8].DspV = V.Pawn; TDs[9].DspV = V.PaymentSource;
            FBoxContainer.appendChild(NewBox);
        },
        M020106: function (V) {
            ///<summary>已拒绝预约</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].querySelector("label").DspV = V.BorrowAmt; TDs[2].DspV = V.AssureWay; TDs[3].querySelector("label").DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.Investor; TDs[7].DspV = V.AdviserNo; TDs[8].DspV = V.Pawn; TDs[9].DspV = V.PaymentSource;
            FBoxContainer.appendChild(NewBox);
        },
        M020107: function (V) {
            ///<summary>预约中债权</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].querySelector("label").DspV = V.BorrowAmt; TDs[2].DspV = V.AssureWay; TDs[3].querySelector("label").DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.Investor; TDs[7].DspV = V.AdviserNo; TDs[8].DspV = V.Pawn; TDs[9].DspV = V.PaymentSource;
            FBoxContainer.appendChild(NewBox);
        },
        M020108: function (V) {
            ///<summary>还款中债权</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].querySelector("label").DspV = V.BorrowAmt; TDs[2].DspV = V.AssureWay; TDs[3].querySelector("label").DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.InvestTime; TDs[7].DspV = V.Investor; TDs[8].DspV = V.Pawn; TDs[9].DspV = V.OverdueTime;
            FBoxContainer.appendChild(NewBox);
        },
        M020109: function (V) {
            ///<summary>已还款债权</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].querySelector("label").DspV = V.BorrowAmt; TDs[2].DspV = V.AssureWay; TDs[3].querySelector("label").DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.PaymentTime; TDs[7].DspV = V.OverdueTime; TDs[8].DspV = V.PaymentState; TDs[9].DspV = V.Investor;
            FBoxContainer.appendChild(NewBox);
        },
        M020110: function (V) {
            ///<summary>用户备注信息</summary>
            var TDs = MVB.MBC.querySelector("textarea").value = V;
        },
        M020111: function (res) {
            ///<summary>保存用户备注</summary>
            MVB.Close();
            if (res == true) { TransingStatus.SetStatus(3); } else { TransingStatus.SetStatus(2); }
        },
        M020112: function (IsLast, Num) {
            ///<summary>数据加载完毕后执行</summary><param name="IsLast">是否是最后一条数据;true:是;false:否;</param><param name="Num">加载信息的数量</param>
            M0201.LoadParam.From += Num;
            Continue.innerText = 1;
            if (IsLast) { LastItem = 1; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 0); } else { LastItem = 0; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 1); }
            LoadingBoxCtl(0);
        },//数据加载完毕后执行
        M020114: function (Res, Act) {
            ///<summary>修改服务状态</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
                M0201.FocusItem.querySelector(".SlideButton").setAttribute("data-sw", Act);
            } else {
                TransingStatus.SetStatus(2);
            }
        }
    },
};