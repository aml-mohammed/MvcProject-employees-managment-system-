using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = " Email Address Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String Email { get; set; }
    }
}
