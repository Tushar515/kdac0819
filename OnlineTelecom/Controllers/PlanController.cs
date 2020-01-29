using OnlineTelecom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineTelecom.Controllers
{
    public class PlanController : ApiController
    {
        FinalProject_DBEntities dalobject = new FinalProject_DBEntities();
        Response response = new Response();

        public PlanController()
        {
            dalobject.Configuration.ProxyCreationEnabled = false;
        }



        // GET: api/Plan
        public Response Get()
        {

            var ProductList = dalobject.T_Products.ToList();
            var List = (from product in ProductList
                        where product.CategoryId == 1
                        select product).ToList();
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



        [Route("api/Plan/Hardware")]
        public Response Get1()
        {

            var ProductList = dalobject.T_Products.ToList();
            var List = (from product in ProductList
                        where product.CategoryId == 2
                        select product).ToList();
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




        // GET: api/Plan/5
        public Response Get(int id)
        {

            var ProductList = dalobject.T_Products.ToList();
            var List = (from product in ProductList
                        where product.CategoryId == id
                        select product).ToList();
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

        // POST: api/Plan
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Plan/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Plan/5
        public void Delete(int id)
        {
        }
    }
}
