using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class QRController : Controller
    {
        // GET: QR
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string kod)// dışardan gelen 10 dizelik ifade. Bunlara göre qr kod üretilecek
        {
            using (MemoryStream ms = new MemoryStream())//memorystream:Dosya akışlarında kullanılan, resim oluşturma, resim çizim,qr işlemlerde kullanılan bir sınıf.
            {
                QRCodeGenerator koduret = new QRCodeGenerator();//
                QRCodeGenerator.QRCode karekod = koduret.CreateQrCode(kod, QRCodeGenerator.ECCLevel.Q);//(gönderilecek text ifadesi,)  //
                using (Bitmap resim = karekod.GetGraphic(10))//oluşturulacak qr ifadesinin görüntü çözünürlüğü. 10'dan azaldıkça boyut küçülür
                {
                    resim.Save(ms, ImageFormat.Png);// resmi kaydediyoruz png formatında
                    ViewBag.karekodimage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());//base64 formatında metinleri resme kaydediyor. MS'den gelen metni
                }
            }
            return View();
        }
    }
}