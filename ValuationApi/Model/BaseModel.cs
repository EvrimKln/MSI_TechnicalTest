using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ValuationApi.Model
{
    public class BaseModel
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatedBy { get; set; }// Normally --> User.UserId
        public DateTime? UpdateDate { get; set; }
        public int? UpdatedBy { get; set; }// Normally --> User.UserId
        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
