using System;
using System.Collections.Generic;
using System.Text;
using static Epc.Sgtin.Sgtin;

namespace Epc.Sgtin
{
    public interface ISgtin
    {
        byte Header { get; }
        SgtinFilter Filter { get; }
        byte Partition { get; }
        ulong CompanyPrefix { get; }
        uint ItemReference { get; }
        ulong SerialReference { get; }
    }
}
