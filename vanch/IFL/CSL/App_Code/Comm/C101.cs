using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.Net.Mail;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace IFL.Comm
{
    public class C101
    {
        private static List<char> notation33 = new List<char>(){'0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F','G','H','J','K','L','M','N','P','Q','R','S','T','U','V','W','X','Y'};

        /// <summary>
        /// 10进制转换为33进制
        /// </summary>
        public static string FC10101(int decimalNum, int digitNum)
        {
            int temp10;
            char temp33;
            string res = string.Empty;

            for(int i=0;i<digitNum;i++)
            {
                temp10 = decimalNum % 33;
                temp33 = notation33[temp10];
                res += temp33;

                decimalNum = decimalNum / 33;
            }

            return new string(res.Reverse().ToArray());
        }

        /// <summary>
        /// 获取数据库主键33进制编码
        /// </summary>
        public static string FC10102(string tableName,int SNDigitLength,string prefix)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                B002 b002 = dbma1.B002s.Where(c => c.tableName == tableName).First();
                b002.maxDecimalSN++;
                int maxDecimalSN = b002.maxDecimalSN;

                dbma1.SubmitChanges();

                return string.Format("{0}{1}", prefix, Comm.C101.FC10101(maxDecimalSN, SNDigitLength));
            }
        }

        #region md5
        /// <summary>
        /// 转换为md5加密字符串
        /// </summary>
        public static string FC10103(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// 加密字符串是否匹配
        /// </summary>
        public static bool FC10104(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = FC10103(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 发送验证码
        /// </summary>
        public static bool FC10105(string phoneNum, string content)
        {
            return true;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        public static void FC10106(string fromEmail, string pwd, string toEmail, string smtp, int port, string subject,string body)
        {
            //bool ifBodyHtml = false;
            //string fromEmailPwd = pwd;

            //MailMessage message = new MailMessage();
            //message.From = new MailAddress(fromEmail);   //源邮件地址
            //message.To.Add(new MailAddress(toEmail));  //增加要发送的邮件地址,可以多个
            //message.Subject = subject;                   //发送邮件的标题
            //message.IsBodyHtml = ifBodyHtml;                    //发送邮件是否是html格式
            //message.Body = body;                         //发送邮件的内容

            //SmtpClient client = new SmtpClient();
            //client.Host = "smtp." + message.From.Host;
            //client.Credentials = new System.Net.NetworkCredential(fromEmail, fromEmailPwd);

            //client.Send(message);

            bool ifBodyHtml = false;
            string fromEmailPwd = pwd;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromEmail);   //源邮件地址
            message.To.Add(new MailAddress(toEmail));  //增加要发送的邮件地址,可以多个
            message.Subject = subject;                   //发送邮件的标题
            message.IsBodyHtml = ifBodyHtml;                    //发送邮件是否是html格式
            message.Body = body;                         //发送邮件的内容

            SmtpClient client = new SmtpClient();
            //client.EnableSsl = false;
            //client.Port = port;
            client.Host = smtp;
            client.Port = 25;
            //client.Host = "smtp.163.com";
            client.Credentials = new System.Net.NetworkCredential(fromEmail, fromEmailPwd);

            client.Send(message);
        }

        /// <summary>
        /// 类序列化成jsonStr
        /// </summary>
        public static string FC10107(object jsonObj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(jsonObj);
        }

        /// <summary>
        /// jsonStr反序列化成类
        /// </summary>
        public static object FC10108(string jsonStr, Type type)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Deserialize(jsonStr, type);
        }

        #region CF10109 发送短信
        public static string CF10109(string phone, string content, int seqid, int priority)
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
            string cdkey = "0SDK-EAA-6688-JEXMT";
            string password = "260070";
            content = CF10110(content);
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

            string res = CF10111(XDocument.Parse(readStream.ReadToEnd().Trim()));

            // 完成后要关闭字符流，字符流底层的字节流将会自动关闭
            response.Close();
            readStream.Close();

            return res;
        }
        #endregion

        #region CF10110 转成utf8
        private static string CF10110(string inpStr)
        {
            StringBuilder sb = new StringBuilder();

            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(inpStr);

            for (int i = 0; i < byStr.Length; i++)
            {

                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return sb.ToString();
        }
        #endregion

        #region CF10111 获取短信返回值
        private static string CF10111(XDocument xdoc)
        {
            var lv1s = (from lv1 in xdoc.Descendants("error")
                        select lv1.Value).First().ToString();

            return lv1s;
        }
        #endregion

        #region CF10112 33进制转10进制
        public static int CF10112(string threeThreeStr,int start, int length)
        {
            List<char> threeThreeNum = threeThreeStr.Substring(start, length).ToList();
            threeThreeNum.Reverse();

            int res = 0;
            for(int i = 0;i < 6;i++)
            {
                res += Convert.ToInt32(threeThreeNum[i] - 48) * Convert.ToInt32(Math.Pow(33, i));
            }

            return res;
        }
        #endregion
    }
}
