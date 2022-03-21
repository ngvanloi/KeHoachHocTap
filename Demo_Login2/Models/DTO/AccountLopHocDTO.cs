using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.DTO
{
    public class AccountLopHocDTO
    {
        public int ID { get; set; }
        public int Name { get; set; }
        public string Ma { get; set; }
        public int? IDLopHoc { get; set; }
        public int? IDAccount { get; set; }
        public bool IsDisable { get; set; }
        public string GhiChu { get; set; }
    }
}