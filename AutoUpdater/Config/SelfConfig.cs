using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdater.Config
{
    class SelfConfig
    {
        public int LinkTimes
        {
            get; set;
        }

        public int TimeSpan
        {
            get;set;
        }

        public AutoUpdaterConfig AutoUpdaterConfig
        {
            get;set;
        }

        public string Background
        {
            get;set;
        }
    }
}
