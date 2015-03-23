using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KbAPI.CL.Controller;

namespace KbAPI.API.Controllers.P01
{
    public class P0101 : Controller
    {
        public string Login(string userName)
        {
            Models.Business.P01.UserBasicInfo userBasicInfoObj = new Models.Business.P01.UserBasicInfo();

            Models.JsonClass.P01.UserBasicInfo userBasicInfoData = userBasicInfoObj.Login(userName);
            return EnJson(userBasicInfoData);
        }
    }
}