using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.DTO
{
    public class ChuongTrinhDaoTaoDTO
    {
        public int ID { get; set; }
        public int? IDMonHoc { get; set; }
        public string Ma_MonHoc { get; set; }
        public string Ten_MonHoc { get; set; }
        public int SoTinChi_MonHoc { get; set; }
        public int SoTietLyThuyet_MonHoc { get; set; }
        public int SoTietThucHanh_MonHoc { get; set; }       
        public int? IDKhoaDaoTao { get; set; }
        public string Ten_KhoaDaoTao { get; set; }
        public int? IDHocKi { get; set; }
        public string Ten_HocKi { get; set; }

        public int? IDHocPhanTienQuyet { get; set; }
        public int? IDMonHocTienQuyet { get; set; }
        public string TenMonHocTienQuyet { get; set; }

        public int? IDHocPhanHocTruoc { get; set; }
        public int? IDMonHocHocTruoc { get; set; }
        public string TenMonHocHocTruoc { get; set; }

        public int? IDKhoaBoMon { get; set; }
        public string TenKhoaBoMon { get; set; }
        public int? IDPhanLoaiMonHoc { get; set; }
        public string LoaiMonHoc { get; set; }
        
    }
}