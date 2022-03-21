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
    public class SinhVienDangKiKeHoachHocTapController : Controller
    {
        // GET: SinhVienPage/SinhVienDangKiKeHoachHocTap
        public ActionResult Index()
        {
            HttpCookie IDKhoaDaoTao = HttpContext.Request.Cookies.Get("idKhoaDaoTao");
            var idKhoaDT = Convert.ToInt32(IDKhoaDaoTao.Value);

            HttpCookie idAcc = HttpContext.Request.Cookies.Get("idAccount");
            var idAccount = Convert.ToInt32(idAcc.Value);

            int idHocKi = getHocKiChoSVDangKi(idKhoaDT); 
            ViewBag.tenhocki = LayTenHocKi(idHocKi);
            ViewBag.hocki = LayDanhSachHocKi();
            var result = Mo_TrangThaiDangKiMonHoc(idKhoaDT, idHocKi);

            var lstctrdaotao = this.LayKeHoachHocTap_Moi(0, 0);
            ViewBag.LichSu = LayLichSuDangKiTheHocKi(idAccount, idHocKi);

            if (result == true)
            {
                var lstSV = LayKeHoachHocTapCuaSinhVien_Moi(idAccount, idKhoaDT, idHocKi);
                lstctrdaotao = this.LayKeHoachHocTap_Moi(idKhoaDT, idHocKi);

                foreach(var itemSV in lstSV)
                {
                    var trangthaimonhoc = lstctrdaotao.FirstOrDefault(s => s.MaMonHoc == itemSV.MaMonHoc);
                    if(trangthaimonhoc != null)
                    {
                        trangthaimonhoc.TrangThai = itemSV.TrangThai;
                    }
                }
                //if (lst.Count() > 0)
                //{
                //    lstctrdaotao = lst;
                //}
                //else
                //{
                //    lstctrdaotao = this.LayKeHoachHocTap_Moi(idKhoaDT,idHocKi );

                //}

            }
            else
            {
                ViewBag.Errorthoigiandangki = "Chưa đến thời gian mở đăng kí";
            }

            return View(lstctrdaotao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<SinhVienDangKiKeHoachHocTapDTO> list)
        {
            HttpCookie IDKhoaDaoTao = HttpContext.Request.Cookies.Get("idKhoaDaoTao");
            var idKhoaDT = Convert.ToInt32(IDKhoaDaoTao.Value);

            HttpCookie idAcc = HttpContext.Request.Cookies.Get("idAccount");
            var idAccount = Convert.ToInt32(idAcc.Value);

            HttpCookie idLopCC = HttpContext.Request.Cookies.Get("idLopHoc");
            var idLopHoc = Convert.ToInt32(idLopCC.Value);

            int idHocKi = getHocKiChoSVDangKi(idKhoaDT);
            ViewBag.tenhocki = LayTenHocKi(idHocKi);
            ViewBag.hocki = LayDanhSachHocKi();

            Them_KeHoachHocTapChoSV(list, idAccount, idLopHoc);

            ViewBag.LichSu = LayLichSuDangKiTheHocKi(idAccount, idHocKi);
            var lstSV = LayKeHoachHocTapCuaSinhVien_Moi(idAccount, idKhoaDT, idHocKi);

            TempData["Success"] = "Thành công";
            return RedirectToAction("Index");
        }

        public int getHocKiChoSVDangKi(int idKhoaDT)
        {
            int thanghientai = DateTime.Now.Month;
            int namhientai = DateTime.Now.Year;

            List<int> danhsachhocki = new List<int>();

            List<HocKiDTO> lsthocki = this.LayDanhSachHocKi();
            int nienkhoa = LayNamHocCuaKhoaDaoTao(idKhoaDT);
            foreach (var item in lsthocki)
            {
                if (item.ThangBatDau <= thanghientai && item.ThangKetThuc >= thanghientai)
                {
                    danhsachhocki.Add(item.ID);
                }
            }
            if ((namhientai - nienkhoa >= 0) && danhsachhocki[namhientai - nienkhoa] > 0)
            {
                return danhsachhocki[namhientai - nienkhoa] + 1;
            }
            return 0;
        }

        public string LayTenHocKi(int idHocKi)
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayTenHocKi(idHocKi);
            }
        }
        public int LayNamHocCuaKhoaDaoTao(int idKhoaDT)
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayNamHocCuaKhoaDaoTao(idKhoaDT);
            }
        }
        public List<HocKiDTO> LayDanhSachHocKi()
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayDanhSachHocKi();
            }
        }
        public bool Mo_TrangThaiDangKiMonHoc(int idKhoaDT, int idHocKi)
        {
            using (TrangThaiDangKiMonHocBusiness bs = new TrangThaiDangKiMonHocBusiness())
            {
                return bs.Mo_TrangThaiDangKiMonHoc(idKhoaDT, idHocKi);
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> LayKeHoachHocTap_Moi(int idkhoadt, int idhocki)
        {
            using (SinhVienDangKiKeHoachHocTapBusiness bs = new SinhVienDangKiKeHoachHocTapBusiness())
            {
                return bs.LayKeHoachHocTap_Moi(idkhoadt, idhocki);
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> LayLichSuDangKiTheHocKi(int idAccount, int idHocKi)
        {
            using (SinhVienDangKiKeHoachHocTapBusiness bs = new SinhVienDangKiKeHoachHocTapBusiness())
            {
                return bs.LayLichSuDangKiTheHocKi(idAccount, idHocKi);
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> LayKeHoachHocTapCuaSinhVien_Moi(int idAccount, int idKhoaDT, int idHocKi)
        {
            using (SinhVienDangKiKeHoachHocTapBusiness bs = new SinhVienDangKiKeHoachHocTapBusiness())
            {
                return bs.LayKeHoachHocTapCuaSinhVien_Moi(idAccount,idKhoaDT ,idHocKi);
            }
        }
        public bool Them_KeHoachHocTapChoSV(List<SinhVienDangKiKeHoachHocTapDTO> list, int idAccount, int idLopHoc)
        {
            using (SinhVienDangKiKeHoachHocTapBusiness bs = new SinhVienDangKiKeHoachHocTapBusiness())
            {
                return bs.Them_KeHoachHocTapChoSV(list, idAccount, idLopHoc);
            }
        }
        public bool Check_TrangThaiMonHocQuaThoiGianDangKi(int idKhoaDT, int idHocKi)
        {
            using (TrangThaiDangKiMonHocBusiness bs = new TrangThaiDangKiMonHocBusiness())
            {
                return bs.Check_TrangThaiMonHocQuaThoiGianDangKi(idKhoaDT, idHocKi);
            }
        }

    }
}