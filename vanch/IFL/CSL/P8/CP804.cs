using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;

namespace IFL.P8
{
    //注册、登录
    public class CP804
    {
        #region 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns>0:注册成功; 1:短信验证失败; 2:手机号码已经被注册； 3:身份证号码已经被注册; 4:身份核实失败; 5:获取户口地址失败; 6:手机号超过一天规定注册数 </returns>
        public string FP80401(string phoneNum,string pwd,string name,string idCard,string identifyingCode)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //验证手机号是否超过一天规定注册数
                if(dbma1.U007s.Where(c => c.phoneNum == phoneNum && c.regDate.Date == DateTime.Now.Date).Count() >= 3)
                {
                    return "6";
                }
                //添加记录到注册记录表 T002
                else
                {
                    U007 u007 = new U007();
                    u007.SN = Comm.C101.FC10102("U007", 8, "UG");
                    u007.phoneNum = phoneNum;
                    u007.idCard = idCard;
                    u007.regDate = DateTime.Now;
                    dbma1.U007s.InsertOnSubmit(u007);
                }
                

                //检测验证码是否正确
                if (IfIdentifyingCodeCorrect(dbma1, phoneNum, identifyingCode) == false)
                {
                    dbma1.SubmitChanges();

                    return "1";
                }

                //手机号码已经注册
                if (IfPhoneHasBeenReg(dbma1, phoneNum) == true)
                {
                    dbma1.SubmitChanges();

                    return "2";
                }

                //身份证号码已经注册
                if(IfIdCardHasBeenReg(dbma1,idCard) == true)
                {
                    dbma1.SubmitChanges();

                    return "3";
                }

                //身份核实
                //if (VerifyIdCard(name, idCard) == false)
                //{
                //    dbma1.SubmitChanges();

                //    return "4";
                //}

                //获取性别,true:man; false:woman
                bool gender = Convert.ToInt32(idCard.Substring(16, 1)) % 2 == 1 ? true : false;

                //获取出生日期
                int year = Convert.ToInt32(idCard.Substring(6, 4));
                int month = Convert.ToInt32(idCard.Substring(10, 2));
                int day = Convert.ToInt32(idCard.Substring(12, 2));
                DateTime birthday = new DateTime(year, month, day);

                //获取户口地址
                string registeredResidence = GetRegisteredResidence(dbma1, (idCard.Substring(0, 6)));
                if(registeredResidence == null)
                {
                    dbma1.SubmitChanges();

                    return "5";
                }

                //插入用户表 U000
                string max33SN = InsertIntoU00(dbma1,phoneNum,pwd,name);

                //插入用户信息表 U002
                InsertIntoU002(dbma1,max33SN,name,birthday,gender,registeredResidence,idCard,phoneNum);

                //插入服务表 U001
                InsertIntoU001(dbma1, max33SN);

                //插入余额表 F000
                InsertIntoF000(dbma1, max33SN);

                //插入安全管理表 U003
                InsertIntoU003(dbma1, max33SN, pwd);

                //插入客户分配表 B005（拥有客户数最少的客户经理分配给用户）
                AssignCM(dbma1, max33SN);

                dbma1.SubmitChanges();

                HttpContext.Current.Session["userSN"] = max33SN;

                return "0";
            }
        }

        //检测验证码是否正确
        private bool IfIdentifyingCodeCorrect(DBMA1DataContext dbma1, string phoneNum, string identifyingCode)
        {
            //找到最新一条验证码
            U004 identifyingCodeRecord = (from c in dbma1.U004s
                                          where c.phone == phoneNum
                                              //最新发送的验证码
                                          && c.sendTime == (dbma1.U004s.Where(o => o.phone == phoneNum && o.businessType == "注册账号").Max(p => p.sendTime))
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

        //身份证是否已经注册
        private bool IfIdCardHasBeenReg(DBMA1DataContext dbma1, string idCard)
        {
            var linqData = dbma1.U002s.Where(c => c.idCard == idCard).FirstOrDefault();

            return linqData == null ? false : true;
        }

        //身份核实
        private bool VerifyIdCard(string name, string idCard)
        {
            var service = new AAA.IdentifierService ();
            var cred = new AAA.Credential()
            {
                UserName = "nbfq_admin",
                Password = "nbfq123"
            };
            var request = new AAA.CheckRequest()
            {
                IDNumber = idCard,
                Name = name
            };
            var res = service.SimpleCheck(request, cred);

            T002 t002 = new T002();
            t002.SN = Comm.C101.FC10102("T002", 6, "T");
            t002.name = name;
            t002.idCard = idCard;
            t002.generateDate = DateTime.Now;
            t002.ifSuc = res.ResponseCode == 100 ? true : false;

            return res.ResponseCode == 100 ? true : false;
        }

        //获取户口地址
        private string GetRegisteredResidence(DBMA1DataContext dbma1, string num)
        {
            var registeredResidence = dbma1.A020s.Where(c => c.num == num).FirstOrDefault();
            if (registeredResidence == null)
            {
                return null;
            }
            else
            {
                return registeredResidence.address;
            }
        }

        //插入用户表
        private string InsertIntoU00(DBMA1DataContext dbma1, string phoneNum, string pwd, string name)
        {
            string tableName = "U000";
            int SNDigitLength = 5;
            string prefix = "U";
            string max33SN = Comm.C101.FC10102(tableName, SNDigitLength, prefix);
            string pwdMD5 = Comm.C101.FC10103(pwd);

            U000 u000 = new U000();
            u000.userSN = max33SN;
            u000.phone = phoneNum;
            u000.pwd = pwdMD5;
            u000.name = name;
            u000.registerDate = DateTime.Now;
            u000.lastLoginDate = DateTime.Now;
            u000.lastLoginIp = HttpContext.Current.Request.UserHostAddress;

            dbma1.U000s.InsertOnSubmit(u000);

            return max33SN;
        }

        //插入用户信息表
        private void InsertIntoU002(DBMA1DataContext dbma1,string max33SN, string name, DateTime birthday, bool gender, string registeredResidence, string idCard, string phoneNum)
        {
            U002 u002 = new U002();
            u002.userSN = max33SN;
            u002.name = name;
            u002.birthday = birthday;
            u002.gender = gender;
            u002.registeredResidence = registeredResidence;
            u002.idCard = idCard;
            u002.phone = phoneNum;

            dbma1.U002s.InsertOnSubmit(u002);
        }

        //插入服务表 U001
        private void InsertIntoU001(DBMA1DataContext dbma1,string max33SN)
        {
            U001 u001 = new U001();
            u001.userSN = max33SN;
            u001.creditRightInvestApplyStauts = 0;
            u001.creditRightFinancingApplyStatus = 0;
            u001.consultantApplyStatus = 0;
            u001.assetsPurchaseApplyStatus = 0;
            u001.assetsSellingApplyStatus = 0;

            dbma1.U001s.InsertOnSubmit(u001);
        }

        //插入余额表 F000
        private void InsertIntoF000(DBMA1DataContext dbma1,string max33SN)
        {
            F000 f000 = new F000();
            f000.userSN = max33SN;
            f000.balance = 0;
            f000.lastOperate = DateTime.Now;

            dbma1.F000s.InsertOnSubmit(f000);
        }

        //插入安全管理表 U003
        private void InsertIntoU003(DBMA1DataContext dbma1, string max33SN,string pwd)
        {
            U003 u003 = new U003();
            u003.userSN = max33SN;
            u003.transactPwd = IFL.Comm.C101.FC10103(pwd) ;

            dbma1.U003s.InsertOnSubmit(u003);
        }

        ////插入客户分配表 B005
        //private void InsertIntoB005(DBMA1DataContext dbma1,string userSN)
        //{
        //    B005 b005 = new B005();
        //    b005.userSN = userSN;

        //    dbma1.B005s.InsertOnSubmit(b005);
        //}

        //自动分配客户经理（拥有客户数最少的客户经理分配给用户）
        private void AssignCM(DBMA1DataContext dbma1,string userSN)
        {
            bool ifCanAssign = true;
            int? internalUserSN = null;

            var data1 = from c in dbma1.B003s.Where(c => c.roleTypeSN == "B01" && c.deleteDate == null)
                         join o in dbma1.B005s
                         on c.internalUserSN equals o.internalUserSN into p
                         from o in p.DefaultIfEmpty()
                         select new
                         {
                             c.internalUserSN,
                             o.userSN
                         };
            if(data1.Count() == 0)
            {
                ifCanAssign = false;
            }
            else
            {
                var data2 = (from c in data1
                             group c by c.internalUserSN into g
                             select new
                             {
                                 internalUserSN = g.Key,
                                 count = g.Count()
                             });

                var data3 = (from c in data2
                             where c.count == data2.Min(o => o.count)
                             select c).First();

                internalUserSN = data3.internalUserSN;
            }

            //添加记录
            B005 b005 = new B005();
            b005.userSN = userSN;
            if (ifCanAssign == true)
            {
                b005.internalUserSN = internalUserSN;
                b005.assignDate = DateTime.Now;
            }

            dbma1.B005s.InsertOnSubmit(b005);
        }

        #endregion

        #region 发送短信验证码
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        public bool FP80402(string phoneNum)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                if(dbma1.U000s.Where(c => c.phone == phoneNum).FirstOrDefault() != null)
                {
                    return false;
                }

                //判断是否需要产生新的验证码
                string identifyingCode = IfIdentifyCodePastDue(dbma1, phoneNum);
                if(identifyingCode == null)
                {
                    //产生6位新的验证码
                    identifyingCode = GenerateIdentifyingCode();
                }

                //发送验证码
                string content = string.Format("您的验证码为{0}，有效时间为10分钟。", identifyingCode);
                if (Comm.SM.F001(dbma1, "U00000", phoneNum, content, 1, false) != "0")
                {
                    dbma1.SubmitChanges();
                    return false;
                }

                //验证码记录写入到数据库中
                IdentifyingCodeInsertIntoDB(dbma1, phoneNum, identifyingCode);

                dbma1.SubmitChanges();
                return true;
            }
        }

        //判断是否需要新的验证码。验证码过期时间为20分钟
        private string IfIdentifyCodePastDue(DBMA1DataContext dbma1, string phoneNum)
        {
            DateTime dt = DateTime.Now.AddMinutes(-20);

            U004 identifyingCodeRecord = (from c in dbma1.U004s
                                          where c.phone == phoneNum
                                            && c.businessType == "注册账号"
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
        private void IdentifyingCodeInsertIntoDB(DBMA1DataContext dbma1, string phoneNum, string identifyingCode)
        {
            //获取编码
            string tableName = "U004";
            int SNDigitLength = 6;
            string prefix = "UA";
            string max33SN = Comm.C101.FC10102(tableName, SNDigitLength, prefix);
            int seqid = Comm.C101.CF10112(max33SN, 2, 6);

            U004 u004 = new U004();
            u004.smIdentifyingCodeRecordSN = max33SN;
            u004.phone = phoneNum;
            u004.identifyingCode = identifyingCode;
            u004.sendTime = DateTime.Now;
            u004.businessType = "注册账号";

            dbma1.U004s.InsertOnSubmit(u004);
        }
        #endregion

        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns>0：正确。1：无此手机号码。2：密码不正确。</returns>
        public int FP80403(string phoneNum,string pwd)
        {
            using(DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                var linqData = dbma1.U000s.Where(c => c.phone == phoneNum).FirstOrDefault();

                if(linqData == null)
                {
                    return 1;
                }

                if(Comm.C101.FC10104(pwd,linqData.pwd) == false)
                {
                    return 2;
                }

                linqData.lastLoginDate = DateTime.Now;
                linqData.lastLoginIp = HttpContext.Current.Request.UserHostAddress;

                HttpContext.Current.Session["userSN"] = linqData.userSN;

                return 0;
            }
        }
        #endregion

        #region 获取密码
        /// <summary>
        /// 获取密码
        /// </summary>
        /// <returns>0：通过邮箱发送密码成功。1：通过邮箱发送密码失败 2：通过短信发送密码成功。3：通过短信发送密码失败。 4：手机号码不存在。 5：获取密码次数达到上限。</returns>
        public string FP80404(string phoneNum)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                //手机号码是否存在
                if (IfExsitPhoneNum(dbma1, phoneNum) == false)
                {
                    return "{\"status\":\"4\"}";
                }

                //是否超过发送次数（一小时以内发送三次）
                if(IfGreaterThanMaxSendNum(dbma1, phoneNum) == true)
                {
                    return "{\"status\":\"5\"}";
                }

                Random rd = new Random();
                string resetPwd = rd.Next(100000, 999999).ToString();
                string content = string.Format("您的重置密码为{0}。登录后请及时修改密码。", resetPwd);
                string res = string.Empty;

                //如有邮箱信息，邮箱发送密码
                string sendEmailRes = IfSendSuccessByEmail(dbma1, phoneNum, resetPwd, content);
                if (sendEmailRes != "2")
                {
                    res = sendEmailRes;
                }
                //如无邮箱信息，短信发送密码
                else
                {
                    res = IfSendSuccessBySM(dbma1, phoneNum, resetPwd, content);
                }

                dbma1.SubmitChanges();
                return res;
            }
        }

        //手机号码是否存在
        private bool IfExsitPhoneNum(DBMA1DataContext dbma1, string phoneNum)
        {
            var linqData = dbma1.U000s.Where(c => c.phone == phoneNum).FirstOrDefault();

            return linqData != null ? true : false;
        }

        //是否超过发送次数（一小时以内发送三次）
        private bool IfGreaterThanMaxSendNum(DBMA1DataContext dbma1, string phoneNum)
        {
            DateTime dt = DateTime.Now.AddHours(-1);

            var linq = from c in dbma1.U005s
                       where c.phone == phoneNum
                        && c.sendTime > dt
                       select c;

            return linq.Count() >= 3 ? true : false;
        }

        //如有邮箱信息，邮箱发送密码
        //0：邮箱发送成功。2：用户没有邮箱信息，或者邮箱已经禁用。
        private string IfSendSuccessByEmail(DBMA1DataContext dbma1, string phoneNum, string resetPwd, string content)
        {
            //发送邮箱禁用
            var data = dbma1.A035s.FirstOrDefault();
            if (data == null)
            {
                return "2";
            }
            else if (data.switchStatus == false)
            {
                return "2";
            }

            //没有接受邮箱
            var linqData = dbma1.U002s.Where(c => c.phone == phoneNum && c.email != null).FirstOrDefault();
            if(linqData == null)
            {
                return "2";
            }

            Comm.C101.FC10106(dbma1,linqData.userSN,data.userName.Trim(), data.pwd.Trim(), linqData.email.Trim(), data.SMTP.Trim(),Convert.ToInt32(data.port), "凡奇网账号新密码", content);
         
            //Comm.C101.FC10106("metamorphosis5@163.com", "ningbo21152", linqData.email.Trim(),"",25, "新密码", content);
            SaveResetPwd(dbma1, phoneNum, resetPwd);

            return string.Format("{{\"status\":\"0\",\"email\":\"{0}\"}}", linqData.email.Trim());
        }

        //如无邮箱信息，短信发送密码
        //2：通过短信发送密码成功。3：通过短信发送密码失败。
        private string IfSendSuccessBySM(DBMA1DataContext dbma1, string phoneNum, string resetPwd, string content)
        {
            //发送密码
            if (Comm.SM.F001(dbma1, null, phoneNum, content, 1, false) != "0")
            {
                return "{\"status\":\"3\"}";
            }

            SaveResetPwd(dbma1, phoneNum, resetPwd);

            return "{\"status\":\"2\"}";
        }

        //保存重置密码
        private void SaveResetPwd(DBMA1DataContext dbma1, string phoneNum, string resetPwd)
        {
            string resetPwdMd5 = Comm.C101.FC10103(resetPwd);
            var linqData = dbma1.U000s.Where(c => c.phone == phoneNum).First();
            linqData.pwd = resetPwdMd5;

            string max33SN = Comm.C101.FC10102("U005", 6, "UB");
            U005 u005 = new U005();
            u005.senderPwdRecordSN = max33SN;
            u005.phone = phoneNum;
            u005.sendTime = DateTime.Now;
            dbma1.U005s.InsertOnSubmit(u005);
        }
        #endregion



    }
}

