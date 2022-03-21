using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class PhanLoaiMonHoc
    {
        [Key]
        public int ID { get; set; }
        public string LoaiMonHoc { get; set; }
        public string GhiChu { get; set; }
        
        public ICollection<MonHocKhoaDaoTao> MonHocKhoaDaoTaos_IDPhanLoaiMonHoc { get; set; }
        public ICollection<ChuongTrinhDaoTao_Moi> ChuongTrinhDaoTao_Mois_IDPhanLoaiMonHoc { get; set; }
        public ICollection<KeHoachHocTap_Moi> KeHoachHocTap_Mois_IDPhanLoaiMonHoc { get; set; }
        public ICollection<SinhVienDangKiKeHoachHocTap> SinhVienDangKiKeHoachHocTaps_IDPhanLoaiMonHoc { get; set; }
    }
}