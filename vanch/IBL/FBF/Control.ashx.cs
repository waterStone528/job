using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FBF.FBF
{
    /// <summary>
    /// Summary description for Control
    /// </summary>
    public class Control : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string busCode = context.Request.Params["busCode"];
            string opeType = context.Request.Params["opeType"];

            string res = string.Empty;

            #region M02 客户管理

                #region M0201 债权融资
                //M0201 INIT，初始化
                if(busCode == "M0201INIT")
                {
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FINIT(pageSize);
                }

                //M020112，排序
                if (busCode == "M020112")
                {
                    string sortStr = context.Request.Params["sortStr"];
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FM020112(sortStr, pageSize);
                }

                //M020113，加载更多
                if (busCode == "M020113")
                {
                    int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);
                    string sortStr = context.Request.Params["sortStr"];
                    

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FM020113(pageFrom, pageSize, sortStr);
                }

                //M020101， 用户信息
                if (busCode == "M020101")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FM020101(userSN);
                }

                //M020103 获取取消发布债权
                if (busCode == "M020103")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FM020103(userSN);
                }

                //M020104 获取发布债权
                if (busCode == "M020104")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FM020104(userSN);
                }

                //M020102 取消已发布债权
                if (busCode == "M020102")
                {
                    string crSN = context.Request.Params["crSN"];

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FM020102(crSN);
                }

                //M020105 取消预约
                if (busCode == "M020105")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FM020105(userSN);
                }
    
                //M020106 拒绝预约
                if (busCode == "M020106")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FM020106(userSN);
                }

                //M020107 预约中债权
                if (busCode == "M020107")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FM020107(userSN);
                }

                //M020108 还款中债权
                if (busCode == "M020108")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FM020108(userSN);
                }

                //M020109 已还款债权
                if (busCode == "M020109")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FM020109(userSN);
                }

                //M020110 查看备注
                if (busCode == "M020110")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    res = ibl.FM020110(userSN);
                }

                //M020111 保存备注
                if (busCode == "M020111")
                {
                    string userSN = context.Request.Params["userSN"];
                    string note = context.Request.Params["note"];

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    ibl.FM020111(userSN, note);
                }

                //M020114，修改服务状态
                if (busCode == "M020114")
                {
                    string userSN = context.Request.Params["userSN"];
                    string status = context.Request.Params["status"];

                    CSL.M02.CM0201 ibl = new CSL.M02.CM0201();
                    ibl.FM020114(userSN, status);
                }
                #endregion

                #region M0202 债权投资
                //M0202 INIT，初始化
                if (busCode == "M0202INIT")
                {
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0202 ibl = new CSL.M02.CM0202();
                    res = ibl.FINIT(pageSize);
                }

                //M020209，排序
                if (busCode == "M020209")
                {
                    string sortStr = context.Request.Params["sortStr"];
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0202 ibl = new CSL.M02.CM0202();
                    res = ibl.FM020209(sortStr, pageSize);
                }

                //M020210，加载更多
                if (busCode == "M020210")
                {
                    int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);
                    string sortStr = context.Request.Params["sortStr"];


                    CSL.M02.CM0202 ibl = new CSL.M02.CM0202();
                    res = ibl.FM020210(pageFrom, pageSize, sortStr);
                }

                //M020201，用户信息
                if (busCode == "M020201")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0202 ibl = new CSL.M02.CM0202();
                    res = ibl.FM020201(userSN);
                }

                //M020202，取消的债权信息
                if (busCode == "M020202")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0202 ibl = new CSL.M02.CM0202();
                    res = ibl.FM020202(userSN);
                }

                //M020203，拒绝的债权信息
                if (busCode == "M020203")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0202 ibl = new CSL.M02.CM0202();
                    res = ibl.FM020203(userSN);
                }

                //M020204，预约中的债权信息
                if (busCode == "M020204")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0202 ibl = new CSL.M02.CM0202();
                    res = ibl.FM020204(userSN);
                }

                //M020205，已投资的债权信息
                if (busCode == "M020205")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0202 ibl = new CSL.M02.CM0202();
                    res = ibl.FM020205(userSN);
                }

                //M020206，已结案的债权信息
                if (busCode == "M020206")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0202 ibl = new CSL.M02.CM0202();
                    res = ibl.FM020206(userSN);
                }

                //M020207，获取备注信息
                if (busCode == "M020207")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0202 ibl = new CSL.M02.CM0202();
                    res = ibl.FM020207(userSN);
                }

                //M020208，获取备注信息
                if (busCode == "M020208")
                {
                    string userSN = context.Request.Params["userSN"];
                    string note = context.Request.Params["note"];

                    CSL.M02.CM0202 ibl = new CSL.M02.CM0202();
                    ibl.FM020208(userSN, note);
                }

                //M020211，修改服务状态
                if (busCode == "M020211")
                {
                    string userSN = context.Request.Params["userSN"];
                    string status = context.Request.Params["status"];

                    CSL.M02.CM0202 ibl = new CSL.M02.CM0202();
                    ibl.FM020211(userSN, status);
                }
                #endregion

                #region M0206 资产出售
                //M020601，初始化
                if (busCode == "M020601")
                {
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020601(pageSize);
                }

                //M020602，排序
                if (busCode == "M020602")
                {
                    string sortStr = context.Request.Params["sortStr"];
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020602(sortStr,pageSize);
                }

                //M020612，加载更多
                if (busCode == "M020612")
                {
                    string sortStr = context.Request.Params["sortStr"];
                    int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020612(sortStr,pageFrom,pageSize);
                }

                //M020603，出售方信息
                if (busCode == "M020603")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020603(userSN);
                }

                //M020604，获取已发布资产信息
                if (busCode == "M020604")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020604(userSN);
                }

                //M020605，获取预约中资产信息
                if (busCode == "M020605")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020605(userSN);
                }

                //M020606，获取已出售资产信息
                if (busCode == "M020606")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020606(userSN);
                }

                //M020607，获取备注信息
                if (busCode == "M020607")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020607(userSN);
                }

                //M020608，保存备注信息
                if (busCode == "M020608")
                {
                    string userSN = context.Request.Params["userSN"];
                    string note = context.Request.Params["note"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    ibl.FM020608(userSN, note);
                }

                //M020609，获取取消发布的资产信息
                if (busCode == "M020609")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020609(userSN);
                }

                //M020610，取消发布的资产
                if (busCode == "M020610")
                {
                    string assetsSN = context.Request.Params["assetsSN"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020610(assetsSN);
                }

                //M020611，获取拒绝预约的资产信息
                if (busCode == "M020611")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020611(userSN);
                }

                //M020613，修改服务状态
                if (busCode == "M020613")
                {
                    string userSN = context.Request.Params["userSN"];
                    string status = context.Request.Params["status"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    ibl.FM020613(userSN, status);
                }
                #endregion

                #region M0203 资产购买
                //INIT，初始化
                if (busCode == "M0203INIT")
                {
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0203 ibl = new CSL.M02.CM0203();
                    res = ibl.FM0203INIT(pageSize);
                }

                //M020303，排序
                if (busCode == "M020303")
                {
                    string sortStr = context.Request.Params["sortStr"];
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0203 ibl = new CSL.M02.CM0203();
                    res = ibl.FM020303(sortStr, pageSize);
                }

                //M020310，加载更多
                if (busCode == "M020310")
                {
                    string sortStr = context.Request.Params["sortStr"];
                    int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0203 ibl = new CSL.M02.CM0203();
                    res = ibl.FM020310(pageFrom, pageSize,sortStr);
                }

                //M020304，用户信息
                if (busCode == "M020304")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0203 ibl = new CSL.M02.CM0203();
                    res = ibl.FM020304(userSN);
                }

                //M020305，取消预约
                if (busCode == "M020305")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0203 ibl = new CSL.M02.CM0203();
                    res = ibl.FM020305(userSN);
                }

                //M020306，获取预约中资产信息
                if (busCode == "M020306")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0203 ibl = new CSL.M02.CM0203();
                    res = ibl.FM020306(userSN);
                }

                //M020307，获取已购买资产信息
                if (busCode == "M020307")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0203 ibl = new CSL.M02.CM0203();
                    res = ibl.FM020307(userSN);
                }

                //M020308，获取备注信息
                if (busCode == "M020308")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0203 ibl = new CSL.M02.CM0203();
                    res = ibl.FM020308(userSN);
                }

                //M020309，保存备注信息
                if (busCode == "M020309")
                {
                    string userSN = context.Request.Params["userSN"];
                    string note = context.Request.Params["note"];

                    CSL.M02.CM0203 ibl = new CSL.M02.CM0203();
                    ibl.FM020309(userSN, note);
                }

                //M020311，修改服务状态
                if (busCode == "M020311")
                {
                    string userSN = context.Request.Params["userSN"];
                    string status = context.Request.Params["status"];

                    CSL.M02.CM0203 ibl = new CSL.M02.CM0203();
                    ibl.FM020311(userSN, status);
                }
                #endregion 

                #region M0204 财务顾问
                //init，初始化
                if (busCode == "M0204INIT")
                {
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    res = ibl.FM0204INIT(pageSize);
                }

                //M020410，排序
                if (busCode == "M020410")
                {
                    string sortStr = context.Request.Params["sortStr"];
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    res = ibl.FM020410(sortStr, pageSize);
                }

                //M020411，加载更多
                if (busCode == "M020411")
                {
                    string sortStr = context.Request.Params["sortStr"];
                    int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    res = ibl.FM020411(pageFrom, pageSize, sortStr);
                }

                //M020401，用户信息
                if (busCode == "M020401")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    res = ibl.FM020401(userSN);
                }

                //M020402，获取拒绝预约信息
                if (busCode == "M020402")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    res = ibl.FM020402(userSN);
                }

                //M020403，获取审核中预约信息
                if (busCode == "M020403")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    res = ibl.FM020403(userSN);
                }

                //M020404，获取已审核信息
                if (busCode == "M020404")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    res = ibl.FM020404(userSN);
                }

                //M020405，获取已结案债权信息
                if (busCode == "M020405")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    res = ibl.FM020405(userSN);
                }

                //M020406，获取备注信息
                if (busCode == "M020406")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    res = ibl.FM020406(userSN);
                }

                //M020407，保存备注信息
                if (busCode == "M020407")
                {
                    string userSN = context.Request.Params["userSN"];
                    string note = context.Request.Params["note"];

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    ibl.FM020407(userSN, note);
                }

                //M020408，查看顾问服务申请时顾问基本信息
                if (busCode == "M020408")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    res = ibl.FM020408(userSN);
                }

                //M020409，通过或者取消申请
                if (busCode == "M020409")
                {
                    string userSN = context.Request.Params["userSN"];
                    string act = context.Request.Params["act"];
                    string auditNote = context.Request.Params["auditNote"];

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    res = ibl.FM020409(userSN, act, auditNote);
                }

                //M020412，修改服务状态
                if (busCode == "M020412")
                {
                    string userSN = context.Request.Params["userSN"];
                    string status = context.Request.Params["status"];

                    CSL.M02.CM0204 ibl = new CSL.M02.CM0204();
                    ibl.FM020412(userSN, status);
                }

                //M020610，取消发布的资产
                if (busCode == "M020610")
                {
                    string assetsSN = context.Request.Params["assetsSN"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020610(assetsSN);
                }

                //M020611，获取拒绝预约的资产信息
                if (busCode == "M020611")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    res = ibl.FM020611(userSN);
                }

                //M020613，修改服务状态
                if (busCode == "M020613")
                {
                    string userSN = context.Request.Params["userSN"];
                    string status = context.Request.Params["status"];

                    CSL.M02.CM0206 ibl = new CSL.M02.CM0206();
                    ibl.FM020613(userSN, status);
                }
                #endregion

                #region M0205 客户分配
                //M020501 初始化
                if(busCode == "M020501")
                {
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0205 ibl = new CSL.M02.CM0205();
                    res = ibl.FM020501(pageSize);
                }

                //M020506 根据客户经理筛选
                if (busCode == "M020506")
                {
                    string filterStr = context.Request.Params["filterStr"];
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0205 ibl = new CSL.M02.CM0205();
                    res = ibl.FM020506(filterStr, pageSize);
                }

                //M020503 滚动加载
                if (busCode == "M020503")
                {
                    string filterStr = context.Request.Params["filterStr"];
                    int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M02.CM0205 ibl = new CSL.M02.CM0205();
                    res = ibl.FM020503(filterStr, pageFrom, pageSize);
                }

                //M020502 获取客户经理列表
                if (busCode == "M020502")
                {
                    CSL.M02.CM0205 ibl = new CSL.M02.CM0205();
                    res = ibl.FM020502();
                }

                //M020505 客户经理列表排序
                if (busCode == "M020505")
                {
                    string sortStr = context.Request.Params["sortStr"];

                    CSL.M02.CM0205 ibl = new CSL.M02.CM0205();
                    res = ibl.FM020505(sortStr);
                }

                //M020504 新增或者修改客户经理
                if (busCode == "M020504")
                {
                    string userSN = context.Request.Params["userSN"];
                    string workNum = context.Request.Params["workNum"];

                    CSL.M02.CM0205 ibl = new CSL.M02.CM0205();
                    res = ibl.FM020504(userSN, workNum);
                }

                #endregion

            #endregion

            #region M06 财务管理
                #region M0601 用户账户
                //init，初始化
                if (busCode == "M0601INIT")
                {
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M06.CM0601 ibl = new CSL.M06.CM0601();
                    res = ibl.FM0601INIT(pageSize);
                }

                //M060102，排序
                if (busCode == "M060102")
                {
                    string sortStr = context.Request.Params["sortStr"];
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M06.CM0601 ibl = new CSL.M06.CM0601();
                    res = ibl.FM060102(sortStr, pageSize);
                }

                //M060101，加载更多
                if (busCode == "M060101")
                {
                    string sortStr = context.Request.Params["sortStr"];
                    int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M06.CM0601 ibl = new CSL.M06.CM0601();
                    res = ibl.FM060101(sortStr, pageFrom, pageSize);
                }

                //M060103，应收账单信息
                if (busCode == "M060103")
                {
                    string userSN = context.Request.Params["userSN"];

                    CSL.M06.CM0601 ibl = new CSL.M06.CM0601();
                    res = ibl.FM060103(userSN);
                }
                #endregion

                #region M0604 流水明细
                //init，初始化
                if (busCode == "M0604INIT")
                {
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M06.CM0604 ibl = new CSL.M06.CM0604();
                    res = ibl.FM0604INIT(pageSize);
                }

                //M060402，删选
                if (busCode == "M060402")
                {
                    string sortStr = context.Request.Params["sortStr"];
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M06.CM0604 ibl = new CSL.M06.CM0604();
                    res = ibl.FM060402(sortStr, pageSize);
                }

                //M060401，加载更多
                if (busCode == "M060401")
                {
                    string sortStr = context.Request.Params["sortStr"];
                    int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M06.CM0604 ibl = new CSL.M06.CM0604();
                    res = ibl.FM060401(sortStr, pageFrom, pageSize);
                }
                #endregion

                #region M0603 手工调账
                //初始化
                if(busCode == "M0603INIT")
                {
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M06.CM0603 ibl = new CSL.M06.CM0603();
                    res = ibl.FM0603INIT(pageSize);
                }

                //M060301 充值
                if(busCode == "M060301")
                {
                    string phone = context.Request.Params["phone"];
                    string name = context.Request.Params["name"];
                    decimal changeAmount = Convert.ToDecimal(context.Request.Params["changeAmount"]);
                    string changeReasonType = context.Request.Params["changeReasonType"];
                    string pwd = context.Request.Params["pwd"];
                    string note = context.Request.Params["note"];

                    CSL.M06.CM0603 ibl = new CSL.M06.CM0603();
                    res = ibl.FM060301(phone, name, changeAmount, changeReasonType, pwd, note);
                }

                //M060302 加载更多
                if (busCode == "M060302")
                {
                    int pageFrom = Convert.ToInt32(context.Request.Params["pageFrom"]);
                    int pageSize = Convert.ToInt32(context.Request.Params["pageSize"]);

                    CSL.M06.CM0603 ibl = new CSL.M06.CM0603();
                    res = ibl.FM060302(pageFrom, pageSize);
                }
                #endregion
            #endregion

            #region M09 系统设置
                #region M0906 债权投资
                //初始化
                if (busCode == "M0906INIT")
                {
                    CSL.M09.CM0906 ibl = new CSL.M09.CM0906();
                    res = ibl.FM0906INIT();
                }

                //M090601 保存参数设置
                if (busCode == "M090601")
                {
                    decimal minInvestMoneyAmount = Convert.ToDecimal(context.Request.Params["minInvestMoneyAmount"]);
                    decimal minDailyRate = Convert.ToDecimal(context.Request.Params["minDailyRate"]);
                    decimal maxDailyRate = Convert.ToDecimal(context.Request.Params["maxDailyRate"]);

                    CSL.M09.CM0906 ibl = new CSL.M09.CM0906();
                    ibl.FM090601(minInvestMoneyAmount, minDailyRate, maxDailyRate);
                }

                //M090602 保存收费设置
                if (busCode == "M090602")
                {
                    decimal openServerCost = Convert.ToDecimal(context.Request.Params["openServerCost"]);
                    decimal consultantReserveCost = Convert.ToDecimal(context.Request.Params["consultantReserveCost"]);
                    decimal financingReserveCost = Convert.ToDecimal(context.Request.Params["financingReserveCost"]);

                    CSL.M09.CM0906 ibl = new CSL.M09.CM0906();
                    ibl.FM090602(openServerCost, consultantReserveCost, financingReserveCost);
                }
                #endregion

                #region M0901 债权融资
                //初始化
                if (busCode == "M0901INIT")
                {
                    CSL.M09.CM0901 ibl = new CSL.M09.CM0901();
                    res = ibl.FM0901INIT();
                }

                //M090101 保存参数设置
                if (busCode == "M090101")
                {
                    int minDeadline = Convert.ToInt32(context.Request.Params["minDeadline"]);
                    int maxDeadline = Convert.ToInt32(context.Request.Params["maxDeadline"]);
                    decimal minFinancingMoneyAmount = Convert.ToDecimal(context.Request.Params["minFinancingMoneyAmount"]);

                    CSL.M09.CM0901 ibl = new CSL.M09.CM0901();
                    ibl.FM090101(minDeadline, maxDeadline, minFinancingMoneyAmount);
                }

                //M090102 保存收费设置
                if (busCode == "M090102")
                {
                    decimal openServerCost = Convert.ToDecimal(context.Request.Params["openServerCost"]);
                    decimal financePublishCost = Convert.ToDecimal(context.Request.Params["financePublishCost"]);
                    decimal investorRecommendCost = Convert.ToDecimal(context.Request.Params["investorRecommendCost"]);

                    CSL.M09.CM0901 ibl = new CSL.M09.CM0901();
                    ibl.FM090102(openServerCost, financePublishCost, investorRecommendCost);
                }

                //M090103 保存账单设置
                if (busCode == "M090103")
                {
                    decimal serviceRateDaily = Convert.ToDecimal(context.Request.Params["serviceRateDaily"]);
                    decimal minServiceRateTotel = Convert.ToDecimal(context.Request.Params["minServiceRateTotel"]);
                    decimal maxServiceRateTotel = Convert.ToDecimal(context.Request.Params["maxServiceRateTotel"]);

                    CSL.M09.CM0901 ibl = new CSL.M09.CM0901();
                    ibl.FM090103(serviceRateDaily, minServiceRateTotel, maxServiceRateTotel);
                }
                #endregion

                #region M0903 顾问参数
                //初始化
                if (busCode == "M0903INIT")
                {
                    CSL.M09.CM0903 ibl = new CSL.M09.CM0903();
                    res = ibl.FM0903INIT();
                }

                //M090301 保存收费设置
                if (busCode == "M090301")
                {
                    decimal openServerCost = Convert.ToDecimal(context.Request.Params["openServerCost"]);

                    CSL.M09.CM0903 ibl = new CSL.M09.CM0903();
                    ibl.FM090301(openServerCost);
                }
                #endregion

                #region M0907 资产购买
                //初始化
                if (busCode == "M0907INIT")
                {
                    CSL.M09.CM0907 ibl = new CSL.M09.CM0907();
                    res = ibl.FM0907INIT();
                }

                //M090701 保存收费设置
                if (busCode == "M090701")
                {
                    decimal openServerCost = Convert.ToDecimal(context.Request.Params["openServerCost"]);
                    decimal assetsReserveCost = Convert.ToDecimal(context.Request.Params["assetsReserveCost"]);

                    CSL.M09.CM0907 ibl = new CSL.M09.CM0907();
                    ibl.FM090701(openServerCost, assetsReserveCost);
                }
                #endregion

                #region M0910 资产出售
                //初始化
                if (busCode == "M0910INIT")
                {
                    CSL.M09.CM0910 ibl = new CSL.M09.CM0910();
                    res = ibl.FM0910INIT();
                }

                //M091001 保存参数设置
                if (busCode == "M091001")
                {
                    decimal minSellAmount = Convert.ToDecimal(context.Request.Params["minSellAmount"]);

                    CSL.M09.CM0910 ibl = new CSL.M09.CM0910();
                    ibl.FM091001(minSellAmount);
                }

                //M091002 保存收费设置
                if (busCode == "M091002")
                {
                    decimal publishAssetsCost = Convert.ToDecimal(context.Request.Params["publishAssetsCost"]);
                    decimal openServerCost = Convert.ToDecimal(context.Request.Params["openServerCost"]);

                    CSL.M09.CM0910 ibl = new CSL.M09.CM0910();
                    ibl.FM091002(publishAssetsCost, openServerCost);
                }

                //M091003 保存账单设置
                if (busCode == "M091003")
                {
                    decimal serviceRate = Convert.ToDecimal(context.Request.Params["serviceRate"]);

                    CSL.M09.CM0910 ibl = new CSL.M09.CM0910();
                    ibl.FM091003(serviceRate);
                }
                #endregion

                #region M0908 财务设置
                //初始化
                if (busCode == "M0908INIT")
                {
                    CSL.M09.CM0908 ibl = new CSL.M09.CM0908();
                    res = ibl.FM0908INIT();
                }

                //M090801 保存应收账款
                if (busCode == "M090801")
                {
                    int needPayDays = Convert.ToInt32(context.Request.Params["needPayDays"]);
                    decimal overdueRateDaily = Convert.ToDecimal(context.Request.Params["overdueRateDaily"]);

                    CSL.M09.CM0908 ibl = new CSL.M09.CM0908();
                    ibl.FM090801(needPayDays, overdueRateDaily);
                }

                //M090802 保存附加收费
                if (busCode == "M090802")
                {
                    decimal shortMessageCost = Convert.ToDecimal(context.Request.Params["shortMessageCost"]);

                    CSL.M09.CM0908 ibl = new CSL.M09.CM0908();
                    ibl.FM090802(shortMessageCost);
                }
                #endregion

                #region M0911 客服参数
                //初始化
                if (busCode == "M0911INIT")
                {
                    CSL.M09.CM0911 ibl = new CSL.M09.CM0911();
                    res = ibl.FM0911INIT();
                }

                //M091101 保存应收账款
                if (busCode == "M091101")
                {
                    int mode = Convert.ToInt32(context.Request.Params["mode"]);

                    CSL.M09.CM0911 ibl = new CSL.M09.CM0911();
                    ibl.FM091101(mode);
                }

                //M091102 保存服务器端
                if (busCode == "M091102")
                {
                    int webDelay = Convert.ToInt32(context.Request.Params["webDelay"]);
                    int maxCusSvrConnLevel = Convert.ToInt32(context.Request.Params["maxCusSvrConnLevel"]);
                    int maxUserConnNum = Convert.ToInt32(context.Request.Params["maxUserConnNum"]);
                    int cusSvrUserMaxAmount = Convert.ToInt32(context.Request.Params["cusSvrUserMaxAmount"]);

                    CSL.M09.CM0911 ibl = new CSL.M09.CM0911();
                    ibl.FM091102(webDelay, maxCusSvrConnLevel, maxUserConnNum, cusSvrUserMaxAmount);
                }

                //M091103 保存客户端倒计时
                if (busCode == "M091103")
                {
                    int countSizeLevel = Convert.ToInt32(context.Request.Params["countSizeLevel"]);
                    int showCountDownSizeLevel = Convert.ToInt32(context.Request.Params["showCountDownSizeLevel"]);

                    CSL.M09.CM0911 ibl = new CSL.M09.CM0911();
                    ibl.FM091103(countSizeLevel, showCountDownSizeLevel);
                } 
                #endregion

                #region M0909 VIP参数
                //初始化
                if (busCode == "M0909INIT")
                {
                    CSL.M09.CM0909 ibl = new CSL.M09.CM0909();
                    res = ibl.FM0909INIT();
                }

                //M090902 VIP1
                if (busCode == "M090902")
                {
                    int value = Convert.ToInt32(context.Request.Params["value"]);
                    decimal rate = Convert.ToDecimal(context.Request.Params["rate"]);

                    CSL.M09.CM0909 ibl = new CSL.M09.CM0909();
                    ibl.FM090902(value,rate);
                }

                //M090903 VIP2
                if (busCode == "M090903")
                {
                    int value = Convert.ToInt32(context.Request.Params["value"]);
                    decimal rate = Convert.ToDecimal(context.Request.Params["rate"]);

                    CSL.M09.CM0909 ibl = new CSL.M09.CM0909();
                    ibl.FM090903(value, rate);
                }

                //M090904 VIP3
                if (busCode == "M090904")
                {
                    int value = Convert.ToInt32(context.Request.Params["value"]);
                    decimal rate = Convert.ToDecimal(context.Request.Params["rate"]);

                    CSL.M09.CM0909 ibl = new CSL.M09.CM0909();
                    ibl.FM090904(value, rate);
                }

                //M090905 VIP4
                if (busCode == "M090905")
                {
                    int value = Convert.ToInt32(context.Request.Params["value"]);
                    decimal rate = Convert.ToDecimal(context.Request.Params["rate"]);

                    CSL.M09.CM0909 ibl = new CSL.M09.CM0909();
                    ibl.FM090905(value, rate);
                }

                //M090906 VIP5
                if (busCode == "M090906")
                {
                    int value = Convert.ToInt32(context.Request.Params["value"]);
                    decimal rate = Convert.ToDecimal(context.Request.Params["rate"]);

                    CSL.M09.CM0909 ibl = new CSL.M09.CM0909();
                    ibl.FM090906(value, rate);
                }

                //M090907 VIP6
                if (busCode == "M090907")
                {
                    int value = Convert.ToInt32(context.Request.Params["value"]);
                    decimal rate = Convert.ToDecimal(context.Request.Params["rate"]);

                    CSL.M09.CM0909 ibl = new CSL.M09.CM0909();
                    ibl.FM090907(value, rate);
                }

                //M090908 VIP7
                if (busCode == "M090908")
                {
                    int value = Convert.ToInt32(context.Request.Params["value"]);
                    decimal rate = Convert.ToDecimal(context.Request.Params["rate"]);

                    CSL.M09.CM0909 ibl = new CSL.M09.CM0909();
                    ibl.FM090908(value, rate);
                }
                #endregion

                #region M0904 VIP参数
                //初始化
                if (busCode == "M0904INIT")
                {
                    CSL.M09.CM0904 ibl = new CSL.M09.CM0904();
                    res = ibl.FM0904INIT();
                }

                //M090401
                if (busCode == "M090401")
                {
                    bool switchStatus = context.Request.Params["switchStatus"] == "1" ? true : false;

                    CSL.M09.CM0904 ibl = new CSL.M09.CM0904();
                    ibl.FM090401(switchStatus);
                }
                #endregion

                #region M0905 邮件接口
                //初始化
                if (busCode == "M0905INIT")
                {
                    CSL.M09.CM0905 ibl = new CSL.M09.CM0905();
                    res = ibl.FM0905INIT();
                }

                //M090501
                if (busCode == "M090501")
                {
                    string smtp = context.Request.Params["smtp"];
                    string userName = context.Request.Params["userName"];
                    string pwd = context.Request.Params["pwd"];
                    int port = Convert.ToInt32(context.Request.Params["port"]);
                    bool switchStatus = context.Request.Params["switchStatus"] == "1" ? true : false;

                    CSL.M09.CM0905 ibl = new CSL.M09.CM0905();
                    ibl.FM090501(smtp, userName, pwd, port, switchStatus);
                }

                #endregion

            #endregion

            #region M10 内部用户
                #region M1004 岗位分配
                if(busCode == "M1004INIT")
                {
                    CSL.M10.CM1004 ibl = new CSL.M10.CM1004();
                    res = ibl.FM1004INIT();
                }

                //加载员工数据
                if(busCode == "M100401")
                {
                    string roleSN = context.Request.Params["roleSN"];

                    CSL.M10.CM1004 ibl = new CSL.M10.CM1004();
                    res = ibl.FM100401(roleSN);
                }

                //删除内部用户
                if (busCode == "M100402")
                {
                    int internalUserSN = Convert.ToInt32(context.Request.Params["internalUserSN"]);
                    string roleSN = context.Request.Params["roleSN"];

                    CSL.M10.CM1004 ibl = new CSL.M10.CM1004();
                    res = ibl.FM100402(roleSN, internalUserSN);
                }

                //分配内部用户
                if (busCode == "M100403")
                {
                    int internalUserSN = Convert.ToInt32(context.Request.Params["internalUserSN"]);
                    string roleSN = context.Request.Params["roleSN"];

                    CSL.M10.CM1004 ibl = new CSL.M10.CM1004();
                    res = ibl.FM100403(roleSN, internalUserSN);
                }

                //添加岗位
                if(busCode == "M100404")
                {
                    string roleName = context.Request.Params["roleName"];
                    string note = context.Request.Params["note"];

                    CSL.M10.CM1004 ibl = new CSL.M10.CM1004();
                    res = ibl.FM100404(roleName, note);
                }

                //删除岗位
                if (busCode == "M100405")
                {
                    string roleSN = context.Request.Params["roleSN"];

                    CSL.M10.CM1004 ibl = new CSL.M10.CM1004();
                    res = ibl.FM100405(roleSN);
                }
                
                #endregion

            #endregion




                #region vanchbg
                switch (busCode)
                {
                    #region 登录
                    case "B0101":
                        if (opeType == "login")
                        {
                            string workNum = context.Request.Params["workNum"].ToString();
                            string pwd = context.Request.Params["pwd"].ToString();

                            //BLL.Login.Test bll1 = new BLL.Login.Test();
                            //bll1.Te();

                            BLL.Login.Login bll = new BLL.Login.Login();
                            res = bll.IsLogin(workNum, pwd).ToString();
                        }
                        else if (opeType == "loginOrNot")
                        {
                            BLL.Login.Login bll = new BLL.Login.Login();
                            res = bll.LoginOrNot();
                        }
                        break;
                    #endregion

                    #region 账户操作
                    case "opeAccount":
                        if (opeType == "getWorkNum")
                        {
                            BLL.OpeAccount.OpeAccount bll = new BLL.OpeAccount.OpeAccount();
                            res = bll.GetWorkNum();
                        }

                        if (opeType == "modifyPwd")
                        {
                            string internalUserId = HttpContext.Current.Session["internalUserId"].ToString();
                            string oldPwd = context.Request.Params["oldPwd"].ToString();
                            string newPwd = context.Request.Params["newPwd"].ToString();

                            BLL.OpeAccount.OpeAccount bll = new BLL.OpeAccount.OpeAccount();
                            res = bll.ModifyPwd(internalUserId, oldPwd, newPwd).ToString();
                        }
                        else if (opeType == "logout")
                        {
                            if(HttpContext.Current.Session["workNum"] == null)
                            {
                                return;
                            }
                            string workNum = HttpContext.Current.Session["workNum"].ToString();

                            BLL.OpeAccount.OpeAccount bll = new BLL.OpeAccount.OpeAccount();
                            bll.LogOut(workNum);
                        }
                        break;
                    #endregion

                    #region 判断是否已经登录和是否有权限
                    //res:
                    //  1:还未登录
                    //  2:没有权限
                    //  3:已登录且有权限
                    case "B0102":
                        if (opeType == "getPermision")
                        {
                            string menuCode = context.Request.Params["parentMenuCode"];

                            BLL.CommCtl bll = new BLL.CommCtl();
                            if (bll.IsLogin() == false)
                            {
                                res = "{\"isLogin\":\"false\"}";
                                break;
                            }

                            string jsonStr = bll.IsPermited(menuCode);

                            res = string.Format("{{\"isLogin\":\"true\",\"menu\":{0}}}", jsonStr);
                        }
                        break;
                    #endregion

                    #region 内部用户
                    #region 用户管理
                    case "B1001":
                        //获取内部用户信息列表
                        if (opeType == "getInternalUserList")
                        {
                            BLL.InternalUser.UserManagement bll = new BLL.InternalUser.UserManagement();
                            res = bll.GetInternalUserList();
                        }
                        //新增一个内部用户
                        else if (opeType == "addOneInternalUser")
                        {
                            string jsonStr = context.Request.Params["jsonStr"];
                            BLL.InternalUser.UserManagement bll = new BLL.InternalUser.UserManagement();
                            res = bll.AddNewInternalUser(jsonStr);
                        }
                        //修改内部用户信息
                        else if (opeType == "editInternalUserInfo")
                        {
                            string jsonStr = context.Request.Params["jsonStr"];
                            BLL.InternalUser.UserManagement bll = new BLL.InternalUser.UserManagement();
                            bll.EditInternalUserInfo(jsonStr);
                        }
                        //删除一个内部用户
                        else if (opeType == "deleteInternalUser")
                        {
                            string internalUserId = context.Request.Params["internalUserId"].ToString();
                            BLL.InternalUser.UserManagement bll = new BLL.InternalUser.UserManagement();
                            bll.DeleteInternalUser(internalUserId);
                        }
                        //启用或者暂停一个内部用户
                        else if (opeType == "enableOrDisableInternalUser")
                        {
                            string internalUserId = context.Request.Params["internalUserId"].ToString();
                            string status = context.Request.Params["status"].ToString();
                            BLL.InternalUser.UserManagement bll = new BLL.InternalUser.UserManagement();
                            bll.EnableOrDisableInternalUser(internalUserId, status);
                        }
                        //修改密码
                        else if (opeType == "changePassword")
                        {
                            string internalUserId = context.Request.Params["internalUserId"].ToString();
                            string newPassword = context.Request.Params["newPassword"].ToString();
                            BLL.InternalUser.UserManagement bll = new BLL.InternalUser.UserManagement();
                            bll.ChangePassword(internalUserId, newPassword);
                        }
                        break;
                    #endregion

                    #region 权限分配
                    case "B1002":
                        if (opeType == "getUserGroupStatisticsInfo")
                        {
                            BLL.InternalUser.PermissionAllocate bll = new BLL.InternalUser.PermissionAllocate();
                            res = bll.GetUserGroupStatisticsInfo();
                        }
                        else if (opeType == "addUserGroup")
                        {
                            BLL.InternalUser.PermissionAllocate bll = new BLL.InternalUser.PermissionAllocate();
                            string userGroupName = context.Request.Params["userGroupName"].ToString();
                            res = bll.AddUserGroup(userGroupName);
                        }
                        else if (opeType == "delUserGroup")
                        {
                            BLL.InternalUser.PermissionAllocate bll = new BLL.InternalUser.PermissionAllocate();
                            string userGroupId = context.Request.Params["userGroupId"].ToString();
                            bll.DelUserGroup(userGroupId);
                        }
                        else if (opeType == "getGroupOrunAllocateUser")
                        {
                            BLL.InternalUser.PermissionAllocate bll = new BLL.InternalUser.PermissionAllocate();
                            string userGroupId = context.Request.Params["userGroupId"].ToString();
                            res = bll.GetGroupAndUnAllocateUser(userGroupId);
                        }
                        else if (opeType == "addUserToGroup")
                        {
                            BLL.InternalUser.PermissionAllocate bll = new BLL.InternalUser.PermissionAllocate();
                            string userGroupId = context.Request.Params["userGroupId"].ToString();
                            string userGroupName = context.Request.Params["userGroupName"].ToString();
                            string internalUserId = context.Request.Params["internalUserId"].ToString();
                            bll.AddUserToGroup(userGroupId, userGroupName, internalUserId);
                        }
                        else if (opeType == "delUserFromGroup")
                        {
                            BLL.InternalUser.PermissionAllocate bll = new BLL.InternalUser.PermissionAllocate();
                            string internalUserId = context.Request.Params["internalUserId"].ToString();
                            bll.DelUserFromGroup(internalUserId);
                        }
                        break;
                    #endregion

                    #region 权限菜单
                    case "B1003":
                        //获得某用户组的权限菜单
                        if (opeType == "getGroupPermissionMenu")
                        {
                            BLL.InternalUser.PermissionMenu bll = new BLL.InternalUser.PermissionMenu();
                            string userGroupId = context.Request.Params["userGroupId"].ToString();
                            res = bll.GetGroupPermissionMenu(userGroupId);
                        }
                        //添加一个权限菜单到一个用户组中
                        else if (opeType == "addPermissionMenuToGroup")
                        {
                            BLL.InternalUser.PermissionMenu bll = new BLL.InternalUser.PermissionMenu();
                            string menuId = context.Request.Params["menuId"].ToString();
                            string userGroupId = context.Request.Params["userGroupId"].ToString();
                            bll.AddPermissionMenuToGroup(menuId, userGroupId);
                        }
                        //从一个用户组中删除一个权限菜单
                        else if (opeType == "delPermissionMenuFromGroup")
                        {
                            BLL.InternalUser.PermissionMenu bll = new BLL.InternalUser.PermissionMenu();
                            string menuId = context.Request.Params["menuId"].ToString();
                            string userGroupId = context.Request.Params["userGroupId"].ToString();
                            bll.DelPermissionMenuFromGroup(menuId, userGroupId);
                        }
                        break;

                    #endregion
                    #endregion

                    #region 在线客服
                    #region 客户端
                    case "K0101":
                        if (opeType == "startConnect")
                        {
                            //string timeIdentity = context.Request.Params["timeIdentity"].ToString();
                            //int intervalMinites = Convert.ToInt32(context.Request.Params["intervalMinites"]);
                            string timeIdentity = "";
                            int intervalMinites = 1;
                            string userNum = context.Request.Params["userNum"].ToString();
                            bool cusSvrBusy = Convert.ToBoolean(context.Request.Params["cusSvrBusy"]);
                            BLL.OnlineCusSvr.OnlineCusSvr bll = new BLL.OnlineCusSvr.OnlineCusSvr();
                            string isConn = bll.IsConnected(intervalMinites, timeIdentity, userNum, cusSvrBusy);

                            BLL.Config.OnlineCusSvrConfig bllConfig = new BLL.Config.OnlineCusSvrConfig();
                            string configData = bllConfig.GetInitData();

                            res = string.Format("{{\"isConn\":{0},\"configData\":{1}}}", isConn, configData);
                        }
                        else if (opeType == "sendMsg")
                        {
                            string msg = context.Request.Params["msg"].ToString();
                            string userNum = context.Request.Params["userNum"].ToString();
                            BLL.OnlineCusSvr.OnlineCusSvr bll = new BLL.OnlineCusSvr.OnlineCusSvr();
                            bll.UserSendMsg(userNum, msg);
                        }
                        else if (opeType == "receiveMsg")
                        {
                            string userNum = context.Request.Params["userNum"].ToString();
                            BLL.OnlineCusSvr.OnlineCusSvr bll = new BLL.OnlineCusSvr.OnlineCusSvr();
                            res = bll.UserReceiveMsg(userNum);
                        }
                        else if (opeType == "keepUserConnect")
                        {
                            string userNum = context.Request.Params["userNum"].ToString();
                            BLL.OnlineCusSvr.OnlineCusSvr bll = new BLL.OnlineCusSvr.OnlineCusSvr();
                            bll.KeepUserConnect(userNum);
                        }
                        break;
                    #endregion
                    #region 客服端
                    case "K0102":
                        if (opeType == "initCusSvr")
                        {
                            string cusSvrNum = context.Request.Params["cusSvrNum"].ToString();
                            BLL.OnlineCusSvr.OnlineCusSvr bll = new BLL.OnlineCusSvr.OnlineCusSvr();
                            bll.InitCusSvr(cusSvrNum);
                        }
                        else if (opeType == "sendMsg")
                        {
                            string userNum = context.Request.Params["userNum"].ToString();
                            string msg = context.Request.Params["msg"].ToString();
                            string cusSvrNum = context.Request.Params["cusSvrNum"].ToString();
                            BLL.OnlineCusSvr.OnlineCusSvr bll = new BLL.OnlineCusSvr.OnlineCusSvr();
                            bll.CusSvrSendMsg(cusSvrNum, userNum, msg);
                        }
                        else if (opeType == "receiveMsg")
                        {
                            string cusSvrNum = context.Request.Params["cusSvrNum"].ToString();
                            BLL.OnlineCusSvr.OnlineCusSvr bll = new BLL.OnlineCusSvr.OnlineCusSvr();
                            res = bll.CusSvrReceiveMsg(cusSvrNum);
                        }
                        break;
                    #endregion

                    #endregion

                    #region 参数设置
                    #region 在线客服参数设置
                    case "B0911":
                        if (opeType == "init")
                        {
                            BLL.Config.OnlineCusSvrConfig bll = new BLL.Config.OnlineCusSvrConfig();
                            res = bll.GetInitData();
                        }
                        else if (opeType == "saveM091101")
                        {
                            int mode = Convert.ToInt32(context.Request.Params["mode"]);
                            int levelSeconds = Convert.ToInt32(context.Request.Params["levelSeconds"]);

                            BLL.Config.OnlineCusSvrConfig bll = new BLL.Config.OnlineCusSvrConfig();
                            bll.SaveM091101(mode, levelSeconds);
                        }
                        else if (opeType == "saveM091102")
                        {
                            int webDelay = Convert.ToInt32(context.Request.Params["webDelay"]);
                            int maxCusSvrConnLevel = Convert.ToInt32(context.Request.Params["maxCusSvrConnLevel"]);
                            int maxUserConnNum = Convert.ToInt32(context.Request.Params["maxUserConnNum"]);
                            int cusSvrUserMaxAmount = Convert.ToInt32(context.Request.Params["cusSvrUserMaxAmount"]);

                            BLL.Config.OnlineCusSvrConfig bll = new BLL.Config.OnlineCusSvrConfig();
                            bll.SaveM091102(webDelay, maxCusSvrConnLevel, maxUserConnNum, cusSvrUserMaxAmount);
                        }
                        else if (opeType == "saveM091103")
                        {
                            int countSizeLevel = Convert.ToInt32(context.Request.Params["countSizeLevel"]);
                            int showCountDownSizeLevel = Convert.ToInt32(context.Request.Params["showCountDownSizeLevel"]);

                            BLL.Config.OnlineCusSvrConfig bll = new BLL.Config.OnlineCusSvrConfig();
                            bll.SaveM091103(countSizeLevel, showCountDownSizeLevel);
                        }
                        break;
                    #endregion
                    #endregion

                    #region 调试
                    case "debug":
                        switch (opeType)
                        {
                            case "showCusSvrTable":
                                BLL.OnlineCusSvr.OnlineCusSvr bll = new BLL.OnlineCusSvr.OnlineCusSvr();
                                bll.WriteToFile();
                                break;
                        }

                        break;
                    #endregion
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