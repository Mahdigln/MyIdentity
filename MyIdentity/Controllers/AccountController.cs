using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyIdentity.Models.Dto;
using MyIdentity.Models.Entities;
using MyIdentity.Services;

namespace MyIdentity.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
   // private readonly EmailService _emailService;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager/*, EmailService emailService*/ )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        //_emailService = new EmailService();;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(RegisterDto register)
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
            UserName = register.Email,
            
        };

        var result = _userManager.CreateAsync(newUser, register.Password).Result;
        if (result.Succeeded)
        {

            var token = _userManager.GenerateEmailConfirmationTokenAsync(newUser).Result;
            string callbackUrl = Url.Action("ConfirmEmail", "Account", new
            {
                UserId = newUser.Id
                ,
                token = token
            }, protocol: Request.Scheme);

            string body = $"لطفا برای فعال حساب کاربری بر روی لینک زیر کلیک کنید!  <br/> <a href={callbackUrl}> Link </a>";
                EmailService.Send(newUser.Email, "فعال سازی حساب کاربری", body);


            #region . کد اموزش برا سرویس ایمیل ک کار نکرد بالای صفحه هم سرویس ایمیل کامنت شد

            // _emailService.Execute(newUser.Email, body, "فعال سازی حساب کاربری ");

            #endregion

            return RedirectToAction("DisplayEmail");

            //return RedirectToAction("Index", "Home");
            
        }

        string message = "";
        foreach (var item in result.Errors.ToList())
        {
            message += item.Description + Environment.NewLine;
        }
        TempData["Message"] = message;
        return View(register);
    }
    [HttpGet]
    public IActionResult Login(string returnUrl = "/")
    {

        return View(new LoginDto
        {
            ReturnUrl = returnUrl,
        });
    }

    public IActionResult ConfirmEmail(string UserId, string Token)
    {
        if (UserId == null || Token == null)
        {
            return BadRequest();
        }
        var user = _userManager.FindByIdAsync(UserId).Result;
        if (user == null)
        {
            return View("Error");
        }

        var result = _userManager.ConfirmEmailAsync(user, Token).Result;
        if (result.Succeeded)
        {
            /// return 
        }
        else
        {

        }
        return RedirectToAction("login");

    }

    public IActionResult DisplayEmail()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Login(LoginDto login)
    {
        if (!ModelState.IsValid)
        {
            return View(login);
        }

        var user = _userManager.FindByNameAsync(login.UserName).Result;

        _signInManager.SignOutAsync();

        var result = _signInManager.PasswordSignInAsync(user, login.Password, login.IsPersistent
            , true).Result;

        if (result.Succeeded == true)
        {
            return Redirect(login.ReturnUrl);
        }
        if (result.RequiresTwoFactor == true)
        {
            //
        }
        if (result.IsLockedOut)
        {
            //
        }

        ModelState.AddModelError(string.Empty, "Login  Error");
        return View();
    }


    public IActionResult LogOut()
    {
        _signInManager.SignOutAsync();
        return RedirectToAction("Index", "home");
    }
}

