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
using System.Web.Mvc;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    public class UrunController : ApiController
    {
        private ETicaretEntities db = new ETicaretEntities();

        //[HttpGet]
        // GET: api/Urun
        public List<Urunler> GetUrunler()
        {
            db.Configuration.ProxyCreationEnabled= false;
            //db.Configuration.LazyLoadingEnabled = false;
            //var urunler = db.Urunler.Include(x => x.Kategoriler);
            //List<Urunler>u=urunler.ToList();
            return db.Urunler.ToList();
        }

        // GET: api/Urun/5
        [ResponseType(typeof(Urunler))]
        public IHttpActionResult GetUrunler(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return NotFound();
            }

            return Ok(urunler);
        }

        // PUT: api/Urun/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUrunler(int id, Urunler urunler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != urunler.UrunID)
            {
                return BadRequest();
            }

            db.Entry(urunler).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrunlerExists(id))
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

        // POST: api/Urun
        [ResponseType(typeof(Urunler))]
        public IHttpActionResult PostUrunler(Urunler urunler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Urunler.Add(urunler);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = urunler.UrunID }, urunler);
        }

        // DELETE: api/Urun/5
        [ResponseType(typeof(Urunler))]
        public IHttpActionResult DeleteUrunler(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return NotFound();
            }

            db.Urunler.Remove(urunler);
            db.SaveChanges();

            return Ok(urunler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UrunlerExists(int id)
        {
            return db.Urunler.Count(e => e.UrunID == id) > 0;
        }
    }
}