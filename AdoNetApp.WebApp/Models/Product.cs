using System.ComponentModel.DataAnnotations;

namespace AdoNetApp.WebApp.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Name should be provided.")]
		[StringLength(50)]
		public string Name { get; set; }

		[Required(ErrorMessage = "Description should be provided.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Weight should be provided.")]
		public int Weight { get; set; }

		[Required(ErrorMessage = "Height should be provided.")]
		public int Height { get; set; }

		[Required(ErrorMessage = "Width should be provided.")]
		public int Width { get; set; }

		[Required(ErrorMessage = "Length should be provided.")]
		public int Length { get; set; }
	}
}
