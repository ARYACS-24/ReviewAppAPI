using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStack.Models
{

    public class Review
    {
        public int EmpID { get; set; }
        [Key]
        public int ReviewID { get; set; }
        public DateTime RDate { get; set; }
        public string Feedback { get; set; }
    }
}
