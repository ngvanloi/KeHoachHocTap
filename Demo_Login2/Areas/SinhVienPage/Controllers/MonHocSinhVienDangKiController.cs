using Demo_Login2.Areas.AdminPage.Business;
using Demo_Login2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Login2.Areas.SinhVienPage.Controllers
{
    public class MonHocSinhVienDangKiController : Controller
    {
        //BỎ PHẦN NÀY
        //GET: SinhVienPage/MonHocSinhVienDangKi
        public ActionResult Index()
        {
            HttpCookie IDKhoaDaoTao = HttpContext.Request.Cookies.Get("idKhoaDaoTao");
            var idKhoaDT = Convert.ToInt32(IDKhoaDaoTao.Value);

            HttpCookie idAcc = HttpContext.Request.Cookies.Get("idAccount");
            var idAccount = Convert.ToInt32(idAcc.Value);

            int idHocKi = getHocKiChoSVDangKi(idKhoaDT);
            //int idHocKi = 3;
            ViewBag.tenhocki = LayTenHocKi(idHocKi);

            HttpCookie idlophoc = HttpContext.Request.Cookies.Get("idLopHoc");
            var idLopHoc = Convert.ToInt32(idlophoc.Value);
            

            var result = Mo_TrangThaiDangKiMonHoc(idKhoaDT, idHocKi);

            var lstmonhocsvdk = this.LayMonHocSinhVienDangKi(0,0,0,0);
            if(result == true)
            {
                lstmonhocsvdk = LayMonHocSinhVienDangKi(idKhoaDT, idAccount, idHocKi,idLopHoc);
            }
            else
            {
                ViewBag.Errorthoigiandangki = "Chưa đến thời gian mở đăng kí";
            }
            
             
            return View(lstmonhocsvdk);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<MonHocSinhVienDangKiDTO> list)
        {
            HttpCookie IDKhoaDaoTao = HttpContext.Request.Cookies.Get("idKhoaDaoTao");
            var idKhoaDT = Convert.ToInt32(IDKhoaDaoTao.Value);

            HttpCookie idAcc = HttpContext.Request.Cookies.Get("idAccount");
            var idAccount = Convert.ToInt32(idAcc.Value);

            int idHocKi = getHocKiChoSVDangKi(idKhoaDT);
            ViewBag.tenhocki = LayTenHocKi(idHocKi);

            HttpCookie idlophoc = HttpContext.Request.Cookies.Get("idLopHoc");
            var idLopHoc = Convert.ToInt32(idlophoc.Value);

            foreach (var item in list)
            {
                var hethanthoigiandangki = Check_TrangThaiMonHocQuaThoiGianDangKi(idKhoaDT, Convert.ToInt32(item.IDHocKi));
                if(hethanthoigiandangki == true)
                {
                    Them_MonHocVuotVaoDanhSach(item);
                }
                else
                {
                    ViewBag.Errorhethandangki = "Đã quá thời gian đăng kí môn học";
                    HttpCookie IDKhoaDaoTao1 = HttpContext.Request.Cookies.Get("idKhoaDaoTao");
                    var idKhoaDT1 = Convert.ToInt32(IDKhoaDaoTao1.Value);

                    HttpCookie idAcc1 = HttpContext.Request.Cookies.Get("idAccount");
                    var idAccount1 = Convert.ToInt32(idAcc1.Value);

                    int idHocKi1 = getHocKiChoSVDangKi(idKhoaDT1);
                    ViewBag.tenhocki = LayTenHocKi(idHocKi1);

                }
            }
            ViewBag.Success = "Thành công";

            var result = Mo_TrangThaiDangKiMonHoc(idKhoaDT, idHocKi);

            var lstmonhocsvdk = this.LayMonHocSinhVienDangKi(0, 0, 0,0);
            if (result == true)
            {
                lstmonhocsvdk = LayMonHocSinhVienDangKi(idKhoaDT, idAccount, idHocKi,idLopHoc);
            }
            else
            {
                ViewBag.Errorthoigiandangki = "Chưa đến thời gian mở đăng kí";
            }

            return View(lstmonhocsvdk);
        }

        public bool Mo_TrangThaiDangKiMonHoc(int idKhoaDT,int idHocKi)
        {
            using (TrangThaiDangKiMonHocBusiness bs = new TrangThaiDangKiMonHocBusiness())
            {
                return bs.Mo_TrangThaiDangKiMonHoc(idKhoaDT, idHocKi);
            }
        }

        public bool Check_TrangThaiMonHocQuaThoiGianDangKi(int idKhoaDT,int idHocKi)
        {
            using(TrangThaiDangKiMonHocBusiness bs = new TrangThaiDangKiMonHocBusiness())
            {
                return bs.Check_TrangThaiMonHocQuaThoiGianDangKi(idKhoaDT, idHocKi);
            }
        }

        public ActionResult Lay_MonHocVuot(string id)
        {
            HttpCookie IDKhoaDaoTao = HttpContext.Request.Cookies.Get("idKhoaDaoTao");
            var idKhoaDT = Convert.ToInt32(IDKhoaDaoTao.Value);

            HttpCookie IDAcc = HttpContext.Request.Cookies.Get("idAccount");
            var idAccount = Convert.ToInt32(IDAcc.Value);

            int idHocKi = getHocKiChoSVDangKi(idKhoaDT);

            HttpCookie idlophoc = HttpContext.Request.Cookies.Get("idLopHoc");
            var idLopHoc = Convert.ToInt32(idlophoc.Value);

            var lstmonhocvuotsvdk = this.LayMonHocSinhVienDangKi(idKhoaDT, idAccount, idHocKi,idLopHoc);
            var monhocvuot = LayMonHocVuotTheoMa(id);


            var findTrungMon = CheckMonHocVuotHocKiTruoc(monhocvuot.ID, idAccount);
            var findMonHV = lstmonhocvuotsvdk.Where(s => s.IDMonHoc == monhocvuot.ID).FirstOrDefault();
            
            if(findMonHV == null && findTrungMon == 0)
            {
                lstmonhocvuotsvdk.Insert(0, new MonHocSinhVienDangKiDTO
                {
                    IDMonHoc = monhocvuot.ID,
                    MaMonHoc = monhocvuot.MaMonHoc,
                    TenMonHoc = monhocvuot.TenMonHoc,
                    SoTinChi = monhocvuot.SoTinChi,
                    SoTietLyThuyet = monhocvuot.SoTietLyThuyet,
                    SoTietThucHanh = monhocvuot.SoTietThucHanh,
                    IDKhoaBoMon = monhocvuot.IDKhoaBoMon,
                    TenKhoaBoMon = monhocvuot.TenKhoaBoMon,

                    //Error
                    IDHocPhanTienQuyet = monhocvuot.IDHocPhanTienQuyet,
                    IDMonHocTienQuyet = monhocvuot.IDMonHocTienQuyet,
                    TenMonHocTienQuyet = monhocvuot.TenMonHoc,
                    IDHocPhanHocTruoc = monhocvuot.IDHocPhanHocTruoc,
                    IDMonHocHocTruoc = monhocvuot.IDMonHocHocTruoc,
                    TenMonHocHocTruoc = monhocvuot.TenMonHoc,

                    LoaiDangKi = 4,
                    TrangThai = false,
                    IDHocKi = idHocKi,
                    IDAccount = idAccount,
                    IDKhoaDaoTao = idKhoaDT,
                    IDLopHoc = idLopHoc
                });
                return View("Index", lstmonhocvuotsvdk);
            }
            else
            {
                return RedirectToAction("Index");
            }
            

            //ViewBag.Success = "Thành công";
            //var lstmonhocvuotsvd = this.LayMonHocSinhVienDangKi(idKhoaDT, idAccount, idHocKi);


        }

        public List<MonHocSinhVienDangKiDTO> LayMonHocSinhVienDangKi(int idkhoadt,int idaccount,int idhocki,int idlophoc)
        {
            using(MonHocSinhVienDangKiBusiness bs = new MonHocSinhVienDangKiBusiness())
            {
                return bs.LayMonHocSinhVienDangKi(idkhoadt, idaccount,idhocki,idlophoc);
            }
        }

        public bool Them_MonHocVuotVaoDanhSach(MonHocSinhVienDangKiDTO item)
        {
            using(MonHocSinhVienDangKiBusiness bs = new MonHocSinhVienDangKiBusiness())
            {
                return bs.Them_MonHocVuotVaoDanhSach(item);
            }
        }

        public MonHocSinhVienDangKiDTO LayMonHocVuotTheoMa(string ma)
        {
            using(MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayMonHocVuotTheoMa(ma);
            }
        }
        public int CheckMonHocVuotHocKiTruoc(int idMonHoc, int idAccount)
        {
            using (MonHocSinhVienDangKiBusiness bs = new MonHocSinhVienDangKiBusiness())
            {
                return bs.CheckMonHocVuotHocKiTruoc(idMonHoc, idAccount);
            }
        }



        public int getHocKiChoSVDangKi(int idKhoaDT)
        {
            int thanghientai = DateTime.Now.Month;
            int namhientai = DateTime.Now.Year;

            List<int> danhsachhocki = new List<int>();

            List<HocKiDTO> lsthocki = this.LayDanhSachHocKi();
            int nienkhoa = LayNamHocCuaKhoaDaoTao(idKhoaDT);
            foreach(var item in lsthocki)
            {
                if(item.ThangBatDau <= thanghientai && item.ThangKetThuc >= thanghientai)
                {
                    danhsachhocki.Add(item.ID);
                }
            }
            if((namhientai - nienkhoa >= 0) && danhsachhocki[namhientai - nienkhoa] > 0)
            {
                return danhsachhocki[namhientai - nienkhoa] + 1;
            }
            return 0;
        }

        public List<HocKiDTO> LayDanhSachHocKi()
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayDanhSachHocKi();
            }
        }

        public int LayNamHocCuaKhoaDaoTao(int idKhoaDT)
        {
            using(KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayNamHocCuaKhoaDaoTao(idKhoaDT);
            }
        }

        public string LayTenHocKi(int idHocKi)
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayTenHocKi(idHocKi);
            }
        }


    }
}