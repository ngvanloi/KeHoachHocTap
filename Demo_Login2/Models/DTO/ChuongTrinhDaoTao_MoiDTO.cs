using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.DTO
{
    public class ChuongTrinhDaoTao_MoiDTO
    {
        public int ID { get; set; }
        public int? IDMonHoc { get; set; }
        public string MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public int SoTinChi { get; set; }
        public int SoTietLyThuyet { get; set; }
        public int SoTietThucHanh { get; set; }

        public int? IDKhoaDaoTao { get; set; }
        public string TenKhoaDaoTao { get; set; }

        public int? IDHocKi { get; set; }
        public string TenHocKi { get; set; }


        public string TenMonHocTienQuyet { get; set; }

        public string TenMonHocHocTruoc { get; set; }

        public int? IDKhoaBoMon { get; set; }
        public string TenKhoaBoMon { get; set; }

        public int? IDPhanLoaiMonHoc { get; set; }
        public string LoaiMonHoc { get; set; }

        public string KhoiKienThuc { get; set; }
        public string GhiChu { get; set; }
    }
}