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
                        this.STR.EVLogin(LoginName.value, LoginPwd.value);
                    }
                    //var oURLInp = document.querySelector('.URLInp')
                    //oURLInp.setAttribute('style', 'background-color: #000;border:1px solid #000;')
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
            Comm.UID.logout(2);
            LoginBoxi.style.display = "";
        } else { LoginBoxi.style.display = "none";  }
    },
    UIP: { },
    STR: {
        //0：正确。1：用户名不正确。2：密码不正确。
        //登录
        EVLogin: function (LoginName, Password) {
            var busCode = "P80403";
            var data = "busCode=" + busCode + "&phoneNum=" + LoginName + "&pwd=" + Password;

            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.timeout = 0;
            ajaxObj.success = function (res) {
                if (res == 0) {
                    Login.UID.LoginReturn(true);
                }
                else {
                    Login.UID.LoginReturn(false, res);
                }
            };
            ajaxObj.start();
        },
        //0：通过邮箱发送密码成功。1：通过邮箱发送密码失败。 2：通过短信发送密码成功。3：通过短信发送密码失败。 4：手机号码不存在。 5：获取密码次数达到上限。
        EVGetPassword: function (LoginName) {
            var busCode = "P80404";
            var data = "busCode=" + busCode + "&phoneNum=" + LoginName;

            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.timeout = 0;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                if (resObj.status == 0) {
                    Login.UID.GetPassword(resObj.status, resObj.email);
                }
                else {
                    Login.UID.GetPassword(resObj.status);
                }

            }
            ajaxObj.error = function () {
                alter("ajax error");
                Login.UID.GetPassword(1);
            }
            ajaxObj.start();
        }
    },
    UID: {
        
        LoginReturn: function (Res, ErrAct) {
            ///<summary>登录返回结果</summary><param name="Res">true Or false</param><param name="ErrAct">1:用户名错误;2:密码错误;3:用户名密码都不正确</param>
            if (Res) {
                Login.LoginBoxCtl(0);
                Location("P802");
            } else {
                if (ErrAct == "1") {
                    IRC.ErrText(LoginName, "手机号码不正确");
                } else if (ErrAct == "2") {
                    IRC.ErrText(LoginPwd, "密码不正确");
                } else {
                    IRC.ErrText(LoginPwd, "密码不正确");
                    IRC.ErrText(LoginName, "手机号码不正确");
                }
            }
            Login.FocusItem.innerText = "登录";
            Login.FocusItem.disabled = false;
        },
        GetPassword: function (Act,Key) {
            if (Act == "0") { LoginBoxLostPwdDis.innerText = "新密码己发送到您的邮箱" + Key; }
            if (Act == "2") { LoginBoxLostPwdDis.innerText = "新密码己发送到您的手机"; }
            if (Act == "4") { LoginBoxLostPwdDis.innerText = "无此用户，请检查用户名"; }
            if (Act == "5") { LoginBoxLostPwdDis.innerText = "忘记密码次数达到上限，1小时内只能获取三次"; }
            if (Act == "1" || Act == "3") { LoginBoxLostPwdDis.innerText = "发送失败，请重试"; }
            LoadingBoxCtl(0);
        }

    },
    
}

