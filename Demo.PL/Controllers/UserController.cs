using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,IMapper mapper)
        {
           _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var users = await _userManager.Users.Select(
                     U => new UserViewModel()
                     {
                         Id = U.Id,
                         FName = U.FName,
                         LName = U.LName,
                         Email = U.Email,
                         PhoneNumber = U.PhoneNumber,
                         Roles = _userManager.GetRolesAsync(U).Result
                     }).ToListAsync();
                return View(users);
            }
            else
            {
                var User =await _userManager.FindByEmailAsync(SearchValue);
                var mappedUser = new UserViewModel()
                {
                    Id = User.Id,
                    FName = User.FName,
                    LName = User.LName,
                    Email = User.Email,
                    PhoneNumber = User.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(User).Result

                };
            return View(new List<UserViewModel> { mappedUser });
            }
           // return View();
        }
        public async Task<IActionResult> Details(string id,string ViewName="Details")
        {
            if (id is null) 
         
                return BadRequest();
           
            var user =await _userManager.FindByIdAsync(id);
                if (user is null)
                   return NotFound();
                var MappedsUer=_mapper.Map<ApplicationUser,UserViewModel>(user);
            return View(ViewName, MappedsUer);

        }
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id,UserViewModel model)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.PhoneNumber = model.PhoneNumber;
                    user.FName = model.FName;
                    user.LName = model.LName;
                //    var mappedUser = _mapper.Map<UserViewModel, ApplicationUser>(model);
                    await _userManager.UpdateAsync(user);
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
                var User= await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(User);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
