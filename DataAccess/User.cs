using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleWare.DataAccess
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Username is required")]
        public string Password { get; set; }
    }
}
