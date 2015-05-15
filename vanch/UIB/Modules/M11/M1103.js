// JavaScript source code
var M1103 = {
    FocusModule: null,
    Init: function () {

    },
    MonitorKeys: function (Sender) {
        if (event.keyCode == 13) {
            M1103.LoginBoxBt(Sender.querySelector(".URLBt"), 1, 1);
        }
    },
    LoginBoxBt: function (obj, BtID) {
        switch (BtID) {
            case 1:
                if (this.LoginDataIntegrityCk()) {
                    this.LoginBtCk(obj);
                    document.querySelector("#ForgetPWDBt").style.display = 'none';
                }
                break;
            case 3:
                LoginBoxLostPwdDis.innerText = "密码己发送到你的邮箱：be**@gmail.com";
                break;
            default:
                break;
        }
    },
    LoginDataIntegrityCk: function () {
        var Pass = true;
        document.querySelector("#LoginPwdP").style.display = 'none';
        document.querySelector("#LoginPwd").style.display = '';
        if (!IRC.LoginNameCk(document.querySelector("#LoginName"), document.querySelector("#LoginName").value)) { Pass = false; }
        if (!IRC.LoginPwdCk(document.querySelector("#LoginPwd"), document.querySelector("#LoginPwd").value)) { Pass = false; }
        return Pass;
    },
    LoginBtCk: function (obj) {
        obj.innerText = "登陆中...";
        obj.disabled = true;
        MToolBar.STR.EVM0003(LoginName.value, LoginPwd.value);
    },
    Textfocus: function (obj) {
        if (obj.value == obj.placeholder) {
            obj.value = '';
            obj.className += ' URLInpI';
            document.querySelector("#ForgetPWDBt").style.display = '';
        }
    },
    Textblur: function (obj) {
        if (obj.value.length < 1) {
            obj.value = obj.placeholder;
            obj.className = obj.className.replace(' URLInpI', '');
            document.querySelector("#ForgetPWDBt").style.display = 'none';
        }
    },
    Pwdfocus: function (obj) {
        if (obj.value == obj.placeholder) {
            obj.style.display = 'none';
            document.querySelector("#LoginPwd").style.display = '';
            document.querySelector("#LoginPwd").focus();
        }
    }
};