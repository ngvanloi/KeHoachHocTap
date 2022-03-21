using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class HocKi
    {
        [Key]
        public int ID { get; set; }
        public string TenHocKi { get; set; }
        public int? IDPhanLoaiHocKi { get; set; }
        [ForeignKey("IDPhanLoaiHocKi")]
        public PhanLoaiHocKi PhanLoaiHocKi { get; set; }
        public int ThangBatDau { get; set; }
        public int ThangKetThuc { get; set; }
        public string GhiChu { get; set; }
        public ICollection<MonHocKhoaDaoTao> MonHocKhoaDaoTaos_IDHocKi { get; set; }
        public ICollection<TrangThaiDangKiMonHoc> TrangThaiDangKiMonHocs_IDHocKi { get; set; }
        public ICollection<MonHocSinhVienDangKi> MonHocSinhVienDangKis_IDHocKi { get; set; }
        public ICollection<ChuongTrinhDaoTao_Moi> ChuongTrinhDaoTao_Mois_IDHocKi { get; set; }
        public ICollection<KeHoachHocTap_Moi> KeHoachHocTap_Mois_IDHocKi { get; set; }
        public ICollection<SinhVienDangKiKeHoachHocTap> SinhVienDangKiKeHoachHocTaps_IDHocKi { get; set; }
    }
}