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
    public class HocPhanTienQuyetController : Controller
    {
        //LayDanhSachHocPhanTienQuyet
        public ActionResult Index()
        {
            var lsthocphanTQ = LayDanhSachHocPhanTienQuyet();
            ViewBag.monhoc = LayDanhSachMonHoc();
            ViewBag.monhoctienquyet = LayDanhSachMonHoc();
            return View(lsthocphanTQ);
        }
        public List<MonHocDTO> LayDanhSachMonHoc()
        {
            using (MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayDanhSachMonHoc();
            }
        }
 
        public List<HocPhanTienQuyetDTO> LayDanhSachHocPhanTienQuyet()
        {
            using(HocPhanTienQuyetBusiness bs = new HocPhanTienQuyetBusiness())
            {
                return bs.LayDanhSachHocPhanTienQuyet();
            }
        }
        public ActionResult Create_ThayDoiMonHoc(int id)
        {
            HttpCookie idMonHoc = HttpContext.Request.Cookies.Get("idMonHoc");
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc", idMonHoc.Value);
            ViewData["monhoctienquyet"] = new SelectList(LayDanhSachMonHocTienQuyet(id), "ID", "TenMonHoc");
            return View("Create");
        }

        //Get : TaoHocPhanTienQuyet
        public ActionResult Create()
        {
            var lstmonhoc = LayDanhSachMonHoc();
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
            ViewData["monhoctienquyet"] = new SelectList(LayDanhSachMonHocTienQuyet(lstmonhoc[0].ID), "ID", "TenMonHoc");
            return View();
        }

        //Post : TaoHocPhanTienQuyet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HocPhanTienQuyetDTO hocphanTQ)
        {
            var findmonhoc = CheckLoiMonHocTienQuyetTheoMonHocDaTonTai(hocphanTQ.IDMonHoc, hocphanTQ.IDMonHocTienQuyet);
            Response.Cookies.Remove("idMonHoc");
            if (findmonhoc > 0)
            {
                ViewBag.Error = "Môn Học Tiên Quyết đã trong Môn Học này";
                var lstmonhoc = LayDanhSachMonHoc();
                ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
                ViewData["monhoctienquyet"] = new SelectList(LayDanhSachMonHocTienQuyet(Convert.ToInt32(hocphanTQ.IDMonHoc)), "ID", "TenMonHoc");
                return View();
            }
            else
            {
                ThemHocPhanTienQuyet(hocphanTQ);
                return RedirectToAction("Index");
            }
            //var output = ThemHocPhanTienQuyet(hocphanTQ);
            //Response.Cookies.Remove("idMonHoc");
            //if (output)
            //{
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    return View("Fail");
            //}


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
                        List<HocPhanTienQuyetDTO> hocphantienquyetList = new List<HocPhanTienQuyetDTO>();

                        List<int> lstRowError_TenMonHocTienQuyet_Trung = new List<int>();
                        List<int> lstRowError_MonHoc_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_TenMonHocTienQuyet_KhongCoKiTu = new List<int>();

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var lsthocphantienquyet = new HocPhanTienQuyetDTO();
                            lsthocphantienquyet.IDMonHoc = Convert.ToInt32(workSheet.Cells[rowIterator, 1].Value);
                            lsthocphantienquyet.IDMonHocTienQuyet = Convert.ToInt32(workSheet.Cells[rowIterator, 2].Value);
                            lsthocphantienquyet.GhiChu = Convert.ToString(workSheet.Cells[rowIterator, 3].Value).Trim();

                            var findmonhoc = CheckLoiMonHocTienQuyetTheoMonHocDaTonTai(lsthocphantienquyet.IDMonHoc, lsthocphantienquyet.IDMonHocTienQuyet);
                            if(lsthocphantienquyet.IDMonHoc < 1)
                            {
                                lstRowError_MonHoc_KhongCoKiTu.Add(rowIterator);

                            }
                            if(lsthocphantienquyet.IDMonHocTienQuyet < 1)
                            {
                                lstRowError_TenMonHocTienQuyet_KhongCoKiTu.Add(rowIterator);

                            }
                            if (findmonhoc > 0)
                            {
                                lstRowError_TenMonHocTienQuyet_Trung.Add(rowIterator);

                            }
                            else
                            {
                                hocphantienquyetList.Add(lsthocphantienquyet);
                            }
                        }

                        if(lstRowError_MonHoc_KhongCoKiTu.Count() > 0 || lstRowError_TenMonHocTienQuyet_KhongCoKiTu.Count() > 0 || lstRowError_TenMonHocTienQuyet_Trung.Count() > 0)
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
                            if (lstRowError_TenMonHocTienQuyet_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Các dòng chưa nhập đầy đủ thông tin trong File Excel cột Tên Môn Học Tiên Quyết : ";
                                foreach (var item in lstRowError_TenMonHocTienQuyet_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
                            }
                            if (lstRowError_TenMonHocTienQuyet_Trung.Count() > 0)
                            {
                                var error = "Các dòng bị trùng ở cột Tên Môn Học Tiên Quyết trong File Excel : ";
                                foreach (var item in lstRowError_TenMonHocTienQuyet_Trung)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.trungdata += error + "</br>";
                            }
                            var lsthocphanTQs = LayDanhSachHocPhanTienQuyet();
                            ViewBag.monhoc = LayDanhSachMonHoc();
                            ViewBag.monhoctienquyet = LayDanhSachMonHoc();
                            return View("Index", lsthocphanTQs);
                        }
                        else
                        {
                            foreach (var item in hocphantienquyetList)
                            {
                                ThemHocPhanTienQuyet(item);
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.Error = "Chưa có File";
                    var lsthocphanTQs = LayDanhSachHocPhanTienQuyet();
                    ViewBag.monhoc = LayDanhSachMonHoc();
                    ViewBag.monhoctienquyet = LayDanhSachMonHoc();
                    return View("Index", lsthocphanTQs);
                }
            }
            ViewBag.Success = "Thành Công";
            var lsthocphanTQ = LayDanhSachHocPhanTienQuyet();
            ViewBag.monhoc = LayDanhSachMonHoc();
            ViewBag.monhoctienquyet = LayDanhSachMonHoc();
            return View("Index", lsthocphanTQ);
        }

        public int CheckLoiMonHocTienQuyetTheoMonHocDaTonTai(int? idMonHoc,int? idMonHocTienQuyet)
        {
            using(MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.CheckLoiMonHocTienQuyetTheoMonHocDaTonTai(idMonHoc, idMonHocTienQuyet);
            }
        }

        public List<MonHocDTO> LayDanhSachMonHocTienQuyet(int id)
        {
            using(MonHocBusiness bs = new MonHocBusiness())
            {
                return bs.LayDanhSachMonHocTienQuyet(id);
            }
        }

        public int LayHocPhanTienQuyetDaTonTai(int? id)
        {
            using (HocPhanTienQuyetBusiness bs = new HocPhanTienQuyetBusiness())
            {
                return bs.LayHocPhanTienQuyetDaTonTai(id);
            }
        }


        public bool ThemHocPhanTienQuyet(HocPhanTienQuyetDTO hocphanTQ)
        {
            using (HocPhanTienQuyetBusiness bs = new HocPhanTienQuyetBusiness())
            {
                return bs.ThemHocPhanTienQuyet(hocphanTQ);
            }
        }

        public ActionResult Edit_ThayDoiMonHoc(int id)
        {
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc", id);
            ViewData["monhoctienquyet"] = new SelectList(LayDanhSachMonHocTienQuyet(id), "ID", "TenMonHoc");
            return View("Edit");
        }

        //Get : SuaHocPhanTienQuyet
        public ActionResult Edit(int id)
        {
            HttpCookie idEdit = new HttpCookie("idEdit");
            idEdit.Value = id.ToString();
            idEdit.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(idEdit);

            HocPhanTienQuyetDTO hocphanTQ = LayHocPhanTienQuyet(id);
            ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
            ViewData["monhoctienquyet"] = new SelectList(LayDanhSachMonHocTienQuyet(Convert.ToInt32(hocphanTQ.IDMonHoc)), "ID", "TenMonHoc");
            return View(hocphanTQ);
        }

        //Post : SuaHocPhanTienQuyet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HocPhanTienQuyetDTO hocphanTQ)
        {
            //var output = SuaHocPhanTienQuyet(hocphanTQ);
            //if (output)
            //{
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    return View("Fail");
            //}
            var findmonhoc = CheckLoiMonHocTienQuyetTheoMonHocDaTonTai(hocphanTQ.IDMonHoc, hocphanTQ.IDMonHocTienQuyet);
            if (findmonhoc == hocphanTQ.ID || findmonhoc == 0)
            {
                SuaHocPhanTienQuyet(hocphanTQ);
                return RedirectToAction("Index");
            }
            else if(findmonhoc != hocphanTQ.ID && findmonhoc > 0)
            {
                ViewBag.Error = "Môn Học Tiên Quyết đã trong Môn Học này";
                var lstmonhoc = LayDanhSachMonHoc();
                ViewData["monhoc"] = new SelectList(LayDanhSachMonHoc(), "ID", "TenMonHoc");
                ViewData["monhoctienquyet"] = new SelectList(LayDanhSachMonHocTienQuyet(Convert.ToInt32(hocphanTQ.IDMonHoc)), "ID", "TenMonHoc");
                return View();
            }else
            {
                return View();
            }
        }

        public HocPhanTienQuyetDTO LayHocPhanTienQuyet(int id)
        {
            using(HocPhanTienQuyetBusiness bs = new HocPhanTienQuyetBusiness())
            {
                return bs.LayHocPhanTienQuyet(id);
            }
        }

        public bool SuaHocPhanTienQuyet(HocPhanTienQuyetDTO hocphanTQ)
        {
            using(HocPhanTienQuyetBusiness bs = new HocPhanTienQuyetBusiness())
            {
                return bs.SuaHocPhanTienQuyet(hocphanTQ);
            }
        }

        //XoaHocPhanTienQuyet
        public async Task<ActionResult> Delete(int id)
        {
            var findhocphan = CheckLoiHocPhanTienQuyetDaTonTai(id);
            if (findhocphan > 0)
            {
                TempData["error"] = "lỗi";
                return RedirectToAction("Index");
            }
            else
            {
                var output = XoaHocPhanTienQuyet(id);
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

        public bool XoaHocPhanTienQuyet(int id)
        {
            using (HocPhanTienQuyetBusiness bs = new HocPhanTienQuyetBusiness())
            {
                return bs.XoaHocPhanTienQuyet(id);
            }
        }

        public int CheckLoiHocPhanTienQuyetDaTonTai(int? id)
        {
            using (HocPhanTienQuyetBusiness bs = new HocPhanTienQuyetBusiness())
            {
                return bs.CheckLoiHocPhanTienQuyetDaTonTai(id);
            }
        }

        //XemChiTietHocPhanTienQuyet
        public ActionResult Details(int id)
        {
            var hocphanTQ = LayHocPhanTienQuyet(id);
            ViewBag.monhoc = LayDanhSachMonHoc();
            ViewBag.monhoctienquyet = LayDanhSachMonHoc();
            return View(hocphanTQ);
        }
    }
}