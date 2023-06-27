using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManagementSystem.Models
{
    public class Payroll
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PayrollId { get; set; }
        public string EmployeeId { get; set; }
        public string OrganisationId { get; set; }
        public DateTime PayPeriod { get; set; }
        public decimal BasicSalary { get; set; }
        public Allowance Allowances { get; set; } = new();
        public Deduction Deductions { get; set; } = new();
        public decimal NetSalary { get; set; }


        public decimal CalculateNetSalary()
        {
            decimal grossSalary = BasicSalary + Allowances.CalculateTotalAllowances();
            decimal totalDeductions = Deductions.CalculateTotalDeductions();
            NetSalary = grossSalary - totalDeductions;

            return NetSalary;
        }
    }
}
