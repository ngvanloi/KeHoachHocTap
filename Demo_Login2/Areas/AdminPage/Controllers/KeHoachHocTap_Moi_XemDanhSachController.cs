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
    public class KeHoachHocTap_Moi_XemDanhSachController : Controller
    {
        // GET: AdminPage/KeHoachHocTap_Moi_XemDanhSach
        public ActionResult Index()
        {
            var lstkehoachHT = this.LayDanhSachKetQuaHocTap_Moi_TheoKhoaDaoTao(0);

            ThuVienKhoaDaoTao();
            return View(lstkehoachHT);
        }

        [HttpPost]
        public ActionResult Index(int id)
        {
            var lstkehoachHT = this.LayDanhSachKetQuaHocTap_Moi_TheoKhoaDaoTao(id);

            ThuVienKhoaDaoTao();
            return View(lstkehoachHT);
        }

        public async Task<ActionResult> Delete(int idKhoaDT, int idHocki)
        {
            
            if (idKhoaDT > 0 && idHocki > 0)
            {
                var acc = Check_XoaKhoaDaoTaoKhiKhongCoDuLieu(idKhoaDT, idHocki);               
                if (acc == false)
                {
                    var output = Xoa_KeHoachHocTap(idKhoaDT, idHocki);
                    if (output)
                    {
                        TempData["Success"] = "Thành công";
                        return RedirectToAction("Index");

                    }
                    
                }
                TempData["Errorkhongcodulieu"] = "Không có dữ liệu để xóa";
                return RedirectToAction("Index");

            }
            TempData["Error"] = "Phải chọn Khóa Đào Tạo và Học kì trước khi Xóa";
            return RedirectToAction("Index");
        }

        public void ThuVienKhoaDaoTao()
        {
            ViewBag.HocKi = LayDanhSachHocKi();
            ViewBag.PhanLoaiMonHoc = LayDanhSachPhanLoaiMonHoc();
            ViewBag.MonHoc = LayDanhSachMonHoc();

            var listkhoaDT = LayDanhSachKhoaDaoTao();
            listkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Chọn Khóa Đào Tạo"
            });
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao");

            var listhocki = LayDanhSachHocKi();
            listhocki.Insert(0, new HocKiDTO
            {
                ID = 0,
                TenHocKi = "Chọn Học Kì"
            });
            ViewData["HK"] = new SelectList(listhocki, "ID", "TenHocKi");
        }

        public List<KeHoachHocTap_MoiDTO> LayDanhSachKetQuaHocTap_Moi_TheoKhoaDaoTao(int id)
        {
            using(KeHoachHocTap_MoiBusiness bs = new KeHoachHocTap_MoiBusiness())
            {
                return bs.LayDanhSachKetQuaHocTap_Moi_TheoKhoaDaoTao(id);
            }
        }

        public List<HocKiDTO> LayDanhSachHocKi()
        {
            using (HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayDanhSachHocKi();
            }
        }
        public List<MonHocDTO> LayDanhSachMonHoc()
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayDanhSachMonHoc();
            }
        }
        public List<PhanLoaiMonHocDTO> LayDanhSachPhanLoaiMonHoc()
        {
            using (PhanLoaiMonHocBusiness bs = new PhanLoaiMonHocBusiness())
            {
                return bs.LayDanhSachPhanLoaiMonHoc();
            }
        }
        public List<KhoaDaoTaoDTO> LayDanhSachKhoaDaoTao()
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayDanhSachKhoaDaoTao();
            }
        }

        public bool Xoa_KeHoachHocTap(int idKhoaDT,int idHocKi)
        {
            using(KeHoachHocTap_MoiBusiness bs = new KeHoachHocTap_MoiBusiness())
            {
                return bs.Xoa_KeHoachHocTap(idKhoaDT, idHocKi);
            }
        }
        public bool Check_XoaKhoaDaoTaoKhiKhongCoDuLieu(int idKhoaDT, int idHocKi)
        {
            using (KeHoachHocTap_MoiBusiness bs = new KeHoachHocTap_MoiBusiness())
            {
                return bs.Check_XoaKhoaDaoTaoKhiKhongCoDuLieu(idKhoaDT, idHocKi);
            }
        }




    }
}