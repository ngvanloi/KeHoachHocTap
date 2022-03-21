using Demo_Login2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class ThongKeBusiness : BaseBusiness
    {
        public int LayTongTatCaSinhVien(int idKhoaDT)
        {
            try
            {
                var lstsv = model.SinhVienLopHocs.Where(s => s.LopHoc.IDKhoaDaoTao == idKhoaDT && s.Account.PhanLoai == 1).Select(s => s.IDAccount).ToList();
                return lstsv.Count();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public int LayTongSinhVienDaDangKi(int idKhoaDT,int idHocKi)
        {
            try
            {
                var lstsv = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDKhoaDaoTao == idKhoaDT && s.IDHocKi == idHocKi).Select(s => s.IDAccount).ToList();

                List<int> lstResult = new List<int>();
                foreach(var item in lstsv)
                {
                    var taikhoan = lstResult.FirstOrDefault(s => s == item);
                    if(taikhoan == 0)
                    {
                        lstResult.Add((int)item);
                    }
                }
                return lstResult.Count();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string LayTenKhoaDaoTao(int idKhoaDT)
        {
            try
            {
                var lstsv = model.KhoaDaoTaos.Where(s => s.ID == idKhoaDT).Select(s => s.TenKhoaDaoTao).FirstOrDefault();
                return lstsv;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<KeHoachHocTap_MoiDTO> LayKeHoachHocTapTheoKhoaDaoTaoVaHocKi(int idKhoaDT,int idHocKi)
        {
            try
            {
                var lstsv = model.KeHoachHocTap_Mois.Where(s => s.IDKhoaDaoTao == idKhoaDT && s.IDHocKi == idHocKi).Select(s => new KeHoachHocTap_MoiDTO
                {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc
                }).ToList();
                return lstsv;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<KeHoachHocTap_MoiDTO> LayKeHoachHocTapSVDaDangKiTheoKhoaDaoTaoVaHocKi(int idKhoaDT,int idHocKi)
        {
            try
            {
                var lstsv = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDKhoaDaoTao == idKhoaDT && s.IDHocKi == idHocKi).Select(s => new KeHoachHocTap_MoiDTO
                {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc
                }).ToList();
                return lstsv;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}