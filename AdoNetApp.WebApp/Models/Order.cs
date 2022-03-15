using System;
using System.ComponentModel.DataAnnotations;

namespace AdoNetApp.WebApp.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Status { get; set; }

		public DateTime CreatedDate { get; set; }

		public DateTime? UpdatedDate { get; set; }

		public int ProductId { get; set; }

	}
}
