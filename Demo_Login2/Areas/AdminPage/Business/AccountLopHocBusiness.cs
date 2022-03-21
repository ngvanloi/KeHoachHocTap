using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class AccountLopHocBusiness : BaseBusiness
    {
        public AccountLopHocDTO LayAccountLopHoc(int id)
        {
            try
            {
                var acclop = model.AccountLopHocs.Where(s => s.ID == id).Select(s => new AccountLopHocDTO
                {
                    ID = s.ID,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    Name = s.Name,
                    Ma = s.Ma,
                    IsDisable = s.IsDisabled,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return acclop;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int LayLopHocDaTonTai(int? id)
        {
            try
            {
                return model.AccountLopHocs.Where(s => s.IDLopHoc == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int LayGiangVienDaTonTai(int? id)
        {
            try
            {
                return model.AccountLopHocs.Where(s => s.IDAccount == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<AccountLopHocDTO> LayDanhSachAccountLopHoc()
        {
            try
            {
                var listAcclop = model.AccountLopHocs.Select(s => new AccountLopHocDTO
                {
                    ID = s.ID,
                    IDAccount = s.IDAccount,
                    IDLopHoc = s.IDLopHoc,
                    Name = s.Name,
                    Ma = s.Ma,
                    IsDisable = s.IsDisabled,
                    GhiChu = s.GhiChu
                }).ToList();
                return listAcclop;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AccountLopHocDTO> LayDanhSachAccountLopHocTheoKhoaDT(int id)
        {
            try
            {
                if (id == 0)
                {
                    var listAcclop = model.AccountLopHocs.Select(s => new AccountLopHocDTO
                    {
                        ID = s.ID,
                        IDAccount = s.IDAccount,
                        IDLopHoc = s.IDLopHoc,
                        Name = s.Name,
                        Ma = s.Ma,
                        IsDisable = s.IsDisabled,
                        GhiChu = s.GhiChu
                    }).ToList();
                    return listAcclop;
                }
                else
                {
                    var listAcclop = model.AccountLopHocs.Where(s => s.LopHoc.KhoaDaoTao.ID == id).Select(s => new AccountLopHocDTO
                    {
                        ID = s.ID,
                        IDAccount = s.IDAccount,
                        IDLopHoc = s.IDLopHoc,
                        Name = s.Name,
                        IsDisable = s.IsDisabled,
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
        public bool ThemAccountLopHoc(AccountLopHocDTO acclop)
        {
            try
            {
                var newacclop = new AccountLopHoc();
                newacclop.IDAccount = acclop.IDAccount;
                newacclop.IDLopHoc = acclop.IDLopHoc;               
                newacclop.GhiChu = acclop.GhiChu;
                model.AccountLopHocs.Add(newacclop);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool XoaAccountLopHoc(int id)
        {
            try
            {
                var acclop = model.AccountLopHocs.Where(s => s.ID == id).FirstOrDefault();
                model.AccountLopHocs.Remove(acclop);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SuaAccountLopHoc(AccountLopHocDTO acclop)
        {
            try
            {
                var accoutlop = model.AccountLopHocs.Where(s => s.ID == acclop.ID).FirstOrDefault();
                accoutlop.ID = acclop.ID;
                accoutlop.IDAccount = acclop.IDAccount;
                accoutlop.IDLopHoc = acclop.IDLopHoc;
                accoutlop.Name = acclop.Name;
                accoutlop.Ma = acclop.Ma;
                accoutlop.IsDisabled = acclop.IsDisable;
                accoutlop.GhiChu = acclop.GhiChu;
                model.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int LayIDAccountTheoMa(string magiangvien)
        {
            try
            {
                return model.Accounts.Where(s => s.Ma == magiangvien && s.PhanLoai == 2).Select(s => s.ID).FirstOrDefault();
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
                var check = model.AccountLopHocs.Where(s => s.IDAccount == idAccount).Select(s => s.ID).FirstOrDefault();
                return check;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public int Check_LopHocTonTai(int idLopHoc)
        {
            try
            {
                var check = model.AccountLopHocs.Where(s => s.IDLopHoc == idLopHoc).Select(s => s.ID).FirstOrDefault();
                return check;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public int LayIDAccountTheoMail(string mailvl)
        //{
        //    try
        //    {
        //        return model.Accounts.Where(s => s.MailVL == mailvl).Select(s => s.ID).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}