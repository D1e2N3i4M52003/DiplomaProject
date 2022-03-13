using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TouristAgency.Models
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
        public decimal Price { get; set; }

        public virtual List<Destinations> Destinations { get; set; }

        public virtual List<User> Participants { get; set; }
    }
}
