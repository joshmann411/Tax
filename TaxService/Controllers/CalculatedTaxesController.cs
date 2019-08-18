using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TaxService.Models;

namespace TaxService.Controllers
{
    public class CalculatedTaxesController : ApiController
    {
        private TaxCalculatorDBEntities db = new TaxCalculatorDBEntities();

        // GET: api/CalculatedTaxes
        public IQueryable<CalculatedTax> GetCalculatedTaxes()
        {
            return db.CalculatedTaxes;
        }

        // GET: api/CalculatedTaxes/5
        [ResponseType(typeof(CalculatedTax))]
        public IHttpActionResult GetCalculatedTax(int id)
        {
            CalculatedTax calculatedTax = db.CalculatedTaxes.Find(id);
            if (calculatedTax == null)
            {
                return NotFound();
            }

            return Ok(calculatedTax);
        }

        // PUT: api/CalculatedTaxes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCalculatedTax(int id, CalculatedTax calculatedTax)
        {
            if (id != calculatedTax.Id)
            {
                return BadRequest();
            }

            db.Entry(calculatedTax).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalculatedTaxExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CalculatedTaxes
        [ResponseType(typeof(CalculatedTax))]
        public IHttpActionResult PostCalculatedTax(CalculatedTax calculatedTax)
        {
            db.CalculatedTaxes.Add(calculatedTax);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CalculatedTaxExists(calculatedTax.Id))
                {
                    return Conflict();
                }
                else
                {
                    Random rand = new Random();
                    int newID = rand.Next(100000);
                    calculatedTax.Id = newID;
                    db.SaveChanges();
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = calculatedTax.Id }, calculatedTax);
        }

        // DELETE: api/CalculatedTaxes/5
        [ResponseType(typeof(CalculatedTax))]
        public IHttpActionResult DeleteCalculatedTax(int id)
        {
            CalculatedTax calculatedTax = db.CalculatedTaxes.Find(id);
            if (calculatedTax == null)
            {
                return NotFound();
            }

            db.CalculatedTaxes.Remove(calculatedTax);
            db.SaveChanges();

            return Ok(calculatedTax);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CalculatedTaxExists(int id)
        {
            return db.CalculatedTaxes.Count(e => e.Id == id) > 0;
        }
    }
}