using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

using IFL;
using IFL.Comm;

namespace UIF
{
    /// <summary>
    /// Summary description for Control
    /// </summary>
    public class Control : IHttpHandler,IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string busCode = context.Request.Params["busCode"];
            string res = string.Empty;

            #region 首页
            //记录userAgent
            if (busCode == "recordUserAgent")
            {
                string userAgent = context.Request.Params["userAgent"];

                IFL.Index ifl = new Index();
                ifl.RecordUserAgent(userAgent);
            }

            //初始化
            if (busCode == "INIT")
            {
                IFL.Index ifl = new Index();
                res = ifl.Init();
            }

            //点击模块
            if (busCode == "LoadSvr")
            {
                string whichSvr = context.Request.Params["whichSvr"];

                IFL.Index ifl = new Index();
                res = ifl.LoadSvr(whichSvr);
            }

            //获取用户编号
            if(busCode == "GetUserSN")
            {
                IFL.Index ifl = new Index();
                res = ifl.GetUserSN();
            }
            #endregion

            #region P1 债权投资
            #region P101 寻找债权
            //页面初始化
            if(busCode == "P10101")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P1. CP101 ifl = new IFL.P1.CP101();
                res = ifl.FP10101(pageSize);
            }

            //滚动获取债权信息
            if(busCode == "P10102")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);
                string keyword = context.Request.Params["keyword"];
                string provinceSN = context.Request.Params["provinceSN"];
                string citySN = context.Request.Params["citySN"];
                string capitalPurposeSN = context.Request.Params["capitalPurposeSN"];
                string paymentTypeSN = context.Request.Params["paymentTypeSN"];
                string guaranteeTypeSN = context.Request.Params["guaranteeTypeSN"];
                string financingMain = context.Request.Params["financingMain"];
                string sortStr = context.Request.Params["sort"];

                IFL.P1.CP101 ifl = new IFL.P1.CP101();
                res = ifl.FP10102(maxDatetime, pageFrom, pageSize, keyword, provinceSN, citySN, capitalPurposeSN, paymentTypeSN, guaranteeTypeSN, financingMain, sortStr);
            }

            //获取融资方信息
            if (busCode == "P10105")
            {
                string financierUserSN = context.Request.Params["financierUserSN"];

                IFL.P1.CP101 ifl = new IFL.P1.CP101();
                res = ifl.FP10105(financierUserSN);
            }

            //搜索
            if(busCode == "P10106")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);
                string keyword = context.Request.Params["keyword"];
                string provinceSN = context.Request.Params["provinceSN"];
                string citySN = context.Request.Params["citySN"];
                string capitalPurposeSN = context.Request.Params["capitalPurposeSN"];
                string paymentTypeSN = context.Request.Params["paymentTypeSN"];
                string guaranteeTypeSN = context.Request.Params["guaranteeTypeSN"];
                string financingMain = context.Request.Params["financingMain"];
                string sortStr = context.Request.Params["sort"];

                IFL.P1.CP101 ifl = new IFL.P1.CP101();
                res = ifl.FP10106(pageSize,keyword, provinceSN, citySN, capitalPurposeSN, paymentTypeSN, guaranteeTypeSN, financingMain, sortStr);
            }

            //投资方债权预约
            if(busCode == "P10107")
            {
                string pwd = context.Request.Params["pwd"];
                string crSN = context.Request.Params["crSN"];
                string financierUserSN = context.Request.Params["financierUserSN"];

                IFL.P1.CP101 ifl = new IFL.P1.CP101();
                res = ifl.FP10107(pwd,crSN, financierUserSN);
            }

            //获取账户余额及债权预约费用
            if(busCode == "P10109")
            {
                IFL.P1.CP101 ifl = new IFL.P1.CP101();
                res = ifl.FP10109();
            }
            #endregion

                #region P102 预约中债权
            //初始化加载
            if(busCode == "P10203")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                res = ifl.FP10203(pageSize);
            }

            //滚动加载
            if (busCode == "P10214")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                res = ifl.FP10214(maxDatetime, pageFrom, pageSize);
            }

            //取消债权预约
            if(busCode == "P10207")
            {
                string crSN = context.Request.Params["crSN"];
                string cancelReasonTypeSN = context.Request.Params["cancelReasonTypeSN"];

                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                ifl.FP10207(crSN, cancelReasonTypeSN);
            }

            //拒绝债权预约
            if (busCode == "P10216")
            {
                string crSN = context.Request.Params["crSN"];
                string refuseReasonTypeSN = context.Request.Params["refuseReasonTypeSN"];

                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                ifl.FP10216(crSN, refuseReasonTypeSN);
            }

            //删除预约 (融资方拒绝)
            if (busCode == "P10215")
            {
                string crSN = context.Request.Params["crSN"];

                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                ifl.FP10215(crSN);
            }

            //删除拒绝预约 （融资方取消）
            if (busCode == "P10217")
            {
                string crSN = context.Request.Params["crSN"];

                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                ifl.FP10217(crSN);
            }

            //投资
            if(busCode == "P10204")
            {
                string financierUserSN = context.Request.Params["financierUserSN"];
                string crSN = context.Request.Params["crSN"];
                decimal investMoneyAmount = Convert.ToDecimal(context.Request.Params["investMoneyAmount"]);
                DateTime investDate = Convert.ToDateTime(context.Request.Params["investDate"]);
                DateTime dealineDate = Convert.ToDateTime(context.Request.Params["dealineDate"]);
                string repaymentTypeSN = context.Request.Params["repaymentTypeSN"];
                decimal dailyRate = Convert.ToDecimal(context.Request.Params["dailyRate"]);

                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                ifl.FP10204(financierUserSN, crSN, investMoneyAmount, investDate, dealineDate, repaymentTypeSN, dailyRate);
            }

            //获取选择顾问列表
            if (busCode == "P10210")
            {
                string crSN = context.Request.Params["crSN"];
                string sortStr = context.Request.Params["sortStr"];

                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                res = ifl.FP10210(crSN, sortStr);
            }

            //获得顾问隐藏后信息
            if (busCode == "P10211")
            {
                string consultantUserSN = context.Request.Params["consultantUserSN"];
                int ifVisiable = Convert.ToInt32(context.Request.Params["ifVisiable"]);

                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                res = ifl.FP10211(consultantUserSN, ifVisiable);
            }

            //获得顾问隐藏的信息
            if (busCode == "P10213")
            {
                string pwd = context.Request.Params["pwd"];
                string consultantUserSN = context.Request.Params["consultantUserSN"];
                string crSN = context.Request.Params["crSN"];

                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                res = ifl.FP10213(pwd,consultantUserSN,crSN);
            }

            //确认预约财务顾问
            if (busCode == "P10301")
            {
                string consultantUserSN = context.Request.Params["consultantUserSN"];
                string crSN = context.Request.Params["crSN"];
                decimal quotePricePercent = Convert.ToDecimal(context.Request.Params["quotePricePercent"]);

                
                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                ifl.FP10301(consultantUserSN, crSN, quotePricePercent);
            }

            //查看顾问信息
            if (busCode == "P10208" || busCode == "P10308")
            {
                string consultantUserSN = context.Request.Params["consultantUserSN"];
                string crSN = context.Request.Params["crSN"];

                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                res = ifl.FP10208(crSN, consultantUserSN);
            }

            //取消财务顾问
            if (busCode == "P10209")
            {
                string crSN = context.Request.Params["crSN"];

                IFL.P1.CP102 ifl = new IFL.P1.CP102();
                ifl.FP10209(crSN);
            }
            #endregion 

                #region P103 已投资债权
                //初始化加载
                if(busCode == "P10303")
                {
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    IFL.P1.CP103 ifl = new IFL.P1.CP103();
                    res = ifl.FP10303(pageSize);
                }

                //滚动加载
                if (busCode == "P10304")
                {
                    DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                    int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    IFL.P1.CP103 ifl = new IFL.P1.CP103();
                    res = ifl.FP10304(maxDatetime, pageFrom, pageSize);
                }

                //融资方历史信息
                if (busCode == "P10307")
                {
                    string crSN = context.Request.Params["crSN"];
                    string financierUserSN = context.Request.Params["financierUserSN"];

                    IFL.P1.CP103 ifl = new IFL.P1.CP103();
                    res = ifl.FP10307(crSN,financierUserSN);
                }

                //结案
                if(busCode == "P10309")
                {
                    string crSN = context.Request.Params["crSN"];
                    DateTime repaymentDate = Convert.ToDateTime(context.Request.Params["repaymentDate"]);

                    IFL.P1.CP103 ifl = new IFL.P1.CP103();
                    ifl.FP10309(crSN, repaymentDate);
                }

                

                #endregion

                #region P104 已结案债权
                //初始化加载
                if (busCode == "P10501")
                {
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    IFL.P1.CP104 ifl = new IFL.P1.CP104();
                    res = ifl.FP10501(pageSize);
                }

                //滚动加载
                if (busCode == "P10502")
                {
                    DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                    int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    IFL.P1.CP104 ifl = new IFL.P1.CP104();
                    res = ifl.FP10502(maxDatetime, pageFrom, pageSize);
                }

                //删除
                if(busCode == "P10503")
                {
                    string crSN = context.Request.Params["crSN"];

                    IFL.P1.CP104 ifl = new IFL.P1.CP104();
                    ifl.FP10503(crSN);
                }
                #endregion


            #endregion

            #region P2 债权融资
            #region P201 发布债权
            //初始化
                if (busCode == "P20101")
                {
                    int num = Convert.ToInt32(context.Request.Params["num"]);

                    IFL.P2.CP201 ifl = new IFL.P2.CP201();
                    res = ifl.FP20101(num);
                }

                //滚动获取发布债权
                if(busCode == "P20102")
                {
                    DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                    int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    IFL.P2.CP201 ifl = new IFL.P2.CP201();
                    res = ifl.FP20102(maxDatetime, pageFrom, pageSize);
                }

                //发布债权
                if(busCode == "P20103")
                {
                    string creditRightData = context.Request.Params["crediteRightData"];
                    string pawnData = context.Request.Params["pawnData"];
                    string enterpriseData = context.Request.Params["enterpriseData"];
                    string pwd = context.Request.Params["pwd"];

                    IFL.P2.CP201 ifl = new IFL.P2.CP201();
                    res = ifl.FP20103(creditRightData, pawnData, enterpriseData,pwd);
                }

                //取消发布的债权
                if(busCode == "P20106")
                {
                    string crSN = context.Request.Params["crSN"];

                    IFL.P2.CP201 ifl = new IFL.P2.CP201();
                    ifl.FP20106(crSN);
                }

                //获取推荐投资方列表
                if(busCode == "P20107")
                {
                    string sortStr = context.Request.Params["sortStr"];

                    IFL.P2.CP201 ifl = new IFL.P2.CP201();
                    res = ifl.FP20107(sortStr);
                }

                //获取投资方详细信息
                if(busCode == "P20108")
                {
                    string investorUserSN = context.Request.Params["investorUserSN"];

                    IFL.P2.CP201 ifl = new IFL.P2.CP201();
                    res = ifl.FP20108(investorUserSN);
                }

                // 融资方发出预约
                if(busCode == "P20109")
                {
                    string pwd = context.Request.Params["pwd"];
                    string investorUserSN = context.Request.Params["investorUserSN"];
                    string crSN = context.Request.Params["crSN"];

                    IFL.P2.CP201 ifl = new IFL.P2.CP201();
                    res = ifl.FP20109(pwd,investorUserSN, crSN);
                }

                //获取发布费用和账户余额
                if(busCode == "P20111")
                {
                    IFL.P2.CP201 ifl = new IFL.P2.CP201();
                    res = ifl.FP20111();
                }
                #endregion

            #region P202 预约中债权
        //初始化
        if(busCode == "P20203")
        {
            int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

            IFL.P2.CP202 ifl = new IFL.P2.CP202();
            res = ifl.FP20203(pageSize);
        }

        //查看顾问信息
            if(busCode == "P20205")
            {
                string consultantUserSN = context.Request.Params["consultantUserSN"];

                IFL.P2.CP202 ifl = new IFL.P2.CP202();
                res = ifl.FP20205(consultantUserSN);
            }

        //滚动加载
        if(busCode == "P20209")
        {
            DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
            int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
            int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

            IFL.P2.CP202 ifl = new IFL.P2.CP202();
            res = ifl.FP20209(maxDatetime, pageFrom, pageSize);
        }

        //融资方拒绝债权预约
        if(busCode == "P20207")
        {
            string crSN = context.Request.Params["crSN"];
            string refuseReasonTypeSN = context.Request.Params["refuseReasonTypeSN"];

            IFL.P2.CP202 ifl = new IFL.P2.CP202();
            ifl.FP20207(crSN, refuseReasonTypeSN);
        }

        //融资取消预约
        if (busCode == "P20208")
        {
            string crSN = context.Request.Params["crSN"];
            string cancelReasonTypeSN = context.Request.Params["cancelReasonTypeSN"];

            IFL.P2.CP202 ifl = new IFL.P2.CP202();
            ifl.FP20208(crSN, cancelReasonTypeSN);
        }

        //删除预约（投资方拒绝）
        if (busCode == "P20210")
        {
            string crSN = context.Request.Params["crSN"];

            IFL.P2.CP202 ifl = new IFL.P2.CP202();
            ifl.FP20210(crSN);
        }

        //删除预约（投资方取消）
        if (busCode == "P20211")
        {
            string crSN = context.Request.Params["crSN"];

            IFL.P2.CP202 ifl = new IFL.P2.CP202();
            ifl.FP20211(crSN);
        }
        #endregion

            #region P103 还款中债权
            //初始化加载
            if (busCode == "P2030101")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P2.CP203 ifl = new IFL.P2.CP203();
                res = ifl.FP2030101(pageSize);
            }

            //滚动加载
            if (busCode == "P2030102")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P2.CP203 ifl = new IFL.P2.CP203();
                res = ifl.FP2030102(maxDatetime, pageFrom, pageSize);
            }
            #endregion

            #region P104 已还款债权
            if (busCode == "P2030201")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P2.CP204 ifl = new IFL.P2.CP204();
                res = ifl.FP2030201(pageSize);
            }

            //滚动加载
            if (busCode == "P2030202")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P2.CP204 ifl = new IFL.P2.CP204();
                res = ifl.FP2030202(maxDatetime, pageFrom, pageSize);
            }

            //删除
            if (busCode == "P2030203")
            {
                string crSN = context.Request.Params["crSN"];

                IFL.P2.CP204 ifl = new IFL.P2.CP204();
                ifl.FP2030203(crSN);
            }
            #endregion

            #endregion

            #region P3 资产出售

            #region P301 发布资产
            //P30101 初始化
            if(busCode == "P30101")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P3.CP301 ifl = new IFL.P3.CP301();
                res = ifl.FP30101(pageSize);
            }

            //P30102 滚动加载
            if (busCode == "P30102")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P3.CP301 ifl = new IFL.P3.CP301();
                res = ifl.FP30102(maxDatetime, pageFrom, pageSize);
            }

            //P30103 发布
            if (busCode == "P30103")
            {
                string assetsData = context.Request.Params["assetsData"];
                string pwd = context.Request.Params["pwd"];

                IFL.P3.CP301 ifl = new IFL.P3.CP301();
                res = ifl.FP30103(assetsData, pwd);
            }

            //P30104 取消发布
            if (busCode == "P30104")
            {
                string assetsSN = context.Request.Params["assetsSN"];

                IFL.P3.CP301 ifl = new IFL.P3.CP301();
                ifl.FP30104(assetsSN);
            }

            //P30105 获取发布资产费用、余额、服务费率
            if (busCode == "P30105")
            {
                IFL.P3.CP301 ifl = new IFL.P3.CP301();
                res = ifl.FP30105();
            }
            #endregion

            #region P302 预约中债权
            //P30201 初始化
            if (busCode == "P30201")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P3.CP302 ifl = new IFL.P3.CP302();
                res = ifl.FP30201(pageSize);
            }

            //P30202 滚动加载
            if (busCode == "P30202")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P3.CP302 ifl = new IFL.P3.CP302();
                res = ifl.FP30202(maxDatetime, pageFrom, pageSize);
            }

            //P30203 购买方信息
            if (busCode == "P30203")
            {
                string purchaserUserSN = context.Request.Params["purchaserUserSN"];

                IFL.P3.CP302 ifl = new IFL.P3.CP302();
                res = ifl.FP30203(purchaserUserSN);
            }

            //P30204 拒绝
            if (busCode == "P30204")
            {
                string reserveSN = context.Request.Params["reserveSN"];
                string refuseReasonTypeSN = context.Request.Params["refuseReasonTypeSN"];

                IFL.P3.CP302 ifl = new IFL.P3.CP302();
                ifl.FP30204(reserveSN, refuseReasonTypeSN);
            }

            #endregion

            #region P303 已售资产
            //P30301 初始化
            if (busCode == "P30301") 
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P3.CP303 ifl = new IFL.P3.CP303();
                res = ifl.FP30301(pageSize);
            }

            //P30302 滚动加载
            if (busCode == "P30302")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P3.CP303 ifl = new IFL.P3.CP303();
                res = ifl.FP30302(maxDatetime, pageFrom, pageSize);
            }

            //P30303 删除
            if (busCode == "P30303")
            {
                string purchaseSN = context.Request.Params["purchaseSN"];

                IFL.P3.CP303 ifl = new IFL.P3.CP303();
                ifl.FP30303(purchaseSN);
            }
            #endregion

            #endregion

            #region P4 资产购买

            #region P401 寻找资产
            //P40101 初始化
            if (busCode == "P40101")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P4.CP401 ifl = new IFL.P4.CP401();
                res = ifl.FP40101(pageSize);
            }

            //P40102 滚动加载
            if (busCode == "P40102")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P4.CP401 ifl = new IFL.P4.CP401();
                res = ifl.FP40102(maxDatetime, pageFrom, pageSize);
            }

            //P40103 出售方信息
            if(busCode == "P40103")
            {
                string sellerSN = context.Request.Params["sellerSN"];

                IFL.P4.CP401 ifl = new IFL.P4.CP401();
                res = ifl.FP40103(sellerSN);
            }
            #endregion 

            #region P402 预约中资产
            //P40201 预约
            if(busCode == "P40201")
            {
                string assetsSN = context.Request.Params["assetsSN"];
                string pwd = context.Request.Params["pwd"];

                IFL.P4.CP402 ifl = new IFL.P4.CP402();
                res = ifl.FP40201(assetsSN, pwd);
            }

            //P40202 初始化
            if (busCode == "P40202")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P4.CP402 ifl = new IFL.P4.CP402();
                res = ifl.FP40202(pageSize);
            }

            //P40203 滚动加载
            if (busCode == "P40203")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P4.CP402 ifl = new IFL.P4.CP402();
                res = ifl.FP40203(maxDatetime, pageFrom, pageSize);
            }

            //P40204 出售方信息
            if (busCode == "P40204")
            {
                string sellerUserSN = context.Request.Params["sellerUserSN"];

                IFL.P4.CP402 ifl = new IFL.P4.CP402();
                res = ifl.FP40204(sellerUserSN);
            }

            //P40205 购买
            if (busCode == "P40205")
            {
                string reserveSN = context.Request.Params["reserveSN"];
                decimal amount = Convert.ToDecimal(context.Request.Params["amount"]);

                IFL.P4.CP402 ifl = new IFL.P4.CP402();
                ifl.FP40205(reserveSN, amount);
            }

            //P40206 取消预约
            if (busCode == "P40206")
            {
                string reserveSN = context.Request.Params["reserveSN"];
                string cancelReasonTypeSN = context.Request.Params["cancelReasonTypeSN"];

                IFL.P4.CP402 ifl = new IFL.P4.CP402();
                ifl.FP40206(reserveSN, cancelReasonTypeSN);
            }

            //P40207 获取预约费用，余额
            if (busCode == "P40207")
            {
                IFL.P4.CP402 ifl = new IFL.P4.CP402();
                res = ifl.FP40207();
            }


            #endregion 

            #region P404 已购资产
            //P40401 初始化
            if (busCode == "P40401")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P4.CP404 ifl = new IFL.P4.CP404();
                res = ifl.FP40401(pageSize);
            }

            //P40402 滚动加载
            if (busCode == "P40402")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P4.CP404 ifl = new IFL.P4.CP404();
                res = ifl.FP40402(maxDatetime, pageFrom, pageSize);
            }

            //P40403 出售方信息
            if (busCode == "P40403")
            {
                string sellerUserSN = context.Request.Params["sellerUserSN"];

                IFL.P4.CP404 ifl = new IFL.P4.CP404();
                res = ifl.FP40403(sellerUserSN);
            }

            //P40404 删除
            if (busCode == "P40404")
            {
                string purchaseSN = context.Request.Params["purchaseSN"];

                IFL.P4.CP404 ifl = new IFL.P4.CP404();
                ifl.FP40404(purchaseSN);
            }

            #endregion

            #endregion

            #region P5 财务顾问
            #region P502 预约中债权
            //初始化
            if (busCode == "P50201")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P5.CP502 ifl = new IFL.P5.CP502();
                res = ifl.FP50201(pageSize);
            }

            //滚动加载
            if (busCode == "P50203")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P5.CP502 ifl = new IFL.P5.CP502();
                res = ifl.FP50203(maxDatetime, pageFrom,pageSize);
            }

            //接受预约
            if (busCode == "P50202")
            {
                string reserveSN = context.Request.Params["reserveSN"];

                IFL.P5.CP502 ifl = new IFL.P5.CP502();
                ifl.FP50202(reserveSN);
            }

            //拒绝预约
            if (busCode == "P50208")
            {
                string reserveSN = context.Request.Params["reserveSN"];
                string refuseReasonTypeSN = context.Request.Params["refuseReasonTypeSN"];

                IFL.P5.CP502 ifl = new IFL.P5.CP502();
                ifl.FP50208(reserveSN, refuseReasonTypeSN);
            }

            //获取抵押物信息
            if (busCode == "P50205")
            {
                string reserveSN = context.Request.Params["reserveSN"];

                IFL.P5.CP502 ifl = new IFL.P5.CP502();
                res = ifl.FP50205(reserveSN);
            }

            //企业信息
            if (busCode == "P50207")
            {
                string reserveSN = context.Request.Params["reserveSN"];

                IFL.P5.CP502 ifl = new IFL.P5.CP502();
                res = ifl.FP50207(reserveSN);
            }
            #endregion

            #region P503 审核中债权
            //初始化
            if (busCode == "P50301")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P5.CP503 ifl = new IFL.P5.CP503();
                res = ifl.FP50301(pageSize);
            }

            //滚动加载
            if (busCode == "P50302")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P5.CP503 ifl = new IFL.P5.CP503();
                res = ifl.FP50302(maxDatetime, pageFrom, pageSize);
            }

            //获取抵押物信息
            if(busCode == "P50306")
            {
                string serverSN = context.Request.Params["serverSN"];

                IFL.P5.CP503 ifl = new IFL.P5.CP503();
                res = ifl.FP50306(serverSN);
            }

            //投资方信息
            if (busCode == "P50307")
            {
                string serverSN = context.Request.Params["serverSN"];

                IFL.P5.CP503 ifl = new IFL.P5.CP503();
                res = ifl.FP50307(serverSN);
            }

            //企业信息
            if (busCode == "P50308")
            {
                string serverSN = context.Request.Params["serverSN"];

                IFL.P5.CP503 ifl = new IFL.P5.CP503();
                res = ifl.FP50308(serverSN);
            }

            //审核通过
            if (busCode == "P50303")
            {
                string serverSN = context.Request.Params["serverSN"];

                IFL.P5.CP503 ifl = new IFL.P5.CP503();
                ifl.FP50303(serverSN);
            }

            //审核未过
            if (busCode == "P50309")
            {
                string serverSN = context.Request.Params["serverSN"];
                string auditNotPassReasonTypeSN = context.Request.Params["auditNotPassReasonTypeSN"];

                IFL.P5.CP503 ifl = new IFL.P5.CP503();
                ifl.FP50309(serverSN, auditNotPassReasonTypeSN);
            }

            //删除
            if (busCode == "P50310")
            {
                string serverSN = context.Request.Params["serverSN"];

                IFL.P5.CP503 ifl = new IFL.P5.CP503();
                ifl.FP50310(serverSN);
            }
            #endregion

            #region P50401 已审核债权
            //初始化
            if(busCode == "P5040101")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P5.CP50401 ifl = new IFL.P5.CP50401();
                res = ifl.FP5040101(pageSize);
            }

            //滚动加载
            if (busCode == "P5040102")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P5.CP50401 ifl = new IFL.P5.CP50401();
                res = ifl.FP5040102(maxDatetime, pageFrom,pageSize);
            }

            //融资方历史信息
            if (busCode == "P5040104")
            {
                string crSN = context.Request.Params["crSN"];
                string financierUserSN = context.Request.Params["financierUserSN"];

                IFL.P5.CP50401 ifl = new IFL.P5.CP50401();
                res = ifl.FP5040104(crSN, financierUserSN);
            }
            #endregion 

            #region P50402 已结案债权
            //初始化
            if(busCode == "P5040201")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P5.CP50402 ifl = new IFL.P5.CP50402();
                res = ifl.FP5040201(pageSize);
            }

            //滚动加载
            if (busCode == "P5040202")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P5.CP50402 ifl = new IFL.P5.CP50402();
                res = ifl.FP5040202(maxDatetime, pageFrom, pageSize);
            }

            //删除
            if(busCode == "P5040203")
            {
                string serverSN = context.Request.Params["serverSN"];

                IFL.P5.CP50402 ifl = new IFL.P5.CP50402();
                ifl.FP5040203(serverSN);
            }
            #endregion

            #endregion

            #region P7 促销活动

            #region P701 成长值
            //VIP等级
            if(busCode == "P70101")
            {
                IFL.P7.CP701 ifl = new IFL.P7.CP701();
                res = ifl.FP70101();
            }

            //成长明细初始化
            if(busCode == "P70102")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P7.CP701 ifl = new IFL.P7.CP701();
                res = ifl.FP70102(pageSize);
            }

            //成长明细滚动加载
            if (busCode == "P70103")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P7.CP701 ifl = new IFL.P7.CP701();
                res = ifl.FP70103(maxDatetime, pageFrom, pageSize);
            }
            #endregion

            #region P702 扩广活动
            //注册充值 P70201
            if (busCode == "P70201")
            {
                IFL.P7.CP702 ifl = new IFL.P7.CP702();
                ifl.FP70201();
            }

            #endregion

            #endregion

            #region P8 用户账户

            #region P802 用户中心 申请服务
            //初始化
            if (busCode == "P80200")
            {
                IFL.P8.CP802 ifl = new IFL.P8.CP802();
                res = ifl.FP80200();
            }

            //获取投资方用户信息 P802010105
            if (busCode == "P802010206")
            {
                IFL.P8.CP802 ifl = new IFL.P8.CP802();
                res = ifl.FP802010206();
            }

            //申请投资服务 P802010106
            if (busCode == "P802010106")
            {
                string investSvrData = context.Request.Params["investSvrData"];
                string pwd = context.Request.Params["pwd"];

                IFL.P8.CP802 ifl = new IFL.P8.CP802();
                res = ifl.FP802010106(investSvrData, pwd);
            }

            //获取融资方用户信息 P802010105
            if (busCode == "P802010105")
            {
                IFL.P8.CP802 ifl = new IFL.P8.CP802();
                res = ifl.FP802010105();
            }

            //申请融资服务 P802010207
            if (busCode == "P802010207")
            {
                string financingSvrData = context.Request.Params["financingSvrData"];
                string pwd = context.Request.Params["pwd"];

                IFL.P8.CP802 ifl = new IFL.P8.CP802();
                res = ifl.FP802010207(financingSvrData, pwd);
            }

            //获取出售方用户信息 P802010303
            if (busCode == "P802010303")
            {
                IFL.P8.CP802 ifl = new IFL.P8.CP802();
                res = ifl.FP802010303();
            }

            //申请出售服务 P802010304
            if (busCode == "P802010304")
            {
                string SellingSvrData = context.Request.Params["SellingSvrData"];
                string pwd = context.Request.Params["pwd"];

                IFL.P8.CP802 ifl = new IFL.P8.CP802();
                res = ifl.FP802010304(SellingSvrData, pwd);
            }

            //获取购买方用户信息 P802010404
            if (busCode == "P802010404")
            {
                IFL.P8.CP802 ifl = new IFL.P8.CP802();
                res = ifl.FP802010404();
            }

            //申请购买服务 P802010405
            if (busCode == "P802010405")
            {
                string PurchaseSvrData = context.Request.Params["PurchaseSvrData"];
                string pwd = context.Request.Params["pwd"];

                IFL.P8.CP802 ifl = new IFL.P8.CP802();
                res = ifl.FP802010405(PurchaseSvrData, pwd);
            }

            //获取顾问用户信息 P802010505
            if (busCode == "P802010505")
            {
                IFL.P8.CP802 ifl = new IFL.P8.CP802();
                res = ifl.FP802010505();
            }

            //申请顾问服务 P802010506
            if (busCode == "P802010506")
            {
                string consultantSvrData = context.Request.Params["consultantSvrData"];
                string pwd = context.Request.Params["pwd"];

                IFL.P8.CP802 ifl = new IFL.P8.CP802();
                res = ifl.FP8010506(consultantSvrData, pwd);
            }

        #endregion

            #region P801 基本资料
            //初始化
            if(busCode == "P8010100")
            {
                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                res = ifl.FP8010100();
            }

            #region 债权投资
            //债权投资载入
            if (busCode == "P8010101")
            {
                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                res = ifl.FP8010101();
            }

            //P8010102 个人信息
            if(busCode == "P8010102")
            {
                string maritalStatusTypeSN = context.Request.Params["maritalStatusTypeSN"];
                bool procreateStatus = Convert.ToBoolean(context.Request.Params["procreateStatus"]);
                string currentAddressProvinceSN = context.Request.Params["currentAddressProvinceSN"];
                string currentAddressCitySN = context.Request.Params["currentAddressCitySN"];
                string currentAddressDetails = context.Request.Params["currentAddressDetails"];

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010102(maritalStatusTypeSN, procreateStatus, currentAddressProvinceSN, currentAddressCitySN, currentAddressDetails);
            }

            //P8010103 投资意向
            if (busCode == "P8010103")
            {
                string investMainTypeSN = context.Request.Params["investMainTypeSN"];
                bool financingMain = Convert.ToBoolean(context.Request.Params["financingMain"]);
                decimal maxMortgageRate = Convert.ToDecimal(context.Request.Params["maxMortgageRate"]);
                string guaranteeTypeSN = context.Request.Params["guaranteeTypeSN"];
                string investProvinceSN = context.Request.Params["investProvinceSN"];
                string investCitySN = context.Request.Params["investCitySN"];
                decimal minInvestMoneyAmount = Convert.ToDecimal(context.Request.Params["minInvestMoneyAmount"]);
                decimal minInterestRate = Convert.ToDecimal(context.Request.Params["minInterestRate"]);
                short minInvestDays = Convert.ToInt16(context.Request.Params["minInvestDays"]);
                short maxInvestDays = Convert.ToInt16(context.Request.Params["maxInvestDays"]);
                string collateralDemand = context.Request.Params["collateralDemand"];

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010103(investMainTypeSN, financingMain, maxMortgageRate, guaranteeTypeSN, investProvinceSN, investCitySN, minInvestMoneyAmount, minInterestRate, minInvestDays, maxInvestDays, collateralDemand);
            }

            //P8010104 工作信息
            if (busCode == "P8010104")
            {
                string workEnterprise = context.Request.Params["workEnterprise"];
                string enterpriseTypeSN = context.Request.Params["enterpriseTypeSN"];
                DateTime? hiredate;
                if(context.Request.Params["hiredate"] == null)
                {
                    hiredate = null;
                }
                else
                {
                    hiredate = Convert.ToDateTime(context.Request.Params["hiredate"]);
                }
                string workTel = context.Request.Params["workTel"];
                string post = context.Request.Params["post"];
                string enterpriseSwitchboard = context.Request.Params["enterpriseSwitchboard"];
                string enterpriseWebsite = context.Request.Params["enterpriseWebsite"];

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010104(workEnterprise, enterpriseTypeSN, hiredate, workTel, post, enterpriseSwitchboard, enterpriseWebsite);
            }
            #endregion

            #region 债权融资
            //P8010201 债权融资载入
            if (busCode == "P8010201")
            {
                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                res = ifl.FP8010201();
            }

            //P8010202 个人信息
            if(busCode == "P8010202")
            {
                string maritalStatusTypeSN = context.Request.Params["maritalStatusTypeSN"];
                bool procreateStatus = Convert.ToBoolean(context.Request.Params["procreateStatus"]);
                string healthyStatusTypeSN = context.Request.Params["healthyStatusTypeSN"];
                bool ifBasicLivingAllowance = Convert.ToBoolean(context.Request.Params["ifBasicLivingAllowance"]);
                string currentAddressProvinceSN = context.Request.Params["currentAddressProvinceSN"];
                string currentAddressCitySN = context.Request.Params["currentAddressCitySN"];
                string currentAddressDetails = context.Request.Params["currentAddressDetails"];
                string graduateSchool = context.Request.Params["graduateSchool"];
                string degreeTypeSN = context.Request.Params["degreeTypeSN"];
                string degreeCard = context.Request.Params["degreeCard"];
                string friendName = context.Request.Params["friendName"];
                string friendPhone = context.Request.Params["friendPhone"];

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010202(maritalStatusTypeSN, procreateStatus, healthyStatusTypeSN, ifBasicLivingAllowance, currentAddressProvinceSN, currentAddressCitySN, currentAddressDetails, graduateSchool, degreeTypeSN, degreeCard, friendName, friendPhone);
            }

            //P8010203 家庭信息
            if(busCode == "P8010203")
            {
                string spouseName = context.Request.Params["spouseName"];
                string spousePhone = context.Request.Params["spousePhone"];
                string spouseIdCard = context.Request.Params["spouseIdCard"];
                string spouseEnterprise = context.Request.Params["spouseEnterprise"];
                string kinName = context.Request.Params["kinName"];
                string kinRelationshipTypeSN = context.Request.Params["kinRelationshipTypeSN"];
                string kinPhone = context.Request.Params["kinPhone"];
                string kinIdCard = context.Request.Params["kinIdCard"];
                string kinEnterprise = context.Request.Params["kinEnterprise"];

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010203(spouseName, spousePhone, spouseIdCard, spouseEnterprise, kinName, kinRelationshipTypeSN, kinPhone, kinIdCard, kinEnterprise);
            }

            //P8010204 工作信息
            if(busCode == "P8010204")
            {
                string workEnterprise = context.Request.Params["workEnterprise"];
                string enterpriseTypeSN = context.Request.Params["enterpriseTypeSN"];
                DateTime? hiredate;
                if(context.Request.Params["hiredate"] == null)
                {
                    hiredate = null;
                }
                else
                {
                    hiredate = Convert.ToDateTime(context.Request.Params["hiredate"]);
                }
                string workTel = context.Request.Params["workTel"];
                string post = context.Request.Params["post"];
                string enterpriseSwitchboard = context.Request.Params["enterpriseSwitchboard"];
                string enterpriseWebsite = context.Request.Params["enterpriseWebsite"];
                string colleageName = context.Request.Params["colleageName"];
                string colleagePhone = context.Request.Params["colleagePhone"];

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010204(workEnterprise, enterpriseTypeSN, hiredate, workTel, post, enterpriseSwitchboard, enterpriseWebsite, colleageName, colleagePhone);
            }

            //P8010205 财务信息
            if(busCode == "P8010205")
            {
                decimal monthlyTotalIncome = Convert.ToDecimal(context.Request.Params["monthlyTotalIncome"]);
                decimal monthlyTotalExpenditure = Convert.ToDecimal(context.Request.Params["monthlyTotalExpenditure"]);
                decimal monthlyNetIncome = Convert.ToDecimal(context.Request.Params["monthlyNetIncome"]);
                decimal totalAssets = Convert.ToDecimal(context.Request.Params["totalAssets"]);
                decimal totalDebt = Convert.ToDecimal(context.Request.Params["totalDebt"]);
                bool ifCourtImplementation = Convert.ToBoolean(context.Request.Params["ifCourtImplementation"]);
                bool creditStatus = Convert.ToBoolean(context.Request.Params["creditStatus"]);

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010205(monthlyTotalIncome, monthlyTotalExpenditure, monthlyNetIncome, totalAssets, totalDebt, ifCourtImplementation, creditStatus);
            }
            #endregion

            #region P80103 资产出售
            //P8010301 载入
            if (busCode == "P8010301")
            {
                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                res = ifl.FP8010301();
            }

            //P8010302 个人信息
            if(busCode == "P8010302")
            {
                string maritalStatusTypeSN = context.Request.Params["maritalStatusTypeSN"];
                bool procreateStatus = Convert.ToBoolean(context.Request.Params["procreateStatus"]);
                string currentAddressProvinceSN = context.Request.Params["currentAddressProvinceSN"];
                string currentAddressCitySN = context.Request.Params["currentAddressCitySN"];
                string currentAddressDetails = context.Request.Params["currentAddressDetails"];

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010302(maritalStatusTypeSN, procreateStatus, currentAddressProvinceSN, currentAddressCitySN, currentAddressDetails);
            }
            #endregion

            #region P80104 资产购买
            //P8010401 资产购买载入
            if(busCode == "P8010401")
            {
                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                res = ifl.FP8010401();
            }

            //P8010402 个人信息
            if(busCode == "P8010402")
            {
                string maritalStatusTypeSN = context.Request.Params["maritalStatusTypeSN"];
                bool procreateStatus = Convert.ToBoolean(context.Request.Params["procreateStatus"]);
                string currentAddressProvinceSN = context.Request.Params["currentAddressProvinceSN"];
                string currentAddressCitySN = context.Request.Params["currentAddressCitySN"];
                string currentAddressDetails = context.Request.Params["currentAddressDetails"];

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010402(maritalStatusTypeSN, procreateStatus, currentAddressProvinceSN, currentAddressCitySN, currentAddressDetails);
            }

            //P8010403 购买意向
            if(busCode == "P8010403")
            {
                string assetsProvince = context.Request.Params["assetsProvince"];
                decimal minPurchasePrice = Convert.ToDecimal(context.Request.Params["minPurchasePrice"]);
                decimal maxPurchasePrice = Convert.ToDecimal(context.Request.Params["maxPurchasePrice"]);
                string assetsType = context.Request.Params["assetsType"];

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010403(assetsProvince, minPurchasePrice, maxPurchasePrice, assetsType);
            }
            #endregion

            #region P80105 顾问信息
            //P8010501 顾问信息载入
            if (busCode == "P8010501")
            {
                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                res = ifl.FP8010501();
            }

            //P8010502 个人信息
            if(busCode == "P8010502")
            {
                string maritalStatusTypeSN = context.Request.Params["maritalStatusTypeSN"];
                bool procreateStatus = Convert.ToBoolean(context.Request.Params["procreateStatus"]);
                string currentAddressProvinceSN = context.Request.Params["currentAddressProvinceSN"];
                string currentAddressCitySN = context.Request.Params["currentAddressCitySN"];
                string currentAddressDetails = context.Request.Params["currentAddressDetails"];
                string graduateSchool = context.Request.Params["graduateSchool"];
                string degreeTypeSN = context.Request.Params["degreeTypeSN"];
                string degreeCard = context.Request.Params["degreeCard"];

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010502(maritalStatusTypeSN, procreateStatus, currentAddressProvinceSN, currentAddressCitySN, currentAddressDetails, graduateSchool, degreeTypeSN, degreeCard);
            }

            //P8010503 工作信息
            if(busCode == "P8010503")
            {
                string workEnterprise = context.Request.Params["workEnterprise"];
                string enterpriseTypeSN = context.Request.Params["enterpriseTypeSN"];
                string industryTypeSN = context.Request.Params["industryTypeSN"];
                DateTime? hiredate;
                if (context.Request.Params["hiredate"] == null)
                {
                    hiredate = null;
                }
                else
                {
                    hiredate = Convert.ToDateTime(context.Request.Params["hiredate"]);
                }
                string workTel = context.Request.Params["workTel"];
                string post = context.Request.Params["post"];
                string enterpriseSwitchboard = context.Request.Params["enterpriseSwitchboard"];
                string enterpriseWebsite = context.Request.Params["enterpriseWebsite"];
                string colleageName = context.Request.Params["colleageName"];
                string colleagePhone = context.Request.Params["colleagePhone"];
                string colleageIdCard = context.Request.Params["colleageIdCard"];

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010503(workEnterprise, enterpriseTypeSN, industryTypeSN, hiredate, workTel, post, enterpriseSwitchboard, enterpriseWebsite, colleageName, colleagePhone, colleageIdCard);
            }

            //P8010504 顾问介绍
            if(busCode == "P8010504")
            {
                string serviceProvinceSN = context.Request.Params["serviceProvinceSN"];
                string serviceCitySN = context.Request.Params["serviceCitySN"];
                bool investigate = Convert.ToBoolean(context.Request.Params["investigate"]);
                bool assetsEvaluate = Convert.ToBoolean(context.Request.Params["assetsEvaluate"]);
                bool creditRightGuarantee = Convert.ToBoolean(context.Request.Params["creditRightGuarantee"]);
                bool badLoanCollect = Convert.ToBoolean(context.Request.Params["badLoanCollect"]);
                string consultantDetails = context.Request.Params["consultantDetails"];

                IFL.P8.CP801 ifl = new IFL.P8.CP801();
                ifl.FP8010504(serviceProvinceSN, serviceCitySN, investigate, assetsEvaluate, creditRightGuarantee, badLoanCollect, consultantDetails);
            }
            #endregion

            #endregion

            #region P803 安全管理
            //初始化
            if(busCode == "P80306")
            {
                IFL.P8.CP803 ifl = new IFL.P8.CP803();
                res = ifl.FP80306();
            }

            //发送验证码
            if(busCode == "P80307")
            {
                string phone = context.Request.Params["phone"];
                string businessType = context.Request.Params["businessType"];

                IFL.P8.CP803 ifl = new IFL.P8.CP803();
                ifl.FP80307(phone, businessType);
            }

            //手机绑定
            if (busCode == "P80308")
            {
                string oldPhone = context.Request.Params["oldPhone"];
                string oldIC = context.Request.Params["oldIC"];
                string newPhone = context.Request.Params["newPhone"];
                string newIC = context.Request.Params["newIC"];

                IFL.P8.CP803 ifl = new IFL.P8.CP803();
                res = ifl.FP80308(oldPhone, oldIC, newPhone, newIC).ToString();
            }

            //登录密码
            if (busCode == "P80309")
            {
                string oldPwd = context.Request.Params["oldPwd"];
                string newPwd = context.Request.Params["newPwd"];

                IFL.P8.CP803 ifl = new IFL.P8.CP803();
                res = ifl.FP80309(oldPwd, newPwd).ToString();
            }

            //交易密码
            if (busCode == "P80310")
            {
                string IC = context.Request.Params["IC"];
                string newPwd = context.Request.Params["newPwd"];

                IFL.P8.CP803 ifl = new IFL.P8.CP803();
                res = ifl.FP80310(IC, newPwd).ToString();
            }

            //邮箱绑定
            if (busCode == "P80311")
            {
                string oldEmail = context.Request.Params["oldEmail"];
                string newEmail = context.Request.Params["newEmail"];

                IFL.P8.CP803 ifl = new IFL.P8.CP803();
                res = ifl.FP80311(oldEmail, newEmail).ToString();
            }

            //通知设定
            if (busCode == "P80312")
            {
                string name = context.Request.Params["name"];
                string value = context.Request.Params["value"];

                IFL.P8.CP803 ifl = new IFL.P8.CP803();
                ifl.FP80312(name, value);
            }
            #endregion

            #region P804，登录、注册
            //注册
    if(busCode == "P80401")
    {
        string phoneNum = context.Request.Params["phoneNum"];
        string pwd = context.Request.Params["pwd"];
        string name = context.Request.Params["name"];
        string idCard = context.Request.Params["idCard"];
        string identifyingCode = context.Request.Params["identifyingCode"];

        IFL.P8.CP804 ifl = new IFL.P8.CP804();
        res = ifl.FP80401(phoneNum, pwd, name, idCard, identifyingCode);
    }

    //发送短信验证码
    if (busCode == "P80402")
    {
        string phoneNum = context.Request.Params["phoneNum"];

        IFL.P8.CP804 ifl = new IFL.P8.CP804();
        ifl.FP80402(phoneNum);
    }

    //登录
    if(busCode == "P80403")
    {
        string phoneNum = context.Request.Params["phoneNum"];
        string pwd = context.Request.Params["pwd"];

        IFL.P8.CP804 ifl = new IFL.P8.CP804();
        res = ifl.FP80403(phoneNum, pwd).ToString();
    }

    //忘记密码
    if (busCode == "P80404")
    {
        string phoneNum = context.Request.Params["phoneNum"];

        IFL.P8.CP804 ifl = new IFL.P8.CP804();
        res = ifl.FP80404(phoneNum).ToString();
    }
    #endregion 

        #endregion

            #region P9 财务管理

            #region P901 账户信息
            //P90101 初始化
            if(busCode == "P90101")
            {
                IFL.P9.CP901 ifl = new IFL.P9.CP901();
                res = ifl.FP90101();
            }

            //P90102 充值
            if(busCode == "P90102")
            {
                decimal rechargeAmount = Convert.ToInt32(context.Request.Params["rechargeAmount"]);
                string cardType = context.Request.Params["cardType"];
                string bankId = context.Request.Params["bankId"];

                IFL.P9.CP901 ifl = new IFL.P9.CP901();
                res = ifl.FP90102(rechargeAmount, cardType, bankId);
            }

            //P901020101 用户点击充值成功或者失败
            if (busCode == "P901020101")
            {
                string billNo = context.Request.Params["billNo"];

                IFL.P9.CP901 ifl = new IFL.P9.CP901();
                res = ifl.FP901020101(billNo);
            }

            #endregion

            #region P902 账单信息
            //初始化
            if (busCode == "P90201")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P9.CP902 ifl = new IFL.P9.CP902();
                res = ifl.FP90201(pageSize);
            }

            //滚动加载
            if (busCode == "P90203")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P9.CP902 ifl = new IFL.P9.CP902();
                res = ifl.FP90203(maxDatetime, pageFrom, pageSize);
            }

            //获取账户余额
            if(busCode == "P90204")
            {
                IFL.P9.CP902 ifl = new IFL.P9.CP902();
                res = ifl.FP90204();
            }

            //付款
            if(busCode =="P90202")
            {
                string billSN = context.Request.Params["billSN"];
                string pwd = context.Request.Params["pwd"];

                IFL.P9.CP902 ifl = new IFL.P9.CP902();
                res = ifl.FP90202(billSN,pwd);
            }
            #endregion

            #region P903 历史明细
            //初始化
            if(busCode == "P90301")
            {
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P9.CP903 ifl = new IFL.P9.CP903();
                res = ifl.FP90301(pageSize);
            }

            //滚动加载
            if (busCode == "P90302")
            {
                DateTime maxDatetime = Convert.ToDateTime(context.Request.Params["maxDatetime"]);
                int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                IFL.P9.CP903 ifl = new IFL.P9.CP903();
                res = ifl.FP90302(maxDatetime, pageFrom,pageSize);
            }
            #endregion 

            #endregion

            #region 公共
            //由省份获取市
            if (busCode == "P8020001" || busCode == "P10108" || busCode == "P8010105" || busCode == "P20110")
            {
                string provinceSN = context.Request.Params["provinceSN"];

                res = IFL.Comm.C201.FC20140(provinceSN);
            }

                //获取抵押物信息
            if (busCode == "P10103" || busCode == "P20104" || busCode == "P20206" || busCode == "P10206" || busCode == "P10305" || busCode == "P2030104" || busCode == "P5040105")
            {
                string crSN = context.Request.Params["crSN"];

                res = IFL.Comm.C201.FC20141(crSN);
            }

                //获取企业信息
            if (busCode == "P10104" || busCode == "P20105" || busCode == "P10212" || busCode == "P10310" || busCode == "P5040107")
            {
                string crSN = context.Request.Params["crSN"];

                res = IFL.Comm.C201.FC20142(crSN);
            }

                //获取融资方信息
            if (busCode == "P10205" || busCode == "P10306" || busCode == "P50204" || busCode == "P50304" || busCode == "P5040103")
            {
                string financierUserSN = context.Request.Params["financierUserSN"];

                res = C201.FC20143(financierUserSN);
            }

            //投资方信息
            if (busCode == "P20204" || busCode == "P2030103" || busCode == "P50206" || busCode == "P5040106")
            {
                string investorUserSN = context.Request.Params["investorUserSN"];

                res = C201.FC20144(investorUserSN);
            }

            //账户退出
            if(busCode == "logout")
            {
                C201 c = new C201();
                c.FC20150();
            }
            #endregion
            context.Response.ContentType = "text/plain";
            context.Response.Write(res);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}