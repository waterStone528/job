using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Controllers.P01
{
    //上传用户使用手机的基本环境（如经纬度等）
    public class P0199
    {
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
            Models.Business.P01.UserBasicInfo userBasicInfo = new Models.Business.P01.UserBasicInfo();
            userBasicInfo.PostEnvironmentInfo(userId, latitude, longitude, privateIp, publicIp, modelStr, systemVersion, modelType, appVersion);
        }
    }
}