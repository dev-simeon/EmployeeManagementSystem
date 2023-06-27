using EmployeeManagementSystem.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EmployeeManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        public const string LoggedInUserId = "currUserId";
        public const string LoggedInUserOrganisationId = "organisationId";

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public PageResult OnGet()
        {
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            var db = new Db();
            try
            {
                var employee = db.FindAnEmployee(Email);
                if (employee != null)
                {
                    if (employee.Email == Email && employee.Password == Password)
                    {
                        var claims = new List<Claim>{
                            new Claim(ClaimTypes.NameIdentifier, employee.Id!),
                            new Claim("OrganisationId", employee.OrganisationId!),
                        };

                        var claimsIdentity = new ClaimsIdentity(
                                claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(
                            new ClaimsPrincipal(claimsIdentity));

                        return RedirectToPage("Overview");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid email and password combination");
                        return Page();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An erro has occured. See below for more details");
                Console.WriteLine(ex.Message);
            }
            ModelState.AddModelError("", "User does not exist, please check login details.");
            return Page();
        }
    }
}