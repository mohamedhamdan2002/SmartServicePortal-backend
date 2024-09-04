using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Core.Entities
{
    public class Review : BaseEntity
    {
        public string Title { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public bool AsAnonymous { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public string CustomerId { get; set; }
        public User Customer { get; set; }
        public int ServiceId { get; set; }
    }
}
