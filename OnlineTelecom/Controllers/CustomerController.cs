using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineTelecom.Models;
using MyLoggerLib;

namespace OnlineTelecom.Controllers
{
    public class CustomerController : ApiController
    {
        FinalProject_DBEntities dalObj = new FinalProject_DBEntities();

        public CustomerController()
        {
            dalObj.Configuration.ProxyCreationEnabled = false;
        }
          
        
              

        // GET: api/Customer
        public IEnumerable<T_Products> Get()
        {
            return ((dalObj.T_Products.ToList()));
        }

        // GET: api/Customer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Customer
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}
