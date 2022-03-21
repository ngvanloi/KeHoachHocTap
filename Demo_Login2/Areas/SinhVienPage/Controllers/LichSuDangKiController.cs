using Demo_Login2.Areas.AdminPage.Business;
using Demo_Login2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Login2.Areas.SinhVienPage.Controllers
{
    public class LichSuDangKiController : Controller
    {
        // GET: SinhVienPage/LichSuDangKi
        public ActionResult Index()
        {
            HttpCookie idAcc = HttpContext.Request.Cookies.Get("idAccount");
            var idAccount = Convert.ToInt32(idAcc.Value);

            var lstlichsu = this.LayLichSuDangKi(idAccount);
            ViewBag.hocki = LayDanhSachHocKi();
            return View(lstlichsu);
        }

        public List<HocKiDTO> LayDanhSachHocKi()
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayDanhSachHocKi();
            }
        }

        public List<MonHocSinhVienDangKiDTO> LayLichSuDangKi(int idaccount)
        {
            using(MonHocSinhVienDangKiBusiness bs = new MonHocSinhVienDangKiBusiness())
            {
                return bs.LayLichSuDangKi(idaccount);
            }
        }
    }
}