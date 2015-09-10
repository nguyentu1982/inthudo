using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InthudoService
{
    public interface ISettingService
    {
        bool GetBoolSetting(string name);

        string GetStringSetting(string p);
    }
}
