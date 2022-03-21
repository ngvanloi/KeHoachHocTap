using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class HocPhanHocTruoc
    {
        [Key]
        public int ID { get; set; }
        public int? IDMonHoc { get; set; }
        [ForeignKey("IDMonHoc")]
        public MonHoc MonHoc { get; set; }
        public int? IDMonHocHocTruoc { get; set; }
        [ForeignKey("IDMonHocHocTruoc")]
        public MonHoc MonHocHocTruoc { get; set; }
        public string GhiChu { get; set; }
        //public ICollection<MonHoc> MonHocs_IDHocPhanHocTruoc { get; set; }
        public ICollection<MonHocKhoaDaoTao> MonHocKhoaDaoTaos_IDHocPhanHocTruoc { get; set; }
        public ICollection<MonHocSinhVienDangKi> MonHocSinhVienDangKis_IDHocPhanHocTruoc { get; set; }
        public ICollection<KetQuaHocTap> KetQuaHocTaps_IDHocPhanHocTruoc { get; set; } 
        
    }
}