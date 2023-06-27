using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagementSystem.Pages
{
    [Authorize]
    public class EmployeesModel : PageModel
    {
        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string JobTitle { get; set; }

        [BindProperty]
        public DateTime DateOfBirth { get; set; }

        [BindProperty]
        public string Role { get; set; }

        [BindProperty]
        public decimal Salary { get; set; }

        public List<Employee> Employees { get; set; }
        

        private readonly Db _db;
        private string _organisationId;

        public EmployeesModel(IHttpContextAccessor httpContextAccessor)
        {
            _db = new Db();
            var organisationIdClaim = httpContextAccessor.HttpContext.User.FindFirst("OrganisationId");
            _organisationId = organisationIdClaim!.Value;
            Employees = _db.FindAllEmployeesInAnOrganisation(_organisationId);
        }

        public PageResult OnGet()
        {
            return Page();
        }

        public PageResult OnPost() 
        {
            foreach (var employee in Employees)
            {
                if(employee.Email == Email)
                {
                    ModelState.AddModelError("", "employee with email address already exists.");
                    return Page();
                }
            }

            var role = new Role { Name = Role };
            _db.CreateRole(role);
            
            var newEmployee = new Employee
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Address = Address,
                JobTitle = JobTitle,
                DateOfBirth = DateOfBirth,
                EmploymentDate = DateTime.UtcNow,
                RoleId = role.Id,
                Salary = Salary,
                OrganisationId = HttpContext.User.FindFirst("OrganisationId").Value
            };

            _db.AddEmployee(newEmployee);
            Employees.Add(newEmployee);

            return Page();
        }

        public IActionResult OnPostDeleteEmployee(string employeeId)
        {
            var currentEmployee = _db.FindEmployeeWithId(employeeId);
            var role = _db.FindRole(currentEmployee.RoleId);
            if(role.Name == "Admin")
            {
                _db.DeleteEmployee(employeeId);
                _db.DeleteRole(currentEmployee.RoleId);
                _db.DeleteOrganisation(currentEmployee.OrganisationId);
                return RedirectToPage("Index");
            }
            _db.DeleteEmployee(employeeId);
            _db.DeleteRole(currentEmployee.RoleId);
            return RedirectToPage("Employees");
        }

        public IActionResult OnPostEditEmployee(string employeeId)
        {
            var currentEmployee = _db.FindEmployeeWithId(employeeId);
            var role = _db.FindRole(currentEmployee.RoleId);
            role.Name = Role;
            _db.UpdateRole(role);
            currentEmployee.PhoneNumber = PhoneNumber;
            currentEmployee.Salary = Salary;
            currentEmployee.Address = Address;
            currentEmployee.JobTitle = JobTitle;
            currentEmployee.RoleId = role.Id;

            _db.UpdateUserInfo(currentEmployee);
            return RedirectToPage("Employees");
        }
    }
}
