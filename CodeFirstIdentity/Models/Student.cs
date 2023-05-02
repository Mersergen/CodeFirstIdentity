using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstIdentity.Models
{
    public class Student
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StudentNumber { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        public int DepartID { get; set; }
        [ForeignKey("DepartID")]
        public virtual Department department { get; set; }

    }
}
