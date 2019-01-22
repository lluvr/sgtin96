using System;
using System.Collections.Generic;
using System.Text;

namespace Epc.Sgtin
{
    public interface ISgtinPartition
    {
        int PartitionValue { get; set; }
        int CompanyPrefixBits { get; set; }
        int CompanyPrefixDigits { get; set; }
        int ItemReferenceBits { get; set; }
        int ItemReferenceDigits { get; set; }
    }
}
