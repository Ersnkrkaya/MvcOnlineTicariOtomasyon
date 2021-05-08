using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

using PagedList;
using PagedList.Mvc;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index(int sayfa=1)
        {
            var kategori = c.Kategoris.ToList().ToPagedList(sayfa,3);
            return View(kategori);
        }
        [HttpGet]// sayfa yüklendiğinde çalışan method
        public ActionResult YeniKategori()
        {
            return View();//sayfa yüklendiğinde veri eklemeyle ilgili herhangi bir işlem yapmak istemiceğimiz ve boş verilerin eklenmesini engellemek istediğimiz için sadece sayfayı döndürmesini
            //istiyoruz
        }
        [HttpPost]// sayfa post edildiğinde çalışacak method
        public ActionResult YeniKategori(Kategori k)
        {
            c.Kategoris.Add(k);//context sınıfından türetilen c'den Kategori tablosuna Add işlemi yaparak eklemesini istiyoruz.
            c.SaveChanges();//değişiklikler kaydedilsin
            return RedirectToAction("Index");// Index sayfasına gönderilsin
        }
        public ActionResult KategoriSil(int id)
        {
            var sil = c.Kategoris.Find(id);
            c.Kategoris.Remove(sil);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var kategori = c.Kategoris.Find(id);
            return View("KategoriGetir", kategori);
            
        }
        public ActionResult KategoriGuncelle(Kategori k)
        {
            var ktg = c.Kategoris.Find(k.KategoriID);
            ktg.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Deneme()
        {
            Class3 cs = new Class3();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "Urunid", "UrunAd");
            return View(cs);

        }
        public JsonResult UrunGetir(int p)
        {
            var urunlistesi = (from x in c.Uruns
                               join y in c.Kategoris
                               on x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID == p
                               select new
                               {
                                   Text = x.UrunAd,
                                   Value = x.Urunid.ToString()
                               });
            return Json(urunlistesi, JsonRequestBehavior.AllowGet);
        }
    }
}