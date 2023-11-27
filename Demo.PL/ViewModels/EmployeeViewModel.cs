using Demo.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Name Cant be More Than 50 Character")]
        [MinLength(5, ErrorMessage = "Name Cant be Less Than 5 Character")]
        public string Name { get; set; }
        [Range(25, 35, ErrorMessage = "Age Must Be Between 25 And 35")]
        public int? Age { get; set; }
        //[RegularExpression("^[0-9]{1,3}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
        //    ErrorMessage ="Address Must Be Like 123-street-city-country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public String Email { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }

        //  [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        [InverseProperty("Employees")]
        public Department Department { get; set; }
    }
}
