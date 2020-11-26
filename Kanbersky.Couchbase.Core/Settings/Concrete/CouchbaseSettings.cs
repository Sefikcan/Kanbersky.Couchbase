using Kanbersky.Couchbase.Core.Settings.Abstract;
using System.Collections.Generic;

namespace Kanbersky.Couchbase.Core.Settings.Concrete
{
    public class CouchbaseSettings : ISettings
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public IEnumerable<string> ServerUrls { get; set; }
    }
}
