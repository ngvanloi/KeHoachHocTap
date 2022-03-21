using Demo_Login2.Areas.AdminPage.Business;
using Demo_Login2.Models.DTO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_Login2.Areas.AdminPage.Controllers
{
    public class KetQuaLapKeHoachDangKiSinhVienController : Controller
    {
        // GET: AdminPage/KetQuaLapKeHoachDangKiSinhVien
        public ActionResult Index()
        {
            var lstketqua = this.LayDanhSachKetQuaLapKeHoachDangKiSinhVien(0, 0, 0);

            var lstkhoaDT = LayDanhSachKhoaDaoTao();
            var lsthocki = LayDanhSachHocKi();
            var lstsinhvien = LayDanhSachSinhVien();

            lstkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Chọn Khóa Đào Tạo"
            });
            lsthocki.Insert(0, new HocKiDTO
            {
                ID = 0,
                TenHocKi = "Chọn Học Kì"
            });
            lstsinhvien.Insert(0, new AccountDTO
            {
                ID = 0,
                Ma = "Chọn Mã Sinh Viên"
            });
            ViewData["khoaDT"] = new SelectList(lstkhoaDT, "ID", "TenKhoaDaoTao");
            ViewData["HK"] = new SelectList(lsthocki, "ID", "TenHocKi");
            ViewData["SV"] = new SelectList(lstsinhvien, "ID", "Ma");

            ViewBag.HocKi = LayDanhSachHocKi();
            ViewBag.SinhVien = LayDanhSachTaiKhoan();

            Session["lstketquakehoach"] = lstketqua;
            Session["idHocKi"] = 0;

            return View(lstketqua);
        }

        [HttpPost]
        public ActionResult Index(int idKhoaDT,int idHocKi,int idSinhVien)
        {
            var lstketqua = this.LayDanhSachKetQuaLapKeHoachDangKiSinhVien(idKhoaDT, idHocKi, idSinhVien);

            var lstkhoaDT = LayDanhSachKhoaDaoTao();
            var lsthocki = LayDanhSachHocKi();
            var lstsinhvien = LayDanhSachSinhVien();

            lstkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Chọn Khóa Đào Tạo"
            });
            lsthocki.Insert(0, new HocKiDTO
            {
                ID = 0,
                TenHocKi = "Chọn Học Kì"
            });
            lstsinhvien.Insert(0, new AccountDTO
            {
                ID = 0,
                Ma = "Chọn Mã Sinh Viên"
            });
            ViewData["khoaDT"] = new SelectList(lstkhoaDT, "ID", "TenKhoaDaoTao");
            ViewData["HK"] = new SelectList(lsthocki, "ID", "TenHocKi");
            ViewData["SV"] = new SelectList(lstsinhvien, "ID", "Ma");

            ViewBag.HocKi = LayDanhSachHocKi();
            ViewBag.SinhVien = LayDanhSachTaiKhoan();

            Session["lstketquakehoach"] = lstketqua;
            Session["idHocKi"] = idHocKi;

            return View(lstketqua);
        }

        //XuatFileExcel

        public void XuatBaoCaoKeHoachSinhVien()
        {
            ExcelPackage EP = new ExcelPackage();
            ExcelWorksheet Sheet = EP.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "STT";
            Sheet.Cells["B1"].Value = "Tên Sinh viên";
            Sheet.Cells["C1"].Value = "Khóa Đào Tạo";
            Sheet.Cells["D1"].Value = "Lớp";
            Sheet.Cells["E1"].Value = "Học kì";
            Sheet.Cells["F1"].Value = "Tên Môn Học";
            Sheet.Cells["G1"].Value = "Số Tín Chỉ";
            Sheet.Cells["H1"].Value = "Số Tiết Lý Thuyết";
            Sheet.Cells["I1"].Value = "Số Tiết Thực Hành";
            Sheet.Cells["J1"].Value = "Tên Khoa Bộ Môn";
            Sheet.Cells["K1"].Value = "Môn Học Tiên Quyết";
            Sheet.Cells["L1"].Value = "Môn Học Học Trước";
            Sheet.Cells["M1"].Value = "Loại Môn Học";
            Sheet.Cells["N1"].Value = "Trạng thái Đăng Kí";

            int row = 2;
            foreach(var item in (List<SinhVienDangKiKeHoachHocTapDTO>)Session["lstketquakehoach"])
            {
                Sheet.Cells[String.Format("A{0}", row)].Value = row - 1;
                Sheet.Cells[String.Format("B{0}", row)].Value = item.TenSinhVien;
                Sheet.Cells[String.Format("C{0}", row)].Value = item.TenKhoaDaoTao;
                Sheet.Cells[String.Format("D{0}", row)].Value = item.TenLop;
                Sheet.Cells[String.Format("E{0}", row)].Value = item.TenHocKi;
                Sheet.Cells[String.Format("F{0}", row)].Value = item.TenMonHoc;
                Sheet.Cells[String.Format("G{0}", row)].Value = item.SoTinChi;
                Sheet.Cells[String.Format("H{0}", row)].Value = item.SoTietLyThuyet;
                Sheet.Cells[String.Format("I{0}", row)].Value = item.SoTietThucHanh;
                Sheet.Cells[String.Format("J{0}", row)].Value = item.TenKhoaBoMon;
                Sheet.Cells[String.Format("K{0}", row)].Value = item.TenMonHocTienQuyet;
                Sheet.Cells[String.Format("L{0}", row)].Value = item.TenMonHocHocTruoc;
                Sheet.Cells[String.Format("M{0}", row)].Value = getLoaiDangKi((int)item.IDPhanLoaiMonHoc);
                Sheet.Cells[String.Format("N{0}", row)].Value = getTrangThai(item.TrangThai);
                row++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.BinaryWrite(EP.GetAsByteArray());
            Response.End();
        }

        public string getLoaiDangKi(int idPhanLoai)
        {
            string result = "";
            if (idPhanLoai == 1)
            {
                result = "Bắt Buộc";
            }
            else if(idPhanLoai == 2)
            {
                result = "Tự Chọn";
            }
            return result;
        }

        public string getTrangThai(Boolean trangthai)
        {
            string result = "";
            if(trangthai == true)
            {
                result = "Đã Đăng Kí";
            }
            else
            {
                result = "Chưa Đăng Kí";
            }
            return result;
        }

        public List<SinhVienDangKiKeHoachHocTapDTO> LayDanhSachKetQuaLapKeHoachDangKiSinhVien(int idKhoaDT, int idHocKi, int idSinhVien)
        {
            using(KetQuaLapKeHoachDangKiSinhVienBusiness bs = new KetQuaLapKeHoachDangKiSinhVienBusiness())
            {
                return bs.LayDanhSachKetQuaLapKeHoachDangKiSinhVien(idKhoaDT, idHocKi, idSinhVien);
            }
        }

        public List<KhoaDaoTaoDTO> LayDanhSachKhoaDaoTao()
        {
            using(KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
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

        public List<AccountDTO> LayDanhSachSinhVien()
        {
            using(TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayDanhSachSinhVien();
            }
        }

        public List<AccountDTO> LayDanhSachTaiKhoan()
        {
            using(TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayDanhSachTaiKhoan();
            }
        }


    }
}