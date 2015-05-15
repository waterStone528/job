// JavaScript source code
var M0206 = {
    FocusItem: null,
    FocusBox: null,
    L1DERParam: ",",
    FirstLoadNum: 35,
    LoadParam: {From: 0, Num: 10 },
    Init: function () {
        LoadingBoxCtl(1);
        M0206.STR.EVM020601(this.FirstLoadNum);
    },
    GetFocusUserNum: function () {
        return this.FocusItem.parentElement.querySelector("[data-btid = '1']").innerText;
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
        this.STR.EVM020602(this.L1DERParam, this.FirstLoadNum);
    },
    Button: function (Sender) { this.SltItem(Sender); },
    SltItem: function (TheItem) {
        if (this.FocusItem != null && TheItem != this.FocusItem) { this.FocusItem.setAttribute("data-slt", 0); };
        if (TheItem == null) { this.FocusItem.setAttribute("data-slt", 0); return; };
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
    SlideSwitch: function (Sender) {
        var Switch = null;
        TransingStatus.SetStatus(1);
        this.FocusItem = Sender.parentElement;
        if (Sender.getAttribute('data-sw') == '1') { Switch = 0; } else { Switch = 1; }
        M0206.STR.EVM020613(this.GetFocusUserNum(), Switch);
    },
    LoadMoreInfo: function () {
        if (parseInt(Continue.innerText) == 1) {
            Continue.innerText = 0;
            M0206.STR.EVM020612(M0206.L1DERParam, M0206.LoadParam.From, M0206.LoadParam.Num);
        }
    },    //加载更多数据
    DspListBak: function (BtID) {
        MVB.ContainerWidth = 600;
        MVB.Template = document.querySelector(".TP" + BtID + ">.TbList");
        var MVBTitle = "";
        switch (BtID) {
            case "1": MVBTitle = "客户经理选择"; break;
            case "2": MVBTitle = "备注信息"; break;
        }
        MVB.Buttons = ["知道了", "MVB.Close()"];
        MVB.Open(document.querySelector('#MainZone .ListContainer'), "M0206.SltItem(null)", MVBTitle);
    },
    DspList: function (BtID) {
        MVB.Template = document.querySelector(".TP" + BtID).children[0];
        var containerObj = document.querySelector('#MainZone .ListContainer');
        switch (BtID) {
            case "1": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0206.SltItem(null)", "用户信息"); this.STR.EVM020603(this.GetFocusUserNum()); break;
            case "2": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0206.SltItem(null)", "用户" + this.GetFocusUserNum() + "-取消发布"); this.STR.EVM020609(this.GetFocusUserNum()); break;
            case "3": MVB.ContainerWidth = 400; MVB.Buttons = ["取消", "M0206.CancelPublish();"]; MVB.Open(containerObj, "M0206.SltItem(null)", "用户" + this.GetFocusUserNum() + "-已发布资产"); this.STR.EVM020604(this.GetFocusUserNum()); break; 
            case "4": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0206.SltItem(null)", "用户" + this.GetFocusUserNum() + "-拒绝预约"); this.STR.EVM020611(this.GetFocusUserNum()); break; 
            case "5": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0206.SltItem(null)", "用户" + this.GetFocusUserNum() + "-预约中资产"); this.STR.EVM020605(this.GetFocusUserNum()); break; 
            case "6": MVB.ContainerWidth = 400; MVB.Buttons = []; MVB.Open(containerObj, "M0206.SltItem(null)", "用户" + this.GetFocusUserNum() + "-已出售资产"); this.STR.EVM020606(this.GetFocusUserNum()); break; 
            case "7": MVB.ContainerWidth = 500; MVB.Buttons = ["保存", "M0206.ClkSaveNote();"]; MVB.Open(containerObj, "M0206.SltItem(null)", "用户" + this.GetFocusUserNum() + "-备注信息"); this.STR.EVM020607(this.GetFocusUserNum()); break; 
        }
    },
    EnableBt: function (bt) {
        var btObj = document.querySelector(".MovingBox>.ButtonBar").children[1];
        btObj.setAttribute("data-btst", "E");
    },
    CancelPublish: function () {
        TransingStatus.SetStatus(1);
        var ItemSN = document.querySelector(".MovingBox>.Container>.FBoxContainer>[data-slt='1']").querySelector(".Hidden").innerText;
        this.STR.EVM020610(ItemSN);
    },
    ClkSaveNote: function () {
        TransingStatus.SetStatus(1);
        var noteText = document.querySelector(".MovingBox>.Container textarea").innerText;
        this.STR.EVM020608(this.GetFocusUserNum(), noteText);
    },
    Details: function (Obt) {
        ///<summary>查看资产详情</summary>
        this.FocusBox = Obt.parentElement.parentElement.parentElement.parentElement;
        var Texterea = this.FocusBox.querySelector(".Textarea");
        if (Texterea.getAttribute("data-sw") == "1") { Texterea.setAttribute("data-sw", "0"); } else { Texterea.setAttribute("data-sw", "1"); }
    },
    UIP: {  },
    STR: {
        EVM020601: function (Num) {
            ///<summary>初始化页面</summary><param name="Num">首次加载信息的数量</param>
            setTimeout("for (var i = 1 ; i <= " + Num + "; i++) {if(i%4!=0){M0206.UID.M020601(\"USSSS0\" + i, \"姓名\" + i, 2, 4, 5, 6, 3, 1, 3, \"609\", \"通过\", 1);}else{M0206.UID.M020601(\"USSSS0\" + i, \"姓名\" + i, 0, 0, 0, 0, 0, 0, 0, \"609\", \"未通过\", 1);} }; M0206.UID.M020602(false, " + Num + ");", 100);
        },//初始化页面
        EVM020602: function (L1DERParam, Num) {
            ///<summary>排序筛选</summary>
            ///<param name="L1DERParam">筛选条件</param><param name="Num">首次加载信息的数量</param>
            console.log(L1DERParam);
            setTimeout("for (var i = 1 ; i <= " + Num + "; i++) {M0206.UID.M020601(\"USSSS0\" + i, \"姓名\" + i, 2, 4, 5, 6, 3, 1, 3, \"609\", \"通过\", 1); }; M0206.UID.M020602(false, " + Num + ");", 100);
        },
        EVM020603: function (UserSN) {
            var UserInfo = { No: "U20156065", Name: "张学友", BirthDay: "1965/08/09", Sex: "男", CardAddress: "中国香港", CardID: "789454*", Mobile: "17066666666", Email: "zhangxueyou@qq.com", Marry: "已婚", Breed: "已育", LivePro: "浙江", LiveCity: "宁波", LiveStreet: "高新区" };
            M0206.UID.M020603(UserInfo);
        },//用户信息
        EVM020604: function (UserSN) {
            var ItemInfo = { ItemSN:"A0001", No: "U12153561", Type: "住宅房", Source: "购买", GetTime: "2010/01/02", Saler: "U51888466", GetCost: "98", Appraiser: "赵忠祥", MarketAmt: 134, AssetArea: "浙江", SaleAmt: 150, Use: "闲置", Remarks: "好房" };
            M0206.UID.M020604(ItemInfo); M0206.UID.M020604(ItemInfo); M0206.UID.M020604(ItemInfo); M0206.UID.M020604(ItemInfo);//UI演示数据，正式使用需删除此行
        },//己发布资产移出框出现后
        EVM020605: function (UserSN) {
            var ItemInfo = { No: "U20164811", Type: "汽车", Source: "购买", Use: "自用", Appraiser: "任达华", MarketAmt: "120", Buyer: "U20156065", SaleAmt: "105", BespeakTime: "2014/07/03", Ramerks: "保时捷-卡宴 里程20万公里，2011年买进，改装费20万，最高时间可达268公里/小时！" };
            M0206.UID.M020605(ItemInfo); M0206.UID.M020605(ItemInfo); M0206.UID.M020605(ItemInfo); M0206.UID.M020605(ItemInfo); M0206.UID.M020605(ItemInfo);//UI演示数据，正式使用需删除此行
        },//预约中资产移出框出现后
        EVM020606: function (UserSN) {//已出售资产
            var ItemInfo = { No: "U20164811", Type: "汽车", Source: "购买", Use: "自用", Appraiser: "任达华", MarketAmt: "120", Buyer: "U20156065", SaleAmt: "105", BespeakTime: "2014/07/03", Ramerks: "保时捷-卡宴 里程20万公里，2011年买进，改装费20万，最高时间可达268公里/小时！" };
            M0206.UID.M020606(ItemInfo); M0206.UID.M020606(ItemInfo); M0206.UID.M020606(ItemInfo);//UI演示数据，正式使用需删除此行
        },
        EVM020607: function (userSN) {//用户备注
            M0206.UID.M020607("这是备注的测试数据");
        },
        EVM020608: function (UserSN, noteText) {//UserSN为选中的用户编号,note为备注信息
            setTimeout("M0206.UID.M020608(true);", 1000); //演示
        },
        EVM020609: function (UserSN) {
            var ItemInfo = { No: "U12153561", Type: "住宅房", Source: "购买", GetTime: "2010/01/02", Saler: "U51888466", GetCost: "98", Appraiser: "赵忠祥", MarketAmt: 134, AssetArea: "浙江", SaleAmt: 150, Use: "闲置", Remarks: "好房" };
            M0206.UID.M020609(ItemInfo); M0206.UID.M020609(ItemInfo); M0206.UID.M020609(ItemInfo); M0206.UID.M020609(ItemInfo);//UI演示数据，正式使用需删除此行
        },//取消发布资产移出框
        EVM020610: function (ItemSN) {
            console.log(ItemSN);
            setTimeout("M0206.UID.M020610(true);", 1000); //演示
        },//取消发布资产按钮
        EVM020611: function (UserSN) {
            var ItemInfo = { No: "U20164811", Type: "汽车", Source: "购买", Use: "自用", Appraiser: "任达华", MarketAmt: "120", Buyer: "U20156065", SaleAmt: "105", BespeakTime: "2014/07/03", Ramerks: "保时捷-卡宴 里程20万公里，2011年买进，改装费20万，最高时间可达268公里/小时！" };
            M0206.UID.M020611(ItemInfo); M0206.UID.M020611(ItemInfo); M0206.UID.M020611(ItemInfo); M0206.UID.M020611(ItemInfo);//UI演示数据，正式使用需删除此行
        },//拒绝预约移出框
        EVM020612: function (L1DERParam, From, Num) {
            ///<summary>加载更多数据</summary>
            ///<param name="L1DERParam">筛选条件</param><param name="From">从第N条开始加载</param><param name="Num">加载信息的数量</param>
            for (var i = 1 ; i <= Num; i++) { M0206.UID.M020601("USSSS0" + i, "姓名" + i, 2, 4, 5, 6, 3, 1, 3, "609", "通过", 1); }
            console.log(L1DERParam);
            M0206.UID.M020602(true, Num);
        },//加载更多数据
        EVM020613: function (UserSN, Act) {
            ///<summary>服务状态</summary><param name="UserSN">用户编号</param><param name="Act">状态值</param>
            console.log(UserSN);
            setTimeout("M0206.UID.M020612(true," + Act + ")", 200);
        }//服务状态
    },
    UID: {
        M020601: function (c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12 ) {
            var NewRow = document.querySelector(".TbList .Template").cloneNode(true);
            NewRow.removeAttribute("class");
            var NewRowTDs = NewRow.querySelectorAll("td");
            NewRowTDs[0].innerText = c1; NewRowTDs[1].innerText = c2; NewRowTDs[2].innerText = c3; NewRowTDs[3].innerText = c4; NewRowTDs[4].innerText = c5; NewRowTDs[5].innerText = c6;
            NewRowTDs[6].innerText = c7; NewRowTDs[7].innerText = c8; NewRowTDs[8].innerText = c9; NewRowTDs[9].innerText = c10; NewRowTDs[11].innerText = c11; NewRowTDs[12].children[0].setAttribute('data-sw', c12);
            document.querySelector(".TbList>tbody").appendChild(NewRow);
        },
        M020602: function (IsLast, Num) {
            ///<summary>数据加载完毕后执行</summary>
            ///<param name="IsLast">是否是最后一条数据;true:是;false:否;</param><param name="Num">加载信息的数量</param>
            M0206.LoadParam.From += Num;
            Continue.innerText = 1;
            if (IsLast) { LastItem = 1; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 0); } else { LastItem = 0; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 1); }
            LoadingBoxCtl(0);
        },//数据加载完毕后执行
        M020603: function (V) {
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Name; TDs[2].DspV = V.BirthDay; TDs[3].DspV = V.Sex; TDs[4].DspV = V.CardAddress; TDs[5].DspV = V.CardID; TDs[6].DspV = V.Mobile; TDs[7].DspV = V.Email; TDs[8].DspV = V.Marry; TDs[9].DspV = V.Breed; TDs[10].DspV = V.LivePro + " " + V.LiveCity + " " + V.LiveStreet;
            FBoxContainer.appendChild(NewBox);
        },//用户信息
        M020604: function (V) {
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Type; TDs[2].DspV = V.Source; TDs[3].DspV = V.GetTime; TDs[4].DspV = V.Saler; TDs[5].querySelector("label").DspV = V.GetCost; TDs[6].DspV = V.Appraiser; TDs[7].querySelector("label").DspV = V.MarketAmt; TDs[8].DspV = V.AssetArea; TDs[9].querySelector("label").DspV = V.SaleAmt; TDs[10].DspV = V.Use; NewBox.querySelector(".Textarea").innerText = V.Remarks;
            NewBox.querySelector(".Hidden").DspV = V.ItemSN;
            FBoxContainer.appendChild(NewBox);
        },//已发布资产
        M020605: function (V) {
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Type; TDs[2].DspV = V.Source; TDs[3].DspV = V.Use; TDs[4].DspV = V.Appraiser; TDs[5].querySelector("label").DspV = V.MarketAmt; TDs[6].DspV = V.Buyer; TDs[7].querySelector("label").DspV = V.SaleAmt; TDs[8].DspV = V.BespeakTime; NewBox.querySelector(".Textarea").innerText = V.Ramerks;
            FBoxContainer.appendChild(NewBox);
        },//预约中资产
        M020606: function (V) {//已出售资产
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Type; TDs[2].DspV = V.Source; TDs[3].DspV = V.Use; TDs[4].DspV = V.Appraiser; TDs[5].querySelector("label").DspV = V.MarketAmt; TDs[6].DspV = V.Buyer; TDs[7].querySelector("label").DspV = V.SaleAmt; TDs[8].DspV = V.BespeakTime; NewBox.querySelector(".Textarea").innerText = V.Ramerks;
            FBoxContainer.appendChild(NewBox);
        },
        M020607: function (txt) {//用户备注信息
            var TTA = MVB.MBC.querySelector("div>textarea");
            TTA.value = txt;
        },
        M020608: function (res) {
            MVB.Close();
            if (res == true) {
                TransingStatus.SetStatus(3);
            }
            else {
                TransingStatus.SetStatus(2);
            }
        },
        M020609: function (V) {
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Type; TDs[2].DspV = V.Source; TDs[3].DspV = V.GetTime; TDs[4].DspV = V.Saler; TDs[5].querySelector("label").DspV = V.GetCost; TDs[6].DspV = V.Appraiser; TDs[7].querySelector("label").DspV = V.MarketAmt; TDs[8].DspV = V.AssetArea; TDs[9].querySelector("label").DspV = V.SaleAmt; TDs[10].DspV = V.Use; NewBox.querySelector(".Textarea").innerText = V.Remarks;
            FBoxContainer.appendChild(NewBox);
        },//取消发布
        M020610: function (res) {//取消发布资产按钮
            if (res == true) {
                MVB.MBC.querySelector(".Container>.FBoxContainer").removeChild(M0206.FocusBox);
                M0206.FocusBox = null;
                TransingStatus.SetStatus(3);
            }
            else {
                TransingStatus.SetStatus(2);
            }
        },
        M020611: function (V) {
            var NewBox = MVB.MBC.querySelector(".FBoxContainer>.Template").children[0].cloneNode(true);
            var FBoxContainer = MVB.MBC.querySelector(".FBoxContainer");
            var TDs = NewBox.querySelectorAll("tr>td:nth-child(2n)");
            TDs[0].DspV = V.No; TDs[1].DspV = V.Type; TDs[2].DspV = V.Source; TDs[3].DspV = V.Use; TDs[4].DspV = V.Appraiser; TDs[5].querySelector("label").DspV = V.MarketAmt; TDs[6].DspV = V.Buyer; TDs[7].querySelector("label").DspV = V.SaleAmt; TDs[8].DspV = V.BespeakTime; NewBox.querySelector(".Textarea").innerText = V.Ramerks;
            FBoxContainer.appendChild(NewBox);
        },//拒绝预约
        M020612: function (Res, Act) {
            ///<summary>修改服务状态</summary>
            if (Res) {
                TransingStatus.SetStatus(3);
                M0206.FocusItem.querySelector(".SlideButton").setAttribute("data-sw", Act);
            } else {
                TransingStatus.SetStatus(2);
            }
        }
    }
};