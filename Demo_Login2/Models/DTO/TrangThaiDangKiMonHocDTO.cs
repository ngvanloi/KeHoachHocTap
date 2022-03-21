using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.DTO
{
    public class TrangThaiDangKiMonHocDTO
    {
        public int ID { get; set; }
        public int? IDKhoaDaoTao { get; set; }
        public int? IDHocKi { get; set; }
        public string ThoiGianBatDau { get; set; }
        public string ThoiGianKetThuc { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
}