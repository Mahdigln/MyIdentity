using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyIdentity.Areas.Admin.Models.Dto;
using MyIdentity.Models.Dto;
using MyIdentity.Models.Entities;

namespace MyIdentity.Areas.Admin.Controllers;


[Area("Admin")]
public class UsersController : Controller
{
    private readonly UserManager<User> _userManager;
    public UsersController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users
            .Select(p => new UserListDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                UserName = p.UserName,
                PhoneNumber = p.PhoneNumber,
                EmailConfirmed = p.EmailConfirmed,
                AccessFailedCount = p.AccessFailedCount
            }).ToList();
        return View(users);
    }


    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(RegisterDto register)
    {
        if (ModelState.IsValid == false)
        {
            return View(register);
        }

        User newUser = new User()
        {
            FirstName = register.FirstName,
            LastName = register.LastName,
            Email = register.Email,
            UserName = register.Email
        };

        var result = _userManager.CreateAsync(newUser, register.Password).Result;
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Users", new { area = "admin" });
        }

        string message = "";
        foreach (var item in result.Errors.ToList())
        {
            message += item.Description + Environment.NewLine;
        }
        TempData["Message"] = message;
        return View(register);
    }


    public IActionResult Edit(string Id)
    {
        var user = _userManager.FindByIdAsync(Id).Result;
        // return View(user); this is true and don't need to write below but it take all data
        UserEditDto userEdit = new UserEditDto()
        {
            Email = user.Email,
            FirstName = user.FirstName,
            Id = user.Id,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName,
        };
        return View(userEdit);

    }


    [HttpPost]
    public IActionResult Edit(UserEditDto userEdit)
    {
        var user = _userManager.FindByIdAsync(userEdit.Id).Result;
        user.FirstName = userEdit.FirstName;
        user.LastName = userEdit.LastName;
        user.PhoneNumber = userEdit.PhoneNumber;
        user.Email = userEdit.Email;
        user.UserName = userEdit.UserName;

        var result = _userManager.UpdateAsync(user).Result;

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }
        string message = "";
        foreach (var item in result.Errors.ToList())
        {
            message += item.Description + Environment.NewLine;
        }
        TempData["Message"] = message;
        return View(userEdit);
    }

    public IActionResult Delete(string Id)
    {
        var user = _userManager.FindByIdAsync(Id).Result;
        UserDeleteDto userDelete = new UserDeleteDto()
        {
            Email = user.Email,
            FullName = $"{user.FirstName}  {user.LastName}",
            Id = user.Id,
            UserName = user.UserName,
        };
        return View(userDelete);
    }

    [HttpPost]
    public IActionResult Delete(UserDeleteDto userDelete)
    {
        var user = _userManager.FindByIdAsync(userDelete.Id).Result;

        var result = _userManager.DeleteAsync(user).Result;

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Users", new { area = "Admin" });
            
        }

        string message = "";
        foreach (var item in result.Errors.ToList())
        {
            message += item.Description + Environment.NewLine;
        }
        TempData["Message"] = message;

        return View(userDelete);
    }

}

