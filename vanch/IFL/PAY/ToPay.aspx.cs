using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
//using OLPay.Biz;

public partial class _Default : System.Web.UI.Page
{
    ///<summary>商户号</summary>
    public string Mer_code = "000015";

    ///<summary>订单编号</summary>
    public string BillNo = "";

    ///<summary>订单金额</summary>
    public string Amount = "";

    ///<summary>订单日期</summary>
    public string Date = "20140811";

    ///<summary>币种</summary>
    public string Currency_Type = "RMB";

    ///<summary>支付卡种</summary>
    public string Gateway_Type = "01";  //01 —人民币借记卡

    /// <summary>语言</summary>
    public string Lang = "GB";

    /// <summary>支付结果成功返回的商户URL</summary>
    public string Merchanturl = "";

    ///<summary>支付结果失败返回的商户URL</summary>
    public string FailUrl = "";

    ///<summary>支付故障地址</summary>
    public string ErrorUrl = "";

    ///<summary>商户数据包</summary>
    public string Attach = "";

    ///<summary>订单支付接口加密方式</summary>
    public string OrderEncodeType = "5";

    /// <summary>交易返回接口加密方式</summary>
    ///16：交易返回采用Md5WithRsa的签名认证方式 
    ///17：交易返回采用Md5的摘要认证方式 
    public string RetEncodeType = "17";

    /// <summary>返回方式</summary>
    ///说明：IPS为商户提供了2种返回方式，分别为： Server to Server返回（必选） Browser返回（必选）该字段存放商户是否选择Server to Server返回方式 1：选择  0：不选择
    public string Rettype = "1";

    /// <summary>Server to Server 返回页面</summary>
    public string ServerUrl = "";

    ///<summary>商户证书</summary>
    public string SignMD5 = "GDgLwwdK270Qj1w4xho8lyTpRQZV9Jm5x4NwWOTThUa4fMhEBK9jOXFrKRT6xhlJuU2FEa89ov0ryyjfJuuPkcGzO5CeVx5ZIrkkt1aBlZV36ySvHOMcNv8rncRiy3DQ";

    /// <summary>直连选项 </summary>
    ///此选项决定商户是否采用直连 方式进行交易: 1:直连 
    public string DoCredit = "1";

    ///<summary>银行代码</summary>
    public string Bankco = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //GetBankCode(Mer_code, Mer_code + SignMD5);
        Amount = float.Parse(Request.QueryString["Amount"]).ToString("0.00");
        Attach = "系统测试";
        DoCredit = Request.QueryString["DoCredit"].Trim();
        Bankco = Request.QueryString["Bankco"].Trim();
        Gateway_Type = Request.QueryString["Gateway_Type"].Trim();
        //BillNo = Request.QueryString["Billno"].Trim();
        //Attach = Request.QueryString["Attach"].Trim();
        IFL.P9.CP901 ifl = new IFL.P9.CP901();
        string res = ifl.FP90102(Convert.ToDecimal(Amount), DoCredit, Bankco);
        BillNo = res.Split('%')[0];
        Attach = res.Split('%')[1];

        //Merchanturl = "http://www.vanch.cd/UIF/Pay/IPS/ReturnBridge.aspx?V=2.01";
        //FailUrl = "";
        //ServerUrl = "http://w.vanch.com:81/PAY/ReturnPay.aspx";
        Merchanturl = "http://www.vanch.co/UIF/Pay/IPS/ReturnBridge.aspx?V=2.01";
        FailUrl = "";
        ServerUrl = "http://w.vanch.com:82/PAY/ReturnPay.aspx";
        string Url = "http://pay.ips.net.cn/ipayment.aspx";
        SignMD5 = "billno" + BillNo + "currencytypeRMBamount" + Amount + "date" + Date + "orderencodetype" + OrderEncodeType + SignMD5;
        SignMD5 = FormsAuthentication.HashPasswordForStoringInConfigFile(SignMD5, "MD5").ToLower();

        //Post方式提交表单
        string postForm = "<form name=\"IPS\" id=\"IPS\" method=\"post\" action=\"" + Url + "\">";
        postForm += "<input type=\"hidden\" name=\"Mer_code\" value=\"" + Mer_code + "\" />";
        postForm += "<input type=\"hidden\" name=\"BillNo\" value=\"" + BillNo + "\" />";
        postForm += "<input type=\"hidden\" name=\"Amount\" value=\"" + Amount + "\" />";
        postForm += "<input type=\"hidden\" name=\"Date\" value=\"" + Date + "\" />";
        postForm += "<input type=\"hidden\" name=\"Currency_Type\" value=\"" + Currency_Type + "\" />";
        postForm += "<input type=\"hidden\" name=\"Gateway_Type\" value=\"" + Gateway_Type + "\" />";
        postForm += "<input type=\"hidden\" name=\"Lang\" value=\"" + Lang + "\" />";
        postForm += "<input type=\"hidden\" name=\"Merchanturl\" value=\"" + Merchanturl + "\" />";
        postForm += "<input type=\"hidden\" name=\"FailUrl\" value=\"" + FailUrl + "\" />";
        postForm += "<input type=\"hidden\" name=\"Attach\" value=\"" + Attach + "\" />";
        postForm += "<input type=\"hidden\" name=\"OrderEncodeType\" value=\"" + OrderEncodeType + "\" />";
        postForm += "<input type=\"hidden\" name=\"RetEncodeType\" value=\"" + RetEncodeType + "\" />";
        postForm += "<input type=\"hidden\" name=\"Rettype\" value=\"" + Rettype + "\" />";
        postForm += "<input type=\"hidden\" name=\"ServerUrl\" value=\"" + ServerUrl + "\" />";
        postForm += "<input type=\"hidden\" name=\"SignMD5\" value=\"" + SignMD5 + "\" />";
        postForm += "<input type=\"hidden\" name=\"DoCredit\" value=\"" + DoCredit + "\" />";
        postForm += "<input type=\"hidden\" name=\"Bankco\" value=\"" + Bankco + "\" />";
        postForm += "</form>";

        postForm += "<script language=\"javascript\"> window.parent.opener.P9.OrderSN = '" + BillNo + "';document.getElementById(\"IPS\").submit();</script>";

        Response.Write(postForm);
    }
}