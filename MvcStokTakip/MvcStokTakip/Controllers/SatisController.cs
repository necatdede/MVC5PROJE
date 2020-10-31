using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MvcStokTakip.Models.Entity;

namespace MvcStokTakip.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var satislar = db.tblsatis.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> urun = (from x in db.tblurun.ToList() select new SelectListItem { Text = x.ad, Value = x.id.ToString() }).ToList();
            List<SelectListItem> personel = (from x in db.tblpersonel.ToList() select new SelectListItem { Text = x.ad+" "+x.soyad, Value = x.id.ToString() }).ToList();
            List<SelectListItem> musteri = (from x in db.tblmusteri.ToList() select new SelectListItem { Text = x.ad+" "+x.soyad, Value = x.id.ToString() }).ToList();
            ViewBag.urun = urun;
            ViewBag.personel = personel;
            ViewBag.musteri = musteri;
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(tblsatis p)
        {
            var urun = db.tblurun.Where(x => x.id == p.tblurun.id).FirstOrDefault();
            var personel = db.tblpersonel.Where(x => x.id == p.tblpersonel.id).FirstOrDefault();
            var musteri = db.tblmusteri.Where(x => x.id == p.tblmusteri.id).FirstOrDefault();
            p.tblurun = urun;
            p.tblpersonel = personel;
            p.tblmusteri = musteri;
            p.tarih = DateTime.Now;
            db.tblsatis.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}