using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.DTO
{
    public class LopHocDTO
    {
        public int ID { get; set; }
        public int? IDKhoaDaoTao { get; set; }
        public string TenLop { get; set; }
        public string GhiChu { get; set; }
    }
}