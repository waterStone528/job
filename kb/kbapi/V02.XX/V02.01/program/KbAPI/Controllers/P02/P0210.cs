using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Controllers.P02
{
    /// <summary>
    ///     获取上传的测试结果，并进行相应的处理
    /// </summary>
    public class P0210
    {
        private Models.EF.KbEntities db = new Models.EF.KbEntities();

        public void PostTestAnswer(int userId, List<Models.JsonClass.P02.TestAnswer> testAnswer)
        {
            DateTime now = DateTime.Now;

            //获取已经测试的次数，并相应+1（P0101）
            int testedTimes = OpeTestTimes(userId);

            //保存记录到P0103和P0104
            Models.EF.P0103 p0103 = SaveToP0103P0104(userId, testedTimes, now, testAnswer);

            //计算9种体质得分，并保存到P0102
            Models.JsonClass.Core.ConstitutionScore constitutionScore = CalConstitutionScoreAndSave(userId, testedTimes, now, p0103);

            //获取是和基本是体质类型，并保存到P0101中
            GetConstitutionAndSave(userId, constitutionScore);

            db.SaveChanges();
        }

        /// <summary>
        ///     获取已经测试的次数，并相应+1（P0101）
        /// </summary>
        private int OpeTestTimes(int userId)
        {
            Models.EF.P0101 p0101 = db.P0101.Where(c => c.id == userId).First();
            int testedTimes = Convert.ToInt32(p0101.testedTimes);
            p0101.testedTimes = testedTimes + 1;
            return testedTimes;
        }

        /// <summary>
        ///     保存记录到P0103和P0104
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="testedTimes">测试次数，用于判断P0103是新添加记录还是更新记录</param>
        /// <param name="now">当前时间</param>
        /// <param name="testAnswer">测试答案</param>
        private Models.EF.P0103 SaveToP0103P0104(int userId, int testedTimes, DateTime now, List<Models.JsonClass.P02.TestAnswer> testAnswer)
        {
            Models.EF.P0103 p0103 = testedTimes == 0 ? new Models.EF.P0103() : db.P0103.Where(c => c.userId == userId).First();
            Models.EF.P0104 p0104 = new Models.EF.P0104();
            p0104.userId = userId;
            p0104.testTime = now;
            p0104.times = testedTimes + 1;

            Models.JsonClass.P02.TestAnswer testAnswerOne = testAnswer.Where(c => c.a == 1).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q1 = testAnswerOne.b;
                p0104.q1 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 2).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q2 = testAnswerOne.b;
                p0104.q2 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 3).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q3 = testAnswerOne.b;
                p0104.q3 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 4).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q4 = testAnswerOne.b;
                p0104.q4 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 5).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q5 = testAnswerOne.b;
                p0104.q5 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 6).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q6 = testAnswerOne.b;
                p0104.q6 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 7).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q7 = testAnswerOne.b;
                p0104.q7 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 8).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q8 = testAnswerOne.b;
                p0104.q8 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 9).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q9 = testAnswerOne.b;
                p0104.q9 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 10).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q10 = testAnswerOne.b;
                p0104.q10 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 11).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q11 = testAnswerOne.b;
                p0104.q11 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 12).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q12 = testAnswerOne.b;
                p0104.q12 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 13).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q13 = testAnswerOne.b;
                p0104.q13 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 14).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q14 = testAnswerOne.b;
                p0104.q14 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 15).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q15 = testAnswerOne.b;
                p0104.q15 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 16).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q16 = testAnswerOne.b;
                p0104.q16 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 17).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q17 = testAnswerOne.b;
                p0104.q17 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 18).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q18 = testAnswerOne.b;
                p0104.q18 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 19).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q19 = testAnswerOne.b;
                p0104.q19 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 20).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q20 = testAnswerOne.b;
                p0104.q20 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 21).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q21 = testAnswerOne.b;
                p0104.q21 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 22).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q22 = testAnswerOne.b;
                p0104.q22 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 23).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q23 = testAnswerOne.b;
                p0104.q23 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 24).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q24 = testAnswerOne.b;
                p0104.q24 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 25).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q25 = testAnswerOne.b;
                p0104.q25 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 26).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q26 = testAnswerOne.b;
                p0104.q26 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 27).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q27 = testAnswerOne.b;
                p0104.q27 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 28).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q28 = testAnswerOne.b;
                p0104.q28 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 29).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q29 = testAnswerOne.b;
                p0104.q29 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 30).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q30 = testAnswerOne.b;
                p0104.q30 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 31).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q31 = testAnswerOne.b;
                p0104.q31 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 32).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q32 = testAnswerOne.b;
                p0104.q32 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 33).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q33 = testAnswerOne.b;
                p0104.q33 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 34).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q34 = testAnswerOne.b;
                p0104.q34 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 35).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q35 = testAnswerOne.b;
                p0104.q35 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 36).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q36 = testAnswerOne.b;
                p0104.q36 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 37).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q37 = testAnswerOne.b;
                p0104.q37 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 38).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q38 = testAnswerOne.b;
                p0104.q38 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 39).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q39 = testAnswerOne.b;
                p0104.q39 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 40).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q40 = testAnswerOne.b;
                p0104.q40 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 41).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q41 = testAnswerOne.b;
                p0104.q41 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 42).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q42 = testAnswerOne.b;
                p0104.q42 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 43).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q43 = testAnswerOne.b;
                p0104.q43 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 44).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q44 = testAnswerOne.b;
                p0104.q44 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 45).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q45 = testAnswerOne.b;
                p0104.q45 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 46).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q46 = testAnswerOne.b;
                p0104.q46 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 47).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q47 = testAnswerOne.b;
                p0104.q47 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 48).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q48 = testAnswerOne.b;
                p0104.q48 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 49).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q49 = testAnswerOne.b;
                p0104.q49 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 50).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q50 = testAnswerOne.b;
                p0104.q50 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 51).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q51 = testAnswerOne.b;
                p0104.q51 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 52).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q52 = testAnswerOne.b;
                p0104.q52 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 53).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q53 = testAnswerOne.b;
                p0104.q53 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 54).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q54 = testAnswerOne.b;
                p0104.q54 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 55).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q55 = testAnswerOne.b;
                p0104.q55 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 56).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q56 = testAnswerOne.b;
                p0104.q56 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 57).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q57 = testAnswerOne.b;
                p0104.q57 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 58).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q58 = testAnswerOne.b;
                p0104.q58 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 59).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q59 = testAnswerOne.b;
                p0104.q59 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 60).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q60 = testAnswerOne.b;
                p0104.q60 = testAnswerOne.b;
            }

            testAnswerOne = testAnswer.Where(c => c.a == 61).FirstOrDefault();
            if (testAnswerOne != null)
            {
                p0103.q61 = testAnswerOne.b;
                p0104.q61 = testAnswerOne.b;
            }

            if (testedTimes == 0)
            {
                p0103.userId = userId;
                db.P0103.Add(p0103);
            }
            db.P0104.Add(p0104);

            return p0103;
        }

        /// <summary>
        ///     计算9种体质得分，并保存到P0102
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="testedTimes">已经测试的次数（不算本次）</param>
        /// <param name="now">当前时间</param>
        /// <param name="p0103">综合测试答案</param>
        /// <returns></returns>
        private Models.JsonClass.Core.ConstitutionScore CalConstitutionScoreAndSave(int userId, int testedTimes, DateTime now, Models.EF.P0103 p0103)
        {
            bool gender = Convert.ToBoolean(db.P0101.Where(c => c.id == userId).First().gender);
            Core.Core core = new Core.Core();
            Models.JsonClass.Core.ConstitutionScore constitutionScore = core.CalConstitutionScore(p0103, gender);

            //保存记录到P0102中
            Models.EF.P0102 p0102 = new Models.EF.P0102();
            p0102.userId = userId;
            p0102.pinHZScore = constitutionScore.pinHZ;
            p0102.qiXZScore = constitutionScore.qiXZ;
            p0102.yinXZScore = constitutionScore.yinXZ;
            p0102.yangXZScore = constitutionScore.yangXZ;
            p0102.tanSZScore = constitutionScore.tanSZ;
            p0102.shiRZScore = constitutionScore.shiRZ;
            p0102.xueYZScore = constitutionScore.xueYZ;
            p0102.qiYZScore = constitutionScore.qiYZ;
            p0102.teBZScore = constitutionScore.teBZ;
            p0102.times = testedTimes + 1;
            p0102.testTime = now;

            db.P0102.Add(p0102);

            return constitutionScore;
        }


        /// <summary>
        ///     获取是和基本是体质类型，并保存到P0101中
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="constitutionScore">9种体质的得分</param>
        private void GetConstitutionAndSave(int userId, Models.JsonClass.Core.ConstitutionScore constitutionScore)
        {
            Core.Core core = new Core.Core();
            Dictionary<string, string> constitution = core.JudgeConstitution(constitutionScore);

            Models.EF.P0101 p0101 = db.P0101.Where(c => c.id == userId).First();
            p0101.constitutionType = constitution["yes"];
            p0101.possibleConstitutionType = constitution["yesPossible"];
        }
    }
}