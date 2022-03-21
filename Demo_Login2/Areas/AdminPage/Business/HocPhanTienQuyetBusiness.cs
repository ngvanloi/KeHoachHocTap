using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class HocPhanTienQuyetBusiness : BaseBusiness
    {
        public HocPhanTienQuyetDTO LayHocPhanTienQuyet(int id)
        {
            try
            {
                var hocphanTQ = model.HocPhanTienQuyets.Where(s => s.ID == id).Select(s => new HocPhanTienQuyetDTO
                {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    IDMonHocTienQuyet = s.IDMonHocTienQuyet,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return hocphanTQ;
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }



        public int LayHocPhanTienQuyetDaTonTai(int? id)
        {
            try
            {
                return model.HocPhanTienQuyets.Where(s => s.IDMonHocTienQuyet == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int CheckLoiHocPhanTienQuyetDaTonTai(int? id)
        {
            try
            {
                return model.MonHocKhoaDaoTaos.Where(s => s.IDHocPhanTienQuyet == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public List<HocPhanTienQuyetHienThiTrongMonHocKhoaDTO> LayDanhSachHocPhanTienQuyetTheoMonHocKhoa(int id)
        {
            try
            {
                var lsthocphanTQ = model.HocPhanTienQuyets.Where(s => s.IDMonHoc == id).Select(s => new HocPhanTienQuyetHienThiTrongMonHocKhoaDTO
                {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    IDMonHocTienQuyet = s.IDMonHocTienQuyet,
                    TenMonHocTienQuyet = s.MonHocTienQuyet.TenMonHoc,
                    GhiChu = s.GhiChu
                }).ToList();
                return lsthocphanTQ;
            }catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public List<HocPhanTienQuyetDTO> LayDanhSachHocPhanTienQuyet()
        {
            try
            {
                var lsthocphanTQ = model.HocPhanTienQuyets.Select(s => new HocPhanTienQuyetDTO
                {
                    ID = s.ID,
                    IDMonHoc = s.IDMonHoc,
                    IDMonHocTienQuyet = s.IDMonHocTienQuyet,
                    GhiChu = s.GhiChu
                }).ToList();
                return lsthocphanTQ;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ThemHocPhanTienQuyet(HocPhanTienQuyetDTO hocphanTQ)
        {
            try
            {
                var newhocphanTQ = new HocPhanTienQuyet();
                newhocphanTQ.ID = hocphanTQ.ID;
                newhocphanTQ.IDMonHoc = hocphanTQ.IDMonHoc;
                newhocphanTQ.IDMonHocTienQuyet = hocphanTQ.IDMonHocTienQuyet;
                newhocphanTQ.GhiChu = hocphanTQ.GhiChu;

                model.HocPhanTienQuyets.Add(newhocphanTQ);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool XoaHocPhanTienQuyet(int id)
        {
            try
            {
                var hocphanTQ = model.HocPhanTienQuyets.Where(s => s.ID == id).FirstOrDefault();
                model.HocPhanTienQuyets.Remove(hocphanTQ);
                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool SuaHocPhanTienQuyet(HocPhanTienQuyetDTO hocphanTQ)
        {
            try
            {
                var hocphanTQs = model.HocPhanTienQuyets.Where(s => s.ID == hocphanTQ.ID).FirstOrDefault();
                hocphanTQs.ID = hocphanTQ.ID;
                hocphanTQs.IDMonHoc = hocphanTQ.IDMonHoc;
                hocphanTQs.IDMonHocTienQuyet = hocphanTQ.IDMonHocTienQuyet;
                hocphanTQs.GhiChu = hocphanTQ.GhiChu;

                model.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}