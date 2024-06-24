using ArtHandler.DAL;
using ArtHandler.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Repository
{
    public class Email
    {
        string Rootpath = string.Empty;
        string Imagepath = string.Empty;
        string EmailTemplatePath = string.Empty;
        string EmailFromUserName = string.Empty;
        string NetWorkUserName = string.Empty;
        string NetWorkPassword = string.Empty;
        string SMTPHost = string.Empty;
        string SMTPPort = string.Empty;
        string Subject = string.Empty;
        string Domain = string.Empty;

        public Email()
        {
            Rootpath = AppDomain.CurrentDomain.BaseDirectory;
            Imagepath = System.IO.Path.Combine(Rootpath, "Images\\ArtImages\\");
            EmailTemplatePath = System.IO.Path.Combine(Rootpath, "Template\\");

            using (DataTable dt = new DAL_Settings().GetArtSMTP())
            {
                if (dt.Rows.Count > 0)
                {
                    EmailFromUserName = Convert.ToString(dt.Rows[0]["EmailFromUserName"]);
                    NetWorkUserName = Convert.ToString(dt.Rows[0]["NetWorkUserName"]);
                    NetWorkPassword = Utility.Encryptor.Decrypt(Convert.ToString(dt.Rows[0]["NetWorkPassword"]), Constants.PASSPHARSE);
                    SMTPHost = Convert.ToString(dt.Rows[0]["SMTPHost"]);
                    SMTPPort = Convert.ToString(dt.Rows[0]["SMTPPort"]);
                    Subject = Convert.ToString(dt.Rows[0]["Subject"]);
                    Domain = Convert.ToString(dt.Rows[0]["Domain"]);
                }
            }
        }

        private string PopulateBody(string userName)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Template/emailer.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            return body;
        }

        public bool SendHtmlFormattedEmail(string userName, string recepientEmail)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(EmailFromUserName);
                    mailMessage.Subject = Subject;
                    mailMessage.Body = PopulateBody(userName);
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(new MailAddress(recepientEmail));
                    
                    // create image resource from image path using LinkedResource class..
                    //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailMessage.Body, null, "text/html");

                    //LinkedResource imageResource = new LinkedResource(Imagepath + "Logo.png", "image/jpeg");
                    //imageResource.ContentId = "logoIMG";
                    //imageResource.TransferEncoding = TransferEncoding.Base64;
                    //htmlView.LinkedResources.Add(imageResource);

                    //imageResource = new LinkedResource(Imagepath + "banner.png", "image/jpeg");
                    //imageResource.ContentId = "bannerIMG";
                    //imageResource.TransferEncoding = TransferEncoding.Base64;
                    //htmlView.LinkedResources.Add(imageResource);

                    SmtpClient smtp = new SmtpClient();
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = NetWorkUserName;
                    NetworkCred.Password = NetWorkPassword;
                    NetworkCred.Domain = Domain;
                    smtp.Host = SMTPHost;
                    smtp.Port = Convert.ToInt32(SMTPPort);
                    //smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.UseDefaultCredentials = true;

                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    
                    //mailMessage.AlternateViews.Add(htmlView);
                    
                    smtp.Send(mailMessage);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException("SendHtmlFormattedEmail", ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
    }
}
