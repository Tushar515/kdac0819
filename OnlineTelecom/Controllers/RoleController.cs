using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineTelecom.Models;
using System.Web.Http.Cors;
using MyLoggerLib;

namespace OnlineTelecom.Controllers
{

    public class RoleController : ApiController
    {
        FinalProject_DBEntities dalobject = new FinalProject_DBEntities();
       // GET: api/Role
        public IEnumerable<T_Roles> Get()
        {
            FileLogger flog = FileLogger.GetLogger();
            flog.Log("Logger is working fine");
            return ((dalobject.T_Roles.ToList()));
        }


        // GET: api/Role/5
        public T_Roles Get(int id)
        {
           return dalobject.T_Roles.Find(id);
        }

        // POST: api/Role
        [HttpPost]
        public void Post([FromBody]T_Roles t)
        {

            dalobject.proc_AddRole(t.RoleName);
        }

        // PUT: api/Role/5
        public void Put(int id, [FromBody]T_Roles t)
        {
            dalobject.proc_ModifyRole(id, t.RoleName);
        }

        // DELETE: api/Role/5
        public void Delete(int id)
        {
            dalobject.proc_RemoveRole(id);

        }
    }
}
