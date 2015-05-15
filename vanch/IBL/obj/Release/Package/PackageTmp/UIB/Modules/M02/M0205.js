// JavaScript source code
var M0205 = {
    FocusRow: null,
    L1DERParam: ",",
    Keyword: "无",
    FirstLoadNum:35,
    LoadParam: { From: 0, Num: 10 },
    Init: function () {
        LoadingBoxCtl(1);
        this.STR.EVM020501(this.FirstLoadNum);
    },
    //加载更多数据
    LoadMoreInfo: function () {
        if (parseInt(Continue.innerText) == 1) {
            Continue.innerText = 0;
            M0205.STR.EVM020503(M0205.Keyword, M0205.LoadParam.From, M0205.LoadParam.Num);
        }
    },
    Button: function (Sender) {
        this.SltItem(Sender);
    },
    GetFocusUserNum: function () {
        return this.FocusItem.parentElement.querySelectorAll("td")[0].innerText;
    },
    HButton: function (Sender) {
        var BtID = parseInt(Sender.getAttribute("data-btid"));
        switch (BtID) {
            case 1: if (Sender.getAttribute("data-ft") == "0") { Sender.setAttribute("data-ft", "1"); Sender.querySelector("input").focus(); } else { Sender.setAttribute("data-ft", "0"); this.SearchManager(Sender.querySelector("input").value); } break;
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
        switch (Sender.getAttribute("data-ft")) {
            case "2": Sender.setAttribute("data-ft", "3"); Str += ID + "#A,"; break;
            case "3": Sender.setAttribute("data-ft", "4"); Str += ID + "#D,"; break;
            case "4": Sender.setAttribute("data-ft", "2"); break;
            default: break;
        }
        return Str;
    },
    SltItem: function (TheItem) {
        if (this.FocusItem != null && TheItem != this.FocusItem) { this.FocusItem.setAttribute("data-slt", 0); };
        if (TheItem == null) { this.FocusItem.setAttribute("data-slt", 0); return; };
        this.FocusItem = TheItem;
        if (this.FocusItem.getAttribute("data-slt") == "1") { this.FocusItem.setAttribute("data-slt", 0); }
        else {
            this.FocusItem.setAttribute("data-slt", 1);
            var BtID = parseInt(TheItem.getAttribute("data-btid"));
            switch (BtID) {
                case 1: this.DspList(this.FocusItem.getAttribute("data-btid")); break;
                case 2: this.DspMemo(this.FocusItem.getAttribute("data-btid")); break;
            }
        };
    },
    L1Filter: function () {
        LoadingBoxCtl(1);
        var NewRow = document.querySelector(".T1>.TbList>tbody>.Template").cloneNode(true);
        document.querySelector(".MovingBox>.Container>.T1>.TbList>tbody").innerHTML = "";
        document.querySelector(".MovingBox>.Container>.T1>.TbList>tbody").appendChild(NewRow);
        this.STR.EVM020505(this.L1DERParam);
    },
    SearchManager: function (Key) {
        LoadingBoxCtl(1);this.Keyword = Key;
        var NewRow = document.querySelector(".TbList .Template").cloneNode(true);
        document.querySelector(".TbList>tbody").innerHTML = "";
        document.querySelector(".TbList>tbody").appendChild(NewRow);
        this.STR.EVM020506(Key, this.FirstLoadNum);
    },
    DspList: function () {
        MVB.ContainerWidth = 600;
        M0205.L1DERParam = ",";
        MVB.Template = document.querySelector(".Template>.T1");
        var MVBTitle = "客户经理分派";
        MVB.Buttons = ["确定", "M0205.ConfirmMgr();"];
        MVB.Open(document.querySelector(".ListContainer"), "M0205.SltItem(null)", MVBTitle);
        this.STR.EVM020502();
    },
    SltMgrRow: function (TheItem) {
        if (this.FocusRow != null && TheItem != this.FocusRow) { this.FocusRow.setAttribute("data-slt", 0); };
        if (TheItem == null) { this.FocusRow.setAttribute("data-slt", 0); this.FocusRow = TheItem; document.querySelector(".MovingBox>.ButtonBar>div[data-btst]").setAttribute("data-btst", "D"); return; };
        this.FocusRow = TheItem;
        if (this.FocusRow.getAttribute("data-slt") == "1") { this.FocusRow.setAttribute("data-slt", 0); document.querySelector(".MovingBox>.ButtonBar>div[data-btst]").setAttribute("data-btst", "D"); }
        else { this.FocusRow.setAttribute("data-slt", 1); document.querySelector(".MovingBox>.ButtonBar>div[data-btst]").setAttribute("data-btst", "E"); };
    },
    ConfirmMgr: function () {
        TransingStatus.SetStatus(1);
        var MgrSN = this.FocusRow.querySelectorAll("td")[0].innerText, UserSN = this.GetFocusUserNum();
        M0205.STR.EVM020504(MgrSN, UserSN);
    },
    UIP: {
        P1: null,//融资发布金额最小值
        P2: null,//融资发布金额最小值
    },//UI操作需预置的基础参数
    STR: {
        //初始化
        EVM020501: function (Num) {
            ///<summary>一切就绪</summary><param name="Num">首次加载信息的数量</param>
            //var Info = { UserSN: "U0001", Name: "X", Regdate: "2014/01/01", IDCardSN: "330000000000", BirthDay: "1980/08/08", Sex: "男", IDCardAddress: "浙江宁波", Mobile: "1377777777", Email: "123@qq.com", Marry: "已婚", Breed: "已育", Edu: "大专", MonthIn: 100000, IS: "1", BS: "1", AS: "0", ABS: "1", ASS: "1", Manager: "无" }
            //for (var i = 1 ; i <= Num; i++) { M0205.UID.M020503(Info); };
            //M0205.UID.M020501(false, Num);

            var busCode = "M020501";
            var data = "busCode=" + busCode + "&pageSize=" + Num;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0205.STR.UserDataBdToFt(resObj[i]);
                    M0205.UID.M020503(dataFt);
                }

                var isOver = res.length == Num ? true : false;
                M0205.UID.M020501(isOver, Num);
            }
            ajaxObj.start();
        },
        //客户经理列表
        EVM020502: function () {
            ///<summary>客户经理列表</summary>
            //var Info = { StaffSN: "U0001", Name: "X", InDate: "2014/01/01", ClientNum: 7, InvestNum: 2, BorrowNum: 2, AdvisorNum: 1, SaleNum: 1, BuyNum: 1 }
            //for (var i = 1 ; i <= 25; i++) { M0205.UID.M020502(Info); };

            var busCode = "M020502";
            var data = "busCode=" + busCode;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0205.STR.ClientManagerBdToFt(resObj[i]);
                    M0205.UID.M020502(dataFt);
                }
            }
            ajaxObj.start();
        },
        //主列表我要加载更多
        EVM020503: function (Key, endrowid, rows) {
            //主列表我要加载更多 rows:数据行数
            //var Info = { UserSN: "U0001", Name: "X", Regdate: "2014/01/01", IDCardSN: "330000000000", BirthDay: "1980/08/08", Sex: "男", IDCardAddress: "浙江宁波", Mobile: "1377777777", Email: "123@qq.com", Marry: "已婚", Breed: "已育", Edu: "大专", MonthIn: 100000, IS: "1", BS: "1", AS: "0", ABS: "1", ASS: "1", Manager: Key }
            //setTimeout("for (var i = 1 ; i <= " + rows + "; i++) { M0205.UID.M020503(" + JSON.stringify(Info) + "); };M0205.UID.M020501(true, " + rows + ");", 500);
            var busCode = "M020503";
            var data = "busCode=" + busCode + "&filterStr=" + Key + "&pageFrom" + endrowid + "&pageSize=" + Num;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0205.STR.UserDataBdToFt(resObj[i]);
                    M0205.UID.M020503(dataFt);
                }

                var isOver = res.length == Num ? true : false;
                M0205.UID.M020501(isOver, Num);
            }
            ajaxObj.start();
        },
        EVM020504: function (MgrSN, UserSN) {
            //setTimeout("M0205.UID.M020504(true,'" + MgrSN + "');", 500);
            ///<summary>客户经理列表按确认后</summary>

            var busCode = "M020504";
            var data = "busCode=" + busCode + "&userSN=" + UserSN + "&workNum=" + MgrSN.trim();
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                if (res == "0") {
                    M0205.UID.M020504(true, MgrSN);
                }
                else {
                    M0205.UID.M020504(false, MgrSN);
                }
            }
            ajaxObj.start();
        },
        //数据排序
        EVM020505: function (L1DERParam) {
            ///<summary>数据排序</summary>
            ///<param name="L1DERParam">排序字符串</param>
            //var Info = { StaffSN: "U0001", Name: "X", InDate: "2014/01/01", ClientNum: 7, InvestNum: 2, BorrowNum: 2, AdvisorNum: 1, SaleNum: 1, BuyNum: 1 }
            //setTimeout("for (var i = 1 ; i <= 15; i++) { M0205.UID.M020502(" + JSON.stringify(Info) + "); };M0205.UID.M020505();", 500);

            var busCode = "M020505";
            var data = "busCode=" + busCode + "&sortStr=" + L1DERParam;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0205.STR.ClientManagerBdToFt(resObj[i]);
                    M0205.UID.M020502(dataFt);
                }
            }
            ajaxObj.start();
        },
        //客户经理搜索
        EVM020506: function (Key, Num) {
            ///<summary>客户经理搜索</summary>
            ///<param name="Key">关键字</param><param name="Num">数据加载数量</param>
            //var Info = { UserSN: "U0001", Name: "X", Regdate: "2014/01/01", IDCardSN: "330000000000", BirthDay: "1980/08/08", Sex: "男", IDCardAddress: "浙江宁波", Mobile: "1377777777", Email: "123@qq.com", Marry: "已婚", Breed: "已育", Edu: "大专", MonthIn: 100000, IS: "1", BS: "1", AS: "0", ABS: "1", ASS: "1", Manager: Key }
            //setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { M0205.UID.M020503(" + JSON.stringify(Info) + "); };M0205.UID.M020501(false, " + Num + ");", 500);

            var busCode = "M020506";
            var data = "busCode=" + busCode + "&filterStr=" + Key + "&pageSize=" + Num;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0205.STR.UserDataBdToFt(resObj[i]);
                    M0205.UID.M020503(dataFt);
                }

                var isOver = res.length == Num ? true : false;
                M0205.UID.M020501(isOver, Num);
            }
            ajaxObj.start();
        },

        //后-前 初始化
        UserDataBdToFt: function (dataBd) {
            var dataFt = {};

            dataFt.UserSN = dataBd.userSN;
            dataFt.Name = dataBd.name;
            dataFt.Regdate = BdDateStrFormate(dataBd.registerDate);
            dataFt.BirthDay = BdDateStrFormate(dataBd.birthday);
            dataFt.IDCardSN = dataBd.idCard.substr(0, 6) + "*";
            dataFt.Sex = dataBd.gender == true ? "男" : "女";
            dataFt.IDCardAddress = dataBd.registeredResidence;
            dataFt.Mobile = dataBd.phone;
            dataFt.Email = dataBd.email == null ? "无" : dataBd.email;
            dataFt.Marry = dataBd.maritalStatusType == null ? "无" : dataBd.maritalStatusType;
            dataFt.Breed = dataBd.procreateStatus == true ? "已育" : "未育";
            dataFt.Edu = dataBd.degreeType == null ? "无" : dataBd.degreeType;
            dataFt.MonthIn = dataBd.monthlyTotalIncome == null ? "无" : dataBd.monthlyTotalIncome;
            dataFt.IS = dataBd.creditRightInvestStatus == true ? "1" : "0";
            dataFt.BS = dataBd.creditRightFinancingStatus == true ? "1" : "0";
            dataFt.AS = dataBd.consultantStatus == true ? "1" : "0";
            dataFt.ABS = dataBd.assetsPruchaseStatus == true ? "1" : "0";
            dataFt.ASS = dataBd.assetsSellingStatus == true ? "1" : "0";
            dataFt.Manager = dataBd.work_num == null ? "无" : dataBd.work_num;

            return dataFt;
        },

        //后-前 客户经理列表
        ClientManagerBdToFt: function (dataBd) {
            var dataFt = {};

            //dataFt.StaffSN = dataBd.internalUserSN;
            dataFt.StaffSN = dataBd.workNum;
            dataFt.Name = dataBd.name == null ? "" : dataBd.name.trim();
            dataFt.InDate = dataBd.reg_date == null ? "" : BdDateStrFormate(dataBd.reg_date);
            dataFt.ClientNum = dataBd.clientTotalAmount;
            dataFt.InvestNum = dataBd.financierAmount;
            dataFt.BorrowNum = dataBd.investorAmount;
            dataFt.AdvisorNum = dataBd.consultantAmount;
            dataFt.SaleNum = dataBd.sellerAmount;
            dataFt.BuyNum = dataBd.purchaserAmount

            return dataFt;
        }
    },
    UID: {
        M020501: function (IsLast, Num) {
            ///<summary>数据加载完毕后执行</summary><param name="IsLast">是否是最后一条数据;true:是;false:否;</param><param name="Num">加载信息的数量</param>
            M0205.LoadParam.From += Num;
            Continue.innerText = 1;
            if (IsLast) { LastItem = 1; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 0); } else { LastItem = 0; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 1); }
            LoadingBoxCtl(0);
        },//数据加载完毕后执行
        M020502: function (RowData) {
            ///<summary>客户经理列表添加行</summary><param name="Data">行数据</param>
            var NewRow = document.querySelector(".T1>.TbList>tbody>.Template").cloneNode(true);
            NewRow.removeAttribute("class");
            var RowCells = NewRow.querySelectorAll("td");
            RowCells[0].innerText = RowData.StaffSN;
            RowCells[1].innerText = RowData.Name;
            RowCells[2].innerText = RowData.InDate;
            RowCells[3].innerText = RowData.ClientNum;
            RowCells[4].innerText = RowData.BorrowNum;
            RowCells[5].innerText = RowData.InvestNum;
            RowCells[6].innerText = RowData.AdvisorNum;
            RowCells[7].innerText = RowData.SaleNum;
            RowCells[8].innerText = RowData.BuyNum;
            document.querySelector(".MovingBox>.Container>.T1>.TbList>tbody").appendChild(NewRow);
        },
        M020503: function (RowData) {
            ///<summary>主列表添加行</summary>
            var NewRow = document.querySelector(".TbList .Template").cloneNode(true);
            NewRow.removeAttribute("class");
            var RowCells = NewRow.querySelectorAll("td");
            RowCells[0].innerText = RowData.UserSN; RowCells[0].setAttribute("data-userid", RowData.UserSN);
            RowCells[1].innerText = RowData.Name;
            RowCells[2].innerText = RowData.Regdate;
            RowCells[3].innerText = RowData.IDCardSN;
            RowCells[4].innerText = RowData.BirthDay;
            RowCells[5].innerText = RowData.Sex;
            RowCells[6].innerText = RowData.IDCardAddress;
            RowCells[7].innerText = RowData.Mobile;
            RowCells[8].innerText = RowData.Email;
            RowCells[9].innerText = RowData.Marry;
            RowCells[10].innerText = RowData.Breed;
            RowCells[11].innerText = RowData.Edu;
            RowCells[12].innerText = RowData.MonthIn;
            RowCells[14].innerText = RowData.Manager;
            if (RowData.IS == "1") { RowCells[13].innerText += " 投 "; }
            if (RowData.BS == "1") { RowCells[13].innerText += " 融 "; }
            if (RowData.AS == "1") { RowCells[13].innerText += " 顾 "; }
            if (RowData.ABS == "1") { RowCells[13].innerText += " 买 "; }
            if (RowData.ASS == "1") { RowCells[13].innerText += " 卖 "; }
            document.querySelector(".TbList>tbody").appendChild(NewRow);
        },
        M020504: function (Res, MgrSN) {
            if (Res) { TransingStatus.SetStatus(3); M0205.SltMgrRow(null); MVB.Close(); M0205.FocusItem.setAttribute("data-userid", MgrSN); M0205.FocusItem.innerText = MgrSN; }
            else { TransingStatus.SetStatus(2); M0205.SltMgrRow(null); }
            
        },
        M020505: function () { LoadingBoxCtl(0); },
    }
};