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
    public class SinhVienLopHocController : Controller
    {
        //Get : LayDanhSachSinhVien
        public ActionResult Index()
        {
            var lstsvlop = this.LayDanhSachSinhVienLopHocTheoKhoaDT(0);
            ViewBag.masinhvien = LayDanhSachTaiKhoan();
            ViewBag.tensinhvien = LayDanhSachTaiKhoan();
            ViewBag.phanloaitaikhoan = LayDanhSachTaiKhoan();
            ViewBag.phanloailophoc = LayDanhSachLopHoc();

            var listkhoaDT = LayDanhSachKhoaDaoTao();
            listkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Tất Cả Khóa Đào Tạo"
            });
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao",0);
            return View(lstsvlop);
        }
        //Post : LayDanhSachSinhVien
        [HttpPost]
        public ActionResult Index(int id)
        {
            var lstsvlop = this.LayDanhSachSinhVienLopHocTheoKhoaDT(id);
            ViewBag.masinhvien = LayDanhSachTaiKhoan();
            ViewBag.tensinhvien = LayDanhSachTaiKhoan();
            ViewBag.phanloaitaikhoan = LayDanhSachTaiKhoan();
            ViewBag.phanloailophoc = LayDanhSachLopHoc();

            var listkhoaDT = LayDanhSachKhoaDaoTao();
            listkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Tất Cả Khóa Đào Tạo"
            });
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao",0);
            return View(lstsvlop);
        }
            public List<KhoaDaoTaoDTO> LayDanhSachKhoaDaoTao()
        {
            using(KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayDanhSachKhoaDaoTao();
            }
        }
        public List<SinhVienLopHocDTO> LayDanhSachSinhVienLopHocTheoKhoaDT(int id)
        {
            using(SinhVienLopHocBusiness bs = new SinhVienLopHocBusiness())
            {
                return bs.LayDanhSachSinhVienLopHocTheoKhoaDT(id);
            }
        }
        public List<SinhVienLopHocDTO> LayDanhSachSinhVienLopHoc()
        {
            using (SinhVienLopHocBusiness bs = new SinhVienLopHocBusiness())
            {
                return bs.LayDanhSachSinhVienLopHoc();
            };
        }

        public List<AccountDTO> LayDanhSachTaiKhoan_SinhVienDeChon(int id)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayDanhSachTaiKhoan_SinhVienDeChon(id);
            }
        }

        public ActionResult Create_ThayDoiTaiKhoan(int id)
        {
            HttpCookie idAccount = HttpContext.Request.Cookies.Get("idAccount");
            HttpCookie idLopHoc = HttpContext.Request.Cookies.Get("idLopHoc");

            ViewData["masinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVien(), "ID", "Ma", idAccount.Value);
            ViewData["tensinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVienDeChon(id), "ID", "HoVaTen");
            ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_SinhVienDeChon(id), "ID", "MailVL");
            ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop", idLopHoc.Value);            
            return View("Create");
        }

        //Get : TaoSinhVienVaoLop
        public ActionResult Create()
        {
            var lstAccount = LayDanhSachTaiKhoan_SinhVien();

            ViewData["masinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVien(), "ID", "Ma");
            ViewData["tensinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVienDeChon(lstAccount[0].ID), "ID", "HoVaTen");
            ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_SinhVienDeChon(lstAccount[0].ID), "ID", "MailVL");
            ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop");
            
            return View();
        }

        //Post :TaoSinhVienVaoLop
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SinhVienLopHocDTO svlop)
        {
            var findsv = LaySinhVienLopHocDaTonTai(svlop.IDAccount);
            Response.Cookies.Remove("idAccount");
            Response.Cookies.Remove("idLopHoc");
            if(findsv > 0)
            {
                ViewBag.Error = "Sinh Viên đã được thêm vào lớp khác";
                ViewData["masinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVien(), "ID", "Ma");
                ViewData["tensinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVienDeChon(Convert.ToInt32(svlop.IDAccount)), "ID", "HoVaTen");
                ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_SinhVienDeChon(Convert.ToInt32(svlop.IDAccount)), "ID", "MailVL");
                ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop");
                return View();
            }
            else
            {
                ThemSinhVienLopHoc(svlop);
                TempData["Success"] = "Thành công";
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
                        List<SinhVienLopHocDTO> sinhvienlophocList = new List<SinhVienLopHocDTO>();

                        List<int> lstRowError_TaiKhoan_TonTai = new List<int>();
                        List<int> lstRowError_TaiKhoan_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_LopHoc_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_MaTaiKhoan_BiTrungTrenFileExcel = new List<int>();

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            try
                            {
                                var lstsinhvien = new SinhVienLopHocDTO();
                                var masinhvien = Convert.ToString(workSheet.Cells[rowIterator, 1].Value).Trim();
                                var lophoc = Convert.ToString(workSheet.Cells[rowIterator, 2].Value).Trim();
                                var ghichu = Convert.ToString(workSheet.Cells[rowIterator, 3].Value).Trim();

                                var IDAccount = LayIDAccountTheoMa(masinhvien);
                                var IDLopHoc = LayIDLopHocTheoTen(lophoc);
                                var check_taikhoantontai = Check_TaiKhoanTonTai(IDAccount);
                                if(IDAccount == 0) 
                                {
                                    lstRowError_TaiKhoan_KhongCoKiTu.Add(rowIterator);
                                }
                                if(IDLopHoc == 0)
                                {
                                    lstRowError_LopHoc_KhongCoKiTu.Add(rowIterator);
                                }
                                if(check_taikhoantontai > 0)
                                {
                                    lstRowError_TaiKhoan_TonTai.Add(rowIterator);
                                }
                                else
                                {
                                    var checkloimataikhoantrungtrenExcel = sinhvienlophocList.Where(s => s.IDAccount == IDAccount).FirstOrDefault();
                                    if (checkloimataikhoantrungtrenExcel != null)
                                    {
                                        lstRowError_MaTaiKhoan_BiTrungTrenFileExcel.Add(rowIterator);
                                    }
                                    else
                                    {
                                        lstsinhvien.IDAccount = IDAccount;
                                        lstsinhvien.IDLopHoc = IDLopHoc;
                                        lstsinhvien.GhiChu = ghichu;
                                        sinhvienlophocList.Add(lstsinhvien);
                                    }
                                        
                                }
                            }catch(Exception ex)
                            {
                            }                          
                        }
                        if(lstRowError_TaiKhoan_KhongCoKiTu.Count() > 0 || lstRowError_LopHoc_KhongCoKiTu.Count() > 0 || lstRowError_TaiKhoan_TonTai.Count() > 0 || lstRowError_MaTaiKhoan_BiTrungTrenFileExcel.Count() > 0)
                        {
                            if (lstRowError_TaiKhoan_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Mã Tài Khoản bị trống hoặc quá 50 kí tự hoặc mã không phải là của Sinh Viên trong File Excel : ";
                                foreach (var item in lstRowError_TaiKhoan_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
                            }
                            if (lstRowError_LopHoc_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Tên Lớp Học bị trống hoặc quá 50 kí tự trong File Excel : ";
                                foreach (var item in lstRowError_LopHoc_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
                            }
                            if (lstRowError_TaiKhoan_TonTai.Count() > 0)
                            {
                                var error = "Sinh Viên đã được thêm vào lớp khác : ";
                                foreach (var item in lstRowError_TaiKhoan_TonTai)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.trungdata += error + "</br>";
                            }
                            if (lstRowError_MaTaiKhoan_BiTrungTrenFileExcel.Count() > 0)
                            {
                                var error = "Tên Tài Khoản bị trùng trên File Excel : ";
                                foreach (var item in lstRowError_MaTaiKhoan_BiTrungTrenFileExcel)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorTenTaiKhoanBiTrungTrenExcel += error + "</br>";
                            }

                            var lstsvlop = this.LayDanhSachSinhVienLopHoc();
                            ViewBag.tensinhvien = LayDanhSachTaiKhoan();
                            ViewBag.phanloaitaikhoan = LayDanhSachTaiKhoan();
                            ViewBag.phanloailophoc = LayDanhSachLopHoc();
                            var listkhoaDTs = LayDanhSachKhoaDaoTao();
                            listkhoaDTs.Insert(0, new KhoaDaoTaoDTO
                            {
                                ID = 0,
                                TenKhoaDaoTao = "Tất Cả Khóa Đào Tạo"
                            });
                            ViewData["khoaDT"] = new SelectList(listkhoaDTs, "ID", "TenKhoaDaoTao");
                            return View("Index", lstsvlop);
                        }
                        else
                        {
                            foreach (var item in sinhvienlophocList)
                            {
                                ThemSinhVienLopHoc(item);
                            }
                        }                      
                    }
                }
                else
                {
                    ViewBag.Error = "Chưa có File";
                    var lstsvlop = this.LayDanhSachSinhVienLopHoc();
                    ViewBag.tensinhvien = LayDanhSachTaiKhoan();
                    ViewBag.phanloaitaikhoan = LayDanhSachTaiKhoan();
                    ViewBag.phanloailophoc = LayDanhSachLopHoc();
                    var listkhoaDTs = LayDanhSachKhoaDaoTao();
                    listkhoaDTs.Insert(0, new KhoaDaoTaoDTO
                    {
                        ID = 0,
                        TenKhoaDaoTao = "Tất Cả Khóa Đào Tạo"
                    });
                    ViewData["khoaDT"] = new SelectList(listkhoaDTs, "ID", "TenKhoaDaoTao");
                    return View("Index", lstsvlop);
                }
            }
            ViewBag.Success = "Thành Công";
            var lstsinhvienvlop = this.LayDanhSachSinhVienLopHoc();
            ViewBag.tensinhvien = LayDanhSachTaiKhoan();
            ViewBag.phanloaitaikhoan = LayDanhSachTaiKhoan();
            ViewBag.phanloailophoc = LayDanhSachLopHoc();
            var listkhoaDT = LayDanhSachKhoaDaoTao();
            listkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Tất Cả Khóa Đào Tạo"
            });
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao");

            return View("Index", lstsinhvienvlop);
        }

        public int LaySinhVienLopHocDaTonTai(int? id)
        {
            using(SinhVienLopHocBusiness bs = new SinhVienLopHocBusiness())
            {
                return bs.LaySinhVienLopHocDaTonTai(id);
            }
        }

        public List<AccountDTO> LayDanhSachTaiKhoan_SinhVien()
        {
            using(TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayDanhSachTaiKhoan_SinhVien();
            }
        }

        public List<AccountDTO> LayDanhSachTaiKhoan()
        {
            using(TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayDanhSachTaiKhoan();
            }
        }

        public List<LopHocDTO> LayDanhSachLopHoc()
        {
            using(LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.LayDanhSachLopHoc();
            }
        }

        public bool LayMaKhongCoKiTu(SinhVienLopHocDTO svlop)
        {
            if (String.IsNullOrEmpty(svlop.Ma))
            {
                return false;
            }
            return true;
        }

        public bool ThemSinhVienLopHoc(SinhVienLopHocDTO svlop)
        {
            using(SinhVienLopHocBusiness bs = new SinhVienLopHocBusiness())
            {
                return bs.ThemSinhVienLopHoc(svlop);
            }
        }

        public ActionResult Edit_ThayDoiTaiKhoan(int id)
        {

            ViewData["masinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVien(), "ID", "Ma",id);
            ViewData["tensinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVienDeChon(id), "ID", "HoVaTen");
            ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_SinhVienDeChon(id), "ID", "MailVL");
            ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop");
            return View("Edit");

        }

        //Get: SuaSinhVienVaoLop
        public ActionResult Edit(int id)
        {
            HttpCookie idEdit = new HttpCookie("idEdit");
            idEdit.Value = id.ToString();
            idEdit.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(idEdit);

            SinhVienLopHocDTO svlop = LaySinhVienLopHoc(id);
            ViewData["masinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVien(), "ID", "Ma");
            ViewData["tensinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVienDeChon(Convert.ToInt32(svlop.IDAccount)), "ID", "HoVaTen");
            ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_SinhVienDeChon(Convert.ToInt32(svlop.IDAccount)), "ID", "MailVL");
            ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop");
            
            return View(svlop);
        }

        //Post : SuaChuNhiem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SinhVienLopHocDTO svlop)
        {
            var findsv = LaySinhVienLopHocDaTonTai(svlop.IDAccount);
            Response.Cookies.Remove("idAccount");
            Response.Cookies.Remove("idLopHoc");
            if (findsv == svlop.ID || findsv == 0)
            {
                SuaSinhVienLopHoc(svlop);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Sinh Viên đã được thêm vào lớp khác";
                ViewData["masinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVien(), "ID", "Ma");
                ViewData["tensinhvien"] = new SelectList(LayDanhSachTaiKhoan_SinhVienDeChon(Convert.ToInt32(svlop.IDAccount)), "ID", "HoVaTen");
                ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_SinhVienDeChon(Convert.ToInt32(svlop.IDAccount)), "ID", "MailVL");
                ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop");
                return View();
            }
        }

        public SinhVienLopHocDTO LaySinhVienLopHoc(int id)
        {
            using(SinhVienLopHocBusiness bs = new SinhVienLopHocBusiness())
            {
                return bs.LaySinhVienLopHoc(id);
            }
        }

        public bool SuaSinhVienLopHoc(SinhVienLopHocDTO svlop)
        {
            using (SinhVienLopHocBusiness bs = new SinhVienLopHocBusiness())
            {
                return bs.SuaSinhVienLopHoc(svlop);
            }
        }

        //XoaSinhVien
        public async Task<ActionResult> Delete(int id)
        {
            var output = XoaSinhVienLopHoc(id);
            if (output)
            {
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
            else
            {
                return View("fail");
            }
        }

        public bool XoaSinhVienLopHoc(int id)
        {
            using(SinhVienLopHocBusiness bs = new SinhVienLopHocBusiness())
            {
                return bs.XoaSinhVienLopHoc(id);
            }
        }

        //Xemchitiet
        public ActionResult Details(int id)
        {
            var svlop = LaySinhVienLopHoc(id);
            ViewBag.masinhvien = LayDanhSachTaiKhoan();
            ViewBag.tensinhvien = LayDanhSachTaiKhoan();
            ViewBag.taikhoan = LayDanhSachTaiKhoan();
            ViewBag.lophoc = LayDanhSachLopHoc();
            return View(svlop);
        }

        public int LayIDAccountTheoMa(string masinhvien)
        {
            using(SinhVienLopHocBusiness bs = new SinhVienLopHocBusiness())
            {
                return bs.LayIDAccountTheoMa(masinhvien);
            }
        }

        public int LayIDLopHocTheoTen(string lophoc)
        {
            using (SinhVienLopHocBusiness bs = new SinhVienLopHocBusiness())
            {
                return bs.LayIDLopHocTheoTen(lophoc);
            }
        }

        public int Check_TaiKhoanTonTai(int idAccount)
        {
            using (SinhVienLopHocBusiness bs = new SinhVienLopHocBusiness())
            {
                return bs.Check_TaiKhoanTonTai(idAccount);
            }
        }
    }
}