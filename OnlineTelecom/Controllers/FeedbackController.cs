using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineTelecom.Models;

namespace OnlineTelecom.Controllers
{
    public class FeedbackController : ApiController
    {
        FinalProject_DBEntities obj = new FinalProject_DBEntities();
        Response rs = new Response();
        // GET: api/Feedback
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Feedback/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Feedback
        public Response Post([FromBody]T_Feedbacks value)
        {
            T_Feedbacks feedback = new T_Feedbacks();
            var userlist = obj.T_Users.ToList();
            var User = (from u in userlist
                        where u.EmailId == value.EmailId
                        select u).SingleOrDefault();

            int userid = User.UserId;

            if (User != null)
            {
                feedback.Description = value.Description;
                obj.T_Feedbacks.Add(value);
                obj.SaveChanges();
                rs.Data = value;
                rs.Error = null;
                rs.Status = "Success";
                return rs;
            }
            else
            {
                rs.Error = null;
                rs.Status = "Fail";
                return rs;
            }
        }






        // PUT: api/Feedback/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Feedback/5
        public void Delete(int id)
        {
        }
    }
}
