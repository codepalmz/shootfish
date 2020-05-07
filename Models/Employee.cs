using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheckIn.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "Name is too long.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = "Name is too long.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Phone]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address")]
        [StringLength(240, ErrorMessage = "Description too long (240 char).")]
        public string Address { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(50)]
        [Display(Name = "Next Of Kin")]
        public string NextOfKin { get; set; }

        [Phone]
        [Display(Name = "Next Of Kin PhoneNumber")]
        public string NextOfKinPhoneNumber { get; set; }

        
        [Required]
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;
    }
}
