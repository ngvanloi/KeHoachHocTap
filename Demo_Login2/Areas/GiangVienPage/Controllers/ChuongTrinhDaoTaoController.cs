using Demo_Login2.Areas.AdminPage.Business;
using Demo_Login2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Login2.Areas.GiangVienPage.Controllers
{
    public class ChuongTrinhDaoTaoController : Controller
    {
        // GET: LayDanhSachChuongTrinhDaoTao
        public ActionResult Index()
        {
            var lstctrdaotao = this.LayDanhSachChuongTrinhDaoTao_Moi_TheoKhoaDaoTao(0);
            ViewBag.HocKi = LayDanhSachHocKi();
            ViewBag.PhanLoaiMonHoc = LayDanhSachPhanLoaiMonHoc();
            ViewBag.MonHoc = LayDanhSachMonHoc();

            var listkhoaDT = LayDanhSachKhoaDaoTao();
            listkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Chọn Khóa"
            });
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao");
            return View(lstctrdaotao);
        }

        //Post :LayDanhSachChuongTrinhDaoTao
        [HttpPost]
        public ActionResult Index(int id)
        {
            var lstctrdaotao = this.LayDanhSachChuongTrinhDaoTao_Moi_TheoKhoaDaoTao(id);
            ViewBag.HocKi = LayDanhSachHocKi();
            ViewBag.PhanLoaiMonHoc = LayDanhSachPhanLoaiMonHoc();
            ViewBag.MonHoc = LayDanhSachMonHoc();

            var listkhoaDT = LayDanhSachKhoaDaoTao();
            listkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Chọn Khóa"
            });
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao");
            return View(lstctrdaotao);
        }

        public List<ChuongTrinhDaoTao_MoiDTO> LayDanhSachChuongTrinhDaoTao_Moi_TheoKhoaDaoTao(int id)
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.LayDanhSachChuongTrinhDaoTao_Moi_TheoKhoaDaoTao(id);
            }
        }

        public List<HocKiDTO> LayDanhSachHocKi()
        {
            using(HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayDanhSachHocKi();
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
            using(KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayDanhSachKhoaDaoTao();
            }
        }
    }
}