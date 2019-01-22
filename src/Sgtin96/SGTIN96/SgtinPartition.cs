using System;
using System.Collections.Generic;
using System.Text;

namespace Epc.Sgtin
{
    /// <summary>
    /// Sgtin Partition 
    /// </summary>
    internal class SgtinPartition : ISgtinPartition
    {
        public int PartitionValue { get; set; }

        public int CompanyPrefixBits { get; set; }
        public int CompanyPrefixDigits { get; set; }

        public int ItemReferenceBits { get; set; }
        public int ItemReferenceDigits { get; set; }
    }
}
