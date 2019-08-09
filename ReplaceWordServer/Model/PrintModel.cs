using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWordServer.Model
{
    public class PrintModel
    {
        public String InputFileName { get; set; }
        public String BasePath { get; set; }
        public String BasePathReplaced { get; set; }
        public String KeyPrefix { get; set; }
        public IList<ReplaceModel> Replaces { get; set; }

    }
}
