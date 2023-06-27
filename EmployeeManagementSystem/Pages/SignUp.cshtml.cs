using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagementSystem.Pages
{
    public class SignUpModel : PageModel
    {
        public const string LoggedInUserId = "currUserId";
        public const string LoggedInUserOrganisationId = "organisationId";

        [BindProperty]
        public string? OrganisationName { get; set; }

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? FirstName { get; set; }

        [BindProperty]
        public string? LastName { get; set; }

        [BindProperty]
        public string? PhoneNumber { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [BindProperty]
        public string? ConfirmPassword { get; set; }


        public PageResult OnGet()
        {
            return Page();
        }

        public ActionResult OnPost()
        {
            var db = new Db();
            List<Organisation> organisations = db.FindAllOrganisation();
            foreach (var organisation in organisations)
            {
                if (organisation.OrganisationName == OrganisationName)
                {
                    ModelState.AddModelError("", "Organisation with name already exist.");
                    return Page();
                }
            }
            if (Password == ConfirmPassword)
            {
                var organisation = new Organisation { OrganisationName = OrganisationName };
                db.CreateOrganisation(organisation);

                Role role = new() { Name = "Admin" };
                db.CreateRole(role);

                var employee = new Employee(Email!, PhoneNumber!, FirstName!, LastName!, ConfirmPassword!, role.Id!, organisation.Id!);
                db.AddEmployee(employee);

                return RedirectToPage("/Overview");
            }
            ModelState.AddModelError("", "Invalid Password Combination");
            return Page();
        }
    }
}