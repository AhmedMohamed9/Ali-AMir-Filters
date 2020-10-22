using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ali_Amir_Filters.Models.ViewModel
{
    public class Accountance_summary
    {

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("تاريخ الحسابات")]
        public DateTime acc_date { get; set; }
        [DisplayName("مصروفات")]
        public decimal money_out { get; set; }
        [DisplayName("ايرادات")]
        public decimal money_in { get; set; }
        [DisplayName("عمولات")]
        public decimal money_commission { get; set; }
        [DisplayName("صافى ربح")]
        public decimal money_profit { get; set; }

    }
}