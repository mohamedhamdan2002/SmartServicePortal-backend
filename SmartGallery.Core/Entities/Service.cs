using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Core.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Cost { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
