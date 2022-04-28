using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
	public class Destinations : BaseEntity
	{

		[Required]
		public string Name { get; set; }

		[Required]
		public string City { get; set; }

		public string Description { get; set; }

		public ICollection<Excursion> Excursions { get; set; }

	}
}
