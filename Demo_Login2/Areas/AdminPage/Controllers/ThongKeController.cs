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
    public class ThongKeController : Controller
    {
        // GET: AdminPage/ThongKe
        public ActionResult Index()
        {
            int idHocKi = this.LayDanhSachHocKi()[0].ID;
            ThuVienChung();
            var lstKhoaDaoTao = LayDanhSachKhoaDaoTao();
            for(var i = 0; i < lstKhoaDaoTao.Count(); i++)
            {
                PieChart(lstKhoaDaoTao[i].ID, idHocKi);
                BarChart(lstKhoaDaoTao[i].ID, idHocKi);
            }
            return View(lstKhoaDaoTao);
        }
        //Post : ThongKe
        [HttpPost]
        public async Task<ActionResult> Index(int idHocKi)
        {
            ThuVienChung();
            var lstKhoaDaoTao = LayDanhSachKhoaDaoTao();
            for(var i = 0;i < lstKhoaDaoTao.Count(); i++)
            {
                PieChart(lstKhoaDaoTao[i].ID, idHocKi);
                BarChart(lstKhoaDaoTao[i].ID, idHocKi);
            }
            return View(lstKhoaDaoTao);
        }

        public void PieChart(int idKhoaDT,int idHocKi)
        {
            var TongTatCaSV = LayTongTatCaSinhVien(idKhoaDT);
            var TongSVDaDangKi = LayTongSinhVienDaDangKi(idKhoaDT, idHocKi);
            var TongSVChuaDangKi = TongTatCaSV - TongSVDaDangKi;

            ViewData["piechart_data" + idKhoaDT] = TongSVDaDangKi.ToString() + "," + TongSVChuaDangKi.ToString();
            ViewData["piechart_title" + idKhoaDT] = "Tổng số lượng sinh viên đăng kí Kế Hoạch Học Tập Của Khóa " + LayTenKhoaDaoTao(idKhoaDT);
        }

        public void BarChart(int idKhoaDT,int idHocKi)
        {
            var danhsachmontrongKHHT = LayKeHoachHocTapTheoKhoaDaoTaoVaHocKi(idKhoaDT, idHocKi);
            var danhsachsvdadangkimontrongKHHT = LayKeHoachHocTapSVDaDangKiTheoKhoaDaoTaoVaHocKi(idKhoaDT, idHocKi);

            var TongTatCaSV = LayTongTatCaSinhVien(idKhoaDT);
            ViewData["barchart_title" + idKhoaDT] = "Số lượng sinh viên đăng kí theo từng môn Của Khóa " + LayTenKhoaDaoTao(idKhoaDT);

            for(var j = 0; j < danhsachmontrongKHHT.Count(); j++)
            {
                ViewData["barchart_label" + idKhoaDT] += '"' + danhsachmontrongKHHT[j].TenMonHoc + '"' + ',';

                var barchart_monhocsvdadangki = danhsachsvdadangkimontrongKHHT.Where(s => s.IDMonHoc == danhsachmontrongKHHT[j].IDMonHoc).Count();
                ViewData["barchart_data_monhocsvdadangki" + idKhoaDT] += barchart_monhocsvdadangki.ToString() + ",";

                var barchart_monhocsvchuadangki = TongTatCaSV - barchart_monhocsvdadangki;
                ViewData["barchart_data_monhocsvchuadangki" + idKhoaDT] += barchart_monhocsvchuadangki.ToString() + ",";
            }

        }

        public void ThuVienChung()
        {
            var lstHocKi = LayDanhSachHocKi();          
            ViewData["HK"] = new SelectList(lstHocKi, "ID", "TenHocKi");
        }
        public List<HocKiDTO> LayDanhSachHocKi()
        {
            using(HocKiBusiness bs = new HocKiBusiness())
            {
                return bs.LayDanhSachHocKi();
            }
        }
        public List<KhoaDaoTaoDTO> LayDanhSachKhoaDaoTao()
        {
            using(KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayDanhSachKhoaDaoTao();
            }
        }
        public int LayTongTatCaSinhVien(int idKhoaDT)
        {
            using(ThongKeBusiness bs = new ThongKeBusiness())
            {
                return bs.LayTongTatCaSinhVien(idKhoaDT);
            }
        }
        public int LayTongSinhVienDaDangKi(int idKhoaDT,int idHocKi)
        {
            using(ThongKeBusiness bs = new ThongKeBusiness())
            {
                return bs.LayTongSinhVienDaDangKi(idKhoaDT, idHocKi);
            }
        }
        public string LayTenKhoaDaoTao(int idKhoaDT)
        {
            using(ThongKeBusiness bs = new ThongKeBusiness())
            {
                return bs.LayTenKhoaDaoTao(idKhoaDT);
            }
        }
        public List<KeHoachHocTap_MoiDTO> LayKeHoachHocTapTheoKhoaDaoTaoVaHocKi(int idKhoaDT,int idHocKi)
        {
            using(ThongKeBusiness bs = new ThongKeBusiness())
            {
                return bs.LayKeHoachHocTapTheoKhoaDaoTaoVaHocKi(idKhoaDT, idHocKi);
            }
        }

        public List<KeHoachHocTap_MoiDTO> LayKeHoachHocTapSVDaDangKiTheoKhoaDaoTaoVaHocKi(int idKhoaDT, int idHocKi)
        {
            using (ThongKeBusiness bs = new ThongKeBusiness())
            {
                return bs.LayKeHoachHocTapSVDaDangKiTheoKhoaDaoTaoVaHocKi(idKhoaDT, idHocKi);
            }
        }
    }
}