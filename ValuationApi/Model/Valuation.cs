using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ValuationApi.Model
{
    public class Valuation : BaseModel
    {
        [Required]
        public int ImoNumber { get; set; }
        public Vessel Vessel { get; set; }
        [Required]
        public int VesselTypeId { get; set; }
        [Required]
        public int Year { get; set; }
        public int Age { get; set; }
        [Display(Name = "Fair Market Value($ Mn)")]
        public decimal FairMarketValue { get; set; }
        [Display(Name = "Operating Costs($ k/Day)")]
        public decimal OperatingCosts { get; set; }
        [Display(Name = "Scrap Price($ Mn)")]
        public decimal ScrapPrice { get; set; }  
    }
}
