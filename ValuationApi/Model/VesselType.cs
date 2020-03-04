using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ValuationApi.Model
{
    public class VesselType : BaseModel
    {    
        [Required]
        public string Type { get; set; }

        public ICollection<Vessel> Vessels { get; set; }
        public ICollection<TimeSeries> TimeSeries { get; set; }
    }
}
