using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MVCWithDB_Identity.Models
{
	public class CustomUser : IdentityUser
	{
		[Column(TypeName = "nvarchar(50)")]
		public string FirstName { get; set; } = null!;

		[Column(TypeName = "nvarchar(50)")]
		public string LastName { get; set; } = null!;
		public DateTime BirthDay { get; set; }
		//[AllowNull]
		public byte[]? Image { get; set; }
		public string? Gender { get; set; }

	}
}
