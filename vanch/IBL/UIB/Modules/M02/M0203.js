// JavaScript source code
var M0203 = {
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
            M0203.STR.EVM020310(M0203.L1DERParam, M0203.LoadParam.From, M0203.LoadParam.Num);
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
        };
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
        this.STR.EVM020303(M0203.L1DERParam, this.FirstLoadNum);
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
        M0203.STR.EVM020311(this.GetFocusUserNum(), Switch);
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
        MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0203.SltItem(null)", MVBTitle);
    },
    DspList: function (BtID) {
        MVB.Template = document.querySelector(".TP" + BtID).children[0];
        var containerObj = document.querySelector('#MainZone .ListContainer');
        switch (BtID) {
            case "1": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0203.SltItem(null)", "用户信息"); this.STR.EVM020304(this.GetFocusUserNum()); break;
            case "2": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0203.SltItem(null)", "用户" + this.GetFocusUserNum() + "-取消预约"); this.STR.EVM020305(this.GetFocusUserNum()); break;
            case "3": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0203.SltItem(null)", "用户" + this.GetFocusUserNum() + "-预约中资产"); this.STR.EVM020306(this.GetFocusUserNum()); break;
            case "4": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0203.SltItem(null)", "用户" + this.GetFocusUserNum() + "-已购买资产"); this.STR.EVM020307(this.GetFocusUserNum()); break;
            case "5": MVB.ContainerWidth = 500; MVB.Buttons = ["保存", "M0203.ClkSaveNote();"]; MVB.Open(containerObj, "M0203.SltItem(null)", "用户" + this.GetFocusUserNum() + "-备注信息"); this.STR.EVM020308(this.GetFocusUserNum()); break;
        }
    },
    EnableBt: function (bt) {
        var btObj = document.querySelector(".MovingBox>.ButtonBar").children[1];
        btObj.setAttribute("data-btst", "E");
    },
    ClkSaveNote: function () {
        TransingStatus.SetStatus(1);
        var noteText = document.querySelector(".MovingBox>.Container textarea").innerText;
        this.STR.EVM020309(this.GetFocusUserNum(), noteText);
    },
    Details: function (Obt) {
        ///<summary>查看资产详情</summary>
        this.FocusBox = Obt.parentElement.parentElement.parentElement.parentElement;
        var Texterea = this.FocusBox.querySelector(".Textarea");
        if (Texterea.getAttribute("data-sw") == "1") { Texterea.setAttribute("data-sw", "0"); } else { Texterea.setAttribute("data-sw", "1"); }
    },

    UIP: {
        AssetType: [{ ID: "A01", Name: "住宅房" }, { ID: "A02", Name: "办公楼" }, { ID: "A03", Name: "商铺" }, { ID: "A04", Name: "工业用地" }, { ID: "A05", Name: "商业用地" }, { ID: "A06", Name: "汽车" }, { ID: "A07", Name: "贵金属" }, { ID: "A08", Name: "收藏品" }, { ID: "A09", Name: "股权" }, { ID: "A10", Name: "债券" }, { ID: "A11", Name: "混合资产" }, { ID: "A12", Name: "厂房" }, { ID: "A13", Name: "其它" }],
    },
    STR: {
        EVINIT: function (Num) {
            ///<summary>初始化页面</summary><param name="Num">首次加载信息的数量</param>
            //setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { M0203.UID.M020301(\"USSSS0\" + i, \"姓名\" + i, 1, 4, 5, 3, \"609\", \"通过\", 1); };M0203.UID.M020302();M0203.UID.M020303(false, " + Num + ");", 500);

            var busCode = "M0203INIT";
            var data = "busCode=" + busCode + "&pageSize=" + Num;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                M0203.UIP.AssetType = resObj.pawn;
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

                    M0203.UID.M020301(dataList[i].userSN, dataList[i].name.trim(), dataList[i].cancelReserveAmount, dataList[i].reservingAmount, dataList[i].purchasedAssetsAmount, dataList[i].vipLevel, clientManagerWorkNum, applyStatus, serverStauts);
                }

                var isOver = dataList.length < Num ? true : false;

                M0203.UID.M020303(isOver, dataList.length);
                M0203.UID.M020302();
            }
            ajaxObj.start();
        },
        //数据排序
        EVM020303: function (L1DERParam, Num) {
            ///<summary>数据排序</summary>
            ///<param name="L1DERParam">排序字符串</param><param name="Num">已加载的信息数据</param>
            console.log(L1DERParam);
            //setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { M0203.UID.M020301(\"USSSS0\" + i, \"姓名\" + i, 1, 4, 5, 3, \"609\", \"通过\", 1); };M0203.UID.M020302();M0203.UID.M020303(false, " + Num + ");", 500);

            var busCode = "M020303";
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

                    M0203.UID.M020301(dataList[i].userSN, dataList[i].name.trim(), dataList[i].cancelReserveAmount, dataList[i].reservingAmount, dataList[i].purchasedAssetsAmount, dataList[i].vipLevel, clientManagerSN, applyStatus, serverStauts);
                }

                var isOver = dataList.length < Num ? true : false;

                M0203.UID.M020303(isOver, dataList.length);
            }
            ajaxObj.start();
        },
        //用户信息
        EVM020304: function (UserSN) {
            //var UserInfo = { No: "U20156065", Name: "张学友", BirthDay: "1965/08/09", Sex: "男", CardAddress: "中国香港", CardID: "789454*", Mobile: "170****6666", Email: "zhangxueyou@qq.com", Marry: "已婚", Breed: "已育", LivePro: "浙江", LiveCity: "宁波", LiveStreet: "高新区", AssetArea: "浙江", AmtMin: "1", AmtMax: "9999", AssetType: ",A01,A03,A05,A08,A09,A10," };
            //M0203.UID.M020304(UserInfo); //UI演示数据，正式使用需删除此行

            var busCode = "M020304";
            var data = "busCode=" + busCode + "&userSN=" + UserSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                var dataFt = M0203.STR.PurchaserBdToFt(resObj);
                M0203.UID.M020304(dataFt);
            }
            ajaxObj.start();
        },
        //取消预约
        EVM020305: function (userSN) {
            //var ItemInfo = { No: "U20164811", Type: "汽车", Source: "购买", GetAmt: 100, Use: "自用", Appraiser: "任达华", MarketAmt: 120, AssetArea: "浙江", Saler: "U20156065", SaleAmt: 105, GetTime: "2014/07/03", Ramerks: "保时捷-卡宴 里程20万公里，2011年买进，改装费20万，最高时间可达268公里/小时！" };
            //M0203.UID.M020305(ItemInfo); M0203.UID.M020305(ItemInfo); M0203.UID.M020305(ItemInfo);

            var busCode = "M020305";
            var data = "busCode=" + busCode + "&userSN=" + userSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0203.STR.CancelReservedAssetsBdToFt(resObj[i]);
                    M0203.UID.M020305(dataFt);
                }
            }
            ajaxObj.start();
        },
        //预约中资产
        EVM020306: function (userSN) {
            //var ItemInfo = { No: "U20164811", Type: "汽车", Source: "购买", GetAmt: 100, Use: "自用", Appraiser: "任达华", MarketAmt: 120, AssetArea: "浙江", Saler: "U20156065", SaleAmt: 105, GetTime: "2014/07/03", Ramerks: "保时捷-卡宴 里程20万公里，2011年买进，改装费20万，最高时间可达268公里/小时！" };
            //M0203.UID.M020306(ItemInfo); M0203.UID.M020306(ItemInfo); M0203.UID.M020306(ItemInfo);

            var busCode = "M020306";
            var data = "busCode=" + busCode + "&userSN=" + userSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0203.STR.ReservingAssetsBdToFt(resObj[i]);
                    M0203.UID.M020306(dataFt);
                }
            }
            ajaxObj.start();
        },
        //已购买资产
        EVM020307: function (userSN) {
            //var ItemInfo = { No: "U20164813", Type: "汽车", Source: "购买", GetAmt: 100, BuyAmt: "133", Appraiser: "任达华", MarketAmt: 120, AssetArea: "浙江", Saler: "U20156065", SaleAmt: 105, GetTime: "2014/07/03", Ramerks: "保时捷-卡宴 里程20万公里，2011年买进，改装费20万！" };
            //M0203.UID.M020307(ItemInfo); M0203.UID.M020307(ItemInfo); M0203.UID.M020307(ItemInfo);

            var busCode = "M020307";
            var data = "busCode=" + busCode + "&userSN=" + userSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0203.STR.PurchasedAssetsBdToFt(resObj[i]);
                    M0203.UID.M020307(dataFt);
                }
            }
            ajaxObj.start();
        },
        EVM020308: function (userSN) {//备注
            //M0203.UID.M020308("这是备注的测试数据");

            var busCode = "M020308";
            var data = "busCode=" + busCode + "&userSN=" + userSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                res = res == null ? "" : res.trim();
                M0203.UID.M020308(res);
            }
            ajaxObj.start();
        },
        EVM020309: function (UserSN, noteText) {//UserSN为选中的用户编号,note为备注信息
            //setTimeout("M0203.UID.M020309(true);", 1000); //演示

            var busCode = "M020309";
            var data = "busCode=" + busCode + "&userSN=" + UserSN + "&note=" + noteText;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                M0203.UID.M020309(true);
            }
            ajaxObj.start();
        },
        //加载更多数据
        EVM020310: function (L1DERParam, Form, Num) {
            ///<summary>加载更多数据</summary>
            ///<param name="L1DERParam">排序字符串</param><param name="From">从第N条开始加载</param><param name="Num">加载信息的数量</param>
            console.log(L1DERParam);
            //setTimeout("for (var i = 1 ; i <= " + Num + "; i++) { M0203.UID.M020301(\"USSSS0\" + i, \"姓名\" + i, 1, 4, 5, 3, \"609\", \"通过\", 1);  };M0203.UID.M020302();M0203.UID.M020303(true, " + Num + ");", 500);

            var busCode = "M020310";
            var data = "busCode=" + busCode + "&sortStr=" + L1DERParam + "&pageFrom=" + Form + "&pageSize=" + Num;
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

                    M0203.UID.M020301(dataList[i].userSN, dataList[i].name.trim(), dataList[i].cancelReserveAmount, dataList[i].reservingAmount, dataList[i].purchasedAssetsAmount, dataList[i].vipLevel, clientManagerSN, applyStatus, serverStauts);
                }

                var isOver = dataList.length < Num ? true : false;

                M0203.UID.M020303(isOver, dataList.length);
            }
            ajaxObj.start();
        },
        EVM020311: function (UserSN, Act) {
            ///<summary>服务状态</summary><param name="UserSN">用户编号</param><param name="Act">状态值</param>
            //console.log(UserSN);
            //setTimeout("M0203.UID.M020310(true," + Act + ")", 200);

            var busCode = "M020311";
            var data = "busCode=" + busCode + "&userSN=" + UserSN + "&status=" + Act;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function () {
                M0203.UID.M020310(true, Act);
            }
            ajaxObj.start();
        },//服务状态

        //后-前，购买方
        PurchaserBdToFt: function (dataBd) {
            var dataFt = {};

            dataFt.No = dataBd.userSN;
            dataFt.Name = dataBd.name == null ? "" : dataBd.name.trim();
            dataFt.Sex = dataBd.gender == true ? "男" : "女";
            dataFt.BirthDay = BdDateStrFormate(dataBd.birthday);
            dataFt.CardID = dataBd.idCard;
            dataFt.Mobile = dataBd.phone;
            dataFt.CardAddress = dataBd.registeredResidence.trim();
            dataFt.Email = dataBd.email == null ? "" : dataBd.email.trim();
            dataFt.Marry = dataBd.maritalStatusType.trim();
            dataFt.Breed = dataBd.procreateStatus == true ? "已育" : "未育";
            dataFt.LivePro = dataBd.currentAddressProvince.trim();
            dataFt.LiveCity = dataBd.currentAddressCity == null ? "" : dataBd.currentAddressCity.trim();
            dataFt.LiveStreet = dataBd.currentAddressDetails == null ? "" : dataBd.currentAddressDetails.trim();
            dataFt.AssetArea = dataBd.assetsProvince;
            dataFt.AmtMin = dataBd.minPurchasePrice;
            dataFt.AmtMax = dataBd.maxPurchasePrice;
            dataFt.AssetType = dataBd.assetsType == null ? "," : dataBd.assetsType.trim();

            return dataFt;
        },

        //后-前，取消预约资产
        CancelReservedAssetsBdToFt: function (dataBd) {
            var dataFt = {};

            dataFt.No = dataBd.assetsSN;
            dataFt.Type = dataBd.assetsType.trim();
            dataFt.Source = dataBd.assetsSourceType.trim();
            dataFt.AssetArea = dataBd.assetsProvince.trim();
            dataFt.Use = dataBd.useStatusType.trim();
            dataFt.GetAmt = dataBd.cost;
            dataFt.Saler = dataBd.receiverUserSN;
            dataFt.Appraiser = dataBd.assessmentAgency.trim();
            dataFt.MarketAmt = dataBd.marketPrice;
            dataFt.SaleAmt = dataBd.sellPrice;
            dataFt.GetTime = BdDateStrFormate(dataBd.acquireDate);
            dataFt.Ramerks = dataBd.details == null ? "" : dataBd.details.trim();

            return dataFt;
        },

        //后-前，预约中资产
        ReservingAssetsBdToFt: function (dataBd) {
            var dataFt = {};

            dataFt.No = dataBd.assetsSN;
            dataFt.Type = dataBd.assetsType.trim();
            dataFt.Source = dataBd.assetsSourceType.trim();
            dataFt.AssetArea = dataBd.assetsProvince.trim();
            dataFt.Use = dataBd.useStatusType.trim();
            dataFt.GetAmt = dataBd.cost;
            dataFt.Saler = dataBd.receiverUserSN;
            dataFt.Appraiser = dataBd.assessmentAgency.trim();
            dataFt.MarketAmt = dataBd.marketPrice;
            dataFt.SaleAmt = dataBd.sellPrice;
            dataFt.GetTime = BdDateStrFormate(dataBd.acquireDate);
            dataFt.Ramerks = dataBd.details == null ? "" : dataBd.details.trim();

            return dataFt;
        },

        //后-前，已购买资产
        PurchasedAssetsBdToFt: function (dataBd) {
            var dataFt = {};

            dataFt.No = dataBd.assetsSN;
            dataFt.Type = dataBd.assetsType.trim();
            dataFt.Source = dataBd.assetsSourceType.trim();
            dataFt.AssetArea = dataBd.assetsProvince.trim();
            dataFt.BuyAmt = dataBd.purchasePrice;
            dataFt.GetAmt = dataBd.cost;
            dataFt.Saler = dataBd.sellerUserSN;
            dataFt.Appraiser = dataBd.assessmentAgency.trim();
            dataFt.MarketAmt = dataBd.marketPrice;
            dataFt.SaleAmt = dataBd.sellPrice;
            dataFt.GetTime = BdDateStrFormate(dataBd.acquireDate);
            dataFt.Ramerks = dataBd.details == null ? "" : dataBd.details.trim();

            return dataFt;
        },
    },
    UID: {
        M020301: function (c1, c2, c3, c4, c5, c6, c7, c8, c9) {
            ///<summary>资产购买用户列表</summary><param name="c1">用户编号</param><param name="c2">姓名</param><param name="c3">取消预约</param><param name="c4">预约中资产</param><param name="c5">已购买资产</param><param name="c6">VIP等级</param><param name="c7">客户经理</param><param name="c8">申请状态</param><param name="c9">服务状态</param>
            var NewRow = document.querySelector(".TbList .Template").cloneNode(true);
            NewRow.removeAttribute("class");
            var NewRowTDs = NewRow.querySelectorAll("td");
            NewRowTDs[0].innerText = c1; NewRowTDs[1].innerText = c2; NewRowTDs[2].innerText = c3; NewRowTDs[3].innerText = c4; NewRowTDs[4].innerText = c5;
            NewRowTDs[5].innerText = c6; NewRowTDs[6].innerText = c7; NewRowTDs[8].innerText = c8; NewRowTDs[9].children[0].setAttribute('data-sw', c9);
            document.querySelector(".TbList>tbody").appendChild(NewRow);
        },//资产购买用户列表
        M020302: function () {
            ///<summary>资产类型</summary>
            Str = " <table style=\"width:100%\"><tr>"; var s = 0;
            for (var i in M0203.UIP.AssetType) {
                if (M0203.UIP.AssetType[i].Name != "混合资产") {
                    s++;
                    Str += "<td><div class=\"UpDownButton\" data-sw='0' data-id=\"" + M0203.UIP.AssetType[i].ID + "\"></div>" + M0203.UIP.AssetType[i].Name + "</td>";
                    if (s % 3 == 0) { Str += "</tr><tr>"; }
                }
            }
            Str += "</tr></table>";
            document.querySelector(".TP1>.FBoxContainer>.Template .Pawn").innerHTML = Str;
        },//资产类型
        M020303: function (IsLast, Num) {
            ///<summary>数据加载完毕后执行</summary><param name="IsLast">是否是最后一条数据;true:是;false:否;</param><param name="Num">加载信息的数量</param>
            M0203.LoadParam.From += Num;
            Continue.innerText = 1;
            if (IsLast) { LastItem = 1; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 0); } else { LastItem = 0; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 1); }
            LoadingBoxCtl(0);
        },//数据加载完毕后执行
        M020304: function (V) {
            ///<summary>用户信息</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Name; TDs[2].DspV = V.BirthDay; TDs[3].DspV = V.Sex; TDs[4].DspV = V.CardAddress; TDs[5].DspV = V.CardID; TDs[6].DspV = V.Mobile; TDs[7].DspV = V.Email; TDs[8].DspV = V.Marry; TDs[9].DspV = V.Breed;
            TDs[10].DspV = V.LivePro + " " + V.LiveCity + " " + V.LiveStreet; TDs[11].DspV = V.AssetArea; TDs[12].querySelectorAll("label")[0].DspV = V.AmtMin; TDs[12].querySelectorAll("label")[1].DspV = V.AmtMax;
            var PawnSlt = V.AssetType.split(",");
            for (var i = 0; i < PawnSlt.length; i++) { if (PawnSlt[i].length >= 2) { NewBox.querySelector("[data-id='" + PawnSlt[i] + "']").setAttribute("data-sw", "1"); } }
            FBoxContainer.appendChild(NewBox);
        },//用户信息
        M020305: function (V) {
            ///<summary>取消预约</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Source; TDs[2].DspV = V.Type; TDs[3].DspV = V.GetTime; TDs[4].DspV = V.Saler; TDs[5].querySelector("label").DspV = V.GetAmt; TDs[6].DspV = V.Appraiser; TDs[7].querySelector("label").DspV = V.MarketAmt; TDs[8].DspV = V.AssetArea; TDs[9].querySelector("label").DspV = V.SaleAmt; TDs[10].DspV = V.Use;
            NewBox.querySelector(".Textarea").innerText = V.Ramerks;
            FBoxContainer.appendChild(NewBox);
        },//取消预约
        M020306: function (V) {
            ///<summary>预约中资产</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Source; TDs[2].DspV = V.Type; TDs[3].DspV = V.GetTime; TDs[4].DspV = V.Saler; TDs[5].querySelector("label").DspV = V.GetAmt; TDs[6].DspV = V.Appraiser; TDs[7].querySelector("label").DspV = V.MarketAmt; TDs[8].DspV = V.AssetArea; TDs[9].querySelector("label").DspV = V.SaleAmt; TDs[10].DspV = V.Use;
            NewBox.querySelector(".Textarea").innerText = V.Ramerks;
            FBoxContainer.appendChild(NewBox);
        },//预约中资产
        M020307: function (V) {
            ///<summary>已购买资产</summary>
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Source; TDs[2].DspV = V.Type; TDs[3].DspV = V.GetTime; TDs[4].DspV = V.Saler; TDs[5].querySelector("label").DspV = V.GetAmt; TDs[6].DspV = V.Appraiser; TDs[7].querySelector("label").DspV = V.MarketAmt; TDs[8].DspV = V.AssetArea; TDs[9].querySelector("label").DspV = V.SaleAmt; TDs[10].querySelector("label").DspV = V.BuyAmt;
            NewBox.querySelector(".Textarea").innerText = V.Ramerks;
            FBoxContainer.appendChild(NewBox);
        },//已购买资产
        M020308: function (txt) {
            var TTA = MVB.MBC.querySelector("div>textarea");
            TTA.value = txt;
        },
        M020309: function (res) {
            MVB.Close();
            if (res == true) {
                TransingStatus.SetStatus(3);
            }
            else {
                TransingStatus.SetStatus(2);
            }
        },
        M020310: function (Res, Act) {
            ///<summary>修改服务状态</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
                M0203.FocusItem.querySelector(".SlideButton").setAttribute("data-sw", Act);
            } else {
                TransingStatus.SetStatus(2);
            }
        }

    }

};