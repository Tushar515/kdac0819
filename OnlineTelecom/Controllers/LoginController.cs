using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineTelecom.Models;
using System.Data.Entity.Validation;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;

namespace OnlineTelecom.Controllers
{
    public class LoginController : ApiController
    {
        FinalProject_DBEntities dalobject = new FinalProject_DBEntities();

        public LoginController ()
        {
            dalobject.Configuration.ProxyCreationEnabled = false;
        }
        Response response = new Response();

        // GET: api/Login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/Login/PassHistory")]
        public IEnumerable<T_PasswordHistoryLog> Get_data()
        {
            return ((dalobject.T_PasswordHistoryLog.ToList()));
        }

        [Route("api/Login/OnlineUsers")]
        public Response get()
        {

            var UserList = dalobject.T_Users.ToList();
            var List = (from u in UserList
                        where u.IsOnline == true
                        select u).ToList();
            if (List != null)
            {
                response.Data = List;
                response.Error = null;
                response.Status = "Success";
                return response;

            }
            else
            {
                response.Status = "Fail";
                return response;

            }
        }


        // GET: api/Login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Login
        [HttpPost]
        public Response Post([FromBody]T_Users data)
        {
            if(data.EmailId != null && data.Password !=null)
            {
                string encrypttext = Encrypt(data.Password);
                data.Password = encrypttext;
                List<T_Users> user = dalobject.T_Users.ToList();
                               
                T_Users validuser = (from u in user
                                     where u.EmailId == data.EmailId && u.Password == data.Password
                                     select u).SingleOrDefault();
                if(validuser !=null)
                {

                    statusonline(validuser.UserId);
                    response.Data = validuser;
                    response.Error = null;
                    response.Status = "Success";
                    return response;
                }
                else
                {
                
                    response.Error = null;
                    response.Status = "Fail";
                    return response;

                }
                                     

            }
            else
            {
                response.Error = null;
                response.Status = "Fail";
                return response;

            }
        }

  

        //register new user
        [HttpPost]
            [Route("api/Login/Registration")]
            public Response Registration(T_Users data)
             {
               Console.WriteLine(data);
                data.RoleId = 1;

                string encrypttext = Encrypt(data.Password);
                data.Password = encrypttext;
                dalobject.T_Users.Add(data);
                //dalobject.SaveChanges();
            try
            {
                dalobject.SaveChanges();
                response.Data = data;
                response.Error = null;
                response.Status = "Success";
                return response;
            }
            catch (Exception ex)
            {
                
                response.Data = null;
                response.Error = ex;
                response.Status = "Fail";
                return response;
            }


        }

        public string Encrypt(string input)
        {
            string key = "ABCDEFGHzxyspouytklmnpqr";
            string securedText = "";
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            securedText = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            return securedText;
        }


        public string Decrypt(string input)
        {
            string key = "ABCDEFGHzxyspouytklmnpqr";
            string plainText = "";
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            plainText = UTF8Encoding.UTF8.GetString(resultArray);
            return plainText;
        }


   
        public void statusonline(int UserId)
        {

           T_Users user = dalobject.T_Users.Find(UserId);
            user.IsOnline = true;
            dalobject.SaveChanges();

        }

      
        [HttpPut]
        [Route("api/Login/statusoffline")]
        public Response statusoffline([FromBody]dynamic value)
        {

            var userlist = dalobject.T_Users.ToList();
            

            var User = (from u in userlist
                        where u.EmailId == value.EmailId.ToString()
                        select u).SingleOrDefault();

            if (User != null)
            {
               
                User.IsOnline = false;
                dalobject.SaveChanges();
                response.Status = "Success";
                response.Error = null;
                response.Data = User;
                return response;
            }
            else
            {

                response.Status = "Fail";
                response.Error = null;
                response.Data = null;
                return response;
            }
        }









        // PUT: api/Login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Login/5
        public void Delete(int id)
        {
        }
    }
}
