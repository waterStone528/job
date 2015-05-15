using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Config
{
    public class OnlineCusSvrConfig
    {
        private DAL.Config.OnlineCusSvrConfig dal = new DAL.Config.OnlineCusSvrConfig();

        //get config data
        public DAL.config GetConfigData()
        {
            return dal.GetConfigData();
        }

        //get config data
        public Dictionary<string, int> GetConfigDataG()
        {
            return dal.GetConfigDataG();
        }

        //get config page init config data
        public string GetInitData()
        {
            return dal.GetInitData();
        }

        //save M091101
        public void SaveM091101(int mode,int levelSeconds)
        {
            dal.SaveM091101(mode, levelSeconds);
        }

        //save M091102
        public void SaveM091102(int webDelay, int maxCusSvrConnLevel, int maxUserConnNum, int cusSvrUserMaxAmount)
        {
            dal.SaveM091102(webDelay, maxCusSvrConnLevel, maxUserConnNum, cusSvrUserMaxAmount);
        }

        //save M091103
        public void SaveM091103(int countSizeLevel, int showCountDownSizeLevel)
        {
            dal.SaveM091103(countSizeLevel, showCountDownSizeLevel);
        }
    }
}
