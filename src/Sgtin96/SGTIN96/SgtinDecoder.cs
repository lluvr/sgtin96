using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Epc.Sgtin
{
    /// <summary>
    /// Decode sgtin formated input
    /// </summary>
    public static class SgtinDecoder
    {
        /// <summary>
        /// Convert hexadecimal string to bit array
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static BitArray HexStringToBitArray(string hex)
        {
            if (hex == null) { throw new ArgumentNullException("hex value not provided"); }

            // each char as hex => 4 bits 
            BitArray bitArray = new BitArray(4 * hex.Length);

            for (int i = 0; i < hex.Length; i++)
            {
                // parse hex char into byte
                byte hexCharAsByte = byte.Parse(hex[i].ToString(), NumberStyles.HexNumber);

                // foreach bit 
                for (int j = 0; j < 4; j++)
                {
                    /* get current array position starting from fighest value (2pow3) and
                     * do logic AND on current hexCharAsByte and 1 shifted in same direction
                     * from 2po3 to 2pow0. If bits match 1 & 1 hexChar as byte has 1 on that 2powN (position)
                     */
                    bitArray.Set(i * 4 + j, (hexCharAsByte & (1 << (3 - j))) != 0);
                }
            }
            return bitArray;
        }

        /// <summary>
        /// Convert bitArray into uInt by provided bit range
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="startingBit"></param>
        /// <param name="bitCount"></param>
        /// <returns></returns>
        public static uint BitArrayToUInt(BitArray bits, int startingBit, int bitCount)
        {
            if (startingBit < 0 || startingBit + bitCount > bits.Count)
            {
                throw new ArgumentOutOfRangeException("Provided starting bit or bit count are exceeding BitArray limits.");
            } 

            // foreach bit in bit count
            uint result = 0;
            for (int i = 0; i < bitCount; i++)
            {
                var sf = bits[startingBit + bitCount - 1 - i];

                /* starting from last bit (min 2pow) and going to 
                 * the higher value get bit value and if 1 (true) */
                if (bits[startingBit + bitCount - 1 - i])
                {
                    /* 1u: unsigned literal of value 1
                     * Starting from least valuable bit shift left by 1 bit only if current bits array bit is 1 (true).
                     * Each time do logic OR opeation so on each new shift changes will be perserved in a result. 
                     * Short: Logic copies positive bits in same 2pow order (from least value bit) as in provided bits array */
                    result |= (1u << i);
                }
            }

            return result;
        }

        /// <summary>
        /// Convert bitArray into ulong by provided bit range
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="startingBit"></param>
        /// <param name="bitCount"></param>
        /// <returns></returns>
        public static ulong BitArrayToULong(BitArray bits, int startingBit, int bitCount)
        {
            if (startingBit < 0 || startingBit + bitCount > bits.Count)
            {
                throw new ArgumentOutOfRangeException("Provided starting bit or bit count are exceeding BitArray limits.");
            }

            ulong result = 0;
            for (int i = 0; i < bitCount; i++)
            {
                if (bits[startingBit + bitCount - 1 - i])
                {
                    result |= (1u << i);
                }               
            }

            return result;
        }

        /// <summary>
        /// Decode partition related sgtin values using default partition table
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="partitionDefaults"></param>
        /// <param name="partition"></param>
        /// <param name="companyPrefix"></param>
        /// <param name="itemReference"></param>
        internal static void DecodePartition(BitArray bits, ISgtinPartition[] partitionDefaults, byte partition, out ulong companyPrefix, out uint itemReference)
        {
            if (partitionDefaults.Length < partition || partitionDefaults[partition] == null)
            {
                throw new FormatException("Partition value is invalid or undefined.");
            }

            int startingBit = 14;
            ISgtinPartition partitionDefault = partitionDefaults[partition];

            int companyPrefixDefaultBits = partitionDefault.CompanyPrefixBits;
            int itemReferenceDefaultBits = partitionDefault.ItemReferenceBits;

            companyPrefix = SgtinDecoder.BitArrayToULong(bits, startingBit, companyPrefixDefaultBits);
            itemReference = SgtinDecoder.BitArrayToUInt(bits, startingBit + companyPrefixDefaultBits, itemReferenceDefaultBits);
        }
    }
}
