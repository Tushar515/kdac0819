using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineTelecom.Models;

namespace OnlineTelecom.Controllers
{
    public class ProductController : ApiController
    {
        FinalProject_DBEntities dalobject = new FinalProject_DBEntities();
        Response response = new Response();

        public ProductController()
        {
            dalobject.Configuration.ProxyCreationEnabled = false;
        }



        // GET: api/Product
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Product/5

        public Response get(int id)
        {

            var ProductList = dalobject.T_Products.ToList();
            var List = (from product in ProductList
                        where product.ProductId == id
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


        // POST: api/Product
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Product/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Product/5
        public void Delete(int id)
        {
        }
    }
}
