using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SRAPI.Models
{
    public class Brand
    {
        [Key]
        public Guid BrandId { get; set; }
        [Required]
        [MaxLength(50)]
        public string BrandName { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
