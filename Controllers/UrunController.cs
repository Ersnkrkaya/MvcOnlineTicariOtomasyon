using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var urun = from k in c.Uruns select k; // Sağ-> db de ki TBLKITAP tablosunda ki veriler seçilir ve bu veriler k değişkenine aktarılır. K değişkeni de
            //kitaplar değişkenine aktarılar.
            if (!string.IsNullOrEmpty(p))//gelen p parametresinin içi boş değilse 
            {
                urun = urun.Where(m => m.UrunAd.Contains(p));// p değişkeninden gelen değer kitaplar tablosunda contains methoduyla AD sütununda ki değerle search edilir
            }
            //var kitaplar = db.TBLKITAP.ToList();

            return View(urun.ToList());

            //var urunler = c.Uruns.Where(u=>u.Durum==true).ToList();
            //return View(urunler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from i in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.KategoriAd,
                                               Value = i.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun u)
        {
            c.Uruns.Add(u);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var urun = c.Uruns.Find(id);
            urun.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from i in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.KategoriAd,
                                               Value = i.KategoriID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var urundeger = c.Uruns.Find(id);
            return View("UrunGetir", urundeger);
        }
        public ActionResult UrunGuncelle(Urun p)
        {
            var urun = c.Uruns.Find(p.Urunid);
            urun.AlisFiyat = p.AlisFiyat;
            urun.Durum = p.Durum;
            urun.Kategoriid = p.Kategoriid;
            urun.Marka = p.Marka;
            urun.SatisFiyat = p.SatisFiyat;
            urun.Stok = p.Stok;
            urun.UrunAd = p.UrunAd;
            urun.UrunGorsel = p.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();
            var urundeger = c.Uruns.Find(id);
            ViewBag.dgr3 = deger3;
            ViewBag.dgr1 = urundeger.Urunid;
            ViewBag.dgr2 = urundeger.SatisFiyat;
            return View();
        }

        [HttpPost]
        public ActionResult SatisYap(SatisHareket s)
        {

            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index","Satis");
        }
    }
}