// JavaScript source code
var M1001 = {
    FocusItem: null,
    FirstLoadNum: 15,
    GetStaffSN: function () {
        return this.FocusItem.getAttribute("data-SN");
    },
    Init: function () {
        LoadingBoxCtl(0);
        this.STR.EVINIT(this.FirstLoadNum);
    },
    Button: function (BtObj, BtID) {
        if (BtID == 0) {
            this.SltItem(BtObj);
        }
        if (BtID > 10) {
            switch (BtID) {
                case 11: this.InitAdd(); this.ShowButton(2); break;
                case 12: this.SaveIns("Add"); break; 
                case 13: this.ShowButton(3); break;
                case 14: this.ShowButton(5); break;
                case 15: this.SaveIns("Edit"); break;
                case 16: this.ShowButton(1); break;
                case 17: TransingStatus.SetStatus(1); this.STR.EVM100102(this.GetStaffSN()); break;
                case 18: this.ShowButton(1); break;
                case 19: TransingStatus.SetStatus(1); this.STR.EVM100101(this.GetStaffSN(), true); break;
                case 20: TransingStatus.SetStatus(1); this.STR.EVM100101(this.GetStaffSN(), false); break;
                case 21: this.PwdResetBox(2); this.ShowButton(4); break;
                case 22: this.ResetPwd(); break;
                case 23: this.ShowButton(1); break;
                default: break;
            }
        };
    },
    ResetPwd: function () {
        if (this.InterCK("PwdReset")) {
            var Inp = MainZone.querySelectorAll(".PasswordReset input");
            TransingStatus.SetStatus(1);
            this.STR.EVM100103(this.GetStaffSN(), Inp[0].value);
        }
    },
    SaveIns: function (Act) {
        if (this.InterCK("Save")) {
            TransingStatus.SetStatus(1);
            var Staff = { C1: "员工工号", C2: "真实姓名", C3: "性别", C4: "部门名称", C5: "岗位" };
            Staff.C1 = document.querySelector(".TbList>tfoot>.InputBar").children[0].firstChild.value;
            Staff.C2 = document.querySelector(".TbList>tfoot>.InputBar").children[1].firstChild.value;
            Staff.C3 = document.querySelector(".TbList>tfoot>.InputBar").children[2].firstChild.value;
            Staff.C4 = document.querySelector(".TbList>tfoot>.InputBar").children[4].firstChild.value;
            Staff.C5 = document.querySelector(".TbList>tfoot>.InputBar").children[5].firstChild.value;
            if (Act == "Edit") { this.STR.EVM100104(this.GetStaffSN(), Staff); } else { this.STR.EVM100105(Staff); }
        } 
    },
    PwdResetBox: function (status) {
        if (status == 0) { if (document.querySelector(".TbList>tfoot .PasswordReset").getAttribute("data-show") == 1) { document.querySelector(".TbList>tfoot .PasswordReset").setAttribute("data-show", "0"); }; }
        if (status == 1) { if (document.querySelector(".TbList>tfoot .PasswordReset").getAttribute("data-show") != 1) { document.querySelector(".TbList>tfoot .PasswordReset").setAttribute("data-show", "1"); }; }
        if (status == 2) { var PS = MainZone.querySelector(".PasswordReset"); PS.querySelectorAll("input")[0].value = ""; PS.querySelectorAll("input")[1].value = ""; PS.querySelectorAll(".PlaceHolder")[0].removeAttribute("style"); PS.querySelectorAll(".PlaceHolder")[1].removeAttribute("style"); }//彻底重置框内信息与显示密码框
    },
    SaveSta: function (SetStatus) {
        if (SetStatus == true) { this.UpdItem(5); this.ShowButton(6); };
        if (SetStatus == false) { this.UpdItem(1); this.ShowButton(1); };
    },
    InitAdd: function () {//重置信息框
        var InputBar = document.querySelector(".TbList>tfoot>.InputBar").children;
        InputBar[0].firstChild.value = ""
        InputBar[1].firstChild.value = "";
        InputBar[2].firstChild.value = 1;
        InputBar[3].innerText = "";
        InputBar[4].firstChild.value = "";
        InputBar[5].firstChild.value = "";
        InputBar[6].innerText = "";
        InputBar[7].innerText = "";
        InputBar[8].innerText = "";
        InputBar[9].innerText = "";
    },
    UpdItem: function (StatusCode) {//更新选中的显示记录
        this.FocusItem.children[9].setAttribute("data-state", StatusCode);
        if (StatusCode == 0) { this.FocusItem.children[9].innerText = "待用"; }
        if (StatusCode == 1) { this.FocusItem.children[9].innerText = "暂停"; }
        if (StatusCode == 5) { this.FocusItem.children[9].innerText = "正常"; }
    },
    DelItem: function () {
        document.querySelector(".ListContainer>.TbList>tbody").removeChild(this.FocusItem);
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
                document.querySelector(".TbList>tfoot>.InputBar").children[0].firstChild.value = this.FocusItem.children[0].innerText;
                document.querySelector(".TbList>tfoot>.InputBar").children[1].firstChild.value = this.FocusItem.children[1].innerText;
                document.querySelector(".TbList>tfoot>.InputBar").children[2].firstChild.value = this.FocusItem.children[2].getAttribute("data-org");
                document.querySelector(".TbList>tfoot>.InputBar").children[3].innerText = this.FocusItem.children[3].innerText;
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
    InterCK: function (Act) {
        var Str = true;
        switch (Act) {
            case "PwdReset":
                var Inp = MainZone.querySelectorAll(".PasswordReset input");
                if (Inp[0].value.length < 6) { Inp[0].parentNode.querySelector(".PlaceHolder").setAttribute("style","display:none"); IRC.ErrTip(Inp[0], "请输入大于6位的密码"); Str = false; }
                if (Inp[1].value.length < 6) { Inp[1].parentNode.querySelector(".PlaceHolder").setAttribute("style", "display:none"); IRC.ErrTip(Inp[1], "请输入大于6位的密码"); Str = false; }
                if (Inp[1].value != Inp[0].value) { IRC.ErrTip(Inp[1], "密码不一致"); Str = false; }
                break;
            case "Save":
                var Inp = document.querySelectorAll(".TbList>tfoot>.InputBar>td>input");
                if (Inp[0].value == "") { IRC.ErrTip(Inp[0], "员工工号不能为空"); Str = false; }
                if (Inp[1].value == "") { IRC.ErrTip(Inp[1], "真实姓名不能为空"); Str = false; }
                break;
            default: break;
        }
        return Str;
    },

    UIP: {},
    STR: {
        EVINIT: function (Num) {
            ///<summary>初始化页面</summary><param name="Num">首次加载信息的数量</param>
            var StaffList = [
                { SN:"1", No: "8001", Name: "姓名1", Sex: "男", RegDate: "2014/01/01", Dept: "客服1部", Position: "客服", Group: "第一组", AllotDate: "2014/01/02", Operator: "操作人", State: "正常" },
                { SN:"2",No: "8002", Name: "姓名2", Sex: "女", RegDate: "2014/02/01", Dept: "客服2部", Position: "客服", Group: "第二组", AllotDate: "2014/01/02", Operator: "操作人", State: "暂停" },
                { SN:"3",No: "8003", Name: "姓名3", Sex: "男", RegDate: "2014/03/01", Dept: "客服3部", Position: "客服", Group: "第三组", AllotDate: "2014/01/02", Operator: "操作人", State: "待用" }]
            M1001.UID.M100101(StaffList[0]);
            M1001.UID.M100101(StaffList[1]);
            M1001.UID.M100101(StaffList[2]);
            for (var i = 0; i < Num; i++) { M1001.UID.M100101(StaffList[2]); }
            M1001.UID.M100102();
        },
        EVM100101: function (StaffSN,State) {
            ///<summary>修改员工状态</summary>
            ///<param name="StaffSN">员工编号</param><param name="State">员工状态</param>
            console.log(StaffSN + "___" + State);
            setTimeout("M1001.UID.M100103(true," + State + ")", 200);
        },//修改员工状态
        EVM100102: function (StaffSN) {
            ///<summary>删除员工信息</summary>
            console.log(StaffSN);
            setTimeout("M1001.UID.M100104(true)", 200);
        },//删除员工
        EVM100103: function (StaffSN, PWD) {
            ///<summary>修改员工密码</summary>
            console.log(StaffSN + "__" + PWD);
            setTimeout("M1001.UID.M100105(true)", 200);
        },//修改密码
        EVM100104: function (StaffSN, Info) {
            ///<summary>修改员工信息</summary>
            ///<param name="StaffSN">员工编号</param><param name="Info">员工信息</param>
            console.log(StaffSN + "__" + Info.C3);
            setTimeout("M1001.UID.M100106(true," + JSON.stringify(Info) + ")", 200);
        },//修改员工信息
        EVM100105: function (Info) {
            ///<summary>新增员工信息</summary><param name="Info">员工信息</param>
            console.log(Info.C3);

            //.......

            var Staff = { C1: Info.C1, C2: Info.C2, C3: Info.C3, C4: "2014/07/24", C5: Info.C4, C6: Info.C5, C7: "", C8: "", C9: "", C10: "待用", C11: "编号" }
            setTimeout("M1001.UID.M100107(true," + JSON.stringify(Staff) + ")", 200);
        },//新增员工信息
    },
    UID: {
        M100101: function (Staff) {
            ///<summary>显示员工信息</summary>
            ///<param name="Staff">员工数据</param>
            var NewRow = MainZone.querySelector(".ListContainer>.TbList>.Template>tr").cloneNode(true);
            var NewRowTDs = NewRow.querySelectorAll("td");
            NewRowTDs[0].DspV = Staff.No; NewRowTDs[1].DspV = Staff.Name; NewRowTDs[2].DspV = Staff.Sex;
            NewRowTDs[3].DspV = Staff.RegDate; NewRowTDs[4].DspV = Staff.Dept; NewRowTDs[5].DspV = Staff.Position;
            NewRowTDs[6].DspV = Staff.Group; NewRowTDs[7].DspV = Staff.AllotDate; NewRowTDs[8].DspV = Staff.Operator;
            NewRowTDs[9].DspV = Staff.State; NewRow.setAttribute("data-SN", Staff.SN);
            if (Staff.Sex == "男") { NewRowTDs[2].setAttribute("data-org", 1); }
            if (Staff.State == "待用") { NewRowTDs[9].setAttribute("data-state", 0); }
            if (Staff.State == "暂停") { NewRowTDs[9].setAttribute("data-state", 1); }
            if (Staff.State == "正常") { NewRowTDs[9].setAttribute("data-state", 5); }
            var NewRow = MainZone.querySelectorAll(".ListContainer>.TbList>tbody")[0].appendChild(NewRow);
        },
        M100102: function () {
            ///<summary>首次加载完后调用的</summary>
            M1001.ShowButton(3);
            LoadingBoxCtl(0);
        },
        M100103: function (Res, State) {
            ///<summary>保存员工状态</summary>
            if (Res) {
                M1001.SaveSta(State);
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(3);
            }
        },//保存员工状态
        M100104: function (Res) {
            ///<summary>删除员工</summary>
            if (Res) {
                M1001.DelItem();
                M1001.ShowButton(3);
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },//删除员工
        M100105: function (Res) {
            ///<summary>修改员工密码</summary>
            if (Res) {
                M1001.ShowButton(1);
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },//修改密码
        M100106: function (Res,Staff) {
            ///<summary>修改员工信息</summary>
            if (Res) {
                M1001.ShowButton(1);
                M1001.FocusItem.children[0].innerText = Staff.C1;
                M1001.FocusItem.children[1].innerText = Staff.C2;
                M1001.FocusItem.children[2].setAttribute("data-org", Staff.C3);
                if (Staff.C3 == "1") { M1001.FocusItem.children[2].innerText = "男" } else { M1001.FocusItem.children[2].innerText = "女" };
                M1001.FocusItem.children[4].innerText = Staff.C4;
                M1001.FocusItem.children[5].innerText = Staff.C5;
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },//修改员工信息
        M100107: function (Res, Staff) {
            ///<summary>新增员工信息</summary>
            if (Res) {
                M1001.ShowButton(3);
                console.log(Staff);
                var Template = document.querySelector(".ListContainer>.TbList>.Template").firstChild.cloneNode(true);
                var NewTds = Template.querySelectorAll("td");
                NewTds[0].innerText = Staff.C1; NewTds[1].innerText = Staff.C2; NewTds[2].setAttribute("Staff-org", Staff.C3); NewTds[3].innerText = Staff.C4;
                if (Staff.C3 == "1") { NewTds[2].innerText = "男" } else { NewTds[2].innerText = "女" };
                NewTds[4].innerText = Staff.C5; NewTds[5].innerText = Staff.C6; NewTds[6].innerText = Staff.C7; NewTds[7].innerText = Staff.C8;
                NewTds[8].innerText = Staff.C9; NewTds[9].innerText = Staff.C10; Template.setAttribute("data-SN", Staff.C11);
                document.querySelector(".ListContainer>.TbList>tbody").appendChild(Template);
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        }//新增员工信息
    }
};