using System;
using System.Collections.Generic;
using System.Text;

namespace rpa_functions.rpa_pc269
{
    public partial class Assets
    {
        public int AssetId { get; set; }
        public string Country { get; set; }
        public string AssetName { get; set; }
        public string InstType { get; set; }
        public string Verifier { get; set; }
        public short Sla { get; set; }
        public string AbbyyBatch { get; set; }
    }
}
