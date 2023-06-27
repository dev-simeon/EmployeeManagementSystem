using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EmployeeManagementSystem.Pages
{
    [Authorize]
    public class OverviewModel : PageModel
    {
        public Employee? Employee { get; set; }
        [BindProperty]
        public string? FirstName { get; set; }

        [BindProperty]
        public string? LastName { get; set; }

        [BindProperty]
        public string? Address { get; set; }

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? Number { get; set; }

        public ActionResult OnGet()
        {
            var db = new Db();
            Employee = db.FindEmployeeWithId(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return Page();
        }
    }
}
