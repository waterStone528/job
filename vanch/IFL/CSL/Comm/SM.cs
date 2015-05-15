using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Xml.Linq;

namespace IFL.Comm
{
    public class SM
    {
        #region F001 验证短信开关，并发送短信
        //unavailable：短信开关关闭。balanceNotEnough:余额不足。0：发送短信成功。其他：发送失败状态码。
        public static string F001(DBMA1DataContext dbma1, string userSN, string phone, string content, int priority, bool ifNeedPay)
        {
            try
            {
                //验证短信开关是否打开
                if (dbma1.A034s.First().switchStatus != true)
                {
                    return "unavailable";
                }

                decimal smFee = Convert.ToDecimal(dbma1.A028s.First().shortMessageCost);

                //余额不足
                if (ifNeedPay == true)
                {
                    if (dbma1.F000s.Where(c => c.userSN == userSN).First().balance < smFee)
                    {
                        return "balanceNotEnough";
                    }
                }

                string smSN = C101.FC10102("T000", 8, "T");
                int seqid = Comm.C101.CF10112(smSN, 1, 8);

                //发送短信
                string res = SendSM(phone, content, seqid, priority);

                //添加短信记录表
                T000 t000 = new T000();
                t000.smSN = smSN;
                t000.userSN = userSN;
                t000.phoneNum = phone;
                t000.smContent = content;
                t000.generateDate = DateTime.Now;
                t000.sendDate = DateTime.Now;
                t000.ifSuc = res == "0" ? true : false;
                dbma1.T000s.InsertOnSubmit(t000);

                //扣款
                if (ifNeedPay == true && res == "0")
                {
                    C201.FC20154(dbma1, userSN, smFee, "短信发送", t000.smSN);
                }

                return res;
            }
            catch(Exception ex)
            {
                return "";
            }
        }
        #endregion

        #region 发送短信
        public static string SendSM(string phone, string content, int seqid, int priority)
        {
            string url = "http://sdk4report.eucp.b2m.cn:8080/sdkproxy/sendsms.action";

            // 创建 WebRequest 对象，WebRequest 是抽象类，定义了请求的规定,
            // 可以用于各种请求，例如：Http, Ftp 等等。
            // HttpWebRequest 是 WebRequest 的派生类，专门用于 Http
            System.Net.HttpWebRequest request
                = System.Net.HttpWebRequest.Create(url) as System.Net.HttpWebRequest;

            // 请求的方式通过 Method 属性设置 ，默认为 GET
            // 可以将 Method 属性设置为任何 HTTP 1.1 协议谓词：GET、HEAD、POST、PUT、DELETE、TRACE 或 OPTIONS。
            request.Method = "POST";

            // 输入 POST 的数据.
            string cdkey = "6SDK-EMY-6688-KEUMQ";
            string password = "487759";
            content = C101.CF10110(content);
            string postData = string.Format("cdkey={0}&password={1}&phone={2}&message={3}&addserial=&seqid={4}&smspriority={5}", cdkey, password, phone, content, seqid.ToString(), priority);

            // 拼接成请求参数串，并进行编码，成为字节
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byte1 = encoding.GetBytes(postData);

            // 设置请求的参数形式
            request.ContentType = "application/x-www-form-urlencoded";

            // 设置请求参数的长度.
            request.ContentLength = byte1.Length;

            // 取得发向服务器的流
            System.IO.Stream newStream = request.GetRequestStream();

            // 使用 POST 方法请求的时候，实际的参数通过请求的 Body 部分以流的形式传送
            newStream.Write(byte1, 0, byte1.Length);

            // 完成后，关闭请求流.
            newStream.Close();

            // GetResponse 方法才真的发送请求，等待服务器返回
            System.Net.HttpWebResponse response
                = (System.Net.HttpWebResponse)request.GetResponse();

            // 然后可以得到以流的形式表示的回应内容
            System.IO.Stream receiveStream
                = response.GetResponseStream();

            // 还可以将字节流包装为高级的字符流，以便于读取文本内容 
            // 需要注意编码
            System.IO.StreamReader readStream
                = new System.IO.StreamReader(receiveStream, Encoding.UTF8);

            string res = GetSendRes(XDocument.Parse(readStream.ReadToEnd().Trim()));

            // 完成后要关闭字符流，字符流底层的字节流将会自动关闭
            response.Close();
            readStream.Close();

            return res;
        }
        #endregion

        #region 获取短信返回值
        private static string GetSendRes(XDocument xdoc)
        {
            var lv1s = (from lv1 in xdoc.Descendants("error")
                        select lv1.Value).First().ToString();

            return lv1s;
        }
        #endregion
    }
}
