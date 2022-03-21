using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class KetQuaHocTapBusiness : BaseBusiness
    {
        public KetQuaHocTapDTO LayKetQuaHocTap(int id)
        {
            try
            {
                var ketqua = model.KetQuaHocTaps.Where(s => s.ID == id).Select(s => new KetQuaHocTapDTO
                {
                    ID = s.ID,
                    IDAccount = s.IDAccount,
                    IDMonHoc = s.IDMonHoc,
                    SoTinChi = s.SoTinChi,
                    Diem = s.Diem,
                    DiemChu = s.DiemChu,
                    KetQua = s.KetQua,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return ketqua;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public List<KetQuaHocTapDTO> LayDanhSachKetQuaHocTap()
        {
            try
            {
                var listketqua = model.KetQuaHocTaps.Select(s => new KetQuaHocTapDTO
                {
                    ID = s.ID,
                    IDAccount = s.IDAccount,
                    IDMonHoc = s.IDMonHoc,
                    SoTinChi = s.SoTinChi,
                    Diem = s.Diem,
                    DiemChu = s.DiemChu,
                    KetQua = s.KetQua,
                    GhiChu = s.GhiChu
                }).ToList();
                return listketqua;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool ThemKetQuaHocTap(KetQuaHocTapDTO ketqua)
        {
            try
            {
                var newketqua = new KetQuaHocTap();
                newketqua.ID = ketqua.ID;
                newketqua.IDAccount = ketqua.IDAccount;
                newketqua.IDMonHoc = ketqua.IDMonHoc;
                newketqua.SoTinChi = ketqua.SoTinChi;
                newketqua.Diem = ketqua.Diem;
                newketqua.DiemChu = ketqua.DiemChu;
                newketqua.KetQua = ketqua.KetQua;
                newketqua.GhiChu = ketqua.GhiChu;

                model.KetQuaHocTaps.Add(newketqua);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool XoaKetQuaHocTap(int id)
        {
            try
            {
                var ketqua = model.KetQuaHocTaps.Where(s => s.ID == id).FirstOrDefault();
                model.KetQuaHocTaps.Remove(ketqua);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SuaKetQuaHocTap(KetQuaHocTapDTO ketqua)
        {
            try
            {
                var ketquas = model.KetQuaHocTaps.Where(s => s.ID == ketqua.ID).FirstOrDefault();
                ketquas.ID = ketqua.ID;
                ketquas.IDAccount = ketqua.IDAccount;
                ketquas.IDMonHoc = ketqua.IDMonHoc;
                ketquas.SoTinChi = ketqua.SoTinChi;
                ketquas.Diem = ketqua.Diem;
                ketquas.DiemChu = ketqua.DiemChu;
                ketquas.KetQua = ketqua.KetQua;
                ketquas.GhiChu = ketqua.GhiChu;

                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        


    }
}