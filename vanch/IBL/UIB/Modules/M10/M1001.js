// JavaScript source code
var M1001 = {
    FocusItem: null,
    Init: function () {
        this.ShowDefaultUserList();
    },
    Button: function (BtObj, BtID) {
        if (BtID == 0) {
            this.SltItem(BtObj);
        }
        if (BtID > 10) {
            switch (BtID) {
                case 11: this.ShowButton(2); break;
                case 12: this.SaveIns(); this.ShowButton(3); break;
                case 13: this.ShowButton(3); break;
                case 14: this.ShowButton(5); break;
                case 15: this.SaveEdt(); this.ShowButton(1); break;
                case 16: this.ShowButton(1); break;
                case 17: this.SaveDel(); this.ShowButton(3); break;
                case 18: this.ShowButton(1); break;
                case 19: this.SaveSta(true); break;
                case 20: this.SaveSta(false); break;
                case 21: this.ShowButton(4); break;
                case 22: this.SavePwd(); break;
                case 23: this.ShowButton(1); break;
                default: break;
            }
        };
    },
    SaveDel: function () { this.DelItem(); },
    SaveEdt: function () { this.EditInternalUserInfo(); },
    SaveIns: function () { this.AddNewInternalUser();},
    SavePwd: function () {
        if (this.VerifyPwd() == false) {
            return;
        }

        this.ChangePassword();
    },
    VerifyPwd: function () {
        if (document.querySelector("#password").value == "") {
            alert("请输入密码");
            return false;
        }

        if (document.querySelector("#password").value != document.querySelector("#verifyPassword").value) {
            alert("两次输入的密码不相等");
            return false;
        }
        
        return true;
    },
    UpdItem: function (OnlyStatus, StatusCode) {//更新选中的显示记录
        if (OnlyStatus == true) {
            this.FocusItem.children[9].setAttribute("data-state", StatusCode);
            if (StatusCode == 0) { this.FocusItem.children[9].innerText = "待用"; }
            if (StatusCode == 1) { this.FocusItem.children[9].innerText = "暂停"; }
            if (StatusCode == 5) { this.FocusItem.children[9].innerText = "正常"; }
            return;
        };
        this.FocusItem.children[0].innerText = document.querySelector(".TbList>tfoot>.InputBar").children[0].firstChild.value;
        this.FocusItem.children[1].innerText = document.querySelector(".TbList>tfoot>.InputBar").children[1].firstChild.value;
        this.FocusItem.children[2].setAttribute("data-org", document.querySelector(".TbList>tfoot>.InputBar").children[2].firstChild.value);
        var IndexP = 0; if (document.querySelector(".TbList>tfoot>.InputBar").children[2].firstChild.value == 1) { IndexP = 0; }; if (document.querySelector(".TbList>tfoot>.InputBar").children[2].firstChild.value == 0) { IndexP = 1; }
        this.FocusItem.children[2].innerText = document.querySelector(".TbList>tfoot>.InputBar").children[2].firstChild.options[IndexP].text;
        this.FocusItem.children[4].innerText = document.querySelector(".TbList>tfoot>.InputBar").children[4].firstChild.value;
        this.FocusItem.children[5].innerText = document.querySelector(".TbList>tfoot>.InputBar").children[5].firstChild.value;
        //.......
    },
    PwdResetBox: function (status) {
        if (status == 0) { if (document.querySelector(".TbList>tfoot .PasswordReset").getAttribute("data-show") == 1) { document.querySelector(".TbList>tfoot .PasswordReset").setAttribute("data-show", "0"); }; }
        if (status == 1) { if (document.querySelector(".TbList>tfoot .PasswordReset").getAttribute("data-show") != 1) { document.querySelector(".TbList>tfoot .PasswordReset").setAttribute("data-show", "1"); }; }
        if (status == 2) { }//预留彻底重置框内信息与显示密码框
    },
    SaveSta: function (SetStatus) {
        if (SetStatus == true) { this.EnableOrDisableInternalUser(5); };
        if (SetStatus == false) { this.EnableOrDisableInternalUser(1); };
    },
    AddItemList: function (jsonData) {//增加一条显示记录,Data provide from ajax variable, usually suggest use callback function to call this.
        var Template = document.querySelector(".ListContainer>.TbList>.Template").firstChild.cloneNode(true);
        //...give data....
        Template.querySelectorAll("td")[0].setAttribute("internalUserId", jsonData.InternalUserId);
        Template.querySelectorAll("td")[0].innerText = jsonData.WorkNum;
        Template.querySelectorAll("td")[1].innerText = jsonData.Name;
        Template.querySelectorAll("td")[2].innerText = jsonData.Gender == true ? "男" : "女";
        var regDate = "";
        if (jsonData.RegDate != null) {
            var dateObj = eval("(new " + jsonData.RegDate.replace('/', '', 'g').replace('/', '', 'g') + ")");
            regDate = dateObj.toLocaleDateString();
        }
        Template.querySelectorAll("td")[3].innerText = regDate;
        Template.querySelectorAll("td")[4].innerText = jsonData.DepartmentName;
        Template.querySelectorAll("td")[5].innerText = jsonData.Jobs;
        Template.querySelectorAll("td")[6].innerText = jsonData.UserGroup;
        var allocateDate = "";
        if (jsonData.AllocateDate != null) {
            var dateObj = eval("(new " + jsonData.AllocateDate.replace('/', '', 'g').replace('/', '', 'g') + ")");
            allocateDate = dateObj.toLocaleDateString();
        }
        Template.querySelectorAll("td")[7].innerText = allocateDate;
        Template.querySelectorAll("td")[8].innerText = jsonData.Operater;
        var useStatus = "";
        if (jsonData.UseStatus == "5") {
            useStatus = "正常";
        }
        else if (jsonData.UseStatus == "1") {
            useStatus = "暂停";
        }
        else {
            useStatus = "待用";
        }

        Template.querySelectorAll("td")[9].innerText = useStatus;
        Template.querySelectorAll("td")[9].setAttribute("data-state", jsonData.UseStatus);
        document.querySelector(".ListContainer>.TbList>tbody").appendChild(Template);
    },
    AddOneItem: function(jsonObj){
        var Template = document.querySelector(".ListContainer>.TbList>.Template").firstChild.cloneNode(true);
        //把inpBar中的值写到template中
        Template.querySelectorAll("td")[0].setAttribute("internalUserId", jsonObj.internalUserId);
        Template.querySelectorAll("td")[0].innerText = document.querySelectorAll(".TbList>tfoot>.InputBar>td")[0].firstChild.value;
        Template.querySelectorAll("td")[1].innerText = document.querySelectorAll(".TbList>tfoot>.InputBar>td")[1].firstChild.value;
        Template.querySelectorAll("td")[2].innerText = document.querySelectorAll(".TbList>tfoot>.InputBar>td")[2].firstChild.value == 1 ? "男" : "女";
        Template.querySelectorAll("td")[3].innerText = new Date().toLocaleDateString();
        Template.querySelectorAll("td")[4].innerText = document.querySelectorAll(".TbList>tfoot>.InputBar>td")[4].firstChild.value;
        Template.querySelectorAll("td")[5].innerText = document.querySelectorAll(".TbList>tfoot>.InputBar>td")[5].firstChild.value;
        Template.querySelectorAll("td")[8].innerText = jsonObj.operator;
        Template.querySelectorAll("td")[9].innerText = "待用";
        Template.querySelectorAll("td")[9].setAttribute("data-state", "0");
        document.querySelector(".ListContainer>.TbList>tbody").appendChild(Template);
    },
    DelItem: function () {
        var busCode = "B1001";
        var opeType = "deleteInternalUser";
        var internalUserId = this.FocusItem.firstChild.getAttribute("internalUserId");
        var data = "busCode=" + busCode + "&opeType=" + opeType + "&internalUserId=" + internalUserId;

        var ajaxObj = new AJAXC();
        ajaxObj.data = data;
        ajaxObj.success = function () {
            document.querySelector(".ListContainer>.TbList>tbody").removeChild(M1001.FocusItem);
        }
        ajaxObj.start();

        //parent.AJAXM.data = params;
        //parent.AJAXM.success = function () {
        //    document.querySelector(".ListContainer>.TbList>tbody").removeChild(M1001.FocusItem);
        //}
        //parent.AJAXM.send();
    },
    SltItem: function (TheItem) {
        if (this.FocusItem != null && TheItem != this.FocusItem) { this.FocusItem.setAttribute("data-slt", 0); }; this.FocusItem = TheItem;
        if (this.FocusItem.getAttribute("data-slt") == 1) {
            this.FocusItem.setAttribute("data-slt", 0);
            this.ShowButton(3);
        }
        else {
            this.FocusItem.setAttribute("data-slt", 1);
            if (this.FocusItem.lastChild.getAttribute("data-state") != 5) { this.ShowButton(1); }
            if (this.FocusItem.lastChild.getAttribute("data-state") == 5) { this.ShowButton(6); }
        }
    },
    ShowButton: function (OPType) {//1-selection ACD;2-Input Status Add;3-default status;4-resetpassword;5-Input status Edit;6-selection ACE
        if (OPType == 1) {
            this.ShowInputBar(false, false);
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[0], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[1], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[2], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[3], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[4], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[5], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[6], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[7], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[8], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[9], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[10], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[11], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[12], "H");
            this.PwdResetBox(0);
        }
        if (OPType == 2) {
            this.ShowInputBar(true, false);
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[0], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[1], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[2], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[3], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[4], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[5], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[6], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[7], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[8], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[9], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[10], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[11], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[12], "H");
        }
        if (OPType == 3) {
            this.ShowInputBar(false, false);
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[0], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[1], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[2], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[3], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[4], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[5], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[6], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[7], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[8], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[9], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[10], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[11], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[12], "H");
            this.PwdResetBox(0);
        }
        if (OPType == 4) {
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[0], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[1], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[2], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[3], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[4], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[5], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[6], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[7], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[8], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[9], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[10], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[11], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[12], "U");
            this.PwdResetBox(1);
        }
        if (OPType == 5) {
            this.ShowInputBar(true, true);
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[0], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[1], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[2], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[3], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[4], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[5], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[6], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[7], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[8], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[9], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[10], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[11], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[12], "H");
        }
        if (OPType == 6) {
            this.ShowInputBar(false, false);
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[0], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[1], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[2], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[3], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[4], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[5], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[6], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[7], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[8], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[9], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[10], "U");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[11], "H");
            BtStatus(document.querySelectorAll(".TbList>tfoot .ButtonBar>div[data-btst]")[12], "H");
            this.PwdResetBox(0);
        }
    },
    ShowInputBar: function (Show, DataPrep) {
        if (Show == true) {
            document.querySelector(".TbList>tfoot>.InputBar").setAttribute("data-inp", "1");
            if (DataPrep == true) {
                document.querySelector(".TbList>tfoot>.InputBar").children[0].firstChild.setAttribute("internalUserId", this.FocusItem.children[0].getAttribute("internalUserId"));
                document.querySelector(".TbList>tfoot>.InputBar").children[0].firstChild.value = this.FocusItem.children[0].innerText;
                document.querySelector(".TbList>tfoot>.InputBar").children[1].firstChild.value = this.FocusItem.children[1].innerText;
                document.querySelector(".TbList>tfoot>.InputBar").children[2].firstChild.value = this.FocusItem.children[2].getAttribute("data-org");
                document.querySelector(".TbList>tfoot>.InputBar").children[3].firstChild.value = this.FocusItem.children[3].innerText;
                document.querySelector(".TbList>tfoot>.InputBar").children[4].firstChild.value = this.FocusItem.children[4].innerText;
                document.querySelector(".TbList>tfoot>.InputBar").children[5].firstChild.value = this.FocusItem.children[5].innerText;
                document.querySelector(".TbList>tfoot>.InputBar").children[6].innerText = this.FocusItem.children[6].innerText;
                document.querySelector(".TbList>tfoot>.InputBar").children[7].innerText = this.FocusItem.children[7].innerText;
                document.querySelector(".TbList>tfoot>.InputBar").children[8].innerText = this.FocusItem.children[8].innerText;
                document.querySelector(".TbList>tfoot>.InputBar").children[9].innerText = this.FocusItem.children[9].innerText;
            }
        }
        if (Show == false) { document.querySelector(".TbList>tfoot>.InputBar").setAttribute("data-inp", "0"); }
    },
    M1001RD: function () {//读取数据
        this.FocusModule = M090101;
        this.ShowButton(1);
        //M090101.querySelectorAll(".ItemText")[0].DspV = "1";
        //M090101.querySelectorAll(".ItemText")[1].DspV = "2";
        //M090101.querySelectorAll(".ItemText")[3].DspV = "3";
        //M090101.querySelectorAll(".ItemText")[5].DspV = "4";
        //M090101.querySelectorAll(".ItemText")[4].DspV = "5";
        //M090101.querySelectorAll(".ItemText")[5].DspV = "6";
        //M090101.querySelectorAll(".ItemText")[6].DspV = "7";
    },

    M1001SD: function () {//回写数据
        TransingStatus.SetStatus(1);
        //.........
        setTimeout("TransingStatus.SetStatus(3);", 200);
    },

    //把inputBar中的内容变为jsonStr
    inpBarToJsonStr:function (){
        var jsonObj = {};
        jsonObj.internal_user_id = document.querySelectorAll(".TbList>tfoot>.InputBar>td")[0].firstChild.getAttribute("internalUserId");
        if (jsonObj.internal_user_id == null) {
            jsonObj.internal_user_id = 0;
        }
        jsonObj.work_num = document.querySelectorAll(".TbList>tfoot>.InputBar>td")[0].firstChild.value.trim();
        jsonObj.pwd = "123456";
        jsonObj.name = document.querySelectorAll(".TbList>tfoot>.InputBar>td")[1].firstChild.value.trim();
        jsonObj.gender = document.querySelectorAll(".TbList>tfoot>.InputBar>td")[2].firstChild.value.trim() == "1" ? true : false;
        jsonObj.reg_date = new Date();
        jsonObj.department_name = document.querySelectorAll(".TbList>tfoot>.InputBar>td")[4].firstChild.value.trim();
        jsonObj.jobs = document.querySelectorAll(".TbList>tfoot>.InputBar>td")[5].firstChild.value.trim();
        jsonObj.user_group = null;
        jsonObj.fk_user_group_id = null;
        jsonObj.allocate_date = new Date();
        jsonObj.operater = null;
        jsonObj.use_status = '3';

        return JSON.stringify(jsonObj);
    },

    //显示用户管理中所有的用户列表
    ShowDefaultUserList: function () {
        var busCode = "B1001";
        var opeType = "getInternalUserList";
        var params = "busCode=" + busCode + "&opeType=" + opeType;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            var jsonData = eval("(" + res + ")");

            for (var i in jsonData) {
                M1001.AddItemList(jsonData[i]);

                M1001.ShowButton(3);
            }

            document.querySelector(".ButtonBar").style.display = "block";
        }
        ajaxObj.start();
    },

    //新增用户时，判断是否已经有此工号
    IsExistWorkNum : function (workNum) {
        var workerList = document.querySelector(".TbList>tbody").querySelectorAll("tr");
        for (var i = 0; i < workerList.length;i++){
            var trWorkNum = workerList[i].querySelectorAll("td")[0].innerText.trim();
            if (workNum == trWorkNum) {
                return true;
            }
        }

        return false;
    },

    //新增一个内部用户，ajax
    AddNewInternalUser: function () {
        var workNum = document.querySelectorAll(".TbList>tfoot>.InputBar>td")[0].firstChild.value.trim();
        var reg = new RegExp("^[0-9]{3}$");
        if (!reg.test(workNum)) {
            alert("请输入三位数字工号");
            return;
        }
        if (this.IsExistWorkNum(workNum) == true) {
            alert("此用户已经存在");
            return;
        }

        var busCode = "B1001";
        var opeType = "addOneInternalUser";
        var jsonStr = this.inpBarToJsonStr();
        var params = "busCode=" + busCode + "&opeType=" + opeType + "&jsonStr=" + jsonStr;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            var jsonObj = eval('(' + res + ')');
            M1001.AddOneItem(jsonObj);
        }
        ajaxObj.start();
    },

    //修改一个内部用户信息,ajax
    EditInternalUserInfo: function () {
        var busCode = "B1001";
        var opeType = "editInternalUserInfo";
        var jsonStr = this.inpBarToJsonStr();
        var params = "busCode=" + busCode + "&opeType=" + opeType + "&jsonStr=" + jsonStr;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            M1001.UpdItem(false, null);
        }
        ajaxObj.start();
    },

    //启用或者停止一个内部用户,ajax
    EnableOrDisableInternalUser: function (status) {
        var busCode = "B1001";
        var opeType = "enableOrDisableInternalUser";
        var internalUserId = this.FocusItem.firstChild.getAttribute("internalUserId");
        var params = "busCode=" + busCode + "&opeType=" + opeType + "&internalUserId=" + internalUserId + "&status=" + status;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            if (status == 5) {
                M1001.UpdItem(true, 5);
                M1001.ShowButton(6);
            }
            else {
                M1001.UpdItem(true, 1);
                M1001.ShowButton(1);
            }
        }
        ajaxObj.start();
    },

    //修改密码,ajax
    ChangePassword: function () {
        var busCode = "B1001";
        var opeType = "changePassword";
        var internalUserId = this.FocusItem.firstChild.getAttribute("internalUserId");
        var newPassword = document.querySelector("#password").value;
        var params = "busCode=" + busCode + "&opeType=" + opeType + "&internalUserId=" + internalUserId + "&newPassword=" + newPassword;

        var ajaxObj = new AJAXC();
        ajaxObj.data = params;
        ajaxObj.success = function (res) {
            M1001.ShowButton(1);
        }
        ajaxObj.start();
    },
};
