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
    public class AccountLopHocController : Controller
    {
        //Get: LayDanhSachChuNhiem
        public ActionResult Index()
        {
            var lstacclop = this.LayDanhSachAccountLopHocTheoKhoaDT(0);
            ViewBag.magiaovien = LayDanhSachTaiKhoan();
            ViewBag.tengiaovien = LayDanhSachTaiKhoan();
            ViewBag.phanloaitaikhoan = LayDanhSachTaiKhoan();
            ViewBag.phanloailophoc = LayDanhSachLopHoc();

            var listkhoaDT = LayDanhSachKhoaDaoTao();
            listkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Tất cả Khóa Đào Tạo"
            });
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao",0);

            return View(lstacclop);
        }

        //Post :LayDanhSachChuNhiem
        [HttpPost]
        public ActionResult Index(int id)
        {
            var lstacclop = this.LayDanhSachAccountLopHocTheoKhoaDT(id);
            ViewBag.magiaovien = LayDanhSachTaiKhoan();
            ViewBag.tengiaovien = LayDanhSachTaiKhoan();
            ViewBag.phanloaitaikhoan = LayDanhSachTaiKhoan();
            ViewBag.phanloailophoc = LayDanhSachLopHoc();
            var listkhoaDT = this.LayDanhSachKhoaDaoTao();
            listkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Tất cả Khóa Đào Tạo"
            });
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao",0);
            return View(lstacclop);
        }

        public List<KhoaDaoTaoDTO> LayDanhSachKhoaDaoTao()
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayDanhSachKhoaDaoTao();
            }
        }
        public List<AccountLopHocDTO> LayDanhSachAccountLopHocTheoKhoaDT(int id)
        {
            using (AccountLopHocBusiness bs = new AccountLopHocBusiness())
            {
                return bs.LayDanhSachAccountLopHocTheoKhoaDT(id);
            }
        }
        public List<AccountLopHocDTO> LayDanhSachAccountLopHoc()
        {
            using (AccountLopHocBusiness bs = new AccountLopHocBusiness())
            {
                return bs.LayDanhSachAccountLopHoc();
            }
        }

        public List<AccountDTO> LayDanhSachTaiKhoan_GiangVienDeChon(int id)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayDanhSachTaiKhoan_GiangVienDeChon(id);
            }
        }

        public ActionResult Create_ThayDoiTaiKhoan(int id)
        {
            HttpCookie idAccount = HttpContext.Request.Cookies.Get("idAccount");
            HttpCookie idLopHoc = HttpContext.Request.Cookies.Get("idLopHoc");

            ViewData["magiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVien(), "ID", "Ma", idAccount.Value);
            ViewData["tengiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(id), "ID", "HoVaTen");
            ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(id), "ID", "MailVL");
            ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop", idLopHoc.Value);
            return View("Create");

        }

        //Get : TaoChuNhiem
        public ActionResult Create()
        {
            var lstAccount = LayDanhSachTaiKhoan_GiangVien();

            ViewData["magiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVien(), "ID", "Ma");
            ViewData["tengiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(lstAccount[0].ID), "ID", "HoVaTen");
            ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(lstAccount[0].ID), "ID", "MailVL");
            ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop");
            return View();
        }
        //Post :TaoChuNhiem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AccountLopHocDTO acclop)
        {


            var findlop = LayLopHocDaTonTai(acclop.IDLopHoc);
            var findgv = LayGiangVienDaTonTai(acclop.IDAccount);
            Response.Cookies.Remove("idAccount");
            Response.Cookies.Remove("idLopHoc");

            if (findgv > 0)
            {
                ViewBag.Errorgv = "Giảng viên đã là chủ nhiệm lớp khác";
                ViewData["magiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVien(), "ID", "Ma");
                ViewData["tengiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(Convert.ToInt32(acclop.IDAccount)), "ID", "HoVaTen");
                ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(Convert.ToInt32(acclop.IDAccount)), "ID", "MailVL");
                ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop");
                return View();
            }
            else if (findlop > 0)
            {
                ViewBag.Errorlop = "Lớp đã có chủ nhiệm";
                ViewData["magiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVien(), "ID", "Ma");
                ViewData["tengiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(Convert.ToInt32(acclop.IDAccount)), "ID", "HoVaTen");
                ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(Convert.ToInt32(acclop.IDAccount)), "ID", "MailVL");
                ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop");
                return View();
            }
            else
            {
                ThemAccountLopHoc(acclop);
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
                        List<AccountLopHocDTO> accountlophocList = new List<AccountLopHocDTO>();

                        List<int> lstRowCheck_TaiKhoan_TonTai = new List<int>();
                        List<int> lstRowCheck_LopHoc_TonTai = new List<int>();
                        List<int> lstRowCheck_TaiKhoan_Trong = new List<int>();
                        List<int> lstRowCheck_LopHoc_Trong = new List<int>();
                        List<int> lstRowError_MaTaiKhoan_BiTrungTrenFileExcel = new List<int>();
                        //List<int> lstRowError_Mail_DinhDangHopLe = new List<int>();

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            try
                            {
                                var lstchunhiem = new AccountLopHocDTO();
                                var magiangvien = Convert.ToString(workSheet.Cells[rowIterator, 1].Value).Trim();
                                var lophoc = Convert.ToString(workSheet.Cells[rowIterator, 2].Value).Trim();
                                var ghichu = Convert.ToString(workSheet.Cells[rowIterator, 3].Value).Trim();

                                var IDAccount = LayIDAccountTheoMa(magiangvien);
                                var IDLopHoc = LayIDLopHocTheoTen(lophoc);
                                var check_taikhoantontai = Check_TaiKhoanTonTai(IDAccount);
                                var check_tenloptontai = Check_LopHocTonTai(IDLopHoc);

                               
                                //var checkMail = LayMailHopLe(acc.MailVL);

                                if (IDAccount == 0)
                                {
                                    lstRowCheck_TaiKhoan_Trong.Add(rowIterator);
                                }                               
                                if(IDLopHoc == 0)
                                {
                                    lstRowCheck_LopHoc_Trong.Add(rowIterator);
                                }
                                if (check_taikhoantontai > 0)
                                {
                                    lstRowCheck_TaiKhoan_TonTai.Add(rowIterator);
                                }
                                if (check_tenloptontai > 0)
                                {
                                    lstRowCheck_LopHoc_TonTai.Add(rowIterator);
                                }
                                //if (checkMail == false)
                                //{

                                //    lstRowError_Mail_DinhDangHopLe.Add(rowIterator);
                                //}
                                else
                                {
                                    var checkloimataikhoantrungtrenExcel = accountlophocList.Where(s => s.IDAccount == IDAccount).FirstOrDefault();
                                    if (checkloimataikhoantrungtrenExcel != null)
                                    {
                                        lstRowError_MaTaiKhoan_BiTrungTrenFileExcel.Add(rowIterator);
                                    }
                                    else
                                    {
                                        lstchunhiem.IDAccount = IDAccount;
                                        lstchunhiem.IDLopHoc = IDLopHoc;
                                        lstchunhiem.GhiChu = ghichu;
                                        accountlophocList.Add(lstchunhiem);
                                    }
                                    
                                }
                            }
                            catch (Exception)
                            {}
                            
                        }
                        if(lstRowCheck_TaiKhoan_TonTai.Count() > 0 || lstRowCheck_TaiKhoan_Trong.Count() > 0 || lstRowCheck_LopHoc_Trong.Count() > 0 || lstRowCheck_LopHoc_TonTai.Count() > 0 
                            || lstRowError_MaTaiKhoan_BiTrungTrenFileExcel.Count() > 0/*|| lstRowError_Mail_DinhDangHopLe.Count() > 0*/)
                        {

                            if (lstRowCheck_TaiKhoan_TonTai.Count() > 0)
                            {
                                var error = "Giảng Viên đã là chủ nhiệm của lớp khác : ";
                                foreach (var item in lstRowCheck_TaiKhoan_TonTai)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorGV += error + "</br>";
                            }

                            if (lstRowCheck_LopHoc_TonTai.Count() > 0)
                            {
                                var error = "Lớp Học đã có Chủ Nhiệm : ";
                                foreach (var item in lstRowCheck_LopHoc_TonTai)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorLop += error + "</br>";
                            }

                            if (lstRowCheck_TaiKhoan_Trong.Count() > 0)
                            {
                                var error = "Mã Tài Khoản bị trống hoặc quá 50 kí tự hoặc mã không phải là của Giảng Viên trong File Excel  : ";
                                foreach (var item in lstRowCheck_TaiKhoan_Trong)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
                            }
                            if (lstRowCheck_LopHoc_Trong.Count() > 0)
                            {
                                var error = "Tên Lớp Học bị trống hoặc quá 50 kí tự trong File Excel : ";
                                foreach (var item in lstRowCheck_LopHoc_Trong)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorkhongcokitu += error + "</br>";
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
                            //if (lstRowError_Mail_DinhDangHopLe.Count() > 0)
                            //{
                            //    var error = "aaa : ";
                            //    foreach (var item in lstRowError_Mail_DinhDangHopLe)
                            //    {
                            //        error += "Dòng " + item + " ";
                            //    }
                            //    ViewBag.Errorrrr+= error + "</br>";
                            //}
                            var lstacclop = this.LayDanhSachAccountLopHoc();
                            ViewBag.tengiaovien = LayDanhSachTaiKhoan();
                            ViewBag.phanloaitaikhoan = LayDanhSachTaiKhoan();
                            ViewBag.phanloailophoc = LayDanhSachLopHoc();
                            var listkhoaDTs = LayDanhSachKhoaDaoTao();
                            listkhoaDTs.Insert(0, new KhoaDaoTaoDTO
                            {
                                ID = 0,
                                TenKhoaDaoTao = "Tất cả Khóa Đào Tạo"
                            });
                            ViewData["khoaDT"] = new SelectList(listkhoaDTs, "ID", "TenKhoaDaoTao");
                            return View("Index", lstacclop);
                        }
                        else
                        {
                            foreach (var item in accountlophocList)
                            {
                                ThemAccountLopHoc(item);
                            }
                        }                       
                    }
                }
                else
                {
                    ViewBag.Error = "Chưa có File";
                    var lstacclop = this.LayDanhSachAccountLopHoc();
                    ViewBag.tengiaovien = LayDanhSachTaiKhoan();
                    ViewBag.phanloaitaikhoan = LayDanhSachTaiKhoan();
                    ViewBag.phanloailophoc = LayDanhSachLopHoc();
                    var listkhoaDTs = LayDanhSachKhoaDaoTao();
                    listkhoaDTs.Insert(0, new KhoaDaoTaoDTO
                    {
                        ID = 0,
                        TenKhoaDaoTao = "Tất cả Khóa Đào Tạo"
                    });
                    ViewData["khoaDT"] = new SelectList(listkhoaDTs, "ID", "TenKhoaDaoTao");
                    return View("Index", lstacclop);
                }
            }
            ViewBag.Success = "Thành Công";
            var lstaccountlophoc = this.LayDanhSachAccountLopHoc();
            ViewBag.tengiaovien = LayDanhSachTaiKhoan();
            ViewBag.phanloaitaikhoan = LayDanhSachTaiKhoan();
            ViewBag.phanloailophoc = LayDanhSachLopHoc();
            var listkhoaDT = LayDanhSachKhoaDaoTao();
            listkhoaDT.Insert(0, new KhoaDaoTaoDTO
            {
                ID = 0,
                TenKhoaDaoTao = "Tất cả Khóa Đào Tạo"
            });
            ViewData["khoaDT"] = new SelectList(listkhoaDT, "ID", "TenKhoaDaoTao");
            return View("Index", lstaccountlophoc);
        }



        public int LayGiangVienDaTonTai(int? id)
        {
            using (AccountLopHocBusiness bs = new AccountLopHocBusiness())
            {
                return bs.LayGiangVienDaTonTai(id);
            }
        }

        public int LayLopHocDaTonTai(int? id)
        {
            using (AccountLopHocBusiness bs = new AccountLopHocBusiness())
            {
                return bs.LayLopHocDaTonTai(id);
            }
        }
        public List<AccountDTO> LayDanhSachTaiKhoan_GiangVien()
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayDanhSachTaiKhoan_GiangVien();
            }
        }
        public List<AccountDTO> LayDanhSachTaiKhoan()
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayDanhSachTaiKhoan();
            }
        }

        public List<LopHocDTO> LayDanhSachLopHoc()
        {
            using (LopHocBusiness bs = new LopHocBusiness())
            {
                return bs.LayDanhSachLopHoc();
            }
        }

        public bool ThemAccountLopHoc(AccountLopHocDTO acclop)
        {
            using (AccountLopHocBusiness bs = new AccountLopHocBusiness())
            {
                return bs.ThemAccountLopHoc(acclop);
            }
        }

        public bool LayMaKhongCoKiTu(AccountLopHocDTO acclop)
        {
            if (String.IsNullOrEmpty(acclop.Ma))
            {
                return false;
            }
            return true;
        }

        public ActionResult Edit_ThayDoiTaiKhoan(int id)
        {          

            ViewData["magiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVien(), "ID", "Ma",id);
            ViewData["tengiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(id), "ID", "HoVaTen");
            ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(id), "ID", "MailVL");
            ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop");
            return View("Edit");
        }

        //Get: SuaChuNhiem
        public ActionResult Edit(int id)
        {
            HttpCookie idEdit = new HttpCookie("idEdit");
            idEdit.Value = id.ToString();
            idEdit.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(idEdit);

            AccountLopHocDTO acclop = LayAccountLopHoc(id);
            ViewData["magiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVien(), "ID", "Ma");
            ViewData["tengiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(Convert.ToInt32(acclop.IDAccount)), "ID", "HoVaTen");
            ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(Convert.ToInt32(acclop.IDAccount)), "ID", "MailVL");
            ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop");

            return View(acclop);
        }

        //Post : SuaChuNhiem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AccountLopHocDTO acclop)
        {
            var findlop = LayLopHocDaTonTai(acclop.IDLopHoc);
            var findgv = LayGiangVienDaTonTai(acclop.IDAccount);
            Response.Cookies.Remove("idAccount");
            Response.Cookies.Remove("idLopHoc");
            if ((findgv == acclop.ID || findgv == 0) && (findlop == acclop.ID || findlop == 0))
            {
                SuaAccountLopHoc(acclop);
                TempData["Success"] = "Thành công";
                return RedirectToAction("Index");
            }
            else
            {
                if (findgv != acclop.ID && findgv > 0)
                {
                    ViewBag.Errorgv = "Giảng viên đã là chủ nhiệm lớp khác";
                    ViewData["magiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVien(), "ID", "Ma");
                    ViewData["tengiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(Convert.ToInt32(acclop.IDAccount)), "ID", "HoVaTen");
                    ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(Convert.ToInt32(acclop.IDAccount)), "ID", "MailVL");
                    ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop");
                    return View();
                }
                else if (findlop != acclop.ID && findlop > 0)
                {
                    ViewBag.Errorlop = "Lớp đã có chủ nhiệm";
                    ViewData["magiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVien(), "ID", "Ma");
                    ViewData["tengiaovien"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(Convert.ToInt32(acclop.IDAccount)), "ID", "HoVaTen");
                    ViewData["account"] = new SelectList(LayDanhSachTaiKhoan_GiangVienDeChon(Convert.ToInt32(acclop.IDAccount)), "ID", "MailVL");
                    ViewData["lophoc"] = new SelectList(LayDanhSachLopHoc(), "ID", "TenLop");
                    return View();
                }
                else
                {
                    return View();
                }
            }
        }

        public AccountLopHocDTO LayAccountLopHoc(int id)
        {
            using (AccountLopHocBusiness bs = new AccountLopHocBusiness())
            {
                return bs.LayAccountLopHoc(id);
            }
        }
        public bool SuaAccountLopHoc(AccountLopHocDTO acclop)
        {
            using (AccountLopHocBusiness bs = new AccountLopHocBusiness())
            {
                return bs.SuaAccountLopHoc(acclop);
            }
        }

        //XoaChuNhiem
        public async Task<ActionResult> Delete(int id)
        {
            var output = XoaAccountLopHoc(id);
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

        public bool XoaAccountLopHoc(int id)
        {
            using (AccountLopHocBusiness bs = new AccountLopHocBusiness())
            {
                return bs.XoaAccountLopHoc(id);
            }
        }

        //Xemchitiet
        public ActionResult Details(int id)
        {
            var acclop = LayAccountLopHoc(id);
            ViewBag.magiaovien = LayDanhSachTaiKhoan();
            ViewBag.tengiaovien = LayDanhSachTaiKhoan();
            ViewBag.taikhoan = LayDanhSachTaiKhoan();
            ViewBag.lophoc = LayDanhSachLopHoc();
            return View(acclop);
        }

        public int LayIDAccountTheoMa(string magiangvien)
        {
            using(AccountLopHocBusiness bs = new AccountLopHocBusiness())
            {
                return bs.LayIDAccountTheoMa(magiangvien);
            }
        }

        public int LayIDLopHocTheoTen(string lophoc)
        {
            using (AccountLopHocBusiness bs = new AccountLopHocBusiness())
            {
                return bs.LayIDLopHocTheoTen(lophoc);
            }
        }

        public int Check_TaiKhoanTonTai(int idAccount)
        {
            using (AccountLopHocBusiness bs = new AccountLopHocBusiness())
            {
                return bs.Check_TaiKhoanTonTai(idAccount);
            }
        }

        public int Check_LopHocTonTai(int idLopHoc)
        {
            using (AccountLopHocBusiness bs = new AccountLopHocBusiness())
            {
                return bs.Check_LopHocTonTai(idLopHoc);
            }
        }

        //public bool LayMailHopLe(string mail)
        //{


        //    var data = mail.Split('@');
        //    if (data.Length == 2 && (data[1] == "vlu.edu.vn"))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

    }
}