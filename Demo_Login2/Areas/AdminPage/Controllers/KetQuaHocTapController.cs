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
    public class KetQuaHocTapController : Controller
    {
        //LaydanhsachKetQuaHocTap
        public ActionResult Index()
        {
            var lstketqua = this.LayDanhSachKetQuaHocTap();
            ViewBag.taikhoan = LayDanhSachTaiKhoan();
            ViewBag.monhoc = LayDanhSachMonHoc();
            return View(lstketqua);
        }

        public List<KetQuaHocTapDTO> LayDanhSachKetQuaHocTap()
        {
            using (KetQuaHocTapBusiness bs = new KetQuaHocTapBusiness())
            {
                return bs.LayDanhSachKetQuaHocTap();
            }
        }
        public List<AccountDTO> LayDanhSachTaiKhoan()
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayDanhSachTaiKhoan();
            }
        }
        public List<MonHocDTO> LayDanhSachMonHoc()
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayDanhSachMonHoc();
            }
        }

        //Upload Danh sach Excel 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(FormCollection formCollection)
        {
            if(Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (var package = new OfficeOpenXml.ExcelPackage(file.InputStream))
                    {
                        var currenSheet = package.Workbook.Worksheets;
                        var workSheet = currenSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;
                        ViewBag.Error = "";
                        List<KetQuaHocTapDTO> listketqua = new List<KetQuaHocTapDTO>();
                        for(int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var ketqua = new KetQuaHocTapDTO();
                            ketqua.IDAccount = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            ketqua.IDMonHoc = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                            ketqua.SoTinChi = Convert.ToInt32(workSheet.Cells[rowIterator, 3].Value);
                            ketqua.Diem = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value);
                            ketqua.GhiChu = Convert.ToString(workSheet.Cells[rowIterator, 5].Value).Trim();
                            ketqua.KetQua = LayKetQuaTheoDiem(ketqua.Diem);
                            ketqua.DiemChu = LayDiemChuTheoDiem(ketqua.Diem);

                            var findmonhoc = LayMonHocTheoAccountDaTonTai(ketqua.IDMonHoc, ketqua.IDAccount);
                            if(findmonhoc > 0)
                            {
                                ViewBag.trungdata = String.Format("Dữ liệu bị trùng ở hàng {0} cột Môn Học trong file Excel", rowIterator);
                                var lstketqua = this.LayDanhSachKetQuaHocTap();
                                ViewBag.taikhoan = LayDanhSachTaiKhoan();
                                ViewBag.monhoc = LayDanhSachMonHoc();
                                return View("Index",lstketqua);
                            }
                            else
                            {
                                listketqua.Add(ketqua);
                            }
                        }
                        foreach(var item in listketqua)
                        {
                            ThemKetQuaHocTap(item);
                        }
                    }
                }
                else
                {
                    ViewBag.Error = "Chưa có File";
                    var lstketqua = this.LayDanhSachKetQuaHocTap();
                    ViewBag.taikhoan = LayDanhSachTaiKhoan();
                    ViewBag.monhoc = LayDanhSachMonHoc();
                    return View("Index", lstketqua);
                }
            }
            ViewBag.Success = "Thành công";
            var lstkq = this.LayDanhSachKetQuaHocTap();
            ViewBag.taikhoan = LayDanhSachTaiKhoan();
            ViewBag.monhoc = LayDanhSachMonHoc();
            return View("Index", lstkq);

        }

        public List<MonHocDTO> LayDanhSachMonHocDeChon(int id)
        {
            using(MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayDanhSachMonHocDeChon(id);
            }
        }

        public ActionResult Create_LaySoTinChiTheoMonHoc(int id)
        {
            HttpCookie idMonHoc = HttpContext.Request.Cookies.Get("idMonHoc");
            HttpCookie idAccount = HttpContext.Request.Cookies.Get("idAccount");

            ViewData["tensinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVien(), "ID", "HoVaTen",idAccount.Value);
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc",idMonHoc.Value);
            ViewData["sotinchi"] = new SelectList(LayDanhSachMonHocDeChon(id), "ID", "SoTinChi");
            return View("Create");
        }

        //Get : TaoketQuaHocTap
        public ActionResult Create()
        {
            var lstmonhoc = LayDanhSachMonHoc();

            ViewData["tensinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVien(), "ID", "HoVaTen");
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
            ViewData["sotinchi"] = new SelectList(LayDanhSachMonHocDeChon(lstmonhoc[0].ID), "ID", "SoTinChi");
            return View();
        }

        //Post : TaoKetQuaHocTap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(KetQuaHocTapDTO ketqua)
        {           
            var findmonhoc = LayMonHocTheoAccountDaTonTai(ketqua.IDMonHoc,ketqua.IDAccount);
            Response.Cookies.Remove("idAccount");
            Response.Cookies.Remove("idMonHoc");
            if (findmonhoc > 0)
            {
                ViewBag.Error = "Môn Học trong sinh viên này đã tồn tại";
                ViewData["tensinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVien(), "ID", "HoVaTen");
                ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
                ViewData["sotinchi"] = new SelectList(LayDanhSachMonHocDeChon(Convert.ToInt32(ketqua.IDMonHoc)), "ID", "SoTinChi");
                return View();
            }else
            {
                ketqua.KetQua = LayKetQuaTheoDiem(ketqua.Diem);
                ketqua.DiemChu = LayDiemChuTheoDiem(ketqua.Diem);
                ThemKetQuaHocTap(ketqua);
                return RedirectToAction("Index");
            }
        }

        public bool LayKetQuaTheoDiem(double diem)
        {
            var result = false;
            if (diem >= 4)
            {
                result = true;
            }
            return result;
        }
        public string LayDiemChuTheoDiem(double diem)
        {
            var result = "";
            if (diem == 10)
            {
                result = "A+";
            }
            else if(diem >= 8)
            {
               result = "A";
            }
            else if (diem >= 7 && diem < 8)
            {
                result = "B";
            }
            else if (diem >= 6.5 && diem < 7)
            {
                result = "C+";
            }
            else if (diem >= 5 && diem < 6.5)
            {
                result = "D+";
            }
            else if (diem >= 4 && diem < 5)
            {
                result = "D";
            }
            else if (diem >= 0.1 && diem < 4)
            {
                result = "F";
            }
            else if (diem == 0)
            {
                result = "VT";
            }
            return result;
        }

        public int LayMonHocTheoAccountDaTonTai(int? idMonHoc, int? idAccount)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayMonHocTheoAccountDaTonTai(idMonHoc, idAccount);
            }
        }

        public bool ThemKetQuaHocTap(KetQuaHocTapDTO ketqua)
        {
            using(KetQuaHocTapBusiness bs = new KetQuaHocTapBusiness())
            {
                return bs.ThemKetQuaHocTap(ketqua);
            }
        }

        public List<AccountDTO> LayDanhSachTaiKhoan_SinhVien()
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayDanhSachTaiKhoan_SinhVien();
            }
        }

        public ActionResult Edit_LaySoTinChiTheoMonHoc(int id)
        {
            HttpCookie idMonHoc = HttpContext.Request.Cookies.Get("idMonHoc");
            HttpCookie idAccount = HttpContext.Request.Cookies.Get("idAccount");

            ViewData["tensinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVien(), "ID", "HoVaTen", idAccount.Value);
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc", idMonHoc.Value);
            ViewData["sotinchi"] = new SelectList(LayDanhSachMonHocDeChon(id), "ID", "SoTinChi");
            return View("Edit");
        }

        //Get : SuaKetQuaHocTap
        public ActionResult Edit(int id)
        {
            HttpCookie idEdit = new HttpCookie("idEdit");
            idEdit.Value = id.ToString();
            idEdit.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(idEdit);

            KetQuaHocTapDTO ketqua = LayKetQuaHocTap(id);
            ViewData["tensinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVien(), "ID", "HoVaTen");
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
            ViewData["sotinchi"] = new SelectList(LayDanhSachMonHocDeChon(Convert.ToInt32(ketqua.IDMonHoc)), "ID", "SoTinChi");
            return View(ketqua);
        }

        //Post : SuaKetQuaHocTap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(KetQuaHocTapDTO ketqua)
        {
            
            var findmonhoc = LayMonHocTheoAccountDaTonTai(ketqua.IDMonHoc, ketqua.IDAccount);
            Response.Cookies.Remove("idMonHoc");
            Response.Cookies.Remove("idAccount");
            if (findmonhoc == ketqua.ID || findmonhoc == 0)
            {
                ketqua.KetQua = LayKetQuaTheoDiem(ketqua.Diem);
                ketqua.DiemChu = LayDiemChuTheoDiem(ketqua.Diem);
                SuaKetQuaHocTap(ketqua);
                return RedirectToAction("Index");
            }
            else if (findmonhoc != ketqua.ID && findmonhoc > 0)
            {
                    ViewBag.Error = "Môn Học trong sinh viên này đã tồn tại";
                    ViewData["tensinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVien(), "ID", "HoVaTen");
                    ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
                    ViewData["sotinchi"] = new SelectList(LayDanhSachMonHocDeChon(Convert.ToInt32(ketqua.IDMonHoc)), "ID", "SoTinChi");
                return View();
            }else
            {
                return View();
            }
            
        }

        public KetQuaHocTapDTO LayKetQuaHocTap(int id)
        {
            using(KetQuaHocTapBusiness bs = new KetQuaHocTapBusiness())
            {
                return bs.LayKetQuaHocTap(id);
            }
        }

        public bool SuaKetQuaHocTap(KetQuaHocTapDTO ketqua)
        {
            using (KetQuaHocTapBusiness bs = new KetQuaHocTapBusiness())
            {
                return bs.SuaKetQuaHocTap(ketqua);
            }
        }

        //XoaKetQuaHocTap
        public async Task<ActionResult> Delete(int id)
        {
            var output = XoaKetQuaHocTap(id);
            if (output)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Fail");
            }

        }

        public bool XoaKetQuaHocTap(int id)
        {
            using (KetQuaHocTapBusiness bs = new KetQuaHocTapBusiness())
            {
                return bs.XoaKetQuaHocTap(id);
            }
        }

        //XemChiTietKetQuaHocTap
        public ActionResult Details(int id)
        {
            var ketqua = LayKetQuaHocTap(id);
            ViewBag.taikhoan = LayDanhSachTaiKhoan();
            ViewBag.monhoc = LayDanhSachMonHoc();           
            return View(ketqua);
        }
    }
}