using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokTakip.Models.Entity;

namespace MvcStokTakip.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index()
        {
            var kategoriler = db.tblkategori.ToList();
            return View(kategoriler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        
        }

        [HttpPost]
        public ActionResult YeniKategori(tblkategori p)
        {
            db.tblkategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(int id)
        {
            var ktg = db.tblkategori.Find(id);
            db.tblkategori.Remove(ktg);
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.tblkategori.Find(id);
            return View("KategoriGetir",ktg);
        }
        public ActionResult KategoriGuncelle(tblkategori k)
        {
            var ktg = db.tblkategori.Find(k.id);
            ktg.ad = k.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}