// JavaScript source code

function UserRegBoxCtl(op) {
    if (op == 1) {
        UserRegBoxi.style.display = "";
    } else { UserRegBoxi.style.display = "none"; }
}
var Reg = {
    FocusItem: Object,
    RebateIntvID: 0,
    UserInfo: {},
    InterCK: function () {
        var Pass = true;
        var RI = UserRegBoxElms.querySelectorAll(".ItemText>input");
        if (!IRC.LoginNameCk(RI[0], RI[0].value)) { Pass = false; }
        if (!IRC.CheckCode(RI[7], RI[7].value)) { Pass = false; }

        if (URegPwd1.value.length < 6) {
            if (URegPwd1.style.display != "none") {
                IRC.ErrText(URegPwd1, "密码长度要大于6位");
                Pass = false;
            } else {
                IRC.ErrText(URegPwd1P, "密码长度要大于6位");
                Pass = false;
            }
        }
        if (URegPwd2.value.length < 6) {
            if (URegPwd2.style.display != "none") {
                IRC.ErrText(URegPwd2, "密码长度要大于6位");
                Pass = false;
            } else {
                IRC.ErrText(URegPwd2p, "密码长度要大于6位");
                Pass = false;
            }
        } else if (URegPwd2.value != URegPwd1.value) {
            if (URegPwd2.style.display != "none") {
                IRC.ErrText(URegPwd2, "密码不一致");
                Pass = false;
            }
        }

        if (RI[5].value.length < 2 || RI[5].value == "真实姓名") { IRC.ErrText(RI[5], "请输入真实姓名"); Pass = false;}
        if (!IRC.RegCard(RI[6], RI[6].value)) { Pass = false;}

        if (UserRegBoxElms.querySelector(".UpDownButton").getAttribute('data-sw') != 1) { UserRegBoxElms.querySelector(".memo").innerText = "必须同意用户注册协议后才能注册"; Pass = false; }
        return Pass;
    },
    CountDown: function (Sec) {
        var Patrn = /^[1][3]\d{9}$|^[1][5]\d{9}$|^[1][7]\d{9}$|^[1][8]\d{9}$/;
        var RI = UserRegBoxElms.querySelectorAll(".ItemText>input");
        if (!Patrn.exec(RI[0].value)) { IRC.ErrTip(RI[0], "请输入正确的手机号码"); RI[0].parentNode.querySelector(".PmpTxt").style.width = (RI[0].parentNode.querySelector(".PmpTxt").clientWidth + 2) + "px"; RI[0].parentNode.querySelector(".PmpTxt").style.height = (RI[0].parentNode.querySelector(".PmpTxt").clientHeight + 2) + "px"; return;}

        UserRegSendSMS.setAttribute("onmouseup", "");
        window.clearInterval(this.RebateIntvID);
        UserRegSendSMS.innerText = "发送成功，" + Sec + "秒后重新获取"; Sec--;
        this.RebateIntvID = window.setInterval("Reg.CountDown(" + Sec + ");", 1000);
        if (Sec < 0) {
            UserRegSendSMS.innerText = " 发送短信验证码";
            UserRegSendSMS.setAttribute("onmouseup", "Reg.BtAction(this, 1);");
            window.clearInterval(this.RebateIntvID);
        }
    },
    BtAction: function (BtObj, Act) {
        switch (Act) {
            case 1:
                this.CountDown(60);
                this.STR.EVSendSMS(UserRegBoxElms.querySelectorAll("input")[0].value);
                break;
            case 2:
                this.FocusItem = BtObj;
                if (this.InterCK()) {
                    if (BtObj.disabled == true) { BtObj.style.cursor = ""; return; }
                    var RI = UserRegBoxElms.querySelectorAll(".ItemText>input");
                    BtObj.innerText = "注册中.....";
                    BtObj.disabled = true;
                    TransingStatus.SetStatus(1);
                    this.UserInfo.Mobile = RI[0].value;
                    this.UserInfo.Possword = RI[1].value;
                    this.UserInfo.Name = RI[5].value;
                    this.UserInfo.CardID = RI[6].value;
                    this.STR.EVReg(this.UserInfo, RI[7].value);
                }
                break;
            case 3:
                UserRegBoxCtl(0);
                break
            case 4:
                UserRegBox.querySelector(".UserRegAgreem").style.right = "0px";
                setTimeout("UserRegBox.setAttribute('onmouseup', 'Reg.Close();');", 200);
                BtStatusClass(BtObj, 1);
                break;
            default:
                BtStatusClass(BtObj, 2);
                break;
        }
    },
    Close: function () {
        UserRegBox.querySelector(".UserRegAgreem").style.right = "-500px";
        UserRegBox.removeAttribute("onmouseup");
    },
    UserAgreen: function (BtObj) {
        if (BtObj.getAttribute("data-sw") != "1") { BtObj.setAttribute("data-sw", "1"); } else { BtObj.setAttribute("data-sw", 0) }
        BtObj.className = BtObj.className;
    },
    UIP: {},
    STR: {
        EVSendSMS: function (TelNo) {/*后台产生验证码*/
            var busCode = "P80402";
            var data = "busCode=" + busCode + "&phoneNum=" + TelNo;

            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.timeout = 0;
            ajaxObj.start();
        },
        EVReg: function (UserInfo, VCode) {
            var busCode = "P80401";
            var data = "busCode=" + busCode + "&phoneNum=" + UserInfo.Mobile + "&pwd=" + UserInfo.Possword + "&name=" + UserInfo.Name + "&idCard=" + UserInfo.CardID + "&identifyingCode=" + VCode;

            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.timeout = 0;
            ajaxObj.success = function (res) {
                if (res == 0) {
                    Reg.UID.RegReturn(true);
                }
                else {
                    Reg.UID.RegReturn(false);
                }
            }
            ajaxObj.error = function () {
                Reg.UID.RegReturn(false);
            }
            ajaxObj.start();
        }
    },
    UID: {
        RegReturn: function (Res, ErrTxt) {
            if (Res) {
                TransingStatus.SetStatus(3);
                setTimeout("Location('index');", 2000);
            } else {
                TransingStatus.SetStatus(2, ErrTxt);
            }
            Reg.FocusItem.innerText = "注册";
            Reg.FocusItem.disabled = false;
        }
    },
}