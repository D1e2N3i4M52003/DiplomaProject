using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Post : BaseEntity
    {

        [Required]
        public User Author { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        public DateTime PostDate { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

    }
}
