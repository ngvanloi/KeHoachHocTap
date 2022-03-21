using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.DTO
{
    public class KetQuaHocTapDTO
    {
        public int ID { get; set; }
        public int? IDAccount { get; set; }
        public int? IDMonHoc { get; set; }
        public int? IDHocPhanTienQuyet { get; set; }
        public int? IDHocPhanHocTruoc { get; set; }
        public int SoTinChi { get; set; }
        public double Diem { get; set; }
        public string DiemChu { get; set; }
        public bool KetQua { get; set; }
        public string GhiChu { get; set; }
    }
}