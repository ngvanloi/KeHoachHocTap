using Demo_Login2.Areas.AdminPage.Business;
using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Demo_Login2.Areas.AdminPage.Controllers
{
    public class TaiKhoanController : Controller
    {
        //Get: LayDanhSachTaiKhoan
        public ActionResult Index()
        {
            var lstAccount = this.LayDanhSachTaiKhoan();
            ViewBag.phanloaitaikhoan = LayDanhSachPhanLoaiTaiKhoan();
            return View(lstAccount);
        }
        public List<AccountDTO> LayDanhSachTaiKhoan()
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayDanhSachTaiKhoan();
            }
        }
        public List<PhanLoaiTaiKhoanDTO> LayDanhSachPhanLoaiTaiKhoan()
        {
            using (PhanLoaiTaiKhoanBusiness bs = new PhanLoaiTaiKhoanBusiness())
            {
                return bs.LayDanhSachPhanLoaiTaiKhoan();
            }
        }
        //Get: TaoTaiKhoan
        public ActionResult Create()
        {
            ViewData["phanloaitaikhoan"] = new SelectList(LayDanhSachPhanLoaiTaiKhoan(), "ID", "LoaiTaiKhoan");
            return View();
        }

        //Post :TaoTaiKhoan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AccountDTO account)
        {
            var findMail = LayMailDaTonTai(account.MailVL);
            var findMa = LayMaDaTonTai(account.Ma);

            var resultMa = LayYeuCauNhapMa(account);
            var resultHoTen = LayYeuCauNhapHoTen(account);
            var resultMail = LayYeuCauNhapMail(account);

            var resultAll = false;

            if (resultMa == false)
            {

                ViewBag.ErrorkhongcokituMa = "Yêu cầu nhập đầy đủ các trường bắt buộc";
                resultAll = true;
            }
            if (resultHoTen == false)
            {

                ViewBag.ErrorkhongcokituHoTen = "Yêu cầu nhập đầy đủ các trường bắt buộc";
                resultAll = true;

            }
            if (resultMail == false)
            {

                ViewBag.ErrorkhongcokituMail = "Yêu cầu nhập đầy đủ các trường bắt buộc";
                resultAll = true;

            }
            if (findMa > 0)
            {
                ViewBag.ErrorTrungMa = "Mã đã tồn tại";
                resultAll = true;

            }
            if (findMail > 0)
            {
                ViewBag.ErrorTrungMail = "Mail đã tồn tại";
                resultAll = true;

            }
            if (resultAll == false)
            {
                var checkMail = LayMailHopLe(account.MailVL,account.PhanLoai);
                if (checkMail == false)
                {
                    ViewBag.ErrorDinhDangMail = "Mail nhập sai định dạng hoặc vai trò sai";
                    ViewData["phanloaitaikhoan"] = new SelectList(LayDanhSachPhanLoaiTaiKhoan(), "ID", "LoaiTaiKhoan");
                    return View();
                }
                else
                {
                    ThemTaiKhoan(account);
                    ViewData["phanloaitaikhoan"] = new SelectList(LayDanhSachPhanLoaiTaiKhoan(), "ID", "LoaiTaiKhoan");
                    TempData["Success"] = "Thành công";
                    return RedirectToAction("Index");
                }

            }
            ViewData["phanloaitaikhoan"] = new SelectList(LayDanhSachPhanLoaiTaiKhoan(), "ID", "LoaiTaiKhoan");
            return View();
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
                        List<AccountDTO> accountList = new List<AccountDTO>();

                        List<int> lstRowError_Ma_Trung = new List<int>();
                        List<int> lstRowError_Mail_Trung = new List<int>();
                        List<int> lstRowError_Ma_VuotQuaKiTu = new List<int>();
                        List<int> lstRowError_HovaTen_VuotQuaKiTu = new List<int>();
                        List<int> lstRowError_Mail_VuotQuaKiTu = new List<int>();
                        List<int> lstRowError_Mail_DinhDangHopLe = new List<int>();
                        List<int> lstRowError_PhanLoai_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_Ma_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_HoTen_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_Mail_KhongCoKiTu = new List<int>();
                        List<int> lstRowError_Ma_BiTrungTrenFileExcel = new List<int>();
                        List<int> lstRowError_Mail_BiTrungTrenFileExcel = new List<int>();

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {

                            var acc = new AccountDTO();
                            acc.Ma = Convert.ToString(workSheet.Cells[rowIterator, 1].Value).Trim();
                            acc.HoVaTen = Convert.ToString(workSheet.Cells[rowIterator, 2].Value).Trim();
                            acc.MailVL = Convert.ToString(workSheet.Cells[rowIterator, 3].Value).Trim();
                            acc.PhanLoai = Convert.ToInt32(workSheet.Cells[rowIterator, 4].Value);
                            //acc.DaXem = Convert.ToBoolean(workSheet.Cells[rowIterator, 5].Value);
                            acc.GhiChu = Convert.ToString(workSheet.Cells[rowIterator, 6].Value).Trim();

                            var findMail = LayMailDaTonTai(acc.MailVL);
                            var findMa = LayMaDaTonTai(acc.Ma);
                            var checkMail = LayMailHopLe(acc.MailVL,acc.PhanLoai);
                            var resultMa = LayMaKhongCoKiTu(acc);
                            var resultHoTen = LayHoTenKhongCoKiTu(acc);
                            var resultMail = LayMailKhongCoKiTu(acc);

                            if(resultMa == false)
                            {
                                lstRowError_Ma_KhongCoKiTu.Add(rowIterator);
                            }
                            if (resultHoTen == false)
                            {
                                lstRowError_HoTen_KhongCoKiTu.Add(rowIterator);
                            }
                            if (resultMail == false)
                            {
                                lstRowError_Mail_KhongCoKiTu.Add(rowIterator);
                            }
                            if (findMa > 0)
                            {
                                lstRowError_Ma_Trung.Add(rowIterator);
                            }
                            if (findMail > 0)
                            {
                                lstRowError_Mail_Trung.Add(rowIterator);
                            }
                            if (acc.Ma.Length > 50)
                            {
                                lstRowError_Ma_VuotQuaKiTu.Add(rowIterator);
                            }
                            if (acc.HoVaTen.Length > 50)
                            {
                                
                                lstRowError_HovaTen_VuotQuaKiTu.Add(rowIterator);
                            }
                            if (acc.MailVL.Length > 50)
                            {
                                lstRowError_Mail_VuotQuaKiTu.Add(rowIterator);
                            }
                            if(acc.PhanLoai < 1)
                            {
                                lstRowError_PhanLoai_KhongCoKiTu.Add(rowIterator);
                            }
                            if (checkMail == false)
                            {
                               
                                lstRowError_Mail_DinhDangHopLe.Add(rowIterator);
                            }
                            else
                            {
                                var checkloimatrungtrenExcel = accountList.Where(s => s.Ma == acc.Ma).FirstOrDefault();
                                var checkloimailtrungtrenExcel = accountList.Where(s => s.MailVL == acc.MailVL).FirstOrDefault();
                                if (checkloimatrungtrenExcel != null)
                                {
                                    lstRowError_Ma_BiTrungTrenFileExcel.Add(rowIterator);
                                }
                                if (checkloimailtrungtrenExcel != null)
                                {
                                    lstRowError_Mail_BiTrungTrenFileExcel.Add(rowIterator);
                                }
                                else
                                {
                                    accountList.Add(acc);
                                }
                               
                            }
                        }
                        if(lstRowError_Ma_Trung.Count() > 0 || lstRowError_Mail_Trung.Count() > 0 || lstRowError_Ma_VuotQuaKiTu.Count() > 0
                            || lstRowError_HovaTen_VuotQuaKiTu.Count() > 0 || lstRowError_Mail_VuotQuaKiTu.Count() > 0 || lstRowError_Mail_DinhDangHopLe.Count() > 0 
                            || lstRowError_Ma_KhongCoKiTu.Count() > 0 || lstRowError_HoTen_KhongCoKiTu.Count() > 0|| lstRowError_Mail_KhongCoKiTu.Count() > 0 || lstRowError_PhanLoai_KhongCoKiTu.Count() > 0
                            || lstRowError_Ma_BiTrungTrenFileExcel.Count() > 0 || lstRowError_Mail_BiTrungTrenFileExcel.Count() > 0)
                        {
                            if(lstRowError_Ma_Trung.Count() > 0)
                            {
                                var error = "Các dòng bị trùng ở cột Mã trong file Excel : ";
                                foreach(var item in lstRowError_Ma_Trung)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.trungdata += error + "<br/>";
                            }
                            if(lstRowError_Mail_Trung.Count() > 0)
                            {
                                var error = "Các dòng bị trùng ở cột MailVL trong file Excel : ";
                                foreach(var item in lstRowError_Mail_Trung)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.trungdata += error + "<br/>";
                            }
                            if(lstRowError_Ma_VuotQuaKiTu.Count() > 0)
                            {
                                var error = "Các dòng ở cột Mã vượt quá 50 kí tự trong File Excel : ";
                                foreach(var item in lstRowError_Ma_VuotQuaKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorvuotquakitu += error + "<br/>";
                            }
                            if(lstRowError_HovaTen_VuotQuaKiTu.Count() > 0)
                            {
                                var error = "Các dòng ở cột Họ Và Tên vượt quá 50 kí tự trong File Excel :";
                                foreach(var item in lstRowError_HovaTen_VuotQuaKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorvuotquakitu += error + "<br/>";
                            }
                            if(lstRowError_Mail_VuotQuaKiTu.Count() > 0)
                            {
                                var error = "Các dòng ở cột Mail vượt quá 50 kí tự trong File Excel : ";
                                foreach(var item in lstRowError_Mail_VuotQuaKiTu)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.Errorvuotquakitu += error + "<br/>";
                            }
                            if(lstRowError_Mail_DinhDangHopLe.Count() > 0)
                            {
                                var error = "Các dòng ở cột Mail nhập sai định dạng hoặc vai trò sai trong File Excel : ";
                                foreach(var item in lstRowError_Mail_DinhDangHopLe)
                                {
                                    error += "Dòng " + item + " ";
                                }
                                ViewBag.ErrorDinhDangMail += error + "<br/>";
                            }
                            if (lstRowError_PhanLoai_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Các dòng chưa nhập đầy đủ thông tin trong File Excel cột Phân Loại : ";
                                foreach (var item in lstRowError_PhanLoai_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + "  ";
                                }
                                ViewBag.Errorkhongcokitu += error + "<br/>";
                            }
                            if (lstRowError_Ma_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Các dòng chưa nhập đầy đủ thông tin trong File Excel cột Mã : ";
                                foreach(var item in lstRowError_Ma_KhongCoKiTu)
                                {
                                    error += "Dòng " + item  + "  " ;
                                }
                                ViewBag.Errorkhongcokitu += error + "<br/>";
                            }
                            if (lstRowError_HoTen_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Các dòng chưa nhập đầy đủ thông tin trong File Excel cột Họ Tên : ";
                                foreach (var item in lstRowError_HoTen_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + "  ";
                                }
                                ViewBag.Errorkhongcokitu += error + "<br/>";
                            }
                            if (lstRowError_Mail_KhongCoKiTu.Count() > 0)
                            {
                                var error = "Các dòng chưa nhập đầy đủ thông tin trong File Excel cột MailVL : ";
                                foreach (var item in lstRowError_Mail_KhongCoKiTu)
                                {
                                    error += "Dòng " + item + "  ";
                                }
                                ViewBag.Errorkhongcokitu += error + "<br/>";
                            }
                            if (lstRowError_Ma_BiTrungTrenFileExcel.Count() > 0)
                            {
                                var error = "Mã bị trùng trên File Excel : ";
                                foreach (var item in lstRowError_Ma_BiTrungTrenFileExcel)
                                {
                                    error += "Dòng " + item + "  ";
                                }
                                ViewBag.ErrorMaBiTrungTrenExcel += error + "<br/>";
                            }
                            if (lstRowError_Mail_BiTrungTrenFileExcel.Count() > 0)
                            {
                                var error = "MailVL bị trùng trên File Excel : ";
                                foreach (var item in lstRowError_Mail_BiTrungTrenFileExcel)
                                {
                                    error += "Dòng " + item + "  ";
                                }
                                ViewBag.ErrorMailBiTrungTrenExcel += error + "<br/>";
                            }
                            var lstAccount = this.LayDanhSachTaiKhoan();
                            ViewBag.phanloaitaikhoan = LayDanhSachPhanLoaiTaiKhoan();
                            return View("Index", lstAccount);
                        }
                        else
                        {
                            foreach (var item in accountList)
                            {
                                ThemTaiKhoan(item);
                            }
                        }                      

                    }
                }
                else
                {
                    ViewBag.Error = "Chưa có File";
                    var lstAccount = this.LayDanhSachTaiKhoan();
                    ViewBag.phanloaitaikhoan = LayDanhSachPhanLoaiTaiKhoan();
                    return View("Index", lstAccount);

                }
            }
            ViewBag.Success = "Thành công";
            var lstAcc = this.LayDanhSachTaiKhoan();
            ViewBag.phanloaitaikhoan = LayDanhSachPhanLoaiTaiKhoan();
            return View("Index", lstAcc);
        }

        public int LayMailDaTonTai(string mailvl)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayMailDaTonTai(mailvl);
            }
        }

        public int LayMaDaTonTai(string ma)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayMaDaTonTai(ma);
            }
        }

        public bool LayMailHopLe(string mail,int phanloai)
        {


            var data = mail.Split('@');
            if(phanloai == 1)
            {
                if (data.Length == 2 && data[1] == "vanlanguni.vn")
                {
                    return true;
                }
            }
            else
            {
                if (data.Length == 2 && data[1] == "vlu.edu.vn")
                {
                    return true;
                }
                
            }         
            return false;
        }

        public bool LayMaKhongCoKiTu(AccountDTO account)
        {
            if(String.IsNullOrEmpty(account.Ma))
            {
                return false;
            }
            return true;
        }

        public bool LayHoTenKhongCoKiTu(AccountDTO account)
        {
            if (String.IsNullOrEmpty(account.HoVaTen))
            {
                return false;
            }
            return true;
        }

        public bool LayMailKhongCoKiTu(AccountDTO account)
        {
            if (String.IsNullOrEmpty(account.MailVL))
            {
                return false;
            }
            return true;
        }

        public bool LayYeuCauNhapMa(AccountDTO account)
        {
            if (account.Ma == null)
            {
                return false;

            }
            return true;
        }

        public bool LayYeuCauNhapHoTen(AccountDTO account)
        {
            if (account.HoVaTen == null)
            {
                return false;

            }
            return true;
        }

        public bool LayYeuCauNhapMail(AccountDTO account)
        {
            if (account.MailVL == null)
            {
                return false;

            }
            return true;
        }


        public List<KhoaDaoTaoDTO> LayDanhSachKhoaDaoTao()
        {
            using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
            {
                return bs.LayDanhSachKhoaDaoTao();
            }
        }
        public bool ThemTaiKhoan(AccountDTO taikhoan)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.ThemTaiKhoan(taikhoan);
            }
        }

        //Get: Suataikhoan
        public ActionResult Edit(int id)
        {
            ViewData["phanloaitaikhoan"] = new SelectList(LayDanhSachPhanLoaiTaiKhoan(), "ID", "LoaiTaiKhoan");
            AccountDTO account = LayTaiKhoan(id);
            return View(account);
        }
        //Post : SuaTaiKhoan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AccountDTO account)
        {
            var findMa = LayMaDaTonTai(account.Ma);
            var findMail = LayMailDaTonTai(account.MailVL);

            var resultMa = LayYeuCauNhapMa(account);
            var resultHoTen = LayYeuCauNhapHoTen(account);
            var resultMail = LayYeuCauNhapMail(account);

            var resultAll = false;

            if ((findMa == account.ID || findMa == 0) 
                && (findMail == account.ID || findMail == 0) 
                && (resultMa == true && resultHoTen == true && resultMail == true) 
                && resultAll == false)
            {
                var checkMail = LayMailHopLe(account.MailVL,account.PhanLoai);
                if (checkMail == true)
                {

                    SuaTaiKhoan(account);
                    TempData["Success"] = "Thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorDinhDangMail = "Mail nhập sai định dạng hoặc vai trò sai";
                    ViewData["phanloaitaikhoan"] = new SelectList(LayDanhSachPhanLoaiTaiKhoan(), "ID", "LoaiTaiKhoan");
                    return View();
                }
            }
            else
            {

                if (resultMa == false)
                {
                    ViewBag.ErrorkhongcokituMa = "Yêu cầu nhập đầy đủ các trường bắt buộc";
                    resultAll = true;
                }
                if (resultHoTen == false)
                {
                    ViewBag.ErrorkhongcokituHoTen = "Yêu cầu nhập đầy đủ các trường bắt buộc";
                    resultAll = true;
                }
                if (resultMail == false)
                {
                    ViewBag.ErrorkhongcokituMail = "Yêu cầu nhập đầy đủ các trường bắt buộc";
                    resultAll = true;
                }
                if (findMa != account.ID && findMa > 0)
                {
                    ViewBag.ErrorMa = "Mã đã tồn tại";
                    resultAll = true;
                }
                if(findMail != account.ID && findMail > 0)
                {
                    ViewBag.ErrorMail = "Mail đã tồn tại";
                    resultAll = true;
                }
            }
            ViewData["phanloaitaikhoan"] = new SelectList(LayDanhSachPhanLoaiTaiKhoan(), "ID", "LoaiTaiKhoan");
            return View();

        }

        public AccountDTO LayTaiKhoan(int id)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.LayTaiKhoan(id);
            }
        }
        public bool SuaTaiKhoan(AccountDTO taikhoan)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.SuaTaiKhoan(taikhoan);
            }
        }

        //Xoataikhoan
        public async Task<ActionResult> Delete(int id)
        {
            var findgv = CheckLoiGiangVienDaTonTai(id);
            var findsv = CheckLoiSinhVienDaTonTai(id);
            var findsv_ketquahoctap = CheckLoiSinhVienDaTonTai_KetQuaHocTap(id);
            var findsv_sinhviendangkikehoachhoctap = CheckLoiSinhVienDangKiKeHoachHocTap_Moi(id);
            if (findgv > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaitaikhoan = LayDanhSachPhanLoaiTaiKhoan();
                return RedirectToAction("Index");
            }
            else if (findsv > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaitaikhoan = LayDanhSachPhanLoaiTaiKhoan();
                return RedirectToAction("Index");
            }
            else if (findsv_ketquahoctap > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaitaikhoan = LayDanhSachPhanLoaiTaiKhoan();
                return RedirectToAction("Index");
            }
            else if (findsv_sinhviendangkikehoachhoctap > 0)
            {
                TempData["error"] = "lỗi";
                ViewBag.phanloaitaikhoan = LayDanhSachPhanLoaiTaiKhoan();
                return RedirectToAction("Index");
            }
            else
            {
                var output = XoaTaiKhoan(id);
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


        }

        public bool XoaTaiKhoan(int id)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.XoaTaiKhoan(id);
            }
        }

        //Xemchitiet
        public ActionResult Details(int id)
        {

            var account = LayTaiKhoan(id);
            ViewBag.taikhoan = LayDanhSachTaiKhoan();
            ViewBag.phanloaitaikhoan = LayDanhSachPhanLoaiTaiKhoan();
            return View(account);
        }

        public int CheckLoiGiangVienDaTonTai(int? idaccount)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.CheckLoiGiangVienDaTonTai(idaccount);
            }
        }

        public int CheckLoiSinhVienDaTonTai(int? idaccount)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.CheckLoiSinhVienDaTonTai(idaccount);
            }
        }

        public int CheckLoiSinhVienDaTonTai_KetQuaHocTap(int? idaccount)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.CheckLoiSinhVienDaTonTai_KetQuaHocTap(idaccount);
            }
        }
        public int CheckLoiSinhVienDangKiKeHoachHocTap_Moi(int? idaccount)
        {
            using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
            {
                return bs.CheckLoiSinhVienDangKiKeHoachHocTap_Moi(idaccount);
            }
        }

        //public ActionResult LayDanhSachTaiKhoan(dynamic dynamic)
        //{
        //    using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
        //    {
        //        return View("Index", bs.LayDanhSachTaiKhoan());
        //    }

        //    using (KhoaDaoTaoBusiness bs = new KhoaDaoTaoBusiness())
        //    {
        //        return View("Index", bs.LayDanhSachKhoaDaoTao());
        //    }
        //}

        //public ActionResult LayTaiKhoan(int id)
        //{
        //    using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
        //    {
        //        AccountDTO acc = bs.LayTaiKhoan(id);
        //        return View("Index",acc);
        //    }
        //}


        //public ActionResult ThemTaiKhoan(AccountDTO taikhoan)
        //{
        //    using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
        //    {
        //        return View("Index",bs.ThemTaiKhoan(taikhoan));
        //    }
        //}

        //public ActionResult XoaTaiKhoan(int id)
        //{
        //    using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
        //    {
        //        var result = bs.XoaTaiKhoan(id);
        //        if (result)
        //            return View("Successed");
        //        else
        //            return View("Failed");
        //    }
        //}

        //public ActionResult SuaTaiKhoan(AccountDTO taikhoan)
        //{
        //    using (TaiKhoanBusiness bs = new TaiKhoanBusiness())
        //    {
        //        return View("Index", bs.SuaTaiKhoan(taikhoan));
        //    }
        //}
    }
}