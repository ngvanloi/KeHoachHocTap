using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class SinhVienDangKiKeHoachHocTap
    {
        [Key]
        public int ID { get; set; }
        public int? IDMonHoc { get; set; }
        [ForeignKey("IDMonHoc")]
        public MonHoc MonHoc { get; set; }
        public int SoTinChi { get; set; }
        public int SoTietLyThuyet { get; set; }
        public int SoTietThucHanh { get; set; }
        public int? IDAccount { get; set; }
        [ForeignKey("IDAccount")]
        public Account Account { get; set; }
        public int? IDHocKi { get; set; }
        [ForeignKey("IDHocKi")]
        public HocKi HocKi { get; set; }
        public int? IDKhoaDaoTao { get; set; }
        [ForeignKey("IDKhoaDaoTao")]
        public KhoaDaoTao KhoaDaoTao { get; set; }
        public int? IDLopHoc { get; set; }
        [ForeignKey("IDLopHoc")]
        public LopHoc LopHoc { get; set; }
        public string TenMonHocTienQuyet { get; set; }
        public string TenMonHocHocTruoc { get; set; }
        public int? IDPhanLoaiMonHoc { get; set; }
        [ForeignKey("IDPhanLoaiMonHoc")]
        public PhanLoaiMonHoc PhanLoaiMonHoc { get; set; }
        public bool TrangThai { get; set; }
    }
}