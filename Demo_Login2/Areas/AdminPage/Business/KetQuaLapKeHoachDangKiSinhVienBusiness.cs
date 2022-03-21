using Demo_Login2.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class KetQuaLapKeHoachDangKiSinhVienBusiness : BaseBusiness
    {
        public List<SinhVienDangKiKeHoachHocTapDTO> LayDanhSachKetQuaLapKeHoachDangKiSinhVien(int idKhoaDT, int idHocKi, int idSinhVien)
        {
            try
            {
                List<SinhVienDangKiKeHoachHocTapDTO> listketqua = new List<SinhVienDangKiKeHoachHocTapDTO>();
                if (idKhoaDT > 0 && idHocKi > 0 && idSinhVien > 0)
                {
                    listketqua = Lay_KetQuaLapKeHoachDangKiSinhVien_KhoaDT_HocKi_SinhVien(idKhoaDT, idHocKi, idSinhVien);
                }else if(idKhoaDT > 0 && idHocKi > 0 && idSinhVien == 0)
                {
                    listketqua = Lay_KetQuaLapKeHoachDangKiSinhVien_KhoaDT_HocKi(idKhoaDT, idHocKi);
                }else if(idKhoaDT > 0 && idHocKi == 0 && idSinhVien > 0)
                {
                    listketqua = Lay_KetQuaLapKeHoachDangKiSinhVien_KhoaDT_SinhVien(idKhoaDT, idSinhVien);
                }else if(idKhoaDT == 0 && idHocKi > 0 && idSinhVien > 0)
                {
                    listketqua = Lay_KetQuaLapKeHoachDangKiSinhVien_HocKi_SinhVien(idHocKi, idSinhVien);
                }else if(idKhoaDT > 0 && idHocKi == 0 && idSinhVien == 0)
                {
                    listketqua = Lay_KetQuaLapKeHoachDangKiSinhVien_KhoaDT(idKhoaDT);
                }else if(idKhoaDT == 0 && idHocKi > 0 && idSinhVien == 0)
                {
                    listketqua = Lay_KetQuaLapKeHoachDangKiSinhVien_HocKi(idHocKi);
                }else if (idKhoaDT == 0 && idHocKi == 0 && idSinhVien > 0)
                {
                    listketqua = Lay_KetQuaLapKeHoachDangKiSinhVien_SinhVien(idSinhVien);
                }

                return listketqua;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> LayDanhSachKetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem(int idLopHoc,int idHocKi,int idSinhVien)
        {
            try
            {
                List<SinhVienDangKiKeHoachHocTapDTO> listketqua = new List<SinhVienDangKiKeHoachHocTapDTO>();
                if(idHocKi > 0 && idSinhVien > 0)
                {
                    listketqua = Lay_KetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem_HocKi_SinhVien(idLopHoc,idHocKi, idSinhVien);
                }else if(idHocKi > 0 && idSinhVien == 0)
                {
                    listketqua = Lay_KetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem_HocKi(idLopHoc, idHocKi);
                }else if(idHocKi == 0 && idSinhVien > 0)
                {
                    listketqua = Lay_KetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem_SinhVien(idLopHoc, idSinhVien);
                }else if(idHocKi == 0 && idSinhVien == 0)
                {
                    listketqua = Lay_KetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem_LopHoc(idLopHoc);
                }

                return listketqua;

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        //Sinh Vien
        public List<SinhVienDangKiKeHoachHocTapDTO> Lay_KetQuaLapKeHoachDangKiSinhVien_All()
        {
            try
            {
                var ketqua = model.SinhVienDangKiKeHoachHocTaps.Select(s => new SinhVienDangKiKeHoachHocTapDTO {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,

                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,

                    TenSinhVien = s.Account.HoVaTen,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    TenLop = s.LopHoc.TenLop,
                    TenHocKi = s.HocKi.TenHocKi
                }).ToList();
                return ketqua;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<SinhVienDangKiKeHoachHocTapDTO> Lay_KetQuaLapKeHoachDangKiSinhVien_KhoaDT_HocKi_SinhVien(int idKhoaDT, int idHocKi, int idSinhVien)
        {
            try
            {
                var ketqua = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDHocKi == idHocKi && s.IDAccount == idSinhVien && s.IDKhoaDaoTao == idKhoaDT).Select(s => new SinhVienDangKiKeHoachHocTapDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,

                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,

                    TenSinhVien = s.Account.HoVaTen,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    TenLop = s.LopHoc.TenLop,
                    TenHocKi = s.HocKi.TenHocKi
                }).ToList();
                return ketqua;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> Lay_KetQuaLapKeHoachDangKiSinhVien_KhoaDT_HocKi(int idKhoaDT,int idHocKi)
        {
            try
            {
                var ketqua = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDHocKi == idHocKi && s.IDKhoaDaoTao == idKhoaDT).Select(s => new SinhVienDangKiKeHoachHocTapDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,

                    TenSinhVien = s.Account.HoVaTen,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    TenLop = s.LopHoc.TenLop,
                    TenHocKi = s.HocKi.TenHocKi
                }).ToList();
                return ketqua;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> Lay_KetQuaLapKeHoachDangKiSinhVien_KhoaDT_SinhVien(int idKhoaDT, int idSinhVien)
        {
            try
            {
                var ketqua = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDAccount == idSinhVien && s.IDKhoaDaoTao == idKhoaDT).Select(s => new SinhVienDangKiKeHoachHocTapDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,

                    TenSinhVien = s.Account.HoVaTen,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    TenLop = s.LopHoc.TenLop,
                    TenHocKi = s.HocKi.TenHocKi
                }).ToList();
                return ketqua;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> Lay_KetQuaLapKeHoachDangKiSinhVien_HocKi_SinhVien(int idHocKi, int idSinhVien)
        {
            try
            {
                var ketqua = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDHocKi == idHocKi && s.IDAccount == idSinhVien).Select(s => new SinhVienDangKiKeHoachHocTapDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    TenSinhVien = s.Account.HoVaTen,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    TenLop = s.LopHoc.TenLop,
                    TenHocKi = s.HocKi.TenHocKi
                }).ToList();
                return ketqua;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> Lay_KetQuaLapKeHoachDangKiSinhVien_KhoaDT(int idKhoaDT)
        {
            try
            {
                var ketqua = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDKhoaDaoTao == idKhoaDT).Select(s => new SinhVienDangKiKeHoachHocTapDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    TenSinhVien = s.Account.HoVaTen,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    TenLop = s.LopHoc.TenLop,
                    TenHocKi = s.HocKi.TenHocKi
                }).ToList();
                return ketqua;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> Lay_KetQuaLapKeHoachDangKiSinhVien_HocKi(int idHocKi)
        {
            try
            {
                var ketqua = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDHocKi == idHocKi).Select(s => new SinhVienDangKiKeHoachHocTapDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,

                    TenSinhVien = s.Account.HoVaTen,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    TenLop = s.LopHoc.TenLop,
                    TenHocKi = s.HocKi.TenHocKi
                }).ToList();
                return ketqua;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> Lay_KetQuaLapKeHoachDangKiSinhVien_SinhVien(int idSinhVien)
        {
            try
            {
                var ketqua = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDAccount == idSinhVien).Select(s => new SinhVienDangKiKeHoachHocTapDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    TenSinhVien = s.Account.HoVaTen,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    TenLop = s.LopHoc.TenLop,
                    TenHocKi = s.HocKi.TenHocKi
                }).ToList();
                return ketqua;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Giang Vien
        public List<SinhVienDangKiKeHoachHocTapDTO> Lay_KetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem_HocKi_SinhVien(int idLopHoc,int idHocKi,int idSinhVien)
        {
            try
            {
                var ketqua = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDHocKi == idHocKi && s.IDAccount == idSinhVien && s.IDLopHoc == idLopHoc).Select(s => new SinhVienDangKiKeHoachHocTapDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    TenSinhVien = s.Account.HoVaTen,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    TenLop = s.LopHoc.TenLop,
                    TenHocKi = s.HocKi.TenHocKi
                }).ToList();
                return ketqua;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> Lay_KetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem_HocKi(int idLopHoc, int idHocKi)
        {
            try
            {
                var ketqua = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDHocKi == idHocKi && s.IDLopHoc == idLopHoc).Select(s => new SinhVienDangKiKeHoachHocTapDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    TenSinhVien = s.Account.HoVaTen,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    TenLop = s.LopHoc.TenLop,
                    TenHocKi = s.HocKi.TenHocKi
                }).ToList();
                return ketqua;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> Lay_KetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem_SinhVien(int idLopHoc, int idSinhVien)
        {
            try
            {
                var ketqua = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDAccount == idSinhVien && s.IDLopHoc == idLopHoc).Select(s => new SinhVienDangKiKeHoachHocTapDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    TenSinhVien = s.Account.HoVaTen,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    TenLop = s.LopHoc.TenLop,
                    TenHocKi = s.HocKi.TenHocKi
                }).ToList();
                return ketqua;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SinhVienDangKiKeHoachHocTapDTO> Lay_KetQuaLapKeHoachDangKiSinhVien_TheoLopChuNhiem_LopHoc(int idLopHoc)
        {
            try
            {
                var ketqua = model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDLopHoc == idLopHoc).Select(s => new SinhVienDangKiKeHoachHocTapDTO
                {
                    IDMonHoc = s.IDMonHoc,
                    MaMonHoc = s.MonHoc.MaMonHoc,
                    TenMonHoc = s.MonHoc.TenMonHoc,
                    SoTinChi = s.MonHoc.SoTinChi,
                    SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
                    SoTietThucHanh = s.MonHoc.SoTietThucHanh,
                    IDKhoaBoMon = s.MonHoc.KhoaBoMon.ID,
                    TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

                    TenMonHocTienQuyet = s.TenMonHocTienQuyet,
                    TenMonHocHocTruoc = s.TenMonHocHocTruoc,

                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenPhanLoaiMonHoc = s.PhanLoaiMonHoc.LoaiMonHoc,
                    TrangThai = s.TrangThai,
                    IDHocKi = s.IDHocKi,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    TenSinhVien = s.Account.HoVaTen,
                    TenKhoaDaoTao = s.KhoaDaoTao.TenKhoaDaoTao,
                    TenLop = s.LopHoc.TenLop,
                    TenHocKi = s.HocKi.TenHocKi
                }).ToList();
                return ketqua;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}