using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.DTO
{
    public class KhoaDaoTaoDTO
    {
        public int ID { get; set; }
        public string TenKhoaDaoTao { get; set; }
        public int NienKhoa { get; set; }
        public int? IDLoaiHinhDaoTao { get; set; }
        public string GhiChu { get; set; }
    }
}