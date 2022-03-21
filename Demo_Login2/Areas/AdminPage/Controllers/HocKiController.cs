using Demo_Login2.Areas.AdminPage.Business;
using Demo_Login2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Demo_Login2.Areas.AdminPage.Controllers
{
    public class HocKiController : Controller
    {
        //TaoDanhSachCacThangTrongNam
        List<SelectListItem> lstThang = new List<SelectListItem>()
        {
            new SelectListItem
            {
                Text = "Tháng 1",Value = "1"
            },new SelectListItem
            {
                Text = "Tháng 2",Value = "2"
            },new SelectListItem
            {
                Text = "Tháng 3",Value = "3"
            },new SelectListItem
            {
                Text = "Tháng 4",Value = "4"
            },new SelectListItem
            {
                Text = "Tháng 5",Value = "5"
            },new SelectListItem
            {
                Text = "Tháng 6",Value = "6"
            },new SelectListItem
            {
                Text = "Tháng 7",Value = "7"
            },new SelectListItem
            {
                Text = "Tháng 8",Value = "8"
            },new SelectListItem
            {
                Text = "Tháng 9",Value = "9"
            },new SelectListItem
            {
                Text = "Tháng 10",Value = "10"
            },new SelectListItem
            {
                Text = "Tháng 11",Value = "11"
            },new SelectListItem
            {
                Text = "Tháng 12",Value = "12"
            },
        };


        //LayDanhSachHocKi
        public ActionResult Index()
        {
            var lstHocKi = this.LayDanhSachHocKi();
            ViewBag.phanloaihocki = LayDanhSachPhanLoaiHocKi();
            return View(lstHocKi);
        }

        public List<HocKiDTO> LayDanhSachHocKi()
        {
            using(HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayDanhSachHocKi();
            }
        }

        //Get : TaoHocKi
        public ActionResult Create()
        {
            ViewData["phanloaihocki"] = new SelectList(LayDanhSachPhanLoaiHocKi(), "ID", "LoaiHocKi");
            ViewBag.lstThangs = lstThang;
            return View();
        }

        //Post : TaoHocKi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HocKiDTO hocki)
        {
            var id = LayHocKiDaTonTai(hocki.TenHocKi);
            var resultTen = LayYeuCauNhapTenHocKi(hocki);
            var resultThangBD = LayYeuCauNhapThangBatDau(hocki);
            var resultThangKT = LayYeuCauNhapThangKetThuc(hocki);
            var resultAll = false;
            

            if(resultTen == false)
            {
                resultAll = true;
                ViewBag.ErrorkhongcokituTen = "Yêu cầu nhập các trường bắt buộc";
            }
            if(resultThangBD == false)
            {
                resultAll = true;
                ViewBag.ErrorkhongcokituThangBD = "Yêu cầu nhập các trường bắt buộc và Tháng Bắt Đầu phải lớn hơn 0";
            }
            if(resultThangKT == false)
            {
                resultAll = true;
                ViewBag.ErrorkhongcokituThangKT = "Yêu cầu nhập các trường bắt buộc và Tháng Kết Thúc phải lớn hơn 0";
            }
            if(hocki.ThangBatDau < 0)
            {
                resultAll = true;
                ViewBag.ErrordieukienThangBDNhoHon = "Tháng Bắt Đầu không được nhỏ hơn 0";
            }
            if(hocki.ThangBatDau > 12)
            {
                resultAll = true;
                ViewBag.ErrordieukienThangBDLonHon = "Tháng Bắt Đầu không được lớn hơn 12";
            }
            if (hocki.ThangKetThuc < 0)
            {
                resultAll = true;
                ViewBag.ErrordieukienThangKTNhoHon = "Tháng Kết Thúc không được nhỏ hơn 0";
            }
            if (hocki.ThangKetThuc > 12)
            {
                resultAll = true;
                ViewBag.ErrordieukienThangKTLonHon = "Tháng Kết Thúc không được lớn hơn 12";
            }
            if(id > 0)
            {
                resultAll = true;
                ViewBag.Message = "Tên Học Kì đã tồn tại";
            }
            if(resultAll == false)
            {
                ThemHocKi(hocki);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["phanloaihocki"] = new SelectList(LayDanhSachPhanLoaiHocKi(), "ID", "LoaiHocKi");
                return View();
            }
        }

        public int LayHocKiDaTonTai(string hocki)
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayHocKiDaTonTai(hocki);
            }
        }
        public List<PhanLoaiHocKiDTO> LayDanhSachPhanLoaiHocKi()
        {
            using (PhanLoaiHocKiBusiness bs = new PhanLoaiHocKiBusiness())
            {
                return bs.LayDanhSachPhanLoaiHocKi();
            }
        }

        public bool LayYeuCauNhapTenHocKi(HocKiDTO hocki)
        {
            if(hocki.TenHocKi == null)
            {
                return false;
            }
            return true;
        }

        public bool LayYeuCauNhapThangBatDau(HocKiDTO hocki)
        {
            if(hocki.ThangBatDau == 0)
            {
                return false;
            }
            return true;
        }

        public bool LayYeuCauNhapThangKetThuc(HocKiDTO hocki)
        {
            if(hocki.ThangKetThuc == 0)
            {
                return false;
            }
            return true;
        }

        public bool ThemHocKi(HocKiDTO hocki)
        {
            using(HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.ThemHocKi(hocki);
            }
        }

        //Get : SuaHocKi
        public ActionResult Edit(int id)
        {
            ViewBag.lstThangs = lstThang;
            ViewData["phanloaihocki"] = new SelectList(LayDanhSachPhanLoaiHocKi(), "ID", "LoaiHocKi");
            HocKiDTO hocki = LayHocKi(id);
            return View(hocki);
        }

        //Post : SuaHocKi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HocKiDTO hocki)
        {
            var id = LayHocKiDaTonTai(hocki.TenHocKi);

            var resultTen = LayYeuCauNhapTenHocKi(hocki);
            var resultThangBD = LayYeuCauNhapThangBatDau(hocki);
            var resultThangKT = LayYeuCauNhapThangKetThuc(hocki);
            var resultAll = false;

            if ((id == hocki.ID || id == 0) && resultTen == true && resultThangBD == true && resultThangKT == true && hocki.ThangBatDau <= 12 && hocki.ThangKetThuc <= 12 && 
                resultAll == false && hocki.ThangBatDau > 0 && hocki.ThangKetThuc > 0)
            {
                SuaHocKi(hocki);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");

            }
            else
            {
                if (resultTen == false)
                {
                    resultAll = true;
                    ViewBag.ErrorkhongcokituTen = "Yêu cầu nhập các trường bắt buộc";
                }
                if (resultThangBD == false)
                {
                    resultAll = true;
                    ViewBag.ErrorkhongcokituThangBD = "Yêu cầu nhập các trường bắt buộc và Tháng Bắt Đầu phải lớn hơn 0";
                }
                if (resultThangKT == false)
                {
                    resultAll = true;
                    ViewBag.ErrorkhongcokituThangKT = "Yêu cầu nhập các trường bắt buộc và Tháng Kết Thúc phải lớn hơn 0";
                }
                if (hocki.ThangBatDau < 0)
                {
                    resultAll = true;
                    ViewBag.ErrordieukienThangBDNhoHon = "Tháng Bắt Đầu không được nhỏ hơn 0";
                }
                if (hocki.ThangBatDau > 12)
                {
                    resultAll = true;
                    ViewBag.ErrordieukienThangBD = "Tháng Bắt Đầu không được lớn hơn 12";
                }
                if (hocki.ThangKetThuc < 0)
                {
                    resultAll = true;
                    ViewBag.ErrordieukienThangKTNhoHon = "Tháng Kết Thúc không được nhỏ hơn 0";
                }
                if (hocki.ThangKetThuc > 12)
                {
                    resultAll = true;
                    ViewBag.ErrordieukienThangKT = "Tháng Kết Thúc không được lớn hơn 12";
                }
                if(id != hocki.ID && id > 0)
                {
                    resultAll = true;
                    ViewBag.Message = "Học Kì đã tồn tại!!";
                }
                ViewData["phanloaihocki"] = new SelectList(LayDanhSachPhanLoaiHocKi(), "ID", "LoaiHocKi");
                return View();
            }

        }

        public HocKiDTO LayHocKi(int id)
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayHocKi(id);
            }
        }

        public bool SuaHocKi(HocKiDTO hocki)
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.SuaHocKi(hocki);
            }
        }

        //XoaHocKi
        public async Task<ActionResult> Delete(int id)
        {
            var findhocki_monhockhoaDT = CheckLoiHocKiDaTonTai_MonHocKhoaDaoTao(id);
            var findhocki_trangthaidangkimon = CheckLoiHocKiDaTonTai_TrangThaiDangKiMon(id);
            var findhocki_chuongtrinhdaotao_moi = CheckLoiHocKiDaTonTai_ChuongTrinhDaoTao_Moi(id);
            var findhocki_kehoachhoctap_moi = CheckLoiHocKiDaTonTai_KeHoachHocTap_Moi(id);
            var findhocki_sinhviendangkikehoachhoctap = CheckLoiSinhVienDangKiKeHoachHocTap_Moi(id);
            if (findhocki_monhockhoaDT > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaihocki = LayDanhSachPhanLoaiHocKi();
                return RedirectToAction("Index");
            }
            else if(findhocki_trangthaidangkimon > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaihocki = LayDanhSachPhanLoaiHocKi();
                return RedirectToAction("Index");
            }
            else if(findhocki_chuongtrinhdaotao_moi > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaihocki = LayDanhSachPhanLoaiHocKi();
                return RedirectToAction("Index");
            }
            else if (findhocki_kehoachhoctap_moi > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaihocki = LayDanhSachPhanLoaiHocKi();
                return RedirectToAction("Index");
            }
            else if (findhocki_sinhviendangkikehoachhoctap > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaihocki = LayDanhSachPhanLoaiHocKi();
                return RedirectToAction("Index");
            }
            else
            {
                var output = XoaHocKi(id);
                if (output)
                {
                    TempData["Success"] = "Thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("fail");
                }
            }
        }
        public bool XoaHocKi(int id)
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.XoaHocKi(id);
            }
        }
        public int CheckLoiHocKiDaTonTai_MonHocKhoaDaoTao(int? id)
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.CheckLoiHocKiDaTonTai_MonHocKhoaDaoTao(id);
            }
        }
        public int CheckLoiHocKiDaTonTai_TrangThaiDangKiMon(int? id)
        {
            using(HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.CheckLoiHocKiDaTonTai_TrangThaiDangKiMon(id);
            }
        }
        public int CheckLoiHocKiDaTonTai_ChuongTrinhDaoTao_Moi(int? id)
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.CheckLoiHocKiDaTonTai_ChuongTrinhDaoTao_Moi(id);
            }
        }
        public int CheckLoiHocKiDaTonTai_KeHoachHocTap_Moi(int? id)
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.CheckLoiHocKiDaTonTai_KeHoachHocTap_Moi(id);
            }
        }
        public int CheckLoiSinhVienDangKiKeHoachHocTap_Moi(int? id)
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.CheckLoiSinhVienDangKiKeHoachHocTap_Moi(id);
            }
        }
        //XemChiTietHocKi
        public ActionResult Details(int id)
        {
            var hocki = LayHocKi(id);
            ViewBag.phanloaihocki = LayDanhSachPhanLoaiHocKi();
            ViewBag.lstThangs = lstThang;
            return View(hocki);
        }
    }


}