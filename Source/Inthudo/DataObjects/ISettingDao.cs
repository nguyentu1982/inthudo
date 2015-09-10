using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public interface ISettingDao
    {
        bool GetBoolSetting(string name);

        string GetStringSetting(string name);
    }
}
