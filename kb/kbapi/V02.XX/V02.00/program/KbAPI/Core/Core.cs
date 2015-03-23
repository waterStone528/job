using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KbAPI.API.Core
{
    /// <summary>
    ///     核心。
    ///     功能：
    ///         1、计算9种体质得分。
    ///         2、是和基本是的体质。
    /// </summary>
    public class Core
    {
        /// <summary>
        ///     计算中医9种体质得分
        /// </summary>
        /// <param name="p0103">测试题答案</param>
        /// <param name="gender">性别</param>
        /// <returns></returns>
        public Models.JsonClass.Core.ConstitutionScore CalConstitutionScore(Models.EF.P0103 p0103, bool gender)
        {
            int pinHZNum = 0, qiXZNum = 0, yangXZNum = 0, yinXZNum = 0, tanSZNum = 0, shiRZNum = 0, xueYZNum = 0, qiYZNum = 0, teBZNum = 0;
            int pinHZScoreOriginal = 0, qiXZScoreOriginal = 0, yangXZScoreOriginal = 0, yinXZScoreOriginal = 0, tanSZScoreOriginal = 0, shiRZScoreOriginal = 0, xueYZSCoreOriginal = 0, qiYZScoreOriginal = 0, teBZScoreOriginal = 0;

            if (p0103.q1 != null)
            {
                pinHZNum++;
                pinHZScoreOriginal += Convert.ToInt32(p0103.q1);
            }
            if (p0103.q2 != null)
            {
                pinHZNum++;
                pinHZScoreOriginal += 6 - Convert.ToInt32(p0103.q2);

                qiXZNum++;
                qiXZScoreOriginal += Convert.ToInt32(p0103.q2);
            }
            if (p0103.q3 != null)
            {
                pinHZNum++;
                pinHZScoreOriginal += 6 - Convert.ToInt32(p0103.q3);

                qiXZNum++;
                qiXZScoreOriginal += Convert.ToInt32(p0103.q3);
            }
            if (p0103.q4 != null)
            {
                pinHZNum++;
                pinHZScoreOriginal += 6 - Convert.ToInt32(p0103.q4);

                qiYZNum++;
                qiYZScoreOriginal += Convert.ToInt32(p0103.q4);
            }
            if (p0103.q5 != null)
            {
                pinHZNum++;
                pinHZScoreOriginal += 6 - Convert.ToInt32(p0103.q5);

                yangXZNum++;
                yangXZScoreOriginal += Convert.ToInt32(p0103.q5);
            }
            if (p0103.q6 != null)
            {
                pinHZNum++;
                pinHZScoreOriginal += Convert.ToInt32(p0103.q6);
            }
            if (p0103.q7 != null)
            {
                pinHZNum++;
                pinHZScoreOriginal += 6 - Convert.ToInt32(p0103.q7);
            }
            if (p0103.q8 != null)
            {
                pinHZNum++;
                pinHZScoreOriginal += 6 - Convert.ToInt32(p0103.q8);

                xueYZNum++;
                xueYZSCoreOriginal += Convert.ToInt32(p0103.q8);
            }
            if (p0103.q9 != null)
            {
                qiXZNum++;
                qiXZScoreOriginal += Convert.ToInt32(p0103.q9);
            }
            if (p0103.q10 != null)
            {
                qiXZNum++;
                qiXZScoreOriginal += Convert.ToInt32(p0103.q10);
            }
            if (p0103.q11 != null)
            {
                qiXZNum++;
                qiXZScoreOriginal += Convert.ToInt32(p0103.q11);
            }
            if (p0103.q12 != null)
            {
                qiXZNum++;
                qiXZScoreOriginal += Convert.ToInt32(p0103.q12);

                yangXZNum++;
                yangXZScoreOriginal += Convert.ToInt32(p0103.q12);
            }
            if (p0103.q13 != null)
            {
                qiXZNum++;
                qiXZScoreOriginal += Convert.ToInt32(p0103.q13);
            }
            if (p0103.q14 != null)
            {
                qiXZNum++;
                qiXZScoreOriginal += Convert.ToInt32(p0103.q14);
            }
            if (p0103.q15 != null)
            {
                yangXZNum++;
                yangXZScoreOriginal += Convert.ToInt32(p0103.q15);
            }
            if (p0103.q16 != null)
            {
                yangXZNum++;
                yangXZScoreOriginal += Convert.ToInt32(p0103.q16);
            }
            if (p0103.q17 != null)
            {
                yangXZNum++;
                yangXZScoreOriginal += Convert.ToInt32(p0103.q17);
            }
            if (p0103.q18 != null)
            {
                yangXZNum++;
                yangXZScoreOriginal += Convert.ToInt32(p0103.q18);
            }
            if (p0103.q19 != null)
            {
                yangXZNum++;
                yangXZScoreOriginal += Convert.ToInt32(p0103.q19);
            }
            if (p0103.q20 != null)
            {
                yinXZNum++;
                yinXZScoreOriginal += Convert.ToInt32(p0103.q20);
            }
            if (p0103.q21 != null)
            {
                yinXZNum++;
                yinXZScoreOriginal += Convert.ToInt32(p0103.q21);
            }
            if (p0103.q22 != null)
            {
                yinXZNum++;
                yinXZScoreOriginal += Convert.ToInt32(p0103.q22);
            }
            if (p0103.q23 != null)
            {
                yinXZNum++;
                yinXZScoreOriginal += Convert.ToInt32(p0103.q23);
            }
            if (p0103.q24 != null)
            {
                yinXZNum++;
                yinXZScoreOriginal += Convert.ToInt32(p0103.q24);
            }
            if (p0103.q25 != null)
            {
                yinXZNum++;
                yinXZScoreOriginal += Convert.ToInt32(p0103.q25);
            }
            if (p0103.q26 != null)
            {
                yinXZNum++;
                yinXZScoreOriginal += Convert.ToInt32(p0103.q26);
            }
            if (p0103.q27 != null)
            {
                yinXZNum++;
                yinXZScoreOriginal += Convert.ToInt32(p0103.q27);
            }
            if (p0103.q28 != null)
            {
                tanSZNum++;
                tanSZScoreOriginal += Convert.ToInt32(p0103.q28);
            }
            if (p0103.q29 != null)
            {
                tanSZNum++;
                tanSZScoreOriginal += Convert.ToInt32(p0103.q29);
            }
            if (p0103.q30 != null)
            {
                tanSZNum++;
                tanSZScoreOriginal += Convert.ToInt32(p0103.q30);
            }
            if (p0103.q31 != null)
            {
                tanSZNum++;
                tanSZScoreOriginal += Convert.ToInt32(p0103.q31);
            }
            if (p0103.q32 != null)
            {
                tanSZNum++;
                tanSZScoreOriginal += Convert.ToInt32(p0103.q32);
            }
            if (p0103.q33 != null)
            {
                tanSZNum++;
                tanSZScoreOriginal += Convert.ToInt32(p0103.q33);
            }
            if (p0103.q34 != null)
            {
                tanSZNum++;
                tanSZScoreOriginal += Convert.ToInt32(p0103.q34);
            }
            if (p0103.q35 != null)
            {
                tanSZNum++;
                tanSZScoreOriginal += Convert.ToInt32(p0103.q35);
            }
            if (p0103.q36 != null)
            {
                shiRZNum++;
                shiRZScoreOriginal += Convert.ToInt32(p0103.q36);
            }
            if (p0103.q37 != null)
            {
                shiRZNum++;
                shiRZScoreOriginal += Convert.ToInt32(p0103.q37);
            }
            if (p0103.q38 != null)
            {
                shiRZNum++;
                shiRZScoreOriginal += Convert.ToInt32(p0103.q38);
            }
            if (p0103.q39 != null)
            {
                shiRZNum++;
                shiRZScoreOriginal += Convert.ToInt32(p0103.q39);
            }
            if (p0103.q40 != null)
            {
                shiRZNum++;
                shiRZScoreOriginal += Convert.ToInt32(p0103.q40);
            }
            if(gender == true)  //男
            {
                if(p0103.q42 != null)
                {
                    shiRZNum++;
                    shiRZScoreOriginal += Convert.ToInt32(p0103.q42);
                }
            }
            if (gender == false)  //女
            {
                if (p0103.q41 != null)
                {
                    shiRZNum++;
                    shiRZScoreOriginal += Convert.ToInt32(p0103.q41);
                }
            }
            if (p0103.q43 != null)
            {
                xueYZNum++;
                xueYZSCoreOriginal += Convert.ToInt32(p0103.q43);
            }
            if (p0103.q44 != null)
            {
                xueYZNum++;
                xueYZSCoreOriginal += Convert.ToInt32(p0103.q44);
            }
            if (p0103.q45 != null)
            {
                xueYZNum++;
                xueYZSCoreOriginal += Convert.ToInt32(p0103.q45);
            }
            if (p0103.q46 != null)
            {
                xueYZNum++;
                xueYZSCoreOriginal += Convert.ToInt32(p0103.q46);
            }
            if (p0103.q47 != null)
            {
                xueYZNum++;
                xueYZSCoreOriginal += Convert.ToInt32(p0103.q47);
            }
            if (p0103.q48 != null)
            {
                xueYZNum++;
                xueYZSCoreOriginal += Convert.ToInt32(p0103.q48);
            }
            if (p0103.q49 != null)
            {
                qiYZNum++;
                qiYZScoreOriginal += Convert.ToInt32(p0103.q49);
            }
            if (p0103.q50 != null)
            {
                qiYZNum++;
                qiYZScoreOriginal += Convert.ToInt32(p0103.q50);
            }
            if (p0103.q51 != null)
            {
                qiYZNum++;
                qiYZScoreOriginal += Convert.ToInt32(p0103.q51);
            }
            if (p0103.q52 != null)
            {
                qiYZNum++;
                qiYZScoreOriginal += Convert.ToInt32(p0103.q52);
            }
            if (p0103.q53 != null)
            {
                qiYZNum++;
                qiYZScoreOriginal += Convert.ToInt32(p0103.q53);
            }
            if (p0103.q54 != null)
            {
                qiYZNum++;
                qiYZScoreOriginal += Convert.ToInt32(p0103.q54);
            }
            if (p0103.q55 != null)
            {
                teBZNum++;
                teBZScoreOriginal += Convert.ToInt32(p0103.q55);
            }
            if (p0103.q56 != null)
            {
                teBZNum++;
                teBZScoreOriginal += Convert.ToInt32(p0103.q55);
            }
            if (p0103.q57 != null)
            {
                teBZNum++;
                teBZScoreOriginal += Convert.ToInt32(p0103.q55);
            }
            if (p0103.q58 != null)
            {
                teBZNum++;
                teBZScoreOriginal += Convert.ToInt32(p0103.q55);
            }
            if (p0103.q59 != null)
            {
                teBZNum++;
                teBZScoreOriginal += Convert.ToInt32(p0103.q55);
            }
            if (p0103.q60 != null)
            {
                teBZNum++;
                teBZScoreOriginal += Convert.ToInt32(p0103.q55);
            }
            if (p0103.q61 != null)
            {
                teBZNum++;
                teBZScoreOriginal += Convert.ToInt32(p0103.q55);
            }

            int num = 4;

            Models.JsonClass.Core.ConstitutionScore constitutionScore = new Models.JsonClass.Core.ConstitutionScore();

            if (pinHZNum != 0)
            {
                constitutionScore.pinHZ = Convert.ToInt32(((decimal)(pinHZScoreOriginal - pinHZNum) / (pinHZNum * num)) * 100);
            }
            if (qiXZNum != 0)
            {
                constitutionScore.qiXZ = Convert.ToInt32(((decimal)(qiXZScoreOriginal - qiXZNum) / (qiXZNum * num)) * 100);
            }
            if (yinXZNum != 0)
            {
                constitutionScore.yinXZ = Convert.ToInt32(((decimal)(yinXZScoreOriginal - yinXZNum) / (yinXZNum * num)) * 100);
            }
            if (yangXZNum != 0)
            {
                constitutionScore.yangXZ = Convert.ToInt32(((decimal)(yangXZScoreOriginal - yangXZNum) / (yangXZNum * num)) * 100);
            }
            if (tanSZNum != 0)
            {
                constitutionScore.tanSZ = Convert.ToInt32(((decimal)(tanSZScoreOriginal - tanSZNum) / (tanSZNum * num)) * 100);
            }
            if (shiRZNum != 0)
            {
                constitutionScore.shiRZ = Convert.ToInt32(((decimal)(shiRZScoreOriginal - shiRZNum) / (shiRZNum * num)) * 100);
            }
            if (xueYZNum != 0)
            {
                constitutionScore.xueYZ = Convert.ToInt32(((decimal)(xueYZSCoreOriginal - xueYZNum) / (xueYZNum * num)) * 100);
            }
            if (qiYZNum != 0)
            {
                constitutionScore.qiYZ = Convert.ToInt32(((decimal)(qiYZScoreOriginal - qiYZNum) / (qiYZNum * num)) * 100);
            }
            if (teBZNum != 0)
            {
                constitutionScore.teBZ = Convert.ToInt32(((decimal)(teBZScoreOriginal - teBZNum) / (teBZNum * num)) * 100);
            }

            return constitutionScore;
        }

        /// <summary>
        ///     判断是和基本是的体质
        /// </summary>
        /// <param name="constitutionScore"></param>
        /// <returns></returns>
        public Dictionary<string, string> JudgeConstitution(Models.JsonClass.Core.ConstitutionScore constitutionScore)
        {
            string yes = string.Empty;
            string yesPossible = string.Empty;

            if (constitutionScore.qiXZ >= 40)
            {
                yes += "气虚质%";
            }
            else if (constitutionScore.qiXZ >= 30)
            {
                yesPossible += "气虚质%";
            }

            if (constitutionScore.yangXZ >= 40)
            {
                yes += "阳虚质%";
            }
            else if (constitutionScore.yangXZ >= 30)
            {
                yesPossible += "阳虚质%";
            }

            if (constitutionScore.yinXZ >= 40)
            {
                yes += "阴虚质%";
            }
            else if (constitutionScore.yinXZ >= 30)
            {
                yesPossible += "阴虚质%";
            }

            if (constitutionScore.tanSZ >= 40)
            {
                yes += "痰湿质%";
            }
            else if (constitutionScore.tanSZ >= 30)
            {
                yesPossible += "痰湿质%";
            }

            if (constitutionScore.shiRZ >= 40)
            {
                yes += "湿热质%";
            }
            else if (constitutionScore.shiRZ >= 30)
            {
                yesPossible += "湿热质%";
            }

            if (constitutionScore.xueYZ >= 40)
            {
                yes += "血瘀质%";
            }
            else if (constitutionScore.xueYZ >= 30)
            {
                yesPossible += "血瘀质%";
            }

            if (constitutionScore.qiYZ >= 40)
            {
                yes += "气郁质%";
            }
            else if (constitutionScore.qiYZ >= 30)
            {
                yesPossible += "气郁质%";
            }

            if (constitutionScore.teBZ >= 40)
            {
                yes += "特禀质%";
            }
            else if (constitutionScore.teBZ >= 30)
            {
                yesPossible += "特禀质%";
            }

            if (constitutionScore.pinHZ >= 60 && yes == string.Empty && yesPossible == string.Empty)
            {
                yes += "平和质%";
            }
            else if (constitutionScore.pinHZ >= 60 && yes == string.Empty)
            {
                yesPossible += "平和质%";
            }

            yes = yes != string.Empty ? yes.Substring(0, yes.Length - 1) : null;
            yesPossible = yesPossible != string.Empty ? yesPossible.Substring(0, yesPossible.Length - 1) : null;

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("yes", yes);
            dictionary.Add("yesPossible", yesPossible);
            return dictionary;
        }
    }
}