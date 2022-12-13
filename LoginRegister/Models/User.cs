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
       

        [DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required]
        

        [DataType(DataType.Text)]
        public string MiddleName { get; set; }
        [Required]

        [DataType(DataType.Text)]   
        public string LastName { get; set; }
        [Required]

        [DataType(DataType.Text)]
        public string FathersName { get; set; }
        

        [Required]
        
        [RegularExpression(@"^([0-9]{10})$",ErrorMessage = "please Enter valid mobile number")]
        [MaxLength(10)]
        [MinLength(10)]
        public string MobileNumber { get; set; }
        [Required]
       


        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage ="Please Enter valid EmailID")]
        public string Email{ get; set; }

        [Required]


        [MaxLength(12)]
        [MinLength(12)]
        //[RegularExpression("[^0-9]", ErrorMessage = "please Enter Aadhar number")]
        public string AadharNumber { get; set; }
        [Required]
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public string DateofBirth { get; set; }
        [Required]

        
        public string Address { get; set; }
        [Required]
        

        public string Occupation { get; set; }
        [Required]

       // [RegularExpression("[^0-9]", ErrorMessage = "please Enter Annual income")]
        public string AnnualIncome { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6)]


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
    