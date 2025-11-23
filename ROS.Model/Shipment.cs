using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROS.Model
{
    public class Shipment
    {

        [Column(TypeName = "varchar(2)")]
        public string Sno { get; set; } // Foreign Key to Supplier

        [Column(TypeName = "varchar(2)")]
        public string Pno { get; set; } // Foreign Key to Part

        //[Range(0, int.MaxValue)]
        public int? Qty { get; set; } = 100; // Default value of 100

        //[Range(0.001, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal? Price { get; set; }

        // Navigation properties
        public Supplier Supplier { get; set; }
        public Part Part { get; set; }
    }
}
