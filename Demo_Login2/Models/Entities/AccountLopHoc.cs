using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.Entities
{
    public class AccountLopHoc
    {
        [Key]
        public int ID { get; set; }
        public int  Name { get; set; }
        public string Ma { get; set; }
        public int? IDLopHoc { get; set; }
        [ForeignKey("IDLopHoc")]
        public LopHoc LopHoc { get; set; }
        public int? IDAccount { get; set; }
        [ForeignKey("IDAccount")]
        public Account Account { get; set; }
        public bool IsDisabled { get; set; }
        public string GhiChu { get; set; }
  
    }
}