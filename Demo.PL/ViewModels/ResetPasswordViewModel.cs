using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="Password equired")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Password equired")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="password Does not match")]
        public string ConfirmedPassword { get; set; }
    }
}
