using System;
using System.Collections.Generic;

namespace VividRasV2.Models
{
    public partial class User
    {
        public User()
        {
            Reviews = new HashSet<Review>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
