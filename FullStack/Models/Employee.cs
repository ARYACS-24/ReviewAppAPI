using System.ComponentModel.DataAnnotations;

namespace FullStack.Models
{
    public class Employee
    {
        public int SNo { get; set; }
        [Key]
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string Email { get; set; }
        public string Mentor { get; set; }

    }
}
