using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epc.Sgtin.Console
{
    /// <summary>
    /// csv data realted class
    /// </summary>
    public class ItemDetails
    {
        public ItemDetails()
        {}

        public ItemDetails(ulong companyPrefix, string companyName, uint itemReference, string itemName)
        {
            this.CompanyPrefix = companyPrefix;
            this.CompanyName = companyName;
            this.ItemReference = itemReference;
            this.ItemName = itemName;
        }

        public ulong CompanyPrefix { get; set; }
        public string CompanyName { get; set; }
        public uint ItemReference { get; set; }
        public string ItemName { get; set; }
    }
}
