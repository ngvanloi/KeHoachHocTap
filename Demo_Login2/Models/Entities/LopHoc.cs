using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class LopHoc
    {
        [Key]
        public int ID { get; set; }
        public int? IDKhoaDaoTao { get; set; }
        [ForeignKey("IDKhoaDaoTao")]
        public KhoaDaoTao KhoaDaoTao { get; set; }
        public string TenLop { get; set; }
        public string GhiChu { get; set; }
        public ICollection<AccountLopHoc> AccountLopHocs_IDLopHoc { get; set; }
        public ICollection<SinhVienLopHoc> SinhVienLopHocs_IDLopHoc { get; set; }
        public ICollection<MonHocSinhVienDangKi> MonHocSinhVienDangKis_IDLopHoc { get; set; }
        public ICollection<SinhVienDangKiKeHoachHocTap> SinhVienDangKiKeHoachHocTaps_IDLopHoc { get; set; }
    }
        
}