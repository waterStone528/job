// JavaScript source code
document.onkeyup = function () {
    //if (event.ctrlKey && event.keyCode == 13) { BtAct(SendBt, 1, 1); }
    if (event.keyCode == 13) { document.querySelector(".SendBt").onmouseup(); }
};

var M0100 = {
    LastCID: null,
    InitCusSvrTable:function(){
        var busCode = "K0102";
        var opeType = "initCusSvr";
        var cusSvrNum = document.querySelector(".MToolBar .UserID").innerText;
        var data = "busCode=" + busCode + "&opeType=" + opeType + "&cusSvrNum=" + cusSvrNum;

        var ajaxObj = new AJAXC();
        ajaxObj.data = data;
        ajaxObj.start();
    },
    GetTIme: function () { return (new Date()).getHours() + ":" + (new Date()).getMinutes() + ":" + (new Date()).getSeconds() },
    AddMessage: function (TempName, Mesg, MTime) {
        if (TempName == "MA") { MessageObj = MessageTemp.querySelector(".MessageA"); }
        if (TempName == "MB") { MessageObj = MessageTemp.querySelector(".MessageB"); }
        if (TempName == "MC") { MessageObj = MessageTemp.querySelector(".MessageC"); }
        MessageObj.querySelector(".Content").innerText = Mesg;
        MessageObj.querySelector(".Time").innerText = this.GetTIme();
        MessageArea.appendChild(MessageObj.cloneNode(true));
        this.ScrollToNew();
    },
    AddMessageNotSelect: function (TempName, Mesg, OPCustomer) {
        if (TempName == "MA") { MessageObj = MessageTemp.querySelector(".MessageA"); }
        if (TempName == "MB") { MessageObj = MessageTemp.querySelector(".MessageB"); }
        if (TempName == "MC") { MessageObj = MessageTemp.querySelector(".MessageC"); }
        MessageObj.querySelector(".Content").innerText = Mesg;
        MessageObj.querySelector(".Time").innerText = this.GetTIme();
        var innerHtml = OPCustomer.getAttribute("data-chathis") + MessageObj.outerHTML;
        OPCustomer.setAttribute("data-chathis", innerHtml);

        //设置未读消息数
        OPCustomer.querySelector(" .MsgCount").innerText = parseInt(OPCustomer.querySelector(" .MsgCount").innerText) + 1;
        OPCustomer.className = OPCustomer.className.replace(" MsgCount", "");
        OPCustomer.className = OPCustomer.className + " MsgCount";
    },
    ScrollToNew: function () {
        MessageArea.scrollTop = MessageArea.scrollHeight - MessageArea.clientHeight;
    },
    Button: function (BtObj, Act, BtID) {
        if (Act == 0) {
        } else {
            switch (BtID) {
                case 1:
                    if ((document.querySelector(".CustomList .Selected") == null) || (document.querySelector(".CustomList .Selected.CloseBt") != null)) {
                        return
                    }
                    
                    if (InputedText.value.length < 1) { return; }
                    if (InputedText.value == "\n") { InputedText.value = ""; return;}
                    if (InputedText.value.length > 180) {
                        this.AddMessage("MC", "已超过最大输入字符数，最大输入字符数为180个", (new Date()).getHours() + ":" + (new Date()).getMinutes() + ":" + (new Date()).getSeconds());
                        return;
                    }
                    this.AddMessage("MB", InputedText.value, (new Date()).getHours() + ":" + (new Date()).getMinutes() + ":" + (new Date()).getSeconds());
                    this.SendMsg(InputedText.value);
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
    AutoReply: function () {
        this.AddMessage("MB", "UI摸拟程序回复,您好", new Date().toLocaleTimeString());
    },
    Init: function () {
        
        this.InitCusSvrTable();
        this.ReceiveMsg();
        //this.Present();
    },
    KeepChatHis: function (OPCustomer, e) {
        OPCustomer = OPCustomer || document.createElement();
        if (e == 1) { OPCustomer.setAttribute("data-chathis", ""); }
        else { OPCustomer.setAttribute("data-chathis", MessageArea.innerHTML); }
    },
    ShowChatHis: function (OPCustomer) {
        OPCustomer = OPCustomer || document.createElement();
        MessageArea.innerHTML = OPCustomer.getAttribute("data-chathis");
    },
    CustomerButton: function (Sender, BtID, Act) {
        if (BtID == 1) {
            if (this.LastCID != null) { this.CustomerList(5, this.LastCID); }
            this.LastCID = Sender.getAttribute("data-CID");
            this.CustomerList(3, this.LastCID);
        }
        if (BtID == 2) {
            if (Sender.parentElement.getAttribute("data-CID") == this.LastCID) {
                this.CustomerList(4, Sender.parentElement.getAttribute("data-CID"));
            } else {
                this.CustomerButton(Sender, 1, 1);
            }
        }
    },
    CustomerList: function (Opt, CID, Ext) {
        //opt=1,新增用户；opt=2,设置用户未读消息数；opt=3,选中的用户;opt=4,删除该用户；opt=5,未选中一般状态；opt=6,可删除用户
        var Template = document.querySelector(".CustomList>.Template").firstChild.cloneNode(true);
        var CustomerList = document.querySelector(".CustomList");
        var OPCustomer = document.querySelector(".CustomList>.CID" + CID);
        switch (Opt) {
            case 1:
                Template.className = Template.className + " CID" + CID;
                Template.setAttribute("data-CID", CID);
                Template.querySelector(".Name").innerText = CID;
                CustomerList.appendChild(Template);
                //this.KeepChatHis(Template, 1);
                break;
            case 2:
                if (Ext > 0) {
                    OPCustomer.className = OPCustomer.className + " MsgCount";
                    OPCustomer.querySelector(".MsgCount").innerText = Ext;
                } else {
                    OPCustomer.className = OPCustomer.className.replace(" MsgCount", "");
                }
                break;
            case 3:
                if (OPCustomer == null) { MessageArea.innerHTML = ""; break; };
                OPCustomer.className = OPCustomer.className + " Selected";
                OPCustomer.className = OPCustomer.className.replace(" MsgCount", "");
                OPCustomer.querySelector(" .MsgCount").innerText = 0;
                this.ShowChatHis(OPCustomer);
                break;
            case 4:
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
    Present: function () {
        setTimeout("M0100.AddMessage('MC','排队连接客服.....')", 100);
        setTimeout("M0100.AddMessage('MB','您好，有什么可以为您服务吗？')", 200);
        setTimeout("M0100.AddMessage('MA','客户提问语言')", 300);
        setTimeout("M0100.AddMessage('MB','客服回复语言')", 400);
        setTimeout("M0100.AddMessage('MA','客户提问语言')", 500);
        setTimeout("M0100.AddMessage('MB','客服回复语言')", 600);
        setTimeout("M0100.AddMessage('MA','客户提问语言')", 700);
        setTimeout("M0100.AddMessage('MA','客户提问语言')", 800);
        setTimeout("M0100.AddMessage('MB','客服回复语言')", 900);
        setTimeout("M0100.AddMessage('MA','客户提问语言')", 1000);
        setTimeout("M0100.AddMessage('MB','客服回复语言')", 1100);
        this.CustomerList(1, "001");
        this.CustomerList(1, "002");
        this.CustomerList(2, "002", 2);
        this.CustomerList(1, "003");
        this.CustomerList(2, "003",6);
        this.CustomerList(1, "004");
        this.CustomerList(6, "004");
    },
    ///<summary>发送消息</summary>
    SendMsg: function(msg){
        var busCode = "K0102";
        var opeType = "sendMsg";
        var userNum = document.querySelector(".CustomList .Selected").getAttribute("data-CID");
        var cusSvrNum = document.querySelector(".MToolBar .UserID").innerText;
        var data = "busCode=" + busCode + "&opeType=" + opeType + "&userNum=" + userNum + "&cusSvrNum=" + cusSvrNum + "&msg=" + msg;

        var ajaxObj = new AJAXC();
        ajaxObj.data = data;
        ajaxObj.start();
    },
    ///<summary>接收消息</summary>
    ReceiveMsg: function () {
        var busCode = "K0102";
        var opeType = "receiveMsg";
        var cusSvrNum = document.querySelector(".MToolBar .UserID").innerText;
        var data = "busCode=" + busCode + "&opeType=" + opeType + "&cusSvrNum=" + cusSvrNum;

        var ajaxObj = new AJAXC();
        ajaxObj.data = data;
        ajaxObj.success = function (res) {
            var userNumMap = new Map();
            var jsonObjList = eval("(" + res + ")");

            if (jsonObjList.error != undefined) {
                alert(jsonObjList.error);

                return;
            }

            for (var i in jsonObjList) {
                userNumMap.set(jsonObjList[i].userNum, "0");
                if (jsonObjList[i].msg == null) {
                    continue;
                }
                var msgList = jsonObjList[i].msg.split("&&");
                var OPCustomer = document.querySelector(".CustomList>.CID" + jsonObjList[i].userNum);
                //新增客户
                if (OPCustomer == null) {
                    M0100.CustomerList(1, jsonObjList[i].userNum);
                    //新增客户为当前客服中的唯一客户时
                    if (document.querySelectorAll(".CustomList .Customer").length == 2) {
                        M0100.CustomerButton(document.querySelector(".CustomList>.CID" + jsonObjList[i].userNum), 1, 1);
                        for (var j in msgList) {
                            M0100.AddMessage("MA", msgList[j]);
                        }
                    }
                    else {
                        for (var j in msgList) {
                            M0100.AddMessageNotSelect("MA", msgList[j], document.querySelector(".CustomList>.CID" + jsonObjList[i].userNum));
                        }
                    }
                }
                    //已有客户
                else {
                    //已经是可删除状态，去除可删除状态标记
                    if (OPCustomer.className.indexOf("CloseBt") > 0) {
                        OPCustomer.className = OPCustomer.className.replace(" CloseBt", "");
                    }
                    //选中状态
                    if (OPCustomer.className.indexOf("Selected") > 0) {
                        for (var j in msgList) {
                            M0100.AddMessage("MA", msgList[j]);
                        }
                    }
                        //未选中状态
                    else {
                        for (var j in msgList) {
                            M0100.AddMessageNotSelect("MA", msgList[j], OPCustomer);
                        }
                    }
                }
            }

            //标记可删除用户
            var customerLsit = document.querySelectorAll(".CustomList>.Customer");
            for(var i=0;i<customerLsit.length;i++){
                if (userNumMap.has(customerLsit[i].getAttribute("data-CID")) == false) {
                    var className = customerLsit[i].className;
                    if (className.indexOf("CloseBt") < 0) {
                        M0100.CustomerList(6, customerLsit[i].getAttribute("data-CID"));
                    }
                }
            }

            setTimeout("M0100.ReceiveMsg()", 1000);
        };
        ajaxObj.error = function (xmlhttp) {
            alert("ajax error!\nreadyState:" + xmlhttp.readyState + "\nstatus:" + xmlhttp.status + "\nstatusText:" + xmlhttp.statustext);

            setTimeout("M0100.ReceiveMsg()", 1000);
        };
        ajaxObj.start();
    },
};