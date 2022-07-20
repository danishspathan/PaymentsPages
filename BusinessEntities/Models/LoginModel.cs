using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class LoginModel
    {
        /// <summary>
        /// Login Model used for user login
        /// </summary>
        public class UserLogin
        {
            public string UserName { get; set; }
            public string Firstname { get; set; }
            public string LastName { get; set; }
            [RegularExpression(".+@.+\\..+", ErrorMessage = "Invalid email address")]
            public string Email { get; set; }
            public int AccountType { get; set; }

            public string Password { get; set; }
            public bool IsActive { get; set; }
            public bool IsAdmin { get; set; }
            public bool IsClient { get; set; }
            public int Credit { get; set; }

            public string CompanyName { get; set; }

            public string ClientID { get; set; }
        }
    }
}
