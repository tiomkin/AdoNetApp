using System;
using System.ComponentModel.DataAnnotations;
using AdoNetApp.WebApp.Attributes;
using AdoNetApp.WebApp.Enums;

namespace AdoNetApp.WebApp.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		[StringRange(EnumType = typeof(Status), ErrorMessage = "Status should be just of specific values")]
		public string Status { get; set; }

		public DateTime CreatedDate { get; set; }

		public DateTime? UpdatedDate { get; set; }

		public int ProductId { get; set; }

	}
}
