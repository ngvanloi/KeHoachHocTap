using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.DTO
{
    public class MonHocSinhVienDangKiDTO
    {
        public int ID { get; set; }
        public int? IDMonHoc { get; set; }
        public string MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public int SoTinChi { get; set; }
        public int SoTietLyThuyet { get; set; }
        public int SoTietThucHanh { get; set; }

        public int? IDHocPhanTienQuyet { get; set; }
        public int? IDMonHocTienQuyet { get; set; }
        public string TenMonHocTienQuyet { get; set; }

        public int? IDHocPhanHocTruoc { get; set; }
        public int? IDMonHocHocTruoc { get; set; }
        public string TenMonHocHocTruoc { get; set; }

        public int? IDKhoaBoMon { get; set; }
        public string TenKhoaBoMon { get; set; }
        public int? IDAccount { get; set; }       
        public string HoVaTen { get; set; }
        public int? IDHocKi { get; set; }
        public string TenHocKi { get; set; }

        public int? IDKhoaDaoTao { get; set; }
        public string TenKhoaDaoTao { get; set; }

        public int? IDLopHoc { get; set; }
        public string TenLop { get; set; }

        public double Diem { get; set; }
        public int LoaiDangKi { get; set; }
        public bool TrangThai { get; set; }

        public int ChoPhepDangKi { get; set; }
    }
}