using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PaymentGateWayApp.Models.UserDetails;

namespace PaymentGateWayApp.Models
{
    public class UserContext : DbContext
    {
        public DbSet<UserInfo> userClasses { get; set; }
    }
}
