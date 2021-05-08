using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c = new Context();
       
        public ActionResult Index(string p)
        {
            var kargo = from k in c.KargoDetays select k; // Sağ-> db de ki TBLKITAP tablosunda ki veriler seçilir ve bu veriler k değişkenine aktarılır. K değişkeni de

            if (!string.IsNullOrEmpty(p))//gelen p parametresinin içi boş değilse 
            {
                kargo = kargo.Where(m => m.TakipKodu.Contains(p));// p değişkeninden gelen değer kitaplar tablosunda contains methoduyla AD sütununda ki değerle search edilir
            }

            //var kargo = c.KargoDetays.ToList();
            return View(kargo.ToList());
        }
        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random rnd = new Random();
            string[] karakterler = {"A","B","C","D","E","F","G","H","J" };
            int k1, k2, k3;
            k1 = rnd.Next(0, 4);
            k2 = rnd.Next(0, 4);
            k3 = rnd.Next(0, 4);

            int s1, s2, s3;
            
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);

            string kod = s1.ToString() + karakterler[k1] + s2.ToString() + karakterler[k2] + s3.ToString() + karakterler[k3];
            ViewBag.TakipKod = kod;
            return View();
        }
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay k)
        {
            c.KargoDetays.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KargoTakip(string id)
        {
            
            var degerler = c.kargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }
    }
}