using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class KeHoachHocTap_MoiBusiness : BaseBusiness
    {
        public List<KeHoachHocTap_MoiDTO> LayDanhSachKetQuaHocTap_Moi_TheoKhoaDaoTao(int id)
        {
            try
            {
                var lstkehoachHT = model.KeHoachHocTap_Mois.Where(s => s.IDKhoaDaoTao == id).Select(s => new KeHoachHocTap_MoiDTO
                {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.SoTinChi,

                    SoTietLyThuyet = s.SoTietLyThuyet,
                    SoTietThucHanh = s.SoTietThucHanh,

                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,

                    IDHocKi = s.IDHocKi,
                    TenHocKi = s.HocKi.TenHocKi,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    LoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDKhoaBoMon = s.MonHoc.IDKhoaBoMon,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,
                }).ToList();
                return lstkehoachHT;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ThemKeHoachHocTap_Moi(ChuongTrinhDaoTao_MoiDTO lstctrdaotao, int idKhoaDT,int idHocKi)
        {
            try
            {
                var newItem = new KeHoachHocTap_Moi();
                newItem.ID = lstctrdaotao.ID;
                newItem.IDMonHoc = lstctrdaotao.IDMonHoc;
                newItem.IDKhoaDaoTao = idKhoaDT;
                newItem.IDHocKi = idHocKi;
                newItem.SoTinChi = lstctrdaotao.SoTinChi;
                newItem.SoTietLyThuyet = lstctrdaotao.SoTietLyThuyet;
                newItem.SoTietThucHanh = lstctrdaotao.SoTietThucHanh;
                newItem.IDPhanLoaiMonHoc = lstctrdaotao.IDPhanLoaiMonHoc;
                newItem.TenMonHocTienQuyet = lstctrdaotao.TenMonHocTienQuyet;
                newItem.TenMonHocHocTruoc = lstctrdaotao.TenMonHocHocTruoc;

                model.KeHoachHocTap_Mois.Add(newItem);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Xoa_KeHoachHocTap(int idKhoaDT,int idHocKi)
        {
            try
            {
                var lstKHHT = model.KeHoachHocTap_Mois.Where(s => s.IDKhoaDaoTao == idKhoaDT && s.IDHocKi == idHocKi).ToList();
                foreach(var item in lstKHHT)
                {
                    model.KeHoachHocTap_Mois.Remove(item);
                }
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Check_MonHocDaTonTai(string mamonhoc, int idKhoaDT, int idHocKi)
        {
            try
            {
                var lstKHHT = model.KeHoachHocTap_Mois.Where(s => s.MonHoc.MaMonHoc == mamonhoc && s.IDHocKi == idHocKi && s.IDKhoaDaoTao == idKhoaDT).FirstOrDefault();
                if (lstKHHT == null)
                {
                    return true;
                }
                return false;
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public bool Check_XoaKhoaDaoTaoKhiKhongCoDuLieu( int idKhoaDT, int idHocKi)
        {
            try
            {
                var lstKHHT = model.KeHoachHocTap_Mois.Where(s => s.IDHocKi == idHocKi && s.IDKhoaDaoTao == idKhoaDT).FirstOrDefault();
                if (lstKHHT == null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}