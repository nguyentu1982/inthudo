using DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InthudoService
{
    public abstract class ServiceBase
    {
        static readonly string provider = ConfigurationManager.AppSettings.Get("DataProvider");
        public static readonly IDaoFactory factory = DaoFactories.GetFactory(provider);
    }
}
