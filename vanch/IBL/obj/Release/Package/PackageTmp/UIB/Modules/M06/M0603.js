var M0603 = {
    FocusModule: null,
    FocusItem: Object,
    FirstLoadNum: 35,
    LoadParam: { From: 0, Num: 10 },
    Init: function () {
        LoadingBoxCtl(1);
        this.STR.EVINIT(this.FirstLoadNum);
    },
    Button: function (BtObj) {
        if (BtStatus(BtObj, "R") != "D") {
            if (this.InterCk()) {
                this.FocusItem=BtObj;
                BtStatus(BtObj, "D"); TransingStatus.SetStatus(1);
                var UserName = MRechary03.querySelectorAll("input")[0].value;
                var UserSN = MRechary03.querySelectorAll("input")[1].value;
                var Password = MRechary03.querySelectorAll("input")[2].value;
                var ChargeAmt = MRechary03.querySelectorAll("input")[3].value;
                var ChargeText = MRechary03.querySelector("select").value;
                var Remarks = MRechary03.querySelectorAll("input")[4].value;
                this.STR.EVM060301(UserSN, UserName, ChargeAmt, ChargeText, Password, Remarks);
            }
        }
    },

    //加载更多数据
    LoadMoreInfo: function () {
        console.log(M0603.LoadParam.Num);
        if (parseInt(Continue.innerText) == 1) {
            Continue.innerText = 0; 
            M0603.STR.EVM060302(M0603.LoadParam.From, M0603.LoadParam.Num);
        }
    },
    InterCk: function () {
        var Pass = true;
        var Inp = MRechary03.querySelectorAll("input");
        if (Inp[0].value.length < 2) { IRC.ErrTip(Inp[0], "输入有误"); Pass = false; }
        if (Inp[1].value.length != 11) { IRC.ErrTip(Inp[1], "输入有误"); Pass = false; }
        if (Inp[2].value.length == 0) { IRC.ErrTip(Inp[2], "输入有误"); Pass = false; }
        if (Inp[3].value.length == 0) { IRC.ErrTip(Inp[3], "输入有误"); Pass = false; }
        return Pass;
    },
    UIP: {},
    STR: {
        EVINIT: function (Num) {
            ///<summary>初始化页面</summary><param name="Num">首次加载信息的数量</param>
            // InfoList = [{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13000000000", UserName: "王大一", ChargeAmt: "500", ChargeText: "线下充值", Remarks: "号外号外！XXXX" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13111111111", UserName: "王大二", ChargeAmt: "500", ChargeText: "意外丢单", Remarks: "号外号外！XXXX" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13222222222", UserName: "王大三", ChargeAmt: "500", ChargeText: "退款", Remarks: "号外号外！XXXX" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13333333333", UserName: "王大四", ChargeAmt: "500", ChargeText: "其它原因", Remarks: "号外号外！XXXX" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13444444444", UserName: "王大五", ChargeAmt: "500", ChargeText: "退款", Remarks: "号外号外！XXXX" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13555555555", UserName: "王大六", ChargeAmt: "500", ChargeText: "活动奖励", Remarks: "号外号外！XXXX" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13777777777", UserName: "王大七", ChargeAmt: "500", ChargeText: "退款", Remarks: "号外号外！XXXX" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13888888888", UserName: "王大八", ChargeAmt: "500", ChargeText: "线下充值", Remarks: "号外号外！XXXX" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13999999999", UserName: "王大九", ChargeAmt: "500", ChargeText: "意外丢单", Remarks: "号外号外！XXXX" }, ]
            // setTimeout("for (var i in InfoList){ M0603.UID.M060301(InfoList[i]); };M0603.UID.M060302(false," + InfoList.length + ");", 1000);

            var busCode = "M0603INIT";
            var data = "busCode=" + busCode + "&pageSize=" + Num;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0603.STR.DataBdToFt(resObj[i]);

                    M0603.UID.M060301(dataFt);
                }

                var ifOverdue = resObj.length < Num ? true : false;
                M0603.UID.M060302(ifOverdue, resObj.length);
            }
            ajaxObj.start();
        },
        EVM060301: function (UserSN, UserName, ChargeAmt, ChargeText, Password, Remarks) {
            ///<summary>充值操作</summary><param name="UserSN">登陆帐号</param><param name="UserName">真实姓名</param><param name="ChargeAmt">充值金额</param><param name="ChargeText">充值原因</param><param name="Password">员工登陆密码</param>
            //var info = { InfoNo: "UA00512520", Date: "2014/06/09", UserSN: UserSN, UserName: UserName, ChargeAmt: ChargeAmt, ChargeText: ChargeText, Remarks: Remarks };
            //M0603.UID.M060303(true,info);

            var busCode = "M060301";
            var data = "busCode=" + busCode + "&phone=" + UserSN.trim() + "&name=" + UserName.trim() + "&changeAmount=" + ChargeAmt.trim() + "&changeReasonType=" + ChargeText.trim() + "&pwd=" + Password.trim() + "&note=" + Remarks;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                if (resObj.sucStatus == "0") {
                    var info = { InfoNo: resObj.SN, Date: BdDateStrFormate(resObj.date), UserSN: UserSN, UserName: UserName, ChargeAmt: ChargeAmt, ChargeText: ChargeText, Remarks: Remarks };
                    M0603.UID.M060303(true, info);
                }
                else {
                    M0603.UID.M060303(false);
                }
            }
            ajaxObj.start();
        },
        EVM060302: function (From, Num) {
            console.log(From);
            ///<summary>加载更多数据</summary><param name="From">从第N条开始加载</param><param name="Num">加载信息的数量</param>
            //InfoList = [{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13777777777", UserName: "王大一", ChargeAmt: "500", ChargeText: "线下充值", Remarks: "号外号外！XXXX" },
            // { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13888888888", UserName: "王大二", ChargeAmt: "500", ChargeText: "意外丢单", Remarks: "号外号外！XXXX" },
            // { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13999999999", UserName: "王大三", ChargeAmt: "500", ChargeText: "活动奖励", Remarks: "号外号外！XXXX" },
            // { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13000000000", UserName: "王大四", ChargeAmt: "500", ChargeText: "其它原因", Remarks: "号外号外！XXXX" },
            // { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13111111111", UserName: "王大五", ChargeAmt: "500", ChargeText: "活动奖励", Remarks: "号外号外！XXXX" },
            // { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13222222222", UserName: "王大六", ChargeAmt: "500", ChargeText: "活动奖励", Remarks: "号外号外！XXXX" },
            // { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13333333333", UserName: "王大七", ChargeAmt: "500", ChargeText: "线下充值", Remarks: "号外号外！XXXX" },
            // { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13444444444", UserName: "王大八", ChargeAmt: "500", ChargeText: "线下充值", Remarks: "号外号外！XXXX" },
            // { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "13555555555", UserName: "王大九", ChargeAmt: "500", ChargeText: "意外丢单", Remarks: "号外号外！XXXX" }, ]
            //setTimeout("for (var i in InfoList){ M0603.UID.M060301(InfoList[i]); };M0603.UID.M060302(true," + InfoList.length + ");", 1000);

            var busCode = "M060302";
            var data = "busCode=" + busCode + "&pageFrom=" + From + "&pageSize=" + Num;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                for (var i in resObj) {
                    var dataFt = M0603.STR.DataBdToFt(resObj[i]);

                    M0603.UID.M060301(dataFt);
                }

                var ifOverdue = resObj.length < Num ? true : false;
                M0603.UID.M060302(ifOverdue, resObj.length);
            }
            ajaxObj.start();
        },//加载更多数据

        //后-前，手工调账信息
        DataBdToFt: function (dataBd) {
            var dataFt = {};

            dataFt.InfoNo = dataBd.changeAccountSN;
            dataFt.Date = BdDateStrFormate(dataBd.changeDate);
            dataFt.UserSN = dataBd.userSN;
            dataFt.UserName = dataBd.name.trim();
            dataFt.ChargeAmt = dataBd.changeAmount;
            dataFt.ChargeText = dataBd.changeReasonType.trim();
            dataFt.Remarks = dataBd.note == null ? "" : dataBd.note.trim();

            return dataFt;
        }
    },
    UID: {
        M060301: function (Info) {
            var NewItemTemplate = template.children[0].cloneNode(true);
            var NTD = NewItemTemplate.querySelectorAll("tr>td:nth-child(2n)");
            NTD[0].DspV = Info.InfoNo; NTD[1].DspV = Info.Date; NTD[2].DspV = Info.UserSN; NTD[3].DspV = Info.UserName; NTD[4].DspV = Info.ChargeAmt; NTD[5].DspV = Info.ChargeText; NTD[6].DspV = Info.Remarks;
            document.querySelector(".SettingsContainer").appendChild(NewItemTemplate);
            LoadingBoxCtl(0);
            ReCalcItemSize();
        },
        M060302: function (IsLast, Num) {
            ///<summary>数据加载完毕后执行</summary><param name="IsLast">是否是最后一条数据;true:是;false:否;</param><param name="Num">加载信息的数量</param>
            M0603.LoadParam.From += Num;
            Continue.innerText = 1;
            if (IsLast) { LastItem = 1; } else { LastItem = 0; }
            LoadingBoxCtl(0);
        },//数据加载完毕后执行
        M060303: function (Res, Info) {
            ///<summary>充值结果</summary><param name="Res">充值结果;true:成功;false:失败;</param><param name="Info">加载信息</param>
            if (Res == false) { TransingStatus.SetStatus(2); BtStatus(M0603.FocusItem, "E"); return; }
            if (Res == true) {
                var NewItemTemplate = template.children[0].cloneNode(true);
                var NTD = NewItemTemplate.querySelectorAll("tr>td:nth-child(2n)");
                var Inp = MRechary03.querySelectorAll("input");
                for (var i in Inp) { Inp[i].value = "" }
                console.clear();
                console.log(Info.Date);
                NTD[0].DspV = Info.InfoNo; NTD[1].DspV = Info.Date; NTD[2].DspV = Info.UserSN; NTD[3].DspV = Info.UserName; NTD[4].DspV = Info.ChargeAmt; NTD[5].DspV = Info.ChargeText; NTD[6].DspV = Info.Remarks;
                document.querySelector(".SettingsContainer").insertBefore(NewItemTemplate, document.querySelector(".SettingsContainer>#Continue"));
                TransingStatus.SetStatus(3); BtStatus(M0603.FocusItem, "E");
                ReCalcItemSize();
            }
        },//充值结果
    },
}