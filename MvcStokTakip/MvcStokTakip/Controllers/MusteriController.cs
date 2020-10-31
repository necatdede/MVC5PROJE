using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakip.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MvcStokTakip.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(int sayfa=1)
        {
            var musteri = db.tblmusteri.Where(x=>x.durum==true).ToList().ToPagedList(sayfa, 5);
            return View(musteri);
        }
        [HttpGet]
        public ActionResult YeniMusteri() 
        {
            return View();
        
        }

        [HttpPost]
        public ActionResult YeniMusteri(tblmusteri p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            p.durum = true;
            db.tblmusteri.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult MusteriSil(tblmusteri p)
        {
            var musteri = db.tblmusteri.Find(p.id);
            musteri.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.tblmusteri.Find(id);
            return View("MusteriGetir", musteri);
        }

        public ActionResult MusteriGuncelle(tblmusteri p)
        {
            var musteri = db.tblmusteri.Find(p.id);
            musteri.ad = p.ad;
            musteri.soyad = p.soyad;
            musteri.sehir = p.sehir;
            musteri.bakiye = p.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}