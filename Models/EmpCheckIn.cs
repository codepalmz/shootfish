using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CheckIn.Models
{
    public class EmpCheckIn 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpCheckInId { get; set; }

        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee Employee{ get; set; }
        
        [Required]
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;
    }

}
