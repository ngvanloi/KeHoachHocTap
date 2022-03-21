using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.DTO
{
    public class HocPhanTienQuyetDTO
    {
        public int ID { get; set; }
        public int? IDMonHoc { get; set; }
        public int? IDMonHocTienQuyet { get; set; }
        public string GhiChu { get; set; }
    }
}