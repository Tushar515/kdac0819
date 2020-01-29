using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using OnlineTelecom.Models;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Cryptography;

namespace OnlineTelecom.Controllers
{
    public class UserController : ApiController
    {
        FinalProject_DBEntities obj = new FinalProject_DBEntities();
        Response rs = new Response();
        LoginController ctrl = new LoginController();
        // GET: api/User
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        //Email sending logic
        [HttpPost]
    
             public Response Post([FromBody]Email e)
        {
            string to = e.to;
            string body = e.body;
            string subject = e.subject;

            string result = "Message Sent Successfully..!";
            string senderID = "pritidhanawade12@gmail.com";// use sender’s email id here..
            const string senderPassword = "Sunbeam@2104"; // sender password here…
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // smtp server address here…
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(senderID, to, subject, body);
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                result = "Error sending email.!!!";
                rs.Error = ex;

            }
            return rs;
  
    }

        [HttpPost]
        [Route("api/User/IsExist")]
        public Response IsExist([FromBody]T_Users value)
        {

            var userlist = obj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();
            if (User != null)
            {
                string mails = GetOTP();

                T_OTP_Details otp = new T_OTP_Details();
                otp.UserId = User.UserId;
                otp.ValidTill = DateTime.Now;
                otp.GeneratedOn = DateTime.Now;
                otp.OTP = mails;
                obj.T_OTP_Details.Add(otp);
                obj.SaveChanges();
                Email e = new Email
                {
                   to = value.EmailId,
                   body = "Your OTP is " + mails,
                   subject = "OTP for changing password ",
            };

                Post(e);
                rs.Status = "Success";
                rs.Error = null;
                rs.Data = mails;
                return rs;
            }
            else
            {
                rs.Status = "Fail";
                rs.Error = null;
                rs.Data = false;
                return rs;
            }

        }

        private string GetOTP(string otpType = "1", int len = 5)
        {
            //otptype 1 = alpha numeric
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            if (otpType == "1")
            {
                characters += alphabets + small_alphabets + numbers;
            }
            int length = 5;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }



        


        [HttpPost]
        [Route("api/User/OTP")]
        public Response OTP([FromBody]dynamic otpDetails)
        {
            string email = otpDetails.EmailId.ToString();


            var userlist = obj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == email
                        select u).SingleOrDefault();
            string o = otpDetails.OTP.ToString();

            var otpd = obj.T_OTP_Details.ToList();
            var vOTP = (from v in otpd
                        where v.UserId == User.UserId && v.OTP == o
                        select v).SingleOrDefault();

            if (vOTP != null)
            {
        
                rs.Status = "Success";
                rs.Error = null;
                rs.Data = vOTP;
                return rs;
            }
            else
            {
               
                rs.Status = "Fail";
                rs.Error = null;
                rs.Data = false;
                return rs;
            }
        }




        //forgot password
        [HttpPut]
        [Route("api/User/UpdatePassword")]
        public Response updatepassword([FromBody]T_Users value)
        {

            var userlist = obj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();

            if (User != null)
            {
                string newpassword = ctrl.Encrypt(value.Password);
                User.Password = newpassword;
                obj.SaveChanges();
                Response rc = new Response();
                rs.Status = "Success";
                rs.Error = null;
                rs.Data = User;
                return rs;
            }
            else
            {

                rs.Status = "Fail";
                rs.Error = null;
                rs.Data = null;
                return rs;
            }
        }


        //Change Password
        [HttpPut]
        [Route("api/User/ChangePassword")]
        public Response changepassword([FromBody]dynamic value)
        {

            var userlist = obj.T_Users.ToList();
            string encrypttext = ctrl.Encrypt(value.Password.ToString());

            var User = (from u in userlist
                        where u.EmailId == value.EmailId.ToString() && u.Password == encrypttext
                        select u).SingleOrDefault();

            if (User != null)
            {
                string newpassword = ctrl.Encrypt(value.NPassword.ToString());
                User.Password = newpassword;
                obj.SaveChanges();
                Response rc = new Response();
                rs.Status = "Success";
                rs.Error = null;
                rs.Data = User;
                return rs;
            }
            else
            {

                rs.Status = "Fail";
                rs.Error = null;
                rs.Data = null;
                return rs;
            }
        }


        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }

    }


    public class UploadApiController : ApiController
    {


        [HttpPost]
        [Route("api/User/Upload")]
        public async Task<HttpResponseMessage> PostFormData()
        {

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Photos");
            var provider = new CustomMultipartFormDataStreamProvider(root);
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                Response responseStatus = new Response() { Status = "Success", Error = null };
                return Request.CreateResponse(HttpStatusCode.OK, responseStatus);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
        }
    }






}
