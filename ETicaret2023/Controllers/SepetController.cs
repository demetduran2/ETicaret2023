using ETicaret2023.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret2023.Controllers
{
    [Authorize]
    public class SepetController : Controller
    {
        ETicaretEntities db = new ETicaretEntities();
        // GET: Sepet
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SepeteEkle(int id,int adet)
        {
            string kulID = User.Identity.GetUserId();//login olan kullanıcının idsiini alır
            Sepet sepettekiurun=db.Sepet.FirstOrDefault(x=>x.UrunID==id && x.KullaniciID==kulID);

            Urunler urun = db.Urunler.Find(id);

            if (sepettekiurun==null)
            {
                Sepet yeniurun = new Sepet()
                {
                    KullaniciID = kulID,
                    UrunID = id,
                    Adet = adet,
                    ToplamTutar = adet*urun.UrunFiyati
                };
                db.Sepet.Add(yeniurun);
            }
            else
            {
                sepettekiurun.Adet = sepettekiurun.Adet + adet;
                sepettekiurun.ToplamTutar=sepettekiurun.Adet*urun.UrunFiyati;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}