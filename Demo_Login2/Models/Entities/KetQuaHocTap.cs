using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class KetQuaHocTap
    {
        public int ID { get; set; }
        public int? IDAccount { get; set; }
        [ForeignKey("IDAccount")]
        public Account Account { get; set; }
        public int? IDMonHoc { get; set; }  
        [ForeignKey("IDMonHoc")]
        public MonHoc MonHoc { get; set; }
        public int? IDHocPhanTienQuyet { get; set; }
        [ForeignKey("IDHocPhanTienQuyet")]
        public HocPhanTienQuyet HocPhanTienQuyet { get; set; }
        public int? IDHocPhanHocTruoc { get; set; }
        [ForeignKey("IDHocPhanHocTruoc")]
        public HocPhanHocTruoc HocPhanHocTruoc { get; set; }
        public int SoTinChi { get; set; }
        public double Diem { get; set; }

        public string DiemChu { get; set; }
        public bool KetQua { get; set; }
        public string GhiChu { get; set; }
        
    }
}