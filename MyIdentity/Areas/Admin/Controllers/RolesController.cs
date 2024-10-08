﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyIdentity.Areas.Admin.Models.Dto;
using MyIdentity.Areas.Admin.Models.Dto.Roles;
using MyIdentity.Models.Entities;

namespace MyIdentity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public RolesController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles
                .Select(p =>
                 new RoleListDto
                 {
                     Id = p.Id,
                     Description = p.Description,
                     Name = p.Name
                 })
                .ToList();

            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddNewRoleDto newRole)
        {
            Role role = new Role()
            {
                Description = newRole.Description,
                Name = newRole.Name,

            };
            var result = _roleManager.CreateAsync(role).Result;

            //_roleManager.UpdateAsync()
            //_roleManager.DeleteAsync()
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Roles", new { area = "Admin" });
            };
            ViewBag.Errors = result.Errors.ToList();
            return View(newRole);

        }

        public IActionResult UserInRole(string Name)
        {
            var usersInRole = _userManager.GetUsersInRoleAsync(Name).Result;

            return View(usersInRole.Select(p => new UserListDto
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                UserName = p.UserName,
                PhoneNumber = p.PhoneNumber,
                Id = p.Id,
            }));
        }
    }
}
