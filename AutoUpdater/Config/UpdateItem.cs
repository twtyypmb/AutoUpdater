using System.Collections.Generic;

namespace AutoUpdater.Config
{
    public class UpdateItem
    {
        public string Name { get; set; }

        public string Version
        {
            get;set;
        }

        public List<UpdateItem> List { get; set; }
    }
}