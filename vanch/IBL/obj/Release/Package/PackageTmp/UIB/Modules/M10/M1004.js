// JavaScript source code
var M1004 = {
    FocusItem: null,
    FocusStaff: null,
    Init: function () {
        LoadingBoxCtl(1); this.STR.EVINIT();
    },
    GetGroupSN: function () {
        return this.FocusItem.getAttribute("data-SN");
    },
    Button: function (BtObj, BtID) {
        if (BtID == 1) {//用户组
            var ThisItem = BtObj.parentElement;
            if (ThisItem.getAttribute("data-slt") == "1") { this.GroupSlt(ThisItem, false); } else { this.GroupSlt(ThisItem, true); };
        }
        if (BtID == 2) { if (BtObj.getAttribute("data-slt") == "1") { this.StaffSlt(BtObj, false); } else { this.StaffSlt(BtObj, true); }; }//用户
        if (BtID > 10) {
            BtStatus(BtObj, "E"); var ThisItem = BtObj.parentElement.parentElement; this.FocusItem = ThisItem;
            switch (BtID) {
                case 11:
                    LoadingBoxCtl(1); M1004.STR.EVM100405(this.GetGroupSN());
                    break;
                case 12:
                    LoadingBoxCtl(1);
                    var Name = ThisItem.querySelectorAll("input")[0].value;
                    var Remarks = ThisItem.querySelectorAll("input")[1].value;
                    M1004.STR.EVM100404(Name, Remarks);
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
            LoadingBoxCtl(1);
            if (TheItem.getAttribute("data-slt") == "1") { return; };
            if (this.FocusItem != null) { this.FocusItem.setAttribute("data-slt", 0); this.ClrUser(); };
            TheItem.setAttribute("data-slt", 1); this.FocusItem = TheItem; M1004.STR.EVM100401(M1004.GetGroupSN());
        };
        if (Slt == false) { if (TheItem.getAttribute("data-slt") == "1") { TheItem.setAttribute("data-slt", 0); this.ClrUser(); } };
    },//选择用户组
    CountUser: function () {
        var Table = MainZone.querySelector(".HalfContainerR>.ListContainer>.TbList>tbody");
        var Num = Table.querySelectorAll("tr[data-slt='1']").length;
        this.FocusItem.querySelector(".UserCnt").innerText = Num;
        if (Num > 0) { this.FocusItem.querySelector(".ButtonBar>div[data-btst]").setAttribute("data-btst", "H"); } else { this.FocusItem.querySelector(".ButtonBar>div[data-btst]").setAttribute("data-btst", "E"); };
    },//统计用户组里的用户数量
    StaffSlt: function (TheItem, Slt) {
        LoadingBoxCtl(1);
        this.FocusStaff = TheItem;
        var UserSN = TheItem.getAttribute("data-SN");
        if (Slt) { M1004.STR.EVM100403(this.GetGroupSN(), UserSN); } else { M1004.STR.EVM100402(this.GetGroupSN(), UserSN); };
    },//选择员工
    ClrUser: function () { document.querySelector(".HalfContainerR .TbList tbody").innerHTML = ""; },
    UIP: {},
    STR: {
        EVINIT: function () {
            //var Data = [{ C1: "审核员", C2: 2, C3: "", C4: "A1" }, { C1: "客服经理", C2: 5, C3: "关联至M02，勿删除", C4: "A2" }, { C1: "客服经理", C2: 0, C3: "备注", C4: "A3" }]
            //setTimeout("for(var i in " + JSON.stringify(Data) + "){M1004.UID.M100406(" + JSON.stringify(Data) + "[i]);}M1004.UID.M100407();", 200);

            var busCode = "M1004INIT";
            var data = "busCode=" + busCode;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);

                for (var i in resObj) {
                    var dataFt = {};
                    dataFt.C1 = resObj[i].roleName;
                    dataFt.C2 = resObj[i].userAmount;
                    dataFt.C3 = resObj[i].note == null ? "" : resObj[i].note.trim();
                    dataFt.C4 = resObj[i].roleTypeSN;

                    M1004.UID.M100406(dataFt);
                }

                M1004.UID.M100407();
            }
            ajaxObj.start();
        },
        EVM100401: function (GroupID) {
            ///<summary>加载员工数据</summary>
            //console.log(GroupID)
            //var Data = [
            //    { C1: "Staff1", C2: "姓名A", C3: "男", C4: "客服部", C5: "客服人员", C6: "609", C7: "A1", C8: true },
            //    { C1: "Staff2", C2: "姓名B", C3: "男", C4: "客服部", C5: "客服人员", C6: "609", C7: "A2", C8: true },
            //    { C1: "Staff3", C2: "姓名C", C3: "女", C4: "客服部", C5: "客服人员", C6: "609", C7: "A3", C8: false },
            //    { C1: "Staff4", C2: "姓名D", C3: "女", C4: "客服部", C5: "客服人员", C6: "609", C7: "A4", C8: false },
            //    { C1: "Staff5", C2: "姓名E", C3: "男", C4: "客服部", C5: "客服人员", C6: "609", C7: "A5", C8: false }
            //];
            //setTimeout("for(var i in " + JSON.stringify(Data) + "){M1004.UID.M100401(" + JSON.stringify(Data) + "[i]);}M1004.UID.M100407();", 200);

            var busCode = "M100401";
            var data = "busCode=" + busCode + "&roleSN=" + GroupID;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                var dataUse = resObj.dataUse;
                var dataNotUse = resObj.dataNotUse;

                if (resObj.status == "0") {
                    for (var i in dataUse) {
                        var dataFt = {};
                        dataFt.C1 = dataUse[i].work_num;
                        dataFt.C2 = dataUse[i].name;
                        dataFt.C3 = dataUse[i].gender == true ? "男" : "女";
                        dataFt.C4 = dataUse[i].department_name;
                        dataFt.C5 = dataUse[i].jobs;
                        dataFt.C6 = dataUse[i].user_group_name == null ? "无" : dataUse[i].user_group_name;
                        dataFt.C7 = dataUse[i].internal_user_id;
                        dataFt.C8 = true;

                        M1004.UID.M100401(dataFt);
                    }

                    for (var i in dataNotUse) {
                        var dataFt = {};
                        dataFt.C1 = dataNotUse[i].work_num;
                        dataFt.C2 = dataNotUse[i].name;
                        dataFt.C3 = dataNotUse[i].gender == true ? "男" : "女";
                        dataFt.C4 = dataNotUse[i].department_name;
                        dataFt.C5 = dataNotUse[i].jobs;
                        dataFt.C6 = dataNotUse[i].user_group_name == null ? "无" : dataNotUse[i].user_group_name;
                        dataFt.C7 = dataNotUse[i].internal_user_id;
                        dataFt.C8 = false

                        M1004.UID.M100401(dataFt);
                    }

                    M1004.UID.M100407();
                }
                else {

                }

                LoadingBoxCtl(0);
            }
            ajaxObj.start();
        },//加载员工数据
        EVM100402: function (GroupID, UserSN) {
            ///<summary>取消分配</summary>
            //console.log(GroupID + "__" + UserSN);
            //setTimeout("M1004.UID.M100402(true);", 200);

            var busCode = "M100402";
            var data = "busCode=" + busCode + "&roleSN=" + GroupID + "&internalUserSN=" + UserSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                if (res == "0") {
                    M1004.UID.M100402(true);
                }
                else {
                    M1004.UID.M100402(false);
                }
            }
            ajaxObj.error = function () {
                M1004.UID.M100402(false);
            }
            ajaxObj.start();
        },//取消分配
        EVM100403: function (GroupID, UserSN) {
            ///<summary>用户分配</summary>
            //console.log(GroupID + "__" + UserSN);
            //setTimeout("M1004.UID.M100403(true);", 200);

            var busCode = "M100403";
            var data = "busCode=" + busCode + "&roleSN=" + GroupID + "&internalUserSN=" + UserSN;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                if (res == "0" || res == "2") {
                    M1004.UID.M100403(true);
                }
                else {
                    M1004.UID.M100403(false);
                }
            }
            ajaxObj.error = function () {
                M1004.UID.M100403(false);
            }
            ajaxObj.start();
        },//用户分配
        EVM100404: function (Name, Remarks) {
            ///<summary>添加新岗位</summary>
            //var Data = { C1: Name, C2: 0, C3: Remarks, C4: "A7" };
            //setTimeout("M1004.UID.M100406(" + JSON.stringify(Data) + ");M1004.UID.M100404(true);", 200);

            var busCode = "M100404";
            var data = "busCode=" + busCode + "&roleName=" + Name + "&note=" + Remarks;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);

                if (resObj.status == "0") {
                    M1004.UID.M100404(true);

                    var dataFt = { C1: Name, C2: 0, C3: Remarks, C4: resObj.SN };

                    M1004.UID.M100406(dataFt);
                }
                else {
                    M1004.UID.M100404(false);
                }

                LoadingBoxCtl(0);
            }
            ajaxObj.error = function () {
                M1004.UID.M100404(false);

                LoadingBoxCtl(0);
            }
            ajaxObj.start();
        },
        EVM100405: function (GroupID) {
            ///<summary>删除岗位</summary>
            //setTimeout("M1004.UID.M100405(true);", 200);

            var busCode = "M100405";
            var data = "busCode=" + busCode + "&roleSN=" + GroupID;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                if (res == "0" || res == "1") {
                    M1004.UID.M100405(true);
                }
                else {
                    M1004.UID.M100405(false);
                }

                LoadingBoxCtl(0);
            }
            ajaxObj.error = function () {
                M1004.UID.M100405(false);

                LoadingBoxCtl(0);
            }
            ajaxObj.start();
        },
    },
    UID: {
        M100401: function (Data) {
            ///<summary>员工数据</summary>
            var NewRow = document.querySelector(".HalfContainerR>.ListContainer>.TbList>.Template").children[0].cloneNode(true);
            var NewRowTDs = NewRow.querySelectorAll("td");
            NewRowTDs[0].innerText = Data.C1; NewRowTDs[1].innerText = Data.C2; NewRowTDs[2].innerText = Data.C3; NewRowTDs[3].innerText = Data.C4; NewRowTDs[4].innerText = Data.C5; NewRowTDs[5].innerText = Data.C6;
            NewRow.setAttribute("data-SN", Data.C7);
            if (Data.C8) { NewRow.setAttribute("data-slt", 1); }
            document.querySelector(".HalfContainerR>.ListContainer>.TbList>tbody").appendChild(NewRow);
        },//员工列表
        M100402: function (Res) {
            ///<summary>取消分配</summary>
            if (Res) { M1004.FocusStaff.setAttribute("data-slt", 0); M1004.CountUser(); TransingStatus.SetStatus(3); } else { TransingStatus.SetStatus(2); }
        },
        M100403: function (Res) {
            ///<summary>用户分配</summary>
            if (Res) { M1004.FocusStaff.setAttribute("data-slt", 1); M1004.CountUser(); TransingStatus.SetStatus(3); } else { TransingStatus.SetStatus(2); }
        },
        M100404: function (Res) {
            ///<summary>添加新岗位</summary>
            if (Res) { TransingStatus.SetStatus(3); } else { TransingStatus.SetStatus(2); }
        },
        M100405: function (Res) {
            ///<summary>删除岗位</summary>
            if (Res) { TransingStatus.SetStatus(3); M1004.ClrUser(); document.querySelector(".HalfContainerL").removeChild(M1004.FocusItem); } else { TransingStatus.SetStatus(2); }
        },
        M100406: function (Data) {
            var Template = document.querySelector(".HalfContainerL>.Template");
            var NewItem = Template.children[0].cloneNode(true);
            NewItem.querySelector(".GroupName").innerText = Data.C1;
            NewItem.querySelector(".UserCnt").innerText = Data.C2;
            NewItem.querySelector(".MenuL1").innerText = Data.C3;
            NewItem.setAttribute("data-SN", Data.C4)
            if (Data.C2 > 0) { NewItem.querySelector(".ButtonBar>div[data-btst]").setAttribute("data-btst", "H"); };
            document.querySelector(".HalfContainerL").insertBefore(NewItem, Template);
            ReCalcItemSize();
        },
        M100407: function () {
            LoadingBoxCtl(0);
        }
    },
};