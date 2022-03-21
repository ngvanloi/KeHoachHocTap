using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class ChuongTrinhDaoTao_MoiBusiness : BaseBusiness
    {
        public List<ChuongTrinhDaoTao_MoiDTO> LayDanhSachChuongTrinhDaoTao_Moi_TheoKhoaDaoTao(int id)
        {
            try
            {
                var lstctrdaotao = model.ChuongTrinhDaoTao_Mois.Where(s => s.IDKhoaDaoTao == id).Select(s => new ChuongTrinhDaoTao_MoiDTO
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
                return lstctrdaotao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChuongTrinhDaoTao_MoiDTO> LayDanhSachChuongTrinhDaoTao_Moi()
        {
            try
            {
                var lstctrdaotao = model.ChuongTrinhDaoTao_Mois.Select(s => new ChuongTrinhDaoTao_MoiDTO
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
                return lstctrdaotao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChuongTrinhDaoTao_MoiDTO LayMonHocTrongChuongTrinhDaoTao_Moi_TheoMa(string ma)
        {
            try
            {
                var lstctrdaotao = model.ChuongTrinhDaoTao_Mois.Where(s => s.MonHoc.MaMonHoc == ma).Select(s => new ChuongTrinhDaoTao_MoiDTO
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
                }).FirstOrDefault();
                return lstctrdaotao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int LayIDMonHocTheoMaMonHocVaSoTinChi(string mamonhoc, int sotinchi)
        {
            try
            {
                return model.MonHocs.Where(s => s.MaMonHoc == mamonhoc && s.SoTinChi == sotinchi).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int LayIDMonHocTheoTenMonHoc(string tenmonhoc)
        {
            try
            {
                return model.MonHocs.Where(s => s.TenMonHoc == tenmonhoc).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ThemChuongTrinhDaoTao_Moi(ChuongTrinhDaoTao_MoiDTO lstctrdaotao)
        {
            try
            {
                var newItem = new ChuongTrinhDaoTao_Moi();
                newItem.ID = lstctrdaotao.ID;
                newItem.IDMonHoc = lstctrdaotao.IDMonHoc;
                newItem.IDKhoaDaoTao = lstctrdaotao.IDKhoaDaoTao;
                newItem.IDHocKi = lstctrdaotao.IDHocKi;
                newItem.SoTinChi = lstctrdaotao.SoTinChi;
                newItem.SoTietLyThuyet = lstctrdaotao.SoTietLyThuyet;
                newItem.SoTietThucHanh = lstctrdaotao.SoTietThucHanh;
                newItem.IDPhanLoaiMonHoc = lstctrdaotao.IDPhanLoaiMonHoc;
                newItem.TenMonHocHocTruoc = lstctrdaotao.TenMonHocHocTruoc;
                newItem.TenMonHocTienQuyet = lstctrdaotao.TenMonHocTienQuyet;

                model.ChuongTrinhDaoTao_Mois.Add(newItem);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChuongTrinhDaoTao_MoiDTO> LayDanhSachKeHoachHocTap_Moi_TheoChuongTrinhDaoTao(int idKhoaDT, int idHocKi)
        {
            try
            {
                var lstkehoachHT = model.ChuongTrinhDaoTao_Mois.Where(s => s.IDKhoaDaoTao == idKhoaDT && s.IDHocKi == idHocKi).Select(s => new ChuongTrinhDaoTao_MoiDTO
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool LayDanhSachChuongTrinhDaoTaoDaTonTai(int id)
        {
            try
            {
                var lstctrdaotao = model.ChuongTrinhDaoTao_Mois.Where(s => s.IDKhoaDaoTao == id).Select(s => s.ID).FirstOrDefault();
                if (lstctrdaotao > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int LayIDPhanLoaiMonHoc(string phanLoai)
        {
            try
            {
                var lstctrdaotao = model.PhanLoaiMonHocs.Where(s => s.LoaiMonHoc == phanLoai).Select(s => s.ID).FirstOrDefault();

                return lstctrdaotao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int LayIDHocPhanTienQuyet(string hocphanHT)
        {
            try
            {
                var lstctrdaotao = model.MonHocs.Where(s => s.TenMonHoc == hocphanHT).Select(s => s.ID).FirstOrDefault();

                return lstctrdaotao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int LayIDHocPhanHocTruoc(string hocphanHT)
        {
            try
            {
                var lstctrdaotao = model.MonHocs.Where(s => s.TenMonHoc == hocphanHT).Select(s => s.ID).FirstOrDefault();

                return lstctrdaotao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool LayIDHocKi(int id)
        {
            try
            {
                var lstctrdaotao = model.HocKies.Where(s => s.ID == id).FirstOrDefault();              
                return lstctrdaotao != null ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Xoa_ChuongTrinhDaoTao_Moi(int idKhoaDT)
        {
            try
            {
                var lstctrdaotao = model.ChuongTrinhDaoTao_Mois.Where(s => s.IDKhoaDaoTao == idKhoaDT).ToList();
                foreach(var item in lstctrdaotao)
                {
                    model.ChuongTrinhDaoTao_Mois.Remove(item);
                }
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Check_XoaKhoaDaoTaoKhiKhongCoDuLieu(int idKhoaDT)
        {
            try
            {
                var lst = model.ChuongTrinhDaoTao_Mois.Where(s => s.IDKhoaDaoTao == idKhoaDT).FirstOrDefault();
                if(lst == null)
                {
                    return true;
                }
                return false;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}