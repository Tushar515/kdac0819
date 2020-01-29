using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineTelecom.Models;

namespace OnlineTelecom.Controllers
{
    public class OrderController : ApiController
    {
        FinalProject_DBEntities dalobject = new FinalProject_DBEntities();
        Response response = new Response();

        public OrderController()
        {
            dalobject.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Order
        public IEnumerable<T_OrderDetails> Get()
        {
            return ((dalobject.T_OrderDetails.ToList()));
        }

        // GET: api/Order/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Order

            public Response Post([FromBody]dynamic Orderdetails)
        {
            int UserId = Orderdetails.UserId;
            int ProductId = Orderdetails.ProductId;
            string OrderDetails = Orderdetails.OrderDetails.ToString();
            T_OrderDetails details = new T_OrderDetails();
            details.UserId = UserId;
            details.ProductId = ProductId;
            details.OrderDate = DateTime.Now;
            details.Quantity = OrderDetails;
            dalobject.T_OrderDetails.Add(details);
            try
            {
                dalobject.SaveChanges();
                response.Data = details;
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










        // PUT: api/Order/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Order/5
        public void Delete(int id)
        {
        }
    }
}
