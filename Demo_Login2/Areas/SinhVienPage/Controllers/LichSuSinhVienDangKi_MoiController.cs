using Demo_Login2.Areas.AdminPage.Business;
using Demo_Login2.Areas.SinhVienPage.Business;
using Demo_Login2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Login2.Areas.SinhVienPage.Controllers
{
    public class LichSuSinhVienDangKi_MoiController : Controller
    {
        // GET: SinhVienPage/LichSuSinhVienDangKi_Moi
        public ActionResult Index()
        {
            HttpCookie idAcc = HttpContext.Request.Cookies.Get("idAccount");
            var idAccount = Convert.ToInt32(idAcc.Value);

            ViewBag.LichSu = LayLichSuDangKiAll(idAccount);
            ViewBag.hocki = LayDanhSachHocKi();
            return View();
        }
        public List<HocKiDTO> LayDanhSachHocKi()
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayDanhSachHocKi();
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> LayLichSuDangKiAll(int idaccount)
        {
            using (SinhVienDangKiKeHoachHocTapBusiness bs = new SinhVienDangKiKeHoachHocTapBusiness())
            {
                return bs.LayLichSuDangKiAll(idaccount);
            }
        }
    }
}