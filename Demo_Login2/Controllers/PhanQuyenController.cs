using Demo_Login2.Areas.AdminPage.Business;
using Demo_Login2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Demo_Login2.Controllers
{
    [Authorize]
    public class PhanQuyenController : Controller
    {
        // GET: PhanQuyen
        public ActionResult Index()
        {

            var userClaims = User.Identity as ClaimsIdentity;
            var mail = userClaims?.FindFirst("preferred_username")?.Value;
            var ma = mail.Split('@')[0].Split('.')[1];

            HttpCookie mailvl = new HttpCookie("mailvl");
            mailvl.Value = mail;
            mailvl.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(mailvl);

            //Session["mailvl"] = mail;

            //Console.WriteLine(ma);
            using (TaiKhoanBusiness tk = new TaiKhoanBusiness())
            {
                var loai = tk.LayLoaiTaiKhoan(mail);
                if (loai == 1)
                {
                    var KhoaDaoTao = tk.LayKhoaDaoTaoChoTaiKhoan(mail);

                    var idAccount = tk.LayIDChoTaiKhoan(mail);

                    var idLopHoc = tk.LayLopHocChoTaiKhoan_SV(mail);

                    HttpCookie idKhoaDaoTao = new HttpCookie("idKhoaDaoTao");
                    idKhoaDaoTao.Value = KhoaDaoTao.ToString();
                    idKhoaDaoTao.Expires = DateTime.Now.AddDays(1);
                    Response.SetCookie(idKhoaDaoTao);
                    //Session["idKhoaDaoTao"] = KhoaDaoTao; 

                    HttpCookie loaiTK = new HttpCookie("loai");
                    loaiTK.Value = "Sinh Viên";
                    loaiTK.Expires = DateTime.Now.AddDays(1);
                    Response.SetCookie(loaiTK);

                    HttpCookie idaccount = new HttpCookie("idAccount");
                    idaccount.Value = idAccount.ToString();
                    idaccount.Expires = DateTime.Now.AddDays(1);
                    Response.SetCookie(idaccount);

                    HttpCookie idlophoc = new HttpCookie("idLopHoc");
                    idlophoc.Value = idLopHoc.ToString();
                    idlophoc.Expires = DateTime.Now.AddDays(1);
                    Response.SetCookie(idlophoc);

                    return RedirectToAction("Index", "ChuongTrinhDaoTao", new { area = "SinhVienPage" });
                }
                else if (loai == 2)
                {
                    var idLop = tk.LayLopHocChoTaiKhoan_ChuNhiem(mail);

                    HttpCookie idlop = new HttpCookie("idLopChuNhiem");
                    idlop.Value = idLop.ToString();
                    idlop.Expires = DateTime.Now.AddDays(1);
                    Response.SetCookie(idlop);

                    //Session["idLopChuNhiem"] = idLop;

                    HttpCookie loaiTK = new HttpCookie("loai");
                    loaiTK.Value = "Giảng Viên";
                    loaiTK.Expires = DateTime.Now.AddDays(1);
                    Response.SetCookie(loaiTK);
                    return RedirectToAction("Index", "ChuongTrinhDaoTao", new { area = "GiangVienPage" });
                    //return View("GiangVien");
                }
                else if (loai == 3)
                {
                    HttpCookie loaiTK = new HttpCookie("loai");
                    loaiTK.Value = "Khoa CNTT";
                    loaiTK.Expires = DateTime.Now.AddDays(1);
                    Response.SetCookie(loaiTK);
                    return RedirectToAction("Index", "ThongKe", new { area = "AdminPage" });
                    //return View("~/Areas/AdminPage/Views/ThongKe/Index.cshtml");
                }
                else
                {
                    return Redirect("/Login/Index");
                }
            }
        }
    }
}