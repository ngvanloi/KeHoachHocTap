using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.DTO
{
    public class MonHocKhoaDaoTaoDTO
    {
        public int ID { get; set; }
        public int? IDMonHoc { get; set; }
        public int? IDKhoaDaoTao { get; set; }
        public int? IDHocKi { get; set; }
        public int? IDPhanLoaiMonHoc { get; set; }
        public int? IDHocPhanTienQuyet { get; set; }
        public int? IDHocPhanHocTruoc { get; set; }
        public string GhiChu { get; set; }
    }
}