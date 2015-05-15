using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Back : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string BillNo = Request.QueryString["billno"];  //订单编号
        string Mercode = Request.QueryString["mercode"];  //商户号
        string Currency_type = Request.QueryString["Currency_type"]; //币种
        decimal Amount = Convert.ToDecimal(Request.QueryString["amount"]); //订单金额
        string Date = Request.QueryString["date"]; //订单日期
        string Succ = Request.QueryString["succ"]; //是否成功
        string Msg = Request.QueryString["msg"];
        string Attach = Request.QueryString["attach"];  //商户数据包
        string Ipsbillno = Request.QueryString["ipsbillno"]; //
        string Retencodetype = Request.QueryString["retencodetype"]; //交易返回接口加密方式
        string Signature = Request.QueryString["signature"];
        string Bankbillno = Request.QueryString["bankbillno"];

        IFL.P9.CP901 ifl = new IFL.P9.CP901();
        ifl.FP90110(BillNo, Amount, Succ, Ipsbillno, Bankbillno, Attach);
    }
}