using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Text;

namespace AutomationData
{
    public class Common
    {
        public string GenerateOTP()
        {
            string num = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int len = num.Length;
            string otp = string.Empty;
            int otpdigit = 6;
            string finaldigit;
            int getindex;
            for (int i = 0; i < otpdigit; i++)
            {
                do
                {
                    getindex = new Random().Next(0, len);
                    finaldigit = num.ToCharArray()[getindex].ToString();
                } while (otp.IndexOf(finaldigit) != -1);
                otp += finaldigit;
            }

            return otp;
        }

        public bool SendEmailFromTemplate(TokenModel objtokenmodel, CommunicationTemplate CommunicationTemplateID)
        {
            bool status = false;
            List<object> objParam = new List<object>();
            List<object> objValues = new List<object>();

            objParam.Add("@CommunicationTemplateID");
            objValues.Add(CommunicationTemplateID);

            SqlHelper sqlHelper = new SqlHelper();

            DataSet ds = sqlHelper.GetDataUsingStoredProcedures("usp_GetCommunicationTemplateDetails", objParam.ToArray(), objValues.ToArray());

            if(ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string emailBody = ds.Tables[0].Rows[0]["Body"].ToString();
                    string masterHtml = ds.Tables[0].Rows[0]["MasterHtml"].ToString();
                    string Subject = ds.Tables[0].Rows[0]["Subject"].ToString();
                    string BCC = ds.Tables[0].Rows[0]["BCC"].ToString();
                    string CC = ds.Tables[0].Rows[0]["CC"].ToString();
                    string EmailAddress = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                    string DisplayName = ds.Tables[0].Rows[0]["DisplayName"].ToString();
                    string Host = ds.Tables[0].Rows[0]["Host"].ToString();
                    int Port = Convert.ToInt32(ds.Tables[0].Rows[0]["Port"].ToString());
                    string UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                    string Password = ds.Tables[0].Rows[0]["Password"].ToString();


                    emailBody = masterHtml.Replace("{CONTENT}", emailBody);
                    emailBody = UpdateEmailBody(objtokenmodel, emailBody);

                    status = SendEmail(objtokenmodel.EmailID, EmailAddress, Subject, emailBody, BCC, CC, DisplayName, Host, Port, UserName, Password);
                }
            }
            
            return status;
        }

        private bool SendEmail(string strTo, string strFrom, string strSubject, string strBody, string bcc, string cc,string displayName, string host,
            int port, string userName, string password)
        {

            bool blndone = false;
            try
            {
                MailMessage mMailMessage = new MailMessage();
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.From = new MailAddress(strFrom);
                string[] strToEmails = strTo.Split(',');

                for (int j = 0; j < strToEmails.Length; j++)
                {
                    if (strToEmails[j] != null && strToEmails[j] != "")
                    {
                        msg.To.Add(new MailAddress(strToEmails[j].ToString().Trim()));
                    }
                }

                if(!string.IsNullOrEmpty(bcc))
                {
                    string[] strBcc = bcc.Split(',');

                    for (int j = 0; j < strBcc.Length; j++)
                    {
                        if (strBcc[j] != null && strBcc[j] != "")
                        {
                            msg.Bcc.Add(new MailAddress(strBcc[j].ToString().Trim()));
                        }
                    }
                }

                if (!string.IsNullOrEmpty(cc))
                {
                    string[] strCc = cc.Split(',');

                    for (int j = 0; j < strCc.Length; j++)
                    {
                        if (strCc[j] != null && strCc[j] != "")
                        {
                            msg.CC.Add(new MailAddress(strCc[j].ToString().Trim()));
                        }
                    }
                }

                msg.Subject = strSubject;
                msg.Body = strBody;

                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;

                System.Net.Mail.SmtpClient mSmtpClient = new System.Net.Mail.SmtpClient(host);
                mSmtpClient.UseDefaultCredentials = false;
                
                System.Net.NetworkCredential loginInfo = new System.Net.NetworkCredential(userName, password);
                mSmtpClient.Port = port;
                mSmtpClient.Credentials = loginInfo;
                mSmtpClient.Send(msg);

                blndone = true;
            }
            catch (Exception ex)
            {
                blndone = false;
                throw ex;
            }
            return blndone;
        }


        private string UpdateEmailBody(TokenModel objTokenModel, string strbody)
        {
            TokenModel c = new TokenModel();
            c = objTokenModel;
            Type firstType = objTokenModel.GetType();
            foreach (System.Reflection.PropertyInfo propertyInfo in firstType.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    object value = propertyInfo.GetValue(objTokenModel, null);
                    if (value != null)
                    {
                        strbody = strbody.Replace("{" + propertyInfo.Name + "}", value.ToString());
                    }
                }
            }
            return strbody;
        }
    }
}
