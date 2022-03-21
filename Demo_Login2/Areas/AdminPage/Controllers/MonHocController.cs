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
    public class MonHocController : Controller
    {
        //LayDanhSachMonHoc
        public ActionResult Index()
        {
            var lstmonhoc = this.LayDanhSachMonHoc();
            ViewBag.phanloaikhoabm = LayDanhSachKhoaBoMon();
            return View(lstmonhoc);
        }

        public List<MonHocDTO> LayDanhSachMonHoc()
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayDanhSachMonHoc();
            }
        }
        //Get : TaoMonHoc
        public ActionResult Create()
        {
            ViewData["phanloaikhoabm"] = new SelectList(LayDanhSachKhoaBoMon(), "ID", "TenKhoaBoMon");
            return View();
        }

        //Post : TaoMonHoc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MonHocDTO monhoc)
        {

            var findMa = LayMaMonHocDaTonTai(monhoc.MaMonHoc);
            var findTen = LayTenMonHocDaTonTai(monhoc.TenMonHoc);
            var resultMa = LayYeuCauNhapMa(monhoc);
            var resultTenMon = LayYeuCauNhapTenMonHoc(monhoc);
            var resultsotinchi = LayYeuCauNhapSoTinChi(monhoc);

            var resultAll = false;

            if (resultMa == false)
            {
                resultAll = true;
                ViewBag.ErrorkhongcokituMa = "Yêu cầu nhập các trường bắt buộc";

            }
            if (resultTenMon == false)
            {
                ViewBag.ErrorkhongcokituTen = "Yêu cầu nhập các trường bắt buộc";
                resultAll = true;
            }
            if (monhoc.SoTinChi < 0)
            {
                ViewBag.ErrorSoTinChiNhoHon0 = "Số Tín Chỉ không được nhỏ hơn 0";
                resultAll = true;
            }
            if (findMa > 0)
            {
                ViewBag.ErrorMa = "Mã Môn Học đã tồn tại";
                resultAll = true;
            }
            if (findTen > 0)
            {
                ViewBag.ErrorTen = "Tên Môn Học đã tồn tại";
                resultAll = true;
            }
            if (monhoc.SoTietLyThuyet > 500)
            {
                ViewBag.ErrorDieuKienSoTietLT = "Số Tiết Lý Thuyết không được lớn hơn 500";
                resultAll = true;
            }
            if (monhoc.SoTietThucHanh > 500)
            {
                ViewBag.ErrorDieuKienSoTietTH = "Số Tiết Thực Hành không được lớn hơn 500";
                resultAll = true;
            }
            if (monhoc.SoTinChi > 20)
            {
                ViewBag.ErrorDieuKienSoTinChi = "Số Tín Chỉ không được lớn hơn 20";
                resultAll = true;
            }
            if (monhoc.SoTietLyThuyet < 0 || monhoc.SoTietThucHanh < 0)
            {
                ViewBag.ErrorSoTiet = "Số Tiết Lý Thuyết hoặc Số Tiết Thực Hành đều không được nhỏ hơn 0";
                resultAll = true;
            }
            if (resultAll == false)
            {

                ThemMonHoc(monhoc);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["phanloaikhoabm"] = new SelectList(LayDanhSachKhoaBoMon(), "ID", "TenKhoaBoMon");
                return View();
            }

        }

        //Upload Danh sach Excel 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(FormCollection formCollection)
        {
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (var package = new OfficeOpenXml.ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        ViewBag.Error = "";
                        List<MonHocDTO> monhocList = new List<MonHocDTO>();

                        List<int> lstRowError_MaMonHoc_Trung = new List<int>();
                        List<int> lstRowError_TenMonHoc_Trung = new List<int>();
                        List<int> lstRowError_MaMonHoc_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_TenMonHoc_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_KhoaBoMon_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_DieuKienSoTinChi_BeHon0 = new List<int>();
                        List<int> lstRowError_DieuKienSoTiet_BeHon0 = new List<int>();
                        List<int> lstRowError_SoTietLyThuyet_VuotQuaKiTu = new List<int>();
                        List<int> lstRowError_SoTietThucHanh_VuotQuaKiTu = new List<int>();
                        List<int> lstRowError_SoTinChi_VuotQuaKiTu = new List<int>();
                        List<int> lstRowError_MaMonHoc_VuotQuaKiTu = new List<int>();
                        List<int> lstRowError_TenMonHoc_VuotQuaKiTu = new List<int>();
                        List<int> lstRowError_MaMonHoc_BiTrungTrongFileExcel = new List<int>();
                        List<int> lstRowError_TenMonHoc_BiTrungTrongFileExcel = new List<int>();

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            try
                            {
                                
                                var mamonhoc = Convert.ToString(workSheet.Cells[rowIterator, 1].Value).Trim();
                                var tenmonhoc = Convert.ToString(workSheet.Cells[rowIterator, 2].Value).Trim();
                                var khoabomon = Convert.ToString(workSheet.Cells[rowIterator, 3].Value).Trim();
                                var sotietlythuyet = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value);
                                var sotietthuchanh = Convert.ToInt32(workSheet.Cells[rowIterator, 5].Value);
                                var sotinchi = Convert.ToInt32(workSheet.Cells[rowIterator, 6].Value);
                                var ghichu = Convert.ToString(workSheet.Cells[rowIterator, 7].Value);

                                var IDKhoaBoMon = LayIDKhoaBoMonTheoTen(khoabomon);
                                var check_mamonhoctontai = LayMaMonHocDaTonTai(mamonhoc);
                                var check_tenmonhoctontai = LayTenMonHocDaTonTai(tenmonhoc);

                                if(mamonhoc.Length == 0)
                                {
                                    lstRowError_MaMonHoc_KhongCoKiTu.Add(rowIterator);
                                }
                                if(tenmonhoc.Length == 0)
                                {
                                    lstRowError_TenMonHoc_KhongCoKiTu.Add(rowIterator);
                                }
                                if(IDKhoaBoMon == 0)
                                {
                                    lstRowError_KhoaBoMon_KhongCoKiTu.Add(rowIterator);
                                }
                                if(check_mamonhoctontai > 0)
                                {
                                    lstRowError_MaMonHoc_Trung.Add(rowIterator);
                                }
                                if(check_tenmonhoctontai > 0)
                                {
                                    lstRowError_TenMonHoc_Trung.Add(rowIterator);
                                }
                                if(sotinchi < 0)
                                {
                                    lstRowError_DieuKienSoTinChi_BeHon0.Add(rowIterator);
                                }
                                if(sotietlythuyet < 0 || sotietthuchanh < 0)
                                {
                                    lstRowError_DieuKienSoTiet_BeHon0.Add(rowIterator);
                                }
                                if(sotietlythuyet > 500)
                                {
                                    lstRowError_SoTietLyThuyet_VuotQuaKiTu.Add(rowIterator);
                                }
                                if(sotietthuchanh > 500)
                                {
                                    lstRowError_SoTietThucHanh_VuotQuaKiTu.Add(rowIterator);
                                }
                                if(sotinchi > 20)
                                {
                                    lstRowError_SoTinChi_VuotQuaKiTu.Add(rowIterator);
                                }
                                if(mamonhoc.Length > 7)
                                {
                                    lstRowError_MaMonHoc_VuotQuaKiTu.Add(rowIterator);
                                }
                                if(tenmonhoc.Length > 50)
                                {
                                    lstRowError_TenMonHoc_VuotQuaKiTu.Add(rowIterator);
                                }
                                else
                                {
                                    var checkloimamonhoctrungtrongExcel = monhocList.Where(s => s.MaMonHoc == mamonhoc).FirstOrDefault();
                                    var checkloitenmonhoctrungtrongExcel = monhocList.Where(s => s.TenMonHoc == tenmonhoc).FirstOrDefault();
                                    if (checkloimamonhoctrungtrongExcel != null)
                                    {
                                        lstRowError_MaMonHoc_BiTrungTrongFileExcel.Add(rowIterator);
                                    }
                                    if(checkloitenmonhoctrungtrongExcel != null)
                                    {
                                        lstRowError_TenMonHoc_BiTrungTrongFileExcel.Add(rowIterator);
                                    }
                                    else
                                    {
                                        var monhoc = new MonHocDTO();
                                        monhoc.MaMonHoc = mamonhoc;
                                        monhoc.TenMonHoc = tenmonhoc;
                                        monhoc.IDKhoaBoMon = IDKhoaBoMon;
                                        monhoc.SoTietLyThuyet = sotietlythuyet;
                                        monhoc.SoTietThucHanh = sotietthuchanh;
                                        monhoc.SoTinChi = sotinchi;
                                        monhoc.GhiChu = ghichu;

                                        monhocList.Add(monhoc);
                                    }
                                   
                                }

                            }catch(Exception ex)
                            {
                                throw ex;
                            }                            
                        }
                        if(lstRowError_MaMonHoc_Trung.Count() > 0 || lstRowError_TenMonHoc_Trung.Count() > 0 || lstRowError_MaMonHoc_KhongCoKiTu.Count() > 0 || lstRowError_TenMonHoc_KhongCoKiTu.Count() > 0
                            || lstRowError_KhoaBoMon_KhongCoKiTu.Count() > 0 || lstRowError_DieuKienSoTiet_BeHon0.Count() > 0 || lstRowError_SoTietLyThuyet_VuotQuaKiTu.Count() > 0 ||
                            lstRowError_SoTietThucHanh_VuotQuaKiTu.Count() > 0 || lstRowError_DieuKienSoTinChi_BeHon0.Count() > 0 || lstRowError_SoTinChi_VuotQuaKiTu.Count() > 0
                            || lstRowError_MaMonHoc_VuotQuaKiTu.Count() > 0 || lstRowError_TenMonHoc_VuotQuaKiTu.Count() > 0 || lstRowError_MaMonHoc_BiTrungTrongFileExcel.Count() > 0 || lstRowError_TenMonHoc_BiTrungTrongFileExcel.Count() > 0)
                        {
                            if (lstRowError_MaMonHoc_Trung.Count() > 0)
                            {
                                var error = "Các dòng bị trùng ở cột Mã Môn Học trong File Excel : ";
                                foreach (var item in lstRowError_MaMonHoc_Trung)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.trungdata += error + "</br>";
                            }
                            if (lstRowError_TenMonHoc_Trung.Count() > 0)
                            {
                                var error = "Các dòng bị trùng ở cột Tên Môn Học trong File Excel : ";
                                foreach (var item in lstRowError_TenMonHoc_Trung)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.trungdata += error + "</br>";
                            }
                            if (lstRowError_MaMonHoc_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Các dòng chưa nhập đầy đủ thông tin trong File Excel cột Mã Môn Học : ";
                                foreach (var item in lstRowError_MaMonHoc_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
                            }
                            if (lstRowError_TenMonHoc_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Các dòng chưa nhập đầy đủ thông tin trong File Excel cột Tên Môn Học : ";
                                foreach (var item in lstRowError_TenMonHoc_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
                            }
                            if (lstRowError_KhoaBoMon_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Các dòng chưa nhập đầy đủ thông tin trong File Excel cột Khoa Bộ Môn : ";
                                foreach (var item in lstRowError_KhoaBoMon_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
                            }
                            if (lstRowError_DieuKienSoTiet_BeHon0.Count() > 0)
                            {
                                var error = "Cột Số Tiết Lý Thuyết hoặc Thực Hành không được nhỏ hơn hoặc 0 : ";
                                foreach (var item in lstRowError_DieuKienSoTiet_BeHon0)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorDieuKienSoTietBeHon0 += error + "</br>";
                            }
                            if (lstRowError_SoTietLyThuyet_VuotQuaKiTu.Count() > 0)
                            {
                                var error = "Các dòng ở cột Số Tiết Lí Thuyết vượt quá 500 kí tự trong File Excel  : ";
                                foreach (var item in lstRowError_SoTietLyThuyet_VuotQuaKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorDieuKienSoTietLyThuyetLonHon500 += error + "</br>";
                            }
                            if (lstRowError_SoTietThucHanh_VuotQuaKiTu.Count() > 0)
                            {
                                var error = "Các dòng ở cột Số Tiết Thực Hành vượt quá 500 kí tự trong File Excel  : ";
                                foreach (var item in lstRowError_SoTietThucHanh_VuotQuaKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorDieuKienSoTietThucHanhLonHon500 += error + "</br>";
                            }
                            if (lstRowError_DieuKienSoTinChi_BeHon0.Count() > 0)
                            {
                                var error = "Cột Số Tín Chỉ không được nhỏ hơn 0  : ";
                                foreach (var item in lstRowError_DieuKienSoTinChi_BeHon0)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorDieuKienSoTinChiBeHon0 += error + "</br>";
                            }
                            if (lstRowError_SoTinChi_VuotQuaKiTu.Count() > 0)
                            {
                                var error = "Các dòng ở cột Số Tín Chỉ vượt quá 20 kí tự trong File Excel  : ";
                                foreach (var item in lstRowError_SoTinChi_VuotQuaKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorDieuKienSoTinChi += error + "</br>";
                            }
                            if (lstRowError_MaMonHoc_VuotQuaKiTu.Count() > 0)
                            {
                                var error = "Các dòng ở cột Mã Môn Học vượt quá 7 kí tự trong File Excel  : ";
                                foreach (var item in lstRowError_MaMonHoc_VuotQuaKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errordodaima += error + "</br>";
                            }
                            if (lstRowError_TenMonHoc_VuotQuaKiTu.Count() > 0)
                            {
                                var error = "Các dòng ở cột Tên Môn Học vượt quá 50 kí tự trong File Excel  : ";
                                foreach (var item in lstRowError_TenMonHoc_VuotQuaKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorDieuKienTenMonHoc += error + "</br>";
                            }
                            if (lstRowError_MaMonHoc_BiTrungTrongFileExcel.Count() > 0)
                            {
                                var error = "Mã Môn Học bị trùng trong File Excel  : ";
                                foreach (var item in lstRowError_MaMonHoc_BiTrungTrongFileExcel)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorMaMonHocBiTrungTrongExcel += error + "</br>";
                            }
                            if (lstRowError_TenMonHoc_BiTrungTrongFileExcel.Count() > 0)
                            {
                                var error = "Tên Môn Học bị trùng trong File Excel  : ";
                                foreach (var item in lstRowError_TenMonHoc_BiTrungTrongFileExcel)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorTenMonHocBiTrungTrongExcel += error + "</br>";
                            }
                            var lstmonhoc = this.LayDanhSachMonHoc();
                            ViewBag.phanloaikhoabm = LayDanhSachKhoaBoMon();
                            return View("Index", lstmonhoc);

                        }
                        else
                        {
                            foreach (var item in monhocList)
                            {
                                ThemMonHoc(item);
                            }
                        }

                    }
                }
                else
                {
                    ViewBag.Error = "Chưa có File";
                    var lstmonhoc = this.LayDanhSachMonHoc();
                    ViewBag.phanloaikhoabm = LayDanhSachKhoaBoMon();
                    return View("Index", lstmonhoc);

                }
            }
            ViewBag.Success = "Thành công";
            var lstmh = this.LayDanhSachMonHoc();
            ViewBag.phanloaikhoabm = LayDanhSachKhoaBoMon();
            return View("Index", lstmh);
        }


        public int LayMaMonHocDaTonTai(string mamonhoc)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayMaMonHocDaTonTai(mamonhoc);
            }
        }

        public int LayTenMonHocDaTonTai(string tenmonhoc)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayTenMonHocDaTonTai(tenmonhoc);
            }
        }
        public List<HocPhanTienQuyetDTO> LayDanhSachHocPhanTienQuyet()
        {
            using (HocPhanTienQuyetBusiness bs = new HocPhanTienQuyetBusiness())
            {
                return bs.LayDanhSachHocPhanTienQuyet();
            }
        }

        public List<HocPhanHocTruocDTO> LayDanhSachHocPhanHocTruoc()
        {
            using (HocPhanHocTruocBusiness bs = new HocPhanHocTruocBusiness())
            {
                return bs.LayDanhSachHocPhanHocTruoc();
            }
        }

        public List<KhoaBoMonDTO> LayDanhSachKhoaBoMon()
        {
            using (KhoaBoMonBusiness bs = new KhoaBoMonBusiness())
            {
                return bs.LayDanhSachKhoaBoMon();
            }
        }

        public bool LayYeuCauNhapMa(MonHocDTO monhoc)
        {
            if (monhoc.MaMonHoc == null)
            {
                return false;
            }
            return true;
        }

        public bool LayYeuCauNhapTenMonHoc(MonHocDTO monhoc)
        {
            if (monhoc.TenMonHoc == null)
            {
                return false;
            }
            return true;
        }

        public bool LayYeuCauNhapSoTinChi(MonHocDTO monhoc)
        {
            if (monhoc.SoTinChi == 0)
            {
                return false;
            }
            return true;
        }

        public bool LayMaMonHocKhongCoKiTu(MonHocDTO monhoc)
        {
            if (String.IsNullOrEmpty(monhoc.MaMonHoc))
            {
                return false;
            }
            return true;
        }

        public bool LayTenMonHocKhongCoKiTu(MonHocDTO monhoc)
        {
            if (String.IsNullOrEmpty(monhoc.TenMonHoc))
            {
                return false;
            }
            return true;
        }

        public bool ThemMonHoc(MonHocDTO monhoc)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.ThemMonHoc(monhoc);
            }
        }

        //Get : SuaMonHoc
        public ActionResult Edit(int id)
        {
            ViewData["phanloaikhoabm"] = new SelectList(LayDanhSachKhoaBoMon(), "ID", "TenKhoaBoMon");
            MonHocDTO monhoc = LayMonHoc(id);
            return View(monhoc);
        }

        //Post : SuaMonHoc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MonHocDTO monhoc)
        {

            var findMa = LayMaMonHocDaTonTai(monhoc.MaMonHoc);
            var findTen = LayTenMonHocDaTonTai(monhoc.TenMonHoc);

            var resultAll = false;
            var resultMa = LayYeuCauNhapMa(monhoc);
            var resultTenMon = LayYeuCauNhapTenMonHoc(monhoc);
            


            if ((findMa == monhoc.ID || findMa == 0) && (findTen == monhoc.ID || findTen == 0) && (monhoc.SoTietLyThuyet >= 0 && monhoc.SoTietThucHanh >= 0)
                && resultMa == true && resultTenMon == true
                && monhoc.SoTietLyThuyet <= 500 && monhoc.SoTietThucHanh <= 500 && monhoc.SoTinChi <= 20 && monhoc.SoTinChi > 0 || monhoc.SoTinChi == 0 && resultAll == false)
            {
                SuaMonHoc(monhoc);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
            else
            {
                if (resultMa == false)
                {
                    resultAll = true;
                    ViewBag.ErrorkhongcokituMa = "Yêu cầu nhập các trường bắt buộc";
                }
                if (resultTenMon == false)
                {
                    resultAll = true;
                    ViewBag.ErrorkhongcokituTen = "Yêu cầu nhập các trường bắt buộc";
                }
               
                
                if (findMa != monhoc.ID && findMa > 0)
                {
                    resultAll = true;
                    ViewBag.ErrorMa = "Mã Môn Học đã tồn tại";
                }
                if (monhoc.SoTietLyThuyet > 500)
                {
                    resultAll = true;
                    ViewBag.ErrorDieuKienSoTietLT = "Số Tiết Lý Thuyết không được lớn hơn 500";
                }
                if (monhoc.SoTietThucHanh > 500)
                {
                    resultAll = true;
                    ViewBag.ErrorDieuKienSoTietTH = "Số Tiết Thực Hành không được lớn hơn 500";
                }
                if (monhoc.SoTinChi > 20)
                {
                    resultAll = true;
                    ViewBag.ErrorDieuKienSoTinChi = "Số Tín Chỉ không được lớn hơn 20";
                }
                if (monhoc.SoTietLyThuyet < 0 || monhoc.SoTietThucHanh < 0)
                {
                    resultAll = true;
                    ViewBag.ErrorSoTiet = "Số Tiết Lý Thuyết hoặc Số Tiết Thực Hành đều không được nhỏ hơn 0";
                }
                if (monhoc.SoTinChi < 0)
                {
                    resultAll = true;
                    ViewBag.ErrorSoTinChiBeHon0 = "Số Tín Chỉ không được bé hơn 0";
                }
                if (findTen != monhoc.ID && findTen > 0)
                {
                    resultAll = true;
                    ViewBag.ErrorTen = "Tên Môn Học đã tồn tại";
                }
                ViewData["phanloaikhoabm"] = new SelectList(LayDanhSachKhoaBoMon(), "ID", "TenKhoaBoMon");
                return View();
            }
            
        }

        public MonHocDTO LayMonHoc(int id)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayMonHoc(id);
            }
        }

        public bool SuaMonHoc(MonHocDTO monhoc)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.SuaMonHoc(monhoc);
            }
        }

        //XoaMonHoc
        public async Task<ActionResult> Delete(int id)
        {
            var findmonhoc = CheckLoiMonHocDaTonTai(id);
            var findketquahoctap = CheckLoiKetQuaHocTapDaTonTai(id);
            var findketqualapkehoachdkisv = CheckLoiKetQuaLapKeHoachDangKiSinhVien(id);
            var findmonhoc_chuongtrinhdaotao_moi = CheckLoiChuongTrinhDaoTao_Moi(id);
            var findmonhoc_kehoachhoctap_moi = CheckLoiKeHoachHocTap_Moi(id);
            var findmonhoc_sinhviendangkikehoachhoctap = CheckLoiSinhVienDangKiKeHoachHocTap_Moi(id);
            if (findmonhoc > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaikhoabm = LayDanhSachKhoaBoMon();
                return RedirectToAction("Index");
            }
            else if (findketquahoctap > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaikhoabm = LayDanhSachKhoaBoMon();
                return RedirectToAction("Index");
            }
            else if (findketqualapkehoachdkisv > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaikhoabm = LayDanhSachKhoaBoMon();
                return RedirectToAction("Index");
            }
            else if(findmonhoc_chuongtrinhdaotao_moi > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaikhoabm = LayDanhSachKhoaBoMon();
                return RedirectToAction("Index");
            }
            else if (findmonhoc_kehoachhoctap_moi > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaikhoabm = LayDanhSachKhoaBoMon();
                return RedirectToAction("Index");
            }
            else if (findmonhoc_sinhviendangkikehoachhoctap > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaikhoabm = LayDanhSachKhoaBoMon();
                return RedirectToAction("Index");
            }
            else
            {
                var output = XoaMonHoc(id);
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

        public bool XoaMonHoc(int id)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.XoaMonHoc(id);
            }
        }

        public int CheckLoiMonHocDaTonTai(int? id)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.CheckLoiMonHocDaTonTai(id);
            }
        }

        public int CheckLoiKetQuaHocTapDaTonTai(int? id)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.CheckLoiKetQuaHocTapDaTonTai(id);
            }
        }

        public int CheckLoiKetQuaLapKeHoachDangKiSinhVien(int? id)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.CheckLoiKetQuaLapKeHoachDangKiSinhVien(id);
            }
        }
        public int CheckLoiChuongTrinhDaoTao_Moi(int? id)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.CheckLoiChuongTrinhDaoTao_Moi(id);
            }
        }
        public int CheckLoiKeHoachHocTap_Moi(int? id)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.CheckLoiKeHoachHocTap_Moi(id);
            }
        }
        public int CheckLoiSinhVienDangKiKeHoachHocTap_Moi(int? id)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.CheckLoiSinhVienDangKiKeHoachHocTap_Moi(id);
            }
        }

        //XemChiTietMonHoc
        public ActionResult Details(int id)
        {
            var hocki = LayMonHoc(id);
            ViewBag.phanloaikhoabm = LayDanhSachKhoaBoMon();
            return View(hocki);
        }

        public int LayIDKhoaBoMonTheoTen(string tenkhoabm)
        {
            using(MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayIDKhoaBoMonTheoTen(tenkhoabm);
            }
        }
    }
}