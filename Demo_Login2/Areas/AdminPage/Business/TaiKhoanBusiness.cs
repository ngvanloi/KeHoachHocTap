using Demo_Login2.Models.DTO;
using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Areas.AdminPage.Business
{
    public class TaiKhoanBusiness : BaseBusiness
    {
        public AccountDTO LayTaiKhoan(int id)
        {
            try
            {
                var acc = model.Accounts.Where(s => s.ID == id).Select(s => new AccountDTO
                {
                    ID = s.ID,
                    Ma = s.Ma,
                    //IDKhoaDaoTao = s.IDKhoaDaoTao,
                    HoVaTen = s.HoVaTen,
                    MailVL = s.MailVL,
                    PhanLoai = s.PhanLoai,
                    DaXem = s.DaXem,
                    GhiChu = s.GhiChu
                }).FirstOrDefault();
                return acc;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<AccountDTO> LayDanhSachTaiKhoan()
        {
            try
            {
                var listAcc = model.Accounts.Select(s => new AccountDTO
                {
                    ID = s.ID,
                    Ma = s.Ma,
                    //IDKhoaDaoTao = s.IDKhoaDaoTao,
                    HoVaTen = s.HoVaTen,
                    MailVL = s.MailVL,
                    PhanLoai = s.PhanLoai,
                    DaXem = s.DaXem,
                    GhiChu = s.GhiChu
                }).ToList();
                return listAcc;
            }
            catch (Exception ex)
            {

                throw ex;
            }  
        }
        public bool ThemTaiKhoan(AccountDTO account)
        {
            try
            {
                var newItem = new Account();
                newItem.ID = account.ID;
                newItem.Ma = account.Ma;
                //newItem.IDKhoaDaoTao = account.IDKhoaDaoTao;
                newItem.HoVaTen = account.HoVaTen;
                newItem.MailVL = account.MailVL;
                newItem.PhanLoai = account.PhanLoai;
                newItem.DaXem = account.DaXem;
                newItem.GhiChu = account.GhiChu;

                model.Accounts.Add(newItem);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool XoaTaiKhoan(int id)
        {
            try
            {
                var acc = model.Accounts.Where(s => s.ID == id).FirstOrDefault();
                model.Accounts.Remove(acc);
                model.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SuaTaiKhoan(AccountDTO account)
        {
            try
            {
                var acc = model.Accounts.Where(s => s.ID == account.ID).FirstOrDefault();
                acc.HoVaTen = account.HoVaTen;
                acc.Ma = account.Ma;
                //acc.IDKhoaDaoTao = account.IDKhoaDaoTao;
                acc.HoVaTen = account.HoVaTen;
                acc.MailVL = account.MailVL;
                acc.PhanLoai = account.PhanLoai;
                acc.DaXem = account.DaXem;
                acc.GhiChu = account.GhiChu;

                model.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int LayLoaiTaiKhoan(string mailvl)
        {
            try
            {
                return model.Accounts.Where(s => s.MailVL == mailvl).Select(s => s.PhanLoai).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int CheckLoiGiangVienDaTonTai(int? id)
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

        public int CheckLoiSinhVienDaTonTai(int? id)
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

        public int CheckLoiSinhVienDaTonTai_KetQuaHocTap(int? id)
        {
            try
            {
                return model.KetQuaHocTaps.Where(s => s.IDAccount == id).Select(s => s.ID).FirstOrDefault();
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
                return model.SinhVienDangKiKeHoachHocTaps.Where(s => s.IDAccount == id).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int LayMailDaTonTai(string mailvl)
        {
            try
            {
                return model.Accounts.Where(s => s.MailVL == mailvl).Select(s => s.ID).FirstOrDefault();
            }catch(Exception)
            {
                throw;
            }
        }

        public int LayMaDaTonTai(string ma)
        {
            try
            {
                return model.Accounts.Where(s => s.Ma == ma).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<AccountDTO> LayDanhSachTaiKhoan_GiangVien()
        {
            try
            {
                var laydanhsachgv = model.Accounts.Where(s => s.PhanLoai == 2).Select(s => new AccountDTO
                {
                    ID = s.ID,
                    Ma = s.Ma,
                    HoVaTen = s.HoVaTen,
                    MailVL = s.MailVL,
                    PhanLoai = s.PhanLoai,
                    DaXem = s.DaXem,
                    GhiChu = s.GhiChu
                }).ToList();
                return laydanhsachgv;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AccountDTO> LayDanhSachTaiKhoan_GiangVienDeChon(int id)
        {
            try
            {
                var laydanhsachgv = model.Accounts.Where(s => s.PhanLoai == 2 && s.ID == id).Select(s => new AccountDTO
                {
                    ID = s.ID,
                    Ma = s.Ma,
                    HoVaTen = s.HoVaTen,
                    MailVL = s.MailVL,
                    PhanLoai = s.PhanLoai,
                    DaXem = s.DaXem,
                    GhiChu = s.GhiChu
                }).ToList();
                return laydanhsachgv;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AccountDTO> LayDanhSachTaiKhoan_SinhVienDeChon(int id)
        {
            try
            {
                var laydanhsachsv = model.Accounts.Where(s => s.PhanLoai == 1 && s.ID == id).Select(s => new AccountDTO
                {
                    ID = s.ID,
                    Ma = s.Ma,
                    HoVaTen = s.HoVaTen,
                    MailVL = s.MailVL,
                    PhanLoai = s.PhanLoai,
                    DaXem = s.DaXem,
                    GhiChu = s.GhiChu
                }).ToList();
                return laydanhsachsv;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<AccountDTO> LayDanhSachTaiKhoan_SinhVien()
        {
            try
            {
                var lstsvlophoc = model.Accounts.Where(s => s.PhanLoai == 1).Select(s => new AccountDTO
                {
                    ID = s.ID,
                    Ma = s.Ma,
                    HoVaTen = s.HoVaTen,
                    MailVL = s.MailVL,
                    PhanLoai = s.PhanLoai,
                    DaXem = s.DaXem,
                    GhiChu = s.GhiChu
                }).ToList();
                return lstsvlophoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int LayMonHocTheoAccountDaTonTai(int? idMonHoc, int? idAccount)
        {
            try
            {
                return model.KetQuaHocTaps.Where(s => s.IDMonHoc == idMonHoc && s.IDAccount == idAccount).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int? LayKhoaDaoTaoChoTaiKhoan(string mailvl)
        {
            try
            {
                var idAccount = model.Accounts.Where(s => s.MailVL == mailvl).Select(s => s.ID).FirstOrDefault();
                var idLopHoc = model.SinhVienLopHocs.Where(s => s.IDAccount == idAccount).Select(s => s.IDLopHoc).FirstOrDefault();
                return model.LopHocs.Where(s => s.ID == idLopHoc).Select(s => s.IDKhoaDaoTao).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int? LayIDChoTaiKhoan(string mailvl)
        {
            try
            {
                return model.Accounts.Where(s => s.MailVL == mailvl).Select(s => s.ID).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int? LayLopHocChoTaiKhoan_SV(string mailvl)
        {
            try
            {
                var idAccount = model.Accounts.Where(s => s.MailVL == mailvl).Select(s => s.ID).FirstOrDefault();
                return model.SinhVienLopHocs.Where(s => s.IDAccount == idAccount).Select(s => s.IDLopHoc).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int? LayLopHocChoTaiKhoan_ChuNhiem(string mailvl)
        {
            try
            {
                var idAccount = model.Accounts.Where(s => s.MailVL == mailvl).Select(s => s.ID).FirstOrDefault();
                return model.AccountLopHocs.Where(s => s.IDAccount == idAccount).Select(s => s.IDLopHoc).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AccountDTO> LayDanhSachSinhVienTheoKhoaDaoTao(int idKhoaDT)
        {
            try
            {
                var lstsvkhoaDT = model.SinhVienLopHocs.Where(s => s.LopHoc.IDKhoaDaoTao == idKhoaDT).Select(s => new AccountDTO
                {
                    ID = s.Account.ID,
                    Ma = s.Account.Ma,
                    HoVaTen = s.Account.HoVaTen,
                    MailVL = s.Account.MailVL,
                    PhanLoai = s.Account.PhanLoai,
                    DaXem = s.Account.DaXem,
                    GhiChu = s.Account.GhiChu
                }).ToList();
                return lstsvkhoaDT;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<AccountDTO> LayDanhSachSinhVien()
        {
            try
            {
                var lstsvkhoaDT = model.SinhVienLopHocs.Select(s => new AccountDTO
                {
                    ID = s.Account.ID,
                    Ma = s.Account.Ma,
                    HoVaTen = s.Account.HoVaTen,
                    MailVL = s.Account.MailVL,
                    PhanLoai = s.Account.PhanLoai,
                    DaXem = s.Account.DaXem,
                    GhiChu = s.Account.GhiChu
                }).ToList();
                return lstsvkhoaDT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AccountDTO> LayDanhSachSinhVienTheoLopChuNhiem(int idLopHoc)
        {
            try
            {
                var lstsvlophoc = model.SinhVienLopHocs.Where(s => s.IDLopHoc == idLopHoc).Select(s => new AccountDTO
                {
                    ID = s.Account.ID,
                    Ma = s.Account.Ma,
                    HoVaTen = s.Account.HoVaTen,
                    MailVL = s.Account.MailVL,
                    PhanLoai = s.Account.PhanLoai,
                    DaXem = s.Account.DaXem,
                    GhiChu = s.Account.GhiChu
                }).ToList();
                return lstsvlophoc;
            }catch(Exception ex)
            {
                throw ex;
            }
        }







    }
}