using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class PhanLoaiHocKi
    {
        [Key]
        public int ID { get; set; }
        public string LoaiHocKi { get; set; }
        public string GhiChu { get; set; }
        public ICollection<HocKi> HocKis_IDPhanLoaiHocKi { get; set; }
    }
}