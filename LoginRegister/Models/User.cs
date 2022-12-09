using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginRegister.Models
{
    public class User
    {

        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]

        public string MiddleName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]

        public string FathersName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]

        public string MobileNumber { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]


        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email{ get; set; }

        [Required]

        [StringLength(20, MinimumLength = 12)]
        public string AadharNumber { get; set; }
        [Required]
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public string DateofBirth { get; set; }
        [Required]

        [StringLength(100, MinimumLength = 10)]
        public string Address { get; set; }
        [Required]
        

        public string Occupation { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]

        public string AnnualIncome { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]


        public string Password { get; set; }

        [NotMapped]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string FullName()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
    