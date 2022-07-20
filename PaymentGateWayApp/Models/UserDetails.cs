using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PaymentGateWayApp.Models
{
    public class UserDetails
    {
        public class UsersDataVM
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Position { get; set; }
            public string CompanyName { get; set; }
            public string Website { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string Region { get; set; }
            public string LinkedinURL { get; set; }
            //public string JobFunction { get; set; }
            public string Industry { get; set; }
            public string Employees { get; set; }
            public string HeadQuarters { get; set; }
            public string PostalCode { get; set; }
            public string ExcelFileName { get; set; }
            public string Telephone { get; set; }
            public string Address { get; set; }
            public int TotalCount { get; set; }
        }
        public class UserLogin
        {
            public string UserName { get; set; }
            [Required(ErrorMessage = "RequiredField")]
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
        public class OTPDetails
        {
            public string otp { get; set; }
            public bool TrustedDevice { get; set; }

        }
        public class UserRegistrationVM
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
        }
        [Table("tbl_ClientDetails")]
        public class UserInfo
        {
            public string UserName { get; set; }
      
            public string Firstname { get; set; }
            public string LastName { get; set; }
         
            public string Email { get; set; }
            public int AccountType { get; set; }

            public string Password { get; set; }
            public bool IsActive { get; set; }
   
            public bool IsClient { get; set; }
            public int Credit { get; set; }

            public string CompanyName { get; set; }
           
            [Key]
            public int ClientID { get; set; }
            public string HostedPageID { get; set; }
            public string CreditSet { get; set; }
            public string UserPlan { get; set; }
            public string PlanRenewal { get; set; }
        }
    }
}