using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Controllers.P01
{
    /// <summary>
    ///     获得用户的体质类型（是和基本是）
    /// </summary>
    public class P0115 : KbAPI.CL.Controller.Controller
    {
        public string GetConstitution(int userId)
        {
            Models.Business.P01.UserConstitutionInfo userConstitutionInfo = new Models.Business.P01.UserConstitutionInfo();
            Dictionary<string, string> constitution = userConstitutionInfo.GetConstitution(userId);
            var ret = new { a = constitution["yes"], b = constitution["yesPossible"] };
            return EnJson(ret);
        }
    }
}