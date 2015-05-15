// JavaScript source code
var M0202 = {
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
        M0202.STR.EVINIT(this.FirstLoadNum);
    },
    //加载更多数据
    LoadMoreInfo: function () {
        if (parseInt(Continue.innerText) == 1) {
            Continue.innerText = 0; 
            M0202.STR.EVM020210(M0202.L1DERParam, M0202.LoadParam.From, M0202.LoadParam.Num);
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
    L1Filter: function () {
        LoadingBoxCtl(1);
        var NewRow = document.querySelector(".TbList .Template").cloneNode(true);
        document.querySelector(".TbList>tbody").innerHTML = "";
        document.querySelector(".TbList>tbody").appendChild(NewRow);
        this.STR.EVM020209(this.L1DERParam, this.FirstLoadNum);
    },
    Button: function (Sender) { this.SltItem(Sender); },
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
        document.querySelector(".MovingBox>.ButtonBar>div[data-btst]").setAttribute("data-btst", "D");
        TransingStatus.SetStatus(1);
        this.STR.EVTP1Cancel();
    },
    SlideSwitch: function (Sender) {
        var Switch = null;
        TransingStatus.SetStatus(1);
        this.FocusItem = Sender.parentElement;
        if (Sender.getAttribute('data-sw') == '1') { Switch = 0; } else { Switch = 1; }
        M0202.STR.EVM020211(this.GetFocusUserNum(), Switch);
    },
    DspListBak: function (BtID) {
        MVB.ContainerWidth = 600;
        MVB.Template = document.querySelector(".TP" + BtID + ">.TbList");
        var MVBTitle = "";
        switch (BtID) {
            case "1": MVBTitle = "客户经理选择"; break;
            case "2": MVBTitle = "备注信息"; break;
        }
        MVB.Buttons = ["知道了", "MVB.Close()"];
        MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0202.SltItem(null)", MVBTitle);
    },
    DspList: function (BtID) {
        MVB.Template = document.querySelector(".TP" + BtID).children[0];
        var containerObj = document.querySelector('#MainZone .ListContainer');
        switch (BtID) {
            case "1": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0202.SltItem(null)", "用户信息"); this.STR.EVM020201(this.GetFocusUserNum()); break;
            case "2": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0202.SltItem(null)", "用户" + this.GetFocusUserNum() + "-取消预约"); this.STR.EVM020202(this.GetFocusUserNum()); break;
            case "3": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0202.SltItem(null)", "用户" + this.GetFocusUserNum() + "-拒绝预约"); this.STR.EVM020203(this.GetFocusUserNum()); break;
            case "4": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0202.SltItem(null)", "用户" + this.GetFocusUserNum() + "-预约中债权"); this.STR.EVM020204(this.GetFocusUserNum()); break;
            case "5": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0202.SltItem(null)", "用户" + this.GetFocusUserNum() + "-已投资债权"); this.STR.EVM020205(this.GetFocusUserNum()); break;
            case "6": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0202.SltItem(null)", "用户" + this.GetFocusUserNum() + "-已结案债权"); this.STR.EVM020206(this.GetFocusUserNum()); break;
            case "7": MVB.ContainerWidth = 500; MVB.Buttons = ["保存", "M0202.ClkSaveNote();"]; MVB.Open(containerObj, "M0202.SltItem(null)", "用户" + this.GetFocusUserNum() + "-备注信息"); this.STR.EVM020207(this.GetFocusUserNum()); break;
        }
    },
    EnableBt: function (bt) {
        var btObj = document.querySelector(".MovingBox>.ButtonBar").children[1];
        btObj.setAttribute("data-btst", "E");
    },
    ClkSaveNote: function () {
        TransingStatus.SetStatus(1);
        var noteText = document.querySelector(".MovingBox>.Container textarea").innerText;
        this.STR.EVM020208(this.GetFocusUserNum(), noteText);
    },

    UIP: {
        Pawn: [{ ID: "A01", Name: "住宅房" }, { ID: "A02", Name: "办公楼" }, { ID: "A03", Name: "商铺" }, { ID: "A04", Name: "工业用地" }, { ID: "A05", Name: "商业用地" }, { ID: "A06", Name: "汽车" }, { ID: "A07", Name: "贵金属" }, { ID: "A08", Name: "收藏品" }, { ID: "A09", Name: "股权" }, { ID: "A10", Name: "债券" }, { ID: "A11", Name: "混合资产" }, { ID: "A12", Name: "厂房" }, { ID: "A13", Name: "其它" }],
    },
    STR: {
        //初始化页面
        EVINIT: function (Num) {
            ///<summary>初始化页面</summary><param name="Num">首次加载信息的数量</param>
            //setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { M0202.UID.M020210(\"USSSS0\" + i, \"姓名\" + i,4, 5, 6, 3, 2, 3, \"609\", \"通过\", 1); };M0202.UID.M020209(false, " + Num + ");", 100);
            //M0202.UID.M020211();

            var busCode = "M0202INIT";
            var data = "busCode=" + busCode + "&pageSize=" + Num;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                M0202.UIP.Pawn = resObj.pawn;
                var serverStatusList = resObj.serverStatusList;

                var dataList = resObj.dataList;
                for (var i in dataList) {
                    var applyStatus;
                    if (dataList[i].applyStatus == 1) {
                        applyStatus = "审核中";
                    }
                    if (dataList[i].applyStatus == 2) {
                        applyStatus = "申请通过";
                    }
                    if (dataList[i].applyStatus == 3) {
                        applyStatus = "申请未通过";
                    }

                    var serverStauts = serverStatusList[i] == true ? 1 : 0;
                    var clientManagerWorkNum = dataList[i].clientManagerWorkNum == null ? "无" : dataList[i].clientManagerWorkNum;

                    M0202.UID.M020210(dataList[i].userSN, dataList[i].name.trim(), dataList[i].cancelReserveAmount, dataList[i].refuseReserveAmount, dataList[i].reservingCrAmount, dataList[i].investedCrAmount, dataList[i].closedCaseCrAmount, dataList[i].vipLevel, clientManagerWorkNum, applyStatus, serverStauts);
                }

                var isOver = dataList.length < Num ? true : false;

                M0202.UID.M020209(isOver, dataList.length);
                M0202.UID.M020211();
            }
            ajaxObj.start();
        },
        //用户信息
        EVM020201: function (UserSN) {//用户信息
            //var UserInfo = { No: "U1215535", Name: "梁朝伟", BirthDay: "1966/07/25", Sex: "男", CardAddress: "中国香港", CardID: "330327*", Mobile: "13788888888", Email: "zhangxueyou@163.com", Marry: "已婚", Breed: "已育", LivePro: "浙江", LiveCity: "宁波", LiveStreet: "光华路137号", Company: "华视传媒", ComProperty: "广告媒体", JobTime: "2000/01/01", JobTel: "0574-88888888", Job: "演员", CompanyTel: "0574-88888888", CompanyWeb: "www.baidu.com", InvestBody: "个人", BorrowBody: "个人", PawnRate: 0.15, AssureWay: "抵押", InvestArea1: "浙江", InvestArea2: "宁波", InvestAmt: "1", DayRate: "0.16", InvestLimitMin: "1", InvestLimitMax: "9999", Pawn: ",A01,A04,A05,A10,A12," };
            //M0202.UID.M020201(UserInfo); //UI演示数据，正式使用需删除此行

            var busCode = "M020201";
            var data = "busCode=" + busCode + "&userSN=" + UserSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                var dataFt = M0202.STR.InvestorBdToFt(resObj);

                M0202.UID.M020201(dataFt);
            }
            ajaxObj.start();
        },
        //取消预约
        EVM020202: function (UserSN) {
            //var ItemInfo = { No: "U3584235", BorrowAmt: 300, AssureWay: "抵押", BorrowLimit: 300, PaymentWay: "等额本息", DayRate: 0.15, Pawn: "贵金属", DebtArea1: "浙江", DebtArea2: "杭州", FundPurpose: "冲量存款", PaymentSource: "贷款资金", Borrower: "U51345355", AdviserNo: "U3215844" };
            //M0202.UID.M020202(ItemInfo); M0202.UID.M020202(ItemInfo); M0202.UID.M020202(ItemInfo);//UI演示数据，正式使用需删除此行

            var busCode = "M020202";
            var data = "busCode=" + busCode + "&userSN=" + UserSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0202.STR.CancelCrBdToFt(resObj[i]);

                    M0202.UID.M020202(dataFt);
                }
            }
            ajaxObj.start();
        },
        //拒绝预约
        EVM020203: function (UserSN) {
            //var ItemInfo = { No: "U3584235", BorrowAmt: 300, AssureWay: "抵押", BorrowLimit: 300, PaymentWay: "等额本息", DayRate: 0.15, Pawn: "贵金属", DebtArea1: "浙江", DebtArea2: "杭州", FundPurpose: "冲量存款", PaymentSource: "贷款资金", Borrower: "U51345355", AdviserNo: "U3215844" };
            //M0202.UID.M020203(ItemInfo); M0202.UID.M020203(ItemInfo); M0202.UID.M020203(ItemInfo);//UI演示数据，正式使用需删除此行

            var busCode = "M020203";
            var data = "busCode=" + busCode + "&userSN=" + UserSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0202.STR.RefuseCrBdToFt(resObj[i]);

                    M0202.UID.M020203(dataFt);
                }
            }
            ajaxObj.start();
        },
        //预约中债权
        EVM020204: function (UserSN) {
            //var ItemInfo = { No: "U3584235", BorrowAmt: 300, AssureWay: "抵押", BorrowLimit: 300, PaymentWay: "等额本息", DayRate: 0.15, Pawn: "贵金属", DebtArea1: "浙江", DebtArea2: "杭州", FundPurpose: "冲量存款", PaymentSource: "贷款资金", Borrower: "U51345355", AdviserNo: "U3215844" };
            //M0202.UID.M020204(ItemInfo); M0202.UID.M020204(ItemInfo); M0202.UID.M020204(ItemInfo);//UI演示数据，正式使用需删除此行

            var busCode = "M020204";
            var data = "busCode=" + busCode + "&userSN=" + UserSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0202.STR.ReservingCrBdToFt(resObj[i]);

                    M0202.UID.M020204(dataFt);
                }
            }
            ajaxObj.start();
        },
        //已投资债权
        EVM020205: function (UserSN) {
            //var ItemInfo = { No: "U3123327", Amt: 300, AssureWay: "抵押", InvestTime: "2014/03/21", PaymentWay: "等额本息", ExpireTime: "2014/11/21", Pawn: "住宅房", FundPurpose: "日常消费", PaymentSource: "资产抵押", Borrower: "U51588454", AdviserNo: "U32151844", DayRate: 0.15 };
            //M0202.UID.M020205(ItemInfo); M0202.UID.M020205(ItemInfo); M0202.UID.M020205(ItemInfo);//UI演示数据，正式使用需删除此行

            var busCode = "M020205";
            var data = "busCode=" + busCode + "&userSN=" + UserSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0202.STR.InvestedCrBdToFt(resObj[i]);

                    M0202.UID.M020205(dataFt);
                }
            }
            ajaxObj.start();
        },
        //已结案债权
        EVM020206: function (UserSN) {
            //var ItemInfo = { No: "U3581237", Amt: 300, AssureWay: "信用", BorrowLimit: "120", PaymentWay: "等额本息", Pawn: "无", PaymentSource: "日常经营", Borrower: "U51588424", AdviserNo: "U52222222", DayRate: 0.15 };
            //M0202.UID.M020206(ItemInfo); M0202.UID.M020206(ItemInfo); M0202.UID.M020206(ItemInfo);//UI演示数据，正式使用需删除此行

            var busCode = "M020206";
            var data = "busCode=" + busCode + "&userSN=" + UserSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0202.STR.ClosedCaseCrBdToFt(resObj[i]);

                    M0202.UID.M020206(dataFt);
                }
            }
            ajaxObj.start();
        },
        //用户备注信息
        EVM020207: function (userSN) {//用户备注信息
            //M0202.UID.M020207("备注信息备注信息备注信息备注信息");

            var busCode = "M020207";
            var data = "busCode=" + busCode + "&userSN=" + userSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                res = res == null ? "" : res.trim();
                M0202.UID.M020207(res);
            }
            ajaxObj.start();
        },
        EVM020208: function (UserSN, noteText) {//UserSN为选中的用户编号,note为备注信息
            //setTimeout("M0202.UID.M020208(true);", 1000); //演示

            var busCode = "M020208";
            var data = "busCode=" + busCode + "&userSN=" + UserSN + "&note=" + noteText;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                M0202.UID.M020208(true);
            }
            ajaxObj.error = function () {
                M0202.UID.M020208(false);
            }
            ajaxObj.start();
        },
        //数据排序
        EVM020209: function (L1DERParam, Num) {
            ///<summary>数据排序</summary>
            ///<param name="L1DERParam">排序字符串</param><param name="Num">已加载的信息数据</param>
            //console.log(L1DERParam);
            //setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { M0202.UID.M020210(\"USSSS0\" + i, \"姓名\" + i,4, 5, 6, 3, 2, 3, \"609\", \"通过\", 1); };M0202.UID.M020209(false, " + Num + ");", 100);
            //M0202.UID.M020211();

            var busCode = "M020209";
            var data = "busCode=" + busCode + "&sortStr=" + L1DERParam + "&pageSize=" + Num;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                var serverStatusList = resObj.serverStatusList;

                var dataList = resObj.dataList;
                for (var i in dataList) {
                    var applyStatus;
                    if (dataList[i].applyStatus == 1) {
                        applyStatus = "审核中";
                    }
                    if (dataList[i].applyStatus == 2) {
                        applyStatus = "申请通过";
                    }
                    if (dataList[i].applyStatus == 3) {
                        applyStatus = "申请未通过";
                    }

                    var serverStauts = serverStatusList[i] == true ? 1 : 0;
                    var clientManagerSN = dataList.clientManagerSN == null ? "无" : resObj.clientManagerSN;

                    M0202.UID.M020210(dataList[i].userSN, dataList[i].name.trim(), dataList[i].cancelReserveAmount, dataList[i].refuseReserveAmount, dataList[i].reservingCrAmount, dataList[i].investedCrAmount, dataList[i].closedCaseCrAmount, dataList[i].vipLevel, clientManagerSN, applyStatus, serverStauts);
                }

                var isOver = dataList.length < Num ? true : false;

                M0202.UID.M020209(isOver, dataList.length);
            }
            ajaxObj.start();
        },
        //加载更多数据
        EVM020210: function (L1DERParam, Form, Num) {
            ///<summary>加载更多数据</summary>
            ///<param name="L1DERParam">排序字符串</param><param name="From">从第N条开始加载</param><param name="Num">加载信息的数量</param>
            console.log(L1DERParam);
            //setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { M0202.UID.M020210(\"USSSS0\" + i, \"姓名\" + i,4, 5, 6, 3, 2, 3, \"609\", \"通过\", 1); };M0202.UID.M020209(true, " + Num + ");", 500);

            var busCode = "M020210";
            var data = "busCode=" + busCode + "&sortStr=" + L1DERParam + "&pageFrom" + From + "&pageSize=" + Num;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                var serverStatusList = resObj.serverStatusList;

                var dataList = resObj.dataList;
                for (var i in dataList) {
                    var applyStatus;
                    if (dataList[i].applyStatus == 1) {
                        applyStatus = "审核中";
                    }
                    if (dataList[i].applyStatus == 2) {
                        applyStatus = "申请通过";
                    }
                    if (dataList[i].applyStatus == 3) {
                        applyStatus = "申请未通过";
                    }

                    var serverStauts = serverStatusList[i] == true ? 1 : 0;
                    var clientManagerSN = dataList.clientManagerSN == null ? "无" : resObj.clientManagerSN;

                    M0202.UID.M020210(dataList[i].userSN, dataList[i].name.trim(), dataList[i].cancelReserveAmount, dataList[i].refuseReserveAmount, dataList[i].reservingCrAmount, dataList[i].investedCrAmount, dataList[i].closedCaseCrAmount, dataList[i].vipLevel, clientManagerSN, applyStatus, serverStauts);
                }

                var isOver = dataList.length < Num ? true : false;

                M0202.UID.M020209(isOver, dataList.length);
            }
        },
        EVM020211: function (UserSN, Act) {
            ///<summary>服务状态</summary><param name="UserSN">用户编号</param><param name="Act">状态值</param>
            //console.log(UserSN);
            //setTimeout("M0202.UID.M020212(true," + Act + ")", 200);

            var busCode = "M020211";
            var data = "busCode=" + busCode + "&userSN=" + UserSN + "&status=" + Act;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function () {
                M0202.UID.M020212(true, Act);
            }
            ajaxObj.error = function () {
                M0202.UID.M020212(false);
            }
            ajaxObj.start();
        },//服务状态

        //后-前，投资方信息
        InvestorBdToFt: function (investorBdData) {
            var investorFtData = {};

            investorFtData.No = investorBdData.userSN.trim();
            investorFtData.Name = investorBdData.name.trim();
            investorFtData.BirthDay = BdDateStrFormate(investorBdData.birthday);
            investorFtData.Sex = investorBdData.gender == true ? "男" : "女";
            investorFtData.CardAddress = investorBdData.registeredResidence.trim();
            investorFtData.Mobile = investorBdData.phone.trim();
            investorFtData.CardID = investorBdData.idCard.substr(0, 6) + "*";
            investorFtData.Email = investorBdData.email == null ? "" : investorBdData.email.trim();
            investorFtData.Marry = investorBdData.maritalStatusType == null ? "" : investorBdData.maritalStatusType.trim();
            investorFtData.Breed = investorBdData.procreateStatus == false ? "未育" : "已育";
            investorFtData.ComProperty = investorBdData.enterpriseType == null ? "" : investorBdData.enterpriseType.trim();
            investorFtData.JobTime = investorBdData.hiredate == null ? "" : BdDateStrFormate(investorBdData.hiredate);
            investorFtData.JobTel = investorBdData.workTel == null ? "" : investorBdData.workTel.trim();
            investorFtData.Job = investorBdData.post == null ? "" : investorBdData.post.trim();
            investorFtData.CompanyTel = investorBdData.enterpriseSwitchboard == null ? "" : investorBdData.enterpriseSwitchboard.trim();
            investorFtData.CompanyWeb = investorBdData.enterpriseWebsite == null ? "" : investorBdData.enterpriseWebsite.trim();
            investorFtData.InvestBody = investorBdData.investMainType == null ? "" : investorBdData.investMainType.trim();
            investorFtData.BorrowBody = investorBdData.financingMain == false ? "个人" : "企业";
            investorFtData.PawnRate = investorBdData.maxMortgageRate;
            investorFtData.AssureWay = investorBdData.guaranteeType == null ? "" : investorBdData.guaranteeType.trim();
            investorFtData.InvestArea1 = investorBdData.investProvince == null ? "" : investorBdData.investProvince.trim();
            investorFtData.InvestArea2 = investorBdData.investCity == null ? "" : investorBdData.investCity.trim();
            investorFtData.InvestAmt = investorBdData.minInvestMoneyAmount;
            investorFtData.DayRate = investorBdData.minInterestRate;
            investorFtData.InvestLimitMin = investorBdData.minInvestDays;
            investorFtData.InvestLimitMax = investorBdData.maxInvestDays;
            investorFtData.Pawn = investorBdData.collateralDemand == null ? "" : investorBdData.collateralDemand.trim();
            investorFtData.LivePro = investorBdData.currentAddressProvince == null ? "" : investorBdData.currentAddressProvince.trim();
            investorFtData.LiveCity = investorBdData.currentAddressCity == null ? "" : investorBdData.currentAddressCity.trim();
            investorFtData.LiveStreet = investorBdData.currentAddressDetails == null ? "" : investorBdData.currentAddressDetails.trim();
            investorFtData.Company = investorBdData.workEnterprise == null ? "" : investorBdData.workEnterprise.trim();

            return investorFtData;
        },

        //后-前，取消债权
        CancelCrBdToFt: function (crBd) {
            var crFt = {};

            var crFt = {};
            crFt.No = crBd.creditRightSN;
            crFt.BorrowAmt = crBd.financingAmount;
            crFt.AssureWay = crBd.guaranteeType;
            crFt.BorrowLimit = crBd.loanDays;
            crFt.PaymentWay = crBd.repaymentType;
            crFt.DayRate = crBd.dailyRate;
            crFt.Pawn = crBd.collateralType == null ? "无" : crBd.collateralType;
            crFt.DebtArea1 = crBd.province;
            crFt.DebtArea2 = crBd.city;
            crFt.FundPurpose = crBd.capitalPurposeType;
            crFt.PaymentSource = crBd.repaymentSourceType;
            crFt.Borrower = crBd.financierUserSN;
            crFt.AdviserNo = crBd.consultantUserSN == null ? "无" : crBd.consultantUserSN;

            return crFt;
        },

        //后-前，拒绝债权
        RefuseCrBdToFt: function (crBd) {
            var crFt = {};

            var crFt = {};
            crFt.No = crBd.creditRightSN;
            crFt.BorrowAmt = crBd.financingAmount;
            crFt.AssureWay = crBd.guaranteeType;
            crFt.BorrowLimit = crBd.loanDays;
            crFt.PaymentWay = crBd.repaymentType;
            crFt.DayRate = crBd.dailyRate;
            crFt.Pawn = crBd.collateralType == null ? "无" : crBd.collateralType;
            crFt.DebtArea1 = crBd.province;
            crFt.DebtArea2 = crBd.city;
            crFt.FundPurpose = crBd.capitalPurposeType;
            crFt.PaymentSource = crBd.repaymentSourceType;
            crFt.Borrower = crBd.financierUserSN;
            crFt.AdviserNo = crBd.consultantUserSN == null ? "无" : crBd.consultantUserSN;

            return crFt;
        },

        //后-前，预约中债权
        ReservingCrBdToFt: function (crBd) {
            var crFt = {};

            var crFt = {};
            crFt.No = crBd.creditRightSN;
            crFt.BorrowAmt = crBd.financingAmount;
            crFt.AssureWay = crBd.guaranteeType;
            crFt.BorrowLimit = crBd.loanDays;
            crFt.PaymentWay = crBd.repaymentType;
            crFt.DayRate = crBd.dailyRate;
            crFt.Pawn = crBd.collateralType == null ? "无" : crBd.collateralType;
            crFt.DebtArea1 = crBd.province;
            crFt.DebtArea2 = crBd.city;
            crFt.FundPurpose = crBd.capitalPurposeType;
            crFt.PaymentSource = crBd.repaymentSourceType;
            crFt.Borrower = crBd.financierUserSN;
            crFt.AdviserNo = crBd.consultantUserSN == null ? "无" : crBd.consultantUserSN;

            return crFt;
        },

        //后-前，已投资债权
        InvestedCrBdToFt: function (crBd) {
            var crFt = {};

            var crFt = {};
            crFt.No = crBd.creditRightSN;
            crFt.Amt = crBd.investMoneyAmount;
            crFt.AssureWay = crBd.guaranteeType;
            crFt.InvestTime = BdDateStrFormate(crBd.investDate);
            crFt.PaymentWay = crBd.repaymentType;
            crFt.ExpireTime = BdDateStrFormate(crBd.deadlineDate);
            crFt.DayRate = crBd.dailyRate;
            crFt.Pawn = crBd.collateralType == null ? "无" : crBd.collateralType;
            crFt.FundPurpose = crBd.capitalPurposeType;
            crFt.PaymentSource = crBd.repaymentSourceType;
            crFt.Borrower = crBd.financierUserSN;
            crFt.AdviserNo = crBd.consultantUserSN == null ? "无" : crBd.consultantUserSN;

            return crFt;
        },

        //后-前，已结案债权
        ClosedCaseCrBdToFt: function (crBd) {
            var crFt = {};

            var crFt = {};
            crFt.No = crBd.creditRightSN;
            crFt.Amt = crBd.investMoneyAmount;
            crFt.AssureWay = crBd.guaranteeType;
            crFt.BorrowLimit = crBd.loanDays;
            crFt.PaymentWay = crBd.repaymentType;
            crFt.DayRate = crBd.dailyRate;
            crFt.Pawn = crBd.collateralType == null ? "无" : crBd.collateralType;
            crFt.PaymentSource = crBd.repaymentSourceType;
            crFt.Borrower = crBd.financierUserSN;
            crFt.AdviserNo = crBd.consultantUserSN == null ? "无" : crBd.consultantUserSN;

            return crFt;
        },
    },
    UID: {
        M020210: function (c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11) {
            var NewRow = document.querySelector(".TbList .Template").cloneNode(true);
            NewRow.removeAttribute("class");
            var NewRowTDs = NewRow.querySelectorAll("td");
            NewRowTDs[0].innerText = c1; NewRowTDs[1].innerText = c2; NewRowTDs[2].innerText = c3; NewRowTDs[3].innerText = c4; NewRowTDs[4].innerText = c5;
            NewRowTDs[5].innerText = c6; NewRowTDs[6].innerText = c7; NewRowTDs[7].innerText = c8; NewRowTDs[8].innerText = c9; NewRowTDs[10].innerText = c10;
            NewRowTDs[11].children[0].setAttribute('data-sw', c11);
            document.querySelector(".TbList>tbody").appendChild(NewRow);
        },
        M020201: function (V) {
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Name; TDs[2].DspV = V.BirthDay; TDs[3].DspV = V.Sex; TDs[4].DspV = V.CardAddress; TDs[5].DspV = V.CardID; TDs[6].DspV = V.Mobile; TDs[7].DspV = V.Email; TDs[8].DspV = V.Marry; TDs[9].DspV = V.Breed; TDs[10].DspV = V.LivePro + " " + V.LiveCity + " " + V.LiveStreet;
            TDs[11].DspV = V.InvestBody; TDs[12].DspV = V.BorrowBody; TDs[13].querySelector("label").DspV = V.PawnRate; TDs[14].DspV = V.AssureWay; TDs[15].querySelectorAll("label")[0].DspV = V.InvestArea1; TDs[15].querySelectorAll("label")[1].DspV = V.InvestArea2; TDs[16].querySelector("label").DspV = V.InvestAmt; TDs[17].querySelector("label").DspV = V.DayRate; TDs[18].querySelectorAll("label")[0].DspV = V.InvestLimitMin; TDs[18].querySelectorAll("label")[1].DspV = V.InvestLimitMax;
            TDs[19].DspV = V.Company; TDs[20].DspV = V.ComProperty; TDs[21].DspV = V.JobTime; TDs[22].DspV = V.JobTel; TDs[23].DspV = V.Job; TDs[24].DspV = V.CompanyTel; TDs[25].DspV = V.CompanyWeb; 
            var PawnSlt = V.Pawn.split(",");
            for (var i = 0; i < PawnSlt.length; i++) { if (PawnSlt[i].length >= 2) { NewBox.querySelector("[data-id='" + PawnSlt[i] + "']").setAttribute("data-sw", "1"); } }
            FBoxContainer.appendChild(NewBox);
        },
        M020202: function (V) {
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.BorrowAmt; TDs[2].DspV = V.AssureWay; TDs[3].DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.Pawn; TDs[7].querySelectorAll("label")[0].DspV = V.DebtArea1; TDs[7].querySelectorAll("label")[1].DspV = V.DebtArea2; TDs[8].DspV = V.FundPurpose; TDs[9].DspV = V.PaymentSource; TDs[10].DspV = V.Borrower; TDs[11].DspV = V.AdviserNo;
            FBoxContainer.appendChild(NewBox);
        },
        M020203: function (V) {
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.BorrowAmt; TDs[2].DspV = V.AssureWay; TDs[3].DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.Pawn; TDs[7].querySelectorAll("label")[0].DspV = V.DebtArea1; TDs[7].querySelectorAll("label")[1].DspV = V.DebtArea2; TDs[8].DspV = V.FundPurpose; TDs[9].DspV = V.PaymentSource; TDs[10].DspV = V.Borrower; TDs[11].DspV = V.AdviserNo;
            FBoxContainer.appendChild(NewBox);
        },
        M020204: function (V) {
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.BorrowAmt; TDs[2].DspV = V.AssureWay; TDs[3].DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.Pawn; TDs[7].querySelectorAll("label")[0].DspV = V.DebtArea1; TDs[7].querySelectorAll("label")[1].DspV = V.DebtArea2; TDs[8].DspV = V.FundPurpose; TDs[9].DspV = V.PaymentSource; TDs[10].DspV = V.Borrower; TDs[11].DspV = V.AdviserNo;
            FBoxContainer.appendChild(NewBox);
        },
        M020205: function (V) {
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].querySelector("label").DspV = V.Amt; TDs[2].DspV = V.AssureWay; TDs[3].DspV = V.InvestTime; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.ExpireTime; TDs[6].DspV = V.FundPurpose; TDs[7].DspV = V.PaymentSource; TDs[8].DspV = V.Pawn; TDs[9].DspV = V.DayRate; TDs[10].DspV = V.Borrower; TDs[11].DspV = V.AdviserNo;
            FBoxContainer.appendChild(NewBox);
        },
        M020206: function (V) {
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].querySelector("label").DspV = V.Amt; TDs[2].DspV = V.AssureWay; TDs[3].DspV = V.BorrowLimit; TDs[4].DspV = V.PaymentWay; TDs[5].DspV = V.DayRate; TDs[6].DspV = V.Pawn; TDs[7].DspV = V.PaymentSource; TDs[8].DspV = V.Borrower; TDs[9].DspV = V.AdviserNo;
            FBoxContainer.appendChild(NewBox);
        },
        M020207: function (txt) {
            var TTA = MVB.MBC.querySelector("div>textarea");
            TTA.value = txt;
        },
        M020208: function (res) {
            MVB.Close();
            if (res == true) {
                TransingStatus.SetStatus(3);
            }
            else {
                TransingStatus.SetStatus(2);
            }
        },
        M020209 :function (IsLast, Num) {
            ///<summary>数据加载完毕后执行</summary><param name="IsLast">是否是最后一条数据;true:是;false:否;</param><param name="Num">加载信息的数量</param>
            M0202.LoadParam.From += Num;
            Continue.innerText = 1;
            if (IsLast) { LastItem = 1; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 0); } else { LastItem = 0; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 1); }
            LoadingBoxCtl(0);
        },//数据加载完毕后执行
        M020211: function () {
            ///<summary>加载抵押类型</summary>
            Str = " <table style=\"width:100%\"><tr>"; var s = 0;
            for (var i in M0202.UIP.Pawn) {
                if (M0202.UIP.Pawn[i].Name != "混合资产") {
                    s++;
                    Str += "<td><div class=\"UpDownButton\" data-sw='0' data-id=\"" + M0202.UIP.Pawn[i].ID + "\"></div>" + M0202.UIP.Pawn[i].Name + "</td>";
                    if (s % 3 == 0) { Str += "</tr><tr>"; }
                }
            }
            Str += "</tr></table>";
            document.querySelector(".TP1>.FBoxContainer>.Template .Pawn").innerHTML = Str;
        },
        M020212: function (Res, Act) {
            ///<summary>修改服务状态</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
                M0202.FocusItem.querySelector(".SlideButton").setAttribute("data-sw", Act);
            } else {
                TransingStatus.SetStatus(2);
            }
        }
    }
};