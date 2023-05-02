using System.ComponentModel.DataAnnotations;

namespace CodeFirstIdentity.Models
{
    public class Department
    {
        [Key]
        public int ID { get; set; }
        [Required, StringLength(100)]
        public string DepartmentName { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Mail { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
