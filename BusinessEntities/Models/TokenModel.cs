using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class TokenModel
    {
        public int ID { get; set; }

        public string EmailID { get; set; }

        public string FirstName { get; set; }

        public string OTP { get; set; }

        public string UserEmail { get; set; }

        public string IPAddress { get; set; }

        public string MacAddress { get; set; }

        public string BrowserName { get; set; }

        public string BrowserVersion { get; set; }

        public int ClientID { get; set; }

        public string LastName { get; set; }
        public string Password { get; set; }
        
    }
}
