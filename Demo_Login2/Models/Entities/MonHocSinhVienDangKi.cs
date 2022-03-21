using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class MonHocSinhVienDangKi
    {
        public int ID { get; set; }
        public int? IDMonHoc { get; set; }
        [ForeignKey("IDMonHoc")]
        public MonHoc MonHoc { get; set; }
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

        public int? IDHocPhanTienQuyet { get; set; }
        [ForeignKey("IDHocPhanTienQuyet")]
        public HocPhanTienQuyet HocPhanTienQuyet { get; set; }
        public int? IDHocPhanHocTruoc { get; set; }
        [ForeignKey("IDHocPhanHocTruoc")]
        public HocPhanHocTruoc HocPhanHocTruoc { get; set; }
        public int LoaiDangKi { get; set; }
        public bool TrangThai { get; set; }

        public int ChoPhepDangKi { get; set; }
    }
}