// JavaScript source code
var M1101 = {
    LastObj: null,
    Init: function () {
        this.STR.EVINIT();
    },
    Button: function (BtObj) {
        if (BtStatus(BtObj, "R") == "D") { return; }
        if (this.LastObj == null) {
            this.LastObj = BtObj;
            if (this.CheckPwd()) {
                BtStatus(BtObj, "D");
                TransingStatus.SetStatus(1);
                this.STR.EVM110101(OldPwd.value, NewPwd.value, Pwd.value);
            } else { this.LastObj = null; }
        }
        else { BtStatus(BtObj, "E"); return; }
    },
    CheckPwd: function () {
        var Pass = true;
        if (!IRC.CheckOldPwd(document.querySelector("#OldPwd"), document.querySelector("#OldPwd").value)) { Pass = false; };
        if (!IRC.CheckNewPwd(document.querySelector("#NewPwd"), document.querySelector("#NewPwd").value)) { Pass = false; };
        if (!IRC.ComparePwd(document.querySelector("#NewPwd"), document.querySelector("#NewPwd").value, document.querySelector("#Pwd"), document.querySelector("#Pwd").value)) { Pass = false; };
        return Pass;
    },

    UIP: {},
    STR: {
        EVINIT: function () {
            ///<summary>页面初化</summary>
            //var StaffSN = "8601";
            //M1101.UID.M110101(StaffSN);

            var busCode = "opeAccount";
            var opeType = "getWorkNum";
            var data = "busCode=" + busCode + "&opeType=" + opeType;

            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                M1101.UID.M110101(res);
            }
            ajaxObj.start();
        },
        EVM110101: function (OldPwd, NewPwd, Pwd) {
            ///<summary>密码修改</summary>
            ///<param name="OldPwd">原密码</param><param name="NewPwd">新密码</param><param name="Pwd">确认密码</param>
            //console.log(OldPwd)
            //setTimeout("M1101.UID.M110102(true);", 1000);

            var busCode = "opeAccount";
            var opeType = "modifyPwd";
            var oldPwd = document.querySelector("#OldPwd").value;
            var newPwd = document.querySelector("#NewPwd").value;
            var params = "busCode=" + busCode + "&opeType=" + opeType + "&oldPwd=" + oldPwd + "&newPwd=" + newPwd;

            var ajaxObj = new AJAXC();
            ajaxObj.data = params;
            ajaxObj.success = function (res) {
                if (res == "True") {
                    M1101.UID.M110102(true);
                }
                else {
                    M1101.UID.M110102(false);
                }
            }
            ajaxObj.error = function () {
                M1101.UID.M110102(false);
            }
            ajaxObj.start();
        }
    },
    UID: {
        M110101: function (SN) {
            M110101.querySelectorAll("td")[1].DspV = SN;
        },
        M110102: function (Res) {
            if (Res) {
                OldPwd.value = "";
                NewPwd.value = "";
                Pwd.value = "";
                BtStatus(M1101.LastObj, "E");
                TransingStatus.SetStatus(3);
            } else {
                TransingStatus.SetStatus(2);
            }
        },
    }
};