using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class SinhVienLopHocBusiness : BaseBusiness
    {
        public SinhVienLopHocDTO LaySinhVienLopHoc(int id)
        {
            try
            {
                var svlop = model.SinhVienLopHocs.Where(s => s.ID == id).Select(s => new SinhVienLopHocDTO
                {
                    ID = s.ID,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    Name = s.Name,
                    Ma = s.Ma,
                    IsDisable = s.IsDisable,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return svlop;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int LaySinhVienLopHocDaTonTai(int? id)
        {
            try
            {
                return model.SinhVienLopHocs.Where(s => s.IDAccount == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SinhVienLopHocDTO> LayDanhSachSinhVienLopHoc()
        {
            try
            {
                var listsvlop = model.SinhVienLopHocs.Select(s => new SinhVienLopHocDTO
                {
                    ID = s.ID,
                    Name = s.Name,
                    Ma = s.Ma,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    IsDisable = s.IsDisable,
                    GhiChu = s.GhiChu
                }).ToList();
                return listsvlop;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<SinhVienLopHocDTO> LayDanhSachSinhVienLopHocTheoKhoaDT(int id)
        {
            try
            {
                if (id == 0)
                {
                    var listAcclop = model.SinhVienLopHocs.Select(s => new SinhVienLopHocDTO
                    {
                        ID = s.ID,
                        Name = s.Name,
                        Ma = s.Ma,
                        IDAccount = s.IDAccount,
                        IDLopHoc = s.IDLopHoc,
                        IsDisable = s.IsDisable,
                        GhiChu = s.GhiChu
                    }).ToList();
                    return listAcclop;
                }
                else
                {
                    var listAcclop = model.SinhVienLopHocs.Where(s => s.LopHoc.KhoaDaoTao.ID == id).Select(s => new SinhVienLopHocDTO
                    {
                        ID = s.ID,
                        Name = s.Name,
                        Ma = s.Ma,
                        IDAccount = s.IDAccount,
                        IDLopHoc = s.IDLopHoc,
                        IsDisable = s.IsDisable,
                        GhiChu = s.GhiChu
                    }).ToList();
                    return listAcclop;
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public bool ThemSinhVienLopHoc(SinhVienLopHocDTO svlop)
        {
            try
            {
                var newsvlop = new SinhVienLopHoc();
                newsvlop.ID = svlop.ID;
                newsvlop.Name = svlop.Name;
                newsvlop.Ma = svlop.Ma;
                newsvlop.IDAccount = svlop.IDAccount;
                newsvlop.IDLopHoc = svlop.IDLopHoc;
                newsvlop.IsDisable = svlop.IsDisable;
                newsvlop.GhiChu = svlop.GhiChu;
                model.SinhVienLopHocs.Add(newsvlop);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool XoaSinhVienLopHoc(int id)
        {
            try
            {
                var svlop = model.SinhVienLopHocs.Where(s => s.ID == id).FirstOrDefault();
                model.SinhVienLopHocs.Remove(svlop);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SuaSinhVienLopHoc(SinhVienLopHocDTO svlop)
        {
            try
            {
                var svlops = model.SinhVienLopHocs.Where(s => s.ID == svlop.ID).FirstOrDefault();
                svlops.ID = svlop.ID;
                svlops.Name = svlop.Name;
                svlops.Ma = svlop.Ma;
                svlops.IDAccount = svlop.IDAccount;
                svlops.IDLopHoc = svlop.IDLopHoc;
                svlops.IsDisable = svlop.IsDisable;
                svlops.GhiChu = svlop.GhiChu;
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int LayIDAccountTheoMa(string masinhvien)
        {
            try
            {
                return model.Accounts.Where(s => s.Ma == masinhvien && s.PhanLoai == 1).Select(s => s.ID).FirstOrDefault();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public int LayIDLopHocTheoTen(string lophoc)
        {
            try
            {
                return model.LopHocs.Where(s => s.TenLop == lophoc).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Check_TaiKhoanTonTai(int idAccount)
        {
            try
            {
                var checktaikhoantontai = model.SinhVienLopHocs.Where(s => s.IDAccount == idAccount).Select(s => s.ID).FirstOrDefault();
                return checktaikhoantontai;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}