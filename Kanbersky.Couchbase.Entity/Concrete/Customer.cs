using Kanbersky.Couchbase.Core.Entity;

namespace Kanbersky.Couchbase.Entity.Concrete
{
    public class Customer : BaseEntity, IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
