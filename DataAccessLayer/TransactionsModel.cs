using MongoDB.Bson;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
   public class TransactionsModel
    {
        public ObjectId Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow.AddHours(3);
        public Guid IdempotenceKey { get; set; }
    }
}
