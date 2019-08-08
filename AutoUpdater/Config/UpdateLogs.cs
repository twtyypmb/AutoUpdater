using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdater.Config
{
    class UpdateLogs
    {
        public List<string> UpdateLog
        {
            get; set;
        } = new List<string>();


        public List<string> SkipUpdateLog
        {
            get; set;
        } = new List<string>();


        public List<string> NoUpdateLog
        {
            get; set;
        } = new List<string>();
    }
}
