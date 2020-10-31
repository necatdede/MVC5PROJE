using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MvcStokTakip.Models.Entity;

namespace MvcStokTakip.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(string p)
        {
            var urunler = db.tblurun.Where(x => x.durum == true);
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.ad.Contains(p) && x.durum == true);
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktg = (from x in db.tblkategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop = ktg;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(tblurun t)
        {
            var ktgr = db.tblkategori.Where(x => x.id == t.tblkategori.id).FirstOrDefault();
            t.tblkategori = ktgr;
            db.tblurun.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kat = (from x in db.tblkategori.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString()
                                         }).ToList();
            ViewBag.urunkategori = kat;
            var urn = db.tblurun.Find(id);
            return View("UrunGetir",urn);
        }

        public ActionResult UrunGuncelle(tblurun p)
        {
            var urun = db.tblurun.Find(p.id);
            urun.marka = p.marka;
            urun.ad = p.ad;
            urun.stok = p.stok;
            urun.alisfiyat = p.alisfiyat;
            urun.satisfiyat = p.satisfiyat;
            var ktg = db.tblkategori.Where(x => x.id == p.tblkategori.id).FirstOrDefault();
            urun.kategori = ktg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }

        public ActionResult UrunSil(tblurun p)
        {
            var urunbul = db.tblurun.Find(p.id);
            urunbul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }

    }
}