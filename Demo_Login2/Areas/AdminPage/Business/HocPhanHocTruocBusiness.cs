using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class HocPhanHocTruocBusiness : BaseBusiness
    {
        public HocPhanHocTruocDTO LayHocPhanHocTruoc(int id)
        {
            try
            {
                var hocphanHT = model.HocPhanHocTruocs.Where(s => s.ID == id).Select(s => new HocPhanHocTruocDTO
                {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    IDMonHocHocTruoc = s.IDMonHocHocTruoc,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return hocphanHT;
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public int LayHocPhanHocTruocDaTonTai(int? id)
        {
            try
            {
                return model.HocPhanHocTruocs.Where(s => s.IDMonHocHocTruoc == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CheckLoiHocPhanHocTruocDaTonTai(int? id)
        {
            try
            {
                return model.MonHocKhoaDaoTaos.Where(s => s.IDHocPhanHocTruoc == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<HocPhanHocTruocDTO> LayDanhSachHocPhanHocTruoc()
        {
            try
            {
                var lsthocphanHT = model.HocPhanHocTruocs.Select(s => new HocPhanHocTruocDTO
                {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    IDMonHocHocTruoc = s.IDMonHocHocTruoc,
                    GhiChu = s.GhiChu
                }).ToList();
                return lsthocphanHT;
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public bool ThemHocPhanHocTruoc(HocPhanHocTruocDTO hocphanHT)
        {
            try
            {
                var newhocphanHT = new HocPhanHocTruoc();
                newhocphanHT.ID = hocphanHT.ID;
                newhocphanHT.IDMonHoc = hocphanHT.IDMonHoc;
                newhocphanHT.IDMonHocHocTruoc = hocphanHT.IDMonHocHocTruoc;
                newhocphanHT.GhiChu = hocphanHT.GhiChu;

                model.HocPhanHocTruocs.Add(newhocphanHT);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex; 
            }
            
        }

        public bool XoaHocPhanHocTruoc(int id)
        {
            try
            {
                var hocphanHT = model.HocPhanHocTruocs.Where(s => s.ID == id).FirstOrDefault();
                model.HocPhanHocTruocs.Remove(hocphanHT);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool SuaHocPhanHocTruoc(HocPhanHocTruocDTO hocphanHT)
        {
            try
            {
                var hocphanHTs = model.HocPhanHocTruocs.Where(s => s.ID == hocphanHT.ID).FirstOrDefault();
                hocphanHTs.ID = hocphanHT.ID;
                hocphanHTs.IDMonHoc = hocphanHT.IDMonHoc;
                hocphanHTs.IDMonHocHocTruoc = hocphanHT.IDMonHocHocTruoc;
                hocphanHTs.GhiChu = hocphanHT.GhiChu;

                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<HocPhanHocTruocHienThiTrongMonHocKhoaDTO> LayDanhSachHocPhanHocTruocTheoMonHocKhoa(int id)
        {
            try
            {
                var lsthocphanHT = model.HocPhanHocTruocs.Where(s => s.IDMonHoc == id).Select(s => new HocPhanHocTruocHienThiTrongMonHocKhoaDTO
                {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    IDMonHocHocTruoc = s.IDMonHocHocTruoc,
                    TenMonHocHocTruoc = s.MonHocHocTruoc.TenMonHoc,
                    GhiChu = s.GhiChu
                }).ToList();
                return lsthocphanHT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}