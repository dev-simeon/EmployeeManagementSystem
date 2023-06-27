using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManagementSystem.Models
{
    public class Department
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string DepartmentName { get; set; }
        public string ManagerId { get; set; }
        public string OrganisationId { get; set; }


        public Department(string departmentName, string departmentSupervisor, string organisationId)
        {
            DateCreated = DateTime.Now;
            DepartmentName = departmentName;
            ManagerId = departmentSupervisor;
            OrganisationId = organisationId;
        }
    }
}
