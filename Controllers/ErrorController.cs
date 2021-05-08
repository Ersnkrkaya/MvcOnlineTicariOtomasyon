using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult PageError()
        {
            Response.TrySkipIisCustomErrors = true;//kullanıcıdan gelen hata kodunu aktif ediyoruz
            return View();
        }
        public ActionResult Page400()
        {
            Response.StatusCode = 400;//geri dönen hata codu 400 ise
            Response.TrySkipIisCustomErrors = true;// çalıştır
            return View("PageError");//bu sayfaya yönlendir
        }
        public ActionResult Page403()
        {
            Response.StatusCode = 403;//geri dönen hata codu 403 ise
            Response.TrySkipIisCustomErrors = true;//çalıştır
            return View("PageError");// bu sayfaya yönlendir. 
           //Tüm hata sayfalarında mantık aynıdır, sadece hata kodu değişir

        }
        public ActionResult Page404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }
    }
}