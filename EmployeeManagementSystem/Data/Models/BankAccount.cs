using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class BankAccount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string AccountNumber { get; set;}

    }
}
