using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class MonHocKhoaDaoTaoBusiness : BaseBusiness
    {
        public MonHocKhoaDaoTaoDTO LayMonHocKhoaDaoTao(int id)
        {
            try
            {
                var monhockhoaDT = model.MonHocKhoaDaoTaos.Where(s => s.ID == id).Select(s => new MonHocKhoaDaoTaoDTO
                {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    IDHocKi = s.IDHocKi,
                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    IDHocPhanTienQuyet = s.IDHocPhanTienQuyet,
                    IDHocPhanHocTruoc = s.IDHocPhanHocTruoc,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return monhockhoaDT;
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public MonHocKhoaDaoTaoDetailsDTO LayMonHocKhoaDaoTao_Details(int id)
        {
            try
            {
                var monhockhoaDT = model.MonHocKhoaDaoTaos.Where(s => s.ID == id).Select(s => new MonHocKhoaDaoTaoDetailsDTO
                {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    IDHocKi = s.IDHocKi,
                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    TenMonHocTienQuyet = s.HocPhanTienQuyet.MonHocTienQuyet.TenMonHoc,
                    TenMonHocHocTruoc = s.HocPhanHocTruoc.MonHocHocTruoc.TenMonHoc,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return monhockhoaDT;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<MonHocKhoaDaoTaoDTO> LayDanhSachMonHocKhoaDaoTao()
        {
            try
            {
                var monhockhoaDT = model.MonHocKhoaDaoTaos.Select(s => new MonHocKhoaDaoTaoDTO
                {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    IDKhoaDaoTao = s.IDKhoaDaoTao,
                    IDHocKi = s.IDHocKi,
                    IDPhanLoaiMonHoc = s.IDPhanLoaiMonHoc,
                    IDHocPhanTienQuyet = s.HocPhanTienQuyet.IDMonHocTienQuyet,
                    IDHocPhanHocTruoc = s.HocPhanHocTruoc.IDMonHocHocTruoc,
                    GhiChu = s.GhiChu    
                }).ToList();
                return monhockhoaDT;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ThemMonHocKhoaDaoTao(MonHocKhoaDaoTaoDTO monhockhoaDT)
        {
            try
            {
                var newmonhockhoaDT = new MonHocKhoaDaoTao();
                newmonhockhoaDT.ID = monhockhoaDT.ID;
                newmonhockhoaDT.IDMonHoc = monhockhoaDT.IDMonHoc;
                newmonhockhoaDT.IDKhoaDaoTao = monhockhoaDT.IDKhoaDaoTao;
                newmonhockhoaDT.IDHocKi = monhockhoaDT.IDHocKi;
                newmonhockhoaDT.IDPhanLoaiMonHoc = monhockhoaDT.IDPhanLoaiMonHoc;
                newmonhockhoaDT.IDHocPhanTienQuyet = monhockhoaDT.IDHocPhanTienQuyet;
                newmonhockhoaDT.IDHocPhanHocTruoc = monhockhoaDT.IDHocPhanHocTruoc;
                newmonhockhoaDT.GhiChu = monhockhoaDT.GhiChu;

                model.MonHocKhoaDaoTaos.Add(newmonhockhoaDT);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool XoaMonHocKhoaDaoTao(int id)
        {
            try
            {
                var monhockhoaDT = model.MonHocKhoaDaoTaos.Where(s => s.ID == id).FirstOrDefault();
                model.MonHocKhoaDaoTaos.Remove(monhockhoaDT);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool SuaMonHocKhoaDaoTao(MonHocKhoaDaoTaoDTO monhockhoaDT)
        {
            try
            {
                var monhockhoaDTs = model.MonHocKhoaDaoTaos.Where(s => s.ID == monhockhoaDT.ID).FirstOrDefault();
                monhockhoaDTs.ID = monhockhoaDT.ID;
                monhockhoaDTs.IDMonHoc = monhockhoaDT.IDMonHoc;
                monhockhoaDTs.IDKhoaDaoTao = monhockhoaDT.IDKhoaDaoTao;
                monhockhoaDTs.IDHocKi = monhockhoaDT.IDHocKi;
                monhockhoaDTs.IDPhanLoaiMonHoc = monhockhoaDT.IDPhanLoaiMonHoc;
                monhockhoaDTs.IDHocPhanTienQuyet = monhockhoaDT.IDHocPhanTienQuyet;
                monhockhoaDTs.IDHocPhanHocTruoc = monhockhoaDT.IDHocPhanHocTruoc;
                monhockhoaDTs.GhiChu = monhockhoaDT.GhiChu;
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}