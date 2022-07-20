using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaymentGateWayApp.ViewModels
{
    public class Model
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
        public class ImportExcelVM
        {
            public HttpPostedFileBase ExcelFile { get; set; }
        }
        public class UserLogin
        {
          
            public string UserName { get; set; }
            [Required (ErrorMessageResourceName = "RequiredField")]
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
        public class UserRegistrationVM
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
        }

        public class DashboardVM
        {
            public string Region { get; set; }
            public int RegionCount { get; set; }
        }

        public class StatesVM
        {
            public int StateID { get; set; }
            public string StateName { get; set; }
        }

        public class ClientDetails
        {
            public int ClientID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserName { get; set; }
            public string CompanyName { get; set; }
            public string DrpCompanyName { get; set; }
            public string Provider { get; set; }
            public string ClientStatus { get; set; }


            public string Password { get; set; }
            public string Isadmin { get; set; }
            public string Isclient { get; set; }

            public int AccountType { get; set; }
            public string Telephone { get; set; }
            public string Email { get; set; }

            public string Isactive { get; set; }

        }
        public class ClientDetailsList
        {
            List<ClientDetails> lstclientDetails { get; set; }
        }

        public class ClientDataMaster
        {
            public int MasterID { get; set; }
            public int ClientID { get; set; }
            public string ClientName { get; set; }
            public string ExcelFileName { get; set; }
            public bool isViewChecked { get; set; }
            public bool isDownloadChecked { get; set; }
            public string Companyname { get; set; }

        }

        public class ClientDataVM
        {
            public int ClientID { get; set; }
            public string ClientName { get; set; }

        }

        public class ClientAuthorizedValuesVM
        {
            public bool isViewChecked { get; set; }
            public bool isDownloadChecked { get; set; }
            public ClientDataVM clientDataVM { get; set; }
            public HttpPostedFileBase ExcelFile { get; set; }

        }

        public class List_Authorization
        {
            public int ID { get; set; }
            public string HeaderName { get; set; }
            public string PageName { get; set; }
            public string ControllerName { get; set; }
            public bool IsAdmin { get; set; }
            public bool IsClient { get; set; }
            public bool IsActive { get; set; }

        }

        public class List_Controller
        {
            public int CID { get; set; }
            public string ControllerName { get; set; }
            public string HeaderName { get; set; }
            public string HeaderIcon { get; set; }

            public bool IsAdmin { get; set; }
            public bool IsClient { get; set; }

        }

        public class List_ActionName
        {
            public int AID { get; set; }
            public string ActionName { get; set; }
            public int CID { get; set; }

            public bool IsAdmin { get; set; }
            public bool IsClient { get; set; }

        }

        public class List_LeftSidePaneVM
        {
            public List_Controller List_ { get; set; }
            public List<List_ActionName> List_Action { get; set; }

            public List<ClientDetails> lstcd { get; set; }
            public bool IsAdmin { get; set; }
            public bool IsClient { get; set; }

        }

        public class CompanyName_List
        {
            public int ClientID { get; set; }
            public string CompanyName { get; set; }
        }

        public class OTPDetails
        {
            public string otp { get; set; }
            public bool TrustedDevice { get; set; }

        }
        public class EmailExcelFile
        {
            public string EmailId { get; set; }
        }
        public class ContactsVM
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string hunter_email { get; set; }
            public string OtherDetails { get; set; }
            public string CacheAllFalseAccepted { get; set; }
            public string validation_response { get; set; }
            public string Position { get; set; }
            public string CompanyName { get; set; }
            public string Website { get; set; }
            public string Country { get; set; }
            //public string JobFunction { get; set; }
            public string Industry { get; set; }
            public string Telephone { get; set; }
            public string Location { get; set; }
            public string CONTACT_IMAGE_UPLEAD_URL { get; set; }
            public string MasterCompanyRecord_Id { get; set; }
            public string COMPANY_IMAGE_UPLEAD_URL { get; set; }
            public string ContactGuid { get; set; }
            public string CompanyGuid { get; set; }

            public string Type { get; set; }
            public string SICCode { get; set; }
            public string Ticker { get; set; }
            public string NAICSCode { get; set; }
            public string AlexaRanking { get; set; }
            public string Revenue { get; set; }
            public string FortuneRanking { get; set; }
            public string Employees { get; set; }
            public string YearFounded { get; set; }
            public string Description { get; set; }
            public string Technologies { get; set; }
            public string Competitor { get; set; }
            public string CompanyRelatedURL { get; set; }
            public DateTime DateandTime { get; set; }
            public int CompanyRecord_id { get; set; }
            public string Fax { get; set; }


        }

        public class List_CategoryVM
        {
            public List<List_Subcategory> Listsubcategory_ { get; set; }

            public int CategoryID { get; set; }
            public string Category { get; set; }
        }
        public class List_Subcategory
        {
            public int SubcategoryID { get; set; }
            public int CategoryID { get; set; }
            public string Subcategory { get; set; }
        }

        public class CompanyVM
        {
            public string COMPANY_NAME { get; set; }

            public string INDUSTRY { get; set; }
            public string SALES { get; set; }
            public string PHONE { get; set; }
            public string LOCATION { get; set; }
            public string WEBSITE { get; set; }
            public string TITLE { get; set; }
            //public string JobFunction { get; set; }
            public string CONTACTS_AVAILABLE { get; set; }
            public string PLACE_NAME_SPLIT { get; set; }
            public string STATE_CODE_NEW { get; set; }
            public string LinkedInUrl { get; set; }
            public string Email_Id { get; set; }
            public string Hunter_Email { get; set; }
            public string validation_progress { get; set; }
            public int RECORD_ID { get; set; }
            public int Show { get; set; }
            public string COMPANY_IMAGE_UPLEAD_URL { get; set; }
            public string CONTACT_IMAGE_UPLEAD_URL { get; set; }
            public string CompanyGuid { get; set; }
            public string ContactGuid { get; set; }
            public string OTHER_DETAILS { get; set; }
            public string MasterCompanyRecord_Id { get; set; }

            public string Type { get; set; }
            public string SICCode { get; set; }
            public string Ticker { get; set; }
            public string NAICSCode { get; set; }
            public string AlexaRanking { get; set; }
            public string Revenue { get; set; }
            public string FortuneRanking { get; set; }
            public string Employees { get; set; }
            public string YearFounded { get; set; }
            public string Description { get; set; }
            public string Technologies { get; set; }
            public string Competitor { get; set; }
            public string CompanyRelatedURL { get; set; }
            public DateTime DateandTime { get; set; }
            public int CompanyRecord_id { get; set; }
            public string Fax { get; set; }
        }



        public class MyList
        {
            public int SrNo { get; set; }
            public int ListID { get; set; }
            public int Contacts { get; set; }
            public int ClientID { get; set; }
            public string ContactList { get; set; }
            public string ListName { get; set; }
            public string AddedBy { get; set; }
            public string AddedOn { get; set; }
            public string Message { get; set; }
        }

    }
}
