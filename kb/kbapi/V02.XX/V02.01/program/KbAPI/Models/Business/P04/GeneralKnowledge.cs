using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Models.Business.P04
{
    /// <summary>
    ///     饮食常识
    /// </summary>
    public class GeneralKnowledge
    {
        private EF.KbEntities db = new EF.KbEntities();

        public List<EF.P0401> GetGeneralKnowledge()
        {
            return db.P0401.ToList();
        }
    }
}