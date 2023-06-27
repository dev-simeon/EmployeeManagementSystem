using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace EmployeeManagementSystem.Pages
{
    [Authorize]
    public class DepartmentModel : PageModel
    {
        [BindProperty]
        public string DepartmentName { get; set; } = string.Empty;

        [BindProperty]
        public string DepartmentSupervisor { get; set; } = string.Empty;


        public List<Department> Departments { get; set; } = new();
        public List<SelectListItem> Employees { get; set; } = new List<SelectListItem>();

        private readonly Db _db;
        private string _organisationId;

        public DepartmentModel(IHttpContextAccessor httpContextAccessor)
        {
            _db = new Db();
            var organisationIdClaim = httpContextAccessor.HttpContext.User.FindFirst("OrganisationId");
            _organisationId = organisationIdClaim!.Value;
        }

        public void OnGet()
        {
            var employees = _db.FindAllEmployeesInAnOrganisation(_organisationId);

            foreach (var employee in employees)
            {
                Employees.Add(new SelectListItem
                {
                    Value = employee.Id,
                    Text = $"{employee.FirstName} {employee.LastName}"
                });
            }
            Departments = _db.FindAllDepartmentBelongingToAnOrganisation(_organisationId);
        }

        public PageResult OnPost()
        {
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var employee = _db.FindEmployeeWithId(id);

            if (DepartmentSupervisor != null)
            {
                //var departmentSupervisor = _db.FindEmployeeWithId(DepartmentSupervisor);
                //var supervisorName = $"{departmentSupervisor.FirstName} {departmentSupervisor.LastName}";
                var department = new Department(DepartmentName, DepartmentSupervisor, _organisationId);

                _db.CreateDepartment(department);
                Departments.Add(department);

                employee.DepartmentId = department.Id!;
                _db.UpdateUserInfo(employee);

                OnGet();
                return Page();
            }

            ModelState.AddModelError("", "please add department supervisior");
            OnGet();
            return Page();
        }

        public IActionResult OnPostDeleteDepartment(string DepartmentId)
        {
            _db.DeleteDepartment(DepartmentId);
            return RedirectToPage("Department");
        }

        public IActionResult OnPostEditDepartment(string DepartmentId)
        {
            var department = _db.FindADepartment(DepartmentId);
            var departmentSupervisor = _db.FindEmployeeWithId(DepartmentSupervisor);

            var supervisorName = $"{departmentSupervisor.FirstName} {departmentSupervisor.LastName}";

            department.ManagerId = supervisorName;
            department.DepartmentName = DepartmentName;
            department.DateCreated = DateTime.Now;

            _db.UpdateDepartmentInfo(department);
            return RedirectToPage("Department");
        }
    }
}