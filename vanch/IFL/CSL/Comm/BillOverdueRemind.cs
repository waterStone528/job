using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;

namespace IFL.Comm
{
    public class BillOverdueRemind
    {
        public void Send(object obj)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                SendByEmail(dbma1);
                dbma1.SubmitChanges();

                SendBySM(dbma1);
                dbma1.SubmitChanges();
            }
        }

        #region 验证短信开关，并发送短信
        private void SendBySM(DBMA1DataContext dbma1)
        {
            //短信开关打开
            if (dbma1.A034s.First().switchStatus == true)
            {
                decimal smFee = Convert.ToInt32(dbma1.A028s.First().shortMessageCost);

                var smList = dbma1.T000s.Where(c => c.sendDate == null).ToList();
                foreach(var t000 in smList)
                {
                    //余额充足
                    if (dbma1.F000s.Where(c => c.userSN == t000.userSN).First().balance >= smFee)
                    {
                        int seqid = Comm.C101.CF10112(t000.smSN, 1, 8);

                        //发送短信
                        string res = Comm.SM.SendSM(t000.phoneNum, t000.smContent, seqid, 1);
                        t000.sendDate = DateTime.Now;
                        t000.ifSuc = res == "0" ? true : false;

                        //扣款
                        C201.FC20154(dbma1, t000.userSN, smFee, "短信发送", t000.smSN);
                    }
                }
            }
        }
        #endregion

        #region 验证邮件开关，并发送邮箱
        private void SendByEmail(DBMA1DataContext dbma1)
        {
            //发送邮箱禁用
            var data = dbma1.A035s.FirstOrDefault();
            if (data == null)
            {
                return;
            }
            else if (data.switchStatus == false)
            {
                return;
            }

            var emailList = dbma1.T001s.Where(c => c.sendDate == null).ToList();
            foreach(var t001 in emailList)
            {
                bool ifBodyHtml = false;
                string fromEmailPwd = data.pwd.Trim();
                string fromEmail = data.userName.Trim();
                string toEmail = t001.email.Trim();
                string subject = t001.subjuct;
                string body = t001.emailContent;
                string smtp = data.SMTP;

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromEmail);   //源邮件地址
                message.To.Add(new MailAddress(toEmail));  //增加要发送的邮件地址,可以多个
                message.Subject = subject;                   //发送邮件的标题
                message.IsBodyHtml = ifBodyHtml;                    //发送邮件是否是html格式
                message.Body = body;                         //发送邮件的内容

                SmtpClient client = new SmtpClient();
                client.Host = smtp;
                client.Port = 25;
                client.Credentials = new System.Net.NetworkCredential(fromEmail, fromEmailPwd);

                client.Send(message);

                t001.sendDate = DateTime.Now;
            }
        }
        #endregion
    }
}
