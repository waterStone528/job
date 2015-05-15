﻿// JavaScript source code
var M0204 = {
    FocusItem: null,
    FocusBox: null,
    L1DERParam: ",",
    FirstLoadNum: 35,
    LoadParam: { From: 0, Num: 10 },
    Init: function () {
        LoadingBoxCtl(1);
        this.STR.EVINIT(this.FirstLoadNum);
    },
    GetFocusUserNum: function () {
        return this.FocusItem.parentElement.querySelector("[data-btid = '1']").innerText;
    },
    //加载更多数据
    LoadMoreInfo: function () {
        if (parseInt(Continue.innerText) == 1) {
            Continue.innerText = 0;
            M0204.STR.EVM020411(M0204.L1DERParam, M0204.LoadParam.From, M0204.LoadParam.Num);
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
    L1Filter: function () {
        LoadingBoxCtl(1);
        var NewRow = document.querySelector(".TbList .Template").cloneNode(true);
        document.querySelector(".TbList>tbody").innerHTML = "";
        document.querySelector(".TbList>tbody").appendChild(NewRow);
        this.STR.EVM020410(this.L1DERParam, this.FirstLoadNum);
    },
    Button: function (Sender) { this.SltItem(Sender); },
    SltItem: function (TheItem) {
        if (this.FocusItem != null && TheItem != this.FocusItem) { this.FocusItem.setAttribute("data-slt", 0); };
        if (TheItem == null) { this.FocusItem.setAttribute("data-slt", 0); return; };
        this.FocusItem = TheItem;
        if (this.FocusItem.getAttribute("data-slt") == "1") { this.FocusItem.setAttribute("data-slt", 0); }
        else { this.FocusItem.setAttribute("data-slt", 1); this.DspList(this.FocusItem.getAttribute("data-btid")); };
    },
    SlideSwitch: function (Sender) {
        var Switch = null;
        TransingStatus.SetStatus(1);
        this.FocusItem = Sender.parentElement;
        if (Sender.getAttribute('data-sw') == '1') { Switch = 0; } else { Switch = 1; }
        M0204.STR.EVM020412(this.GetFocusUserNum(), Switch);
    },
    DspList: function (BtID) {
        MVB.Template = document.querySelector(".TP" + BtID).children[0];
        var containerObj = document.querySelector('#MainZone .ListContainer');
        var UserSN = this.FocusItem.parentElement.querySelector("[data-btid='1']").innerText;
        switch (BtID) {
            case "1": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0204.SltItem(null)", "用户信息"); this.STR.EVM020401(this.GetFocusUserNum()); break;
            case "2": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0204.SltItem(null)", "用户" + this.GetFocusUserNum() + "-拒绝预约"); this.STR.EVM020402(this.GetFocusUserNum()); break;
            case "3": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0204.SltItem(null)", "用户" + this.GetFocusUserNum() + "-审核中债权"); this.STR.EVM020403(this.GetFocusUserNum()); break;
            case "4": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0204.SltItem(null)", "用户" + this.GetFocusUserNum() + "-已审核债权"); this.STR.EVM020404(this.GetFocusUserNum()); break;
            case "5": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0204.SltItem(null)", "用户" + this.GetFocusUserNum() + "-已结案债权"); this.STR.EVM020405(this.GetFocusUserNum()); break;
            case "6": MVB.ContainerWidth = 500; MVB.Buttons = ["保存", "M0204.ClkSaveNote();"]; MVB.Open(containerObj, "M0204.SltItem(null)", "用户" + this.GetFocusUserNum() + "-备注信息"); this.STR.EVM020406(this.GetFocusUserNum()); break;
            case "7": this.STR.EVM020408(this.GetFocusUserNum(), this.FocusItem.getAttribute("data-apstate")); break;
        }
    },
    EnableBt: function (bt) {
        var btObj = document.querySelector(".MovingBox>.ButtonBar").children[1];
        btObj.setAttribute("data-btst", "E");
    },
    BtStatusSet: function (st) {
        document.querySelector(".MovingBox>.ButtonBar").children[1].setAttribute("data-btst", "E");
        document.querySelector(".MovingBox>.ButtonBar").children[2].setAttribute("data-btst", "E");
    },
    ClkSaveNote: function () {
        TransingStatus.SetStatus(1);
        var noteText = document.querySelector(".MovingBox>.Container textarea").innerText;
        this.STR.EVM020407(this.GetFocusUserNum(), noteText);
    },
    Auditing: function (Act) {
        TransingStatus.SetStatus(1);
        var Ext = document.querySelector(".MovingBox>.Container>.ApplyPage>.Page .AppNote textarea").value;
        this.STR.EVM020409(this.GetFocusUserNum(), Act, Ext);
    },

    UIP: {

    },
    STR: {
        EVINIT: function (Num) {
            ///<summary>初始化页面</summary><param name="Num">首次加载信息的数量</param>
            setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { M0204.UID.M020400(\"USSSS0\" + i, \"姓名\" + i, 2, 4, 5, 3, 6, \"609\", (i % 3 + 1), 1);  };M0204.UID.M020410(false, " + Num + ");", 100);
        },//初始化页面
        EVM020401: function (UserSN) {
            var UserInfo = { No: "U00001", Name: "刘德华", Sex: "男", BirthDay: "1988/08/08", CardID: "330719*", Mobile: "13712345678", CardAddress: "香港特别行政区铜锣湾", Email: "liudehua@163.com", Marry: "已婚", Breed: "已育", LivePro: "浙江", LiveCity: "宁波", LiveStreet: "高新区", School: "宁波江东实验小学", Edu: "小学", EduNo: "No.123456", Company: "", ComProperty: "国有企业", Trade: "信息产业", JobTime: "1998/01/01", JobTel: "0574-12345678", Job: "程序员", ComTel: "0574-87654321", ComWeb: "www.vanch.com", ColleagueName: "张学友", ColleagueTel: "13712345678", ColleagueCardID: "33000000000000", SvrArea1: "浙江", SvrArea2: "宁波", SvrItem1: "1", SvrItem2: "0", SvrItem3: "", SvrItem4: "1", Remarks: "我的备注是空的！" };
            M0204.UID.M020401(UserInfo); //UI演示数据，正式使用需删除此行
        },//用户信息
        EVM020402: function (UserSN) {
            var ItemInfo = { No: "U12155412", Amt: 515, AssureWay: "信用", BorrowLimit: 270, PaymentWay: "月付利息", DayRate: 0.25, Borrower: "U98324483", Area1: "浙江", Area2: "温州", FundPurpose: "日常消费", PawnName: "汽车", Invester: "U11111158", AdviserFee: "2" };
            M0204.UID.M020402(ItemInfo); M0204.UID.M020402(ItemInfo); M0204.UID.M020402(ItemInfo); M0204.UID.M020402(ItemInfo);
        },//拒绝预约
        EVM020403: function (UserSN) {
            var ItemInfo1 = { No: "N02155115", Amt: 100, AssureWay: "抵押", BorrowLimit: 100, PaymentWay: "先息后本", DayRate: 0.15, FundPurpose: "过桥资金", Area1: "浙江", Area2: "宁波", Borrower: "U9865655", Invester: "U9865115", PawnName: "汽车", AdviserFee: 1.9, State: 0 }
            var ItemInfo2 = { No: "N02155115", Amt: 100, AssureWay: "抵押", BorrowLimit: 100, PaymentWay: "先息后本", DayRate: 0.15, FundPurpose: "过桥资金", Area1: "浙江", Area2: "宁波", Borrower: "U9865655", Invester: "U9865115", PawnName: "汽车", AdviserFee: 1.9, State: 1 }
            M0204.UID.M020403(ItemInfo1); M0204.UID.M020403(ItemInfo2); M0204.UID.M020403(ItemInfo1); M0204.UID.M020403(ItemInfo2); M0204.UID.M020403(ItemInfo1); M0204.UID.M020403(ItemInfo2);
        },//审核中债权
        EVM020404: function (UserSN) {//已审核债权
            var ItemInfo = { No: "U12154345", Amt: 515, AssureWay: "抵押", BorrowLimit: 180, PaymentWay: "等额本息", DayRate: 0.25, Invester: "U11111158", Borrower: "U98335548", FundPurpose: "日常经营", ExamineState: "审核通过", PawnName: "汽车", InvestPro: "已投资", AdviserFee: "0.85" };
            M0204.UID.M020404(ItemInfo); M0204.UID.M020404(ItemInfo); M0204.UID.M020404(ItemInfo); M0204.UID.M020404(ItemInfo);
        },
        EVM020405: function (UserSN) {
            var ItemInfo = { No: "U12155418", Amt: 515, AssureWay: "抵押", BorrowLimit: 360, PaymentWay: "月付利息", DayRate: 0.25, Invester: "U11111158", Borrower: "U98343554", FundPurpose: "日常经营", ExamineState: "审核未过", PawnName: "无", InvestPro: "已取消", AdviserFee: "3.5" };
            M0204.UID.M020405(ItemInfo); M0204.UID.M020405(ItemInfo); M0204.UID.M020405(ItemInfo); M0204.UID.M020405(ItemInfo);
        },//已结案债权
        EVM020406: function (userSN) {//userSN:用户编号
            M0204.UID.M020406("这是备注的测试数据");
        },
        EVM020407: function (UserSN, noteText) {//UserSN为选中的用户编号,note为备注信息
            setTimeout("M0204.UID.M020407(true);", 1000); //演示
        },
        EVM020408: function (UserSN, State) {
            var UserInfo = { No: "U00001", Name: "刘德华", Sex: "男", BirthDay: "1988/08/08", CardID: "330719*", Mobile: "13712345678", CardAddress: "香港特别行政区铜锣湾", Email: "liudehua@163.com", Marry: "已婚", Breed: "已育", LivePro: "浙江", LiveCity: "宁波", LiveStreet: "高新区", School: "宁波江东实验小学", Edu: "小学", EduNo: "No.123456", Company: "凡奇科技", ComProperty: "国有企业", Trade: "信息产业", JobTime: "1998/01/01", JobTel: "0574-12345678", Job: "程序员", ComTel: "0574-87654321", ComWeb: "www.vanch.com", ColleagueName: "张学友", ColleagueTel: "13712345678", ColleagueCardID: "33000000000000", SvrArea1: "浙江", SvrArea2: "宁波", SvrItem1: "1", SvrItem2: "0", SvrItem3: "", SvrItem4: "1", Remarks: "我的备注是空的！", State: State, AuditTime: "2014年07月09日", Auditer: "张纪中", AuditerText: "Good Job!" };
            M0204.UID.M020408(UserInfo); //演示
        },
        EVM020409: function (UserSN, Act, Ext) {
            console.log(UserSN + "__" + Act + "__" + Ext);
            var Audit = { Name: "陈凯歌", Time: "2014年07月29日" };
            setTimeout("M0204.UID.M020409(" + Act + "," + JSON.stringify(Audit) + ");", 1000);
        },
        EVM020410: function (L1DERParam, Num) {
            ///<summary>数据排序</summary>
            ///<param name="L1DERParam">排序字符串</param><param name="Num">已加载的信息数据</param>
            console.log(L1DERParam);
            setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { M0204.UID.M020400(\"USSSS0\" + i, \"姓名\" + i, 2, 4, 5, 3, 6, \"609\", (i % 3 + 1), 1);  };M0204.UID.M020410(false, " + Num + ");", 100);
        },
        EVM020411: function (L1DERParam, Form, Num) {
            ///<summary>加载更多数据</summary>
            ///<param name="L1DERParam">排序字符串</param><param name="From">从第N条开始加载</param><param name="Num">加载信息的数量</param>
            console.log(L1DERParam);
            setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { M0204.UID.M020400(\"USSSS0\" + i, \"姓名\" + i, 2, 4, 5, 3, 6, \"609\", " + (i % 3 + 1) + ", 1);  };M0204.UID.M020410(true, " + Num + ");", 500);
        },//加载更多数据
        EVM020412: function (UserSN, Act) {
            ///<summary>服务状态</summary><param name="UserSN">用户编号</param><param name="Act">状态值</param>
            console.log(UserSN);
            setTimeout("M0204.UID.M020411(true," + Act + ")", 200);
        }//服务状态
    },
    UID: {
        M020400: function (c1, c2, c3, c4, c5, c6, c7, c8, c9, c10) {
            ///<summary>财务顾问用户列表</summary><param name="c1">用户编号</param><param name="c2">姓名</param><param name="c3">拒绝预约</param><param name="c4">审核中债权</param><param name="c5">服务中债权</param><param name="c6">已结案债权</param><param name="c7">VIP等级</param><param name="c8">客户经理</param><param name="c9">申请状态</param><param name="c10">服务状态</param>
            var NewRow = document.querySelector(".TbList .Template").cloneNode(true);
            NewRow.removeAttribute("class");
            var NewRowTDs = NewRow.querySelectorAll("td");
            NewRowTDs[0].innerText = c1; NewRowTDs[1].innerText = c2; NewRowTDs[2].innerText = c3; NewRowTDs[3].innerText = c4; NewRowTDs[4].innerText = c5;
            NewRowTDs[5].innerText = c6; NewRowTDs[6].innerText = c7; NewRowTDs[7].innerText = c8; NewRowTDs[10].children[0].setAttribute('data-sw', c10);
            switch (c9) {
                case 1:
                    NewRowTDs[9].innerText = "待审核";
                    NewRowTDs[9].setAttribute("data-apstate", 0);
                    break;
                case 2:
                    NewRowTDs[9].innerText = "己拒签";
                    NewRowTDs[9].setAttribute("data-apstate", 2);
                    break;
                case 3:
                    NewRowTDs[9].innerText = "己通过";
                    NewRowTDs[9].setAttribute("data-apstate", 1);
                    break;
            };
            document.querySelector(".TbList>tbody").appendChild(NewRow);
        },
        M020401: function (V) {
            ///<summary>用户信息</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Name; TDs[2].DspV = V.BirthDay; TDs[3].DspV = V.Sex; TDs[4].DspV = V.CardAddress; TDs[5].DspV = V.CardID; TDs[6].DspV = V.Mobile; TDs[7].DspV = V.Email; TDs[8].DspV = V.Marry; TDs[9].DspV = V.Breed; TDs[10].DspV = V.LivePro + " " + V.LiveCity + " " + V.LiveStreet; TDs[11].DspV = V.School; TDs[12].DspV = V.Edu; TDs[13].DspV = V.EduNo;
            TDs[14].DspV = V.School; TDs[15].DspV = V.ComProperty; TDs[16].DspV = V.Trade; TDs[17].DspV = V.JobTime; TDs[18].DspV = V.JobTel; TDs[19].DspV = V.Job; TDs[20].DspV = V.ComTel; TDs[21].DspV = V.ComWeb; TDs[22].DspV = V.ColleagueName; TDs[23].DspV = V.ColleagueTel; TDs[24].DspV = V.ColleagueCardID;
            TDs[25].querySelectorAll("label")[0].DspV = V.SvrArea1; TDs[25].querySelectorAll("label")[1].DspV = V.SvrArea2;TDs[26].querySelector(".UpDownButton").setAttribute("data-sw", V.SvrItem1); TDs[27].querySelector(".UpDownButton").setAttribute("data-sw", V.SvrItem2); TDs[28].querySelector(".UpDownButton").setAttribute("data-sw", V.SvrItem3); TDs[29].querySelector(".UpDownButton").setAttribute("data-sw", V.SvrItem4); NewBox.querySelector("textarea").innerText = V.Remarks;
            FBoxContainer.appendChild(NewBox);
        },//用户信息
        M020402: function (V) {
            ///<summary>拒绝预约</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].querySelector("label").DspV = V.Amt; TDs[2].DspV = V.AssureWay; TDs[3].querySelector("label").DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.FundPurpose; TDs[7].querySelectorAll("label")[0].DspV = V.Area1; TDs[7].querySelectorAll("label")[1].DspV = V.Area2; TDs[8].DspV = V.Borrower; TDs[9].DspV = V.PawnName; TDs[10].DspV = V.Invester; TDs[11].querySelector("label").DspV = V.AdviserFee;
            FBoxContainer.appendChild(NewBox);
        },//拒绝预约
        M020403: function (V) {
            ///<summary>审核中债权</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].querySelector("label").innerText = V.Amt; TDs[2].DspV = V.AssureWay; TDs[3].querySelector("label").DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.FundPurpose; TDs[7].querySelectorAll("label")[0].DspV = V.Area1; TDs[7].querySelectorAll("label")[1].DspV = V.Area2;; TDs[8].DspV = V.Borrower; TDs[9].DspV = V.Invester; TDs[10].DspV = V.PawnName; TDs[11].querySelector("label").DspV = V.AdviserFee;
            if (V.State == 0) { NewBox.querySelector(".InforBox>div>i").setAttribute("class", "icon-uniE615"); NewBox.querySelector(".InforBox>div>i").style.color = "rgb(207, 136, 0)"; }
            if (V.State == 1) { NewBox.querySelector(".InforBox>div>i").setAttribute("class", "icon-stop"); NewBox.querySelector(".InforBox>div>i").style.color = "rgb(213, 23, 23)"; }
            FBoxContainer.appendChild(NewBox);
        },//审核中债权
        M020404: function (V) {
            ///<summary>已审核债权</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].querySelector("label").innerText = V.Amt; TDs[2].DspV = V.AssureWay; TDs[3].querySelector("label").DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.Invester; TDs[7].DspV = V.PawnName;; TDs[8].DspV = V.Borrower; TDs[9].DspV = V.ExamineState; TDs[10].querySelector("label").DspV = V.AdviserFee; TDs[11].DspV = V.InvestPro;
            FBoxContainer.appendChild(NewBox);
        },//已审核债权
        M020405: function (V) {
            ///<summary>已结案债权</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].querySelector("label").innerText = V.Amt; TDs[2].DspV = V.AssureWay; TDs[3].querySelector("label").DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.Invester; TDs[7].DspV = V.PawnName;; TDs[8].DspV = V.Borrower; TDs[9].DspV = V.ExamineState; TDs[10].querySelector("label").DspV = V.AdviserFee; TDs[11].DspV = V.InvestPro;
            FBoxContainer.appendChild(NewBox);
        },//已结案债权
        M020406: function (txt) {
            ///<summary>备注</summary>
            var TTA = MVB.MBC.querySelector("div>textarea");
            TTA.value = txt;
        },//备注
        M020407: function (res) {
            MVB.Close();
            if (res == true) {
                TransingStatus.SetStatus(3);
            }
            else {
                TransingStatus.SetStatus(2);
            }
        },
        M020408: function (User) {
            MVB.ContainerWidth = 600; MVB.Buttons = ["通过", "M0204.Auditing(1);", "拒绝", "M0204.Auditing(2);"]; MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0204.SltItem(null)", "申请状态");
            var SingStatus = document.querySelector(".MovingBox>.Container>.ApplyPage>.SignStatus");
            if (User.State == 0) { SingStatus.setAttribute("data-status", 0); M0204.BtStatusSet(this); }
            if (User.State == 1) { SingStatus.setAttribute("data-status", 1); SingStatus.querySelector(".Approved>.AuditDate").innerText = User.AuditTime; SingStatus.querySelector(".Approved>.Auditor>label").DspV = User.Auditer; }
            if (User.State == 2) { SingStatus.setAttribute("data-status", 2); SingStatus.querySelector(".Rejected>.AuditDate").innerText = User.AuditTime; SingStatus.querySelector(".Rejected>.Auditor>label").DspV = User.Auditer; }
            var TDs = document.querySelectorAll(".MovingBox>.Container>.ApplyPage>.Page>table td");

            TDs[3].DspV = User.Name; TDs[5].DspV = User.Sex;TDs[7].DspV = User.BirthDay;TDs[9].DspV = User.CardID;TDs[11].DspV = User.Mobile;TDs[13].DspV = User.Email;
            TDs[16].DspV = User.Marry; TDs[18].DspV = User.Breed; TDs[20].DspV = User.CardAddress; TDs[22].querySelectorAll("label")[0].DspV = User.LivePro; TDs[22].querySelectorAll("label")[1].DspV = User.LiveCity; TDs[22].querySelectorAll("label")[2].DspV = User.LiveStreet; TDs[24].DspV = User.School; TDs[26].DspV = User.Edu;
            TDs[29].DspV = User.Company; TDs[31].DspV = User.ComProperty; TDs[33].DspV = User.Trade; TDs[35].DspV = User.JobTime; TDs[37].DspV = User.JobTel; TDs[39].DspV = User.Job;
            TDs[42].DspV = User.SvrArea1 + User.SvrArea2; TDs[51].querySelector("textarea").value = User.Remarks; TDs[53].querySelector("textarea").value = User.AuditerText;
            TDs[44].querySelector(".UpDownButton").setAttribute("data-sw", User.SvrItem1);
            TDs[46].querySelector(".UpDownButton").setAttribute("data-sw", User.SvrItem2);
            TDs[48].querySelector(".UpDownButton").setAttribute("data-sw", User.SvrItem3);
            TDs[50].querySelector(".UpDownButton").setAttribute("data-sw", User.SvrItem4);
        },
        M020409: function (Act, Audit) {
            TransingStatus.SetStatus(3);
            var SingStatus = document.querySelector(".MovingBox>.Container>.ApplyPage>.SignStatus");
            if (Act == 1) {
                M0204.FocusItem.innerText = "已通过 ";
                SingStatus.querySelector(".Approved>.AuditDate").innerText = Audit.Time;
                SingStatus.querySelector(".Approved>.Auditor>label").DspV = Audit.Name;
            } else {
                M0204.FocusItem.innerText = "已拒签 ";
                SingStatus.querySelector(".Rejected>.AuditDate").innerText = Audit.Time;
                SingStatus.querySelector(".Rejected>.Auditor>label").DspV = Audit.Name;
            }
            M0204.FocusItem.setAttribute("data-apstate", Act);
            SingStatus.setAttribute("data-status", Act);
        },
        M020410: function (IsLast, Num) {
            ///<summary>数据加载完毕后执行</summary><param name="IsLast">是否是最后一条数据;true:是;false:否;</param><param name="Num">加载信息的数量</param>
            M0204.LoadParam.From += Num;
            Continue.innerText = 1;
            if (IsLast) { LastItem = 1; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 0); } else { LastItem = 0; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 1); }
            LoadingBoxCtl(0);
        },//数据加载完毕后执行
        M020411: function (Res, Act) {
            ///<summary>修改服务状态</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
                M0204.FocusItem.querySelector(".SlideButton").setAttribute("data-sw", Act);
            } else {
                TransingStatus.SetStatus(2);
            }
        }
    }
};