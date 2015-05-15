// JavaScript source code
var M1002 = {
    FocusItem: null,
    Init: function () {
        this.LoadGroupData();
    },
    Button: function (BtObj, BtID) {
        if (BtID == 1) {//用户组
            var ThisItem = BtObj.parentElement;
            if (ThisItem.getAttribute("data-slt") == "1") { this.GroupSlt(ThisItem, false); } else { this.GroupSlt(ThisItem, true); };
        }
        if (BtID == 2) {//用户
             if (BtObj.getAttribute("data-slt") == "1") { this.DelUserFromGroup(BtObj); } else { this.AddUserToGroup(BtObj); };
        }
        if (BtID > 10) {
            BtStatus(BtObj, "E");
            var ThisItem = BtObj.parentElement.parentElement;
            switch (BtID) {
                case 11:
                    this.DelUserGroup(ThisItem);
                    break;
                case 12:
                    this.AddUserGroup();

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
    //Button: function (BtObj, Act, BtID) {
    //    if (BtID == 1) {//用户组
    //        var ThisItem = BtObj.parentElement;
    //        if (Act == 0) { };
    //        if (Act == 1) {
    //            if (ThisItem.getAttribute("data-slt") == "1") { this.GroupSlt(ThisItem, false); } else { this.GroupSlt(ThisItem, true); };
    //        }
    //        if (Act == 2) { };
    //    }
    //    if (BtID == 2) {//用户
    //        if (Act == 0) { };
    //        if (Act == 1) { if (BtObj.getAttribute("data-slt") == "1") { this.DelUserFromGroup(BtObj); } else { this.AddUserToGroup(BtObj);  }; }
    //        if (Act == 2) { };
    //    }
    //    if (BtID > 10) {
    //        if (Act == 0) { BtStatusClass(BtObj, 2); };
    //        if (Act == 1) {
    //            BtStatusClass(BtObj, 12);
    //            var ThisItem = BtObj.parentElement.parentElement;
    //            switch (BtID) {
    //                case 11:
    //                    this.DelUserGroup(ThisItem);
    //                    break;
    //                //保存
    //                case 12:
    //                    this.AddUserGroup();

    //                    ThisItem.querySelector('.Mask').style.display = '';
    //                    ThisItem.querySelector('.ButtonBar').setAttribute('data-bb', '0');
    //                    break;
    //                //取消
    //                case 13:
    //                    ThisItem.querySelector('.Mask').style.display = '';
    //                    ThisItem.querySelector('.ButtonBar').setAttribute('data-bb', '0');
    //                    break;
    //                default:
    //                    break;
    //            }
    //        };
    //        if (Act == 2) { BtStatusClass(BtObj, 12); };
    //    }
    //},
    GroupSlt: function (TheItem, Slt) {
        if (Slt == true) {
            if (TheItem.getAttribute("data-slt") == "1") { return; };
            if (this.FocusItem != null) { this.FocusItem.setAttribute("data-slt", 0); this.ClrUser(); };
            TheItem.setAttribute("data-slt", 1);
            this.FocusItem = TheItem;
            this.LoadUserData(TheItem);
        };
        if (Slt == false) { if (TheItem.getAttribute("data-slt") == "1") { TheItem.setAttribute("data-slt", 0); this.ClrUser(); } };
    },

    ReadOnlyMask: function (ReadOnly) {
        if (ReadOnly == true) { this.FocusItem.querySelector(".ReadOnlyMask").setAttribute('data-mask', 1); }
        if (ReadOnly == false) { this.FocusItem.querySelector(".ReadOnlyMask").setAttribute('data-mask', 0); }
    },
    PerGroupAdd: function (GroupId,GroupName, UserCnt, MenuL1Cnt, MenuL2Cnt) {
        var Template = document.querySelector(".HalfContainerL>.Template");
        var NewItem = Template.children[0].cloneNode(true);
        NewItem.querySelector(".GroupName").setAttribute("userGroupId", GroupId);
        NewItem.querySelector(".GroupName").innerText = GroupName;
        NewItem.querySelector(".UserCnt").innerText = UserCnt;
        NewItem.querySelector(".MenuL1Cnt").innerText = MenuL1Cnt;
        NewItem.querySelector(".MenuL2Cnt").innerText = MenuL2Cnt;
        if (UserCnt > 0) { NewItem.querySelector(".ButtonBar>div[data-btst]").setAttribute("data-btst", "H"); };
        document.querySelector(".HalfContainerL").insertBefore(NewItem, Template);
        ReCalcItemSize();
    },
    PerGroupDel: function () {

    },
    LoadGroupData: function () {
        LoadingBoxCtl(1);
        //.........
        //setTimeout("TransingStatus.SetStatus(3);", 200);

        this.GetUserGroupStatisticsInfo();
    },
    LoadUserData: function (thiItem) {
        //TransingStatus.SetStatus(1);
        //.........
        //setTimeout("TransingStatus.SetStatus(3);", 200);
        this.ShowGroupOrUnallocateUser(thiItem);
        //this.AddUser(); this.AddUser(); this.AddUser(); this.AddUser(); this.AddUser(); this.AddUser(); this.AddUser(); this.AddUser(); this.AddUser(); this.AddUser(); this.AddUser(); this.AddUser(); this.AddUser(); this.AddUser();
    },
    SaveGURe: function () {
        //TransingStatus.SetStatus(1);
        //.........
        //setTimeout("TransingStatus.SetStatus(3);", 200);
    },
    ShowButton: function (Show) { },
    SaveUserSlt: function () { },
    AddUser: function (jsonObj) {
        var Template = document.querySelector(".HalfContainerR .TbList>.Template");
        var NewItem = Template.children[0].cloneNode(true);

        NewItem.children[0].innerText = jsonObj.WorkNum;
        NewItem.children[0].setAttribute("internalUserId", jsonObj.InternalUserId);
        NewItem.children[1].innerText = jsonObj.Name;
        NewItem.children[2].innerText = jsonObj.Gender == true ? "男" : "女";
        NewItem.children[3].innerText = jsonObj.DepartmentName;
        NewItem.children[4].innerText = jsonObj.Jobs;
        NewItem.children[5].innerText = jsonObj.UserGroup;

        if (jsonObj.UserGroup != "") {
            NewItem.setAttribute("data-slt", "1");
        }

        document.querySelector(".HalfContainerR .TbList tbody").appendChild(NewItem);
    },
    ClrUser: function () { document.querySelector(".HalfContainerR .TbList tbody").innerHTML = ""; },
    
    //获取用户组统计信息,ajax
    GetUserGroupStatisticsInfo: function () {
        var busCode = "B1002";
        var opeType = "getUserGroupStatisticsInfo";
        var params = "busCode=" + busCode + "&opeType=" + opeType;
        var url = "Control.ashx?" + params;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            var jsonObj = eval("(" + res + ")");

            for (var i in jsonObj) {
                M1002.PerGroupAdd(jsonObj[i].UserGroupId, jsonObj[i].UserGroupName, jsonObj[i].UserAmount, jsonObj[i].FirstMenuAmount, jsonObj[i].SecondMenuAmount);
            };

            LoadingBoxCtl(0);
        }
        ajaxObj.start();
    },

    //添加一个用户组
    AddUserGroup: function () {
        var busCode = "B1002";
        var opeType = "addUserGroup";
        var userGroupName = document.querySelector("#inpUserGroupName").value;
        var params = "busCode=" + busCode + "&opeType=" + opeType + "&userGroupName=" + userGroupName;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            M1002.PerGroupAdd(res, userGroupName, 0, 0, 0);
        }
        ajaxObj.start();
    },

    //删除一个用户组
    DelUserGroup: function (ThisItem) {
        var busCode = "B1002";
        var opeType = "delUserGroup";
        var userGroupId = ThisItem.querySelector(".GroupName").getAttribute("userGroupId");
        var params = "busCode=" + busCode + "&opeType=" + opeType + "&userGroupId=" + userGroupId;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            document.querySelector(".HalfContainerL").removeChild(ThisItem);
        }
        ajaxObj.start();
    },

    //显示一个用户组的用户以及未分配用户组的用户
    ShowGroupOrUnallocateUser: function (ThisItem) {
        var busCode = "B1002";
        var opeType = "getGroupOrunAllocateUser";
        var userGroupId = ThisItem.querySelector(".GroupName").getAttribute("userGroupId");
        var params = "busCode=" + busCode + "&opeType=" + opeType + "&userGroupId=" + userGroupId;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            var jsonObjList = eval("(" + res + ")");

            for (var i in jsonObjList) {
                M1002.AddUser(jsonObjList[i]);
            }
        }
        ajaxObj.start();
    },

    //添加一个用户到用户组中
    AddUserToGroup: function (thisItem) {
        var busCode = "B1002";
        var opeType = "addUserToGroup";
        var userGroupId = document.querySelector(".HalfContainerL [data-slt='1'] .GroupName").getAttribute("userGroupId");
        var userGroupName = document.querySelector(".HalfContainerL [data-slt='1'] .GroupName").innerText;
        var internalUserId = thisItem.children[0].getAttribute("internalUserId");
        var params = "busCode=" + busCode + "&opeType=" + opeType + "&userGroupId=" + userGroupId + "&userGroupName=" + userGroupName + "&internalUserId=" + internalUserId;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            document.querySelector(".HalfContainerL [data-slt='1'] .UserCnt").innerText = parseInt(document.querySelector(".HalfContainerL [data-slt='1'] .UserCnt").innerText) + 1;
            thisItem.setAttribute("data-slt", 1);
            thisItem.children[5].innerText = userGroupName;

            document.querySelector(".HalfContainerL [data-slt='1'] .ButtonBar div").setAttribute("data-btst", "H");

            thisItem.setAttribute("onmouseup", "M1002.Button(this,2)");
        }
        ajaxObj.error = function (xmlhttp) {
            alert("ajax error!\nreadyState:" + xmlhttp.readyState + "\nstatus:" + xmlhttp.status + "\nstatusText:" + xmlhttp.statustext);
            thisItem.setAttribute("onmouseup", "M1002.Button(this,11)");
        }
        ajaxObj.start();
        //禁用
        thisItem.setAttribute("onmouseup", "");
    },

    //从一个用户组中删除一个用户
    DelUserFromGroup: function (thisItem) {
        var busCode = "B1002";
        var opeType = "delUserFromGroup";
        var internalUserId = thisItem.children[0].getAttribute("internalUserId");
        var params = "busCode=" + busCode + "&opeType=" + opeType + "&internalUserId=" + internalUserId;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            document.querySelector(".HalfContainerL [data-slt='1'] .UserCnt").innerText = parseInt(document.querySelector(".HalfContainerL [data-slt='1'] .UserCnt").innerText) - 1;
            thisItem.setAttribute("data-slt", 0);
            thisItem.children[5].innerText = "";

            if (document.querySelector(".HalfContainerL [data-slt='1'] .UserCnt").innerText == 0) {
                document.querySelector(".HalfContainerL [data-slt='1'] .ButtonBar div").setAttribute("data-btst","E");
            }

            thisItem.setAttribute("onmouseup", "M1002.Button(this,2)");
        }
        ajaxObj.error = function (xmlhttp) {
            alert("ajax error!\nreadyState:" + xmlhttp.readyState + "\nstatus:" + xmlhttp.status + "\nstatusText:" + xmlhttp.statustext);
            thisItem.setAttribute("onmouseup", "M1002.Button(this,11)");
        }
        ajaxObj.start();
    },
};