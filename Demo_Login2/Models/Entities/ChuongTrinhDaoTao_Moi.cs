using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class ChuongTrinhDaoTao_Moi
    {
        [Key]
        public int ID { get; set; }
        public int? IDMonHoc { get; set; }
        [ForeignKey("IDMonHoc")]
        public MonHoc MonHoc { get; set; }
        public int SoTinChi { get; set; }
        public int SoTietLyThuyet { get; set; }
        public int SoTietThucHanh { get; set; }

        public int? IDKhoaDaoTao { get; set; }
        [ForeignKey("IDKhoaDaoTao")]
        public KhoaDaoTao KhoaDaoTao { get; set; }
        public int? IDHocKi { get; set; }
        [ForeignKey("IDHocKi")]
        public HocKi HocKi { get; set; }
        public int? IDPhanLoaiMonHoc { get; set; }
        [ForeignKey("IDPhanLoaiMonHoc")]
        public PhanLoaiMonHoc PhanLoaiMonHoc { get; set; }
        public string TenMonHocTienQuyet { get; set; }
        
        public string TenMonHocHocTruoc { get; set; }
        
        public string KhoiKienThuc { get; set; }
        public string GhiChu { get; set; }
    }
}