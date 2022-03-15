using System;
using System.ComponentModel.DataAnnotations;

namespace AdoNetApp.WebApp.Models
{
	public class CreateOrder
	{

		[Required]
		[StringLength(50)]
		public string Status { get; set; }

		public int ProductId { get; set; }

	}
}