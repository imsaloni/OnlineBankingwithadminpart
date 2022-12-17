using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginRegister.Models
{
    public class Admin
    {
       
            [Key]
            public int Id { get; set; }
            public int AdminId { get; set; }
            public string Password { get; set; }
        
    }
}