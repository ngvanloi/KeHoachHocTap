using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models
{
    public class KHHTDbContext : DbContext
    {
        public KHHTDbContext() : base("KHHTConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountLopHoc> AccountLopHocs { get; set; }
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<KhoaBoMon> KhoaBoMons { get; set; }
        public DbSet<HocPhanTienQuyet> HocPhanTienQuyets { get; set; }
        public DbSet<HocKi> HocKies { get; set; }
        //public DbSet<DaoTaoHocKi> DaoTaoHocKis { get; set; }
        //public DbSet<SinhVienKeHoachHocTap> SinhVienKeHoachHocTaps { get; set; }
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<KhoaDaoTao> KhoaDaoTaos { get; set; }
        //public DbSet<KeHoachDaoTao> KeHoachDaoTaos { get; set; }
        //public DbSet<KeHoachHocTap> KeHoachHocTaps { get; set; }
        public DbSet<LoaiHinhDaoTao> LoaiHinhDaoTaos { get; set; }
        public DbSet<PhanLoaiHocKi> PhanLoaiHocKis { get; set; }
        public DbSet<PhanLoaiTaiKhoan> PhanLoaiTaiKhoans { get; set; }
        public DbSet<SinhVienLopHoc> SinhVienLopHocs { get; set; }
        public DbSet<MonHocKhoaDaoTao> MonHocKhoaDaoTaos { get; set; }
        public DbSet<PhanLoaiMonHoc> PhanLoaiMonHocs { get; set; }
        public DbSet<HocPhanHocTruoc> HocPhanHocTruocs { get; set; }
        public DbSet<TrangThaiDangKiMonHoc> TrangThaiDangKiMonHocs { get; set; }
        public DbSet<KetQuaHocTap> KetQuaHocTaps { get; set; }
        public DbSet<MonHocSinhVienDangKi> MonHocSinhVienDangKis { get; set; }
        public DbSet<ChuongTrinhDaoTao_Moi> ChuongTrinhDaoTao_Mois { get; set; }
        public DbSet<KeHoachHocTap_Moi> KeHoachHocTap_Mois { get; set; }
        public DbSet<SinhVienDangKiKeHoachHocTap> SinhVienDangKiKeHoachHocTaps { get; set; }
        //public DbSet<HocPhanTienQuyet2> HocPhanTienQuyet2s { get; set; }

        //public System.Data.Entity.DbSet<Demo_Login2.Models.DTO.AccountDTO> AccountDTOes { get; set; }
    }
}