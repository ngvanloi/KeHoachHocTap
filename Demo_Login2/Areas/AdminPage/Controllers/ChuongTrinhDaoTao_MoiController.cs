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
    public class ChuongTrinhDaoTao_MoiController : Controller
    {
        // GET: LayDanhSachChuongTrinhDaoTao_Moi
        public ActionResult Index()
        {
            var lstctrdaotao = this.LayDanhSachChuongTrinhDaoTao_Moi_TheoKhoaDaoTao(0);
            ThuVienChung();
            return View(lstctrdaotao);
        }

        //Post :LayDanhSachChuongTrinhDaoTao_Moi
        [HttpPost]
        public ActionResult Index(int id)
        {
            var lstctrdaotao = this.LayDanhSachChuongTrinhDaoTao_Moi_TheoKhoaDaoTao(id);
            ThuVienChung();
            return View(lstctrdaotao);
        }

        public async Task<ActionResult> Delete(int idKhoaDT)
        {
            
            
            if (idKhoaDT > 0)
            {
                var acc = Check_XoaKhoaDaoTaoKhiKhongCoDuLieu(idKhoaDT);
                if (acc == false)
                {
                    var output = Xoa_ChuongTrinhDaoTao_Moi(idKhoaDT);
                    if (output)
                    {
                        TempData["Success"] = "Thành công";
                        return RedirectToAction("Index");

                    }
                }
                TempData["Errorkhongcodulieu"] = "Không có dữ liệu để xóa";
                return RedirectToAction("Index");

            }
            TempData["Error"] = "Thất bại";
            return RedirectToAction("Index");

        }

        public void ThuVienChung()
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
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao", 0);
        }

        //Upload Danh sach Excel 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(FormCollection formCollection, int id)
        {
            if (Request != null)
            {
                var checktrung = LayDanhSachChuongTrinhDaoTaoDaTonTai(id);
                if (checktrung == false)
                {
                    ViewBag.ErrorTrungKhoaDT = "Lỗi Chương Trình Đào Tạo Đã Có trong Khóa này";
                    var lstctrdaotao1 = this.LayDanhSachChuongTrinhDaoTao_Moi_TheoKhoaDaoTao(0);
                    ThuVienChung();
                    return View("Index", lstctrdaotao1);
                }
                else
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

                            List<ChuongTrinhDaoTao_MoiDTO> chuongtrinhdaotao = new List<ChuongTrinhDaoTao_MoiDTO>();
                            List<int> Loi_MonHoc_TonTai = new List<int>();
                            List<int> Loi_PhanLoai_TonTai = new List<int>();
                            List<int> Loi_MonHocTienQuyet_TonTai = new List<int>();
                            List<int> Loi_MonHocHocTruoc_TonTai = new List<int>();
                            List<int> Loi_HocKi_TonTai = new List<int>();
                            List<int> lstRowError_MaMonHoc_BiTrungTrenFileExcel = new List<int>();
                            List<int> lstRowError_TenMonHoc_BiTrungTrenFileExcel = new List<int>();
                            for (int rowIterator = 6; rowIterator <= noOfRow; rowIterator++)
                            {
                                try
                                {
                                    var danhsachchuongtrinhdaotao = new ChuongTrinhDaoTao_MoiDTO();
                                    var stt = Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value);
                                    var mamonhoc = Convert.ToString(workSheet.Cells[rowIterator, 4].Value).Trim();
                                    var tenmonhoc = Convert.ToString(workSheet.Cells[rowIterator, 5].Value).Trim();
                                    var sotinchi = Convert.ToInt32(workSheet.Cells[rowIterator, 6].Value);
                                    var sotietlythuyet = Convert.ToInt32(workSheet.Cells[rowIterator, 7].Value);
                                    var sotietthuchanh = Convert.ToInt32(workSheet.Cells[rowIterator, 8].Value);
                                    var phanloaimonhoc = Convert.ToString(workSheet.Cells[rowIterator, 9].Value).Trim();
                                    var hocki = Convert.ToInt32(workSheet.Cells[rowIterator, 11].Value);
                                    var hocphantienquyet = Convert.ToString(workSheet.Cells[rowIterator, 12].Value).Trim();
                                    var hocphanhoctruoc = Convert.ToString(workSheet.Cells[rowIterator, 13].Value).Trim();

                                    var IDMonHocTheoMaMonHocVaSoTinChi = LayIDMonHocTheoMaMonHocVaSoTinChi(mamonhoc, sotinchi);
                                    var IDMonHocTheoTenMonHoc = LayIDMonHocTheoTenMonHoc(tenmonhoc);
                                    var IDPhanLoaiMonHoc = LayIDPhanLoaiMonHoc(phanloaimonhoc);
                                    var IDHocKi = LayIDHocKi(hocki);
                                    var IDHocPhanTienQuyet = LayIDHocPhanTienQuyet(hocphantienquyet);
                                    var IDHocPhanHocTruoc = LayIDHocPhanHocTruoc(hocphanhoctruoc);



                                    if (IDMonHocTheoMaMonHocVaSoTinChi <= 0)
                                    {
                                        Loi_MonHoc_TonTai.Add(rowIterator);
                                    }
                                    else if (IDPhanLoaiMonHoc <= 0)
                                    {
                                        Loi_PhanLoai_TonTai.Add(rowIterator);
                                    }
                                    else if (String.IsNullOrEmpty(hocphantienquyet) == false && IDHocPhanTienQuyet <= 0)
                                    {
                                        
                                            Loi_MonHocTienQuyet_TonTai.Add(rowIterator);
                                    }
                                    else if (String.IsNullOrEmpty(hocphanhoctruoc) == false && IDHocPhanHocTruoc <= 0)
                                    {
                                        
                                            Loi_MonHocHocTruoc_TonTai.Add(rowIterator);
                                    }
                                    else if (IDHocKi == false)
                                    {
                                        Loi_HocKi_TonTai.Add(rowIterator);
                                    }
                                    else
                                    {
                                        var checkloimamonhoctrungtrenExcel = chuongtrinhdaotao.Where(s => s.IDMonHoc == IDMonHocTheoMaMonHocVaSoTinChi).FirstOrDefault();
                                        var checkloitenmonhoctrungtrenExcel = chuongtrinhdaotao.Where(s => s.IDMonHoc == IDMonHocTheoTenMonHoc).FirstOrDefault();
                                        if (checkloimamonhoctrungtrenExcel != null)
                                        {
                                            lstRowError_MaMonHoc_BiTrungTrenFileExcel.Add(rowIterator);
                                        }
                                        if (checkloitenmonhoctrungtrenExcel != null)
                                        {
                                            lstRowError_TenMonHoc_BiTrungTrenFileExcel.Add(rowIterator);
                                        }
                                        else
                                        {
                                            danhsachchuongtrinhdaotao.IDMonHoc = IDMonHocTheoMaMonHocVaSoTinChi;
                                            danhsachchuongtrinhdaotao.SoTinChi = sotinchi;
                                            danhsachchuongtrinhdaotao.SoTietLyThuyet = sotietlythuyet;
                                            danhsachchuongtrinhdaotao.SoTietThucHanh = sotietthuchanh;
                                            danhsachchuongtrinhdaotao.TenMonHocTienQuyet = hocphantienquyet;
                                            danhsachchuongtrinhdaotao.TenMonHocHocTruoc = hocphanhoctruoc;
                                            danhsachchuongtrinhdaotao.IDPhanLoaiMonHoc = IDPhanLoaiMonHoc;
                                            danhsachchuongtrinhdaotao.IDHocKi = hocki;
                                            danhsachchuongtrinhdaotao.IDKhoaDaoTao = id;

                                            chuongtrinhdaotao.Add(danhsachchuongtrinhdaotao);
                                        }
                                            
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            if (Loi_MonHoc_TonTai.Count() > 0 || Loi_PhanLoai_TonTai.Count() > 0 || Loi_MonHocTienQuyet_TonTai.Count() > 0 || Loi_MonHocHocTruoc_TonTai.Count() > 0 || Loi_HocKi_TonTai.Count() > 0
                                || lstRowError_MaMonHoc_BiTrungTrenFileExcel.Count() > 0 || lstRowError_TenMonHoc_BiTrungTrenFileExcel.Count() > 0)
                            {
                                if (Loi_MonHoc_TonTai.Count() > 0)
                                {
                                    var error = "Lỗi Môn học chưa tồn tại trong Danh Sách Môn Học tại cột Môn Học hoặc Số Tín Chỉ : ";
                                    foreach (var item in Loi_MonHoc_TonTai)
                                    {
                                        error += "Dòng " + item + " ";
                                    }
                                    ViewBag.ErrorLoiMonHoc += error + "<br/>";
                                }
                                if (Loi_PhanLoai_TonTai.Count() > 0)
                                {
                                    var error = "Lỗi Phân Loại Môn Học chưa tồn tại trong Danh Sách Môn Học tại cột Phân Loại Môn Học : ";
                                    foreach (var item in Loi_PhanLoai_TonTai)
                                    {
                                        error += "Dòng " + item + " ";
                                    }
                                    ViewBag.ErrorLoiPhanLoaiMonHoc += error + "<br/>";
                                }
                                if (Loi_MonHocTienQuyet_TonTai.Count() > 0)
                                {
                                    var error = "Lỗi Môn Học chưa tồn tại trong Danh Sách Môn Học tại cột Học Phần Tiên Quyết : ";
                                    foreach (var item in Loi_MonHocTienQuyet_TonTai)
                                    {
                                        error += "Dòng " + item + " ";
                                    }
                                    ViewBag.ErrorLoiMonHoc += error + "<br/>";
                                }
                                if (Loi_MonHocHocTruoc_TonTai.Count() > 0)
                                {
                                    var error = "Lỗi Môn Học chưa tồn tại trong Danh Sách Môn Học tại cột Học Phần Học Trước : ";
                                    foreach (var item in Loi_MonHocTienQuyet_TonTai)
                                    {
                                        error += "Dòng " + item + " ";
                                    }
                                    ViewBag.ErrorLoiMonHoc += error + "<br/>";
                                }
                                if (Loi_HocKi_TonTai.Count() > 0)
                                {
                                    var error = "Lỗi Hoc Kì chưa tồn tại trong Danh Sách : ";
                                    foreach (var item in Loi_HocKi_TonTai)
                                    {
                                        error += "Dòng " + item + " ";
                                    }
                                    ViewBag.ErrorHocKi += error + "<br/>";
                                }
                                if (lstRowError_MaMonHoc_BiTrungTrenFileExcel.Count() > 0)
                                {
                                    var error = "Lỗi Mã Môn Học bị trùng trên File Excel : ";
                                    foreach (var item in lstRowError_MaMonHoc_BiTrungTrenFileExcel)
                                    {
                                        error += "Dòng " + item + " ";
                                    }
                                    ViewBag.ErrorMaMonHocBiTrungTrenExcel += error + "<br/>";
                                }
                                if (lstRowError_TenMonHoc_BiTrungTrenFileExcel.Count() > 0)
                                {
                                    var error = "Lỗi Tên Môn Học bị trùng trên File Excel : ";
                                    foreach (var item in lstRowError_TenMonHoc_BiTrungTrenFileExcel)
                                    {
                                        error += "Dòng " + item + " ";
                                    }
                                    ViewBag.ErrorTenMonHocBiTrungTrenExcel += error + "<br/>";
                                }

                                var lstctrdaotao1 = this.LayDanhSachChuongTrinhDaoTao_Moi_TheoKhoaDaoTao(0);
                                ThuVienChung();
                                return View("Index", lstctrdaotao1);

                            }
                            else
                            {
                                foreach (var item in chuongtrinhdaotao)
                                {
                                    ThemChuongTrinhDaoTao_Moi(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Chưa có File";
                        var lstctrdaotao1 = this.LayDanhSachChuongTrinhDaoTao_Moi_TheoKhoaDaoTao(0);
                        ThuVienChung();
                        return View("Index", lstctrdaotao1);
                    }
                }

            }
            TempData["Success"] = "Thành công";
            //var lstctrdaotao2 = this.LayDanhSachChuongTrinhDaoTao_Moi_TheoKhoaDaoTao(id);
            //ThuVienChung();
            //return View("Index", id);
            return RedirectToAction("Index");

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
        public int LayIDMonHocTheoMaMonHocVaSoTinChi(string mamonhoc, int sotinchi)
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.LayIDMonHocTheoMaMonHocVaSoTinChi(mamonhoc, sotinchi);
            }
        }

        public int LayIDMonHocTheoTenMonHoc(string tenmonhoc)
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.LayIDMonHocTheoTenMonHoc(tenmonhoc);
            }
        }

        public int LayIDPhanLoaiMonHoc(string phanloai)
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.LayIDPhanLoaiMonHoc(phanloai);
            }
        }
        public bool ThemChuongTrinhDaoTao_Moi(ChuongTrinhDaoTao_MoiDTO lstctrdaotao)
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.ThemChuongTrinhDaoTao_Moi(lstctrdaotao);
            }
        }

        public bool LayDanhSachChuongTrinhDaoTaoDaTonTai(int id)
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.LayDanhSachChuongTrinhDaoTaoDaTonTai(id);
            }
        }
        public int LayIDHocPhanTienQuyet(string hocphanHT)
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.LayIDHocPhanTienQuyet(hocphanHT);
            }
        }
        public int LayIDHocPhanHocTruoc(string hocphanHT)
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.LayIDHocPhanHocTruoc(hocphanHT);
            }
        }
        public bool LayIDHocKi(int id)
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.LayIDHocKi(id);
            }
        }

        public bool Xoa_ChuongTrinhDaoTao_Moi(int idKhoaDT)
        {
            using(ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.Xoa_ChuongTrinhDaoTao_Moi(idKhoaDT);
            }
        }

        public bool Check_XoaKhoaDaoTaoKhiKhongCoDuLieu(int idKhoaDT)
        {
            using (ChuongTrinhDaoTao_MoiBusiness bs = new ChuongTrinhDaoTao_MoiBusiness())
            {
                return bs.Check_XoaKhoaDaoTaoKhiKhongCoDuLieu(idKhoaDT);
            }
        }
    }
}