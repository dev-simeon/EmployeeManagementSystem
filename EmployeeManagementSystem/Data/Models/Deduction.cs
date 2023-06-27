using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManagementSystem.Models
{
    public class Deduction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public decimal TaxDeduction { get; set; }
        public decimal InsuranceDeduction { get; set; }

        public decimal CalculateTotalDeductions()
        {
            decimal totalDeductions = TaxDeduction + InsuranceDeduction;

            return totalDeductions;
        }
    }
}
