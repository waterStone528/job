using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.Business.P01
{
    /// <summary>
    ///     用户的体质信息
    /// </summary>
    public class UserConstitutionInfo
    {
        private EF.KbEntities db = new EF.KbEntities();

        /// <summary>
        ///     获得用户的体质类型（是和基本是）
        /// </summary>
        /// <param name="userId">用户id</param>
        public Dictionary<string,string> GetConstitution(int userId)
        {
            EF.P0101 p0101 = db.P0101.Where(c => c.id == userId).First();
            string yes = p0101.constitutionType == null ? null : p0101.constitutionType.Trim();
            string yesPossible = p0101.possibleConstitutionType == null ? null : p0101.possibleConstitutionType.Trim();

            Dictionary<string, string> constitution = new Dictionary<string, string>();
            constitution.Add("yes", yes);
            constitution.Add("yesPossible", yesPossible);

            return constitution;
        }

        /// <summary>
        ///     获取用户9种体质得分的所有记录
        /// </summary>
        /// <param name="userId">用户id</param>
        public List<EF.P0102> GetConstitutionScoreHistory(int userId)
        {
            return db.P0102.Where(c => c.userId == userId).ToList();
        }
    }
}