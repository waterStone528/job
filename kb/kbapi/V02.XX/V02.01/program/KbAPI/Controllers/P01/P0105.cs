using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Controllers.P01
{
    public class P0105
    {
        public void PostGenderAndBirthday(int userId, bool gender, DateTime birthday)
        {
            Models.Business.P01.UserBasicInfo userBasicInfo = new Models.Business.P01.UserBasicInfo();
            userBasicInfo.PostGenderAndBirthday(userId, gender, birthday);
        }
    }
}