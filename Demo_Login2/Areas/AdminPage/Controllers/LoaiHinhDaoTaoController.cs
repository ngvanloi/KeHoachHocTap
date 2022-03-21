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
    public class LoaiHinhDaoTaoController : Controller
    {
        //LaydanhsachLoaiHinhDaoTao
        public ActionResult Index()
        {
            var lstloaihinhDT = this.LayDanhSachLoaiHinhDaoTao();
            return View(lstloaihinhDT);
        }
        public List<LoaiHinhDaoTaoDTO> LayDanhSachLoaiHinhDaoTao()
        {
            using(LoaiHinhDaoTaoBusiness bs = new LoaiHinhDaoTaoBusiness())
            {
                return bs.LayDanhSachLoaiHinhDaoTao();
            }
        }

        //Get : TaoLoaiHinhDaoTao
        public ActionResult Create()
        {
            return View();
        }
        //Post : TaoLoaiHinhDaoTao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LoaiHinhDaoTaoDTO loaihinhDT)
        {
            var id = LayLoaiHinhDaoTaoDaTonTai(loaihinhDT.TenLoaiHinh);
            var resultLoaiHinhDT = LayYeuCauNhapLoaiHinhDaoTao(loaihinhDT);
            if(resultLoaiHinhDT == false)
            {
                ViewBag.ErrorloaihinhDT = "Yêu cầu nhập các trường bắt buộc";
                return View();
            }
            else if (id > 0)
            {
                ViewBag.Message = " Loại Hình Đào Tạo đã tồn tại";               
                return View();
            }
            else
            {
                ThemLoaiHinhDaoTao(loaihinhDT);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
        }
        public int LayLoaiHinhDaoTaoDaTonTai(string tenloaihinh)
        {
            using (LoaiHinhDaoTaoBusiness bs = new LoaiHinhDaoTaoBusiness())
            {
                return bs.LayLoaiHinhDaoTaoDaTonTai(tenloaihinh);
            }
        }

        public bool ThemLoaiHinhDaoTao(LoaiHinhDaoTaoDTO loaihinhDT)
        {
            using(LoaiHinhDaoTaoBusiness bs = new LoaiHinhDaoTaoBusiness())
            {
                return bs.ThemLoaiHinhDaoTao(loaihinhDT);
            }
        }

        //Get : SuaLoaiHinhDaoTao
        public ActionResult Edit(int id)
        {
            LoaiHinhDaoTaoDTO loaihinhDT = LayLoaiHinhDaoTao(id);
            return View(loaihinhDT);
        }

        //Post : SuaLoaiHinhDaoTao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LoaiHinhDaoTaoDTO loaihinhDT)
        {
            var id = LayLoaiHinhDaoTaoDaTonTai(loaihinhDT.TenLoaiHinh);
            var resultLoaiHinhDT = LayYeuCauNhapLoaiHinhDaoTao(loaihinhDT);

            if (id == loaihinhDT.ID || id == 0 && resultLoaiHinhDT == true)
            {
                SuaLoaiHinhDaoTao(loaihinhDT);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");

            }
            else if (resultLoaiHinhDT == false)
            {
                ViewBag.ErrorloaihinhDT = "Yêu cầu nhập các trường bắt buộc";
                return View();
            }
            else
            {
                ViewBag.Message = "Loại Hình Đào Tạo đã tồn tại!!";
                return View();
            }
        }

        public LoaiHinhDaoTaoDTO LayLoaiHinhDaoTao(int id)
        {
            using(LoaiHinhDaoTaoBusiness bs = new LoaiHinhDaoTaoBusiness())
            {
                return bs.LayLoaiHinhDaoTao(id);
            }
        }

        public bool LayYeuCauNhapLoaiHinhDaoTao(LoaiHinhDaoTaoDTO loaihinh)
        {
            if(loaihinh.TenLoaiHinh == null)
            {
                return false;
            }
            return true;
        }

        public bool SuaLoaiHinhDaoTao(LoaiHinhDaoTaoDTO loaihinhDT)
        {
            using(LoaiHinhDaoTaoBusiness bs = new LoaiHinhDaoTaoBusiness())
            {
                return bs.SuaLoaiHinhDaoTao(loaihinhDT);
            }
        }

        //XoaLoaiHinhDaoTao
        public async Task<ActionResult> Delete(int id)
        {
            var findloiloaihinh = CheckLoiLoaiHinhDaoTaoDaTonTai(id);
            if(findloiloaihinh > 0)
            {
                TempData["error"] = "lỗi";
                return RedirectToAction("Index");
            }
            else
            {
                var output = XoaLoaiHinhDaoTao(id);
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

        public bool XoaLoaiHinhDaoTao(int id)
        {
            using (LoaiHinhDaoTaoBusiness bs = new LoaiHinhDaoTaoBusiness())
            {
                return bs.XoaLoaiHinhDaoTao(id);
            }
        }

        public int CheckLoiLoaiHinhDaoTaoDaTonTai(int? id)
        {
            using(LoaiHinhDaoTaoBusiness bs = new LoaiHinhDaoTaoBusiness())
            {
                return bs.CheckLoiLoaiHinhDaoTaoDaTonTai(id);
            }
        }

        //XemChiTietLoaiHinhDaoTao
        public ActionResult Details(int id)
        {
            var loaihinhDT = LayLoaiHinhDaoTao(id);
            return View(loaihinhDT);
        }
    }
}