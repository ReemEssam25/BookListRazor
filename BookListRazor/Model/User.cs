using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Your Name")]

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
