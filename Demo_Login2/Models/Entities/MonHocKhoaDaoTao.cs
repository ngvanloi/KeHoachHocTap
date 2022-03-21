using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class MonHocKhoaDaoTao
    {
        [Key]
        public int ID { get; set; }
        public int? IDMonHoc { get; set; }
        [ForeignKey("IDMonHoc")]
        public MonHoc MonHoc { get; set; }
        public int? IDKhoaDaoTao { get; set; }
        [ForeignKey("IDKhoaDaoTao")]
        public KhoaDaoTao KhoaDaoTao { get; set; }
        public int? IDHocKi { get; set; }
        [ForeignKey("IDHocKi")]
        public HocKi HocKi { get; set; }
        public int? IDPhanLoaiMonHoc { get; set; }
        [ForeignKey("IDPhanLoaiMonHoc")]
        public PhanLoaiMonHoc PhanLoaiMonHoc { get; set; }
        public int? IDHocPhanTienQuyet { get; set; }
        [ForeignKey("IDHocPhanTienQuyet")]
        public HocPhanTienQuyet HocPhanTienQuyet { get; set; }
        public int? IDHocPhanHocTruoc { get; set; }
        [ForeignKey("IDHocPhanHocTruoc")]
        public HocPhanHocTruoc HocPhanHocTruoc { get; set; }
        public string GhiChu { get; set; }
    }
}