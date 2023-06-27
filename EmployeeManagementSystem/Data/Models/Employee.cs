using EmployeeManagementSystem.Data.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string JobTitle { get; set; }
        public string QualificationId { get; set; }
        public string RoleId { get; set; }
        public decimal Salary { get; set; }
        public string DepartmentId { get; set; }
        public string OrganisationId { get; set; }
        public string Password { get; set; }

        public Employee(string email, string phoneNumber, string firstName, string lastName, string password, string roleId, string organisationId)
        {
            Email = email; 
            PhoneNumber = phoneNumber; 
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            RoleId = roleId;
            OrganisationId = organisationId;
            EmploymentDate = DateTime.UtcNow;
            JobTitle = "Manager";
        }

        public Employee()
        {
        }
    }
}
