using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROS.Model
{
    public class Supplier
    {
        [Key]
        [Column(TypeName = "varchar(2)")]
        public string Sno { get; set; } // Primary Key

        [Required]
        [MaxLength(50)]
        public string? Sname { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Status must be greater than 0.")]
        public int? Status { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        // Navigation property for the relationship with Shipment
        public ICollection<Shipment> Shipments { get; set; }
    }
}
