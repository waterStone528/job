using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFL.Comm;
using System.Web;

namespace IFL.P8
{
    //安全管理
    public class CP803
    {
        private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;

        #region P80306 初始化
        public string FP80306()
        {
            //判断是否登录
            if (session["userSN"] == null)
            {
                return "notLogin";
            }

            string userSN = session["userSN"].ToString();

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U000 u000 = dbma1.U000s.Where(c => c.userSN == userSN).First();

                //获取用户名
                string userName = u000.name;

                //获取短信费用
                decimal shortMessageFee = Convert.ToDecimal(dbma1.A028s.First().shortMessageCost);

                //获取手机号码
                string phone = u000.phone;

                //获取原邮箱
                string email = dbma1.U002s.Where(c => c.userSN == userSN).First().email;

                //获取通知设定
                var u003 = (from c in dbma1.U003s
                             where c.userSN == userSN
                             select new
                             {
                                 c.billGenerate_shortMessage,
                                 c.billGenerate_email,
                                 c.overdue_shortMessage,
                                 c.overdue_email,
                                 c.receiveReserve_shortMessage,
                                 c.receiveReserve_email
                             }).First();

                return string.Format("{{\"userName\":\"{0}\",\"shortMessageFee\":\"{1}\",\"phone\":\"{2}\",\"email\":\"{3}\",\"messageConfig\":{4},\"userSN\":\"{5}\"}}", userName, shortMessageFee, phone, email, C101.FC10107(u003), userSN);
            }
        }
        #endregion

        #region P80307 发送短信验证码
        /// <summary>
        /// 发送短信验证码
        /// 1:原手机号码;2:新手机号码;3:交易密码
        /// </summary>
        public bool FP80307(string phoneNum,string businessType)
        {
            string userSN = session["userSN"].ToString();

            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                if (businessType == "1")
                {
                    businessType = "原手机验证";
                }
                else if (businessType == "2")
                {
                    if (dbma1.U000s.Where(c => c.phone == phoneNum).FirstOrDefault() != null)
                    {
                        return false;
                    }

                    businessType = "新手机验证";
                }
                else if (businessType == "3")
                {
                    businessType = "修改交易密码";

                    phoneNum = dbma1.U000s.Where(c => c.userSN == userSN).First().phone;
                }

                //判断是否需要产生新的验证码
                string identifyingCode = IfIdentifyCodePastDue(dbma1, phoneNum, businessType);
                if (identifyingCode == null)
                {
                    //产生6位新的验证码
                    identifyingCode = GenerateIdentifyingCode();
                }

                //发送验证码
                string content = string.Format("您的验证码为{0}，有效时间为10分钟。", identifyingCode);
                if (Comm.SM.F001(dbma1, userSN, phoneNum, content, 1, false) != "0")
                {
                    return false;
                }

                //验证码记录写入到数据库中
                IdentifyingCodeInsertIntoDB(dbma1, phoneNum, identifyingCode, businessType);

                dbma1.SubmitChanges();

                return true;
            }
        }

        //判断是否需要新的验证码。验证码过期时间为20分钟
        private string IfIdentifyCodePastDue(DBMA1DataContext dbma1, string phoneNum, string businessType)
        {
            DateTime dt = DateTime.Now.AddMinutes(-20);

            U004 identifyingCodeRecord = (from c in dbma1.U004s
                                          where c.phone == phoneNum
                                            && c.businessType == businessType
                                            && c.sendTime > dt
                                          select c).FirstOrDefault();

            if (identifyingCodeRecord == null)
            {
                return null;
            }
            else
            {
                return identifyingCodeRecord.identifyingCode;
            }
        }

        //产生6位验证码
        private string GenerateIdentifyingCode()
        {
            Random r = new Random();
            return r.Next(100000, 999999).ToString();
        }

        //验证码记录写入到数据库中
        private void IdentifyingCodeInsertIntoDB(DBMA1DataContext dbma1, string phoneNum, string identifyingCode, string businessType)
        {
            //获取编码
            string tableName = "U004";
            int SNDigitLength = 6;
            string prefix = "UA";

            string max33SN = Comm.C101.FC10102(tableName, SNDigitLength, prefix);

            U004 u004 = new U004();
            u004.smIdentifyingCodeRecordSN = max33SN;
            u004.phone = phoneNum;
            u004.identifyingCode = identifyingCode;
            u004.sendTime = DateTime.Now;
            u004.businessType = businessType;

            dbma1.U004s.InsertOnSubmit(u004);
        }
        #endregion

        #region P80308 手机绑定
        //1:绑定成功;0:操作失败 不返回结果:2:原手机的验证码错误;3:新手机的验证码错误
        public int FP80308 (string oldPhone, string oldIC, string newPhone, string newIC)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //新手机是否被注册
                if (IfPhoneHasBeenReg(dbma1, newPhone) == true)
                {
                    return 0;
                }

                //原手机验证码是否正确
                if (IfIdentifyingCodeCorrect(dbma1, oldPhone, oldIC, "原手机验证") == false)
                {
                    return 2;
                }

                //新手机验证码是否正确
                if (IfIdentifyingCodeCorrect(dbma1, newPhone, newIC, "新手机验证") == false)
                {
                    return 3;
                }

                //修改U000手机
                U000 u000 = dbma1.U000s.Where(c => c.userSN == userSN).First();
                u000.phone = newPhone;

                //修改U002手机
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();
                u002.phone = newPhone;

                dbma1.SubmitChanges();

                return 1;
            }
        }
        #endregion

        #region P80309 登录密码
        //1:修改成功;0:操作失败 不返回结果:2:原密码不正确;
        public int FP80309(string oldPwd,string newPwd)
        {
            string userSN = session["userSN"].ToString();

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //检验原密码是否正确
                U000 u000 = dbma1.U000s.Where(c => c.userSN == userSN).First();
                string oldPwdMd5 = u000.pwd;
                if(C101.FC10104(oldPwd,oldPwdMd5) == false)
                {
                    return 2;
                }

                //修改密码
                string newPwdMd5 = C101.FC10103(newPwd);
                u000.pwd = newPwdMd5;
                u000.ifChangePwd = true;

                dbma1.SubmitChanges();

                return 1;
            }
        }
        #endregion

        #region P80310 交易密码
        //1:修改成功;0:操作失败 不返回结果:2:验证码不正确;
        public int FP80310(string IC,string newPwd)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                string phone = dbma1.U000s.Where(c => c.userSN == userSN).First().phone;

                //检测验证码
                if (IfIdentifyingCodeCorrect(dbma1, phone, IC, "修改交易密码") == false)
                {
                    return 2;
                }

                //修改交易密码
                string newPwdMd5 = C101.FC10103(newPwd);
                U003 u003 = dbma1.U003s.Where(c => c.userSN == userSN).First();
                u003.transactPwd = newPwdMd5;
                u003.ifChangeTransactPwd = true;

                dbma1.SubmitChanges();

                return 1;
            }
        }
        #endregion

        #region P80311 邮箱绑定
        //1:修改成功;0:操作失败 不返回结果:2:原邮箱不正确
        public int FP80311(string oldEmail, string newEamil)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U002 u002 = dbma1.U002s.Where(c => c.userSN == userSN).First();

                //验证原邮箱是否正确
                if (oldEmail != "")
                {
                    string email = u002.email.Trim();
                    if (email == null)
                    {
                        if (oldEmail != "")
                        {
                            return 2;
                        }
                    }
                    else if (email != oldEmail.Trim())
                    {
                        return 2;
                    }
                }

                //修改绑定邮箱
                u002.email = newEamil;
                u002.ifChangeEmail = true;

                dbma1.SubmitChanges();

                return 1;
            }
        }
        #endregion

        #region P80312 通知设定
        public void FP80312(string name, string value)
        {
            string userSN = session["userSN"].ToString();
            //string userSN = "U00001";

            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                U003 u003 = dbma1.U003s.Where(c => c.userSN == userSN).First();
                switch(name)
                {
                    case "AccountSMS":
                        u003.billGenerate_shortMessage = value == "1" ? true : false;
                        break;
                    case "AccountEmail":
                        u003.billGenerate_email = value == "1" ? true : false;
                        break;
                    case "TimeOutSMS":
                        u003.overdue_shortMessage = value == "1" ? true : false;
                        break;
                    case "TimeOutEmail":
                        u003.overdue_email = value == "1" ? true : false;
                        break;
                    case "BookSMS":
                        u003.receiveReserve_shortMessage = value == "1" ? true : false;
                        break;
                    case "BookEmail":
                        u003.receiveReserve_email = value == "1" ? true : false;
                        break;
                }

                dbma1.SubmitChanges();
            }
        }
        #endregion

        //检测验证码是否正确
        private bool IfIdentifyingCodeCorrect(DBMA1DataContext dbma1, string phoneNum, string identifyingCode, string businessType)
        {
            //找到最新一条验证码
            U004 identifyingCodeRecord = (from c in dbma1.U004s
                                          where c.phone == phoneNum
                                              //最新发送的验证码
                                          && c.sendTime == (dbma1.U004s.Where(o => o.phone == phoneNum && o.businessType == businessType).Max(p => p.sendTime))
                                          select c).FirstOrDefault();

            if (identifyingCodeRecord == null)
            {
                return false;
            }

            //验证码是否已经过期，过期时间为20分钟
            int deadlineMinites = 20;
            TimeSpan ts = DateTime.Now - identifyingCodeRecord.sendTime;
            if (ts.Minutes > deadlineMinites)
            {
                return false;
            }

            //验证验证码是否正确
            return identifyingCodeRecord.identifyingCode == identifyingCode ? true : false;
        }

        //手机号是否已经被注册
        private bool IfPhoneHasBeenReg(DBMA1DataContext dbma1, string phoneNum)
        {
            var linqData = dbma1.U000s.Where(c => c.phone == phoneNum).FirstOrDefault();

            return linqData == null ? false : true;
        }

    }
}
