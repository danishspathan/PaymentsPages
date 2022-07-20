using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PaymentGateWayApp.Models
{

    public class DemoModel
    {
        [StringLength(10,MinimumLength =3)]
        [Required(ErrorMessage = "Please enter a valid first name")]
        public string FirstName { get; set; }

        [StringLength(10, MinimumLength = 3)]
        [Required(ErrorMessage = "Please enter a valid last name")]
        public string LastName { get; set; }


        //[StringLength(10, MinimumLength = 3)]
        //[Required(ErrorMessage = "Please enter a valid company")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Please enter a valid email")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please Select a valid plan")]
        public string Plans { get; set; }
     

        [Required]
        [Display(Name = "Country")]
        public string currency { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> selectPlan { get; set; }

        public string hiddenCountry { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }
        public string jobTitle { get; set; }
        public string planRadio { get; set; }


    }

    [Table("tblCountry")]
    public class CountryClass
    {
        [Key]
        public string Country { get; set; }
        public string Currency { get; set; }
        public string TwoDigitCode { get; set; }
        public string ThreeDIgitCode { get; set; }
    }

    //Zoho Subscriptions all mandatory fields for creating a subscriptions
    //public class ZohoSubcriptionParameters
    //{

    //    public string subscription_id { get; set; }
    //    public string name { get; set; }
    //    public string status { get; set; }

    //    public string amount { get; set; }
    //    public string created_at { get; set; }
    //    public string current_term_starts_at { get; set; }
    //    public string current_term_ends_at { get; set; }
    //    public string last_billing_at { get; set; }
    //    public string expires_at { get; set; }
    //    public string interval { get; set; }
    //    public Boolean auto_collect { get; set; }

    //    public string created_time { get; set; }
    //    public string updated_time { get; set; }
    //    public string reference_id { get; set; }
    //    public string place_of_supply { get; set; }

    //    public string salesperson_id { get; set; }
    //    public string salesperson_name { get; set; }
    //    public string child_invoice_id { get; set; }


    //    public string currency_code { get; set; }
    //    public string end_of_term { get; set; }

    //    public string product_id { get; set; }

    //    public string product_name { get; set; }
    //    public string plan { get; set; }
    //    public List<string> addons { get; set; }
    //    public string card { get; set; }
    //    public int payment_terms { get; set; }
    //    public string payment_terms_label { get; set; }
    //    public Boolean can_add_bank_account { get; set; }

    //    public List<string> customer { get; set; }
    //    public List<string> custom_fields { get; set; }
    //    public List<string> taxes { get; set; }
    //    public List<string> contactpersons { get; set; }
    //    public List<string> notes { get; set; }
    //    public List<string> payment_gateways { get; set; }



    //    public string notesunbilled_charge_id { get; set; }




    //}


}