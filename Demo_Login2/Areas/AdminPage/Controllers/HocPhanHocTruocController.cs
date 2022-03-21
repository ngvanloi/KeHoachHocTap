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
    public class HocPhanHocTruocController : Controller
    {
        //LayDanhSachHocPhanHocTruoc
        public ActionResult Index()
        {
            var lsthocphanHT = LayDanhSachHocPhanHocTruoc();
            ViewBag.monhoc = LayDanhSachMonHoc();
            return View(lsthocphanHT);
        }
        public List<MonHocDTO> LayDanhSachMonHoc()
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayDanhSachMonHoc();
            }
        }

        public List<HocPhanHocTruocDTO> LayDanhSachHocPhanHocTruoc()
        {
            using (HocPhanHocTruocBusiness bs = new HocPhanHocTruocBusiness())
            {
                return bs.LayDanhSachHocPhanHocTruoc();
            }
        }

        public ActionResult Create_ThayDoiMonHoc(int id)
        {
            HttpCookie idMonHoc = HttpContext.Request.Cookies.Get("idMonHoc");
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc", idMonHoc.Value);
            ViewData["monhochoctruoc"] = new SelectList(LayDanhSachMonHocHocTruoc(id), "ID", "TenMonHoc");
            return View("Create");
        }

        //Get : TaoHocPhanHocTruoc
        public ActionResult Create()
        {
            var lstmonhoc = LayDanhSachMonHoc();
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
            ViewData["monhochoctruoc"] = new SelectList(LayDanhSachMonHocHocTruoc(lstmonhoc[0].ID), "ID", "TenMonHoc");
            return View();
        }

        //Post : TaoHocPhanHocTruoc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HocPhanHocTruocDTO hocphanHT)
        {
            var findmonhoc = CheckLoiMonHocHocTruocTheoMonHocDaTonTai(hocphanHT.IDMonHoc, hocphanHT.IDMonHocHocTruoc);
            if(findmonhoc > 0)
            {
                ViewBag.Error = "Môn Học Học Trước đã trong Môn Học này";
                var lstmonhoc = LayDanhSachMonHoc();
                ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
                ViewData["monhochoctruoc"] = new SelectList(LayDanhSachMonHocHocTruoc(Convert.ToInt32(hocphanHT.IDMonHoc)), "ID", "TenMonHoc");
                return View();

            }else
            {
                ThemHocPhanHocTruoc(hocphanHT);
                return RedirectToAction("Index");
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
                        List<HocPhanHocTruocDTO> hocphanhoctruocList = new List<HocPhanHocTruocDTO>();

                        List<int> lstRowError_TenMonHocHocTruoc_Trung = new List<int>();
                        List<int> lstRowError_MonHoc_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_TenMonHocHocTruoc_KhongCoKiTu = new List<int>();

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var lsthocphanhoctruoc = new HocPhanHocTruocDTO();
                            lsthocphanhoctruoc.IDMonHoc = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            lsthocphanhoctruoc.IDMonHocHocTruoc = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                            lsthocphanhoctruoc.GhiChu = Convert.ToString(workSheet.Cells[rowIterator, 3].Value).Trim();

                            var findmonhoc = CheckLoiMonHocHocTruocTheoMonHocDaTonTai(lsthocphanhoctruoc.IDMonHoc,lsthocphanhoctruoc.IDMonHocHocTruoc);
                            if(lsthocphanhoctruoc.IDMonHoc < 1)
                            {
                                lstRowError_MonHoc_KhongCoKiTu.Add(rowIterator);

                            }
                            if(lsthocphanhoctruoc.IDMonHocHocTruoc < 1)
                            {
                                lstRowError_TenMonHocHocTruoc_KhongCoKiTu.Add(rowIterator);

                            }
                            if (findmonhoc > 0)
                            {
                                lstRowError_TenMonHocHocTruoc_Trung.Add(rowIterator);

                            }
                            else
                            {
                                hocphanhoctruocList.Add(lsthocphanhoctruoc);
                            }
                        }

                        if(lstRowError_MonHoc_KhongCoKiTu.Count() > 0 || lstRowError_TenMonHocHocTruoc_KhongCoKiTu.Count() > 0 || lstRowError_TenMonHocHocTruoc_Trung.Count() > 0)
                        {
                            if (lstRowError_MonHoc_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Các dòng chưa nhập đầy đủ thông tin trong File Excel cột Tên Môn Học : ";
                                foreach (var item in lstRowError_MonHoc_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
                            }
                            if (lstRowError_TenMonHocHocTruoc_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Các dòng chưa nhập đầy đủ thông tin trong File Excel cột Tên Môn Học Học Trước : ";
                                foreach (var item in lstRowError_TenMonHocHocTruoc_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
                            }
                            if (lstRowError_TenMonHocHocTruoc_Trung.Count() > 0)
                            {
                                var error = "Các dòng bị trùng ở cột Tên Môn Học Học Trước trong File Excel : ";
                                foreach (var item in lstRowError_TenMonHocHocTruoc_Trung)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.trungdata += error + "</br>";
                            }
                            var lsthocphanHT = LayDanhSachHocPhanHocTruoc();
                            ViewBag.monhoc = LayDanhSachMonHoc();
                            return View("Index", lsthocphanHT);
                        }
                        else
                        {
                            foreach (var item in hocphanhoctruocList)
                            {
                                ThemHocPhanHocTruoc(item);
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.Error = "Chưa có File";
                    var lsthocphanHT = LayDanhSachHocPhanHocTruoc();
                    ViewBag.monhoc = LayDanhSachMonHoc();
                    return View("Index", lsthocphanHT);
                }
            }
            ViewBag.Success = "Thành Công";
            var danhsachhocphanHT = LayDanhSachHocPhanHocTruoc();
            ViewBag.monhoc = LayDanhSachMonHoc();
            return View("Index", danhsachhocphanHT);
        }

        public int CheckLoiMonHocHocTruocTheoMonHocDaTonTai(int? idMonHoc,int? idMonHocHocTruoc)
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.CheckLoiMonHocHocTruocTheoMonHocDaTonTai(idMonHoc, idMonHocHocTruoc);
            }
        }

        public List<MonHocDTO> LayDanhSachMonHocHocTruoc(int id)
        {
            using(MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayDanhSachMonHocHocTruoc(id);
            }
        }

        public int LayHocPhanHocTruocDaTonTai(int? id)
        {
            using (HocPhanHocTruocBusiness bs = new HocPhanHocTruocBusiness())
            {
                return bs.LayHocPhanHocTruocDaTonTai(id);
            }
        }
        public bool ThemHocPhanHocTruoc(HocPhanHocTruocDTO hocphanHT)
        {
            using(HocPhanHocTruocBusiness bs = new HocPhanHocTruocBusiness())
            {
                return bs.ThemHocPhanHocTruoc(hocphanHT);
            }
        }

        public ActionResult Edit_ThayDoiMonHoc(int id)
        {
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc", id);
            ViewData["monhochoctruoc"] = new SelectList(LayDanhSachMonHocHocTruoc(id), "ID", "TenMonHoc");
            return View("Edit");
        }

        //Get : SuaHocPhanHocTruoc
        public ActionResult Edit(int id)
        {
            HttpCookie idEdit = new HttpCookie("idEdit");
            idEdit.Value = id.ToString();
            idEdit.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(idEdit);

            HocPhanHocTruocDTO hocphanHT = LayHocPhanHocTruoc(id);
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
            ViewData["monhochoctruoc"] = new SelectList(LayDanhSachMonHocHocTruoc(Convert.ToInt32(hocphanHT.IDMonHoc)), "ID", "TenMonHoc");
            return View(hocphanHT);
        }

        //Post : SuaHocPhanHocTruoc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HocPhanHocTruocDTO hocphanHT)
        {
            var findmonhoc = CheckLoiMonHocHocTruocTheoMonHocDaTonTai(hocphanHT.IDMonHoc, hocphanHT.IDMonHocHocTruoc);
            if (findmonhoc == hocphanHT.ID || findmonhoc == 0)
            {
                SuaHocPhanHocTruoc(hocphanHT);
                return RedirectToAction("Index");
            }
            else if (findmonhoc != hocphanHT.ID && findmonhoc > 0)
            {
                ViewBag.Error = "Môn Học Học Trước đã trong Môn Học này";
                var lstmonhoc = LayDanhSachMonHoc();
                ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
                ViewData["monhochoctruoc"] = new SelectList(LayDanhSachMonHocHocTruoc(Convert.ToInt32(hocphanHT.IDMonHoc)), "ID", "TenMonHoc");
                return View();
            }
            else
            {
                return View();
            }
        }

        public HocPhanHocTruocDTO LayHocPhanHocTruoc(int id)
        {
            using(HocPhanHocTruocBusiness bs = new HocPhanHocTruocBusiness())
            {
                return bs.LayHocPhanHocTruoc(id);
            }
        }

        public bool SuaHocPhanHocTruoc(HocPhanHocTruocDTO hocphanHT)
        {
            using(HocPhanHocTruocBusiness bs = new HocPhanHocTruocBusiness())
            {
                return bs.SuaHocPhanHocTruoc(hocphanHT);
            }
        }

        //XoaHocPhanHocTruoc
        public async Task<ActionResult> Delete(int id)
        {
            var findhocphan = CheckLoiHocPhanHocTruocDaTonTai(id);
            if (findhocphan > 0)
            {
                TempData["error"] = "lỗi";
                return RedirectToAction("Index");
            }
            else
            {
                var output = XoaHocPhanHocTruoc(id);
                if (output)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Fail");
                }
            }

        }

        public int CheckLoiHocPhanHocTruocDaTonTai(int? id)
        {
            using (HocPhanHocTruocBusiness bs = new HocPhanHocTruocBusiness())
            {
                return bs.CheckLoiHocPhanHocTruocDaTonTai(id);
            }
        }

        public bool XoaHocPhanHocTruoc(int id)
        {
            using(HocPhanHocTruocBusiness bs = new HocPhanHocTruocBusiness())
            {
                return bs.XoaHocPhanHocTruoc(id);
            }
        }

        //XemChiTietHocPhanHocTruoc
        public ActionResult Details(int id)
        {
            var hocphanHT = LayHocPhanHocTruoc(id);
            ViewBag.monhoc = LayDanhSachMonHoc();
            return View(hocphanHT);
        }
    }
}