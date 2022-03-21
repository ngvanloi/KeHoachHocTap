using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class HocPhanTienQuyet
    {
        [Key]
        public int ID { get; set; }
        public int? IDMonHoc { get; set; }
        [ForeignKey("IDMonHoc")]
        public MonHoc MonHoc { get; set; }
        public int? IDMonHocTienQuyet { get; set; }
        [ForeignKey("IDMonHocTienQuyet")]
        public MonHoc MonHocTienQuyet { get; set; }
        public string GhiChu { get; set; }
        //public ICollection<MonHoc> MonHocs_IDHocPhanTienQuyet { get; set; }
        public ICollection<MonHocKhoaDaoTao> MonHocKhoaDaoTaos_IDHocPhanTienQuyet { get; set; }
        public ICollection<MonHocSinhVienDangKi> MonHocSinhVienDangKis_IDHocPhanTienQuyet { get; set; }
        public ICollection<KetQuaHocTap> KetQuaHocTaps_IDHocPhanTienQuyet { get; set; }
        
    }
}