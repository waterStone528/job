var M0604 = {
    FocusItem: null,
    L1DERParam: ",",
    FirstLoadNum: 35,
    LoadParam: { From: 0, Num: 10 },
    Init: function () {
        LoadingBoxCtl(1);
        this.STR.EVINIT(this.FirstLoadNum);
    }, //加载更多数据
    LoadMoreInfo: function () {
        if (parseInt(Continue.innerText) == 1) {
            Continue.innerText = 0;
            M0604.STR.EVM060401(M0604.LoadParam.From, M0604.LoadParam.Num, this.L1DERParam);
        }
    },
    HButton: function (Sender) {
        var BtID = parseInt(Sender.getAttribute("data-btid"));
        var arr = this.L1DERParam.split(",");
        switch (BtID) {
            case 1:
                for (var i in arr) { if (arr[i].indexOf("1#") > -1) { this.L1DERParam = this.L1DERParam.replace("," + arr[i], ""); } }
                if (Sender.getAttribute("data-ft") == "0") { Sender.setAttribute("data-ft", "1"); Sender.querySelector("input").focus(); }
                else { Sender.setAttribute("data-ft", "0"); this.L1DERParam += "1#" + Sender.querySelector("input").value + ","; this.L1Filter(); }
                break;
            case 2:
                for (var i in arr) { if (arr[i].indexOf("2#") > -1) { this.L1DERParam = this.L1DERParam.replace("," + arr[i], ""); } }
                if (Sender.getAttribute("data-ft") == "0") { Sender.setAttribute("data-ft", "1"); Sender.querySelector("input").focus(); }
                else { Sender.setAttribute("data-ft", "0"); this.L1DERParam += "2#" + Sender.querySelector("input").value + ","; this.L1Filter(); }
                break;
        }
    },
    L1Filter: function () { LoadingBoxCtl(1); this.STR.EVM060402(this.L1DERParam, this.FirstLoadNum); },

    UIP: {},
    STR: {
        EVINIT: function (Num) {
            ///<summary>初始化页面</summary><param name="Num">首次加载信息的数量</param>
            M0604.UID.M060403();
            //InfoList = [{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "线上充值", ChargeAmt: "500", CostAmt: "0" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "线下充值", ChargeAmt: "500", CostAmt: "0" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "VIP赠送", ChargeAmt: "500", CostAmt: "0" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "活动奖励", ChargeAmt: "500", CostAmt: "0" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "服务开通", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "债权预约", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "债权推荐", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "顾问预约", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "资产预约", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "债权账单", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "资产账单", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "短信服务", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "意外丢单", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "其它原因", ChargeAmt: "0", CostAmt: "500" }];
            //Count = { C1: 5555555, C2: 9999999 };
            //setTimeout("for (var i in InfoList){ M0604.UID.M060401(InfoList[i]); };", 1000);
            //M0604.UID.M060402(false, InfoList.length, Count);

            var busCode = "M0604INIT";
            var data = "busCode=" + busCode + "&pageSize=" + Num;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                var Count = { C1: resObj.revenueTotal, C2: resObj.expenditure };

                var data = resObj.data;
                for (var i in data) {
                    var dataFt = M0604.STR.DataBdToFt(data[i]);
                    M0604.UID.M060401(dataFt);
                }

                var ifOverdue = data.length < Num ? true : false;
                M0604.UID.M060402(ifOverdue, data.length, Count);
            }
            ajaxObj.start();
        },
        //加载更多数据
        EVM060401: function (Form, Num, Sort) {
            ///<summary>加载更多数据</summary><param name="From">从第N条开始加载</param><param name="Num">加载信息的数量</param>
            //console.log(Form + "__" + Num + "__" + Sort);
            //InfoList = [{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "线上充值", ChargeAmt: "500", CostAmt: "0" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "线下充值", ChargeAmt: "500", CostAmt: "0" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "VIP赠送", ChargeAmt: "500", CostAmt: "0" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "活动奖励", ChargeAmt: "500", CostAmt: "0" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "服务开通", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "债权预约", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "债权推荐", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "顾问预约", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "资产预约", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "债权账单", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "资产账单", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "短信服务", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "意外丢单", ChargeAmt: "0", CostAmt: "500" },
            //{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "其它原因", ChargeAmt: "0", CostAmt: "500" }]
            //Count = { C1: 5555555, C2: 9999999 };
            //setTimeout("for (var i in InfoList){ M0604.UID.M060401(InfoList[i]); };", 1000);
            //M0604.UID.M060402(true, InfoList.length, Count);

            var busCode = "M060401";
            var data = "busCode=" + busCode + "&sortStr=" + Sort + "&pageFrom=" + Form + "&pageSize=" + Num;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                var resObj = JSON.parse(res);
                var Count = { C1: resObj.revenueTotal, C2: resObj.expenditure };

                var data = resObj.data;
                for (var i in data) {
                    var dataFt = M0604.STR.DataBdToFt(data[i]);
                    M0604.UID.M060401(dataFt);
                }

                var ifOverdue = data.length < Num ? true : false;
                M0604.UID.M060402(ifOverdue, data.length, Count);
            }
            ajaxObj.start();
        },
        EVM060402: function (Sort, Num) {
            ///<summary>查找，排序</summary>
            //console.log(Sort);
            //M0604.UID.M060403();
            //InfoList = [{ InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "线上充值", ChargeAmt: "500", CostAmt: "0" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "线下充值", ChargeAmt: "500", CostAmt: "0" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "VIP赠送", ChargeAmt: "500", CostAmt: "0" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "活动奖励", ChargeAmt: "500", CostAmt: "0" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "服务开通", ChargeAmt: "0", CostAmt: "500" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "债权预约", ChargeAmt: "0", CostAmt: "500" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "债权推荐", ChargeAmt: "0", CostAmt: "500" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "顾问预约", ChargeAmt: "0", CostAmt: "500" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "资产预约", ChargeAmt: "0", CostAmt: "500" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "债权账单", ChargeAmt: "0", CostAmt: "500" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "资产账单", ChargeAmt: "0", CostAmt: "500" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "短信服务", ChargeAmt: "0", CostAmt: "500" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "意外丢单", ChargeAmt: "0", CostAmt: "500" },
            //    { InfoNo: "UA00512591", Date: "2014/07/02", UserSN: "U1900880", ItemName: "其它原因", ChargeAmt: "0", CostAmt: "500" }]
            //setTimeout("for (var i in InfoList){ M0604.UID.M060401(InfoList[i]); };", 1000);
            //Count = { C1: 5555555, C2: 9999999 };
            //M0604.UID.M060402(false, InfoList.length, Count);

            var busCode = "M060402";
            var data = "busCode=" + busCode + "&sortStr=" + Sort + "&pageSize=" + Num;
            var ajaxObj = new AJAXC();
            ajaxObj.data = data;
            ajaxObj.success = function (res) {
                M0604.UID.M060403();

                var resObj = JSON.parse(res);
                var Count = { C1: resObj.revenueTotal, C2: resObj.expenditure };

                var data = resObj.data;
                for (var i in data) {
                    var dataFt = M0604.STR.DataBdToFt(data[i]);
                    M0604.UID.M060401(dataFt);
                }

                var ifOverdue = data.length < Num ? true : false;
                M0604.UID.M060402(ifOverdue, data.length, Count);
            }
            ajaxObj.start();
        },

        //后-前 流水明细
        DataBdToFt: function (dataBd) {
            var dataFt = {};

            dataFt.InfoNo = dataBd.revenueExpenditureSN;
            dataFt.Date = BdDateStrFormate(dataBd.generateDate);
            dataFt.UserSN = dataBd.userSN;
            dataFt.ItemName = dataBd.type == null ? "" : dataBd.type.trim();
            dataFt.ChargeAmt = dataBd.revenue == null ? 0 : dataBd.revenue;
            dataFt.CostAmt = dataBd.expenditure == null ? 0 : dataBd.expenditure;

            return dataFt;
        }
    },
    UID: {
        M060401: function (Info) {
            var NewRow = document.querySelector(".TbList .Template").cloneNode(true); NewRow.removeAttribute("class");
            var NewRowTDs = NewRow.querySelectorAll("td");
            NewRowTDs[0].innerText = Info.InfoNo; NewRowTDs[1].innerText = Info.Date; NewRowTDs[2].innerText = Info.UserSN; NewRowTDs[3].innerText = Info.ItemName;
            NewRowTDs[4].innerText = Info.ChargeAmt; NewRowTDs[5].innerText = Info.CostAmt; document.querySelector(".TbList>tbody").appendChild(NewRow);
        },
        M060402: function (IsLast, Num, V) {
            ///<summary>数据加载完毕后执行</summary><param name="IsLast">是否是最后一条数据;true:是;false:否;</param><param name="Num">加载信息的数量</param>
            M0604.LoadParam.From += Num;
            Continue.innerText = 1;
            if (IsLast) { LastItem = 1; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 0); } else { LastItem = 0; MainZone.querySelector("tfoot>.RemData").setAttribute("data-dsp", 1); }
            var Tds = document.querySelectorAll(".TbList>tfoot>.Summary>td");
            Tds[4].DspV = V.C1; Tds[5].DspV = V.C2; 
            LoadingBoxCtl(0);
        },//数据加载完毕后执行
        M060403: function (succ) {
            document.querySelectorAll(".TbList>tbody")[0].innerHTML = "";
        },
    },
}