using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineTelecom.Models;

namespace OnlineTelecom.Controllers
{
    public class CategoryController : ApiController
    {
        Response response = new Response();
        FinalProject_DBEntities dalobject = new FinalProject_DBEntities();

        public CategoryController()
        {
            dalobject.Configuration.ProxyCreationEnabled = false;
        }


        // GET: api/Category
        public Response Get()
        {

            var CategoryList = dalobject.T_Categories.ToList();
            if (CategoryList != null)
            {
                response.Data = CategoryList;
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


        // GET: api/Category/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Category
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Category/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
        }
    }
}
