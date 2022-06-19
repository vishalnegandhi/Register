using System;
using System.ComponentModel.DataAnnotations;

namespace Register.Models
{
	public class RegisterModel
	{
			[Required(ErrorMessage = "Please Enter Username")]
			[Display(Name = "Username")]
			public string? Username { get; set; }

			[Required(ErrorMessage = "Please Enter Password")]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string? Password { get; set; }
		
	}
}

