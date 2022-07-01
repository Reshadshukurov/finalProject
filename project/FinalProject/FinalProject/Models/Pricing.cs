using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Pricing : BaseEntity
    {
        public string Price { get; set; }
        public string PriceText { get; set; }
        public string FreeMonth { get; set; }
        public string FreeMonthText1 { get; set; }
        public string FreeMonthText2 { get; set; }
        public string ButtonText { get; set; }
        public string ButtonLink { get; set; }
        public string Hint { get; set; }
    }
}
