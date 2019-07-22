using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdater.Config
{
    class AutoUpdaterConfig
    {
        public DateTime LastUpdateTime { get; set; }
        public string LuanchApplication { get; set; }
        public string RemoteUpdateConfig { get; set; }

        public List<UpdateItem> UpdateList { get; set; } = new List<UpdateItem>();


        public List<string> UpdateLog
        {
            get; set;
        } = new List<string>();


        public List<string> NoUpdateLog
        {
            get; set;
        } = new List<string>();
    }
}
