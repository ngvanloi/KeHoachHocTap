using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class HocKiBusiness : BaseBusiness
    {
        public HocKiDTO LayHocKi(int id)
        {
            try
            {
                var hocki = model.HocKies.Where(s => s.ID == id).Select(s => new HocKiDTO
                {
                    ID = s.ID,
                    TenHocKi = s.TenHocKi,
                    IDPhanLoaiHocKi = s.IDPhanLoaiHocKi,
                    ThangBatDau = s.ThangBatDau,
                    ThangKetThuc = s.ThangKetThuc,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return hocki;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int LayHocKiDaTonTai(string tenhocki)
        {
            try
            {
                return model.HocKies.Where(s => s.TenHocKi == tenhocki).Select(s => s.ID).FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CheckLoiHocKiDaTonTai_MonHocKhoaDaoTao(int? id)
        {
            try
            {
                return model.MonHocKhoaDaoTaos.Where(s => s.IDHocKi == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int CheckLoiHocKiDaTonTai_TrangThaiDangKiMon(int? id)
        {
            try
            {
                return model.TrangThaiDangKiMonHocs.Where(s => s.IDHocKi == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int CheckLoiHocKiDaTonTai_ChuongTrinhDaoTao_Moi(int? id)
        {
            try
            {
                return model.ChuongTrinhDaoTao_Mois.Where(s => s.IDHocKi == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CheckLoiHocKiDaTonTai_KeHoachHocTap_Moi(int? id)
        {
            try
            {
                return model.KeHoachHocTap_Mois.Where(s => s.IDHocKi == id).Select(s => s.ID).FirstOrDefault();
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
                return model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDHocKi == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<HocKiDTO> LayDanhSachHocKi()
        {
            try
            {
                var listhocki = model.HocKies.Select(s => new HocKiDTO
                {
                    ID = s.ID,
                    TenHocKi = s.TenHocKi,
                    IDPhanLoaiHocKi = s.IDPhanLoaiHocKi,
                    ThangBatDau = s.ThangBatDau,
                    ThangKetThuc = s.ThangKetThuc,
                    GhiChu = s.GhiChu
                }).ToList();
                return listhocki;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ThemHocKi(HocKiDTO hocki)
        {
            try
            {
                var newhocki = new HocKi();
                newhocki.ID = hocki.ID;
                newhocki.TenHocKi = hocki.TenHocKi;
                newhocki.IDPhanLoaiHocKi = hocki.IDPhanLoaiHocKi;
                newhocki.ThangBatDau = hocki.ThangBatDau;
                newhocki.ThangKetThuc = hocki.ThangKetThuc;
                newhocki.GhiChu = hocki.GhiChu;

                model.HocKies.Add(newhocki);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool XoaHocKi(int id)
        {
            try
            {
                var hocki = model.HocKies.Where(s => s.ID == id).FirstOrDefault();
                model.HocKies.Remove(hocki);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool SuaHocKi(HocKiDTO hocki)
        {
            try
            {
                var hockies = model.HocKies.Where(s => s.ID == hocki.ID).FirstOrDefault();
                hockies.ID = hocki.ID;
                hockies.TenHocKi = hocki.TenHocKi;
                hockies.IDPhanLoaiHocKi = hocki.IDPhanLoaiHocKi;
                hockies.ThangBatDau = hocki.ThangBatDau;
                hockies.ThangKetThuc = hocki.ThangKetThuc;
                hockies.GhiChu = hocki.GhiChu;

                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public string LayTenHocKi(int idHocKi)
        {
            try
            {
                return model.HocKies.Where(s => s.ID == idHocKi).Select(s => s.TenHocKi).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}