using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginRegister.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

      //  This is for foreign key refrence
      //  [Display(Name = "UserId")]
        //public virtual int UserId { get; set; }

    //    [ForeignKey("UserId")]
      //  public virtual User User { get; set; }


        //Account no foregion key
        [Display(Name ="AccountNumber")]
        public virtual int AccountNumber { get;set; }

       [ForeignKey("AccountNumber")]
       public virtual AccountDetails AccountDetails {get; set;}

        public int payeeAccountNo { get; set; }

        [Required]
       
        public Decimal TransationAmount {get; set; }

        public string TransactionType { get; set; }

        [Required]
        public string TransactionDate { get; set; } 


    }
}