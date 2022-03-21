using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class PhanLoaiHocKiBusiness : BaseBusiness
    {
        public PhanLoaiHocKiDTO LayPhanLoaiHocKi(int id)
        {
            try
            {
                var phanloaihocki = model.PhanLoaiHocKis.Where(s => s.ID == id).Select(s => new PhanLoaiHocKiDTO
                {
                    ID = s.ID,
                    LoaiHocKi = s.LoaiHocKi,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return phanloaihocki;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int LayPhanLoaiHocKiDaTonTai(string loaihocki)
        {
            try
            {
                return model.PhanLoaiHocKis.Where(s => s.LoaiHocKi == loaihocki).Select(s => s.ID).FirstOrDefault();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CheckLoiPhanLoaiHocKiDaTonTai(int? id)
        {
            try
            {
                return model.HocKies.Where(s => s.IDPhanLoaiHocKi == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<PhanLoaiHocKiDTO> LayDanhSachPhanLoaiHocKi()
        {
            try
            {
                var listphanloaihocki = model.PhanLoaiHocKis.Select(s => new PhanLoaiHocKiDTO
                {
                    ID = s.ID,
                    LoaiHocKi = s.LoaiHocKi,
                    GhiChu = s.GhiChu
                }).ToList();
                return listphanloaihocki;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ThemPhanLoaiHocKi(PhanLoaiHocKiDTO phanloaihocki)
        {
            try
            {
                var newphanloaihocki = new PhanLoaiHocKi();
                newphanloaihocki.ID = phanloaihocki.ID;
                newphanloaihocki.LoaiHocKi = phanloaihocki.LoaiHocKi;
                newphanloaihocki.GhiChu = phanloaihocki.GhiChu;

                model.PhanLoaiHocKis.Add(newphanloaihocki);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool XoaPhanLoaiHocKi(int id)
        {
            try
            {
                var phanloaihocki = model.PhanLoaiHocKis.Where(s => s.ID == id).FirstOrDefault();
                model.PhanLoaiHocKis.Remove(phanloaihocki);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SuaPhanLoaiHocKi(PhanLoaiHocKiDTO phanloaihocki)
        {
            try
            {
                var phanloaihockies = model.PhanLoaiHocKis.Where(s => s.ID == phanloaihocki.ID).FirstOrDefault();
                phanloaihockies.ID = phanloaihocki.ID;
                phanloaihockies.LoaiHocKi = phanloaihocki.LoaiHocKi;
                phanloaihockies.GhiChu = phanloaihocki.GhiChu;
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