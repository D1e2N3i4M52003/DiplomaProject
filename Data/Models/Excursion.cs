using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Excursion : BaseEntity
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime StartsOnDate { get; set; }

        [Required]
        public DateTime EndsOnDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(7,2)")]
        public decimal Price { get; set; }

        public ICollection<Destinations> Destinations { get; set; }

        public virtual List<User> Participants { get; set; }
    }
}
