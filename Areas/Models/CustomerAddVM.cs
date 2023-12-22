using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Models{
	public class CustomerAddVM
	{
		[Required]
		public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
		[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
	}
}

