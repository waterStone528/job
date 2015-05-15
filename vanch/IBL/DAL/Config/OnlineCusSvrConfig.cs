using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Config
{
    public class OnlineCusSvrConfig
    {
        //get config data
        public DAL.config GetConfigData()
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                config data = vdc.configs.First();

                return data;
            }
        }

        //get config data
        public Dictionary<string, int> GetConfigDataG()
        {
            using(VanchBgDataContext vdc = new VanchBgDataContext())
            {
                config data = vdc.configs.First();
                Dictionary<string, int> configData = new Dictionary<string, int>();
                configData.Add("cusSvrMode", Convert.ToInt32(data.cusSvrMode));
                configData.Add("cusSvrUserMaxAmount", Convert.ToInt32(data.cusSvrUserMaxAmount));
                configData.Add("cusSvrUserDelLevel", Convert.ToInt32(data.cusSvrUserDelLevel));
                configData.Add("maxCusSvrConnLevel", Convert.ToInt32(data.maxCusSvrConnLevel));
                configData.Add("maxUserConnNum", Convert.ToInt32(data.maxUserConnNum));
                configData.Add("countSizeLevel", Convert.ToInt32(data.countSizeLevel));
                configData.Add("showCountDownSizeLevel", Convert.ToInt32(data.showCountDownSizeLevel));
                //configData.Add("branchCountDownAjaxSizeLevel", Convert.ToInt32(data.branchCountDownAjaxSizeLevel));
                configData.Add("levelSeconds", Convert.ToInt32(data.levelSeconds));

                return configData;
            }
        }

        //get config page init config data
        public string GetInitData()
        {
            using(VanchBgDataContext vdc = new VanchBgDataContext())
            {
                var jsonObj = from c in vdc.configs
                              select new
                              {
                                  c.cusSvrMode,
                                  c.cusSvrUserMaxAmount,
                                  c.webDelay,
                                  c.maxCusSvrConnLevel,
                                  c.maxUserConnNum,
                                  c.countSizeLevel,
                                  c.showCountDownSizeLevel,
                                  c.levelSeconds
                              };

                return Helper.Serialize(jsonObj);
            }
        }

        //save M091101
        public void SaveM091101(int mode,int levelSeconds)
        {
            using(VanchBgDataContext vdc = new VanchBgDataContext())
            {
                config linqData = vdc.configs.First();
                linqData.cusSvrMode = mode;
                linqData.levelSeconds = levelSeconds;

                vdc.SubmitChanges();
            }
        }

        //save M091102
        public void SaveM091102(int webDelay, int maxCusSvrConnLevel, int maxUserConnNum, int cusSvrUserMaxAmount)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                config linqData = vdc.configs.First();
                linqData.webDelay = webDelay;
                linqData.maxCusSvrConnLevel = maxCusSvrConnLevel;
                linqData.maxUserConnNum = maxUserConnNum;
                linqData.cusSvrUserMaxAmount = cusSvrUserMaxAmount;

                vdc.SubmitChanges();
            }
        }

        //save M091103
        public void SaveM091103(int countSizeLevel, int showCountDownSizeLevel)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                config linqData = vdc.configs.First();
                linqData.countSizeLevel = countSizeLevel;
                linqData.showCountDownSizeLevel = showCountDownSizeLevel;

                vdc.SubmitChanges();
            }
        }
    }
}
