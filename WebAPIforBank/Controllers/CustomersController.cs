using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPIforBank.Models;

namespace WebAPIforBank.Controllers
{
    public class CustomersController : ApiController
    {

        private APIBankEntities db = new APIBankEntities();


        //api/{controller}
        public IQueryable<Customers> GetCustomers()
        {
            return db.Customers;
        }

        //api/{controller}/{id}
        [ResponseType(typeof(Customers))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customers customers = db.Customers.Find(id);
            if (customers==null)
            {
                return NotFound();
            }
            return Ok(customers);
        }

        //POST api/{controller}
        [ResponseType(typeof(Customers))]
        public IHttpActionResult PostCustomer(Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            db.Customers.Add(customers);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id=customers.UserId},customers);
        }

        //DELETE api/{controller}{id}
        [ResponseType(typeof(Customers))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customers customers = db.Customers.Find(id);
            if (customers ==null)
            {
                return NotFound();
            }
            db.Customers.Remove(customers);
            db.SaveChanges();
            return Ok(customers);
        }
        //PUT api/{controller}/{id}
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != customers.UserId)
            {
                return BadRequest(ModelState);
            }
            db.Entry(customers).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
