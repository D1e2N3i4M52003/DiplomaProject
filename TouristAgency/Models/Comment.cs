using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TouristAgency.Models
{
    public class Comment : BaseEntity
    {

        [Required]
        public User Author { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        public DateTime PostDate { get; set; }

        [Required]
        public string Text { get; set; }

        public Comment()
        {

        }
    }
}
