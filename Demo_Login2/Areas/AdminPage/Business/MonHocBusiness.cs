using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class MonHocBusiness : BaseBusiness
    {
        public MonHocDTO LayMonHoc(int id)
        {
            try
            {
                var monhoc = model.MonHocs.Where(s => s.ID == id).Select(s => new MonHocDTO {
                    ID = s.ID,
                    IDKhoaBoMon = s.IDKhoaBoMon,
                    MaMonHoc = s.MaMonHoc,
                    TenMonHoc = s.TenMonHoc,
                    //IDHocPhanTienQuyet = s.IDHocPhanTienQuyet,
                    //IDHocPhanHocTruoc = s.IDHocPhanHocTruoc,
                    //SoTiet = s.SoTiet,
                    SoTietLyThuyet = s.SoTietLyThuyet,
                    SoTietThucHanh = s.SoTietThucHanh,
                    SoTinChi = s.SoTinChi,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return monhoc;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<MonHocDTO> LayDanhSachMonHocTienQuyet(int id)
        {
            try
            {
                var listmonhoc = model.MonHocs.Where(s => s.ID != id).Select(s => new MonHocDTO
                {
                    ID = s.ID,
                    IDKhoaBoMon = s.IDKhoaBoMon,
                    MaMonHoc = s.MaMonHoc,
                    TenMonHoc = s.TenMonHoc,
                    SoTietLyThuyet = s.SoTietLyThuyet,
                    SoTietThucHanh = s.SoTietThucHanh,
                    SoTinChi = s.SoTinChi,
                    GhiChu = s.GhiChu
                }).ToList();
                return listmonhoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MonHocDTO> LayDanhSachMonHocHocTruoc(int id)
        {
            try
            {
                var listmonhoc = model.MonHocs.Where(s => s.ID != id).Select(s => new MonHocDTO
                {
                    ID = s.ID,
                    IDKhoaBoMon = s.IDKhoaBoMon,
                    MaMonHoc = s.MaMonHoc,
                    TenMonHoc = s.TenMonHoc,
                    SoTietLyThuyet = s.SoTietLyThuyet,
                    SoTietThucHanh = s.SoTietThucHanh,
                    SoTinChi = s.SoTinChi,
                    GhiChu = s.GhiChu
                }).ToList();
                return listmonhoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int LayMaMonHocDaTonTai(string mamonhoc)
        {
            try
            {
                return model.MonHocs.Where(s => s.MaMonHoc == mamonhoc).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int LayTenMonHocDaTonTai(string tenmonhoc)
        {
            try
            {
                return model.MonHocs.Where(s => s.TenMonHoc == tenmonhoc).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CheckLoiMonHocDaTonTai(int? id)
        {
            try
            {
                return model.MonHocKhoaDaoTaos.Where(s => s.IDMonHoc == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CheckLoiKetQuaHocTapDaTonTai(int? id)
        {
            try
            {
                return model.KetQuaHocTaps.Where(s => s.IDMonHoc == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CheckLoiKetQuaLapKeHoachDangKiSinhVien(int? id)
        {
            try
            {
                return model.MonHocSinhVienDangKis.Where(s => s.IDMonHoc == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int CheckLoiChuongTrinhDaoTao_Moi(int? id)
        {
            try
            {
                return model.ChuongTrinhDaoTao_Mois.Where(s => s.IDMonHoc == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int CheckLoiKeHoachHocTap_Moi(int? id)
        {
            try
            {
                return model.KeHoachHocTap_Mois.Where(s => s.IDMonHoc == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int CheckLoiSinhVienDangKiKeHoachHocTap_Moi(int? id)
        {
            try
            {
                return model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDMonHoc == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MonHocDTO> LayDanhSachMonHoc()
        {
            try
            {
                var listmonhoc = model.MonHocs.Select(s => new MonHocDTO
                {
                    ID = s.ID,
                    IDKhoaBoMon = s.IDKhoaBoMon,
                    MaMonHoc = s.MaMonHoc,
                    TenMonHoc = s.TenMonHoc,
                    //IDHocPhanTienQuyet = s.IDHocPhanTienQuyet,
                    //IDHocPhanHocTruoc = s.IDHocPhanHocTruoc,
                    //SoTiet = s.SoTiet,
                    SoTietLyThuyet = s.SoTietLyThuyet,
                    SoTietThucHanh = s.SoTietThucHanh,
                    SoTinChi = s.SoTinChi,
                    GhiChu = s.GhiChu
                }).ToList();
                return listmonhoc;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<MonHocDTO> LayDanhSachMonHocDeChon(int id)
        {
            try
            {
                var lstmonhoc = model.MonHocs.Where(s => s.ID == id).Select(s => new MonHocDTO
                {
                    ID = s.ID,
                    IDKhoaBoMon = s.IDKhoaBoMon,
                    MaMonHoc = s.MaMonHoc,
                    TenMonHoc = s.TenMonHoc,
                    SoTietLyThuyet = s.SoTietLyThuyet,
                    SoTietThucHanh = s.SoTietThucHanh,
                    SoTinChi = s.SoTinChi,
                    GhiChu = s.GhiChu
                }).ToList();
                return lstmonhoc;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public bool ThemMonHoc(MonHocDTO monhoc)
        {
            try
            {
                var newmonhoc = new MonHoc();
                newmonhoc.ID = monhoc.ID;
                newmonhoc.IDKhoaBoMon = monhoc.IDKhoaBoMon;
                newmonhoc.MaMonHoc = monhoc.MaMonHoc;
                newmonhoc.TenMonHoc = monhoc.TenMonHoc;
                //newmonhoc.IDHocPhanTienQuyet = monhoc.IDHocPhanTienQuyet;
                //newmonhoc.IDHocPhanHocTruoc = monhoc.IDHocPhanHocTruoc;
                //newmonhoc.SoTiet = monhoc.SoTiet;
                newmonhoc.SoTietLyThuyet = monhoc.SoTietLyThuyet;
                newmonhoc.SoTietThucHanh = monhoc.SoTietThucHanh;
                newmonhoc.SoTinChi = monhoc.SoTinChi;
                newmonhoc.GhiChu = monhoc.GhiChu;

                model.MonHocs.Add(newmonhoc);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool XoaMonHoc(int id)
        {
            try
            {
                var monhoc = model.MonHocs.Where(s => s.ID == id).FirstOrDefault();
                model.MonHocs.Remove(monhoc);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool SuaMonHoc(MonHocDTO monhoc)
        {
            try
            {
                var monhocs = model.MonHocs.Where(s => s.ID == monhoc.ID).FirstOrDefault();
                monhocs.ID = monhoc.ID;
                monhocs.IDKhoaBoMon = monhoc.IDKhoaBoMon;
                monhocs.MaMonHoc = monhoc.MaMonHoc;
                monhocs.TenMonHoc = monhoc.TenMonHoc;
                //monhocs.IDHocPhanTienQuyet = monhoc.IDHocPhanTienQuyet;
                //monhocs.IDHocPhanHocTruoc = monhoc.IDHocPhanHocTruoc;
                //monhocs.SoTiet = monhoc.SoTiet;
                monhocs.SoTietLyThuyet = monhoc.SoTietLyThuyet;
                monhocs.SoTietThucHanh = monhoc.SoTietThucHanh;
                monhocs.SoTinChi = monhoc.SoTinChi;
                monhocs.GhiChu = monhoc.GhiChu;

                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public int CheckLoiMonHocTienQuyetTheoMonHocDaTonTai(int? idMonHoc,int? idMonHocTienQuyet)
        {
            try
            {
                return model.HocPhanTienQuyets.Where(s => s.IDMonHoc == idMonHoc && s.IDMonHocTienQuyet == idMonHocTienQuyet).Select(s => s.ID).FirstOrDefault();
            }catch(Exception)
            {
                throw;
            }
        }

        public int CheckLoiMonHocHocTruocTheoMonHocDaTonTai(int? idMonHoc, int? idMonHocHocTruoc)
        {
            try
            {
                return model.HocPhanHocTruocs.Where(s => s.IDMonHoc == idMonHoc && s.IDMonHocHocTruoc == idMonHocHocTruoc).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CheckLoiMonHocTheoKhoaDaoTaoDaTonTai(int? idMonHoc,int? idKhoaDaoTao)
        {
            try
            {
                return model.MonHocKhoaDaoTaos.Where(s => s.IDMonHoc == idMonHoc && s.IDKhoaDaoTao == idKhoaDaoTao).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MonHocSinhVienDangKiDTO LayMonHocVuotTheoMa(string ma)
        {
            try
            {
                var monhocvuot = model.MonHocs.Where(s => s.MaMonHoc == ma).Select(s => new MonHocSinhVienDangKiDTO
                {
                    ID = s.ID,
                    MaMonHoc = s.MaMonHoc,
                    TenMonHoc = s.TenMonHoc,
                    SoTinChi = s.SoTinChi,
                    SoTietLyThuyet = s.SoTietLyThuyet,
                    SoTietThucHanh = s.SoTietThucHanh,
                    IDKhoaBoMon = s.IDKhoaBoMon,
                    TenKhoaBoMon = s.KhoaBoMon.TenKhoaBoMon,

                    //Error
                    IDHocPhanTienQuyet = s.HocPhanTienQuyets_IDMonHoc.FirstOrDefault().ID,
                    IDMonHocTienQuyet = s.HocPhanTienQuyets_IDMonHoc.FirstOrDefault().MonHocTienQuyet.ID,
                    TenMonHocTienQuyet = s.HocPhanTienQuyets_IDMonHoc.FirstOrDefault().MonHocTienQuyet.TenMonHoc,

                    IDHocPhanHocTruoc = s.HocPhanHocTruocs_IDMonHoc.FirstOrDefault().ID,
                    IDMonHocHocTruoc = s.HocPhanHocTruocs_IDMonHoc.FirstOrDefault().MonHocHocTruoc.ID,
                    TenMonHocHocTruoc = s.HocPhanHocTruocs_IDMonHoc.FirstOrDefault().MonHocHocTruoc.TenMonHoc,
                }).FirstOrDefault();
                return monhocvuot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int LayIDKhoaBoMonTheoTen(string tenkhoabm)
        {
            try
            {
                return model.KhoaBoMons.Where(s => s.TenKhoaBoMon == tenkhoabm).Select(s => s.ID).FirstOrDefault();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        //public MonHocSinhVienDangKiDTO LayMonHocVuotTheoMa(string ma)
        //{
        //    try
        //    {
        //        var monhocvuot = model.MonHocSinhVienDangKis.Where(s => s.MonHoc.MaMonHoc == ma).Select(s => new MonHocSinhVienDangKiDTO
        //        {
        //            ID = s.ID,
        //            MaMonHoc = s.MonHoc.MaMonHoc,
        //            TenMonHoc = s.MonHoc.TenMonHoc,
        //            SoTinChi = s.MonHoc.SoTinChi,
        //            SoTietLyThuyet = s.MonHoc.SoTietLyThuyet,
        //            SoTietThucHanh = s.MonHoc.SoTietThucHanh,
        //            IDKhoaBoMon = s.MonHoc.IDKhoaBoMon,
        //            TenKhoaBoMon = s.MonHoc.KhoaBoMon.TenKhoaBoMon,

        //            //Error
        //            IDHocPhanTienQuyet = s.HocPhanTienQuyet.ID,
        //            IDMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.ID,
        //            TenMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.TenMonHoc,

        //            IDHocPhanHocTruoc = s.HocPhanHocTruoc.ID,
        //            IDMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.ID,
        //            TenMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.TenMonHoc,
        //        }).FirstOrDefault();
        //        return monhocvuot;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}