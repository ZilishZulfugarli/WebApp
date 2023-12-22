using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Models{
	public class CustomerUpdateVM
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

