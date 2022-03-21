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
    public class KhoaBoMonController : Controller
    {
        //LaydanhsachKhoaBoMon
        public ActionResult Index()
        {
            var lstkhoabm = this.LayDanhSachKhoaBoMon();
            return View(lstkhoabm);
        }

        public List<KhoaBoMonDTO> LayDanhSachKhoaBoMon()
        {
            using (KhoaBoMonBusiness bs = new KhoaBoMonBusiness())
            {
                return bs.LayDanhSachKhoaBoMon();
            }
        }

        //Get : TaoKhoaBoMon
        public ActionResult Create()
        {
            return View();
        }

        //Post : TaoLoaiHinhDaoTao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(KhoaBoMonDTO khoabm)
        {
            var id = LayKhoaBoMonDaTonTai(khoabm.TenKhoaBoMon);
            var resultkhoaBM = LayYeuCauNhapKhoaBoMon(khoabm);
            if (resultkhoaBM == false)
            {
                ViewBag.ErrorkhoaBM = "Yêu cầu nhập các trường bắt buộc";
                return View();
            }
            else if (id > 0)
            {
                ViewBag.Error = "Khoa Bộ Môn đã tồn tại";
                return View();
            }
            else
            {
                ThemKhoaBoMon(khoabm);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
        }

        public int LayKhoaBoMonDaTonTai(string tenkhoa)
        {
            using (KhoaBoMonBusiness bs = new KhoaBoMonBusiness())
            {
                return bs.LayKhoaBoMonDaTonTai(tenkhoa);
            }
        }

        public bool LayYeuCauNhapKhoaBoMon(KhoaBoMonDTO khoabm)
        {
            if(khoabm.TenKhoaBoMon == null)
            {
                return false;
            }
            return true;
        }

        public bool ThemKhoaBoMon(KhoaBoMonDTO khoabm)
        {
            using (KhoaBoMonBusiness bs = new KhoaBoMonBusiness())
            {
                return bs.ThemKhoaBoMon(khoabm);
            }
        }

        //Get : SuaKhoaBoMon
        public ActionResult Edit(int id)
        {
            KhoaBoMonDTO khoabm = LayKhoaBoMon(id);
            return View(khoabm);
        }

        //Post : SuaKhoaBoMon
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(KhoaBoMonDTO khoabm)
        {
            var id = LayKhoaBoMonDaTonTai(khoabm.TenKhoaBoMon);
            var resultkhoaBM = LayYeuCauNhapKhoaBoMon(khoabm);
            if (id == khoabm.ID || id == 0 && resultkhoaBM == true)
            {
                SuaKhoaBoMon(khoabm);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
            if (resultkhoaBM == false)
            {
                ViewBag.ErrorkhoaBM = "Yêu cầu nhập các trường bắt buộc";
                return View();
            }
            else
            {
                ViewBag.Error = "Khoa Bộ Môn đã tồn tại";
                return View();
            }
        }

        public KhoaBoMonDTO LayKhoaBoMon(int id)
        {
            using(KhoaBoMonBusiness bs = new KhoaBoMonBusiness())
            {
                return bs.LayKhoaBoMon(id);
            }
        }

        public bool SuaKhoaBoMon(KhoaBoMonDTO khoabm)
        {
            using(KhoaBoMonBusiness bs = new KhoaBoMonBusiness())
            {
                return bs.SuaKhoaBoMon(khoabm);
            }
        }

        //Xóa Tên Khoa Bộ Môn khỏi danh sách
        public async Task<ActionResult> Delete(int id)
        {
            //Kiểm tra Tên Khoa Bộ Môn có tồn tại(có liên kết với bảng khác không)
            var findkhoabm = CheckLoiKhoaBoMonDaTonTai(id);
            //Nếu có tồn tại
            if (findkhoabm > 0)
            {
                //Thông báo lỗi
                TempData["error"] = "lỗi";
                //Chuyển hướng về lại trang Index
                return RedirectToAction("Index");
            }
            else
            {
                var output = XoaKhoaBoMon(id);
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

        public bool XoaKhoaBoMon(int id)
        {
            using(KhoaBoMonBusiness bs = new KhoaBoMonBusiness())
            {
                return bs.XoaKhoaBoMon(id);
            }
        }

        public int CheckLoiKhoaBoMonDaTonTai(int? id)
        {
            using(KhoaBoMonBusiness bs = new KhoaBoMonBusiness())
            {
                return bs.CheckLoiKhoaBoMonDaTonTai(id);
            }
        }

        //XemChiTietKhoaBoMon
        public ActionResult Details(int id)
        {
            var khoabm = LayKhoaBoMon(id);
            return View(khoabm);
        }
    }
}