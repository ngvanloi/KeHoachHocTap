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
    public class KhoaDaoTaoController : Controller
    {
        //LaydanhsachKhoaDaoTao
        public ActionResult Index()
        {
            var lstKhoa = this.LayDanhSachKhoaDaoTao();
            ViewBag.loaihinhdaotao = LayDanhSachLoaiHinhDaoTao();
            return View(lstKhoa);
        }
        public List<KhoaDaoTaoDTO> LayDanhSachKhoaDaoTao()
        {
            using(KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayDanhSachKhoaDaoTao();
            }
        }
        //Get : TaoKhoaDaoTao
        public ActionResult Create()
        {
            ViewData["loaihinh"] = new SelectList(LayDanhSachLoaiHinhDaoTao(), "ID", "TenLoaiHinh");
            return View();
        }
        //Post : TaoKhoaDaoTao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(KhoaDaoTaoDTO khoadaotao)
        {
            
            var findtenkhoa = LayTenKhoaDaTonTai(khoadaotao.TenKhoaDaoTao);
            var findnienkhoa = LayNienKhoaDaTonTai(khoadaotao.NienKhoa);
            var resultTenKhoaDT = LayYeuCauNhapTenKhoaDaoTao(khoadaotao);
            var resultAll = false;

            if(resultTenKhoaDT == false)
            {
                resultAll = true;
                ViewBag.ErrorkhongcokituTen = "Yêu cầu nhập các trường bắt buộc";
            }
            if (findtenkhoa > 0)
            {
                ViewBag.ErrorTen = "Tên Khóa Đã Tồn Tại";
                resultAll = true;
            }
            if(khoadaotao.NienKhoa == 0)
            {
                ViewBag.ErrorkhongcokituNienKhoa = "Yêu cầu nhập các trường bắt buộc";
                resultAll = true;
            }
            if(khoadaotao.NienKhoa < 0)
            {
                ViewBag.ErrorNienKhoaNhoHon0 = "Niên Khóa không được nhỏ hơn 0";
                resultAll = true;
            }

            if(findnienkhoa > 0 || (khoadaotao.NienKhoa < 2018 && khoadaotao.NienKhoa > 1) || khoadaotao.NienKhoa > 3000)
            {
                resultAll = true;
                ViewBag.Errorkhoa = "Niên Khóa phải lớn hơn hoặc bằng năm 2018,bé hơn năm 3000 và Niên Khóa không được trùng";
            }

            if(resultAll == false)
            {
                ThemKhoaDaoTao(khoadaotao);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["loaihinh"] = new SelectList(LayDanhSachLoaiHinhDaoTao(), "ID", "TenLoaiHinh");
                return View();
            }
        }
        public int LayTenKhoaDaTonTai(string tenkhoa)
        {
            using(KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayTenKhoaDaTonTai(tenkhoa);
            }
        }

        public int LayNienKhoaDaTonTai(int nienkhoa)
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayNienKhoaDaTonTai(nienkhoa);
            }
        }

        public List<LoaiHinhDaoTaoDTO> LayDanhSachLoaiHinhDaoTao()
        {
            using(LoaiHinhDaoTaoBusiness bs = new LoaiHinhDaoTaoBusiness())
            {
                return bs.LayDanhSachLoaiHinhDaoTao();
            }
        }

        public bool LayYeuCauNhapTenKhoaDaoTao(KhoaDaoTaoDTO khoadt)
        {
            if(khoadt.TenKhoaDaoTao == null)
            {
                return false;
            }
            return true;
        }

        public bool ThemKhoaDaoTao(KhoaDaoTaoDTO khoadaotao)
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.ThemKhoaDaoTao(khoadaotao);
            }
        }



        //Get:SuaKhoaDaoTao
        public ActionResult Edit(int id)
        {
            ViewData["loaihinh"] = new SelectList(LayDanhSachLoaiHinhDaoTao(), "ID", "TenLoaiHinh");
            KhoaDaoTaoDTO khoadaotao = LayKhoaDaoTao(id);
            return View(khoadaotao);
        }

        //Post:SuaKhoaDaoTao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(KhoaDaoTaoDTO khoadaotao)
        {
            var findtenkhoa = LayTenKhoaDaTonTai(khoadaotao.TenKhoaDaoTao);
            var findnienkhoa = LayNienKhoaDaTonTai(khoadaotao.NienKhoa);
            var resultTenKhoaDT = LayYeuCauNhapTenKhoaDaoTao(khoadaotao);
            var resultAll = false;

            if ((findtenkhoa == khoadaotao.ID || findtenkhoa == 0) && (findnienkhoa == khoadaotao.ID || findnienkhoa == 0) &&(Convert.ToInt32(khoadaotao.NienKhoa) > 2017 && Convert.ToInt32(khoadaotao.NienKhoa) < 3000) && resultTenKhoaDT == true && resultAll == false)
            {
                SuaKhoaDaoTao(khoadaotao);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
            else
            {
                if (resultTenKhoaDT == false)
                {
                    resultAll = true;
                    ViewBag.ErrorkhongcokituTen = "Yêu cầu nhập các trường bắt buộc";
                }
                if (khoadaotao.NienKhoa == 0)
                {
                    ViewBag.ErrorkhongcokituNienKhoa = "Yêu cầu nhập các trường bắt buộc";
                    resultAll = true;
                }
                if (findtenkhoa != khoadaotao.ID && findtenkhoa > 0)
                {
                    ViewBag.ErrorTen = "Tên Khóa Đã Tồn Tại";
                    resultAll = true;
                }
                if ((khoadaotao.NienKhoa != khoadaotao.ID) || (khoadaotao.NienKhoa < 2018 && khoadaotao.NienKhoa > 1) || Convert.ToInt32(khoadaotao.NienKhoa) > 3000)
                {
                    ViewBag.Errorkhoa = "Niên Khóa phải lớn hơn hoặc bằng năm 2018,bé hơn năm 3000 và Niên Khóa không được trùng";
                    resultAll = true;
                }
                ViewData["loaihinh"] = new SelectList(LayDanhSachLoaiHinhDaoTao(), "ID", "TenLoaiHinh");
                return View();
            }
            
        }

        public KhoaDaoTaoDTO LayKhoaDaoTao(int id)
        {
            using(KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayKhoaDaoTao(id);
            }
        }

        public bool SuaKhoaDaoTao(KhoaDaoTaoDTO khoadaotao)
        {
            using(KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.SuaKhoaDaoTao(khoadaotao);
            }
        }

        //XoaKhoaDaoTao
        public async Task<ActionResult> Delete(int id)
        {
            var findlop = CheckLoiKhoaDaTonTaiTrongLop(id);
            var findmonhockhoaDT = CheckLoiKhoaDaTonTaiTrongMonHocKhoaDT(id);
            var findtrangthaidangkimonhoc = CheckLoiKhoaDaTonTaiTrongTrangThaiDangKiMonHoc(id);
            var findkhoadaotao_kehoachhoctap_moi = CheckLoiKeHoachHocTap_Moi(id);
            var findkhoadaotao_sinhviendangkikehoachhoctap = CheckLoiSinhVienDangKiKeHoachHocTap_Moi(id);
            if (findlop > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.loaihinhdaotao = LayDanhSachLoaiHinhDaoTao();
                return RedirectToAction("Index");
            }else if(findmonhockhoaDT > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.loaihinhdaotao = LayDanhSachLoaiHinhDaoTao();
                return RedirectToAction("Index");
            }
            else if(findtrangthaidangkimonhoc > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.loaihinhdaotao = LayDanhSachLoaiHinhDaoTao();
                return RedirectToAction("Index");
            }
            else if (findkhoadaotao_kehoachhoctap_moi > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.loaihinhdaotao = LayDanhSachLoaiHinhDaoTao();
                return RedirectToAction("Index");
            }
            else if (findkhoadaotao_sinhviendangkikehoachhoctap > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.loaihinhdaotao = LayDanhSachLoaiHinhDaoTao();
                return RedirectToAction("Index");
            }
            else
            {
                var output = XoaKhoaDaoTao(id);
                if (output)
                {
                    TempData["Success"] = "Thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Fail");
                }
            }
           
        }

        public bool XoaKhoaDaoTao(int id)
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.XoaKhoaDaoTao(id);
            }
        }

        public int CheckLoiKhoaDaTonTaiTrongLop(int? id)
        {
            using(KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.CheckLoiKhoaDaTonTaiTrongLop(id);
            }
        }

        public int CheckLoiKhoaDaTonTaiTrongTrangThaiDangKiMonHoc(int? id)
        {
            using(KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.CheckLoiKhoaDaTonTaiTrongTrangThaiDangKiMonHoc(id);
            }
        }
        public int CheckLoiKhoaDaTonTaiTrongMonHocKhoaDT(int? id)
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.CheckLoiKhoaDaTonTaiTrongMonHocKhoaDT(id);
            }
        }
        public int CheckLoiKeHoachHocTap_Moi(int? id)
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.CheckLoiKeHoachHocTap_Moi(id);
            }
        }
        public int CheckLoiSinhVienDangKiKeHoachHocTap_Moi(int? id)
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.CheckLoiSinhVienDangKiKeHoachHocTap_Moi(id);
            }
        }

        //XemChiTietKhoaDaoTao
        public ActionResult Details(int id)
        {
            var khoa = LayKhoaDaoTao(id);
            ViewBag.loaihinhdaotao = LayDanhSachLoaiHinhDaoTao();
            return View(khoa);
        }
    }
}