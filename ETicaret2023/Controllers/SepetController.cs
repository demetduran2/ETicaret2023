using ETicaret2023.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            string kulID = User.Identity.GetUserId();
            return View(db.Sepet.Where(x=>x.KullaniciID==kulID).ToList());
        }

        public ActionResult SepeteEkle(int UrunID,int adet)
        {
            string kulID = User.Identity.GetUserId();//login olan kullanıcının idsiini alır
            Sepet sepettekiurun=db.Sepet.FirstOrDefault(x=>x.UrunID==UrunID && x.KullaniciID==kulID);

            Urunler urun = db.Urunler.Find(UrunID);

            if (sepettekiurun==null)
            {
                Sepet yeniurun = new Sepet()
                {
                    KullaniciID = kulID,
                    UrunID = UrunID,
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

        public ActionResult SepetGuncelle(int? id,int adet)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sepet sepet=db.Sepet.Find(id);
            if (sepet==null)
            {
                return HttpNotFound();
            }
            Urunler urun=db.Urunler.Find(sepet.UrunID);
                sepet.Adet = adet;
                sepet.ToplamTutar = sepet.Adet * urun.UrunFiyati;
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }
        
        public ActionResult Delete(int id)
        {
            Sepet sepet = db.Sepet.Find(id);
            db.Sepet.Remove(sepet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}