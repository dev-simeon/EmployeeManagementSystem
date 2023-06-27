using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Data.Models;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EmployeeManagementSystem.Pages
{
    [Authorize]
    public class SettingsModel : PageModel
    {
        [BindProperty]
        public BankAccount Account { get; set; }

        [BindProperty]
        public string OldPassword { get; set; }

        [BindProperty]
        public string? NewPassword { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public Qualification Qualification { get; set; }

        [BindProperty]
        public decimal Salary { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }


        private Db _db;
        public Employee CurrentAuthenticatedEmployee { get; set; }

        public SettingsModel(IHttpContextAccessor httpContextAccessor)
        {
            SuccessMessage = "";
            try
            {
                var id = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                _db = new Db();
                CurrentAuthenticatedEmployee = _db.FindEmployeeWithId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred see below for more details");
                Console.WriteLine(ex.Message);
            }

        }

        public PageResult OnGet()
        {
            try
            {
                Account = _db.FindBankAccount(CurrentAuthenticatedEmployee.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occurred: {ex.Message}");
            }
           
            return Page();
        }

        public void Onpost()
        {
            CurrentAuthenticatedEmployee.Salary = Salary;
            _db.UpdateUserInfo(CurrentAuthenticatedEmployee);
        }

        public PageResult OnPostAddBankAccount()
        {
            Account.UserId = CurrentAuthenticatedEmployee.Id;
            _db.CreateBankAccount(Account);
            SuccessMessage = "Bank account added successfully.";
            return Page();
        }

        public PageResult OnPostAddQualification()
        {
            if (Qualification.EndDate.Date > DateTime.Now.Date)
            {
                ModelState.AddModelError("", "please cross-check the end date.");
                return Page();
            }
            _db.AddQualification(Qualification);
            CurrentAuthenticatedEmployee.QualificationId = Qualification.Id;
            _db.UpdateUserInfo(CurrentAuthenticatedEmployee);
            SuccessMessage = "Qualification added successfully.";
            OnGet();
            return Page();
        }

        public PageResult OnPostChangePassword()
        {
            if (OldPassword != CurrentAuthenticatedEmployee.Password)
            {
                ModelState.AddModelError("", "Old passord does not match cross check.");
                return Page();
            }
            if (NewPassword == ConfirmPassword)
            {
                CurrentAuthenticatedEmployee.Password = ConfirmPassword;
                _db.UpdateUserInfo(CurrentAuthenticatedEmployee);
                SuccessMessage = "Password updated successfully.";
                OnGet();
                return Page();
            }
            ModelState.AddModelError("", "invalid password combination.");
            return Page();
        }
    }
}
