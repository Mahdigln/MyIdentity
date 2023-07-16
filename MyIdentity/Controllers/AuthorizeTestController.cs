using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyIdentity.Controllers;

[Authorize(Roles = "Admin,Operator")] //=>  admin or Operator can access
//[Authorize(Roles = "")]


//[Authorize(Roles = "Admin")]
// [Authorize(Roles = "Operator")] these two lines means Admin & Operator can access

public class AuthorizeTestController : Controller
{

    public string Index()
    {
        return "Index";
    }

    [Authorize(Roles = "Operator")]
    public string Edit()
    {
        return "Edit";
    }
}

