using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.SinhVienPage.Business
{
    public class SinhVienDangKiKeHoachHocTapBusiness : BaseBusiness
    {
        public List<SinhVienDangKiKeHoachHocTapDTO> LayKeHoachHocTap_Moi(int idKhoaDT, int idHocKi)
        {
            try
            {
                var lstkehoachHT = model.KeHoachHocTap_Mois.Where(s => s.IDKhoaDaoTao == idKhoaDT && s.IDHocKi == idHocKi).Select(s => new SinhVienDangKiKeHoachHocTapDTO
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
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDKhoaBoMon = s.MonHoc.IDKhoaBoMon,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TrangThai = false

                }).ToList();
                return lstkehoachHT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SinhVienDangKiKeHoachHocTapDTO> LayKeHoachHocTapCuaSinhVien_Moi(int idAccount, int idKhoaDT, int idHocKi)
        {
            try
            {
                var lstkehoachHT = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDAccount == idAccount && s.IDKhoaDaoTao == idKhoaDT && s.IDHocKi == idHocKi).Select(s => new SinhVienDangKiKeHoachHocTapDTO
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
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDKhoaBoMon = s.MonHoc.IDKhoaBoMon,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TrangThai = s.TrangThai
                }).ToList();

                return lstkehoachHT;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SinhVienDangKiKeHoachHocTapDTO> LayLichSuDangKiTheHocKi(int idAccount, int idHocKi)
        {
            try
            {
                var lstkehoachHT = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDAccount == idAccount && s.IDHocKi == idHocKi).Select(s => new SinhVienDangKiKeHoachHocTapDTO
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
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDKhoaBoMon = s.MonHoc.IDKhoaBoMon,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TrangThai = false
                }).ToList();

                return lstkehoachHT;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SinhVienDangKiKeHoachHocTapDTO> LayLichSuDangKiAll(int idAccount)
        {
            try
            {
                var lstkehoachHT = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDAccount == idAccount && s.TrangThai == true).Select(s => new SinhVienDangKiKeHoachHocTapDTO
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
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDKhoaBoMon = s.MonHoc.IDKhoaBoMon,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TrangThai = s.TrangThai
                }).ToList();

                return lstkehoachHT;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Them_KeHoachHocTapChoSV(List<SinhVienDangKiKeHoachHocTapDTO> list, int idAccount, int idLopHoc)
        {
            try
            {
                foreach (var item in list)
                {
                    var checkExist = Check_Exist(item, idAccount, idLopHoc);
                    if (checkExist == true)
                    {
                        var acc = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDAccount == idAccount && s.IDLopHoc == idLopHoc && s.IDKhoaDaoTao == item.IDKhoaDaoTao && s.IDHocKi == item.IDHocKi && s.IDMonHoc == item.IDMonHoc).FirstOrDefault();
                        if(item.TrangThai == false)
                        {
                            model.SinhVienDangKiKeHoachHocTaps.Remove(acc);
                        }
                        //if(item.TrangThai == false)
                        //{

                        //    Xoa_KeHoachHocTapChoSV(item.ID);
                        //    return false;
                        //}
                        //else
                        //{
                        //    var acc = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDAccount == idAccount && s.IDLopHoc == idLopHoc && s.IDKhoaDaoTao == item.IDKhoaDaoTao && s.IDHocKi == item.IDHocKi && s.IDMonHoc == item.IDMonHoc).FirstOrDefault();
                        //    acc.TrangThai = true;
                        //}                        
                    }
                    else
                    {
                        if(item.TrangThai == true)
                        {
                            var newItem = new SinhVienDangKiKeHoachHocTap();
                            newItem.IDMonHoc = item.IDMonHoc;
                            newItem.SoTinChi = item.SoTinChi;
                            newItem.SoTietLyThuyet = item.SoTietLyThuyet;
                            newItem.SoTietThucHanh = item.SoTietThucHanh;
                            newItem.IDAccount = idAccount;
                            newItem.IDHocKi = item.IDHocKi;
                            newItem.IDKhoaDaoTao = item.IDKhoaDaoTao;
                            newItem.IDLopHoc = idLopHoc;
                            newItem.TenMonHocTienQuyet = item.TenMonHocTienQuyet;
                            newItem.TenMonHocHocTruoc = item.TenMonHocHocTruoc;
                            newItem.TrangThai = item.TrangThai;
                            newItem.IDPhanLoaiMonHoc = item.IDPhanLoaiMonHoc;

                            model.SinhVienDangKiKeHoachHocTaps.Add(newItem);
                        }
                        

                    }
                }
                model.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Xoa_KeHoachHocTapChoSV(int id)
        {
            try
            {
                var acc = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.ID == id).FirstOrDefault();
                model.SinhVienDangKiKeHoachHocTaps.Remove(acc);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Check_Exist(SinhVienDangKiKeHoachHocTapDTO tontai, int idAccount, int idLopHoc)
        {
            try
            {
                var khht = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDAccount == idAccount && s.IDLopHoc == idLopHoc && s.IDKhoaDaoTao == tontai.IDKhoaDaoTao && s.IDHocKi == tontai.IDHocKi && s.IDMonHoc == tontai.IDMonHoc).Select(s => s.ID).FirstOrDefault();
                return khht > 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}