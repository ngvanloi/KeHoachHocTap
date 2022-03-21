using Demo_Login2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.GiangVienPage.Business
{
    public class ChuongTrinhDaoTaoBusiness : BaseBusiness
    {
        public List<ChuongTrinhDaoTaoDTO> LayDanhSachChuongTrinhDaoTao()
        {
            try
            {
                var lstctrdaotao = model.MonHocKhoaDaoTaos.Select(s => new ChuongTrinhDaoTaoDTO {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    Ma_MonHoc = s.MonHoc.MaMonHoc,
                    Ten_MonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi_MonHoc = s.MonHoc.SoTinChi,
                    SoTietLyThuyet_MonHoc = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh_MonHoc = s.MonHoc.SoTietThucHanh,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    Ten_KhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    IDHocKi = s.IDHocKi,
                    Ten_HocKi = s.HocKi.TenHocKi,
                    IDHocPhanTienQuyet = s.IDHocPhanTienQuyet,
                    IDMonHocTienQuyet = s.HocPhanTienQuyet.IDMonHocTienQuyet,
                    IDHocPhanHocTruoc = s.IDHocPhanHocTruoc,
                    IDMonHocHocTruoc = s.HocPhanHocTruoc.IDMonHocHocTruoc,
                    IDKhoaBoMon = s.MonHoc.IDKhoaBoMon,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,
                    IDPhanLoaiMonHoc = s.PhanLoaiMonHoc.ID,
                    LoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                    TenMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.TenMonHoc,
                    TenMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.TenMonHoc
                }).ToList();
                return lstctrdaotao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChuongTrinhDaoTaoDTO> LayDanhSachChuongTrinhDaoTaoTheoKhoa(int id)
        {
            try
            {
                if (id == 0)
                {
                    var lstctrdaotao = model.MonHocKhoaDaoTaos.Select(s => new ChuongTrinhDaoTaoDTO
                    {
                        ID = s.ID,
                        IDMonHoc = s.IDMonHoc,
                        Ma_MonHoc = s.MonHoc.MaMonHoc,
                        Ten_MonHoc = s.MonHoc.TenMonHoc,
                        SoTinChi_MonHoc = s.MonHoc.SoTinChi,
                        SoTietLyThuyet_MonHoc = s.MonHoc.SoTietLyThuyet,
                        SoTietThucHanh_MonHoc = s.MonHoc.SoTietThucHanh,
                        IDKhoaDaoTao = s.IDKhoaDaoTao,
                        Ten_KhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                        IDHocKi = s.IDHocKi,
                        Ten_HocKi = s.HocKi.TenHocKi,
                        IDHocPhanTienQuyet = s.IDHocPhanTienQuyet,
                        IDMonHocTienQuyet = s.HocPhanTienQuyet.IDMonHocTienQuyet,
                        IDHocPhanHocTruoc = s.IDHocPhanHocTruoc,
                        IDMonHocHocTruoc = s.HocPhanHocTruoc.IDMonHocHocTruoc,
                        IDKhoaBoMon = s.MonHoc.IDKhoaBoMon,
                        TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,
                        IDPhanLoaiMonHoc = s.PhanLoaiMonHoc.ID,
                        LoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                        TenMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.TenMonHoc,
                        TenMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.TenMonHoc
                    }).ToList();
                    return lstctrdaotao;
                }
                else
                {
                    var lstctrdaotao = model.MonHocKhoaDaoTaos.Where(s => s.IDKhoaDaoTao == id).Select(s => new ChuongTrinhDaoTaoDTO
                    {
                        ID = s.ID,
                        IDMonHoc = s.IDMonHoc,
                        Ma_MonHoc = s.MonHoc.MaMonHoc,
                        Ten_MonHoc = s.MonHoc.TenMonHoc,
                        SoTinChi_MonHoc = s.MonHoc.SoTinChi,
                        SoTietLyThuyet_MonHoc = s.MonHoc.SoTietLyThuyet,
                        SoTietThucHanh_MonHoc = s.MonHoc.SoTietThucHanh,
                        IDKhoaDaoTao = s.IDKhoaDaoTao,
                        Ten_KhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                        IDHocKi = s.IDHocKi,
                        Ten_HocKi = s.HocKi.TenHocKi,
                        IDHocPhanTienQuyet = s.IDHocPhanTienQuyet,
                        IDMonHocTienQuyet = s.HocPhanTienQuyet.IDMonHocTienQuyet,
                        IDHocPhanHocTruoc = s.IDHocPhanHocTruoc,
                        IDMonHocHocTruoc = s.HocPhanHocTruoc.IDMonHocHocTruoc,
                        IDKhoaBoMon = s.MonHoc.IDKhoaBoMon,
                        TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,
                        IDPhanLoaiMonHoc = s.PhanLoaiMonHoc.ID,
                        LoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                        TenMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.TenMonHoc,
                        TenMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.TenMonHoc
                    }).ToList();
                    return lstctrdaotao;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,

                    IDHocKi = s.IDHocKi,
                    TenHocKi = s.HocKi.TenHocKi,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    LoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,

                    //IDHocPhanTienQuyet = s.IDHocPhanTienQuyet,
                    //TenMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.TenMonHoc,

                    //IDHocPhanHocTruoc = s.IDHocPhanHocTruoc,
                    //TenMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.TenMonHoc,

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


    }
}