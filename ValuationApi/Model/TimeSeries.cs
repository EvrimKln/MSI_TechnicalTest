using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ValuationApi.Model
{
    public class TimeSeries : BaseModel
    {
        [Required]        
        public int VesselTypeId { get; set; }
        public VesselType VesselType { get; set; }
        [Required]
        public int Year { get; set; }
    }
}
