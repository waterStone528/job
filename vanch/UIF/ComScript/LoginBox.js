// JavaScript source code
var Login = {
    FocusItem: Object,
    LoginBoxBt:function (obj, Act, BtID) {
        if (Act == 1) {
            BtStatusClass(obj, 1);
            switch (BtID) {
                case 1:
                    this.LoginBoxCtl(0);
                    break;
                case 2:
                    this.FocusItem = obj;
                    if (this.LoginDataIntegrityCk()) {
                        obj.innerText = "登录中...";
                        obj.disabled = true;
                        LoadingBoxCtl(1);
                        this.STR.EVLogin(LoginName.value, LoginPwd.value);
                    }
                    break;
                case 3:
                    if (!IRC.LoginNameCk(LoginName, LoginName.value)) { return; }
                    LoadingBoxCtl(1);
                    this.STR.EVGetPassword(LoginName.value);
                    break;
                default:
                    break;
            }
        } else {
            BtStatusClass(obj, 2);
        }
    },
    LoginDataIntegrityCk:function () {
        var Pass = true;
        LoginPwdP.style.display = 'none';
        LoginPwd.style.display = '';
        if (!IRC.LoginNameCk(LoginName, LoginName.value)) { Pass = false; }
        if (!IRC.LoginPwdCk(LoginPwd, LoginPwd.value)) { Pass = false; }
        return Pass;
    },
    LoginBoxCtl:function (op) {
        if (op == 1) {
            LoginBoxi.style.display = "";
        } else { LoginBoxi.style.display = "none"; }
    },
    UIP: { },
    STR: {
        EVLogin: function (LoginName, Password) { setTimeout("Login.UID.LoginReturn(true)", 1000); },
        EVGetPassword: function (LoginName) { setTimeout("Login.UID.GetPassword(false,1)", 1000); }
    },
    UID: {
        LoginReturn: function (Res, ErrAct) {
            ///<summary>登录返回结果</summary><param name="Res">true Or false</param><param name="ErrAct">1:用户名错误;2:密码错误;3:用户名密码都不正确</param>
            if (Res) {
                Login.LoginBoxCtl(0);
                location.href = "P802.html";
                //TopBarButtons.querySelectorAll(".TopBarButtonsText")[2].querySelectorAll("div")[1].innerText = Login.UIP.UserInfo.Name;
                //TopBarButtons.querySelectorAll(".TopBarButtonsText")[2].setAttribute("onmouseup", "location.href='P802.html'");
            } else {
                if (ErrAct == "1") {
                    IRC.ErrTip(LoginName, "手机号码不正确");
                    LoginName.parentNode.querySelector(".PmpTxt").style.width = (LoginName.parentNode.querySelector(".PmpTxt").clientWidth + 2) + "px";
                    LoginName.parentNode.querySelector(".PmpTxt").style.height = (LoginName.parentNode.querySelector(".PmpTxt").clientHeight + 2) + "px";
                } else if (ErrAct == "2") {
                    IRC.ErrTip(LoginPwd, "密码不正确");
                    LoginPwd.parentNode.querySelector(".PmpTxt").style.width = (LoginPwd.parentNode.querySelector(".PmpTxt").clientWidth + 2) + "px";
                    LoginPwd.parentNode.querySelector(".PmpTxt").style.height = (LoginPwd.parentNode.querySelector(".PmpTxt").clientHeight + 2) + "px";
                } else {
                    IRC.ErrTip(LoginPwd, "密码不正确");
                    LoginPwd.parentNode.querySelector(".PmpTxt").style.width = (LoginPwd.parentNode.querySelector(".PmpTxt").clientWidth + 2) + "px";
                    LoginPwd.parentNode.querySelector(".PmpTxt").style.height = (LoginPwd.parentNode.querySelector(".PmpTxt").clientHeight + 2) + "px";
                    IRC.ErrTip(LoginName, "手机号码不正确");
                    LoginName.parentNode.querySelector(".PmpTxt").style.width = (LoginName.parentNode.querySelector(".PmpTxt").clientWidth + 2) + "px";
                    LoginName.parentNode.querySelector(".PmpTxt").style.height = (LoginName.parentNode.querySelector(".PmpTxt").clientHeight + 2) + "px";
                }
            }
            Login.FocusItem.innerText = "登录";
            Login.FocusItem.disabled = false;
            LoadingBoxCtl(2);
        },
        GetPassword: function (Act) {
            if (Act == "0") { LoginBoxLostPwdDis.innerText = "密码己发送到您的邮箱。"; }
            if (Act == "2") { LoginBoxLostPwdDis.innerText = "密码己发送到您的手机。"; }
            if (Act == "4") { LoginBoxLostPwdDis.innerText = "无此用户，请检查用户名"; }
            if (Act == "5") { LoginBoxLostPwdDis.innerText = "忘记密码次数达到上限，1小时内只能获取三次"; }
            if (Act == "1" || Act == "3") { LoginBoxLostPwdDis.innerText = "发送失败，请重试"; }
            LoadingBoxCtl(0);
        }

    },
}

