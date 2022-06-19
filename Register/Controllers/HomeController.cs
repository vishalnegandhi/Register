using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Register.Models;

namespace Register.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;


    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// validates the user entered password passes both the criteria sets
    /// </summary>
    /// <param name="model"></param>
    public IActionResult ValidatePassword(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var str_password = model.Password;

            var hasNumber = new Regex(@"[0-9]+");
            var hasLetter = new Regex(@"[a-zA-Z]+");
            var hasMin8Chars = new Regex(@".{8,}");
            var hasUpperChar = new Regex(@"(?=.*?[A-Z])");
            var haslowerChar = new Regex(@"(?=.*?[a-z])");
            var notStartWithNumber = new Regex("^[^0-9]");
            var notEndWithNumber = new Regex("[^0-9]$");

            List<string> errorMsgs = new List<string>();

            // set two password validation
            if (str_password.Length < 10)
            {
             
                if(!hasMin8Chars.IsMatch(str_password))
                {
                    errorMsgs.Add("Minimum 8 characters");
                }

                if(!hasNumber.IsMatch(str_password))
                {
                    errorMsgs.Add("At least one number");
                }

                if(!hasLetter.IsMatch(str_password))
                {
                    errorMsgs.Add("At least one letter");
                }

                if(!hasUpperChar.IsMatch(str_password))
                {
                    errorMsgs.Add("At least one upper case letter");
                }

                if(!haslowerChar.IsMatch(str_password))
                {
                    errorMsgs.Add("At least one lower case letter");
                }

                if(!notStartWithNumber.IsMatch(str_password))
                {
                    errorMsgs.Add("Should not start with a number");
                }

                if (!notEndWithNumber.IsMatch(str_password))
                {
                    errorMsgs.Add("Should not end with a number");
                }

            }
            // set one password validation
            else
            {
                if(!hasNumber.IsMatch(str_password))
                {
                    errorMsgs.Add("Should not end with a number");
                }

                if(!hasLetter.IsMatch(str_password))
                {
                    errorMsgs.Add("At least one letter");
                }
            }

            if (errorMsgs.Count > 0)
            {
                addCustomErrors(errorMsgs);
            }
            else
            {
                // do something with user inputs
            }
            
        }
        return View("Index");
    }

    /// <summary>
    /// dipsplays error messages to the user
    /// </summary>
    /// <param name="errors">list of error messages to be displayed</param>
    private void addCustomErrors(List<string> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError("", error);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

