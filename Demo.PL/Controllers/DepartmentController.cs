using Demo.BLL.Interfaces;
using Demo.BLL.Reprositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {

      //  public IDepartmentReprository _DepartmentReprository { get; }
        public IUnitOfWok _UnitOfWok { get; }

        public DepartmentController(IUnitOfWok unitOfWok)
        {
           // _DepartmentReprository = departmentReprository;
            _UnitOfWok = unitOfWok;
        }


        public async Task<IActionResult> Index()
        {
            var department =await _UnitOfWok.DepartmentReprository.GetAll();
            return View(department);
        }
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
             await   _UnitOfWok.DepartmentReprository.Add(department);
              await  _UnitOfWok.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);

        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)

                return BadRequest();
            var department =await _UnitOfWok.DepartmentReprository.GetById(id.Value);
            if (department is null)
                return NotFound();
            return View(department);


        }

        public async Task<IActionResult> Edit(int? id)
        {
            ////if (id is null)

            ////    return BadRequest();
            ////var department = _DepartmentReprository.GetDepartmentById(id.Value);
            ////if (department is null)
            ////    return NotFound();
            ////return View(department);
            return await Details(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _UnitOfWok.DepartmentReprository.Update(department);
                  await  _UnitOfWok.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }

            return View(department);

        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");

            //if (id is null)
            //    return NotFound();
          
            //var dept = _DepartmentReprository.GetDepartmentById(id.Value);
            //if (dept == null)
            //    return NotFound();
            //return View(dept);
         //   _DepartmentReprository.Delete(dept);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id , Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            try
            {
                //   department = _DepartmentReprository.GetDepartmentById(id);
                _UnitOfWok.DepartmentReprository.Delete(department);
             await   _UnitOfWok.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(department);
            }

        }



    }
}