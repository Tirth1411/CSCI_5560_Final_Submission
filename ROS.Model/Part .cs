using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROS.Model
{
    public class Part
    {
        [Key]
        [Column(TypeName = "varchar(2)")]
        public string Pno { get; set; } // Primary Key

        [Required]
        [MaxLength(50)]
        public string Pname { get; set; }

        [MaxLength(50)]
        public string Color { get; set; }

        [Range(1, 100, ErrorMessage = "Weight must be between 1 and 100.")]
        public decimal? Weight { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        // Navigation property for the relationship with Shipment
        public ICollection<Shipment> Shipments { get; set; }
    }
}
