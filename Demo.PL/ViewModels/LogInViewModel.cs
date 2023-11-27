using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = " Email Address Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String Email { get; set; }
        [Required(ErrorMessage = " Passowrd Is Required")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
     
        public bool RememberMe { get; set; }
    }
}
