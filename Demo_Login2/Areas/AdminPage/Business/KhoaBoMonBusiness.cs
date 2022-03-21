using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class KhoaBoMonBusiness : BaseBusiness
    {
        public KhoaBoMonDTO LayKhoaBoMon(int id)
        {
            try
            {
                var khoabm = model.KhoaBoMons.Where(s => s.ID == id).Select(s => new KhoaBoMonDTO
                {
                    ID = s.ID,
                    TenKhoaBoMon = s.TenKhoaBoMon,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return khoabm;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public int LayKhoaBoMonDaTonTai(string tenkhoa)
        {
            try
            {
                return model.KhoaBoMons.Where(s => s.TenKhoaBoMon == tenkhoa).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CheckLoiKhoaBoMonDaTonTai(int? id)
        {
            try
            {
                return model.MonHocs.Where(s => s.IDKhoaBoMon == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<KhoaBoMonDTO> LayDanhSachKhoaBoMon()
        {
            try
            {
                var listkhoabm = model.KhoaBoMons.Select(s => new KhoaBoMonDTO
                {
                    ID = s.ID,
                    TenKhoaBoMon = s.TenKhoaBoMon,
                    GhiChu = s.GhiChu
                }).ToList();
                return listkhoabm;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ThemKhoaBoMon(KhoaBoMonDTO khoabm)
        {
            try
            {
                var newkhoabm = new KhoaBoMon();
                newkhoabm.ID = khoabm.ID;
                newkhoabm.TenKhoaBoMon = khoabm.TenKhoaBoMon;
                newkhoabm.GhiChu = khoabm.GhiChu;

                model.KhoaBoMons.Add(newkhoabm);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public bool XoaKhoaBoMon(int id)
        {
            try
            {
                var monhoc = model.KhoaBoMons.Where(s => s.ID == id).FirstOrDefault();
                model.KhoaBoMons.Remove(monhoc);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public  bool SuaKhoaBoMon(KhoaBoMonDTO khoabm)
        {
            try
            {
                var khoabms = model.KhoaBoMons.Where(s => s.ID == khoabm.ID).FirstOrDefault();
                khoabms.ID = khoabm.ID;
                khoabms.TenKhoaBoMon = khoabm.TenKhoaBoMon;
                khoabms.GhiChu = khoabm.GhiChu;

                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}