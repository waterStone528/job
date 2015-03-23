using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Script.Serialization;

namespace KbAPI.API
{
    /// <summary>
    ///     1、数据的接收和发送。
    ///     2、基本的数据过滤。
    ///     3、路由。
    /// </summary>
    public class Port : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string oprerateCode = GetOperateCode(context.Request.RawUrl);
            string ret = string.Empty;

            #region P01 用户
            if(oprerateCode == "P0101")  //登录
            {
                string userName = context.Request.Params["a"].ToString().Trim();

                Controllers.P01.P0101 p0101 = new Controllers.P01.P0101();
                ret = p0101.Login(userName);
            }
            if(oprerateCode == "P0105")  //上传用户的性别和出生年月
            {
                int userId = Convert.ToInt32(context.Request.Params["a"].ToString());
                bool gender = Convert.ToBoolean(context.Request.Params["b"].ToString());
                DateTime birthday = Convert.ToDateTime(context.Request.Params["c"].ToString());

                Controllers.P01.P0105 P0105 = new Controllers.P01.P0105();
                P0105.PostGenderAndBirthday(userId, gender, birthday);
            }
            if(oprerateCode == "P0115")  //获取用户体质类型（是和基本是）
            {
                int userId = Convert.ToInt32(context.Request.Params["a"].ToString());

                Controllers.P01.P0115 p0115 = new Controllers.P01.P0115();
                ret = p0115.GetConstitution(userId);
            }
            if(oprerateCode == "P0120")  //获得用户的9种体质得分记录
            {
                int userId = Convert.ToInt32(context.Request.Params["a"].ToString());
                List<Models.JsonClass.P01.ConstitutionScoreId> constitutionScoreId = (List<Models.JsonClass.P01.ConstitutionScoreId>)(DeJson(context.Request.Params["L"], typeof(List<Models.JsonClass.P01.ConstitutionScoreId>)));

                Controllers.P01.P0120 p0120 = new Controllers.P01.P0120();
                ret = p0120.GetConstitutionScoreHistory(userId, constitutionScoreId);
            }
            if (oprerateCode == "P0199")  //上传用户使用手机的基本环境（如经纬度）lh
            {
                int userId = Convert.ToInt32(context.Request.Params["a"].ToString());
                string latitude = context.Request.Params["b"].ToString().Trim();
                string longitude = context.Request.Params["c"].ToString().Trim();
                string privateIp = context.Request.Params["d"].ToString().Trim();
                string publicIp = context.Request.UserHostAddress;
                string modelStr = context.Request.Params["e"].ToString().Trim();
                string systemVersion = context.Request.Params["f"].ToString().Trim();
                int modelType = Convert.ToInt32(context.Request.Params["g"].Trim());
                string appVersion = context.Request.Params["h"].ToString().Trim();

                Controllers.P01.P0199 p0199 = new Controllers.P01.P0199();
                p0199.PostEnvironmentInfo(userId, latitude, longitude, privateIp, publicIp, modelStr, systemVersion, modelType, appVersion);
            }
            #endregion

            #region P02
            if(oprerateCode == "P0201")    //获取测试题目（文字）
            {
                int userId = Convert.ToInt32(context.Request.Params["a"].ToString());
                List<Models.JsonClass.P02.TestWordRequest> testWordRequest = (List<Models.JsonClass.P02.TestWordRequest>)(DeJson(context.Request.Params["L"].ToString(), typeof(List<Models.JsonClass.P02.TestWordRequest>)));

                Controllers.P02.P0201 p0201 = new Controllers.P02.P0201();
                ret = p0201.GetTestWord(userId, testWordRequest);

            }
            if(oprerateCode == "P0205")   //获取测试题目（图片）
            {
                List<Models.JsonClass.P02.TestPictureRequest> testPictureRequest = (List<Models.JsonClass.P02.TestPictureRequest>)(DeJson(context.Request.Params["L"].ToString(), typeof(List<Models.JsonClass.P02.TestPictureRequest>)));

                Controllers.P02.P0205 p0205 = new Controllers.P02.P0205();
                ret = p0205.GetTestPicture(testPictureRequest);
            }
            if(oprerateCode == "P0210")  //上传测试结果
            {
                int userId = Convert.ToInt32(context.Request.Params["a"].ToString());
                List<Models.JsonClass.P02.TestAnswer> testAnswer = (List<Models.JsonClass.P02.TestAnswer>)DeJson(context.Request.Params["L"].ToString(), typeof(List<Models.JsonClass.P02.TestAnswer>));

                Controllers.P02.P0210 p0210 = new Controllers.P02.P0210();
                p0210.PostTestAnswer(userId, testAnswer);
            }
            #endregion

            #region P03 食材
            if(oprerateCode == "P0301")  //获取适宜与避免的所有食材（文字）
            {
                int userId = Convert.ToInt32(context.Request.Params["a"].ToString());
                List<Models.JsonClass.P03.FoodTextRequst> foodTextRequst = (List<Models.JsonClass.P03.FoodTextRequst>)DeJson(context.Request.Params["L"].ToString(), typeof(List<Models.JsonClass.P03.FoodTextRequst>));

                Controllers.P03.P0301 p0301 = new Controllers.P03.P0301();
                ret = p0301.GetFoodText(userId, foodTextRequst);
            }
            if (oprerateCode == "P0305")  //获取食材（图片）
            {
                List<Models.JsonClass.P03.FoodPictureRequest> foodPictureRequest = (List<Models.JsonClass.P03.FoodPictureRequest>)DeJson(context.Request.Params["L"].ToString(), typeof(List<Models.JsonClass.P03.FoodPictureRequest>));

                Controllers.P03.P0305 p0305 = new Controllers.P03.P0305();
                ret = p0305.GetFoodPicture(foodPictureRequest);
            }
            #endregion
            #region P04 饮食常识
            if (oprerateCode == "P0401")  //获取饮食常识信息
            {
                List<Models.JsonClass.P04.GeneralKnowledgeRequest> generalKnowledgeRequest = (List<Models.JsonClass.P04.GeneralKnowledgeRequest>)DeJson(context.Request.Params["L"], typeof(List<Models.JsonClass.P04.GeneralKnowledgeRequest>));

                Controllers.P04.P0401 p0401 = new Controllers.P04.P0401();
                ret = p0401.GetGeneralKnowledge(generalKnowledgeRequest);
            }
            #endregion

            context.Response.ContentType = "application/json";
            context.Response.Write(ret);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///     从url中获取操作码
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>操作码</returns>
        private string GetOperateCode(string url)
        {
            string[] urlSplit = url.Split('/');
            return urlSplit[urlSplit.Length - 1];
        }

        private Object DeJson(string jsonStr, Type type)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Deserialize(jsonStr, type);
        }
    }
}