using Demo_Login2.Areas.AdminPage.Business;
using Demo_Login2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Login2.Areas.GiangVienPage.Controllers
{
    public class KetQuaLapKeHoachDangKiSinhVienTheoLopChuNhiemController : Controller
    {
        // GET: GiangVienPage/KetQuaLapKeHoachDangKiSinhVienTheoLopChuNhiem
        public ActionResult Index()
        {
            HttpCookie idlop = HttpContext.Request.Cookies.Get("idLopChuNhiem");
            var idLopChuNhiem = 0;
            List<HocKiDTO> lsthocki = new List<HocKiDTO>();
            if (String.IsNullOrEmpty(idlop.Value))
            {
                // bao loi
                ViewBag.ErrorChuNhiem = "Lỗi Giảng Viên chưa là Chủ Nhiệm";
            }else
            {
                idLopChuNhiem = Convert.ToInt32(idlop.Value); 
                lsthocki = LayDanhSachHocKi();
            }


            var lstketqua = this.LayDanhSachKetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem(idLopChuNhiem, 0, 0);
            var lstlophoc = LayDanhSachLopHocTheoID(idLopChuNhiem);
            var lstsinhvien = LayDanhSachSinhVienTheoLopChuNhiem(idLopChuNhiem);

            lsthocki.Insert(0, new HocKiDTO
            {
                ID = 0,
                TenHocKi = "Chọn Học Kì"
            });

            lstsinhvien.Insert(0, new AccountDTO
            {
                ID = 0,
                Ma = "Chọn Mã Sinh Viên"
            });
            //lstlophoc.Insert(0, new LopHocDTO
            //{
            //    ID = 0,
            //    TenLop = "Chọn Lớp Học"
            //});

            ViewData["LopHoc"] = new SelectList(lstlophoc, "ID", "TenLop");
            ViewData["HK"] = new SelectList(lsthocki, "ID", "TenHocKi");
            ViewData["SV"] = new SelectList(lstsinhvien, "ID", "Ma");

            Session["lstketquakehoach"] = lstketqua;
            Session["idHocKi"] = 0;

            ViewBag.HocKi = LayDanhSachHocKi();
            ViewBag.SinhVien = LayDanhSachSinhVienTheoLopChuNhiem(idLopChuNhiem);
            return View(lstketqua);
        }

        [HttpPost]
        public ActionResult Index(int idLopHoc,int idHocKi,int idSinhVien)
        {

            var lstketqua = this.LayDanhSachKetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem(idLopHoc, idHocKi, idSinhVien);
            var lstlophoc = LayDanhSachLopHocTheoID(idLopHoc);
            var lsthocki = LayDanhSachHocKi();
            var lstsinhvien = LayDanhSachSinhVienTheoLopChuNhiem(idLopHoc);

            lsthocki.Insert(0, new HocKiDTO
            {
                ID = 0,
                TenHocKi = "Chọn Học Kì"
            });
            lstsinhvien.Insert(0, new AccountDTO
            {
                ID = 0,
                Ma = "Chọn Mã Sinh Viên"
            });

            ViewData["LopHoc"] = new SelectList(lstlophoc, "ID", "TenLop");
            ViewData["HK"] = new SelectList(lsthocki, "ID", "TenHocKi");
            ViewData["SV"] = new SelectList(lstsinhvien, "ID", "Ma");

            Session["lstketquakehoach"] = lstketqua;
            Session["idHocKi"] = 0;

            ViewBag.HocKi = LayDanhSachHocKi();
            ViewBag.SinhVien = LayDanhSachSinhVienTheoLopChuNhiem(idLopHoc);

            return View(lstketqua);
        }

        public List<SinhVienDangKiKeHoachHocTapDTO> LayDanhSachKetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem(int idLopHoc,int idHocKi,int idSinhVien)
        {
            using(KetQuaLapKeHoachDangKiSinhVienBusiness bs = new KetQuaLapKeHoachDangKiSinhVienBusiness())
            {
                return bs.LayDanhSachKetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem(idLopHoc,idHocKi,idSinhVien);
            }
        }

        public List<LopHocDTO> LayDanhSachLopHocTheoID(int idLopHoc)
        {
            using(LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.LayDanhSachLopHocTheoID(idLopHoc);
            }
        }

        public List<HocKiDTO> LayDanhSachHocKi()
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayDanhSachHocKi();
            }
        }

        public List<AccountDTO> LayDanhSachSinhVienTheoLopChuNhiem(int idLopHoc)
        {
            using(TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayDanhSachSinhVienTheoLopChuNhiem(idLopHoc);
            }
        }
    }
}