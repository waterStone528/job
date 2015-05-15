// JavaScript source code
var M0100 = {
    LastCID: null,
    NewExt: [],
    T1: 0, T2: 0, T3: 0,
    GetTIme: function () { return (new Date()).getHours() + ":" + (new Date()).getMinutes() + ":" + (new Date()).getSeconds() },
    Init: function () {
        this.STR.EVINIT();
    },
    AddMessage: function (TempName, Mesg) {
        if (TempName == "MA") { MessageObj = MessageTemp.querySelector(".MessageA"); }
        if (TempName == "MB") { MessageObj = MessageTemp.querySelector(".MessageB"); }
        if (TempName == "MC") { MessageObj = MessageTemp.querySelector(".MessageC"); }
        MessageObj.querySelector(".Content").innerText = Mesg;
        MessageObj.querySelector(".Time").innerText = this.GetTIme();
        MessageArea.appendChild(MessageObj.cloneNode(true));
        this.ScrollToNew();
    },

    ScrollToNew: function () {
        MessageArea.scrollTop = MessageArea.scrollHeight - MessageArea.clientHeight;
    },
    Button: function (BtObj, Act, BtID) {
        if (Act == 0) {
        } else {
            switch (BtID) {
                case 1:
                    InputedText.value = InputedText.value.replace(/[\r\n]/g, "");
                    if ((document.querySelector(".CustomList .Selected") == null) || (document.querySelector(".CustomList .Selected.CloseBt") != null)) { return; }
                    if (InputedText.value.length < 1) { return; }
                    if (InputedText.value == "\n") { InputedText.value = ""; return; }
                    if (InputedText.value.length > 180) { this.AddMessage("MC", "已超过最大输入字符数，最大输入字符数为180个", this.GetTIme()); return; }
                    this.STR.EVM010002(this.LastCID, InputedText.value);
                    InputedText.value = "";
                    break;
                case 2:
                    MessageArea.innerHTML = "";
                    break;
                case 3:
                    break
                default:
                    break;
            }
        }
    },
    KeepChatHis: function (OPCustomer, e) {
        OPCustomer = OPCustomer || document.createElement();
        if (e == 1) { OPCustomer.setAttribute("data-chathis", ""); } else { OPCustomer.setAttribute("data-chathis", MessageArea.innerHTML); }
    },//失去选中客户将聊天记录隐藏
    ShowChatHis: function (OPCustomer) {
        OPCustomer = OPCustomer || document.createElement();
        MessageArea.innerHTML = OPCustomer.getAttribute("data-chathis");
    },//选中客户将聊天记录显示出来
    BackChatHis: function (Ext) {
        var MessageObj = MessageTemp.querySelector(".MessageB").cloneNode(true);
        var DIV = document.createElement("div");
        MessageObj.querySelector(".Content").innerText = Ext;
        MessageObj.querySelector(".Time").innerText = this.GetTIme();
        DIV.appendChild(MessageObj);
        return DIV.innerHTML;
    },//没有选中客户时添加新的聊天记录
    CustomerButton: function (Sender, BtID, Act) {
        if (BtID == 1) {
            if (this.LastCID != null) { this.CustomerList(5, this.LastCID); }
            this.LastCID = Sender.getAttribute("data-CID");
            this.CustomerList(3, this.LastCID);
            this.CustomerList(2, this.LastCID, 0);
            this.NewExt[this.LastCID] = 0;
        }
        if (BtID == 2) {
            if (Sender.parentElement.getAttribute("data-CID") == this.LastCID) {
                this.STR.EVM010003(this.LastCID)
            } else {
                this.CustomerButton(Sender.parentElement, 1, 1);
            }
        }
    },
    CustomerList: function (Opt, CID, Ext, CName) {
        //opt=1,新增用户；opt=2,设置用户未读消息数；opt=3,选中的用户;opt=4,删除该用户；opt=5,未选中一般状态；opt=6,可删除用户
        var Template = document.querySelector(".CustomList>.Template").firstChild.cloneNode(true);
        var CustomerList = document.querySelector(".CustomList");
        var OPCustomer = document.querySelector(".CustomList>.CID" + CID);
        switch (Opt) {
            case 1:
                Template.className = Template.className + " CID" + CID;
                Template.setAttribute("data-CID", CID)
                Template.querySelector(".Name").innerText = CName;
                CustomerList.appendChild(Template);
                this.KeepChatHis(Template, 1);
                break;
            case 2:
                if (Ext > 0) {
                    OPCustomer.className = OPCustomer.className.replace(" MsgCount", "");
                    OPCustomer.className = OPCustomer.className + " MsgCount";
                    OPCustomer.querySelector(".MsgCount").innerText = Ext;
                } else {
                    OPCustomer.className = OPCustomer.className.replace(" MsgCount", "");
                }
                break;
            case 3:
                if (OPCustomer == null) { break; };
                OPCustomer.className = OPCustomer.className + " Selected";
                this.ShowChatHis(OPCustomer);
                break;
            case 4:
                OPCustomer.removeAttribute("onmouseup");
                CustomerList.removeChild(OPCustomer);
                break;
            case 5:
                if (OPCustomer == null) { break; };
                OPCustomer.className = OPCustomer.className.replace(" Selected", "");
                this.KeepChatHis(OPCustomer, 0);
                break;
            case 6:
                OPCustomer.className = OPCustomer.className + " CloseBt";
                break;
            default:
                break;
        }
    },
    PulseEvent: function () {
        setInterval("M0100.STR.EVM010001()", 2000);
    },
    UIP: {},
    STR: {
        EVINIT: function () {
            ///<summary>页面初始化</summary>
            M0100.UID.M010001();
        },
        EVM010001: function () {
            ///<summary>心跳事件</summary>
            var rdm = ~~(Math.random() * 20 + 1);
            M0100.T1 = M0100.T1 + 1;
            if (M0100.T1 < 6) { if (M0100.T2 % 4 == 0) { M0100.UID.M010002("A00" + M0100.T1,"客户姓名"); } }
            if (rdm > 10) { var CID = (rdm % 4) + 1; if (CID > M0100.T1) { return; }; M0100.UID.M010003("A00" + CID, "客户提问语言" + rdm); }
            if (rdm == 17 && M0100.T1 > 5 && M0100.T3 == 0) { M0100.T3 = 1; M0100.UID.M010007("A005"); window.clearInterval(M0100.t4); }
        },
        EVM010002: function (UserSN, Ext) {
            ///<summary>发送信息</summary>
            M0100.UID.M010004(Ext);
        },
        EVM010003: function (UserSN) {
            ///<summary>删除用户</summary>
            M0100.UID.M010005(UserSN);
        }
    },
    UID: {
        M010001: function () {
            document.onkeyup = function () { if (event.keyCode == 13) { M0100.Button(null, 1, 1); }; }
            M0100.AddMessage("MC", "排队连接客服.....", M0100.GetTIme());
            M0100.PulseEvent();
        },
        M010002: function (UserSN, UserName) {
            ///<summary>添加客户</summary>
            M0100.CustomerList(1, UserSN, 0, UserName);
        },//添加客户
        M010003: function (UserSN, Ext) {
            ///<summary>设置用户未读消息数</summary>
            var Template = document.querySelector(".CustomList>.CID" + UserSN);
            if (Template.className.indexOf("Selected") > -1) { M0100.AddMessage("MB", Ext); return; };
            Template.setAttribute("data-chathis", Template.getAttribute("data-chathis") + M0100.BackChatHis(Ext));
            if (M0100.NewExt[UserSN] == undefined) { M0100.NewExt[UserSN] = 0; };
            M0100.NewExt[UserSN] = parseInt(M0100.NewExt[UserSN]) + 1;
            M0100.CustomerList(2, UserSN, M0100.NewExt[UserSN]);
        },//设置用户未读消息数
        M010004: function (Ext) {
            ///<summary>发送信息</summary>
            M0100.AddMessage("MA", Ext, M0100.GetTIme());
        },//发送信息
        M010005: function (UserSN) {
            ///<summary>删除用户</summary>
            M0100.CustomerList(4, UserSN);
        },//删除用户
        M010007: function (UserSN) {
            ///<summary>可删除用户</summary>
            M0100.CustomerList(6, UserSN);
        },//可删除用户

    }
};