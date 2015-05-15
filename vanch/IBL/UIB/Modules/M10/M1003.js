// JavaScript source code
var M1003 = {
    FocusItem: null,
    Init: function () {
        this.LoadGroupData();
    },

    Button: function (BtObj, BtID) {
        if (BtID == 0) {
            if (BtObj.getAttribute("data-slt") == "1") { this.GroupSlt(BtObj, false); } else { this.GroupSlt(BtObj, true); };
        }
        if (BtID == 1) {
            if (BtObj.getAttribute("data-slt") == "1") { this.DelPermissionMenuFromGroup(BtObj); } else { this.AddPermissionMenuToGroup(BtObj); };
        }
    },
    //Button: function (BtObj, Act, BtID) {
    //    if (BtID == 0) {
    //        if (Act == 0) { };
    //        if (Act == 1) {
    //            if (BtObj.getAttribute("data-slt") == "1") { this.GroupSlt(BtObj, false); } else { this.GroupSlt(BtObj, true); };
    //        }
    //        if (Act == 2) { };
    //    }
    //    if (BtID == 1) {
    //        if (Act == 0) { };
    //        if (Act == 1) { if (BtObj.getAttribute("data-slt") == "1") { this.DelPermissionMenuFromGroup(BtObj); } else { this.AddPermissionMenuToGroup(BtObj);  }; }
    //        if (Act == 2) { };
    //    }
    //    if (BtID == 2) {
    //        if (Act == 0) { BtStatusClass(BtObj, 2); };
    //        if (Act == 1) { }
    //        if (Act == 2) { BtStatusClass(BtObj, 12); };
    //    }
    //},
    GroupSlt: function (TheItem, Slt) {
        if (Slt == true) {
            if (TheItem.getAttribute("data-slt") == "1") { return; };
            if (this.FocusItem != null) { this.FocusItem.setAttribute("data-slt", 0); this.ClrMod(); };
            TheItem.setAttribute("data-slt", 1); this.FocusItem = TheItem;
            this.LoadModData(TheItem);
        };
        if (Slt == false) { if (TheItem.getAttribute("data-slt") == "1") { TheItem.setAttribute("data-slt", 0); this.ClrMod(); } };
    },
    PerGroupAdd: function (GroupId, GroupName, UserCnt, MenuL1Cnt, MenuL2Cnt) {
        var Template = document.querySelector(".HalfContainerL>.Template");
        var NewItem = Template.children[0].cloneNode(true);
        NewItem.querySelector(".GroupName").setAttribute("userGroupId", GroupId);
        NewItem.querySelector(".GroupName").innerText = GroupName;
        NewItem.querySelector(".UserCnt").innerText = UserCnt;
        NewItem.querySelector(".MenuL1Cnt").innerText = MenuL1Cnt;
        NewItem.querySelector(".MenuL2Cnt").innerText = MenuL2Cnt;
        document.querySelector(".HalfContainerL").insertBefore(NewItem, Template);
        ReCalcItemSize();
    },
    LoadGroupData: function () {
        LoadingBoxCtl(1);
        //.........
        //setTimeout("TransingStatus.SetStatus(3);", 200);
        //this.PerGroupAdd("董事长", 1, 3, 7);
        //this.PerGroupAdd("系统管理", 5, 3, 7);
        //this.PerGroupAdd("权限管理", 0, 3, 7);
        //this.PerGroupAdd("总经理", 2, 3, 7);
        //this.PerGroupAdd("客服人员", 5, 3, 7);
        //this.PerGroupAdd("审核组", 5, 3, 7);
        //this.PerGroupAdd("客服经理", 5, 3, 7);

        this.GetUserGroupStatisticsInfo();
    },
    LoadModData: function (TheItem) {
        LoadingBoxCtl(1);
        //.........
        //setTimeout("TransingStatus.SetStatus(3);", 200);

        this.GetGroupPermissionMenu(TheItem);

        //this.AddMod("M1001", "内部用户", "用户管理"); this.AddMod("M1002", "内部用户", "权限分配"); this.AddMod("M1003", "内部用户", "权限菜单"); this.AddMod(); this.AddMod(); this.AddMod(); this.AddMod(); this.AddMod(); this.AddMod(); this.AddMod(); this.AddMod();
    },
    SaveGMRe: function () {
        LoadingBoxCtl(0);
        //.........
        //setTimeout("TransingStatus.SetStatus(3);", 200);
    },
    AddMod: function (jsonObj) {
        var Template = document.querySelector(".HalfContainerR .TbList>.Template");
        var NewItem = Template.children[0].cloneNode(true);
        var isSlt = jsonObj.isAdd == "true" ? "1" : "0";
        NewItem.setAttribute("data-slt", isSlt);
        NewItem.querySelectorAll("td")[0].setAttribute("menuId", jsonObj.menuId);
        NewItem.querySelectorAll("td")[0].innerText = jsonObj.menuCode;
        NewItem.querySelectorAll("td")[1].innerText = jsonObj.menuParentTitle != null ? jsonObj.menuParentTitle : "";
        NewItem.querySelectorAll("td")[2].innerText = jsonObj.menuTitle;
        document.querySelector(".HalfContainerR .TbList tbody").appendChild(NewItem);
    },
    ClrMod: function () { document.querySelector(".HalfContainerR .TbList tbody").innerHTML = ""; },

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
                M1003.PerGroupAdd(jsonObj[i].UserGroupId, jsonObj[i].UserGroupName, jsonObj[i].UserAmount, jsonObj[i].FirstMenuAmount, jsonObj[i].SecondMenuAmount);
            };

            LoadingBoxCtl(0);
        }
        ajaxObj.start();
    },

    //获得点击的用户组的权限菜单
    GetGroupPermissionMenu: function (TheItem) {
        var busCode = "B1003";
        var opeType = "getGroupPermissionMenu";
        var userGroupId = TheItem.querySelector(".GroupName").getAttribute("userGroupId");
        var params = "busCode=" + busCode + "&opeType=" + opeType + "&userGroupId=" + userGroupId;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            var jsonObjList = eval("(" + res + ")");
            for (var i in jsonObjList) {
                M1003.AddMod(jsonObjList[i]);

                LoadingBoxCtl(0);
            }
        }
        ajaxObj.start();
    },

    //添加一个权限菜单到一个用户组中
    AddPermissionMenuToGroup: function(TheItem){
        var busCode = "B1003";
        var opeType = "addPermissionMenuToGroup";
        var menuId = TheItem.children[0].getAttribute("menuId");
        var userGroupId = document.querySelector(".HalfContainerL [data-slt = '1'] .GroupName").getAttribute("userGroupId");
        var params = "busCode=" + busCode + "&opeType=" + opeType + "&menuId=" + menuId + "&userGroupId=" + userGroupId;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            TheItem.setAttribute("data-slt", 1);

            //一级菜单
            if (TheItem.children[1].innerText == "") {
                document.querySelector(".HalfContainerL [data-slt = '1'] .MenuL1Cnt").innerText = parseInt(document.querySelector(".HalfContainerL [data-slt = '1'] .MenuL1Cnt").innerText) + 1;
            }
                //二级菜单
            else {
                document.querySelector(".HalfContainerL [data-slt = '1'] .MenuL2Cnt").innerText = parseInt(document.querySelector(".HalfContainerL [data-slt = '1'] .MenuL2Cnt").innerText) + 1;
            }

            TheItem.setAttribute("onmouseup", "M1003.Button(this, 1, 1)");
        };
        ajaxObj.error = function (xmlhttp) {
            alert("ajax error!\nreadyState:" + xmlhttp.readyState + "\nstatus:" + xmlhttp.status + "\nstatusText:" + xmlhttp.statustext);
            thisItem.setAttribute("onmouseup", "M1003.Button(this, 1, 1)");
        };
        ajaxObj.start();

        TheItem.setAttribute("onmouseup", "");
    },

    //从一个用户组中删除一个权限菜单
    DelPermissionMenuFromGroup: function (TheItem) {
        var busCode = "B1003";
        var opeType = "delPermissionMenuFromGroup";
        var menuId = TheItem.children[0].getAttribute("menuId");
        var userGroupId = document.querySelector(".HalfContainerL [data-slt = '1'] .GroupName").getAttribute("userGroupId");
        var params = "busCode=" + busCode + "&opeType=" + opeType + "&menuId=" + menuId + "&userGroupId=" + userGroupId;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            TheItem.setAttribute("data-slt", 0);
            //一级菜单
            if (TheItem.children[1].innerText == "") {
                document.querySelector(".HalfContainerL [data-slt = '1'] .MenuL1Cnt").innerText = parseInt(document.querySelector(".HalfContainerL [data-slt = '1'] .MenuL1Cnt").innerText) - 1;
            }
                //二级菜单
            else {
                document.querySelector(".HalfContainerL [data-slt = '1'] .MenuL2Cnt").innerText = parseInt(document.querySelector(".HalfContainerL [data-slt = '1'] .MenuL2Cnt").innerText) - 1;
            }

            TheItem.setAttribute("onmouseup", "M1003.Button(this, 1, 1)");
        };
        ajaxObj.error = function (xmlhttp) {
            alert("ajax error!\nreadyState:" + xmlhttp.readyState + "\nstatus:" + xmlhttp.status + "\nstatusText:" + xmlhttp.statustext);
            thisItem.setAttribute("onmouseup", "M1003.Button(this, 1, 1)");
        };
        ajaxObj.start();

        TheItem.setAttribute("onmouseup", "");
    },
};