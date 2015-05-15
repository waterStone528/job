using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;

namespace DAL
{
    //一些辅助函数
    public class Helper
    {
        //类序列化成jsonStr
        public static string Serialize(object jsonObj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(jsonObj);
        }
    }
}
