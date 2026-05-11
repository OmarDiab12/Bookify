using Bookify.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.User
{
    public class Permission
    {
        public static readonly Permission UsersRead = new(1, "users:read"); 
        public Permission(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; init; } 

        public string Name { get; init; } 

        public ICollection<User> Users { get; init; } = [];
    }
}
