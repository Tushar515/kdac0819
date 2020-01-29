using OnlineTelecom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace OnlineTelecom.Controllers
{
    public class AdminController : ApiController
    {
        Response response = new Response();
        FinalProject_DBEntities dalobject = new FinalProject_DBEntities();
 
        public AdminController()
        {
            dalobject.Configuration.ProxyCreationEnabled = false;
        }


        // GET: api/Admin
        public IEnumerable<T_Users> Get()
        {
            return ((dalobject.T_Users.ToList()));
        }


        [Route("api/Admin/ProductList")]
        public IEnumerable<T_Products> Get1()
        {
            return ((dalobject.T_Products.ToList()));
        }


        // GET: api/Admin/5
        public Response Get(int id)
        {

            List<T_Products> user = dalobject.T_Products.ToList();
            T_Products productlist = (from u in user
                                 where u.ProductId == id 
                                 select u).SingleOrDefault();

            if (productlist != null)
            {

                response.Data = productlist;
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

        // POST: api/Admin

        //Add new plan
        [HttpPost]
        // GET: api/Admin
        public Response Post(T_Products data)
        {
            dalobject.T_Products.Add(data);
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



        // PUT: api/Admin/5
        public void Put(int id, [FromBody]string value)
        {
        }


        [HttpPut]
        [Route("api/Admin/UpdateProduct")]
        public Response updateproduct([FromBody]T_Products value)
        {

            var productlist = dalobject.T_Products.ToList();
            var Product = (from u in productlist
                        where u.ProductId == value.ProductId
                        select u).SingleOrDefault();

            if (Product != null)
            {
                Product.ProductId = value.ProductId;
                Product.ProductName = value.ProductName;
                Product.ProductDescription = value.ProductDescription;
                Product.ProductPrice = value.ProductPrice;
                Product.CategoryId = value.CategoryId;
                dalobject.SaveChanges();
                Response rc = new Response();
                response.Status = "Success";
                response.Error = null;
                response.Data = Product;
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





        // DELETE: api/Admin/5
        public Response Delete(int id)
        {
            T_Products p = dalobject.T_Products.Find(id);
            dalobject.T_Products.Remove(p);
            try
            {
                dalobject.SaveChanges();
                response.Error = null;
                response.Status = "Success";
                return response;
            }
            catch (Exception ex)
            {

                response.Error = ex;
                response.Status = "Fail";
                return response;
            }
        }
    }
}
