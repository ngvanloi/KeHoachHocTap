using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class LoaiHinhDaoTao
    {
        [Key]
        public int ID { get; set; }
        public string TenLoaiHinh { get; set; }
        public string GhiChu { get; set; }
        public ICollection<KhoaDaoTao> KhoaDaoTaos_IDLoaiHinhDaoTao { get; set; }
    }
}