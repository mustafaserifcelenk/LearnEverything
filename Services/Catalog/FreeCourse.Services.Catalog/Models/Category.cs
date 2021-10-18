using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FreeCourse.Services.Catalog.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]  // Id'yi string olarak alıp ObjectId'ye dönüştürüyor, getirirken de string'e dönüştürüp veriyor
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
