using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManagementSystem.Models
{
    public class Allowance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public decimal HousingAllowance { get; set; }
        public decimal TransportAllowance { get; set; }

        public decimal CalculateTotalAllowances()
        {
            decimal totalAllowances = HousingAllowance + TransportAllowance;

            return totalAllowances;
        }
    }
}
