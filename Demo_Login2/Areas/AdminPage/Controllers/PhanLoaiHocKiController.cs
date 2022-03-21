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
    public class PhanLoaiHocKiController : Controller
    {
        //LayPhanLoaiHocKi
        public ActionResult Index()
        {
            var lstphanloaihocki = this.LayDanhSachPhanLoaiHocKi();
            return View(lstphanloaihocki);
        }

        public List<PhanLoaiHocKiDTO> LayDanhSachPhanLoaiHocKi()
        {
            using (PhanLoaiHocKiBusiness bs = new PhanLoaiHocKiBusiness())
            {
                return bs.LayDanhSachPhanLoaiHocKi();
            }
        }

        //Get : TaoPhanLoaiHocKi
        public ActionResult Create()
        {
            return View();
        }
        //Post : TaoPhanLoaiHocKi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PhanLoaiHocKiDTO phanloaihocki)
        {
            var id = LayPhanLoaiHocKiDaTonTai(phanloaihocki.LoaiHocKi);
            var resultloaiHK = LayYeuCauNhapLoaiHocKi(phanloaihocki);
            if(resultloaiHK == false)
            {
                ViewBag.ErrorloaiHK = "Yêu cầu nhập các trường bắt buộc";
                return View();
            }
            else if(id > 0)
            {
                ViewBag.Message = "Loại Học Kì đã tồn tại";
                return View();
            }
            else
            {
                ThemPhanLoaiHocKi(phanloaihocki);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
        }
        public int LayPhanLoaiHocKiDaTonTai(string phanloaihocki)
        {
            using(PhanLoaiHocKiBusiness bs = new PhanLoaiHocKiBusiness())
            {
                return bs.LayPhanLoaiHocKiDaTonTai(phanloaihocki);
            }
        }
        public bool ThemPhanLoaiHocKi(PhanLoaiHocKiDTO phanloaihocki)
        {
            using (PhanLoaiHocKiBusiness bs = new PhanLoaiHocKiBusiness())
            {
                return bs.ThemPhanLoaiHocKi(phanloaihocki);
            }
        }

        //Get : SuaPhanLoaiHocKi
        public ActionResult Edit(int id)
        {
            PhanLoaiHocKiDTO phanloaihocki = LayPhanLoaiHocKi(id);
            return View(phanloaihocki);
        }

        //Post : SuaPhanLoaiHocKi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PhanLoaiHocKiDTO phanloaihocki)
        {
            var id = LayPhanLoaiHocKiDaTonTai(phanloaihocki.LoaiHocKi);
            var resultloaiHK = LayYeuCauNhapLoaiHocKi(phanloaihocki);
            
            if (id == phanloaihocki.ID || id == 0 && resultloaiHK == true)
            {
                SuaPhanLoaiHocKi(phanloaihocki);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");

            }
            else if (resultloaiHK == false)
            {
                ViewBag.ErrorloaiHK = "Yêu cầu nhập các trường bắt buộc";
                return View();
            }
            else
            {
                ViewBag.Message = "Loại Học Kì đã tồn tại!!";
                return View();
            }
        }

        public PhanLoaiHocKiDTO LayPhanLoaiHocKi(int id)
        {
            using (PhanLoaiHocKiBusiness bs = new PhanLoaiHocKiBusiness())
            {
                return bs.LayPhanLoaiHocKi(id);
            }
        }

        public bool LayYeuCauNhapLoaiHocKi(PhanLoaiHocKiDTO phanloai)
        {
            if(phanloai.LoaiHocKi == null)
            {
                return false;
            }
            return true;
        }

        public bool SuaPhanLoaiHocKi(PhanLoaiHocKiDTO phanloaihocki)
        {
            using (PhanLoaiHocKiBusiness bs = new PhanLoaiHocKiBusiness())
            {
                return bs.SuaPhanLoaiHocKi(phanloaihocki);
            }
        }

        //XoaPhanLoaiHocKi
        public async Task<ActionResult> Delete(int id)
        {
            var findphanloaihocki = CheckLoiPhanLoaiHocKiDaTonTai(id);
            if(findphanloaihocki > 0)
            {
                TempData["error"] = "lỗi";
                return RedirectToAction("Index");
            }
            else
            {
                var output = XoaPhanLoaiHocKi(id);
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

        public bool XoaPhanLoaiHocKi(int id)
        {
            using (PhanLoaiHocKiBusiness bs = new PhanLoaiHocKiBusiness())
            {
                return bs.XoaPhanLoaiHocKi(id);
            }
        }
        
        public int CheckLoiPhanLoaiHocKiDaTonTai(int? id)
        {
            using(PhanLoaiHocKiBusiness bs = new PhanLoaiHocKiBusiness())
            {
                return bs.CheckLoiPhanLoaiHocKiDaTonTai(id);
            }
        }

        //XemChiTietPhanLoaiHocKi
        public ActionResult Details(int id)
        {
            var phanloaihocki = LayPhanLoaiHocKi(id);
            return View(phanloaihocki);
        }
    }
}