using Demo_Login2.Areas.AdminPage.Business;
using Demo_Login2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Login2.Areas.AdminPage.Controllers
{
    public class KeHoachHocTap_Moi_TaoDanhSachController : Controller
    {
        // GET: AdminPage/KeHoachHocTap_Moi_TaoDanhSach
        public ActionResult Index()
        {
            var lstctrdaotao = LayDanhSachKeHoachHocTap_Moi_TheoChuongTrinhDaoTao(0, 0);
            Session["lstCTDT"] = lstctrdaotao;

            List<ChuongTrinhDaoTao_MoiDTO> lstkehoachHT = new List<ChuongTrinhDaoTao_MoiDTO>();
            Session["lstKHHT"] = lstkehoachHT;

            List<ChuongTrinhDaoTao_MoiDTO> timmonhocsaukhichon = new List<ChuongTrinhDaoTao_MoiDTO>();
            Session["monhocCTDT"] = timmonhocsaukhichon;

            ThuVienKhoaDaoTaoVaHocKi();
            return View();

        }

        [HttpPost]
        public ActionResult Index(int idKhoaDT, int idHocKi)
        {
            var lstctrdaotao = LayDanhSachKeHoachHocTap_Moi_TheoChuongTrinhDaoTao(idKhoaDT, idHocKi);
            foreach (var chuongtrinhdaotao in (List<ChuongTrinhDaoTao_MoiDTO>)Session["lstKHHT"])
            {
                foreach (var kehoachhoctap in lstctrdaotao)
                {
                    if (chuongtrinhdaotao.ID == kehoachhoctap.ID)
                    {
                        lstctrdaotao.Remove(kehoachhoctap);
                        break;
                    }
                }
            }
            Session["lstCTDT"] = lstctrdaotao;
            Session["idKhoaDT"] = idKhoaDT;
            Session["idHocKi"] = idHocKi;

            ThuVienKhoaDaoTaoVaHocKi();
            return View();
        }

        public ActionResult Them_KeHoachHocTap_Moi(int id)
        {
            var lstctrdaotao = this.LayDanhSachChuongTrinhDaoTao_Moi();
            List<ChuongTrinhDaoTao_MoiDTO> lstChuongTrinhDaoTao = new List<ChuongTrinhDaoTao_MoiDTO>();
            List<ChuongTrinhDaoTao_MoiDTO> lstKeHoachHocTap = new List<ChuongTrinhDaoTao_MoiDTO>();
            List<ChuongTrinhDaoTao_MoiDTO> monhocctrdaotao = new List<ChuongTrinhDaoTao_MoiDTO>();

            lstChuongTrinhDaoTao = (List<ChuongTrinhDaoTao_MoiDTO>)Session["lstCTDT"];
            lstKeHoachHocTap = (List<ChuongTrinhDaoTao_MoiDTO>)Session["lstKHHT"];
            monhocctrdaotao = (List<ChuongTrinhDaoTao_MoiDTO>)Session["monhocCTDT"];
            foreach (var item in lstctrdaotao)
            {
                if (item.ID == id)
                {
                    lstKeHoachHocTap.Add(item);
                    var monhoc = lstChuongTrinhDaoTao.SingleOrDefault(x => x.ID == item.ID);
                    if (monhoc != null)
                    {
                        lstChuongTrinhDaoTao.Remove(monhoc);
                    }

                    var monhocTim = monhocctrdaotao.SingleOrDefault(x => x.ID == item.ID);
                    if (monhocTim != null)
                    {
                        monhocctrdaotao.Remove(monhocTim);
                    }
                    break;
                }
            }

            Session["lstCTDT"] = lstChuongTrinhDaoTao;
            Session["lstKHHT"] = lstKeHoachHocTap;
            Session["monhocCTDT"] = monhocctrdaotao;
            ThuVienKhoaDaoTaoVaHocKi();

            return View("Index");

        }

        public ActionResult Xoa_KeHoachHocTap_Moi(int id)
        {
            var lstctrdaotao = this.LayDanhSachKeHoachHocTap_Moi_TheoChuongTrinhDaoTao((int)Session["idKhoaDT"], (int)Session["idHocKi"]);
            List<ChuongTrinhDaoTao_MoiDTO> lstChuongTrinhDaoTao = new List<ChuongTrinhDaoTao_MoiDTO>();
            List<ChuongTrinhDaoTao_MoiDTO> lstKeHoachHocTap = new List<ChuongTrinhDaoTao_MoiDTO>();

            lstChuongTrinhDaoTao = (List<ChuongTrinhDaoTao_MoiDTO>)Session["lstCTDT"];
            lstKeHoachHocTap = (List<ChuongTrinhDaoTao_MoiDTO>)Session["lstKHHT"];
            foreach (var item in lstKeHoachHocTap)
            {
                if (item.ID == id)
                {
                    var monhoc = lstctrdaotao.SingleOrDefault(x => x.ID == item.ID);
                    if (monhoc != null)
                    {
                        lstChuongTrinhDaoTao.Add(monhoc);
                    }
                    lstKeHoachHocTap.Remove(item);
                    break;
                }
            }
            Session["lstCTDT"] = lstChuongTrinhDaoTao;
            Session["lstKHHT"] = lstKeHoachHocTap;

            ThuVienKhoaDaoTaoVaHocKi();

            return View("Index");
        }

        public ActionResult Tim_MonHocTrongChuongTrinhDaoTao_Moi(string id)
        {
            List<ChuongTrinhDaoTao_MoiDTO> lstTimMon = new List<ChuongTrinhDaoTao_MoiDTO>();
            lstTimMon = (List<ChuongTrinhDaoTao_MoiDTO>)Session["monhocCTDT"];
            var monhoc = LayMonHocTrongChuongTrinhDaoTao_Moi_TheoMa(id);
            if (monhoc != null)
            {
                lstTimMon.Add(monhoc);

            }
            else
            {
                ViewBag.ErrorTimMonHoc = "Lỗi Môn Học không có trong Chương Trình Đào Tạo";
            }

            Session["monhocCTDT"] = lstTimMon;

            ThuVienKhoaDaoTaoVaHocKi();
            return View("Index");
        }

        public ActionResult Luu_KeHoachHocTap_Moi(int idKhoaDT, int idHocKi)
        {
            if (idKhoaDT > 0 && idHocKi > 0)
            {
                List<ChuongTrinhDaoTao_MoiDTO> lstCTDT = new List<ChuongTrinhDaoTao_MoiDTO>(); 
                foreach (var item in (List<ChuongTrinhDaoTao_MoiDTO>)Session["lstKHHT"])
                {
                    var acc = Check_MonHocDaTonTai(item.MaMonHoc, idKhoaDT, idHocKi);
                    if(acc== true)
                    {
                        lstCTDT.Add(item);
                    }
                    else
                    {
                        TempData["Errorchecktrung"] = "Môn Học đã tồn tại trong Kế Hoạch Học Tập";
                        return RedirectToAction("Index", "KeHoachHocTap_Moi_TaoDanhSach");
                    }               
                }
                foreach(var item in lstCTDT)
                {
                    ThemKeHoachHocTap_Moi(item, idKhoaDT, idHocKi);
                }               
                ThuVienKhoaDaoTaoVaHocKi();
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index", "KeHoachHocTap_Moi_XemDanhSach");
            }
            TempData["Error"] = "Phải chọn Khóa Đào Tạo và Học kì trước khi Lưu";
            return RedirectToAction("Index", "KeHoachHocTap_Moi_TaoDanhSach");



        }

        public void ThuVienKhoaDaoTaoVaHocKi()
        {
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

            var listhocki = LayDanhSachHocKi();
            listhocki.Insert(0, new HocKiDTO
            {
                ID = 0,
                TenHocKi = "Chọn Học Kì"
            });
            ViewData["hocki"] = new SelectList(listhocki, "ID", "TenHocKi");
        }

        public List<ChuongTrinhDaoTao_MoiDTO> LayDanhSachKeHoachHocTap_Moi_TheoChuongTrinhDaoTao(int idKhoaDT, int idHocKi)
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.LayDanhSachKeHoachHocTap_Moi_TheoChuongTrinhDaoTao(idKhoaDT, idHocKi);
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
        public List<ChuongTrinhDaoTao_MoiDTO> LayDanhSachChuongTrinhDaoTao_Moi()
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.LayDanhSachChuongTrinhDaoTao_Moi();
            }
        }

        public ChuongTrinhDaoTao_MoiDTO LayMonHocTrongChuongTrinhDaoTao_Moi_TheoMa(string ma)
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.LayMonHocTrongChuongTrinhDaoTao_Moi_TheoMa(ma);
            }
        }

        public bool ThemKeHoachHocTap_Moi(ChuongTrinhDaoTao_MoiDTO lstctrdaotao, int idKhoaDT, int idHocKi)
        {
            using (KeHoachHocTap_MoiBusiness bs = new KeHoachHocTap_MoiBusiness())
            {
                return bs.ThemKeHoachHocTap_Moi(lstctrdaotao, idKhoaDT, idHocKi);
            }
        }
        public bool Check_MonHocDaTonTai(string mamonhoc, int idKhoaDT, int idHocKi)
        {
            using (KeHoachHocTap_MoiBusiness bs = new KeHoachHocTap_MoiBusiness())
            {
                return bs.Check_MonHocDaTonTai(mamonhoc, idKhoaDT, idHocKi);
            }
        }
    }
}