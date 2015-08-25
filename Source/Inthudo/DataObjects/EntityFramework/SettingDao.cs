using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.EntityFramework
{
    public class SettingDao:ISettingDao
    {
        public bool GetBoolSetting(string name)
        {
            using (var context = new InThuDoEntities())
            {
                var query = from s in context.Settings
                            where s.Name == name
                            select s;
                bool result = false;
                bool.TryParse(query.FirstOrDefault().Value, out result);
                return result;
            }
        }
    }
}
