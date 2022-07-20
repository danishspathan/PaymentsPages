using BusinessAccessLayer;
using PaymentGateWayApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PaymentGateWayApp.Models.UserDetails;

namespace PaymentGateWayApp.Data
{
    public class UserRepository
    {

       
        public  void insertHostedPagesID(string userName, string Password,string hostedpageId)
        {

            

            using (var context = new UserContext())
            {
                var result = context.userClasses.Where(r => r.UserName == userName && r.Password == Password).FirstOrDefault();
                if (result != null)
                {
                    result.HostedPageID= hostedpageId;
                    context.SaveChanges();
                }
              
            }
         
        }
        public bool checkIfUserxists(string emailID)
        {



            using (var context = new UserContext())
            {
                var result = context.userClasses.Where(r => r.Email == emailID ).FirstOrDefault();
                if (result != null)
                {
                  return true;
                }

            }
            return false;
        }
        public string plan(string emailID,string password)
        {



            using (var context = new UserContext())
            {
                var result = context.userClasses.Where(r => r.Email == emailID && r.Password==password).FirstOrDefault();
                if (result != null)
                {
                    return result.UserPlan;
                }

            }
            return null;
        }
        public void insertUser(string strfirstName, string lastName, string emailid, string password,string Company)
        {
            using (var context = new UserContext())
            {
                var t = new UserInfo //Make sure you have a table called test in DB
                {
                    Firstname = strfirstName,
                    LastName = lastName,
                    Email = emailid,
                    Password = password,
                    CompanyName = Company,
                    UserName = emailid,
                    IsActive= true ,
                   
                };
                context.userClasses.Add(t);

                context.SaveChanges();

            }
        }

        // Checking if the users credit has been set according to the plan 13012022
        public bool checkIfCreditSet(string userName, string Password)
        {
            bool check = false;


            using (var context = new UserContext())
            {
                var result = context.userClasses.Where(r => r.UserName == userName && r.Password == Password).FirstOrDefault();
                if (result != null)
                {
                    if (result.CreditSet != null)
                        check = true;
                }

            }
            return check;

        }
        // setting the user credit according to the plan 13012022
        public void SetCredit(string userName, string Password, int credit, string userPlan, string userRenewal)
        {

            using (var context = new UserContext())
            {
                var result = context.userClasses.Where(r => r.UserName == userName && r.Password == Password).FirstOrDefault();
                if (result != null)
                {
                    result.Credit = credit;
                    result.CreditSet = "Y";
                    result.UserPlan = userPlan;
                    result.PlanRenewal = userRenewal;
                    context.SaveChanges();
                }

            }
        }
        // Getting  the hostedpagesID for user
        public string GetHostedPageID(string userName, string Password)
        {
            string hostedPagesID = string.Empty;

            using (var context = new UserContext())
            {
                var result = context.userClasses.Where(r => r.UserName == userName && r.Password == Password).FirstOrDefault();
                if (result != null)
                {
                    hostedPagesID = result.HostedPageID;
                }

            }
            return hostedPagesID;
        }


        public UserInfo GetUserInfo(string userName)
        {
            using (var context = new UserContext())
            {
                UserInfo result = context.userClasses.Where(r => r.UserName == userName).FirstOrDefault();
                return result;
            }
        }
    }

}