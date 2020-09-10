using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using CEA_System.Models;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Net.Mime;
using System.IO;

namespace CEA_System.Email
{
    public class EmailHelper
    {
        public static string FromEmail = ConfigurationManager.AppSettings["FromEmail"].ToString();
        public static string FromPwd = ConfigurationManager.AppSettings["FromPwd"].ToString();
        public static string host = ConfigurationManager.AppSettings["EmailHost"].ToString();
        public static string To = ConfigurationManager.AppSettings["To"].ToString();
        public static int port = int.Parse(ConfigurationManager.AppSettings["port"]);
        CEAEntities1 context = new CEAEntities1();


        public  string SendContactUsEmail(int StudentId, int SchoolId, string Filepath)
        {

            var Filepath2 = Filepath.Substring(0, 45);
            var originalpath = Filepath2;
            var studentdata = context.Student_Profile.Where(a => a.StudentId == StudentId).Select(a => a).FirstOrDefault();

            StringBuilder body = new StringBuilder();
            body.Append(studentdata.FirstName + " " + studentdata.LastName + " " + " has applied to the school. Please check the attached admission packet.");
       


            try
            {


                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(FromEmail, "CEA Admission Packet"),

                    Subject = "Admisssion application of " + studentdata.FirstName + " " + studentdata.LastName,
                    Body = body.ToString(),
                    IsBodyHtml = true,
                };

                var filedata = context.SupportingDocs.Where(a => a.StudentId == StudentId).Select(a => a).ToList();
                foreach (var item in filedata)
                {

                    byte[] barrImg = (byte[])item.filecontent;
                    string strfn = Convert.ToString(item.TypeofDoc);
                  
                    string[] filescollection = Directory.GetFiles(originalpath, strfn, SearchOption.AllDirectories);
                    foreach (var item1 in filescollection)
                    {
                        System.IO.File.Delete(item1);
                    }
                    Filepath2 = originalpath + strfn;
                    FileStream fs = new FileStream(Filepath2, FileMode.CreateNew, FileAccess.Write);
                    fs.Write(barrImg, 0, barrImg.Length);

                    fs.Flush();
                    fs.Close();
                    fs.Dispose();

                    Attachment att = new Attachment(Filepath2, MediaTypeNames.Application.Octet);
                    mail.Attachments.Add(att);
                }
                
               


                Attachment data = new Attachment(Filepath,MediaTypeNames.Application.Octet);
                // your path may look like Server.MapPath("~/file.ABC")
                mail.Attachments.Add(data);
                mail.CC.Add(studentdata.Email);
                mail.To.Add(To); // I chage the code here sender email address
                SmtpClient smtp = new SmtpClient(host, port);
                smtp.Credentials = new NetworkCredential(FromEmail, FromPwd);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                var result = "Email Sent Successfully!!"; //chage this message
                return result;

            }
            catch (Exception e1)
            {
                return "Sorry!!!! Your inquire is not submitted .Please try it one more time or later";


            }



        }


        public string SendPasswordEmail(string password,string email)
        {

            StringBuilder body = new StringBuilder();
            body.Append("Your CEA System passowrd is"+" "+password);
            try
            {


                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(FromEmail, "CEA Admission Packet"),

                    Subject = "CEA System",
                    Body = body.ToString(),
                    IsBodyHtml = true,
                };

               


             
                mail.CC.Add(email);
                mail.To.Add(To); // I chage the code here sender email address
                SmtpClient smtp = new SmtpClient(host, port);
                smtp.Credentials = new NetworkCredential(FromEmail, FromPwd);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                var result = "Email Sent Successfully!!"; //chage this message
                return result;

            }
            catch (Exception e1)
            {
                return "Sorry!!!! Your inquire is not submitted .Please try it one more time or later";


            }



        }

    }
}