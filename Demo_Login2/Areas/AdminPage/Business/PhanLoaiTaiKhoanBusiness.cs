using Demo_Login2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class PhanLoaiTaiKhoanBusiness : BaseBusiness
    {
        public List<PhanLoaiTaiKhoanDTO> LayDanhSachPhanLoaiTaiKhoan()
        {
            try
            {
                var listphanloaitaikhoan = model.PhanLoaiTaiKhoans.Select(s => new PhanLoaiTaiKhoanDTO
                {
                    ID = s.ID,
                    LoaiTaiKhoan = s.LoaiTaiKhoan,
                    GhiChu = s.GhiChu
                }).ToList();
                return listphanloaitaikhoan;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}