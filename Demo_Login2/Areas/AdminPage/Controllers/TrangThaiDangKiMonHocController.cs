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
    public class TrangThaiDangKiMonHocController : Controller
    {
        // GET: AdminPage/TrangThaiDangKiMonHoc
        public ActionResult Index()
        {
            var lsttrangthai = LayDanhSachTrangThaiDangKiMonHoc();
            ViewBag.khoaDT = LayDanhSachKhoaDaoTao();
            ViewBag.hocki = LayDanhSachHocKi();
            foreach(var item in lsttrangthai)
            {
                Mo_TrangThaiDangKiMonHoc(Convert.ToInt32(item.IDKhoaDaoTao), Convert.ToInt32(item.IDHocKi));
            }
            foreach(var item1 in lsttrangthai)
            {
                Check_TrangThaiMonHocQuaThoiGianDangKi(Convert.ToInt32(item1.IDKhoaDaoTao), Convert.ToInt32(item1.IDHocKi));
            }
            return View(lsttrangthai);
        }

        public bool Mo_TrangThaiDangKiMonHoc(int idKhoaDT,int idHocKi)
        {
            using (TrangThaiDangKiMonHocBusiness bs = new TrangThaiDangKiMonHocBusiness())
            {
                return bs.Mo_TrangThaiDangKiMonHoc(idKhoaDT,idHocKi);
            }
        }

        public bool Check_TrangThaiMonHocQuaThoiGianDangKi(int idKhoaDT, int idHocKi)
        {
            using (TrangThaiDangKiMonHocBusiness bs = new TrangThaiDangKiMonHocBusiness())
            {
                return bs.Check_TrangThaiMonHocQuaThoiGianDangKi(idKhoaDT, idHocKi);
            }
        }

        public List<HocKiDTO> LayDanhSachHocKi()
        {
            using(HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayDanhSachHocKi();
            }
        }

        public List<TrangThaiDangKiMonHocDTO> LayDanhSachTrangThaiDangKiMonHoc()
        {
            using(TrangThaiDangKiMonHocBusiness bs = new TrangThaiDangKiMonHocBusiness())
            {
                return bs.LayDanhSachTrangThaiDangKiMonHoc();
            }
        }

        public List<KhoaDaoTaoDTO> LayDanhSachKhoaDaoTao()
        {
            using(KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayDanhSachKhoaDaoTao();
            }
        }

        //Get : TaoTrangThaiDangKiMonHoc
        public ActionResult Create()
        {
            ViewData["khoaDT"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
            ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi");
            return View();
        }
        //Post : TaoTrangThaiDangKiMonHoc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TrangThaiDangKiMonHocDTO trangthaidangki)
        {
            var id = LayHocKiTheoKhoaDaoTaoDaTonTai(trangthaidangki.IDHocKi,trangthaidangki.IDKhoaDaoTao);
            var sosanh = SoSanhThoiGian(trangthaidangki.ThoiGianBatDau, trangthaidangki.ThoiGianKetThuc);

            //so sanh thoi gian mo mon hoc
            var thoigianbatdau = Convert.ToDateTime(trangthaidangki.ThoiGianBatDau);
            var thoigianketthuc = Convert.ToDateTime(trangthaidangki.ThoiGianKetThuc);
            var thoigianhientai = DateTime.Now;

            if(thoigianbatdau <= thoigianhientai && thoigianhientai <= thoigianketthuc)
            {
                trangthaidangki.TrangThai = true;
            }
            else
            {
                trangthaidangki.TrangThai = false;
            }
            //bat loi mo dang ki mon hoc
            if (trangthaidangki.ThoiGianBatDau == null || trangthaidangki.ThoiGianKetThuc == null)
            {
                ViewBag.Errorkhongnhapthoigian = "Thời gian bắt đầu hoặc kết thúc không được để trống";
                ViewData["khoaDT"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
                ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi");
                return View();
            }
            else if (id > 0)
            {
                ViewBag.Error = "Học Kì trong Khóa Đào Tạo này đã tồn tại";
                ViewData["khoaDT"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
                ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi");
                return View();
            } else if (sosanh == false)
            {
                ViewBag.ErrorThoiGian = "Thời gian kết thúc phải sau hoặc không trùng Thời gian bắt đầu";
                ViewData["khoaDT"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
                ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi");
                return View();
            }            
            else
            {
                ThemTrangThaiDangKiMonHoc(trangthaidangki);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }


        }

        public int LayHocKiTheoKhoaDaoTaoDaTonTai(int? idHocKi,int? idKhoaDaoTao)
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayHocKiTheoKhoaDaoTaoDaTonTai(idHocKi,idKhoaDaoTao);
            }
        }
        public bool SoSanhThoiGian(string startDate, string endDate)
        {
            var ngaybatdau = Convert.ToDateTime(startDate);
            var ngayketthuc = Convert.ToDateTime(endDate);
            if(ngayketthuc <= ngaybatdau)
            {
                return false;
            }
            return true;

        }
        public bool ThemTrangThaiDangKiMonHoc(TrangThaiDangKiMonHocDTO trangthai)
        {
            using(TrangThaiDangKiMonHocBusiness bs = new TrangThaiDangKiMonHocBusiness())
            {
                return bs.ThemTrangThaiDangKiMonHoc(trangthai);
            }
        }

        //Get : SuaTrangThaiDangKiMonHoc
        public ActionResult Edit(int id)
        {
            TrangThaiDangKiMonHocDTO trangthai = LayTrangThaiDangKiMonHoc(id);
            ViewData["khoaDT"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
            ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi");
            return View(trangthai);
        }

        //Post : SuaTrangThaiDangKiMonHoc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TrangThaiDangKiMonHocDTO trangthaidangki)
        {
            var findkhoa = LayHocKiTheoKhoaDaoTaoDaTonTai(trangthaidangki.IDHocKi,trangthaidangki.IDKhoaDaoTao);
            var sosanh = SoSanhThoiGian(trangthaidangki.ThoiGianBatDau, trangthaidangki.ThoiGianKetThuc);

            //so sanh thoi gian mo mon hoc
            var thoigianbatdau = Convert.ToDateTime(trangthaidangki.ThoiGianBatDau);
            var thoigianketthuc = Convert.ToDateTime(trangthaidangki.ThoiGianKetThuc);
            var thoigianhientai = DateTime.Now;

            if(thoigianbatdau <= thoigianhientai && thoigianhientai <= thoigianketthuc)
            {
                trangthaidangki.TrangThai = true;
            }
            else
            {
                trangthaidangki.TrangThai = false;
            }

            //bat loi mo dang ki mon hoc
            if ((findkhoa == trangthaidangki.ID || findkhoa == 0) && sosanh == true && (trangthaidangki.ThoiGianBatDau != null && trangthaidangki.ThoiGianKetThuc != null))
            {
                SuaTrangThaiDangKiMonHoc(trangthaidangki);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
            else
            {
                if (findkhoa != trangthaidangki.ID && findkhoa > 0)
                {
                    ViewBag.Error = "Học Kì trong Khóa Đào Tạo này đã tồn tại";
                    ViewData["khoaDT"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
                    ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi");
                    return View();

                }
                else if (trangthaidangki.ThoiGianBatDau == null || trangthaidangki.ThoiGianKetThuc == null)
                {
                    ViewBag.Errorkhongnhapthoigian = "Thời gian bắt đầu hoặc kết thúc không được để trống";
                    ViewData["khoaDT"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
                    ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi");
                    return View();
                }
                else if(sosanh == false)
                {
                    ViewBag.ErrorThoiGian = "Thời gian kết thúc phải sau hoặc không trùng Thời gian bắt đầu";
                    ViewData["khoaDT"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
                    ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi");
                    return View();
                }
                else
                {
                    return View();
                }
            }

        }

        public TrangThaiDangKiMonHocDTO LayTrangThaiDangKiMonHoc(int id)
        {
            using (TrangThaiDangKiMonHocBusiness bs = new TrangThaiDangKiMonHocBusiness())
            {
                return bs.LayTrangThaiDangKiMonHoc(id);
            }
        }

        public bool SuaTrangThaiDangKiMonHoc(TrangThaiDangKiMonHocDTO trangthai)
        {
            using(TrangThaiDangKiMonHocBusiness bs = new TrangThaiDangKiMonHocBusiness())
            {
                return bs.SuaTrangThaiDangKiMonHoc(trangthai);
            }
        }

        //XoaTrangThaiDangKiMonHoc
        public async Task<ActionResult> Delete(int id)
        {
            var output = XoaTrangThaiDangKiMonHoc(id);
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

        public bool XoaTrangThaiDangKiMonHoc(int id)
        {
            using(TrangThaiDangKiMonHocBusiness bs = new TrangThaiDangKiMonHocBusiness())
            {
                return bs.XoaTrangThaiDangKiMonHoc(id);
            }
        }

        //XemChiTietTrangThaiDangKiMonHoc
        public ActionResult Details(int id)
        {
            var trangthai = LayTrangThaiDangKiMonHoc(id);
            ViewBag.khoaDT = LayDanhSachKhoaDaoTao();
            ViewBag.hocki = LayDanhSachHocKi();
            return View(trangthai);
        }
    }
}