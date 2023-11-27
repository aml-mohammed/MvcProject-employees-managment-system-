using AutoMapper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            _roleManager = roleManager;
           _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var roles = await _roleManager.Roles.ToListAsync();
                var mappedRoles=  _mapper.Map<IEnumerable<IdentityRole>,IEnumerable<RolesViewModel>>(roles);
                return View(mappedRoles);
            }

            else
            {
                var role =await _roleManager.FindByNameAsync(SearchValue);
                var mappedRoles =  _mapper.Map<IdentityRole,RolesViewModel>(role);
                return View(new List<RolesViewModel>(){mappedRoles });

            }
            //return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RolesViewModel model)
        {
            if (ModelState.IsValid) {
                var mappedRole = _mapper.Map<RolesViewModel, IdentityRole>(model);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
           
        }


        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)

                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            var MappedRole = _mapper.Map<IdentityRole, RolesViewModel>(role);
            return View(ViewName, MappedRole);

        }
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, RolesViewModel model)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = model.RoleName;
                    //    var mappedUser = _mapper.Map<UserViewModel, ApplicationUser>(model);
                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
