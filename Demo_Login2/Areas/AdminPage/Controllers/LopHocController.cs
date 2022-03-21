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
    public class LopHocController : Controller
    {
        //Get : LayDanhSachLopHoc
        public ActionResult Index()
        {
            var lstlophoc = this.LayDanhSachLopHocTheoKhoaDaoTao(0);
            ViewBag.khoadaotao = LayDanhSachKhoaDaoTao();

            var listkhoaDT = LayDanhSachKhoaDaoTao();
            listkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Tất cả Khóa Đào Tạo"
            });
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao",0);
            return View(lstlophoc);
        }
        [HttpPost]
        //Post : LayDanhSachLopHoc
        public ActionResult Index(int id)
        {
            var lstlophoc = this.LayDanhSachLopHocTheoKhoaDaoTao(id);
            ViewBag.khoadaotao = LayDanhSachKhoaDaoTao();

            var listkhoaDT = LayDanhSachKhoaDaoTao();
            listkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Tất cả Khóa Đào Tạo"
            });
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao",0);
            return View(lstlophoc);
        }
        public List<LopHocDTO> LayDanhSachLopHocTheoKhoaDaoTao(int id)
        {
            using (LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.LayDanhSachLopHocTheoKhoaDaoTao(id);
            }
        }

        //Get : TaoLopHoc
        public ActionResult Create()
        {
            ViewData["khoa"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
            return View();
        }

        //Post : TaoLopHoc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LopHocDTO lophoc)
        {
            var id = LayLopHocDaTonTai(lophoc.TenLop);
            var resulttenlop = LayYeuCauNhapTenLop(lophoc);
            if(resulttenlop == false)
            {
                ViewBag.ErrorTenLop = " Yêu cầu nhập đầy đủ các trường bắt buộc";
                ViewData["khoa"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
                return View();
            }else if (id > 0)
            {
                ViewBag.Message = " Lớp Học đã tồn tại";
                ViewData["khoa"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
                return View();
            }
            else
            {
                ThemLopHoc(lophoc);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
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
                        List<LopHocDTO> lophocList = new List<LopHocDTO>();

                        List<int> lstRowError_TenLop_Trung = new List<int>();
                        List<int> lstRowError_KhoaDaoTao_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_TenLop_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_TenLop_VuotQuaKiTu = new List<int>();
                        List<int> lstRowError_TenLop_BiTrungTrenFileExcel = new List<int>();

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            try
                            {
                                var lophoc = new LopHocDTO();
                                var khoadaotao = Convert.ToString(workSheet.Cells[rowIterator, 1].Value).Trim();
                                var tenlop = Convert.ToString(workSheet.Cells[rowIterator, 2].Value).Trim();
                                var ghichu = Convert.ToString(workSheet.Cells[rowIterator, 3].Value).Trim();

                                var IDKhoaDaoTao = LayIDKhoaDaoTaoTheoTen(khoadaotao);
                                var check_tenloptontai = LayLopHocDaTonTai(tenlop);
                                if(IDKhoaDaoTao == 0)
                                {
                                    lstRowError_KhoaDaoTao_KhongCoKiTu.Add(rowIterator);
                                }
                                if(tenlop.Length == 0)
                                {
                                    lstRowError_TenLop_KhongCoKiTu.Add(rowIterator);
                                }
                                if(tenlop.Length > 50)
                                {
                                    lstRowError_TenLop_VuotQuaKiTu.Add(rowIterator);
                                }
                                if(check_tenloptontai > 0)
                                {
                                    lstRowError_TenLop_Trung.Add(rowIterator);
                                }
                                else
                                {
                                    var checkloitenlophoctrungtrenExcel = lophocList.Where(s => s.TenLop == tenlop).FirstOrDefault();
                                    if (checkloitenlophoctrungtrenExcel != null)
                                    {
                                        lstRowError_TenLop_BiTrungTrenFileExcel.Add(rowIterator);
                                    }
                                    else
                                    {
                                        lophoc.IDKhoaDaoTao = IDKhoaDaoTao;
                                        lophoc.TenLop = tenlop;
                                        lophoc.GhiChu = ghichu;
                                        lophocList.Add(lophoc);
                                    }
                                    
                                }
                            }catch(Exception ex) 
                            {}                                                      
                        }
                        if(lstRowError_TenLop_Trung.Count() > 0 || lstRowError_KhoaDaoTao_KhongCoKiTu.Count() > 0 || lstRowError_TenLop_KhongCoKiTu.Count() > 0 || lstRowError_TenLop_VuotQuaKiTu.Count() > 0
                            || lstRowError_TenLop_BiTrungTrenFileExcel.Count() > 0)
                        {
                            
                            if (lstRowError_KhoaDaoTao_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Khóa Đào Tạo bị trống hoặc quá 50 kí tự trong File Excel : ";
                                foreach (var item in lstRowError_KhoaDaoTao_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
                            }
                            if (lstRowError_TenLop_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Các dòng chưa nhập đầy đủ thông tin trong File Excel cột Tên Lớp : ";
                                foreach (var item in lstRowError_TenLop_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
                            }
                            if (lstRowError_TenLop_VuotQuaKiTu.Count() > 0)
                            {
                                var error = "Các dòng ở cột Tên Lớp vượt quá 50 kí tự trong File Excel : ";
                                foreach (var item in lstRowError_TenLop_VuotQuaKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
                            }
                            if (lstRowError_TenLop_Trung.Count() > 0)
                            {
                                var error = "Các dòng bị trùng ở cột Tên Lớp trong File Excel : ";
                                foreach (var item in lstRowError_TenLop_Trung)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.trungdata += error + "</br>";
                            }
                            if (lstRowError_TenLop_BiTrungTrenFileExcel.Count() > 0)
                            {
                                var error = "Tên Lớp Học bị trùng trên File Excel  : ";
                                foreach (var item in lstRowError_TenLop_BiTrungTrenFileExcel)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorTenLopHocBiTrungTrenExcel += error + "</br>";
                            }
                            var lstlophoc = this.LayDanhSachLopHoc();
                            ViewBag.khoadaotao = LayDanhSachKhoaDaoTao();
                            var listkhoaDTs = LayDanhSachKhoaDaoTao();
                            listkhoaDTs.Insert(0, new KhoaDaoTaoDTO
                            {
                                ID = 0,
                                TenKhoaDaoTao = "Tất cả Khóa Đào Tạo"
                            });
                            ViewData["khoaDT"] = new SelectList(listkhoaDTs, "ID", "TenKhoaDaoTao");
                            return View("Index", lstlophoc);
                        }
                        else
                        {
                            foreach (var item in lophocList)
                            {
                                ThemLopHoc(item);
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.Error = "Chưa có File";
                    var lstlophoc = this.LayDanhSachLopHoc();
                    ViewBag.khoadaotao = LayDanhSachKhoaDaoTao();
                    var listkhoaDTs = LayDanhSachKhoaDaoTao();
                    listkhoaDTs.Insert(0, new KhoaDaoTaoDTO
                    {
                        ID = 0,
                        TenKhoaDaoTao = "Tất cả Khóa Đào Tạo"
                    });
                    ViewData["khoaDT"] = new SelectList(listkhoaDTs, "ID", "TenKhoaDaoTao");
                    return View("Index", lstlophoc);
                }
            }
            ViewBag.Success = "Thành Công";
            var lstlop = this.LayDanhSachLopHoc();
            ViewBag.khoadaotao = LayDanhSachKhoaDaoTao();
            var listkhoaDT = LayDanhSachKhoaDaoTao();
            listkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Tất cả Khóa Đào Tạo"
            });
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao");
            return View("Index", lstlop);
        }



        public int LayLopHocDaTonTai(string tenlop)
        {
            using (LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.LayLopHocDaTonTai(tenlop);
            }
        }

        public List<KhoaDaoTaoDTO> LayDanhSachKhoaDaoTao()
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayDanhSachKhoaDaoTao();
            }
        }

        public List<LopHocDTO> LayDanhSachLopHoc()
        {
            using(LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.LayDanhSachLopHoc();
            }
        }

        public bool LayYeuCauNhapTenLop(LopHocDTO lophoc)
        {
            if(lophoc.TenLop == null)
            {
                return false;
            }
            return true;
        }

        public bool LayTenLopKhongCoKiTu(LopHocDTO lophoc)
        {
            if (String.IsNullOrEmpty(lophoc.TenLop))
            {
                return false;
            }
            return true;
        }

        public bool ThemLopHoc(LopHocDTO lophoc)
        {
            using (LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.ThemLopHoc(lophoc);
            }
        }

        //Get:SuaLopHoc
        public ActionResult Edit(int id)
        {
            ViewData["khoa"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
            LopHocDTO lophoc = LayLopHoc(id);
            return View(lophoc);
        }

        //Post:SuaLopHoc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LopHocDTO lophoc)
        {
            var id = LayLopHocDaTonTai(lophoc.TenLop);
            var resulttenlop = LayYeuCauNhapTenLop(lophoc);
            if (id == lophoc.ID || id == 0 && resulttenlop == true)
            {
                SuaLopHoc(lophoc);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");

            }else if(resulttenlop == false)
            {
                ViewBag.ErrorTenLop = " Yêu cầu nhập đầy đủ các trường bắt buộc";
                ViewData["khoa"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
                return View();
            }
            else
            {
                ViewBag.Message = "Lớp Học đã tồn tại!!";
                ViewData["khoa"] = new SelectList(LayDanhSachKhoaDaoTao(), "ID", "TenKhoaDaoTao");
                return View();
            }
        }

        public LopHocDTO LayLopHoc(int id)
        {
            using (LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.LayLopHoc(id);
            }
        }

        public bool SuaLopHoc(LopHocDTO lophoc)
        {
            using (LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.SuaLopHoc(lophoc);
            }
        }

        //XoaLopHoc
        public async Task<ActionResult> Delete(int id)
        {
            var findlophoc = CheckLoiLopHocDaTonTai(id);
            var findlophoc_sinhvienlophoc = CheckLoiLSinhVienLopHoc(id);
            var findlophoc_sinhviendangkikehoachhoctap = CheckLoiSinhVienDangKiKeHoachHocTap_Moi(id);
            if (findlophoc > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.khoadaotao = LayDanhSachKhoaDaoTao();
                return RedirectToAction("Index");

            }
            else if(findlophoc_sinhvienlophoc > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.khoadaotao = LayDanhSachKhoaDaoTao();
                return RedirectToAction("Index");
            }
            else if(findlophoc_sinhviendangkikehoachhoctap > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.khoadaotao = LayDanhSachKhoaDaoTao();
                return RedirectToAction("Index");
            }
            else
            {
                var output = XoaLopHoc(id);
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

        public bool XoaLopHoc(int id)
        {
            using (LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.XoaLopHoc(id);
            }
        }

        public int CheckLoiLopHocDaTonTai(int? id)
        {
            using(LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.CheckLoiLopHocDaTonTai(id);
            }
        }
        public int CheckLoiSinhVienDangKiKeHoachHocTap_Moi(int? id)
        {
            using (LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.CheckLoiSinhVienDangKiKeHoachHocTap_Moi(id);
            }
        }
        public int CheckLoiLSinhVienLopHoc(int? id)
        {
            using (LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.CheckLoiLSinhVienLopHoc(id);
            }
        }

        //XemChiTietLopHoc
        public ActionResult Details(int id)
        {
            var lophoc = LayLopHoc(id);
            ViewBag.khoadaotao = LayDanhSachKhoaDaoTao();
            return View(lophoc);
        }

        public int LayIDKhoaDaoTaoTheoTen(string tenkhoaDT)
        {
            using(LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.LayIDKhoaDaoTaoTheoTen(tenkhoaDT);
            }
        }
    }
}