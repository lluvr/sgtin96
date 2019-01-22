using System;
using System.Collections.Generic;
using System.Text;
using Epc.Sgtin;

namespace Epc.Sgtin
{   
    /// <summary>
    /// Sgtin base class
    /// </summary>
    public abstract class Sgtin : ISgtin
    {
        private readonly byte _header;
        private readonly SgtinFilter _filter;
        private readonly byte _partition;
        private readonly ulong _companyPrefix;
        private readonly uint _itemReference;
        private readonly ulong _serialReference;

        public byte Header => _header;
        public SgtinFilter Filter => _filter;
        public byte Partition => _partition;
        public ulong CompanyPrefix => _companyPrefix;
        public uint ItemReference => _itemReference;
        public ulong SerialReference => _serialReference;

        /// <summary>
        /// Base Constructor
        /// </summary>
        /// <param name="header"></param>
        /// <param name="filter"></param>
        /// <param name="partition"></param>
        /// <param name="companyPrefix"></param>
        /// <param name="itemReference"></param>
        /// <param name="serialReference"></param>
        public Sgtin(byte header, SgtinFilter filter, byte partition, ulong companyPrefix, uint itemReference, ulong serialReference)
        {
            _header = header;
            _filter = filter;
            _partition = partition;
            _companyPrefix = companyPrefix;
            _itemReference = itemReference;
            _serialReference = serialReference;
        }

        /// <summary>
        /// Sgtin Filter enum
        /// </summary>
        public enum SgtinFilter : byte
        {
            AllOther = 0x0,
            PointOfSale = 0x01,
            FullCaseTransport = 0x02,
            Reserved1 = 0x03,
            InnerPack = 0x04,
            Reserved2 = 0x05,
            UnitLoad = 0x06,
            UnitInsideTrade = 0x07
        }

        /// <summary>
        /// Array of by specification default partition related values
        /// </summary>
        internal static ISgtinPartition[] SgtinPartitions = new SgtinPartition[]
        {
            new SgtinPartition() { PartitionValue = 0, CompanyPrefixBits = 40, CompanyPrefixDigits = 12, ItemReferenceBits = 04, ItemReferenceDigits = 1 },
            new SgtinPartition() { PartitionValue = 1, CompanyPrefixBits = 37, CompanyPrefixDigits = 11, ItemReferenceBits = 07, ItemReferenceDigits = 2 },
            new SgtinPartition() { PartitionValue = 2, CompanyPrefixBits = 34, CompanyPrefixDigits = 10, ItemReferenceBits = 10, ItemReferenceDigits = 3 },
            new SgtinPartition() { PartitionValue = 3, CompanyPrefixBits = 30, CompanyPrefixDigits = 9, ItemReferenceBits = 14, ItemReferenceDigits = 4 },
            new SgtinPartition() { PartitionValue = 4, CompanyPrefixBits = 27, CompanyPrefixDigits = 8, ItemReferenceBits = 17, ItemReferenceDigits = 5 },
            new SgtinPartition() { PartitionValue = 5, CompanyPrefixBits = 24, CompanyPrefixDigits = 7, ItemReferenceBits = 20, ItemReferenceDigits = 6 },
            new SgtinPartition() { PartitionValue = 6, CompanyPrefixBits = 20, CompanyPrefixDigits = 6, ItemReferenceBits = 24, ItemReferenceDigits = 7 }
        };
    }
}
