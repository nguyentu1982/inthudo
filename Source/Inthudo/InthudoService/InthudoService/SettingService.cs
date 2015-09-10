using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InthudoService
{
    public class SettingService:ServiceBase, ISettingService
    {
        static readonly ISettingDao settingDao = factory.SettingDao;

        public bool GetBoolSetting(string name)
        {
            return settingDao.GetBoolSetting(name);
        }


        public string GetStringSetting(string name)
        {
            return settingDao.GetStringSetting(name);
        }
    }
}
