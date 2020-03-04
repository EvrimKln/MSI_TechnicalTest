using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValuationApi.Model
{
    public class Vessel : BaseModel
    {
        [Required]
        [Index(IsUnique = true)]
        public int ImoNumber { get; set; }
        [Required]
        public int VesselTypeId { get; set; }
        public VesselType VesselType { get; set; }
        [Required]
        [Range(25000, 125000, ErrorMessage = "Size must be between 25.000 and 125.000")]       
        public int Size { get; set; }
        [Required]
        public int YearOfBuild { get; set; }
        public string Name { get; set; }

        public ICollection<Valuation> Valuation { get; set; }
    }
}
