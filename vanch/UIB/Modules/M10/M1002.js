// JavaScript source code
var M1002 = {
    FocusItem: null,
    FocusStaff: null,
    GetGroupNo: function () {
        return this.FocusItem.getAttribute("data-SN");
    },
    Init: function () {
        LoadingBoxCtl(1);  this.STR.EVINIT();
    },
    Button: function (BtObj, BtID) {
        if (BtID == 1) {//用户组
            var ThisItem = BtObj.parentElement;
            if (ThisItem.getAttribute("data-slt") == "1") { this.GroupSlt(ThisItem, false); } else { this.GroupSlt(ThisItem, true); };
        }
        if (BtID == 2) { this.StaffSlt(BtObj); }//员工
        if (BtID > 10) {
            BtStatus(BtObj, "E"); var ThisItem = BtObj.parentElement.parentElement;
            switch (BtID) {
                case 11:
                    TransingStatus.SetStatus(1);
                    this.FocusItem = BtObj.parentElement.parentElement;
                    this.STR.EVM100203(this.GetGroupNo());
                    break;
                case 12:
                    TransingStatus.SetStatus(1);
                    this.STR.EVM100204(ThisItem.querySelector("input").value)
                    ThisItem.querySelector('.Mask').style.display = '';
                    ThisItem.querySelector('.ButtonBar').setAttribute('data-bb', '0');
                    break;
                case 13:
                    ThisItem.querySelector('.Mask').style.display = '';
                    ThisItem.querySelector('.ButtonBar').setAttribute('data-bb', '0');
                    break;
                default:
                    break;
            }
        }
    },
    GroupSlt: function (TheItem, Slt) {
        if (Slt == true) {
            if (TheItem.getAttribute("data-slt") == "1") { return; };
            LoadingBoxCtl(1);
            if (this.FocusItem != null) { this.FocusItem.setAttribute("data-slt", 0); this.ClrUser(); };
            TheItem.setAttribute("data-slt", 1); this.FocusItem = TheItem; this.STR.EVM100201(this.GetGroupNo());
        };
        if (Slt == false) { if (TheItem.getAttribute("data-slt") == "1") { TheItem.setAttribute("data-slt", 0); this.ClrUser(); } };
    },//选中的用户组
    StaffSlt: function (TheItem) {
        this.FocusStaff = TheItem;
        TransingStatus.SetStatus(1);
        var Slt = 1; if (TheItem.getAttribute("data-slt") == "1") { Slt = 0 };
        M1002.STR.EVM100202(this.GetGroupNo(), TheItem.getAttribute("data-SN"), Slt);

    },//选中的员工
    CountUser: function () {
        var Table = MainZone.querySelector(".HalfContainerR>.ListContainer>.TbList>tbody");
        var Num = Table.querySelectorAll("tr[data-slt='1']").length;
        this.FocusItem.querySelector(".UserCnt").innerText = Num;
        if (Num > 0) { this.FocusItem.querySelector(".ButtonBar>div[data-btst]").setAttribute("data-btst", "H"); } else { this.FocusItem.querySelector(".ButtonBar>div[data-btst]").setAttribute("data-btst", "E"); };
    },//统计用户组里的用户数量
    DelGroup: function () {
        document.querySelector(".HalfContainerL").removeChild(this.FocusItem);
        this.ClrUser();
    },//删除用户组
    AddGroup: function (Data) {
        var Template = document.querySelector(".HalfContainerL>.Template");
        var NewItem = Template.children[0].cloneNode(true);
        NewItem.querySelector(".GroupName").innerText = Data.C1;
        NewItem.querySelector(".UserCnt").innerText = Data.C2;
        NewItem.querySelector(".MenuL1Cnt").innerText = Data.C3;
        NewItem.querySelector(".MenuL2Cnt").innerText = Data.C4;
        NewItem.setAttribute("data-SN", Data.C5);
        if (Data.C2 > 0) { NewItem.querySelector(".ButtonBar>div[data-btst]").setAttribute("data-btst", "H"); };
        document.querySelector(".HalfContainerL").insertBefore(NewItem, Template);
        ReCalcItemSize();
    },//新增用户组
    ClrUser: function () { document.querySelector(".HalfContainerR .TbList tbody").innerHTML = ""; },//清空用户列表
    UIP: {},
    STR: {
        EVINIT: function () {
            ///<summary>页面初始化</summary>
            var Data = [{ C1: "董事长", C2: 1, C3: 3, C4: 7, C5: "A1" }, { C1: "系统管理", C2: 0, C3: 3, C4: 7, C5: "A2" }, { C1: "权限管理", C2: 1, C3: 3, C4: 7, C5: "A3" }, { C1: "总经理", C2: 1, C3: 3, C4: 7, C5: "A4" }, { C1: "客服人员", C2: 1, C3: 3, C4: 7, C5: "A5" }, { C1: "审核组", C2: 1, C3: 3, C4: 7, C5: "A6" }, { C1: "客服经理", C2: 1, C3: 3, C4: 7, C5: "A7" }]
            for(var i in Data){ M1002.UID.M100201(Data[i]) };
            M1002.UID.M100202();
        },
        EVM100201: function (No) {
            ///<summary>加载用户数据</summary><param name="No">用户组编号</param>
            var Data = [
                { C1: "Staff1", C2: "姓名A", C3: "男", C4: "客服部", C5: "客服人员", C6: 609, C7: "A001", C8: true },
                { C1: "Staff2", C2: "姓名B", C3: "女", C4: "客服部", C5: "客服人员", C6: 609, C7: "A002", C8: false },
                { C1: "Staff3", C2: "姓名C", C3: "女", C4: "客服部", C5: "客服人员", C6: 609, C7: "A003", C8: true },
                { C1: "Staff4", C2: "姓名D", C3: "女", C4: "客服部", C5: "客服人员", C6: 609, C7: "A004", C8: false },
                { C1: "Staff5", C2: "姓名E", C3: "女", C4: "客服部", C5: "客服人员", C6: 609, C7: "A005", C8: true },
                { C1: "Staff6", C2: "姓名F", C3: "女", C4: "客服部", C5: "客服人员", C6: 609, C7: "A006", C8: false },
                { C1: "Staff7", C2: "姓名G", C3: "男", C4: "客服部", C5: "客服人员", C6: 609, C7: "A007", C8: false },
                { C1: "Staff8", C2: "姓名H", C3: "男", C4: "客服部", C5: "客服人员", C6: 609, C7: "A008", C8: true }]
            setTimeout("for(var i in " + JSON.stringify(Data) + "){M1002.UID.M100203(" + JSON.stringify(Data) + "[i]);}M1002.UID.M100202();", 200);

        },//加载用户数据
        EVM100202: function (GroupNo, UserSN, Act) {
            ///<summary>选择用户</summary>
            ///<param name="GroupNo">用户组编号</param><param name="UserSN">用户编号</param><param name="Act">状态</param>
            console.log(GroupNo + "__" + UserSN + "__" + Act);
            setTimeout("M1002.UID.M100204(true)", 200);
        },//添加或取消用户
        EVM100203: function (GroupNo) {
            ///<summary>删除用户组</summary>
            console.log(GroupNo);
            setTimeout("M1002.UID.M100205(true)", 200);
        },//删除用户组
        EVM100204: function (Name) {
            ///<summary>新增用户组</summary>
            console.log(Name);
            var Data = { C1: Name, C2: 0, C3: 0, C4: 0, C5: "A7" };
            setTimeout("M1002.UID.M100206(true," + JSON.stringify(Data) + ")", 200);
        }
    },
    UID: {
        M100201: function (Data) {
            ///<summary>用户组</summary>
            var Template = document.querySelector(".HalfContainerL>.Template");
            var NewItem = Template.children[0].cloneNode(true);
            NewItem.querySelector(".GroupName").innerText = Data.C1;
            NewItem.querySelector(".UserCnt").innerText = Data.C2;
            NewItem.querySelector(".MenuL1Cnt").innerText = Data.C3;
            NewItem.querySelector(".MenuL2Cnt").innerText = Data.C4;
            NewItem.setAttribute("data-SN", Data.C5);
            if (Data.C2 > 0) { NewItem.querySelector(".ButtonBar>div[data-btst]").setAttribute("data-btst", "H"); };
            document.querySelector(".HalfContainerL").insertBefore(NewItem, Template);
            ReCalcItemSize();
        },//用户组
        M100202: function () {
            LoadingBoxCtl(0);
        },
        M100203: function (Data) {
            ///<summary>加载员工数据</summary>
            var NewItem = document.querySelector(".HalfContainerR .TbList>.Template").firstChild.cloneNode(true);
            var NewTds = NewItem.querySelectorAll("td");
            NewTds[0].DspV = Data.C1; NewTds[1].DspV = Data.C2; NewTds[2].DspV = Data.C3; NewTds[3].DspV = Data.C4; NewTds[4].DspV = Data.C5; NewTds[5].DspV = Data.C6; NewItem.setAttribute("data-SN", Data.C7);
            if (Data.C8) { NewItem.setAttribute("data-slt", 1); }
            document.querySelector(".HalfContainerR .TbList tbody").appendChild(NewItem);
        },//加载员工数据
        M100204: function (Res) {
            ///<summary>添加或取消用户</summary>
            if (Res) {
                if (M1002.FocusStaff.getAttribute("data-slt") == "1") { M1002.FocusStaff.setAttribute("data-slt", 0); } else { M1002.FocusStaff.setAttribute("data-slt", 1); };
                M1002.CountUser();
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },//操作添加或取消用户返回结果
        M100205: function (Res) {
            ///<summary>删除用户组</summary>
            if (Res) {
                M1002.DelGroup();
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
        M100206: function (Res, Data) {
            ///<summary>新增用户组</summary>
            if (Res) {
                M1002.AddGroup(Data);
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        }
    }
};