using System.Collections.Generic;
using DataAccess;

namespace Data.Entities
{
    public class Person : IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; }

        public int Id { get; set; }
    }
}
