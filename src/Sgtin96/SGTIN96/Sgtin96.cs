using System;
using System.Collections;
using Epc.Sgtin;

namespace Epc.Sgtin
{
    public class Sgtin96 : Sgtin, ISgtin96
    {
        // fixed sgtin96 default header
        public const byte Sgtin96Header = 0x30;

        /// <summary>
        /// sgtin96 constructor
        /// </summary>
        /// <param name="header"></param>
        /// <param name="filter"></param>
        /// <param name="partition"></param>
        /// <param name="companyPrefix"></param>
        /// <param name="itemReference"></param>
        /// <param name="serialReference"></param>
        public Sgtin96(byte header, SgtinFilter filter, byte partition, ulong companyPrefix, uint itemReference, ulong serialReference)
            : base(header, filter, partition, companyPrefix, itemReference, serialReference)
        { }

        /// <summary>
        /// Decode hex string into Sgtin class instance <see cref="Sgtin96"/>
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static Sgtin96 HexStringToSgtin96(string hex)
        {
            // validate by string length and convert to bit array
            if (hex.Length != 24) { throw new FormatException("Provided hexString must be 24 characters long."); }
            BitArray bits = SgtinDecoder.HexStringToBitArray(hex);

            // validate and get header
            uint header = SgtinDecoder.BitArrayToUInt(bits, 0, 8);
            if (header != Sgtin96Header) { throw new FormatException("Provided header is not valid SGTIN96 header."); }

            // get filter and partition uInt from bits
            SgtinFilter filter = (SgtinFilter)SgtinDecoder.BitArrayToUInt(bits, 8, 3);
            byte partition = (byte)SgtinDecoder.BitArrayToUInt(bits, 11, 3);

            // decode partition related values 
            ulong companyPrefix;
            uint itemReference;
            SgtinDecoder.DecodePartition(bits, SgtinPartitions, partition, out companyPrefix, out itemReference);

            // get serial uLong
            ulong serialReference = SgtinDecoder.BitArrayToULong(bits, 58, 38);

            // init and return new class instance
            return new Sgtin96(Sgtin96Header, filter, partition, companyPrefix, itemReference, serialReference);
        }
    }
}
