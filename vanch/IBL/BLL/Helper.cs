using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;
using System.IO;

namespace BLL
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

        //jsonStr反序列化成类
        public static object Deserialize(string jsonStr,Type type)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Deserialize(jsonStr, type);
        }

        //写文件
        public static void WriteToFile(string filePath, string info)
        {
            FileStream fs = null;
            StreamWriter sw = null;

            try
            {
                fs = new FileStream(filePath, FileMode.Append);
                sw = new StreamWriter(fs);

                sw.WriteLine(info);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }

                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        //写日志
        public static void WriteLog(string info)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            string filePath = @"c:\debug\vanchBg.txt";

            try
            {
                fs = new FileStream(filePath, FileMode.Append);
                sw = new StreamWriter(fs);

                sw.WriteLine(info);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }

                if (fs != null)
                {
                    fs.Close();
                }
            }


        }

        /// <summary>
        /// From January 1 1970
        /// </summary>
        public static List<string> GenerateTimeIdentity(int intervalMinites)
        {
            double n = 12113;

            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
            int total1 = Convert.ToInt32((ts.TotalSeconds + (intervalMinites / 2.0) * 60) / (5 * 60));
            int total2 = Convert.ToInt32((ts.TotalSeconds - (intervalMinites / 2.0) * 60) / (5 * 60));
            List<string> timeIdentityList = new List<string>();
            timeIdentityList.Add((total1 * n).ToString().Substring(3,6));
            timeIdentityList.Add((total2 * n).ToString().Substring(3,6));

            return timeIdentityList;
        }
    }
}
