// JavaScript source code
var M1003 = {
    FocusItem: null,
    FocusMenu:null,
    Init: function () {
        this.STR.EVINIT();
    },
    GetGroupNo: function () {
        return this.FocusItem.getAttribute("data-SN");
    },
    Button: function (BtObj, BtID) {
        if (BtID == 0) {
            if (BtObj.getAttribute("data-slt") == "1") { this.GroupSlt(BtObj, false); } else { this.GroupSlt(BtObj, true); };
        }
        if (BtID == 1) {//菜单
            if (BtObj.getAttribute("data-slt") == "1") { this.MemuSlt(BtObj, false); } else { this.MemuSlt(BtObj, true); };
        }
    },
    GroupSlt: function (TheItem, Slt) {
        if (Slt == true) {
            if (TheItem.getAttribute("data-slt") == "1") { return; };
            LoadingBoxCtl(1);
            if (this.FocusItem != null) { this.FocusItem.setAttribute("data-slt", 0); this.ClrMod(); };
            TheItem.setAttribute("data-slt", 1); this.FocusItem = TheItem; this.STR.EVM100301(this.GetGroupNo());
        };
        if (Slt == false) { if (TheItem.getAttribute("data-slt") == "1") { TheItem.setAttribute("data-slt", 0); this.ClrMod(); } };
    },//选中的用户组
    MemuSlt: function (TheItem, Slt) {
        TransingStatus.SetStatus(1);
        this.FocusMenu = TheItem;
        var Slt = 1; if (TheItem.getAttribute("data-slt") == "1") { Slt = 0 };
        M1003.STR.EVM100302(this.GetGroupNo(), TheItem.getAttribute("data-SN"), Slt);
    },//选中的菜单
    ClrMod: function () { document.querySelector(".HalfContainerR .TbList tbody").innerHTML = ""; },//清空菜单列表
    CountMenu: function () {
        var TrSlt = MainZone.querySelectorAll(".HalfContainerR>.ListContainer>.TbList>tbody>tr[data-slt='1']");
        var FM = 0, SM = 0;
        for (var i = 0; i < TrSlt.length;i++) {
            if (TrSlt[i].querySelectorAll("td")[1].innerText != "") { FM++; }
            if (TrSlt[i].querySelectorAll("td")[2].innerText != "") { SM++; }
        }
        this.FocusItem.querySelector(".MenuL1Cnt").innerText = FM;
        this.FocusItem.querySelector(".MenuL2Cnt").innerText = SM;
    },//统计用户组里的菜单数量
    UIP: {},
    STR: {
        EVINIT: function () {
            ///<summary>页面初始化</summary>
            var Data = [{ C1: "董事长", C2: 1, C3: 3, C4: 7, C5: "A1" }, { C1: "系统管理", C2: 0, C3: 3, C4: 7, C5: "A2" }, { C1: "权限管理", C2: 1, C3: 3, C4: 7, C5: "A3" }, { C1: "总经理", C2: 1, C3: 3, C4: 7, C5: "A4" }, { C1: "客服人员", C2: 1, C3: 3, C4: 7, C5: "A5" }, { C1: "审核组", C2: 1, C3: 3, C4: 7, C5: "A6" }, { C1: "客服经理", C2: 1, C3: 3, C4: 7, C5: "A7" }]
            for (var i in Data) { M1003.UID.M100301(Data[i]) };
            M1003.UID.M100302();
        },
        EVM100301: function (No) {
            ///<summary>加载菜单数据</summary><param name="No">菜单组编号</param>
            var Data = [
                { C1: "M1001", C2: "内部用户", C3: "用户管理", C4: "", C5: "", C6: "", C7: "A001", C8: true },
                { C1: "M0201", C2: "客户管理", C3: "债权投资", C4: "", C5: "", C6: "", C7: "A002", C8: false },
                { C1: "M0202", C2: "客户管理", C3: "债权融资", C4: "", C5: "", C6: "", C7: "A003", C8: true },
                { C1: "M0203", C2: "客户管理", C3: "财务顾问", C4: "", C5: "", C6: "", C7: "A004", C8: false },
                { C1: "M0204", C2: "客户管理", C3: "资产出售", C4: "", C5: "", C6: "", C7: "A005", C8: true },
                { C1: "M0205", C2: "客户管理", C3: "资产购买", C4: "", C5: "", C6: "", C7: "A006", C8: false },
                { C1: "M1101", C2: "", C3: "密码修改", C4: "", C5: "", C6: "", C7: "A007", C8: false },
                { C1: "M1102", C2: "", C3: "安全注销", C4: "", C5: "", C6: "", C7: "A008", C8: false }]
            setTimeout("for(var i in " + JSON.stringify(Data) + "){M1003.UID.M100303(" + JSON.stringify(Data) + "[i]);}M1003.UID.M100302();", 200);

        },//加载菜单数据
        EVM100302: function (GroupNo, MenuSN, Act) {
            ///<summary>选择菜单</summary>
            ///<param name="GroupNo">用户组编号</param><param name="MenuSN">菜单编号</param><param name="Act">状态</param>
            console.log(GroupNo + "__" + MenuSN + "__" + Act);
            setTimeout("M1003.UID.M100304(true)", 200);
        },//添加或取消菜单
    },
    UID: {
        M100301: function (Data) {
            ///<summary>用户组</summary>
            var Template = document.querySelector(".HalfContainerL>.Template");
            var NewItem = Template.children[0].cloneNode(true);
            NewItem.querySelector(".GroupName").innerText = Data.C1;
            NewItem.querySelector(".UserCnt").innerText = Data.C2;
            NewItem.querySelector(".MenuL1Cnt").innerText = Data.C3;
            NewItem.querySelector(".MenuL2Cnt").innerText = Data.C4;
            NewItem.setAttribute("data-SN", Data.C5);
            document.querySelector(".HalfContainerL").insertBefore(NewItem, Template);
            ReCalcItemSize();
        },//用户组
        M100302: function () {
            LoadingBoxCtl(0);
        },
        M100303: function (Data) {
            ///<summary>加载菜单数据</summary>
            var NewItem = document.querySelector(".HalfContainerR .TbList>.Template").firstChild.cloneNode(true);
            var NewTds = NewItem.querySelectorAll("td");
            NewTds[0].DspV = Data.C1; NewTds[1].DspV = Data.C2; NewTds[2].DspV = Data.C3; NewTds[3].DspV = Data.C4; NewTds[4].DspV = Data.C5; NewTds[5].DspV = Data.C6; NewItem.setAttribute("data-SN", Data.C7);
            if (Data.C8) { NewItem.setAttribute("data-slt", 1); }
            document.querySelector(".HalfContainerR .TbList tbody").appendChild(NewItem);
        },//加载菜单数据
        M100304: function (Res) {
            ///<summary>添加或取消菜单</summary>
            if (Res) {
                if (M1003.FocusMenu.getAttribute("data-slt") == "1") { M1003.FocusMenu.setAttribute("data-slt", 0); } else { M1003.FocusMenu.setAttribute("data-slt", 1); };
                M1003.CountMenu();
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },//操作添加或取消菜单返回结果
    }
};