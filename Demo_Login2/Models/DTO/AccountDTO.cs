using Demo_Login2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo_Login2.Models.DTO
{
    public class AccountDTO
    {
        public int ID { get; set; }
        public string Ma { get; set; }
        //public int? IDKhoaDaoTao { get; set; }
        public string HoVaTen { get; set; }
        public string MailVL { get; set; }
        public int PhanLoai { get; set; }
        public bool DaXem { get; set; }
        public string GhiChu { get; set; }
    }
}