using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class KhoaBoMon
    {
        [Key]
        public int ID { get; set; }
        public string TenKhoaBoMon { get; set; }
        public string GhiChu { get; set; }
        public ICollection<MonHoc> MonHocs_IDKhoaBoMon { get; set; }
    }
}