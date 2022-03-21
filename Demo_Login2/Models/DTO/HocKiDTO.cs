using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.DTO
{
    public class HocKiDTO
    {
        public int ID { get; set; }
        public string TenHocKi { get; set; }
        public int? IDPhanLoaiHocKi { get; set; }
        public int ThangBatDau { get; set; }
        public int ThangKetThuc { get; set; }
        public string GhiChu { get; set; }
    }
}