using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = " First Name Is Required")]
        public string FName { get; set; }
        [Required(ErrorMessage = " Last Name Is Required")]
        public string LName { get; set; }
        [Required(ErrorMessage = " Email Address Is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email Address")]
        public String Email { get; set; }
        [Required(ErrorMessage = " Passowrd Is Required")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required(ErrorMessage = "Confirmed Passowrd Is Required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password Not Matched")]
        public String ConfirmedPassword { get; set; }
        public bool IsAgree { get; set; }


    }
}
