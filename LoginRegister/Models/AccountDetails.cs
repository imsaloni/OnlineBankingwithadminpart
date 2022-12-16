using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace LoginRegister.Models
{
    public class AccountDetails
    {
        [Key]
        public int AccountNumber { get; set; }

        //This is for foreign key refrence
        [Display(Name = "UserId")]
        public virtual int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public Decimal Balance { get; set; }    

    }
}