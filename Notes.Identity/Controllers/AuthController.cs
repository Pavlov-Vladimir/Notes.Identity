using Microsoft.AspNetCore.Mvc;

namespace Notes.Identity.Controllers;
public class AuthController : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        LoginViewModel viewModel = new() { ReturnUrl = returnUrl };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel viewModel)
    {
        return View(viewModel);
    }
}
