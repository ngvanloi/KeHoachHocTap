namespace Demo_Login2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _04122021 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountLopHocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        Ma = c.String(),
                        IDLopHoc = c.Int(),
                        IDAccount = c.Int(),
                        IsDisabled = c.Boolean(nullable: false),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.IDAccount)
                .ForeignKey("dbo.LopHocs", t => t.IDLopHoc)
                .Index(t => t.IDLopHoc)
                .Index(t => t.IDAccount);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ma = c.String(),
                        HoVaTen = c.String(),
                        MailVL = c.String(),
                        PhanLoai = c.Int(nullable: false),
                        DaXem = c.Boolean(nullable: false),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.KetQuaHocTaps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDAccount = c.Int(),
                        IDMonHoc = c.Int(),
                        IDHocPhanTienQuyet = c.Int(),
                        IDHocPhanHocTruoc = c.Int(),
                        SoTinChi = c.Int(nullable: false),
                        Diem = c.Double(nullable: false),
                        DiemChu = c.String(),
                        KetQua = c.Boolean(nullable: false),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.IDAccount)
                .ForeignKey("dbo.HocPhanHocTruocs", t => t.IDHocPhanHocTruoc)
                .ForeignKey("dbo.HocPhanTienQuyets", t => t.IDHocPhanTienQuyet)
                .ForeignKey("dbo.MonHocs", t => t.IDMonHoc)
                .Index(t => t.IDAccount)
                .Index(t => t.IDMonHoc)
                .Index(t => t.IDHocPhanTienQuyet)
                .Index(t => t.IDHocPhanHocTruoc);
            
            CreateTable(
                "dbo.HocPhanHocTruocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMonHoc = c.Int(),
                        IDMonHocHocTruoc = c.Int(),
                        GhiChu = c.String(),
                        MonHoc_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MonHocs", t => t.MonHoc_ID)
                .ForeignKey("dbo.MonHocs", t => t.IDMonHoc)
                .ForeignKey("dbo.MonHocs", t => t.IDMonHocHocTruoc)
                .Index(t => t.IDMonHoc)
                .Index(t => t.IDMonHocHocTruoc)
                .Index(t => t.MonHoc_ID);
            
            CreateTable(
                "dbo.MonHocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDKhoaBoMon = c.Int(),
                        MaMonHoc = c.String(),
                        TenMonHoc = c.String(),
                        SoTietLyThuyet = c.Int(nullable: false),
                        SoTietThucHanh = c.Int(nullable: false),
                        SoTinChi = c.Int(nullable: false),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.KhoaBoMons", t => t.IDKhoaBoMon)
                .Index(t => t.IDKhoaBoMon);
            
            CreateTable(
                "dbo.ChuongTrinhDaoTao_Moi",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMonHoc = c.Int(),
                        SoTinChi = c.Int(nullable: false),
                        SoTietLyThuyet = c.Int(nullable: false),
                        SoTietThucHanh = c.Int(nullable: false),
                        IDKhoaDaoTao = c.Int(),
                        IDHocKi = c.Int(),
                        IDPhanLoaiMonHoc = c.Int(),
                        TenMonHocTienQuyet = c.String(),
                        TenMonHocHocTruoc = c.String(),
                        KhoiKienThuc = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HocKis", t => t.IDHocKi)
                .ForeignKey("dbo.KhoaDaoTaos", t => t.IDKhoaDaoTao)
                .ForeignKey("dbo.PhanLoaiMonHocs", t => t.IDPhanLoaiMonHoc)
                .ForeignKey("dbo.MonHocs", t => t.IDMonHoc)
                .Index(t => t.IDMonHoc)
                .Index(t => t.IDKhoaDaoTao)
                .Index(t => t.IDHocKi)
                .Index(t => t.IDPhanLoaiMonHoc);
            
            CreateTable(
                "dbo.HocKis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenHocKi = c.String(),
                        IDPhanLoaiHocKi = c.Int(),
                        ThangBatDau = c.Int(nullable: false),
                        ThangKetThuc = c.Int(nullable: false),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhanLoaiHocKis", t => t.IDPhanLoaiHocKi)
                .Index(t => t.IDPhanLoaiHocKi);
            
            CreateTable(
                "dbo.KeHoachHocTap_Moi",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMonHoc = c.Int(),
                        SoTinChi = c.Int(nullable: false),
                        SoTietLyThuyet = c.Int(nullable: false),
                        SoTietThucHanh = c.Int(nullable: false),
                        IDKhoaDaoTao = c.Int(),
                        IDHocKi = c.Int(),
                        IDPhanLoaiMonHoc = c.Int(),
                        TenMonHocTienQuyet = c.String(),
                        TenMonHocHocTruoc = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HocKis", t => t.IDHocKi)
                .ForeignKey("dbo.KhoaDaoTaos", t => t.IDKhoaDaoTao)
                .ForeignKey("dbo.PhanLoaiMonHocs", t => t.IDPhanLoaiMonHoc)
                .ForeignKey("dbo.MonHocs", t => t.IDMonHoc)
                .Index(t => t.IDMonHoc)
                .Index(t => t.IDKhoaDaoTao)
                .Index(t => t.IDHocKi)
                .Index(t => t.IDPhanLoaiMonHoc);
            
            CreateTable(
                "dbo.KhoaDaoTaos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenKhoaDaoTao = c.String(),
                        NienKhoa = c.Int(nullable: false),
                        IDLoaiHinhDaoTao = c.Int(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LoaiHinhDaoTaos", t => t.IDLoaiHinhDaoTao)
                .Index(t => t.IDLoaiHinhDaoTao);
            
            CreateTable(
                "dbo.LoaiHinhDaoTaos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenLoaiHinh = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LopHocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDKhoaDaoTao = c.Int(),
                        TenLop = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.KhoaDaoTaos", t => t.IDKhoaDaoTao)
                .Index(t => t.IDKhoaDaoTao);
            
            CreateTable(
                "dbo.MonHocSinhVienDangKis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMonHoc = c.Int(),
                        IDAccount = c.Int(),
                        IDHocKi = c.Int(),
                        IDKhoaDaoTao = c.Int(),
                        IDLopHoc = c.Int(),
                        IDHocPhanTienQuyet = c.Int(),
                        IDHocPhanHocTruoc = c.Int(),
                        LoaiDangKi = c.Int(nullable: false),
                        TrangThai = c.Boolean(nullable: false),
                        ChoPhepDangKi = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.IDAccount)
                .ForeignKey("dbo.HocKis", t => t.IDHocKi)
                .ForeignKey("dbo.HocPhanHocTruocs", t => t.IDHocPhanHocTruoc)
                .ForeignKey("dbo.HocPhanTienQuyets", t => t.IDHocPhanTienQuyet)
                .ForeignKey("dbo.KhoaDaoTaos", t => t.IDKhoaDaoTao)
                .ForeignKey("dbo.LopHocs", t => t.IDLopHoc)
                .ForeignKey("dbo.MonHocs", t => t.IDMonHoc)
                .Index(t => t.IDMonHoc)
                .Index(t => t.IDAccount)
                .Index(t => t.IDHocKi)
                .Index(t => t.IDKhoaDaoTao)
                .Index(t => t.IDLopHoc)
                .Index(t => t.IDHocPhanTienQuyet)
                .Index(t => t.IDHocPhanHocTruoc);
            
            CreateTable(
                "dbo.HocPhanTienQuyets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMonHoc = c.Int(),
                        IDMonHocTienQuyet = c.Int(),
                        GhiChu = c.String(),
                        MonHoc_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MonHocs", t => t.IDMonHoc)
                .ForeignKey("dbo.MonHocs", t => t.IDMonHocTienQuyet)
                .ForeignKey("dbo.MonHocs", t => t.MonHoc_ID)
                .Index(t => t.IDMonHoc)
                .Index(t => t.IDMonHocTienQuyet)
                .Index(t => t.MonHoc_ID);
            
            CreateTable(
                "dbo.MonHocKhoaDaoTaos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMonHoc = c.Int(),
                        IDKhoaDaoTao = c.Int(),
                        IDHocKi = c.Int(),
                        IDPhanLoaiMonHoc = c.Int(),
                        IDHocPhanTienQuyet = c.Int(),
                        IDHocPhanHocTruoc = c.Int(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HocKis", t => t.IDHocKi)
                .ForeignKey("dbo.HocPhanHocTruocs", t => t.IDHocPhanHocTruoc)
                .ForeignKey("dbo.HocPhanTienQuyets", t => t.IDHocPhanTienQuyet)
                .ForeignKey("dbo.KhoaDaoTaos", t => t.IDKhoaDaoTao)
                .ForeignKey("dbo.MonHocs", t => t.IDMonHoc)
                .ForeignKey("dbo.PhanLoaiMonHocs", t => t.IDPhanLoaiMonHoc)
                .Index(t => t.IDMonHoc)
                .Index(t => t.IDKhoaDaoTao)
                .Index(t => t.IDHocKi)
                .Index(t => t.IDPhanLoaiMonHoc)
                .Index(t => t.IDHocPhanTienQuyet)
                .Index(t => t.IDHocPhanHocTruoc);
            
            CreateTable(
                "dbo.PhanLoaiMonHocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LoaiMonHoc = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SinhVienDangKiKeHoachHocTaps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDMonHoc = c.Int(),
                        SoTinChi = c.Int(nullable: false),
                        SoTietLyThuyet = c.Int(nullable: false),
                        SoTietThucHanh = c.Int(nullable: false),
                        IDAccount = c.Int(),
                        IDHocKi = c.Int(),
                        IDKhoaDaoTao = c.Int(),
                        IDLopHoc = c.Int(),
                        TenMonHocTienQuyet = c.String(),
                        TenMonHocHocTruoc = c.String(),
                        IDPhanLoaiMonHoc = c.Int(),
                        TrangThai = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.IDAccount)
                .ForeignKey("dbo.HocKis", t => t.IDHocKi)
                .ForeignKey("dbo.KhoaDaoTaos", t => t.IDKhoaDaoTao)
                .ForeignKey("dbo.LopHocs", t => t.IDLopHoc)
                .ForeignKey("dbo.MonHocs", t => t.IDMonHoc)
                .ForeignKey("dbo.PhanLoaiMonHocs", t => t.IDPhanLoaiMonHoc)
                .Index(t => t.IDMonHoc)
                .Index(t => t.IDAccount)
                .Index(t => t.IDHocKi)
                .Index(t => t.IDKhoaDaoTao)
                .Index(t => t.IDLopHoc)
                .Index(t => t.IDPhanLoaiMonHoc);
            
            CreateTable(
                "dbo.SinhVienLopHocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        Ma = c.String(),
                        IDAccount = c.Int(),
                        IDLopHoc = c.Int(),
                        IsDisable = c.Boolean(nullable: false),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.IDAccount)
                .ForeignKey("dbo.LopHocs", t => t.IDLopHoc)
                .Index(t => t.IDAccount)
                .Index(t => t.IDLopHoc);
            
            CreateTable(
                "dbo.TrangThaiDangKiMonHocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDKhoaDaoTao = c.Int(),
                        IDHocKi = c.Int(),
                        ThoiGianBatDau = c.String(),
                        ThoiGianKetThuc = c.String(),
                        TrangThai = c.Boolean(nullable: false),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HocKis", t => t.IDHocKi)
                .ForeignKey("dbo.KhoaDaoTaos", t => t.IDKhoaDaoTao)
                .Index(t => t.IDKhoaDaoTao)
                .Index(t => t.IDHocKi);
            
            CreateTable(
                "dbo.PhanLoaiHocKis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LoaiHocKi = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.KhoaBoMons",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenKhoaBoMon = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PhanLoaiTaiKhoans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LoaiTaiKhoan = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HocPhanHocTruocs", "IDMonHocHocTruoc", "dbo.MonHocs");
            DropForeignKey("dbo.HocPhanHocTruocs", "IDMonHoc", "dbo.MonHocs");
            DropForeignKey("dbo.MonHocs", "IDKhoaBoMon", "dbo.KhoaBoMons");
            DropForeignKey("dbo.KetQuaHocTaps", "IDMonHoc", "dbo.MonHocs");
            DropForeignKey("dbo.HocPhanTienQuyets", "MonHoc_ID", "dbo.MonHocs");
            DropForeignKey("dbo.HocPhanHocTruocs", "MonHoc_ID", "dbo.MonHocs");
            DropForeignKey("dbo.ChuongTrinhDaoTao_Moi", "IDMonHoc", "dbo.MonHocs");
            DropForeignKey("dbo.HocKis", "IDPhanLoaiHocKi", "dbo.PhanLoaiHocKis");
            DropForeignKey("dbo.KeHoachHocTap_Moi", "IDMonHoc", "dbo.MonHocs");
            DropForeignKey("dbo.TrangThaiDangKiMonHocs", "IDKhoaDaoTao", "dbo.KhoaDaoTaos");
            DropForeignKey("dbo.TrangThaiDangKiMonHocs", "IDHocKi", "dbo.HocKis");
            DropForeignKey("dbo.SinhVienLopHocs", "IDLopHoc", "dbo.LopHocs");
            DropForeignKey("dbo.SinhVienLopHocs", "IDAccount", "dbo.Accounts");
            DropForeignKey("dbo.MonHocSinhVienDangKis", "IDMonHoc", "dbo.MonHocs");
            DropForeignKey("dbo.MonHocSinhVienDangKis", "IDLopHoc", "dbo.LopHocs");
            DropForeignKey("dbo.MonHocSinhVienDangKis", "IDKhoaDaoTao", "dbo.KhoaDaoTaos");
            DropForeignKey("dbo.HocPhanTienQuyets", "IDMonHocTienQuyet", "dbo.MonHocs");
            DropForeignKey("dbo.MonHocSinhVienDangKis", "IDHocPhanTienQuyet", "dbo.HocPhanTienQuyets");
            DropForeignKey("dbo.SinhVienDangKiKeHoachHocTaps", "IDPhanLoaiMonHoc", "dbo.PhanLoaiMonHocs");
            DropForeignKey("dbo.SinhVienDangKiKeHoachHocTaps", "IDMonHoc", "dbo.MonHocs");
            DropForeignKey("dbo.SinhVienDangKiKeHoachHocTaps", "IDLopHoc", "dbo.LopHocs");
            DropForeignKey("dbo.SinhVienDangKiKeHoachHocTaps", "IDKhoaDaoTao", "dbo.KhoaDaoTaos");
            DropForeignKey("dbo.SinhVienDangKiKeHoachHocTaps", "IDHocKi", "dbo.HocKis");
            DropForeignKey("dbo.SinhVienDangKiKeHoachHocTaps", "IDAccount", "dbo.Accounts");
            DropForeignKey("dbo.MonHocKhoaDaoTaos", "IDPhanLoaiMonHoc", "dbo.PhanLoaiMonHocs");
            DropForeignKey("dbo.KeHoachHocTap_Moi", "IDPhanLoaiMonHoc", "dbo.PhanLoaiMonHocs");
            DropForeignKey("dbo.ChuongTrinhDaoTao_Moi", "IDPhanLoaiMonHoc", "dbo.PhanLoaiMonHocs");
            DropForeignKey("dbo.MonHocKhoaDaoTaos", "IDMonHoc", "dbo.MonHocs");
            DropForeignKey("dbo.MonHocKhoaDaoTaos", "IDKhoaDaoTao", "dbo.KhoaDaoTaos");
            DropForeignKey("dbo.MonHocKhoaDaoTaos", "IDHocPhanTienQuyet", "dbo.HocPhanTienQuyets");
            DropForeignKey("dbo.MonHocKhoaDaoTaos", "IDHocPhanHocTruoc", "dbo.HocPhanHocTruocs");
            DropForeignKey("dbo.MonHocKhoaDaoTaos", "IDHocKi", "dbo.HocKis");
            DropForeignKey("dbo.HocPhanTienQuyets", "IDMonHoc", "dbo.MonHocs");
            DropForeignKey("dbo.KetQuaHocTaps", "IDHocPhanTienQuyet", "dbo.HocPhanTienQuyets");
            DropForeignKey("dbo.MonHocSinhVienDangKis", "IDHocPhanHocTruoc", "dbo.HocPhanHocTruocs");
            DropForeignKey("dbo.MonHocSinhVienDangKis", "IDHocKi", "dbo.HocKis");
            DropForeignKey("dbo.MonHocSinhVienDangKis", "IDAccount", "dbo.Accounts");
            DropForeignKey("dbo.LopHocs", "IDKhoaDaoTao", "dbo.KhoaDaoTaos");
            DropForeignKey("dbo.AccountLopHocs", "IDLopHoc", "dbo.LopHocs");
            DropForeignKey("dbo.KhoaDaoTaos", "IDLoaiHinhDaoTao", "dbo.LoaiHinhDaoTaos");
            DropForeignKey("dbo.KeHoachHocTap_Moi", "IDKhoaDaoTao", "dbo.KhoaDaoTaos");
            DropForeignKey("dbo.ChuongTrinhDaoTao_Moi", "IDKhoaDaoTao", "dbo.KhoaDaoTaos");
            DropForeignKey("dbo.KeHoachHocTap_Moi", "IDHocKi", "dbo.HocKis");
            DropForeignKey("dbo.ChuongTrinhDaoTao_Moi", "IDHocKi", "dbo.HocKis");
            DropForeignKey("dbo.KetQuaHocTaps", "IDHocPhanHocTruoc", "dbo.HocPhanHocTruocs");
            DropForeignKey("dbo.KetQuaHocTaps", "IDAccount", "dbo.Accounts");
            DropForeignKey("dbo.AccountLopHocs", "IDAccount", "dbo.Accounts");
            DropIndex("dbo.TrangThaiDangKiMonHocs", new[] { "IDHocKi" });
            DropIndex("dbo.TrangThaiDangKiMonHocs", new[] { "IDKhoaDaoTao" });
            DropIndex("dbo.SinhVienLopHocs", new[] { "IDLopHoc" });
            DropIndex("dbo.SinhVienLopHocs", new[] { "IDAccount" });
            DropIndex("dbo.SinhVienDangKiKeHoachHocTaps", new[] { "IDPhanLoaiMonHoc" });
            DropIndex("dbo.SinhVienDangKiKeHoachHocTaps", new[] { "IDLopHoc" });
            DropIndex("dbo.SinhVienDangKiKeHoachHocTaps", new[] { "IDKhoaDaoTao" });
            DropIndex("dbo.SinhVienDangKiKeHoachHocTaps", new[] { "IDHocKi" });
            DropIndex("dbo.SinhVienDangKiKeHoachHocTaps", new[] { "IDAccount" });
            DropIndex("dbo.SinhVienDangKiKeHoachHocTaps", new[] { "IDMonHoc" });
            DropIndex("dbo.MonHocKhoaDaoTaos", new[] { "IDHocPhanHocTruoc" });
            DropIndex("dbo.MonHocKhoaDaoTaos", new[] { "IDHocPhanTienQuyet" });
            DropIndex("dbo.MonHocKhoaDaoTaos", new[] { "IDPhanLoaiMonHoc" });
            DropIndex("dbo.MonHocKhoaDaoTaos", new[] { "IDHocKi" });
            DropIndex("dbo.MonHocKhoaDaoTaos", new[] { "IDKhoaDaoTao" });
            DropIndex("dbo.MonHocKhoaDaoTaos", new[] { "IDMonHoc" });
            DropIndex("dbo.HocPhanTienQuyets", new[] { "MonHoc_ID" });
            DropIndex("dbo.HocPhanTienQuyets", new[] { "IDMonHocTienQuyet" });
            DropIndex("dbo.HocPhanTienQuyets", new[] { "IDMonHoc" });
            DropIndex("dbo.MonHocSinhVienDangKis", new[] { "IDHocPhanHocTruoc" });
            DropIndex("dbo.MonHocSinhVienDangKis", new[] { "IDHocPhanTienQuyet" });
            DropIndex("dbo.MonHocSinhVienDangKis", new[] { "IDLopHoc" });
            DropIndex("dbo.MonHocSinhVienDangKis", new[] { "IDKhoaDaoTao" });
            DropIndex("dbo.MonHocSinhVienDangKis", new[] { "IDHocKi" });
            DropIndex("dbo.MonHocSinhVienDangKis", new[] { "IDAccount" });
            DropIndex("dbo.MonHocSinhVienDangKis", new[] { "IDMonHoc" });
            DropIndex("dbo.LopHocs", new[] { "IDKhoaDaoTao" });
            DropIndex("dbo.KhoaDaoTaos", new[] { "IDLoaiHinhDaoTao" });
            DropIndex("dbo.KeHoachHocTap_Moi", new[] { "IDPhanLoaiMonHoc" });
            DropIndex("dbo.KeHoachHocTap_Moi", new[] { "IDHocKi" });
            DropIndex("dbo.KeHoachHocTap_Moi", new[] { "IDKhoaDaoTao" });
            DropIndex("dbo.KeHoachHocTap_Moi", new[] { "IDMonHoc" });
            DropIndex("dbo.HocKis", new[] { "IDPhanLoaiHocKi" });
            DropIndex("dbo.ChuongTrinhDaoTao_Moi", new[] { "IDPhanLoaiMonHoc" });
            DropIndex("dbo.ChuongTrinhDaoTao_Moi", new[] { "IDHocKi" });
            DropIndex("dbo.ChuongTrinhDaoTao_Moi", new[] { "IDKhoaDaoTao" });
            DropIndex("dbo.ChuongTrinhDaoTao_Moi", new[] { "IDMonHoc" });
            DropIndex("dbo.MonHocs", new[] { "IDKhoaBoMon" });
            DropIndex("dbo.HocPhanHocTruocs", new[] { "MonHoc_ID" });
            DropIndex("dbo.HocPhanHocTruocs", new[] { "IDMonHocHocTruoc" });
            DropIndex("dbo.HocPhanHocTruocs", new[] { "IDMonHoc" });
            DropIndex("dbo.KetQuaHocTaps", new[] { "IDHocPhanHocTruoc" });
            DropIndex("dbo.KetQuaHocTaps", new[] { "IDHocPhanTienQuyet" });
            DropIndex("dbo.KetQuaHocTaps", new[] { "IDMonHoc" });
            DropIndex("dbo.KetQuaHocTaps", new[] { "IDAccount" });
            DropIndex("dbo.AccountLopHocs", new[] { "IDAccount" });
            DropIndex("dbo.AccountLopHocs", new[] { "IDLopHoc" });
            DropTable("dbo.PhanLoaiTaiKhoans");
            DropTable("dbo.KhoaBoMons");
            DropTable("dbo.PhanLoaiHocKis");
            DropTable("dbo.TrangThaiDangKiMonHocs");
            DropTable("dbo.SinhVienLopHocs");
            DropTable("dbo.SinhVienDangKiKeHoachHocTaps");
            DropTable("dbo.PhanLoaiMonHocs");
            DropTable("dbo.MonHocKhoaDaoTaos");
            DropTable("dbo.HocPhanTienQuyets");
            DropTable("dbo.MonHocSinhVienDangKis");
            DropTable("dbo.LopHocs");
            DropTable("dbo.LoaiHinhDaoTaos");
            DropTable("dbo.KhoaDaoTaos");
            DropTable("dbo.KeHoachHocTap_Moi");
            DropTable("dbo.HocKis");
            DropTable("dbo.ChuongTrinhDaoTao_Moi");
            DropTable("dbo.MonHocs");
            DropTable("dbo.HocPhanHocTruocs");
            DropTable("dbo.KetQuaHocTaps");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountLopHocs");
        }
    }
}
