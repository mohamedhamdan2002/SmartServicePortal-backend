using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions;

namespace Domain.Entities
{
    public class UsersConnection : Entity
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}
