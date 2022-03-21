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
    public class MonHocKhoaDaoTaoController : Controller
    {
        //Get: LayDanhSachMonHocKhoaDaoTao
        public ActionResult Index()
        {
            var lstmonhockhoaDT = this.LayDanhSachMonHocKhoaDaoTao();
            ViewBag.monhoc = LayDanhSachMonHoc();
            ViewBag.khoadaotao = LayDanhSachKhoaDaoTao();
            ViewBag.hocki = LayDanhSachHocKi();
            ViewBag.phanloaimonhoc = LayDanhSachPhanLoaiMonHoc();
            ViewBag.hocphantienquyet = LayDanhSachHocPhanTienQuyet();
            ViewBag.hocphanhoctruoc = LayDanhSachHocPhanHocTruoc();
            return View(lstmonhockhoaDT);
        }
        public List<HocPhanTienQuyetDTO> LayDanhSachHocPhanTienQuyet()
        {
            using(HocPhanTienQuyetBusiness bs = new HocPhanTienQuyetBusiness())
            {
                return bs.LayDanhSachHocPhanTienQuyet();
            }
        }

        public List<HocPhanTienQuyetHienThiTrongMonHocKhoaDTO> LayDanhSachHocPhanTienQuyetTheoMonHocKhoa(int id)
        {
            using (HocPhanTienQuyetBusiness bs = new HocPhanTienQuyetBusiness())
            {
                return bs.LayDanhSachHocPhanTienQuyetTheoMonHocKhoa(id);
            }
        }

        public List<HocPhanHocTruocHienThiTrongMonHocKhoaDTO> LayDanhSachHocPhanHocTruocTheoMonHocKhoa(int id)
        {
            using(HocPhanHocTruocBusiness bs = new HocPhanHocTruocBusiness())
            {
                return bs.LayDanhSachHocPhanHocTruocTheoMonHocKhoa(id);
            }
        }
        public List<HocPhanHocTruocDTO> LayDanhSachHocPhanHocTruoc()
        {
            using (HocPhanHocTruocBusiness bs = new HocPhanHocTruocBusiness())
            {
                return bs.LayDanhSachHocPhanHocTruoc();
            }
        }
        public List<PhanLoaiMonHocDTO> LayDanhSachPhanLoaiMonHoc()
        {
            using(PhanLoaiMonHocBusiness bs = new PhanLoaiMonHocBusiness())
            {
                return bs.LayDanhSachPhanLoaiMonHoc();
            }
        }
        public List<MonHocDTO> LayDanhSachMonHoc()
        {
            using(MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayDanhSachMonHoc();
            }
        }
        public List<KhoaDaoTaoDTO> LayDanhSachKhoaDaoTao()
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayDanhSachKhoaDaoTao();
            }
        }
        public List<HocKiDTO> LayDanhSachHocKi()
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayDanhSachHocKi();
            }
        }
        public List<MonHocKhoaDaoTaoDTO> LayDanhSachMonHocKhoaDaoTao()
        {
            using(MonHocKhoaDaoTaoBusiness bs = new MonHocKhoaDaoTaoBusiness())
            {
                return bs.LayDanhSachMonHocKhoaDaoTao();
            }
        }

        public ActionResult Create_ThayDoiMonHoc(int id)
        {
            HttpCookie idMonHoc = HttpContext.Request.Cookies.Get("idMonHoc");
            HttpCookie idKhoaDaoTao = HttpContext.Request.Cookies.Get("idKhoaDaoTao");
            HttpCookie idHocKi = HttpContext.Request.Cookies.Get("idHocKi");
            HttpCookie idPhanLoaiMonHoc = HttpContext.Request.Cookies.Get("idPhanLoaiMonHoc");

            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc",idMonHoc.Value);
            ViewData["khoadaotao"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao",idKhoaDaoTao.Value);
            ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi", idHocKi.Value);
            ViewData["phanloaimonhoc"] = new SelectList(LayDanhSachPhanLoaiMonHoc(), "ID", "LoaiMonHoc",idPhanLoaiMonHoc.Value);
            ViewData["hocphantienquyet"] = new SelectList(LayDanhSachHocPhanTienQuyetTheoMonHocKhoa(id), "ID", "TenMonHocTienQuyet");
            ViewData["hocphanhoctruoc"] = new SelectList(LayDanhSachHocPhanHocTruocTheoMonHocKhoa(id), "ID", "TenMonHocHocTruoc");
            return View("Create");
        }

        //Get : TaoMonHocKhoaDaoTao
        public ActionResult Create()
        {
            var lstMonHoc = LayDanhSachMonHoc();
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
            ViewData["khoadaotao"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
            ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi");
            ViewData["phanloaimonhoc"] = new SelectList(LayDanhSachPhanLoaiMonHoc(), "ID", "LoaiMonHoc");
            ViewData["hocphantienquyet"] = new SelectList(LayDanhSachHocPhanTienQuyetTheoMonHocKhoa(lstMonHoc[0].ID), "ID", "TenMonHocTienQuyet");
            ViewData["hocphanhoctruoc"] = new SelectList(LayDanhSachHocPhanHocTruocTheoMonHocKhoa(lstMonHoc[0].ID), "ID", "TenMonHocHocTruoc");
            return View();
        }
        //Post :TaoMonHocKhoaDaoTao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MonHocKhoaDaoTaoDTO monhockhoaDT)
        {
            var findmonhoc = CheckLoiMonHocTheoKhoaDaoTaoDaTonTai(monhockhoaDT.IDMonHoc, monhockhoaDT.IDKhoaDaoTao);
            Response.Cookies.Remove("idMonHoc");
            Response.Cookies.Remove("idKhoaDaoTao");
            Response.Cookies.Remove("idHocKi");
            Response.Cookies.Remove("idPhanLoaiMonHoc");
            if (findmonhoc > 0)
            {
                ViewBag.Error = "Môn Học đã có trong Khóa Đào Tạo này";
                var lstMonHoc = LayDanhSachMonHoc();
                ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
                ViewData["khoadaotao"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
                ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi");
                ViewData["phanloaimonhoc"] = new SelectList(LayDanhSachPhanLoaiMonHoc(), "ID", "LoaiMonHoc");
                ViewData["hocphantienquyet"] = new SelectList(LayDanhSachHocPhanTienQuyetTheoMonHocKhoa(Convert.ToInt32(monhockhoaDT.IDMonHoc)), "ID", "TenMonHocTienQuyet");
                ViewData["hocphanhoctruoc"] = new SelectList(LayDanhSachHocPhanHocTruocTheoMonHocKhoa(Convert.ToInt32(monhockhoaDT.IDMonHoc)), "ID", "TenMonHocHocTruoc");
                return View();

            }
            else
            {
                ThemMonHocKhoaDaoTao(monhockhoaDT);
                return RedirectToAction("Index");
            }
            
            
        }
        public bool ThemMonHocKhoaDaoTao(MonHocKhoaDaoTaoDTO monhockhoaDT)
        {
            using(MonHocKhoaDaoTaoBusiness bs = new MonHocKhoaDaoTaoBusiness())
            {
                return bs.ThemMonHocKhoaDaoTao(monhockhoaDT);
            }
        }

        public int CheckLoiMonHocTheoKhoaDaoTaoDaTonTai(int? idMonHoc,int? idKhoaDaoTao)
        {
            using(MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.CheckLoiMonHocTheoKhoaDaoTaoDaTonTai(idMonHoc, idKhoaDaoTao);
            }
        }

        public ActionResult Edit_ThayDoiMonHoc(int id)
        {
            HttpCookie idMonHoc = HttpContext.Request.Cookies.Get("idMonHoc");
            HttpCookie idKhoaDaoTao = HttpContext.Request.Cookies.Get("idKhoaDaoTao");
            HttpCookie idHocKi = HttpContext.Request.Cookies.Get("idHocKi");
            HttpCookie idPhanLoaiMonHoc = HttpContext.Request.Cookies.Get("idPhanLoaiMonHoc");

            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc", idMonHoc.Value);
            ViewData["khoadaotao"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao", idKhoaDaoTao.Value);
            ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi", idHocKi.Value);
            ViewData["phanloaimonhoc"] = new SelectList(LayDanhSachPhanLoaiMonHoc(), "ID", "LoaiMonHoc", idPhanLoaiMonHoc.Value);
            ViewData["hocphantienquyet"] = new SelectList(LayDanhSachHocPhanTienQuyetTheoMonHocKhoa(id), "ID", "TenMonHocTienQuyet");
            ViewData["hocphanhoctruoc"] = new SelectList(LayDanhSachHocPhanHocTruocTheoMonHocKhoa(id), "ID", "TenMonHocHocTruoc");
            return View("Edit");
        }

        //Get: SuaMonHocKhoaDaoTao
        public ActionResult Edit(int id)
        {
            HttpCookie idEdit = new HttpCookie("idEdit");
            idEdit.Value = id.ToString();
            idEdit.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(idEdit);

            MonHocKhoaDaoTaoDTO monhockhoaDT = LayMonHocKhoaDaoTao(id);
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
            ViewData["khoadaotao"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
            ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi");
            ViewData["phanloaimonhoc"] = new SelectList(LayDanhSachPhanLoaiMonHoc(), "ID", "LoaiMonHoc");
            ViewData["hocphantienquyet"] = new SelectList(LayDanhSachHocPhanTienQuyetTheoMonHocKhoa(Convert.ToInt32(monhockhoaDT.IDMonHoc)), "ID", "TenMonHocTienQuyet");
            ViewData["hocphanhoctruoc"] = new SelectList(LayDanhSachHocPhanHocTruocTheoMonHocKhoa(Convert.ToInt32(monhockhoaDT.IDMonHoc)), "ID", "TenMonHocHocTruoc");            
            return View(monhockhoaDT);
        }

        //Post : SuaMonHocKhoaDaoTao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MonHocKhoaDaoTaoDTO monhockhoaDT)
        {
            var findmonhoc = CheckLoiMonHocTheoKhoaDaoTaoDaTonTai(monhockhoaDT.IDMonHoc, monhockhoaDT.IDKhoaDaoTao);
            Response.Cookies.Remove("idMonHoc");
            Response.Cookies.Remove("idKhoaDaoTao");
            Response.Cookies.Remove("idHocKi");
            Response.Cookies.Remove("idPhanLoaiMonHoc");
            if (findmonhoc == monhockhoaDT.ID || findmonhoc == 0)
            {
                SuaMonHocKhoaDaoTao(monhockhoaDT);
                return RedirectToAction("Index");
            }else if(findmonhoc != 0 && findmonhoc > 0)
            {
                ViewBag.Error = "Môn Học đã có trong Khóa Đào Tạo này";
                var lstMonHoc = LayDanhSachMonHoc();
                ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
                ViewData["khoadaotao"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
                ViewData["hocki"] = new SelectList(LayDanhSachHocKi(), "ID", "TenHocKi");
                ViewData["phanloaimonhoc"] = new SelectList(LayDanhSachPhanLoaiMonHoc(), "ID", "LoaiMonHoc");
                ViewData["hocphantienquyet"] = new SelectList(LayDanhSachHocPhanTienQuyetTheoMonHocKhoa(Convert.ToInt32(monhockhoaDT.IDMonHoc)), "ID", "IDMonHocTienQuyet");
                ViewData["hocphanhoctruoc"] = new SelectList(LayDanhSachHocPhanHocTruocTheoMonHocKhoa(Convert.ToInt32(monhockhoaDT.IDMonHoc)), "ID", "IDMonHocHocTruoc");
                return View();
            }
            else
            {
                return View();
            }
            
           
        }

        public MonHocKhoaDaoTaoDTO LayMonHocKhoaDaoTao(int id)
        {
            using(MonHocKhoaDaoTaoBusiness bs = new MonHocKhoaDaoTaoBusiness())
            {
                return bs.LayMonHocKhoaDaoTao(id);
            }
        }
        public MonHocKhoaDaoTaoDetailsDTO LayMonHocKhoaDaoTao_Details(int id)
        {
            using (MonHocKhoaDaoTaoBusiness bs = new MonHocKhoaDaoTaoBusiness())
            {
                return bs.LayMonHocKhoaDaoTao_Details(id);
            }
        }

        public bool SuaMonHocKhoaDaoTao(MonHocKhoaDaoTaoDTO monhockhoaDT)
        {
            using(MonHocKhoaDaoTaoBusiness bs = new MonHocKhoaDaoTaoBusiness())
            {
                return bs.SuaMonHocKhoaDaoTao(monhockhoaDT);
            }
        }

        //XoaMonHocKhoaDaoTao
        public async Task<ActionResult> Delete(int id)
        {
            var output = XoaMonHocKhoaDaoTao(id);
            if (output)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Fail");
            }
        }

        public bool XoaMonHocKhoaDaoTao(int id)
        {
            using(MonHocKhoaDaoTaoBusiness bs = new MonHocKhoaDaoTaoBusiness())
            {
                return bs.XoaMonHocKhoaDaoTao(id);
            }
        }

        //XemChiTietMonHocKhoaDaoTao
        public ActionResult Details(int id)
        {
            var monhockhoaDT = LayMonHocKhoaDaoTao_Details(id);
            ViewBag.monhoc = LayDanhSachMonHoc();
            ViewBag.khoadaotao = LayDanhSachKhoaDaoTao();
            ViewBag.hocki = LayDanhSachHocKi();
            ViewBag.phanloaimonhoc = LayDanhSachPhanLoaiMonHoc();
            ViewBag.hocphantienquyet = LayDanhSachHocPhanTienQuyet();
            ViewBag.hocphanhoctruoc = LayDanhSachHocPhanHocTruoc();
            return View(monhockhoaDT);
        }
    }
}