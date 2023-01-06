using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApiProject.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace WebApiProject.Controllers
{
    
    public class KategoriController : ApiController
    {
        ETicaretEntities db = new ETicaretEntities();

        [HttpGet]
        public List<Kategoriler> Get()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Kategoriler> liste = db.Kategoriler.ToList();
            return liste;
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            Kategoriler kategori = db.Kategoriler.Find(id);
            Kategori kat = new Kategori()
            {   
                Id=kategori.KategoriID,
                Name=kategori.KategoriAdi
            };
            return Ok(kat);
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Post([FromBody]Kategoriler kategori)
        {
            db.Kategoriler.Add(kategori);
            db.SaveChanges();
            return Ok();
        }
    }
}
