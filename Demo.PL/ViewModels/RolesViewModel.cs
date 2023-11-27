using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class RolesViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="Role Name I Required")]
        public string RoleName { get; set; }
        public RolesViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

}
