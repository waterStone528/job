using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.Business.P01
{
    /// <summary>
    ///     用户基本信息
    /// </summary>
    public class UserBasicInfo
    {
        private EF.KbEntities db = new EF.KbEntities();

        /// <summary>
        ///     注册，并获取用户基本信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户基本信息</returns>
        public JsonClass.P01.UserBasicInfo Login(string userName)
        {
            var p0101 = db.P0101.Where(c => c.phoneLoginEmail == userName).FirstOrDefault();

            JsonClass.P01.UserBasicInfo userBasicInfo = new JsonClass.P01.UserBasicInfo();
            //已经注册
            if (p0101 != null)
            {
                userBasicInfo.a = p0101.id;
                userBasicInfo.b = p0101.gender;
                userBasicInfo.c = p0101.birthday;

                var p0102 = db.P0102.Where(c => c.userId == p0101.id).FirstOrDefault();
                userBasicInfo.d = p0102 == null ? false : true;

                return userBasicInfo;
            }
            //还未注册
            else
            {
                EF.P0101 p0101New = new EF.P0101();
                p0101New.phoneLoginEmail = userName;
                p0101New.nowQuestionGroupId = 0;
                p0101New.testTimes = 0;
                p0101New.testedTimes = 0;
                p0101New.registerTime = DateTime.Now;
                p0101New.lastUseTime = DateTime.Now;

                db.P0101.Add(p0101New);
                db.SaveChanges();

                userBasicInfo.a = p0101New.id;
                userBasicInfo.b = null;
                userBasicInfo.c = null;
                userBasicInfo.d = false;

                return userBasicInfo;
            }
        }

        /// <summary>
        ///     上传性别和生日
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gender"></param>
        /// <param name="birthday"></param>
        public void PostGenderAndBirthday(int userId, bool gender, DateTime birthday)
        {
            var p0101 = db.P0101.Where(c => c.id == userId).First();
            p0101.gender = gender;
            p0101.birthday = birthday;

            db.SaveChanges();
        }

        /// <summary>
        ///     上传用户使用手机的基本环境（如经纬度等）
        ///     注：会更新用户最后使用时间。
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="latitude">经度</param>
        /// <param name="longitude">纬度</param>
        /// <param name="ip">ip</param>
        /// <param name="modelStr">终端型号字符串</param>
        /// <param name="systemVersion">系统版本</param>
        /// <param name="modelType">终端型号类型</param>
        /// <param name="appVersion">app版本</param>
        public void PostEnvironmentInfo(int userId, string latitude, string longitude, string privateIp, string publicIp, string modelStr, string systemVersion, int modelType, string appVersion)
        {
            DateTime dt = DateTime.Now;

            //插入数据
            EF.P0109 p0109 = new EF.P0109();
            p0109.userId = userId;
            p0109.latitude = latitude;
            p0109.longitude = longitude;
            p0109.privateIp = privateIp;
            p0109.publicIp = publicIp;
            p0109.modelStr = modelStr;
            p0109.systemVersion = systemVersion;
            p0109.modelType = modelType;
            p0109.appVersion = appVersion;
            p0109.useTime = dt;
            db.P0109.Add(p0109);

            //更新最后使用时间
            var p0101 = db.P0101.Where(c => c.id == userId).First();
            p0101.lastUseTime = dt;

            db.SaveChanges();
        }
    }
}