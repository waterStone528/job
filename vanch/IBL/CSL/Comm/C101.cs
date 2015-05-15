using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;
using System.Security.Cryptography;

namespace CSL.Comm
{
    public class C101
    {
        private static List<char> notation33 = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y' };

        /// <summary>
        /// 10进制转换为33进制
        /// </summary>
        public static string FC10101(int decimalNum, int digitNum)
        {
            int temp10;
            char temp33;
            string res = string.Empty;

            for (int i = 0; i < digitNum; i++)
            {
                temp10 = decimalNum % 33;
                temp33 = notation33[temp10];
                res += temp33;

                decimalNum = decimalNum / 33;
            }

            return new string(res.Reverse().ToArray());
        }

        /// <summary>
        /// 获取数据库主键33进制编码
        /// </summary>
        public static string FC10102(string tableName, int SNDigitLength, string prefix)
        {
            using (DBMA1DataContext dbma1 = new DBMA1DataContext())
            {
                B002 b002 = dbma1.B002s.Where(c => c.tableName == tableName).First();
                b002.maxDecimalSN++;
                int maxDecimalSN = b002.maxDecimalSN;

                dbma1.SubmitChanges();

                return string.Format("{0}{1}", prefix, Comm.C101.FC10101(maxDecimalSN, SNDigitLength));
            }
        }

        #region md5
        /// <summary>
        /// 转换为md5加密字符串
        /// </summary>
        public static string FC10103(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// 加密字符串是否匹配
        /// </summary>
        public static bool FC10104(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = FC10103(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 类序列化成jsonStr
        /// </summary>
        public static string FC10107(object jsonObj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(jsonObj);
        }

        /// <summary>
        /// jsonStr反序列化成类
        /// </summary>
        public static object FC10108(string jsonStr, Type type)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Deserialize(jsonStr, type);
        }
    }
}
