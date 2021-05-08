using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        Context c = new Context();
        public ActionResult Index()
        {
            var deger1 = c.Carilers.Count().ToString();//carileri say ve string olarak al 
            ViewBag.d1 = deger1;
            
            var deger2 = c.Uruns.Count().ToString();//ürünleri say ve string olarak al 
            ViewBag.d2 = deger2;
            
            var deger3 = c.Personels.Count().ToString();//personeli say ve string olarak al 
            ViewBag.d3 = deger3;

            var deger4 = c.Kategoris.Count().ToString();//kategorileri say ve string olarak al 
            ViewBag.d4 = deger4;

            var deger5 = c.Uruns.Sum(x => x.Stok).ToString();//Toplam ürün stok sayısını say ve string olarak al 
            ViewBag.d5 = deger5;

            var deger6 = (from x in c.Uruns select x.Marka).Distinct().Count().ToString();//ürünler de ki markayı seç; Benzersiz olsun seçilenler, saydırsın ve string'e çevirsin
            ViewBag.d6 = deger6;

            var deger7 = c.Uruns.Count(x => x.Stok<=20).ToString();// stok sayısı 20'nin altında olan ürünleri seç
            ViewBag.d7 = deger7;

            var deger8 = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();//Fiyatı en yüksek ürünü bulmaya çalışıyoruz.
            // Ürünleri fiyatına göre descending yani tersten sırala ve FirstOrDefault yöntemi ile ilk gelen değeri ver yani max fiyatlıyı
            ViewBag.d8 = deger8; 

            var deger9 = (from x in c.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();//Fiyatı en düşük ürünü bulmaya çalışıyoruz.
            // Ürünleri fiyatına göre ascending yani a-z olarak sırala ve FirstOrDefault yöntemi ile ilk gelen değeri ver yani min fiyatlıyı
            ViewBag.d9 = deger9;

            var deger10 = c.Uruns.Where(x => x.UrunAd == "Buzdolabı").Count().ToString();//urunad'ı buzdolabı olan ürünleri say. Toplam Buzdolabı Sayısı
            ViewBag.d10 = deger10; 
            var deger11 = c.Uruns.Where(x => x.UrunAd == "Laptop").Count().ToString();//urunad'ı Laptop olan ürünleri say. Toplam Laptop Sayısı
            ViewBag.d11 = deger11;

            var deger12 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.d12 = deger12;

            

            var deger14 = c.SatisHarekets.Sum(x => x.ToplamTutar).ToString();//kasada ki toplam tutar
            ViewBag.d14 = deger14;

            DateTime bugun = DateTime.Today;
            var deger15 = c.SatisHarekets.Count(x => x.Tarih ==bugun).ToString();// bugün satılan ürünlerin toplamı
            ViewBag.d15 = deger15;

            var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(y =>(decimal?) y.ToplamTutar).ToString();//bugün satılan ürünlerin toplam tutarı
            ViewBag.d16 = deger16;
            return View();
        }
        public ActionResult KolayTablolar()
        {
            var sorgu = (from x in c.Carilers group x by x.CariSehir into g
                         select new SinifGrup
                         {
                             Sehir = g.Key,
                             Sayi=g.Count()
                         });
            return View(sorgu.ToList());
        }
        public PartialViewResult Partial1()
        {
            var sorgu2 = (from x in c.Personels
                          group x by x.Departman.DepartmanAd into g
                          select new SinifGrup2
                          {
                              Departman = g.Key,
                              Sayi = g.Count()
                          });
            return PartialView(sorgu2.ToList());
        }
        public PartialViewResult Partial2()
        {
            var sorgu = c.Carilers.ToList();
            return PartialView(sorgu);
        }
        public PartialViewResult Partial3()
        {
            var sorgu = c.Uruns.ToList();
            return PartialView(sorgu);
        }
        public PartialViewResult Partial4()
        {
            var sorgu = (from x in c.Uruns
                          group x by x.Marka into g
                          select new SinifGrup3
                          {
                              marka = g.Key,
                              sayi = g.Count()
                          });
            return PartialView(sorgu.ToList());
        }
    }
}