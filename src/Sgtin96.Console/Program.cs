using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epc.Sgtin;

namespace Epc.Sgtin.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                Task info: 

                Write a class library using OOP paradigm that will be able to decode SGTIN-96 EPC
                tags. When doing decoding operation, input would be EPC tag string represented in
                the hexadecimal format.

                Using your class library for decoding SGTIN-96 EPCs write a simple console
                application that will scan list of tags found in file tags.txt and answer you following
                question: What is exact count of Milka Oreo chocolates found in tags.txt file?

                Your program should also list all the serial numbers of all Milka Oreo chocolates
                found encoded using SGTIN-96 standard and list all the tags which are not encoded
                properly in SGTIN-96 format.

                Application can be a simple console application which will read all the SGTIN EPC numbers from
                tags.txt file and output the count and lists based on the condition defined in first part
                of the task.
            */

            // read item and company task relevant data
            var itemDetails = File.ReadLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"data\data.csv"));
            ItemDetails milkaOreoItemDetails = null;

            // find milkaOreo item details
            foreach (var itemDetail in itemDetails)
            {
                if (itemDetail.Split(';')[3] == "Milka Oreo")
                {
                    ulong.TryParse(itemDetail.Split(';')[0], out ulong companyPrefix);
                    uint.TryParse(itemDetail.Split(';')[2], out uint itemReference);

                    milkaOreoItemDetails = new ItemDetails(companyPrefix, itemDetail.Split(';')[1], itemReference, itemDetail.Split(';')[3]);
                    break;
                }        
            }

            if (milkaOreoItemDetails != null)
            {
                // read hex tags from task provided file
                var hexTags = File.ReadLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"tags\tags.txt"));

                List<ulong> milkaOreoSerials = new List<ulong>();
                List<string> invalidHexTags = new List<string>();

                foreach (string hexTag in hexTags)
                {
                    try
                    {
                        // decode hexTag
                        var sgtin96 = Sgtin96.HexStringToSgtin96(hexTag);

                        // add found milkaOreo serial numbers to list
                        if (sgtin96.ItemReference == milkaOreoItemDetails.ItemReference) { milkaOreoSerials.Add(sgtin96.ItemReference); }
                    }
                    catch
                    {
                        invalidHexTags.Add(hexTag);
                        continue;
                    }
                }

                // output:
                System.Console.WriteLine("Exact count of Milka Oreo chocolates found in tags.txt file:\n" + milkaOreoSerials.Count);

                System.Console.WriteLine("\nSerial numbers of all Milka Oreo chocolates:");
                milkaOreoSerials.ForEach(x => System.Console.WriteLine("{0}", x));

                System.Console.WriteLine("\nTags which are not encoded properly:");
                invalidHexTags.ForEach(x => System.Console.WriteLine("{0}", x));
            }
            else
            {
                throw new ArgumentNullException("Milka oreo item details not found.");
            }

            // end
            System.Console.WriteLine("\nPress any key to exit.");
            System.Console.ReadKey();
        }
    }
}
