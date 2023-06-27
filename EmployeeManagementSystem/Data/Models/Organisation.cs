using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManagementSystem.Models
{
    public class Organisation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string OrganisationName { get; set; }
    }
}
