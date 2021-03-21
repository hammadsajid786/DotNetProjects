using Common.Library.Interfaces.MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sample.Service.Entities
{

    [BsonIgnoreExtraElements]
    public class Staff : IEntity
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public List<Address> Addresses { get; set; } // = new List<Address>();

        public DateTime DateOfJoin { get; set; }


        //public DateTimeOffset CreatedDate { get; set; }

        //[BsonDateTimeOptions(DateOnly = true)]
        //public DateTime DateOnly { get; set; }

        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        //public DateTime LocalDateProperty { get; set; }

        //[BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        //public DateTime UtcDateProperty { get; set; }

    }
}
