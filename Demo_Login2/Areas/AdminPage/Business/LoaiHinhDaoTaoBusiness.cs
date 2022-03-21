using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class LoaiHinhDaoTaoBusiness : BaseBusiness
    {
        public LoaiHinhDaoTaoDTO LayLoaiHinhDaoTao(int id)
        {
            try
            {
                var loaihinhdt = model.LoaiHinhDaoTaos.Where(s => s.ID == id).Select(s => new LoaiHinhDaoTaoDTO
                {
                    ID = s.ID,
                    TenLoaiHinh = s.TenLoaiHinh,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return loaihinhdt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int LayLoaiHinhDaoTaoDaTonTai(string tenloaihinh)
        {
            try
            {
                return model.LoaiHinhDaoTaos.Where(s => s.TenLoaiHinh == tenloaihinh).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CheckLoiLoaiHinhDaoTaoDaTonTai(int? id)
        {
            try
            {
                return model.KhoaDaoTaos.Where(s => s.IDLoaiHinhDaoTao == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<LoaiHinhDaoTaoDTO> LayDanhSachLoaiHinhDaoTao()
        {
            try
            {
                var listloaihinhDT = model.LoaiHinhDaoTaos.Select(s => new LoaiHinhDaoTaoDTO
                {
                    ID = s.ID,
                    TenLoaiHinh = s.TenLoaiHinh,
                    GhiChu = s.GhiChu
                }).ToList();
                return listloaihinhDT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ThemLoaiHinhDaoTao(LoaiHinhDaoTaoDTO loaihinh)
        {
            try
            {
                var newloaihinhDT = new LoaiHinhDaoTao();
                newloaihinhDT.ID = loaihinh.ID;
                newloaihinhDT.TenLoaiHinh = loaihinh.TenLoaiHinh;
                newloaihinhDT.GhiChu = loaihinh.GhiChu;

                model.LoaiHinhDaoTaos.Add(newloaihinhDT);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool XoaLoaiHinhDaoTao(int id)
        {
            try
            {
                var loaihinhDT = model.LoaiHinhDaoTaos.Where(s => s.ID == id).FirstOrDefault();
                model.LoaiHinhDaoTaos.Remove(loaihinhDT);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SuaLoaiHinhDaoTao(LoaiHinhDaoTaoDTO loaihinh)
        {
            try
            {
                var loaihinhDT = model.LoaiHinhDaoTaos.Where(s => s.ID == loaihinh.ID).FirstOrDefault();
                loaihinhDT.ID = loaihinh.ID;
                loaihinhDT.TenLoaiHinh = loaihinh.TenLoaiHinh;
                loaihinhDT.GhiChu = loaihinh.GhiChu;

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