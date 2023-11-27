using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentReprository _departmentReprository;
        private readonly IUnitOfWok _unitOfWok;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWok unitOfWok
            ,IMapper mapper)
        {
            //_employeeRepository = employeeRepository;
            //_departmentReprository = departmentReprository;
            _unitOfWok = unitOfWok;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchvalue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(searchvalue))
            {
                employees =await _unitOfWok.EmployeeRepository.GetAll();
            }
            else
            {
                 employees = _unitOfWok.EmployeeRepository.GetEmployeeByName(searchvalue);
               
            }
            var mappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmployee);
        }
        public IActionResult Create()
        {
           // ViewBag.Departments = _unitOfWok.DepartmentReprository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {


             string FileName=   DocumentSeting.UploadFile(employeeVM.Image, "Images");
                employeeVM.ImageName = FileName;
             var mappedEmployee=   _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
             await  _unitOfWok.EmployeeRepository.Add(mappedEmployee);
                
             var result= await  _unitOfWok.CompleteAsync(); //savechanges
                if (result > 0)
                    TempData["message"] = "Employee created";

                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee =await _unitOfWok.EmployeeRepository.GetById(id.Value);
            if (employee == null)

                return NotFound();
            var mappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, mappedEmployee);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    string ImageName = DocumentSeting.UploadFile(employeeVM.Image, "Images");
                    employeeVM.ImageName = ImageName;
                    var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWok.EmployeeRepository.Update(mappedEmployee);
                  await  _unitOfWok.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
         return View(employeeVM);

            }

        
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id,EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id)
            
                return BadRequest();
            
           
            try
            { 
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                _unitOfWok.EmployeeRepository.Delete(mappedEmployee);
               int result=await _unitOfWok.CompleteAsync();
                if (result > 0 && EmployeeVM.ImageName!=null)
                {
                    DocumentSeting.DeleteFile("Images", EmployeeVM.ImageName);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(EmployeeVM);
            }

        }



    }
}
